using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Threading;
using System.Windows.Threading;
using nsDataMgr;
using System.Collections.Specialized;
using System.Xml;
using System.Windows.Forms;

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// Interaction logic for sysConfigPart.xaml
    /// </summary>
    public partial class sysConfigPart : System.Windows.Controls.UserControl
    {

        enum fileDealType
        {
            MoldLoad,
            SysLoad,
            InjLoad,
            ScrewLoad,
            MoldSave,
            SysSave,
            InjSave,
            ScrewSave
        }

        DispatcherTimer dtLoad = new DispatcherTimer();
        fileDealType fDealType;
        loadFileCtrl loadPanel = new loadFileCtrl();
        saveFileCtrl savePanel = new saveFileCtrl();
        public sysConfigPart()
        {
            InitializeComponent();
            dtLoad.Tick += new EventHandler(dtLoadFunc);
            dtLoad.Interval = new TimeSpan(20);
            cvsMain.Children.Add(loadPanel);
            Canvas.SetLeft(loadPanel, 200);
            Canvas.SetTop(loadPanel, 200);
            cvsMain.Children.Add(savePanel);

        }
        private void dtLoadFunc(object obj, EventArgs e)
        {
            try
            {
                switch (fDealType)
                {
                    case fileDealType.MoldLoad:
                        {
                            //loadMoldFile();
                        }
                        break;
                    case fileDealType.SysLoad:
                        {
                            //loadSysFile();
                        }
                        break;
                    case fileDealType.InjLoad:
                        {
                            //loadInjFile();
                        }
                        break;
                    case fileDealType.ScrewLoad:
                        {
                            loadScrewFromFile();
                        }
                        break;

                    case fileDealType.MoldSave:
                        {
                            //saveMoldConfFile();
                        }
                        break;
                    case fileDealType.SysSave:
                        {
                            //saveSysConfFile();
                        }
                        break;
                    case fileDealType.InjSave:
                        {
                            //saveInjConfFile();
                        }
                        break;
                    case fileDealType.ScrewSave:
                        {

                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                vm.perror("[loadParamFile] : " + ex.ToString());
            }
            finally
            {
            }
        }
        int curStep = 0;
        List<string> lstLoad = new List<string>();

        int screwStep = 0;
        List<string> lstMoldToScrew = new List<string>();
        List<string> lstScrewToMold = new List<string>();
        int curMoldScrewNum = 0;
        int addr_MtoSTotalNumber = 0;
        int addr_StoMTotalNumber = 0;
        int addr_LoadSSW = 0;
        int addr_Startloadnewfile = 0;
        int addr_TableType = 0;
        int addr_LoadCounter = 0;
        int addr_Para1 = 0;
        int addr_Para2 = 0;
        int addr_ParaAdd = 0;
        int addr_ErrCode = 0;
        int addr_ErrNumber = 0;
        int moldScrewValueNum = 0;
        private void readScrewFile()
        {
            try
            {
                int value = 0;

                string str;
                switch (screwStep)
                {

                    case 0:
                        #region 第一部分
                        {
                            progressBarCtrl1.setCurRate(8.0);
                            if (!LinkMgr.getObjPlcAddr("MoldScrewFile1.MtoSTotalNumber", ref addr_MtoSTotalNumber, "MoldScrewFile1"))
                            {
                                throw new Exception("get MoldScrewFile1.MtoSTotalNumber  addr_MtoSTotalNumber error!");
                            }
                            if (!LinkMgr.getObjPlcAddr("MoldScrewFile1.StoMTotalNumber", ref addr_StoMTotalNumber, "MoldScrewFile1"))
                            {
                                throw new Exception("get MoldScrewFile1.StoMTotalNumber  addr_StoMTotalNumber error!");
                            }
                            if (!LinkMgr.getObjPlcAddr("MoldScrewFile1.LoadSSW", ref addr_LoadSSW, "MoldScrewFile1"))
                            {
                                throw new Exception("get MoldScrewFile1.LoadSSW  addr_LoadSSW error!");
                            }
                            if (!LinkMgr.getObjPlcAddr("MoldScrewFile1.Startloadnewfile", ref addr_Startloadnewfile, "MoldScrewFile1"))
                            {
                                throw new Exception("get MoldScrewFile1.Startloadnewfile  addr_Startloadnewfile error!");
                            }
                            if (!LinkMgr.getObjPlcAddr("MoldScrewFile1.TableType", ref addr_TableType, "MoldScrewFile1"))
                            {
                                throw new Exception("get MoldScrewFile1.TabelType  addr_TabelType error!");
                            }
                            if (!LinkMgr.getObjPlcAddr("MoldScrewFile1.LoadCounter", ref addr_LoadCounter, "MoldScrewFile1"))
                            {
                                throw new Exception("get MoldScrewFile1.LoadCounter  addr_LoadCounter error!");
                            }
                            if (!LinkMgr.getObjPlcAddr("MoldScrewFile1.Para1", ref addr_Para1, "MoldScrewFile1"))
                            {
                                throw new Exception("get MoldScrewFile1.Para1  addr_Para1 error!");
                            }
                            if (!LinkMgr.getObjPlcAddr("MoldScrewFile1.Para2", ref addr_Para2, "MoldScrewFile1"))
                            {
                                throw new Exception("get MoldScrewFile1.Para2  addr_Para2 error!");
                            }
                            if (!LinkMgr.getObjPlcAddr("MoldScrewFile1.ParaAdd", ref addr_ParaAdd, "MoldScrewFile1"))
                            {
                                throw new Exception("get MoldScrewFile1.ParaAdd  addr_ParaAdd error!");
                            }
                            if (!LinkMgr.getObjPlcAddr("MoldScrewFile1.ErrCode", ref addr_ErrCode, "MoldScrewFile1"))
                            {
                                throw new Exception("get MoldScrewFile1.ErrCode  addr_ErrCode error!");
                            }
                            if (!LinkMgr.getObjPlcAddr("MoldScrewFile1.ErrNumber", ref addr_ErrNumber, "MoldScrewFile1"))
                            {
                                throw new Exception("get MoldScrewFile1.ErrNumber  addr_ErrNumber error!");
                            }
                            FileStream fs = new FileStream(lbMoldScrewConfig.Content.ToString(), FileMode.Open);
                            StreamReader sr = new StreamReader(fs);
                            lstMoldToScrew.Clear();
                            lstScrewToMold.Clear();
                            str = sr.ReadLine();
                            while (!String.IsNullOrEmpty(str) && str != "\t")
                            {
                                lstMoldToScrew.Add(str);
                                str = sr.ReadLine();
                            }
                            lstMoldToScrew.Add("");
                            str = sr.ReadLine();
                            while (!String.IsNullOrEmpty(str) && str != "\t")
                            {
                                lstScrewToMold.Add(str);
                                str = sr.ReadLine();
                            }
                            lstScrewToMold.Add("");
                            sr.Close();
                            fs.Close();
                            screwStep = 1;
                            curMoldScrewNum = 0;
                        }
                        break;
                        #endregion
                    case 1:
                        #region 第二步
                        {
                            progressBarCtrl1.setCurRate(10.0);
                            str = lstMoldToScrew[curMoldScrewNum++];
                            //丝杆转换表(*.msf)的头部信息 不要与 合模配置文件(*.mold)头部信息 相同
                            if (str.Contains("R115"))
                            {
                                str = lstMoldToScrew[curMoldScrewNum++];
                                if (str.Contains("CONVERSION TABLE"))
                                {
                                    str = lstMoldToScrew[curMoldScrewNum++];
                                    string[] tmpStr = str.Split('\t');

                                    if (!Lasal32.LslWriteToSvr(addr_MtoSTotalNumber, Int32.Parse(tmpStr[0])))
                                    {
                                        throw new Exception("write Mold To Screw totalNum error!");
                                    }
                                    if (!Lasal32.LslWriteToSvr(addr_StoMTotalNumber, Int32.Parse(tmpStr[1])))
                                    {
                                        throw new Exception("write Screw To Mold totalNum error!");
                                    }
                                    str = lstMoldToScrew[curMoldScrewNum++];
                                    if (str.Contains("MOLD TO SCREW"))
                                    {
                                        //int addr_int = 0;
                                        value = 0;
                                        if (!Lasal32.LslWriteToSvr(addr_Startloadnewfile, 1))
                                        {
                                            throw new Exception("write Startloadnewfile 1 error!");
                                        }
                                        else
                                        {
                                            value = 0;
                                            if (Lasal32.LslReadFromSvr(addr_Startloadnewfile, ref value))
                                            {
                                                if (value != 1)
                                                {
                                                    throw new Exception("Startloadnewfile is not 1!");
                                                }
                                            }
                                        }
                                        Thread.Sleep(500);
                                        if (!Lasal32.LslReadFromSvr(addr_LoadSSW, ref value))
                                        {
                                            throw new Exception("read LoadSSW value error !");
                                        }
                                        else
                                        {
                                            if (value != 2)
                                            {
                                                throw new Exception("LoadSSW value is not 2!");
                                            }
                                            else
                                            {
                                                if (!Lasal32.LslWriteToSvr(addr_TableType, 0))
                                                {
                                                    throw new Exception("write TabelType 0 error !");
                                                }
                                                if (!Lasal32.LslReadFromSvr(addr_LoadSSW, ref value))
                                                {
                                                    throw new Exception("read LoadSSW value error !");
                                                }
                                                else
                                                {
                                                    if (value != 3)
                                                    {
                                                        throw new Exception("LoadSSW value is not 3 !");
                                                    }
                                                    else
                                                    {
                                                        screwStep = 2;
                                                        vm.debug("MOLD TO SCREW start lstMoldToScrew Num : " + lstMoldToScrew.Count);
                                                        moldScrewValueNum = 0;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    throw new Exception("CONVERSION TABLE error !");
                                }

                            }
                            else
                            {
                                throw (new Exception("R115 error!"));
                            }
                        }
                        break;
                        #endregion
                    case 2:
                        #region 第三步 依次将MoldToScrew数据写到plc中
                        {
                            str = lstMoldToScrew[curMoldScrewNum++];
                            if (!string.IsNullOrEmpty(str) && str != "\t")
                            {
                                //if (curMoldScrewNum % 10 == 0)
                                //{
                                progressBarCtrl1.setCurRate(10.0 + 45.0 * curMoldScrewNum / lstMoldToScrew.Count);
                                //}
                                //vm.debug((curMoldScrewNum - 1) + "\t" + str);
                                string[] tmp = str.Split('\t');
                                if (!Lasal32.LslWriteToSvr(addr_Para1, Int32.Parse(tmp[0])))
                                {
                                    throw new Exception("TotalNum error");
                                }
                                if (!Lasal32.LslWriteToSvr(addr_Para2, Int32.Parse(tmp[1])))
                                {
                                    throw new Exception("TotalNum error");
                                }
                                ++moldScrewValueNum;
                                if (!Lasal32.LslWriteToSvr(addr_ParaAdd, 0))
                                {
                                    throw new Exception("TotalNum error");
                                }
                                if (!Lasal32.LslReadFromSvr(addr_ErrCode, ref value))
                                {
                                    throw new Exception("TotalNum error");
                                }
                                else
                                {
                                    if (value == 1)
                                    {
                                        Lasal32.LslReadFromSvr(addr_ErrNumber, ref value);
                                        //lberror.Content = "error Num : " + value.ToString();
                                        vm.perror("value : " + value);
                                        throw new Exception("Plc Alarm");
                                    }
                                }
                            }
                            else
                            {
                                vm.getTm("MOLD TO SCREW end ");
                                vm.debug(moldScrewValueNum.ToString());
                                if (!Lasal32.LslReadFromSvr(addr_LoadCounter, ref value))
                                {
                                    throw new Exception("read LoadCounter value error !");
                                }
                                else
                                {
                                    if (value != moldScrewValueNum)
                                    {
                                        throw new Exception("load table error!");
                                    }
                                }
                                moldScrewValueNum = 0;
                                curMoldScrewNum = 0;
                                screwStep = 3;
                                //progressBarCtrl1.setCurRate(55.0);
                            }

                        }
                        break;
                        #endregion
                    case 3:
                        #region 第四部
                        {

                            str = lstScrewToMold[curMoldScrewNum++];
                            if (str.Contains("SCREW TO MOLD"))
                            {
                                value = 1;
                                if (!Lasal32.LslWriteToSvr(addr_TableType, 1))
                                {
                                    throw new Exception("write TabelType 0 error !");
                                }
                                Thread.Sleep(500);
                                if (!Lasal32.LslReadFromSvr(addr_LoadSSW, ref value))
                                {
                                    throw new Exception("read LoadSSW value error !");
                                }
                                else
                                {
                                    if (value != 4)
                                    {
                                        throw new Exception("LoadSSW value is not 2!");
                                    }
                                    else
                                    {
                                        screwStep = 4;
                                        vm.debug("SCREW TO MOLD start lstScrewToMold Num : " + lstScrewToMold.Count);
                                        moldScrewValueNum = 0;
                                    }
                                }
                            }
                        }
                        break;
                        #endregion
                    case 4:
                        #region 第五步
                        {

                            if (!string.IsNullOrEmpty(str = lstScrewToMold[curMoldScrewNum++]))
                            {
                                //if (curMoldScrewNum % 10 == 0)
                                //{
                                progressBarCtrl1.setCurRate(55.0 + 40.0 * curMoldScrewNum / lstScrewToMold.Count);
                                //}
                                //vm.debug((curMoldScrewNum - 1) + "\t" + str);
                                string[] tmp = str.Split('\t');
                                if (!Lasal32.LslWriteToSvr(addr_Para1, Int32.Parse(tmp[0])))
                                {
                                    throw new Exception("TotalNum error");
                                }
                                if (!Lasal32.LslWriteToSvr(addr_Para2, Int32.Parse(tmp[1])))
                                {
                                    throw new Exception("TotalNum error");
                                }
                                ++moldScrewValueNum;
                                if (!Lasal32.LslWriteToSvr(addr_ParaAdd, 0))
                                {
                                    throw new Exception("TotalNum error");
                                }
                                if (!Lasal32.LslReadFromSvr(addr_ErrCode, ref value))
                                {
                                    throw new Exception("TotalNum error");
                                }
                                else
                                {
                                    if (value == 1)
                                    {
                                        Lasal32.LslReadFromSvr(addr_ErrNumber, ref value);
                                        //lberror.Content = "error Num : " + value.ToString();
                                        vm.perror("value : " + value);
                                        throw new Exception("Plc Alarm");
                                    }
                                }

                            }
                            else
                            {
                                screwStep = 5;
                                vm.debug(moldScrewValueNum.ToString());
                                if (!Lasal32.LslReadFromSvr(addr_LoadCounter, ref value))
                                {
                                    throw new Exception("read LoadCounter value error !");
                                }
                                else
                                {
                                    if (value != moldScrewValueNum)
                                    {
                                        throw new Exception("load table error!");
                                    }
                                }
                                vm.getTm("SCREW TO MOLD end ");
                            }



                        }
                        break;
                        #endregion
                    default:
                        {
                            screwStep = 0;
                            curStep = 2;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                screwStep = 0;
                vm.debug(ex.ToString());
                throw (new Exception("load moldScrewFile error!"));
            }
        }
        private void disposeFile()
        {
            //curLineNr = 0;
            lstLoad.Clear();
            dtLoad.Stop();
            curStep = 0;
            progressBarCtrl1.stop();

            vm.debug("dispos OK!");
        }
        private void loadScrewFromFile()
        {
            try
            {
                #region  转换表操作顺序
                /*
	            sLoadMoldScrewFile(fl_ld_3)写入1,如果sLoadMoldScrewFile(fl_ld_3)变为1那么表示可以进行写入操作.否则失败
	            MtoSTotalNumber(fl_ld_11)写入需要加载的Mold to screw的个数,StoMTotalNumber(fl_ld_11)写入Screw to mold的个数
	            Startloadnewfile(fl_ld_21)写入1
	            如果Startloadnewfile(fl_ld_21)变成1,那么等待LoadSSW(fl_ld_10)的值变成2
	            将TableType(fl_ld_13)写为0,那么表示开始加载Mold to screw的表格;同理写入1表示开始加载Screw to mold的表格
	            等待LoadSSW(fl_ld_10)变成3表示可以开始加载Mold to screw,同理变成4表示可以加载Screw to mold
	            将每一组前一数据写入Para1(fl_ld_15),后一数据写入Para2(fl_ld_16).确认写入后,将ParaAdd(fl_ld_17)写入0
	            如果写入成功,那么LoadCounter(fl_ld_14)会加1.
	            如果ErrCode(fl_ld_19)等于1时,读取ErrNumber(fl_ld_20)表示错误出现在第几个数据
	            当LoadCounter(fl_ld_14)和你需要加载的个数一致时,将TableType(fl_ld_13)写为上次没有写入的数值.上次为0那这次为1.
	            全部加载完成后将SavetoRAMEX(fl_ld_18)写为1.
	            如果SavetoRAMEX(fl_ld_18)变为0,说明保存成功
	            sLoadMoldScrewFile(fl_ld_3)写入0,如果sLoadMoldScrewFile(fl_ld_3)变为0,那么表示加载完成
                */
                #endregion
                int addr_int = 0;
                switch (curStep)
                {
                    case 0:
                        {
                            progressBarCtrl1.start();
                            //progressBarCtrl1.setCurRate(5.0);
                            //progressBarCtrl1.reStart();
                            int value = 1;
                            if (!LinkMgr.getObjPlcAddr("MoldScrewFile1.sReset", ref addr_int, "MoldScrewFile1"))
                            {
                                throw new Exception("get MoldScrewFile1.sReset  addr_int error!");
                            }
                            Lasal32.LslWriteToSvr(addr_int, 1);
                            //bdMoldScrewConfig.Background = Brushes.Transparent;
                            if (!LinkMgr.getObjPlcAddr("MoldScrewFile1.sLoadMoldScrewFile", ref addr_int, "MoldScrewFile1"))
                            {
                                throw new Exception("get MoldScrewFile1.sLoadMoldScrewFile  addr_int error!");
                            }
                            if (LinkMgr.setValueToObj(addr_int, value))
                            {
                                int tmp = 2;
                                if (Lasal32.LslReadFromSvr(addr_int, ref tmp))
                                {
                                    if (tmp == -1)
                                    {
                                        throw new Exception("write MoldScrewFile1.sLoadMoldScrewFile 1 error!");
                                    }
                                }
                                else
                                {
                                    throw new Exception("get MoldScrewFile1.sLoadMoldScrewFile  value error!");
                                }
                            }
                            else
                            {
                                throw new Exception("write " + "MoldScrewFile1.sLoadMoldScrewFile 1 error!");

                            }
                            curStep = 1;
                        }
                        break;
                    case 1:
                        {
                            readScrewFile();
                        }
                        break;
                    case 2:
                        {
                            progressBarCtrl1.setCurRate(100.0);
                            if (!LinkMgr.getObjPlcAddr("MoldScrewFile1.SavetoRAMEX", ref addr_int, "MoldScrewFile1"))
                            {
                                throw new Exception("get MoldScrewFile1.SavetoRAMEX addr_int error!");
                            }
                            if (Lasal32.LslWriteToSvr(addr_int, 1))
                            {
                                Thread.Sleep(100);
                                int tmp = 2;
                                if (Lasal32.LslReadFromSvr(addr_int, ref tmp))
                                {
                                    if (tmp == 0)
                                    {
                                        //bdInjConfig.Background = Brushes.Blue;
                                    }
                                    else
                                    {
                                        throw new Exception("write MoldScrewFile1.SavetoRAMEX 1 error! return value is " + tmp);
                                    }
                                }
                                else
                                {
                                    throw new Exception("get MoldScrewFile1.SavetoRAMEX  value error!");
                                }
                            }
                            else
                            {
                                throw new Exception("write " + "MoldScrewFile1.SavetoRAMEX 1 error!");
                            }
                            //end flag
                            if (!LinkMgr.getObjPlcAddr("MoldScrewFile1.sLoadMoldScrewFile", ref addr_int, "MoldScrewFile1"))
                            {
                                throw new Exception("get MoldScrewFile1.sLoadMoldScrewFile addr_int error!");
                            }
                            if (Lasal32.LslWriteToSvr(addr_int, 0))
                            {
                                Thread.Sleep(100);
                                int tmp = 2;
                                if (Lasal32.LslReadFromSvr(addr_int, ref tmp))
                                {
                                    if (tmp == 0)
                                    {
                                        //bdMoldScrewConfig.Background = Brushes.Blue;
                                        curStep = 0;
                                        disposeFile();
                                        //dtLoad.Stop();
                                    }
                                    else
                                    {
                                        throw new Exception("write MoldScrewFile1.sLoadMoldScrewFile 1 error! return value is " + tmp);
                                    }

                                }
                                else
                                {
                                    throw new Exception("get MoldScrewFile1.sLoadMoldScrewFile  value error!");
                                }
                            }
                            else
                            {
                                throw new Exception("write " + "MoldScrewFile1.sLoadMoldScrewFile 1 error!");

                            }
                        }
                        break;
                    default:
                        {
                            curStep = 0;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                curStep = 0;
                vm.perror(ex.ToString());
                System.Windows.MessageBox.Show(ex.ToString());
                //dtLoad.Stop();
                disposeFile();
                if (progressBarCtrl1.Visibility == Visibility.Visible)
                    progressBarCtrl1.Visibility = Visibility.Hidden;
            }
        }

        private void btnMoldConf_Click(object sender, RoutedEventArgs e)
        {
            loadPanel.init("MoldConfFile", ".mold", valmoWin.SLoadConfFilePanel.show, null);
        }
        private void btnInjConf_Click(object sender, RoutedEventArgs e)
        {
            loadPanel.init("InjConfFile", ".inj", valmoWin.SLoadConfFilePanel.show, null);
        }
        private void btnSysConf_Click(object sender, RoutedEventArgs e)
        {
            loadPanel.init("SysConfFile", ".sys", valmoWin.SLoadConfFilePanel.show, null);
        }
        private void btnMoldScrew_Click(object sender, RoutedEventArgs e)
        {
            loadPanel.init("MoldScrewFile","msf", MoldScrewSelectFunc, null);
        }
        private void MoldScrewSelectFunc(string fileName)
        {
            try
            {
                lbMoldScrewConfig.Content = fileName;
                //Canvas.SetTop(progressBarCtrl1, 141);
                progressBarCtrl1.setSpeed(0);
                fDealType = fileDealType.ScrewLoad;
                curStep = 0;
                dtLoad.Start();
                return;
            }
            catch (Exception ex)
            {
                vm.perror(ex.ToString());
            }
        }

        private void btnMoldSave_Click(object sender, RoutedEventArgs e)
        {
            loadPanel.init("MoldConfFile", ".mold", valmoWin.SSaveConfFilePanel.show, null);
        }
        private void btnInjSave_Click(object sender, RoutedEventArgs e)
        {
            loadPanel.init("InjConfFile", ".inj", valmoWin.SSaveConfFilePanel.show, null);
        }
        private void btnSysSave_Click(object sender, RoutedEventArgs e)
        {
            loadPanel.init("sysConfFile", ".sys", valmoWin.SSaveConfFilePanel.show, null);
        }

        /*************************************************************
         *          需要导出的内容
         * 1、状态栏位置信息
         * 2、键盘位置信息
         * 3、数据分析曲线的配置信息
         * 4、用户信息
         * 
         * 
         *************************************************************/
        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            if (cBExportTopWin.bIsChecked || cBExportKeys.bIsChecked || cBExportLines.bIsChecked)
            {
                XmlTextWriter writer = new XmlTextWriter("conf/MyInfo.conf", System.Text.Encoding.UTF8);
                //使用自动缩进便于阅读 
                writer.Formatting = Formatting.Indented;
                //XML声明         
                writer.WriteStartDocument();
                //书写根元素    
                writer.WriteStartElement("Config");


                if (cBExportTopWin.bIsChecked)
                {
                    StringCollection strStateLst = Properties.Settings.Default.topWinPos;
                    writer.WriteStartElement("Item "); writer.WriteAttributeString("Name", "topWinPosList");
                    foreach (string str in strStateLst)
                    {
                        writer.WriteElementString("pos", str);
                    }
                    writer.WriteEndElement();//end of topWinPosList
                }

                if (cBExportKeys.bIsChecked)
                {
                    StringCollection strKeysLst = Properties.Settings.Default.ctrlLst;
                    writer.WriteStartElement("Item "); writer.WriteAttributeString("Name", "keysList");
                    for (int num = 0; num < strKeysLst.Count; num++)
                    {
                        if (strKeysLst[num] != "0")
                        {
                            writer.WriteStartElement("Key"); writer.WriteAttributeString("ListNr", num.ToString()); writer.WriteAttributeString("id", strKeysLst[num]);
                            writer.WriteEndElement();
                        }
                    }
                    writer.WriteEndElement();//end of keysList
                }

                writer.WriteEndElement();//end of Config
                writer.Close();
            }
        }
        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            if (cBLoadTopWin.bIsChecked || cBLoadKeys.bIsChecked || cBLoadLines.bIsChecked)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("conf/MyInfo.conf");
                XmlNode root = xmlDoc.SelectSingleNode("Config");//查找<bookstore>
                XmlNodeList xnl = root.ChildNodes;
                foreach (XmlElement xe in xnl)
                {
                    XmlNodeList xnf1 = xe.ChildNodes;
                    switch (xe.GetAttribute("Name"))
                    {
                        case "topWinPosList":
                            {
                                if (cBLoadTopWin.bIsChecked)
                                {
                                    foreach (XmlNode xn2 in xnf1)
                                    {
                                        Console.WriteLine(xn2.InnerText);//显示子节点点文本
                                    }
                                }
                            }
                            break;
                        case "keysList":
                            {
                                if (cBLoadKeys.bIsChecked)
                                {
                                    foreach (XmlElement xn2 in xnf1)
                                    {
                                        Console.WriteLine(xn2.GetAttribute("ListNr") + "\t" + xn2.GetAttribute("id"));//显示子节点点文本
                                    }
                                }
                            }
                            break;
                        case "LnParams":
                            {
                                if (cBLoadLines.bIsChecked)
                                {
                                    foreach (XmlElement xn2 in xnf1)
                                    {
                                        Console.WriteLine(xn2.GetAttribute("basicValue") + "\t" + xn2.GetAttribute("offsetValue"));//显示子节点点文本
                                    }
                                }
                            }
                            break;
                    }
                }
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            // Insert code to read the stream here.
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void button1_Click_1(object sender, RoutedEventArgs e)
        {
            ConfigOutPut configOutPutPage = new ConfigOutPut();
            configOutPutPage.ShowDialog();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.InitialDirectory = "d:\\Valmo\\Config";
            openFileDialog.Filter = "Config文件|*.config";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                valmoWin.SLoadConfFilePanel.show(openFileDialog.FileName);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            valmoWin.SSaveConfFilePanel.show();
        }
    }
}
/*
 * 顺序			
1(fl_ld_1)	MoldScrewFile1.sLoadMoldConfig	"开始加载时写1,如果值为1,表示可
                                                以写入,如果值为-1表示不能写入.
                                                原因可能为马达没有关闭,加热没
                                                有关闭,加载持续错误等."	
 *                                              结束加载后,写入0,同时检测'RamSourceFileMoldLoaded.Data'标记位,是否为1,如果为1,说明加载成功
2(fl_ld_2)	MoldScrewFile1.sLoadInjeConfig		结束加载后,写入0,同时检测'RamSourceFileInjection.Data'标记位,是否为1,如果为1,说明加载成功
3(fl_ld_3)	MoldScrewFile1.sLoadMoldScrewFile		结束加载后,写入0,同时检测'RamScrewFileLoaded.Data'标记位,是否为1,如果为1,说明加载成功
4(fl_ld_4)	MoldScrewFile1.sLoadSystemConfig		结束加载后,写入0,同时检测'CheckLoadRam.Data'标记位,是否为1,如果为1,说明加载成功

		CheckLoadRam.Data	
fl_ld_5	RamSourceFileMoldLoaded.Data	"""是否加载模具部配置文件"""	
fl_ld_6	RamSourceFileInjection.Data	"""是否加载注射部配置文件"""	
fl_ld_7	RamScrewFileLoaded.Data	"""是否加载丝杆转换表"""	
fl_ld_8	CheckLoadRam.Data	"""是否加载系统配置文件"""	

加载丝杆转换表文件			
fl_ld_10	MoldScrewFile1.LoadSSW	文件加载模式	1表示开始加载;2表示准备好开始加载;3表示开始加载Mold to screw;4表示开始加载Screw to mold
fl_ld_11	MoldScrewFile1.MtoSTotalNumber	需要加载Mold to screw的个数	
fl_ld_12	MoldScrewFile1.StoMTotalNumber	需要加载Screw to mold的个数	
fl_ld_13	MoldScrewFile1.TableType	需要加载的类型	0表示Mold to screw;1表示Screw to mold
fl_ld_14	MoldScrewFile1.LoadCounter	已加载的个数	
fl_ld_15	MoldScrewFile1.Para1	数据1	
fl_ld_16	MoldScrewFile1.Para2	数据2	
fl_ld_17	MoldScrewFile1.ParaAdd	下一步	
fl_ld_18	MoldScrewFile1.SavetoRAMEX	将加载的内容保存	
fl_ld_19	MoldScrewFile1.ErrCode	错误	0表示没有错误,1表示有错误
fl_ld_20	MoldScrewFile1.ErrNumber	第几个出现错误	
fl_ld_21	MoldScrewFile1.Startloadnewfile	开始加载	

 * 
 * 
sLoadMoldScrewFile(fl_ld_3)写入1,如果sLoadMoldScrewFile(fl_ld_3)变为1那么表示可以进行写入操作.否则失败
MtoSTotalNumber(fl_ld_11)写入需要加载的Mold to screw的个数,StoMTotalNumber(fl_ld_11)写入Screw to mold的个数
Startloadnewfile(fl_ld_21)写入1
如果Startloadnewfile(fl_ld_21)变成1,那么等待LoadSSW(fl_ld_10)的值变成2
将TableType(fl_ld_13)写为0,那么表示开始加载Mold to screw的表格;同理写入1表示开始加载Screw to mold的表格
等待LoadSSW(fl_ld_10)变成3表示可以开始加载Mold to screw,同理变成4表示可以加载Screw to mold
将每一组前一数据写入Para1(fl_ld_15),后一数据写入Para2(fl_ld_16).确认写入后,将ParaAdd(fl_ld_17)写入0
如果写入成功,那么LoadCounter(fl_ld_14)会加1.
如果ErrCode(fl_ld_19)等于1时,读取ErrNumber(fl_ld_20)表示错误出现在第几个数据
当LoadCounter(fl_ld_14)和你需要加载的个数一致时,将TableType(fl_ld_13)写为上次没有写入的数值.上次为0那这次为1.
全部加载完成后将SavetoRAMEX(fl_ld_18)写为1.
如果SavetoRAMEX(fl_ld_18)变为0,说明保存成功
sLoadMoldScrewFile(fl_ld_3)写入0,如果sLoadMoldScrewFile(fl_ld_3)变为0,那么表示加载完成

 * 
 * 
 * 
 */













