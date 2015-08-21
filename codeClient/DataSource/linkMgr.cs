using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nsDataMgr;
using System.Threading;
using System.Windows.Threading;
using System.Net;
using nsVicoClient.ctrls;
using System.Diagnostics;
using System.IO;

namespace nsVicoClient
{
    public class LinkMgr
    {
        dvBase dataBase;
        int iRlbNormal = -1;
        int iRlbNormal2 = -1;
        int iRlbMap = -1;
        static string plcIpAddr = "TCP:10.10.150.13";
        Lasal32.CB_RLADD_FUNCTYPE methptr;

        objUnit objHeart ;
        objUnit objHeartErr ;
        objUnit objHeartTick ;
        DispatcherTimer dtState = new DispatcherTimer();
        DispatcherTimer dtLink = new DispatcherTimer();
        List<string> ipLst = new List<string>();
        List<int> lstIRlb = new List<int>();

        public LinkMgr()
        {
            plcIpAddr = "TCP:" + getIpAddr();
            methptr = new Lasal32.CB_RLADD_FUNCTYPE(RefListCallBack);

            dtState.Tick += new EventHandler(dtState_Tick);
            dtState.Interval = new TimeSpan(0, 0, 0, 0, 126);
            dtLink.Tick += new EventHandler(dtLink_Tick);
            dtLink.Interval = new TimeSpan(0, 0, 0, 0, 100);
        }

        int errNr = 0;
        int curLinkValue = 0;
        int curTickValue = 0;
        int curHeartValue = 0;
        long linkErrTick = 0;
        private void dtState_Tick(object obj, EventArgs e)
        {
            if (curLinkValue == dataBase.SysPr[7].value || curTickValue == objHeartTick.value)
            {
                if (linkErrTick == 0)
                    linkErrTick = DateTime.Now.Ticks;
                if ( DateTime.Now.Ticks - linkErrTick > new TimeSpan(0, 0, 0, 1).Ticks)
                {
                    vm.getTm("Link PLC Error,Stop to link PLC ......\t");
                    dtState.Stop();

                }
                errNr++;
                if (errNr >= 2)
                {
                    vm.getTm("link error!! Start to relink PLC : ");
                    //valmoWin.heartError = true;
                 
                }
            }
            else
            {
                if (linkErrTick != 0)
                    linkErrTick = 0;
                curHeartValue++;
                objHeart.valueNew = curHeartValue;

                curTickValue = objHeartTick.value;
                curLinkValue = dataBase.SysPr[7].value;
                errNr = 0;
                //valmoWin.heartError = false;
            }
        }
        DateTime relinkInterval ;
        TimeSpan diffTm = new TimeSpan(0, 0, 0, 0, 500);
        private void dtLink_Tick(object obj, EventArgs e)
        {
            if (DateTime.Now - relinkInterval > diffTm)
            {
                if (checkLinkPlc())
                {
                    foreach (int iRlb in lstIRlb)
                    {
                        Lasal32.LslRefreshListDestroy(iRlb);
                    }
                    getLink();
                    dtLink.Stop();
                    valmoWin.execHandle(opeOrderType.winMsg, new WinMsg(WinMsgType.mwRelink));
                    dataBase.IprPr[16].valueNew = 1;
                    errNr = 0;
                    vm.printLn("reLinkPlc......");
                }
                else
                {
                    valmoWin.execHandle(opeOrderType.winMsg, new WinMsg(WinMsgType.mwLinkPlcError));
                }
                relinkInterval = DateTime.Now;
            }
        }
        public void dataInit(dvBase db)
        {
            dataBase = db;
            
            objHeartTick = dataBase[0];
            objHeart = dataBase[1];
            objHeartErr = dataBase[2];

            objHeartTick.addMap();
            //objHeart.addMap();
            //objHeartTick.addMap();
        }
        /// <summary>
        /// 创建回调list
        /// </summary>
        public void lstCreate()
        {
            iRlbNormal = Lasal32.LslRefreshListCreateExt(plcIpAddr + ";ApplID=110", 1, 0, 0, null, methptr, 2345, 10000);
            iRlbNormal2 = Lasal32.LslRefreshListCreateExt(plcIpAddr + ";ApplID=111", 1, 0, 0, null, methptr, 2345, 10000);
            iRlbMap = Lasal32.LslRefreshListCreateExt(plcIpAddr + ";ApplID=210", 1, 0, 0, null, methptr, 2346, 10000);
            lstIRlb.Clear();

            lstIRlb.Add(iRlbNormal);
            lstIRlb.Add(iRlbNormal2);
            lstIRlb.Add(iRlbMap);
        }
        /// <summary>
        /// 获取list中添加的对象的地址
        /// </summary>
        public void getLstObjAddr()
        {
            try
            {
                int len = dataBase.length;
                for (int i = 0; i < len; i++)
                {
                    objUnit obj = dataBase[i];
                    if (obj != null)
                    {
                        bool b = Lasal32.LslGetObject(obj.addrLocal, ref obj.addrPlc, ref Lasal32.Mode_0, obj.className);
                        if (!b)
                        {
                            vm.perror("[" + obj.serialNum + ".getPlcAddr] " + "\t" + obj.addrLocal);
                        }
                        else
                        {
                            if (obj.addrPlc != 0)
                            {
                                if (obj.serialNum[0] == 'K')
                                {
                                    if (obj.serialNum == "Key051")
                                    {
                                        obj.var.dwAddr = (UInt32)obj.addrPlc;
                                    }
                                    else
                                    {
                                        obj.var.dwAddr = (UInt32)obj.addrPlc + 12;
                                    }
                                }
                                else
                                    obj.var.dwAddr = (UInt32)obj.addrPlc;
                            }
                            else
                            {
                                vm.perror("[" + obj.serialNum + ".getPlcAddr] " + "\t" + obj.addrLocal);
                            }
                        }
                    }
                }
            }
            catch
            {
                vm.perror("[getLstAddr] error!");
            }
        }
        
