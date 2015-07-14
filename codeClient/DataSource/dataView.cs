using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nsVicoClient;
using System.Windows.Controls;
using nsVicoClient.ctrls;
using System.Windows.Threading;
using System.Threading;
using nsDataMgr;
using System.Security.Cryptography;
using System.IO;

namespace nsVicoClient
{
    public class dataView : dvBase
    {
        public string plcIpAddr = "TCP:10.10.150.10"; 
        public static string curLan = "lanCN";
        public int iRlb = -1;
        public int iRlbMap = -1;
        /// <summary>
        /// "root", "Yehill", 5,
        /// "ser", "Action", 4,
        /// "mgr", "2013", 3,
        /// "mt", "222", 2,
        /// "op", "111", 1
        /// </summary>
        public Users users = new Users();
        public objUnit tmTypeObj;
        public objUnit lenTypeObj;
        public objUnit forceTypeObj;
        public objUnit tempTypeObj;
        public objUnit perTypeObj;
        public objUnit prsTypeObj;
        public objUnit rpsTypeObj;
        public objUnit linkOkObj;
        public ItlPr ItlPr = new ItlPr();
        public List<objUnit> lstObj = new List<objUnit>();
        public LinkMgr linkObj = new LinkMgr();

        public dataView()
        {
            objUnit.unitInit(Properties.Settings.Default.units);
            linkObj.dataInit(this);
            vm.getTm("==========> [startUp][dataView] \t");
            tmTypeObj = new objUnit("TmTypeObj", UnitType.Tm_s, "noMin", "noMax", "Write", "Label", "");
            lenTypeObj = new objUnit("LenObj", UnitType.Len_mm, "noMin", "noMax", "Write", "Label", "");
            forceTypeObj = new objUnit("ForceTypeObj", UnitType.Force_ton, "noMin", "noMax", "Write", "Label", "");

            tempTypeObj = new objUnit("TempTypeObj", UnitType.Temp_C, "noMin", "noMax", "Write", "Label", "");
            perTypeObj  = new objUnit("TempTypeObj", UnitType.Per, "noMin", "noMax", "Write", "Label", "");
            prsTypeObj = new objUnit("prsTypeObj", UnitType.Prs_Mpa, "noMin", "noMax", "Write", "Label", "");
            rpsTypeObj = new objUnit("rpsTypeObj", UnitType.RSpeed, "noMin", "noMax", "Write", "Label", "");

            linkOkObj = new objUnit("LinkOkObj", UnitType.DgtType, "", "", "", "","");
        }

        /// <summary>
        /// 连接plc，开始数据通信
        /// </summary>
        /// <returns></returns>
        public bool getLink()
        {
            return linkObj.getLink();
        }
        public bool lstStart()
        {
            return linkObj.lstStart();
        }
        public bool getLink2()
        {
            return linkObj.getLink2();
        }
        public void relink()
        {
            linkObj.getLink(); 
        }
        /// <summary>
        /// 判断当前登录用户的权限是否不低于指定等级
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public bool checkAccesslevel(int level)
        {
            if (users.curUser == users.nullUser)
            {
                valmoWin.execHandle(opeOrderType.winMsg, new WinMsg(WinMsgType.mwUserNull));
                return false;
            }
            if (users.curUser.accessLevel >= level)
            {
                return true;
            }
            else
            {
                vm.printLn("权限不够");
                valmoWin.execHandle(opeOrderType.winMsg, new WinMsg(WinMsgType.mwAccessLower));
                return false;
            }
        }
        public bool checkAccesslevel()
        {
            return checkAccesslevel(users.curUser.accessLevel);
        }
        /// <summary>
        /// 获取总的生产模数
        /// </summary>
        /// <returns></returns>
        public int getCurPlateNr()
        {
            return PrdPr[22].valueNew;
        }
        /// <summary>
        /// 获取当前编号的后边一个编号对应的对象
        /// </summary>
        /// <param name="serialNum"></param>
        /// <returns></returns>
        public objUnit getObjNext(string serialNum)
        {
            //if (serialNum.Length != 6 && serialNum.Length != 8)
            //    return null;
            objUnit curObj = null;
            int serNr = Int32.Parse(serialNum.Substring(3, serialNum.Length - 3)) + 1;
            switch (serialNum.Substring(0, 3))
            {
                case "Ipr":
                    curObj = IprPr[serNr];
                    break;
                case "Sys":
                    curObj = SysPr[serNr];
                    break;
                case "Mld":
                    curObj = MldPr[serNr];
                    break;
                case "Inj":
                    curObj = InjPr[serNr];
                    break;
                case "Tmp":
                    curObj = TmpPr[serNr];
                    break;
                case "Prd":
                    curObj = PrdPr[serNr];
                    break;
                case "IOS":
                    curObj = IOSPr[serNr];
                    break;
                case "IOF":
                    curObj = IOFPr[serNr];
                    break;
                case "Alm":
                    curObj = AlmPr[serNr];
                    break;
                case "Key":
                    curObj = KeyPr[serNr];
                    break;
            }
            if (serialNum.Length == 8)
            {
                if (curObj != null)
                {
                    switch (serialNum.Substring(6, 2))
                    {
                        case "ht":
                            curObj.setLstSpd(plcLstSpd.highSpdType);
                            break;
                        case "mt":
                            curObj.setLstSpd(plcLstSpd.mapType);
                            break;
                        case "nt":
                            //curObj.addLb(lbMain);
                            break;
                        case "lt":
                            curObj.setLstSpd(plcLstSpd.lowSpdType);
                            break;
                    }
                }
            }
            return curObj;
        }