        /// <summary>
        /// 添加对象到list中
        /// </summary>
        public void lstAdd()
        {
            int count_iRlbMap = 0;
            int len = dataBase.length;
            int lstNormalNr = 0;
            int lstMapNr = 0;
            int lstNr = 0;
            int curNormalNr = iRlbNormal;

            Lasal32.LslRefreshListClear(iRlbMap, Lasal32.CpReflist.RF_STATIC);
            Lasal32.LslRefreshListClear(iRlbNormal, Lasal32.CpReflist.RF_STATIC);
            Lasal32.LslRefreshListClear(iRlbNormal2, Lasal32.CpReflist.RF_STATIC);
            dataBase.SysPr[7].addMap();
            for (lstNr = 0; lstNr < len; lstNr++)
            {
                objUnit obj = dataBase[lstNr];
                if (obj != null)
                {
                    if (obj.lstSpdType == plcLstSpd.mapType)
                    {
                        if (obj.flagListAdded)
                        {
                            if (!Lasal32.LslRefreshListAdd(iRlbMap, ref obj.var, (uint)(lstNr + 1), 15, Lasal32.CpReflist.RF_STATIC))
                            {
                                vm.perror("[lstAdd] " + obj.serialNum);
                            }
                            else
                            {
                                lstMapNr++;
                                count_iRlbMap++;
                            }
                        }
                    }
                    else
                    {
                        if (obj.flagListAdded )
                        {
                            if (!Lasal32.LslRefreshListAdd(curNormalNr, ref obj.var, (uint)(lstNr + 1), 100, Lasal32.CpReflist.RF_STATIC))
                            {
                                vm.perror("[lstAdd] " + obj.serialNum);
                            }
                            else
                            {
                                lstNormalNr++;
                                count_iRlbMap++;
                            }
                            if (lstNormalNr >= 1000)
                            {
                                curNormalNr = iRlbNormal2;
                                lstNormalNr = 0;
                            }
                        }
                    }
                }
            }

            vm.printLn("iRlb count \t" + lstMapNr + "\t" + lstNormalNr + "\t" + count_iRlbMap);
        }
        public void RefListCallBack(uint dwCallbackData, uint dwAddr, uint dwVarID, int nData)
        {
            try
            {
                if (dataBase[(int)dwVarID - 1] != null)
                    dataBase[(int)dwVarID - 1].refresh(nData);
                if (!dtState.IsEnabled)
                {
                    dtState.Start();
                }
            }
            catch
            {
                vm.perror("[RefListCallBack] " + dwVarID + " " + nData);
            }
        }

        public void RefListHeartCallBack(uint dwCallbackData, uint dwAddr, uint dwVarID, int nData)
        {
            try
            {
                if (!dtState.IsEnabled)
                {
                    dtState.Start();
                }
                if (dwVarID == 888)
                {
                    if (!dtState.IsEnabled)
                    {
                        dtState.Start();
                    }
                    objHeart.refresh(nData);
                }
                else if (dwVarID == 111)
                {
                    objHeartErr.refresh(nData);
                }
                else if (dwVarID == 333)
                {
                    objHeartTick.refresh(nData);
                }
            }
            catch
            {
                vm.perror("RefListHeartCallBack : " + dwVarID + " " + nData);
            }
        }
        /// <summary>
        /// 启动list，开始数据通信
        /// </summary>
        /// <returns></returns>
        public bool lstStart()
        {
            foreach (int iRlb in lstIRlb)
            {
                if (!Lasal32.LslRefreshListStart(iRlb, Lasal32.CpReflist.RF_STATIC))
                {
                    vm.perror("[lstStart iRlb] error!");
                    return false;
                }
            }
            valmoWin.execHandle(opeOrderType.winMsg, new WinMsg(WinMsgType.mwLinkPlcOK));
            return true;
        }

        public static bool checkLinkPlc()
        {
            if (!Lasal32.LslRefreshListIsOnline(1))
            {
                Lasal32.Offline();
                bool b = Lasal32.Online(plcIpAddr, 1, 0, 0, 0);
                return b;
            }
            return true;
        }

        /// <summary>
        /// 连接plc,开始数据通信
        /// </summary>
        /// <returns></returns>
        public bool getLink()
        {
            try
            {
                if (onLine())
                {
                    //bool a = Lasal32.SetCommand(3);
                    //Encoding EC = Encoding.Default;
                    //byte[] bytesUni = EC.GetBytes("VICO");
                    //string ddd = Encoding.UTF8.GetString(bytesUni);
                    //bool b = Lasal32.LoadPrj(ddd);
                    //FileInfo lobFile = new FileInfo(@"C:\Users\Valmo\Desktop\Prog\project.lob");
                    //long len = lobFile.Length;

                    //byte[] lst = new byte[len - 1];

                    //FileStream fs = File.OpenRead(@"C:\Users\Valmo\Desktop\Prog\project.lob");

                    //int i = fs.Read(lst, 0, lst.Length);
                    //fs.Close();
                    //bool c = Lasal32.LoadModul(lst, Convert.ToInt32(len), null);
                    //bool status = false;
                    //if (Lasal32.LinkModules())
                    //{

                    //    status = Lasal32.LslSave();
                    //}

                    //while (status)
                    //{
                    //    bool e = Lasal32.SetCommand(4);
                    //}

                    lstCreate();
                    getLstObjAddr();
                    lstAdd();

                    return lstStart();
                    //return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
        public bool getLink2()
        {
            try
            {
                if (onLine())
                {
                    lstCreate();
                    ////getLstObjAddr();
                    //lstAdd();

                    return lstStart();
                    //relinkPlc();
                    //return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        private void relinkPlc()
        {
            try
            {
                //onLine();
                foreach (int iRlb in lstIRlb)
                {
                    Lasal32.LslRefreshListOffline(iRlb);
                    Lasal32.LslRefreshListOnline(iRlb, plcIpAddr, 1, 0, 0);
                }
                getLstObjAddr();
                lstAdd();
                lstStart();
            }
            catch 
            {

            }
        }

        /// <summary>
        /// 连接plc
        /// </summary>
        /// <returns></returns>
        public bool onLine()
        {
            if (checkLinkPlc())
            {
                vm.printLn("link " + plcIpAddr + " OK!");
                vm.printLn("                							||");
                vm.printLn("											||");
                vm.printLn("									||		||");
                vm.printLn("									||		||");
                vm.printLn("							||		||		||");
                vm.printLn("							||		||		||");
                vm.printLn("					||		||		||		||");
                vm.printLn("					||		||		||		||");
                vm.printLn("			||		||		||		||		||");
                vm.printLn("			||		||		||		||		||");
                vm.printLn("	||		||		||		||		||		||");
                vm.printLn("	||		||		||		||		||		||");
                //flagLinkOk = true;
                return true;
            }
            vm.printLn("link " + plcIpAddr + " Error!");
            vm.printLn("	 ×								×  ");
            vm.printLn("		×						×      ");
            vm.printLn("			×				×          ");
            vm.printLn("				×		×              ");
            vm.printLn("					×	                ");
            vm.printLn("				×		×	            ");
            vm.printLn("			×				×		    ");
            vm.printLn("		×						×	    ");
            vm.printLn("	×								×  ");

            IPAddress[] ipHost = Dns.GetHostAddresses(Dns.GetHostName());
            //flagLinkOk = false;
            return false;
        }
        /// <summary>
        /// 判断当前系统和plc是否保持连接状态
        /// </summary>
        public static bool isOnLine()
        {
            return Lasal32.IsOnline();
        }
        /// <summary>
        /// 断开当前系统与plc的连接
        /// </summary>
        public static void OffLine()
        {
            Lasal32.Offline();
        }

        public void setIPAddr(string str)
        {
            Properties.Settings.Default.IPAddr = str;
            Properties.Settings.Default.Save();
        }

        public string getIpAddr()
        {
             string addr = Properties.Settings.Default.IPAddr;

            string[] str = addr.Split('.');
            if (str.Length == 4)
            {
                return addr;
            }
            else
            {
                return "10.10.150.3";
            }
        }

        public List<string> getLocalIpLst()
        {
            ipLst.Clear();
            IPAddress[] ipHost = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress ip in ipHost)
            {
                if (ip.ToString().Split('.').Length == 4)
                {
                    ipLst.Add(ip.ToString());
                    vm.debug(ip.ToString());
                }
            }
            return ipLst;
        }

        public static bool setValueToObj(int objAddr, int dValue)
        {
            if (Lasal32.LslWriteToSvr(objAddr, dValue))
            {
                return true;
            }
            else
            {
                if (checkLinkPlc())
                {
                    if (Lasal32.LslWriteToSvr(objAddr, dValue))
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        public static bool getObjPlcAddr(string objName, ref int objAddr, string pszClsName)
        {
            if (Lasal32.LslGetObject(objName, ref objAddr, ref Lasal32.Mode_0, pszClsName))
            {
                return true;
            }
            else
            {
                if (checkLinkPlc())
                {
                    if (Lasal32.LslGetObject(objName, ref objAddr, ref Lasal32.Mode_0, pszClsName))
                    {
                        return true;
                    }
                }
                vm.perror("link error!");
                return false;
            }
        }
    }
}