        public string getCurDis(string str,int oldValue = -1,int newValue = -1)
        {
            string strRet = "***";
            try
            {
                object obj = App.Current.TryFindResource(str);
                if (obj == null)
                {
                    strRet = str + "***";
                }
                else 
                {
                    strRet = obj.ToString();
                }

                switch (str)
                {
                    case "Sys152":
                        #region
                        {
                            int value = oldValue ^ newValue;
                            int curPos = 0;
                            for (curPos = 0; curPos < 32; curPos++)
                            {
                                if (((value >> curPos) & 0x01) == 1)
                                {
                                    break;
                                }
                            }

                            if ((value & oldValue) == value)
                            {
                                switch (curPos)
                                {
                                    case 0:
                                        strRet = "关闭中子1/中子A与机械手配合功能";
                                        break;
                                    case 1:
                                        strRet = "关闭中子1/中子B与机械手配合功能";
                                        break;
                                    case 2:
                                        strRet = "关闭中子1/中子C与机械手配合功能";
                                        break;
                                    case 3:
                                        strRet = "关闭中子1/中子D与机械手配合功能";
                                        break;
                                    case 4:
                                        strRet = "关闭中子1/中子E与机械手配合功能";
                                        break;
                                    case 5:
                                        strRet = "关闭中子1/中子F与机械手配合功能";
                                        break;
                                }
                            }
                            else
                            {
                                switch (curPos)
                                {
                                    case 0:
                                        strRet = "开启中子1/中子A与机械手配合功能";
                                        break;
                                    case 1:
                                        strRet = "开启中子1/中子B与机械手配合功能";
                                        break;
                                    case 2:
                                        strRet = "开启中子1/中子C与机械手配合功能";
                                        break;
                                    case 3:
                                        strRet = "开启中子1/中子D与机械手配合功能";
                                        break;
                                    case 4:
                                        strRet = "开启中子1/中子E与机械手配合功能";
                                        break;
                                    case 5:
                                        strRet = "开启中子1/中子F与机械手配合功能";
                                        break;
                                }
                            }
                            break;
                        }
                        #endregion
                    case "Sys153":
                        #region
                        {
                            int value = oldValue ^ newValue;
                            int curPos = 0;
                            for (curPos = 0; curPos < 32; curPos++)
                            {
                                if (((value >> curPos) & 0x01) == 1)
                                {
                                    break;
                                }
                            }

                            if ((value & oldValue) == value)
                            {
                                switch (curPos)
                                {
                                    case 0:
                                        strRet = "关闭中子2/中子A与机械手配合功能";
                                        break;
                                    case 1:
                                        strRet = "关闭中子2/中子B与机械手配合功能";
                                        break;
                                    case 2:
                                        strRet = "关闭中子2/中子C与机械手配合功能";
                                        break;
                                    case 3:
                                        strRet = "关闭中子2/中子D与机械手配合功能";
                                        break;
                                    case 4:
                                        strRet = "关闭中子2/中子E与机械手配合功能";
                                        break;
                                    case 5:
                                        strRet = "关闭中子2/中子F与机械手配合功能";
                                        break;
                                }
                            }
                            else
                            {
                                switch (curPos)
                                {
                                    case 0:
                                        strRet = "开启中子2/中子A与机械手配合功能";
                                        break;
                                    case 1:
                                        strRet = "开启中子2/中子B与机械手配合功能";
                                        break;
                                    case 2:
                                        strRet = "开启中子2/中子C与机械手配合功能";
                                        break;
                                    case 3:
                                        strRet = "开启中子2/中子D与机械手配合功能";
                                        break;
                                    case 4:
                                        strRet = "开启中子2/中子E与机械手配合功能";
                                        break;
                                    case 5:
                                        strRet = "开启中子2/中子F与机械手配合功能";
                                        break;
                                }
                            }
                            break;
                        }
                        #endregion
                }
            }
            catch
            {
                vm.perror("get " + curLan + "error! serialNum : " + str);
            }
            return strRet;
        }
        /// <summary>
        /// 设置当前系统需要连接的plc的IP地址
        /// </summary>
        /// <param name="str"></param>
        public void setPlcIpAddr(string str)
        {
            linkObj.setIPAddr(str);
        }
        /// <summary>
        /// 获取当前系统连接的plc的IP地址
        /// </summary>
        /// <returns></returns>
        public string getPlcIpAddr()
        {
            return linkObj.getIpAddr();
        }
        public List<string> getLocalIpLst()
        {
            return linkObj.getLocalIpLst();
        }
        public bool setValue(objUnit obj,int value)
        {
            if (obj.flagInerval)
            {
                obj.vDblNew = value;
                obj.refresh();
                return true;
            }
            else
            {
                int oldValue = obj.value;
                if (obj.setValue(value))
                {
                    if (obj.objType != objectType.ItlPr)
                    {
                      valmoWin.eventMgr.addParamMsg(obj.serialNum, DateTime.Now, oldValue, obj.valueNew, recType.operateType);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //默认密钥向量
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public string EncryptDES(string encryptString, string encryptKey)
        {
            byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
            byte[] rgbIV = Keys;
            byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
            DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray());
        }

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public string DecryptDES(string decryptString, string decryptKey)
        {
            byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
            byte[] rgbIV = Keys;
            byte[] inputByteArray = Convert.FromBase64String(decryptString);
            DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Encoding.UTF8.GetString(mStream.ToArray());
        }
    }

    public class Encrypt
    {
        //默认密钥向量
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string EncryptDES(string encryptString, string encryptKey)
        {
            byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
            byte[] rgbIV = Keys;
            byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
            DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray());
        }

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string DecryptDES(string decryptString, string decryptKey)
        {
            byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
            byte[] rgbIV = Keys;
            byte[] inputByteArray = Convert.FromBase64String(decryptString);
            DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Encoding.UTF8.GetString(mStream.ToArray());
        }
    }
}
