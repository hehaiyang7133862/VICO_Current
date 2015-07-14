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
using System.Xml;
using System.Collections.Specialized;
using nsVicoClient;

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// Interaction logic for interpretorPage.xaml
    /// </summary>
    public partial class interpretorPage : UserControl
    {
        public delegate void StreamSave(FileStream fstream);
        //loadShadeCtrl loadShadePanel = new loadShadeCtrl();
        loadShadeCtrl loadShadePanel2 = new loadShadeCtrl();
        public saveFileCtrl saveIprPanel = new saveFileCtrl();
        public loadFileCtrl loadIprPanel = new loadFileCtrl();
        public newIprConfirmCtrl newIprConfirmPanel = new newIprConfirmCtrl(); 
        private progressCtrl1 progressPanel = new progressCtrl1();
        public confirmCtrl confirmPanel = new confirmCtrl();
        public static bool flagStartScan = false;
        public static int lenLine = 500;
        public int curCode = 0;
        public static bool flagRdyToOp = false;
        FileStream fsIpr;
        static  FileStream fsCurIprPrg = null;
        DispatcherTimer dtSave = new DispatcherTimer();
        string[] strWarn = { "lanKey1030",// "WARN[00] : 未定义\"中子A\" \"进\"",
                             "lanKey1031",// "WARN[01] : 未定义\"中子A\" \"退\"",
                             "lanKey1032",// "WARN[02] : 未定义\"中子B\" \"进\"",
                             "lanKey1033",// "WARN[03] : 未定义\"中子B\" \"退\"",
                             "lanKey1034",// "WARN[04] : 未定义\"中子C\" \"进\"",
                             "lanKey1035",// "WARN[05] : 未定义\"中子C\" \"退\"",
                             "lanKey1036",// "WARN[06] : 未定义\"中子D\" \"进\"",
                             "lanKey1037",// "WARN[07] : 未定义\"中子D\" \"退\"",
                             "lanKey1038",// "WARN[08] : 未定义\"中子E\" \"进\"",
                             "lanKey1039",//  "WARN[09] : 未定义\"中子E\" \"退\"",
                             "lanKey1040",// "WARN[10] : 未定义\"中子F\" \"进\"",
                             "lanKey1041",// "WARN[11] : 未定义\"中子F\" \"退\"",
                             "lanKey1042",// "WARN[12] : 未定义\"顶针\" \"退\"",
                             "lanKey1043",// "WARN[13] : 未定义\"顶针\" \"进\"",
                             "lanKey1044",// "WARN[14] : 程序中没有动作显示能\"合模到底\"",
                             "lanKey1045",// "WARN[15] : 程序中没有定义\"开模动作\"",
                             "lanKey1046",// "WARN[16] : 未定义\"顶针2\" \"退\"",
                             "lanKey1047",// "WARN[17] : 未定义\"顶针2\" \"进\"",
                             "lanKey1048",// "WARN[18] : 在模具闭合的情况下定义了顶针动作",
                             "lanKey1049",// "WARN[19] : 开合模动作被定义在了其他编辑位置",
                             "lanKey1050",// "WARN[20] : 注射动作被定义在了其他编辑位置",
                             "lanKey1051",// "WARN[21] : ",
                             "lanKey1052",// "WARN[22] : 注射动作时,喷嘴可能未顶住模具",
                             "lanKey1053",// "WARN[23] : 程序中没有定义注射动作",
                             "lanKey1054",// "WARN[24] : 程序中没有定义储料动作",
                             "lanKey1055",// "WARN[25] : 程序中没有定义冷却",
                             "lanKey1056",// "WARN[26] : 注射动作时,喷嘴没有定义为开启状态",
                             "lanKey1057",// "WARN[27] : 注射动作时,阀针没有定义为开启状态",
                             "lanKey1058",// "WARN[28] : ",
                             "lanKey1059",// "WARN[29] : ",
                             "lanKey1060",// "WARN[30] : 程序存在设定问题",
                             "lanKey1061",// "WARN[31] : 程序存在逻辑问题"
                           };

        public static DirectoryInfo prgDir = new DirectoryInfo("ipr");
        public static StreamSave saveIprHandle;
        //public static strEvent saveCurIprToFileHandle;
        public StringCollection prgMsg
        {
            get
            {
                if (Properties.Settings.Default.curIprMsg == null)
                {
                    StringCollection iprMsg = new StringCollection();
                    iprMsg.Add("NewIpr.ipr");
                    iprMsg.Add("");
                    iprMsg.Add("");
                    iprMsg.Add("");
                    iprMsg.Add("");
                    Properties.Settings.Default.curIprMsg = iprMsg;
                }

                return Properties.Settings.Default.curIprMsg;
            }
            set
            {
                vm.printLn("curIprMsg");
                foreach (string str in value)
                {
                    vm.printLn(str);
                }

                Properties.Settings.Default.curIprMsg = value;
            }
        }
        string _prgFile = "";

        /// <summary>
        /// 当前教导文件名
        /// </summary>
        public string prgFileName
        {
            get
            {
                return lbPrgName.Content.ToString();
            }
            set
            {
                if (value.Length > 4)
                {
                    if (value.Substring(value.Length - 4, 4) != ".ipr")
                        value += ".ipr";
                }
                else
                {
                    value += ".ipr";
                }
                lbPrgName.Content = value;
                iprSaveCtrl1.lbIprFile.Content = value;
                iprLoadCtrl1.lbIprFile.Content = value;


                Application.Current.Resources["IprFileName"] = lbPrgName.Content;
            }
        }
        public bool flagNeedSaveIprFile
        {
            get;
            set;
        }
        public FileInfo prgFileinfo
        {
            get
            {
                return new FileInfo(prgDir + "\\" + prgFileName);
            }
        }

        public interpretorPage()
        {
            vm.testInitTmStart("\t[interpretorPage]");
            InitializeComponent();
            try
            {
                valmoWin.lstLanRefresh.Add(refreshLan);

                cvsMain.Children.Add(confirmPanel);
                Canvas.SetTop(confirmPanel, 80);

                cvsMain.Children.Add(loadShadePanel2);
                Canvas.SetTop(loadShadePanel2, 80);

                cvsMain.Children.Add(saveIprPanel);
                saveIprPanel.w = 1080;
                saveIprPanel.h = 932;
                Canvas.SetTop(saveIprPanel, 88);

                cvsMain.Children.Add(loadIprPanel);
                loadIprPanel.w = 1080;
                loadIprPanel.h = 932;
                Canvas.SetTop(loadIprPanel, 88);

                cvsMain.Children.Add(newIprConfirmPanel);
                Canvas.SetTop(newIprConfirmPanel, 80);
                init();

                valmoWin.lstStartUpInit.Add(startUpInit);
                //refreshDriver();
                StringCollection curIprMsg = prgMsg;
                cvsMain.Children.Add(progressPanel);

                flagNeedSaveIprFile = true;
                if (curIprMsg.Count == 5)
                {
                    prgFileName = curIprMsg[0];
                }
                else
                {
                    StringCollection iprMsg = new StringCollection();
                    iprMsg.Add("NewIpr.ipr");
                    iprMsg.Add("");
                    iprMsg.Add("");
                    iprMsg.Add("");
                    iprMsg.Add("");
                    prgFileName = "NewIpr.ipr";
                    prgMsg = iprMsg;
                }
                
                saveIprHandle = saveIprFileFunc;
                valmoWin.lstUsbCheckFunc.Add(refreshDriver);
                //saveCurIprToFileHandle = saveCurPrgToFile;
                //saveCurIprToFileHandle = renameCurIpr;
                vm.testInitTmEnd("\t[interpretorPage]");
            }
            catch (Exception ex)
            {
                vm.perror("[interpretorPage] " + ex.ToString() );
            }
        }
        public void init()
        {
            valmoWin.dv.IprPr[4].addHandle(handleIprPr_4);
            valmoWin.dv.IprPr[3].addHandle(handleIprPr_3);
            valmoWin.dv.IprPr[5].addHandle(handleIprPr_5);
            valmoWin.dv.IprPr[55].addHandle(this.handleIprPr_55);
            valmoWin.dv.IprPr[56].addHandle(this.handleIprPr_56);
            valmoWin.dv.IprPr[57].addHandle(this.handleIprPr_57);
            valmoWin.dv.IprPr[58].addHandle(this.handleIprPr_58);
            valmoWin.dv.IprPr[59].addHandle(this.handleIprPr_59);
            valmoWin.dv.IprPr[60].addHandle(this.handleIprPr_60);
            valmoWin.dv.IprPr[61].addHandle(this.handleIprPr_61);
            valmoWin.dv.IprPr[34].addHandle(this.handleIprPr_34);
            valmoWin.dv.IprPr[20].addHandle(this.handleIprPr_20);

            iprFileInfo.renameHandle = renameCurIpr;
        }
        private void refreshPlcValue()
        {
            //curCode = valmoWin.dv.iprPr[6].valueNew;
            if (flagLoading)
                return;

            img0.Opacity = 0;
            img1.Opacity = 0;
            img2.Opacity = 0;
            img3.Opacity = 0;
            img4.Opacity = 0;
            img5.Opacity = 0;
            img6.Opacity = 0;
            img7.Opacity = 0;
            img8.Opacity = 0;
            img9.Opacity = 0;
            img10.Opacity = 0;
            img11.Opacity = 0;
            img12.Opacity = 0;
            img13.Opacity = 0;
            img14.Opacity = 0;
            img15.Opacity = 0;
            img17.Opacity = 0;
            img18.Opacity = 0;
            img19.Opacity = 0;
            img20.Opacity = 0;
            img21.Opacity = 0;
            img22.Opacity = 0;
            img23.Opacity = 0;
            img24.Opacity = 0;
            img29.Opacity = 0;
            img30.Opacity = 0;
            img31.Opacity = 0;
            iprCtrl.curUnit.init();
            iprCtrl.curUnit.get_sActName();
            iprCtrl.curUnit.get_sPos();
            iprCtrl.curUnit.get_sNotReady();
            iprCtrl.curUnit.get_sValueB();
            iprCtrl.curUnit.get_sValueC();
            iprCtrl.curUnit.get_sLinkNode();

            showCtrlMenu(iprCtrl.curUnit.sActName);

            iprCtrl1.showFocusMsg();
            
        }
        private void showCtrlMenu(int actName)
        {
            lbUnitObjName.Background = Brushes.Transparent;
            switch (actName)
            {
                #region  不同对象类型对应的操作菜单
                //null
                case 0:
                    {
                        img0.Opacity = 1;
                        tbMenu.SelectedIndex = 0;
                        lbUnitObjName.Content = "";
                    }
                    break;
                //Blank_Line
                case (int)menuType.ipr_op01_blank:
                    {
                        img0.Opacity = 1;
                        tbMenu.SelectedIndex = 0;
                        lbUnitObjName.Content = "";
                        menuGrpPanel.init();
                        menuGrpPanel.Visibility = Visibility.Visible;
                    }
                    break;
                //
                case (int)menuType.ipr_op05_ejectorback:
                    {
                        tbMenu.SelectedItem = menu_set_ejectorOUT;
                        lbUnitObjName.Content = valmoWin.dv.getCurDis("lanKey951");
                        img1.Opacity = 1;
                        set_ejectorOUT1.setValue();
                    }
                    break;
                case (int)menuType.ipr_op04_ejectorfor:
                    {
                        tbMenu.SelectedItem = menu_set_ejectorIN;
                        lbUnitObjName.Content = valmoWin.dv.getCurDis("lanKey952"); //"顶针前进";
                        img2.Opacity = 1;
                        set_ejectorIN1.setValue();
                    }
                    break;
                case (int)menuType.ipr_op06_safetydoorClose:
                    {
                        tbMenu.SelectedItem = menu_set_doorclose;
                        set_doorclose1.setValue();
                        lbUnitObjName.Content = valmoWin.dv.getCurDis("lanKey953"); //"关闭安全门";
                        img3.Opacity = 1;
                    }
                    break;
                case (int)menuType.ipr_op07_safetydoorOpen:
                    {
                        tbMenu.SelectedItem = menu_set_dooropen;
                        set_dooropen1.setValue();
                        lbUnitObjName.Content = valmoWin.dv.getCurDis("lanKey954"); //"打开安全门";
                        img4.Opacity = 1;
                    }
                    break;
                case (int)menuType.ipr_op08_CarrriageFWD:
                    {
                        tbMenu.SelectedItem = menu_set_carriagefwd;
                        set_carriagefwd1.setValue();
                        lbUnitObjName.Content = valmoWin.dv.getCurDis("lanKey955"); //"射台前进";
                        img5.Opacity = 1;
                    }
                    break;
                case (int)menuType.ipr_op09_CarrriageBWD:
                    {
                        tbMenu.SelectedItem = menu_set_carriagebwd;
                        set_carriagebwd1.setValue();
                        lbUnitObjName.Content = valmoWin.dv.getCurDis("lanKey956"); //"射台后退";
                        img6.Opacity = 1;
                    }
                    break;
                case (int)menuType.ipr_op10_ShackMold:
                    {
                        tbMenu.SelectedItem = menu_set_shackmold;
                        set_shackmold1.setValue();
                        lbUnitObjName.Content = valmoWin.dv.getCurDis("lanKey957"); //"排气动作";
                        img7.Opacity = 1;
                    }
                    break;
                case (int)menuType.ipr_op14_Charge:
                    {
                        tbMenu.SelectedItem = menu_set_charge;
                        set_charge1.setValue();
                        lbUnitObjName.Content = valmoWin.dv.getCurDis("lanKey958"); //"储料";
                        img8.Opacity = 1;
                    }
                    break;
                case (int)menuType.ipr_op17_Air:
                    {

                        tbMenu.SelectedItem = menu_set_air;
                        set_air1.setValue();
                        lbUnitObjName.Content = valmoWin.dv.getCurDis("lanKey959"); //"吹气";
                        img9.Opacity = 1;
                    }
                    break;
                case (int)menuType.ipr_op16_Cores:
                    {
                        tbMenu.SelectedItem = menu_set_cores;
                        set_cores1.setValue();
                        lbUnitObjName.Content = valmoWin.dv.getCurDis("lanKey960"); //"中子";
                        img10.Opacity = 1;
                    }
                    break;
                case (int)menuType.ipr_op21_Rottable:
                    {
                        tbMenu.SelectedItem = menu_set_rottable;
                        set_rottable1.setValue();
                        lbUnitObjName.Content = valmoWin.dv.getCurDis("lanKey961"); //"转盘";
                        img11.Opacity = 1;
                    }
                    break;
                case (int)menuType.ipr_op19_Output:
                    {
                        tbMenu.SelectedItem = menu_set_output;
                        set_output1.setValue();
                        lbUnitObjName.Content = valmoWin.dv.getCurDis("lanKey962"); //"可编程输出";
                        img12.Opacity = 1;
                    }
                    break;
                case (int)menuType.ipr_op22_Nozzle:
                    {
                        tbMenu.SelectedItem = menu_set_nozzle;
                        set_nozzle1.setValue();
                        lbUnitObjName.Content = valmoWin.dv.getCurDis("lanKey963"); //"机器喷嘴";
                        img13.Opacity = 1;
                    }
                    break;
                case (int)menuType.ipr_op23_Valvegate:
                    {
                        tbMenu.SelectedItem = menu_set_valvegate;
                        set_valvegate1.setValue();
                        lbUnitObjName.Content = valmoWin.dv.getCurDis("lanKey964"); //"热流道阀针";
                        img14.Opacity = 1;
                    }
                    break;
                case (int)menuType.ipr_op03_delay:
                    {
                        tbMenu.SelectedItem = menu_delay_me;

                        lbUnitObjName.Content = valmoWin.dv.getCurDis("lanKey966"); //"延时";
                        delay_me1.setValue();
                        img17.Opacity = 1;
                    }
                    break;
                case (int)menuType.ipr_op18_Variable:
                    {
                        tbMenu.SelectedItem = menu_set_variable;
                        set_variable1.setValue();
                        lbUnitObjName.Content = valmoWin.dv.getCurDis("lanKey967"); //"变量操作";
                        img18.Opacity = 1;
                    }
                    break;
                case (int)menuType.ipr_op02_wait:
                    {
                        tbMenu.SelectedItem = Mn_Wait_me;
                        lbUnitObjName.Content = valmoWin.dv.getCurDis("lanKey968"); //"等待";
                        img19.Opacity = 1;
                        wait_me1.setValue();
                    }
                    break;
                case (int)menuType.ipr_op26_jump:
                    {
                        tbMenu.SelectedItem = menu_jump_step;
                        jump_step1.setValue();
                        lbUnitObjName.Content = valmoWin.dv.getCurDis("lanKey969"); //"跳转";
                        img20.Opacity = 1;
                    }
                    break;
                case (int)menuType.ipr_op12_Moldopen:
                    {
                        tbMenu.SelectedItem = menu_set_moldopen;
                        set_moldopen1.setValue();
                        lbUnitObjName.Content = valmoWin.dv.getCurDis("lanKey970"); //"开模";
                        img21.Opacity = 1;
                    }
                    break;
                case (int)menuType.ipr_op11_Moldclose:
                    {
                        tbMenu.SelectedItem = menu_set_moldclose;
                        set_moldclose1.setValue();
                        lbUnitObjName.Content = valmoWin.dv.getCurDis("lanKey971"); //"合模";
                        img22.Opacity = 1;
                    }
                    break;
                case (int)menuType.ipr_op13_Injection:
                    {
                        tbMenu.SelectedItem = menu_set_Injection;
                        set_Injection1.setValue();
                        lbUnitObjName.Content = valmoWin.dv.getCurDis("lanKey972"); //"注射";
                        img23.Opacity = 1;
                    }
                    break;
                case (int)menuType.ipr_op15_cooling:
                    {
                        tbMenu.SelectedItem = menu_set_cooling;
                        set_cooling1.setValue();
                        lbUnitObjName.Content = valmoWin.dv.getCurDis("lanKey973"); //"冷却";
                        img24.Opacity = 1;
                    }
                    break;
                case (int)menuType.ipr_op24_seteuromapsignal://设置机械手信号
                    {
                        tbMenu.SelectedItem = menu_set_EMSignal;
                        set_EMSignal1.setValue();
                        lbUnitObjName.Content = valmoWin.dv.getCurDis("ipr_setEmSignal");
                        img29.Opacity = 1;
                    }
                    break;
                case (int)menuType.ipr_op30_waitstartsignal://等待开始信号
                    {
                        tbMenu.SelectedItem = menu_waitstartSignal;
                        wait_startSignal1.setValue();
                        lbUnitObjName.Content = valmoWin.dv.getCurDis("ipr_waitStartSignal");
                        img30.Opacity = 1;
                    }
                    break;
                case (int)menuType.ipr_op29_waiteuromapsignal://等待机械手信号
                    {
                        tbMenu.SelectedItem = menu_wait_EMSignal;
                        wait_EMSignal1.setValue();
                        lbUnitObjName.Content = valmoWin.dv.getCurDis("ipr_waitEmSignal");
                        img31.Opacity = 1;
                    }
                    break;
                #endregion
            }
            
        }

        private void readDataByPartNrFromFile(iprUnitCtrl itpUnitCtrl, int num)
        {
            for (int i = 0; i < 30; i++)
            {
                byte[] data = new byte[lenLine];
                string strTmp = ",★";
                string[] str;
                //fsInterpretorParam.Read(data, 0, lenLine);
                strTmp = System.Text.Encoding.Default.GetString(data);
                str = strTmp.Split('★');
                //curUnit.setValue(str);
                //curUnit.sPos = ((i % 10) << 16) & ((i / 3) << 8) & num;
                ////curUnit.sPartNr = num;
                ////curUnit.sRowNr = i % 5;
                ////curUnit.sLineNr = i / 6;
                //itpUnitCtrl.showFocusMsg(curUnit);
            }
            //itpUnitCtrl.removeFocus();
        }
        int[] curDif = new int[10];
        private void startUpInit()
        {
            //loadShadePanel.show();
            loadShadePanel2.hide();
            valmoWin.dv.IprPr[16].valueNew = 1;
        }

        private void refreshCtrlsValueOfEveryPart(int partNr)
        {
            if (flagLoading)
                return;
            int curDif = valmoWin.dv.IprPr[54 + partNr].value;
            if (curDif == 0)
                return;
            List<int> checkLst = new List<int>();
            for (int j = 1; j < 31; j++)
            {
                if (((curDif >> j) & 0x01) == 1)
                {
                    checkLst.Add(j - 1);
                }
            }
            for (int j = 0; j < checkLst.Count; j++)
            {
                valmoWin.dv.IprPr[17].valueNew = ((checkLst[j] % 10) << 16) | ((checkLst[j] / 10) << 8) | partNr;
                refreshPlcValue();
            }
            valmoWin.dv.IprPr[54 + partNr].valueNew = 0;
            vm.print("partNr : " + partNr + " ☆☆☆");
            for (int i = 31; i >=  0; i--)
            {
                Console.Write("{0}", (curDif >> i) & 0x01);
                if (i  % 8 == 0)
                    Console.Write("\t");
            }
            vm.printLn("☆☆☆");
        }
        private void handleIprPr_4(objUnit obj)
        {
            flagRdyToOp = (obj.value == 1 ? true : false);
        }
        private void handleIprPr_3(objUnit obj)
        {
            lbIprPr_3.Content = obj.vDblStr + "%";
            if (loadShadePanel2.Visibility == Visibility.Visible)
            {
                if ((int)obj.vDbl < 100)
                {
                    prgComp.Opacity = 1;
                    prgComp.Value = obj.vDbl;
                }
                else
                {
                    prgComp.Opacity = 0;
                    this.loadShadePanel2.hide();
                }
            }
        }
        private void handleIprPr_5(objUnit obj)
        {
            switch (obj.value)
            {
                case 0:
                    {
                        TabContrlOPMode.SelectedIndex = 0;
                    }
                    break;
                case 1:
                    {
                        TabContrlOPMode.SelectedIndex = 1;
                    }
                    break;
                case 2:
                    {
                        TabContrlOPMode.SelectedIndex = 2;
                    }
                    break;
                case 3:
                    {
                        TabContrlOPMode.SelectedIndex = 3;
                    }
                    break;
                case 4:
                    {
                        TabContrlOPMode.SelectedIndex = 4;
                    }
                    break;
                case 8:
                    {
                        TabContrlOPMode.SelectedIndex = 8;
                    }
                    break;
                case 9:
                    {
                        TabContrlOPMode.SelectedIndex = 9;
                    }
                    break;
                default:
                    {

                        TabContrlOPMode.SelectedIndex = 0;
                    }
                    break;
            }
        }

        private void handleIprPr_20(objUnit obj)
        {
            if (obj.value != 0)
            {
                cmpPanel.clear();
                cmpPanel.show();
                for (int i = 0; i < 32; i++)
                {
                    if (((obj.value >> i) & 0x01) == 1)
                    {
                        cmpPanel.add(valmoWin.dv.getCurDis( strWarn[i]));
                    }
                }
            }
            else
            {
                cmpPanel.clear();
                cmpPanel.hide();
            }
        }
        private void handleIprPr_55(objUnit obj)
        {
                refreshCtrlsValueOfEveryPart(1);
        }
        private void handleIprPr_56(objUnit obj)
        {
            refreshCtrlsValueOfEveryPart(2);
        }
        private void handleIprPr_57(objUnit obj)
        {
            refreshCtrlsValueOfEveryPart(3);
        }
        private void handleIprPr_58(objUnit obj)
        {
                refreshCtrlsValueOfEveryPart(4);
        }
        private void handleIprPr_59(objUnit obj)
        {
                refreshCtrlsValueOfEveryPart(5);
        }
        private void handleIprPr_60(objUnit obj)
        {
                refreshCtrlsValueOfEveryPart(6);
        }
        bool startJumpHeart = false;
        private void handleIprPr_61(objUnit obj)
        {
            if(obj.value == 0 && !startJumpHeart)
            {
                startJumpHeart = true;
                valmoWin.flagStartUpOk = true;
                valmoWin.execHandle(opeOrderType.winMsg, new WinMsg(WinMsgType.msJumpHeartStart, null));
                iprCtrl1.refreshCtrlWidth();
            }
            refreshCtrlsValueOfEveryPart(7);
            if (flagNeedSaveIprFile)
            {
                flagNeedSaveIprFile = false;
                //this.Dispatcher.BeginInvoke(new nullEvent(makeNewIprFile));
            }
        }
        private void handleIprPr_34(objUnit obj)
        {
            refreshPlcValue();
        }
        public void saveFile()
        {
            progressPanel.dis = "正在保存当前文件";
            progressPanel.show();
        }

        int total = 0;
        List<iprUnitObj> lstIprCtr = new List<iprUnitObj>();
        XmlNodeList xnlSysPr67 = null;
        XmlNodeList xnlNormal = null;
        int exportCount = 0;
        int curLstEpNr = 0;
        int curStep = 0;
        XmlDocument xmlExport = new XmlDocument();
        XmlNode xnIpr;
        XmlNode xnSysPr67;
        XmlNode xnNormal;
        string exportFileName;

        public void makeNewIprFile(string fileName)
        {
            try
            {
                xnlSysPr67 = null;
                xnlNormal = null;
                xnIpr = null;
                xnSysPr67 = null;
                xnNormal = null;

                exportFileName = prgDir + "\\" + fileName;
                curLstEpNr = 0;
                lstIprCtr.Clear();
                for (int i = 0; i < 210; i++)
                {
                    lstIprCtr.Add(valmoWin.SIprCtrl.getUnitObj(i));
                }
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("ipr\\fileConf.xml");
                XmlNode root = xmlDoc.SelectSingleNode("FileItems");//查找<bookstore>
                foreach (XmlNode xn in root.ChildNodes)
                {
                    switch (xn.Name)
                    {
                        case "SysPr67":
                            xnlSysPr67 = xn.ChildNodes;
                            break;
                        case "Normal":
                            xnlNormal = xn.ChildNodes;
                            break;
                    }
                }
                exportCount = lstIprCtr.Count + xnlSysPr67.Count + xnlNormal.Count;
                curStep = 0;

                XmlTextWriter writer = new XmlTextWriter(exportFileName, System.Text.Encoding.UTF8);
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument();
                writer.WriteStartElement("FileItems");
                writer.WriteStartElement("Ipr");
                //writer.WriteAttributeString("user", valmoWin.dv.users.curUser.name);
                writer.WriteEndElement();//end of param

                writer.WriteStartElement("SysPr67");
                //writer.WriteAttributeString("user", valmoWin.dv.users.curUser.name);
                writer.WriteEndElement();//end of param

                writer.WriteStartElement("Normal");
                //writer.WriteAttributeString("user", valmoWin.dv.users.curUser.name);
                writer.WriteEndElement();//end of param

                //saveIprFileFunc(writer);
                writer.WriteEndElement();//end of ipr
                writer.Close();
                xmlExport.Load(exportFileName);
                XmlNode xnRoot = xmlExport.SelectSingleNode("FileItems");
                foreach (XmlNode xn in xnRoot.ChildNodes)
                {
                    switch (xn.Name)
                    {
                        case "Ipr":
                            xnIpr = xn;
                            break;
                        case "SysPr67":
                            xnSysPr67 = xn;
                            break;
                        case "Normal":
                            xnNormal = xn;
                            break;
                    }
                }
                //xnIpr = xmlExport.SelectSingleNode("Ipr");
                //xnSysPr67 = xmlExport.SelectSingleNode("SysPr67");
                //xnNormal = xmlExport.SelectSingleNode("Normal");
                //if()

                valmoWin.SExportFilePanel.show(exportIprFileFunc, null, exportFileName, exportCount);
            }
            catch
            {
                MessageBox.Show("save error !");
            }
        }

        private double exportIprFileFunc(ref int num)
        {
            double rate = 0;
            num = 1;
            switch (curStep)
            {
                case 0:
                    {
                        if (curLstEpNr < lstIprCtr.Count && xnIpr != null)
                        {
                            lstIprCtr[curLstEpNr].save(xnIpr, xmlExport);
                            rate = 100.0 * curLstEpNr / exportCount;
                            curLstEpNr++;
                        }
                        else
                        {
                            curLstEpNr = 0;
                            curStep = 1;
                            rate = rate = 100.0 * curLstEpNr / exportCount;
                        }
                        
                    }
                    break;
                case 1:
                    {
                        if (curLstEpNr < xnlSysPr67.Count && xnSysPr67 != null)
                        {
                            filterValue(xnlSysPr67[curLstEpNr].InnerText, xnSysPr67, xmlExport);
                            rate = 100.0 * (curLstEpNr + lstIprCtr.Count) / exportCount;
                            curLstEpNr++;
                        }
                        else
                        {
                            curLstEpNr = 0;
                            curStep = 2;
                            rate = 100.0 * (curLstEpNr + lstIprCtr.Count) / exportCount;
                        }
                        
                    }

                    break;
                case 2:
                    {
                        if (curLstEpNr < xnlNormal.Count && xnNormal != null)
                        {
                            filterValue(xnlNormal[curLstEpNr].InnerText, xnNormal, xmlExport);
                            rate = 100.0 * (curLstEpNr + lstIprCtr.Count + xnlSysPr67.Count) / exportCount;
                            curLstEpNr++;
                        }
                        else
                        {
                            xmlExport.Save(exportFileName);
                            rate = 100;
                        }
                        
                    }
                    break;
                case 3:
                    //dtLoad.Stop();
                    break;
            }
            return rate;
        }
        private void filterValue(string str, XmlNode xn, XmlDocument xDoc)
        {
            try
            {
                int AddrPlc = 0;
                int valuePlc = 0;
                string addr = str;
                string[] tmpStr = addr.Split('.');
                if (tmpStr.Length == 2)
                {
                    LinkMgr.getObjPlcAddr(addr, ref AddrPlc, tmpStr[0]);
                    Lasal32.LslReadFromSvr(AddrPlc, ref valuePlc);
                    //XmlElement xe = new XmlElement();
                    XmlElement item = xDoc.CreateElement("item");
                    item.SetAttribute("value", valuePlc.ToString());
                    item.InnerText = str;
                    //xmlDoc.AppendChild(item);
                    xn.AppendChild(item);
                    //vm.printLn(count1++ + "\t" + str + "\t" + valuePlc);
                }


            }
            catch (System.Exception ex)
            {
                vm.perror("[filterValue]\t" + str + " Error");
            }
        }

        double rateNr = 0;
        private double saveFunc()
        {
            rateNr += 1;
            return rateNr;
        }
        public void reNamePrgFile(string filename)
        {
            try
            {

                File.Move(prgDir + prgFileName, prgDir + filename);
                //setCurPrgnameFunc(filename);
            }
            catch(Exception ex)
            {
                //setCurPrgnameFunc(filename);
                //saveNewIprFile();
            }
        }
        private void imgDel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            imgDel.Opacity = 1;
        }
        private void imgMove_MouseDown(object sender, MouseButtonEventArgs e)
        {
            imgMove.Opacity = 1;
        }
        private void imgMdy_MouseDown(object sender, MouseButtonEventArgs e)
        {
            imgMdy.Opacity = 1;
        }
        private void imgDel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            imgDel.Opacity = 0;
            valmoWin.dv.IprPr[36].valueNew = 0;

        }
        private void imgMove_MouseUp(object sender, MouseButtonEventArgs e)
        {
            imgMove.Opacity = 0;
        }

        private void imgMdy_MouseUp(object sender, MouseButtonEventArgs e)
        {
            imgMdy.Opacity = 0;
            if (tbMenu.SelectedIndex != 0)
            {
                tbMenu.SelectedIndex = 0;
                lbUnitObjName.Background = Brushes.Transparent;
                lbUnitObjName.Content = "";
            }

        }

        private void imgDel_MouseLeave(object sender, MouseEventArgs e)
        {
            imgDel.Opacity = 0;
        }
        private void imgMove_MouseLeave(object sender, MouseEventArgs e)
        {
            imgMove.Opacity = 0;
        }
        private void imgMdy_MouseLeave(object sender, MouseEventArgs e)
        {
            imgMdy.Opacity = 0;
        }

        private void imgNew_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.IprPr[1].accessLevel))
            {
                return;
            }
            imgNew.Opacity = 1;
        }


        private void ConfirmSave()
        {
            tbMain.SelectedIndex = 0;
            imgNew.Opacity = 0;
            imgSave.Opacity = 0;
            imgSetting.Opacity = 0;
            imgLoad.Opacity = 0;
            cvsHideModify.Visibility = Visibility.Hidden;
            this.Dispatcher.BeginInvoke(new nullEvent(makeNewIprFunc));
        }

        private void imgNew_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (imgNew.Opacity != 1)
            {
                return;
            }

            valmoWin.sMsgBox.Show(App.Current.TryFindResource("lanKey2202").ToString(), App.Current.TryFindResource("lanKey2203").ToString(), ConfirmSave, null);
        }

        private void lbUnitObjName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (tbMenu.SelectedIndex != 0)
            {
                tbMenu.SelectedIndex = 0;
                lbUnitObjName.Background = Brushes.LightGreen;
            }
            else
            {
                lbUnitObjName.Background = Brushes.Transparent;
                showCtrlMenu(iprCtrl.curUnit.sActName);
            }
        }

        private void imgComp_MouseDown(object sender, MouseButtonEventArgs e)
        {
            imgComp.Opacity = 1;
            //imgCompFore.Opacity = 1;
            Canvas.SetLeft(prgComp, 892);
        }

        private void imgComp_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (valmoWin.dv.IprPr[19].valueNew == 0)
                valmoWin.dv.IprPr[19].valueNew = 1;
            else
                valmoWin.dv.IprPr[19].valueNew = 0;
            tbMain.SelectedIndex = 0;
            imgSave.Opacity = 0;
            imgLoad.Opacity = 0;
            imgComp.Opacity = 0;
            imgSetting.Opacity = 0;
            cvsHideModify.Visibility = Visibility.Hidden;
        }

        private void imgSave_MouseDown(object sender, MouseButtonEventArgs e)
        {
            imgSave.Opacity = 1;
        }

        private void imgSave_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (tbMain.SelectedIndex != 1)
            {
                tbMain.SelectedIndex = 1;
                iprSaveCtrl1.init();
                imgSave.Opacity = 0.7;
                imgLoad.Opacity = 0;
                imgSetting.Opacity = 0;
                cvsHideModify.Visibility = Visibility.Visible;
            }
            else
            {
                tbMain.SelectedIndex = 0;
                imgSave.Opacity = 0;
                cvsHideModify.Visibility = Visibility.Hidden;
            }
        }

        private void imgLoad_MouseDown(object sender, MouseButtonEventArgs e)
        {
            imgLoad.Opacity = 1;
            Canvas.SetLeft(prgComp, 799);
        }

        public static bool flagLoading = false;
        private void imgLoad_MouseUp(object sender, MouseButtonEventArgs e)
        {
            
            if (tbMain.SelectedIndex != 2)
            {
                tbMain.SelectedIndex = 2;
                iprLoadCtrl1.init();
                imgLoad.Opacity = 0.7;
                imgSave.Opacity = 0;
                imgSetting.Opacity = 0;
                cvsHideModify.Visibility = Visibility.Visible;
            }
            else
            {
                tbMain.SelectedIndex = 0;
                imgLoad.Opacity = 0;
                cvsHideModify.Visibility = Visibility.Hidden;
            }
            //loadIprPanel.show();
        }
        int curLineNr = 0;
        private void dtLoad_tick(object sender, EventArgs e)
        {
            try
            {
                //this.Dispatcher.BeginInvoke(new nullEvent(dtLoadFunc));
            }
            catch (Exception ex)
            {
                flagLoading = false;
                vm.perror(ex.ToString());
            }
        }

        XmlNodeList valueLst;
        int curLstNr = 0;
        public void loadIprFileFunc(FileInfo file)
        {
            try
            {
                valmoWin.dv.IprPr[1].valueNew = 1;

                int ret = -1;
                Thread.Sleep(100);
                ret = valmoWin.dv.IprPr[1].valueNew;
                if (ret == 1)
                {
                    prgFileName = file.Name;
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(file.FullName);
                    XmlNodeList root = xmlDoc.SelectSingleNode("FileItems").ChildNodes;
                    curStep = 0;
                    curLstNr = 0;
                    foreach (XmlNode xn in root)
                    {
                        switch (xn.Name)
                        {
                            case "Ipr":
                                xnIpr = xn;
                                break;
                            case "SysPr67":
                                xnSysPr67 = xn;
                                break;
                            case "Normal":
                                xnNormal = xn;
                                break;
                        }
                    }
                    valmoWin.dv.IprPr[5].valueNew = 8;
                    iprCtrl.curUnit.init();
                    iprCtrl1.cleanMsg();
                    exportCount = xnIpr.ChildNodes.Count + xnSysPr67.ChildNodes.Count + xnNormal.ChildNodes.Count;
                    valmoWin.SLoadIprFilePanel.show(loadIprFunc, null, file.FullName, exportCount);
                    //读取param 节点
                    //XmlNode xn = root.Item(0) as XmlNode;
                    //valueLst = (root.Item(1) as XmlNode).ChildNodes;

                    //if (valueLst != null)
                    //{
                    //    flagLoading = true;
                    //    curLstNr = 0;
                    //    valmoWin.dv.IprPr[5].valueNew = 8;
                    //    iprCtrl.curUnit.init();
                    //    iprCtrl1.cleanMsg();
                    //    //return;
                    //    progressPanel.dis = "正在导入教导文件";
                    //    valmoWin.dv.IprPr[3].valueNew = valueLst.Count;
                    //    progressPanel.show(loadFunc,disposeLoad);
                    //    //dtLoad.Start();
                    //}
                }
                else
                {
                    vm.perror("load error!");
                }
            }
            catch (Exception ex)
            {
                vm.perror("load Ipr File error!\t" + ex.ToString());
                valmoWin.dv.IprPr[5].valueNew = 1;
                flagLoading = false;
                fsIpr.Close();
            }
            loadIprPanel.hide();
        }
        private double loadIprFunc(ref int num)
        {
            double rate = 0;
            switch (curStep)
            {
                case 0:
                    {
                        if (curLstNr < xnIpr.ChildNodes.Count)
                        {
                            iprCtrl.curUnit.load(xnIpr.ChildNodes[curLstNr]);
                            rate = 100.0 * curLstNr / exportCount;
                            curLstNr++;
                        }
                        else
                        {
                            curStep = 1;
                            rate = 100.0 * (curLstNr - 1) / exportCount;
                            curLstNr = 0;

                        }
                       
                    }
                    break;
                case 1:
                    {
                        if (curLstNr < xnSysPr67.ChildNodes.Count)
                        {
                            loadItemValue(xnSysPr67.ChildNodes[curLstNr]);
                            rate = 100.0 * (curLstNr + xnIpr.ChildNodes.Count) / exportCount;
                            curLstNr++;
                        }
                        else
                        {
                            curStep = 2;
                            rate = 100.0 * (curLstNr - 1 + xnIpr.ChildNodes.Count) / exportCount;
                            curLstNr = 0;
                        }
                    }
                    break;
                case 2:
                    {
                        if (curLstNr < xnNormal.ChildNodes.Count)
                        {
                            loadItemValue(xnNormal.ChildNodes[curLstNr]);
                            rate = 100.0 * (curLstNr + xnIpr.ChildNodes.Count + xnSysPr67.ChildNodes.Count) / exportCount;
                            curLstNr++;
                        }
                        else
                        {
                            curStep = 3;
                            rate = 100.0 * (curLstNr - 1 + xnIpr.ChildNodes.Count + xnSysPr67.ChildNodes.Count) / exportCount;
                            curLstNr = 0;
                        }
                    }
                    break;
                case 3:
                    {
                        valmoWin.dv.IprPr[5].valueNew = 1;
                        valmoWin.dv.IprPr[16].valueNew = 1;
                        tbMain.SelectedIndex = 0;
                        imgSetting.Opacity = 0;
                        imgLoad.Opacity = 0;
                        imgSave.Opacity = 0;
                        cvsHideModify.Visibility = Visibility.Hidden;
                        rate = 100;
                    }
                    break;
            }
            return rate;
        }
        private double loadFunc()
        {
            if (flagLoading)
            {
                if (curLstNr < valueLst.Count)
                {
                    iprCtrl.curUnit.load(valueLst[curLstNr++]);
                    double Value = 100.0 * curLstNr / valueLst.Count;
                    return Value;
                }
                else
                {
                    return 100;
                }
            }
            return 0;
        }
        private void loadItemValue(XmlNode xn)
        {
            try
            {
                XmlElement xe = xn as XmlElement;
                string addr = xe.InnerText;
                int AddrPlc = 0;
                string[] tmpStr = addr.Split('.');
                {
                    int value = Int32.Parse(xe.GetAttribute("value"));
                    LinkMgr.getObjPlcAddr(addr, ref AddrPlc, tmpStr[0]);
                    //Lasal32.LslReadFromSvr(AddrPlc, ref valuePlc);
                    Lasal32.LslWriteToSvr(AddrPlc, value);
                }

            }
            catch (System.Exception ex)
            {
                vm.perror("[loadItemValue] error!");
            }
        }
        public void disposeLoad()
        {
            flagLoading = false;
            valmoWin.dv.IprPr[5].valueNew = 1;
            valmoWin.dv.IprPr[16].valueNew = 1;
        }

        FileStream fsSaveIpr;
        private void dtSave_tick(object sender, EventArgs e)
        {
            try
            {
                this.Dispatcher.BeginInvoke(new EventHandler(dtSaveFunc), new object[] { sender, e });
            }
            catch (Exception ex)
            {
                vm.perror(ex.ToString());
            }
        }
        int curSaveIprNr = 0;
        public void dtSaveFunc(object sender, EventArgs e)
        {
            if (fsSaveIpr != null && curSaveIprNr <= 300)
            {
                iprCtrl1.saveUnit(fsSaveIpr,curSaveIprNr++);
                prgComp.Value = 100.0 * curSaveIprNr / 300.0;
                vm.debug(prgComp.Value.ToString());
            }
            else
            {
                fsSaveIpr.Close();
                prgComp.Opacity = 0;
                curSaveIprNr = 0;
                dtSave.Stop();
            }
        }

        private void imgComp_MouseLeave(object sender, MouseEventArgs e)
        {
            if (imgComp.Opacity == 1)
            {
                if (valmoWin.dv.IprPr[19].valueNew == 0)
                    valmoWin.dv.IprPr[19].valueNew = 1;
                else
                    valmoWin.dv.IprPr[19].valueNew = 0;
                tbMain.SelectedIndex = 0;
                imgSave.Opacity = 0;
                imgLoad.Opacity = 0;
                imgComp.Opacity = 0;
                imgSetting.Opacity = 0;
                cvsHideModify.Visibility = Visibility.Hidden;
            }
            
        }

        private void imgSave_MouseLeave(object sender, MouseEventArgs e)
        {
            if (imgSave.Opacity == 1)
            {
                if (tbMain.SelectedIndex != 1)
                {
                    tbMain.SelectedIndex = 1;
                    iprSaveCtrl1.init();
                    imgSave.Opacity = 0.7;
                    imgLoad.Opacity = 0;
                    imgSetting.Opacity = 0;
                    cvsHideModify.Visibility = Visibility.Visible;
                }
                else
                {
                    tbMain.SelectedIndex = 0;
                    imgSave.Opacity = 0;
                    cvsHideModify.Visibility = Visibility.Hidden;
                }
            }
        }

        private void imgLoad_MouseLeave(object sender, MouseEventArgs e)
        {
            if (imgLoad.Opacity == 1)
            {
                if (tbMain.SelectedIndex != 2)
                {
                    tbMain.SelectedIndex = 2;
                    iprLoadCtrl1.init();
                    imgLoad.Opacity = 0.7;
                    imgSave.Opacity = 0;
                    imgSetting.Opacity = 0;
                    cvsHideModify.Visibility = Visibility.Visible;
                }
                else
                {
                    tbMain.SelectedIndex = 0;
                    imgLoad.Opacity = 0;
                    cvsHideModify.Visibility = Visibility.Hidden;
                }
            }
        }

        private void imgNew_MouseLeave(object sender, MouseEventArgs e)
        {
            if (imgNew.Opacity == 1)
            {
                imgNew.Opacity = 0;
                //cvsHideModify.Visibility = Visibility.Hidden;
            }
        }

        public void refreshLan()
        {
            showCtrlMenu(iprCtrl.curUnit.sActName);
            menuGrpPanel.refreshLan();
            iprCtrl1.refreshLan();

            int value = valmoWin.dv.IprPr[20].value;
            if (value != 0)
            {
                cmpPanel.clear();
                //cmpPanel.Visibility = Visibility.Visible;
                for (int i = 0; i < 32; i++)
                {
                    if (((value >> i) & 0x01) == 1)
                    {
                        cmpPanel.add(valmoWin.dv.getCurDis(strWarn[i]));
                    }
                }
            }
        }

        private void lbLocal_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tbMain.SelectedIndex = 1;
            iprSaveCtrl1.init();
        }

        public void refreshDriver()
        {
            try
            {
                //DriveInfo[] uin = DriveInfo.GetDrives();
                //bool flagGetUsb = false;
                //foreach (DriveInfo drive in uin)
                //{
                //    if (drive.DriveType == DriveType.Removable)
                //    {
                //        flagGetUsb = true;
                //        break;
                //    }
                //}
                bool flagGetUsb = valmoWin.sUsbPath != null;

                iprSaveCtrl1.usbEnable = flagGetUsb;
                iprLoadCtrl1.usbEnable = flagGetUsb;
            }
            catch
            {

            }
        }

        private void refreshCurIprFile()
        {
            if (fsCurIprPrg != null)
            {

                //btnDel.Visibility = Visibility.Hidden;
            }
        }

        private void lbPrgName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            iprSaveCtrl1.init();
        }

        private void makeNewIprFunc()
        {
            try
            {
                valmoWin.dv.IprPr[1].valueNew = 1;

                int ret = -1;
                Thread.Sleep(100);
                ret = valmoWin.dv.IprPr[1].valueNew;
                if (ret == 1)
                {
                    iprCtrl.curUnit.init();
                    iprCtrl1.cleanMsg();
                    valmoWin.dv.IprPr[16].valueNew = 1;
                    //valmoWin.dv.IprPr[17].valueNew = 1;
                    iprCtrl1.iprUnitCtrl1.iprUnitObjCtrlStart.flagUp = false;
                    iprCtrl1.iprUnitCtrl1.iprUnitObjCtrlStart.flagDown = false;
                    iprCtrl1.iprUnitCtrl2.iprUnitObjCtrlStart.flagUp = false;
                    iprCtrl1.iprUnitCtrl2.iprUnitObjCtrlStart.flagDown = false;
                    iprCtrl1.iprUnitCtrl3.iprUnitObjCtrlStart.flagUp = false;
                    iprCtrl1.iprUnitCtrl3.iprUnitObjCtrlStart.flagDown = false;
                    iprCtrl1.iprUnitCtrl4.iprUnitObjCtrlStart.flagUp = false;
                    iprCtrl1.iprUnitCtrl4.iprUnitObjCtrlStart.flagDown = false;
                    iprCtrl1.iprUnitCtrl5.iprUnitObjCtrlStart.flagUp = false;
                    iprCtrl1.iprUnitCtrl5.iprUnitObjCtrlStart.flagDown = false;
                    iprCtrl1.iprUnitCtrl6.iprUnitObjCtrlStart.flagUp = false;
                    iprCtrl1.iprUnitCtrl6.iprUnitObjCtrlStart.flagDown = false;
                    iprCtrl1.iprUnitCtrl7.iprUnitObjCtrlStart.flagUp = false;
                    iprCtrl1.iprUnitCtrl7.iprUnitObjCtrlStart.flagDown = false;
                }

                bool getFileName = false;

                string newFilename = "NewProject0.ipr";
                int tmpNr = 0;
                while (true)
                {
                    foreach (FileInfo file in prgDir.GetFiles())
                    {
                        if (file.Name == newFilename)
                        {
                            getFileName = true;
                            break;
                        }
                    }
                    if (newFilename + ".tmp" == prgFileName)
                    {
                        getFileName = true;
                    }
                    if (!getFileName)
                    {
                        break;
                    }
                    else
                    {
                        tmpNr++;
                        newFilename = "NewProject" + tmpNr + ".ipr";
                        getFileName = false;
                    }
                }
                prgFileName = newFilename;
                //setCurPrgnameFunc(newFilename);
                flagNeedSaveIprFile = true;
            }
            catch (Exception ex)
            {
                vm.perror("[makeNewIprFunc]" + ex.ToString());
            }
        }

        public string saveCurPrgToFile(string filename)
        {
            if (filename.Length > 4)
            {
                if (filename.Substring(filename.Length - 4, 4) != ".ipr")
                {
                    filename += ".ipr";
                }
            }
            else
            {
                filename += ".ipr";
            }
            makeNewIprFile(filename);
            return filename;
            //if (prgFileName != filename)
            //{
            //    if (File.Exists(prgDir + "/" + filename))
            //        File.Delete(prgDir + "/" + filename);
            
            //    //if (prgFileinfo.Extension == ".tmp")
            //    //{
            //    //    File.Move(prgDir + "/" + prgFilename, prgDir + "/" + filename);
            //    //}
            //    //else
            //    //{
            //    //    if(prgFilename != filename)
            //    File.Copy(prgDir + "/" + prgFileName, prgDir + "/" + filename);
            //    //}
            //}
            //setCurPrgnameFunc(filename);
        }

        private void iprFileInit(FileStream fsCur, string username, string dis)
        {
            fsCurIprPrg.Seek(0, SeekOrigin.Begin);
            byte[] buffer = System.Text.Encoding.Default.GetBytes("username" + "☆" + username + "☆");
            fsCurIprPrg.Write(buffer, 0, buffer.Length);
            for (int i = 0; i < 100 - buffer.Length; i++)
            {
                fsCurIprPrg.WriteByte(0);
            }
            //iprSave();
            byte[] disBuf = System.Text.Encoding.Default.GetBytes(dis + "☆");
            fsCurIprPrg.Seek(100 + 210 * 500, SeekOrigin.Begin);
            fsCurIprPrg.Write(disBuf, 0, disBuf.Length);
            //vm.debug(fsCurIprPrg.Position.ToString());
            fsCurIprPrg.Flush();
        }
        private void iprSave()
        {
            //fsCurIprPrg = prgStream;
            fsCurIprPrg.Seek(100, 0);
            iprCtrl1.save(fsCurIprPrg);
            fsCurIprPrg.Close();

        }
        public void saveIprFileFunc()
        {
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(prgDir + "/" + prgFileName);
                XmlNode xn = xmldoc.SelectSingleNode("ipr");
                iprCtrl1.save(xn,xmldoc);
                xmldoc.Save(prgDir + "/" + prgFileName);
            }
            catch (System.Exception ex)
            {

                XmlTextWriter writer = new XmlTextWriter(prgDir + "/" + prgFileName, System.Text.Encoding.UTF8);
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument();
                writer.WriteStartElement("ipr");
                    writer.WriteStartElement("param");
                    writer.WriteAttributeString("user", valmoWin.dv.users.curUser.name);
                    writer.WriteEndElement();//end of param
                        saveIprFileFunc(writer);
                writer.WriteEndElement();//end of ipr
                writer.Close();

            }
        }
        public void saveIprFileFunc(FileStream fstream)
        {
            iprCtrl1.save(fstream);
            XmlTextWriter writer = new XmlTextWriter("ipr/ipr.xml", System.Text.Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument();
            writer.WriteStartElement("Ipr");
            iprCtrl1.save(writer);
            writer.WriteEndElement();
            writer.Close(); 
        }

        public void saveIprFileFunc(XmlTextWriter writer)
        {
            try
            {
                vm.debug("start to write!!!!!!!!!!!!!!!!!!!!!");
                writer.WriteStartElement("items");
                iprCtrl1.save(writer);
                writer.WriteEndElement();//end of items
                vm.debug("write over!!!!!!!!!!!!!!!!!!!!!!!!!!");
            }
            catch (System.Exception ex)
            {
                vm.perror("[saveIprFileFunc_XmlTextWriter]");
            }

        }
        public void renameCurIpr(string newfilename)
        {
            if (newfilename.Length > 4)
            {
                if (newfilename.Substring(newfilename.Length - 4, 4) != ".ipr")
                {
                    newfilename += ".ipr";
                }
            }
            else
            {
                newfilename += ".ipr";
            }
            if (prgFileinfo.Extension == ".tmp")
            {
                iprSave();
                File.Move(prgFileinfo.FullName, prgDir + "/" + newfilename + ".tmp");
                //setCurPrgnameFunc(newfilename + ".tmp");
                fsCurIprPrg = null;
            }
            else if (prgFileinfo.Extension == ".ipr")
            {
                if (prgFileinfo.Name == newfilename)
                {
                    fsCurIprPrg.Seek(100, SeekOrigin.Begin);
                    saveIprFileFunc(fsCurIprPrg);
                    fsCurIprPrg.Flush();
                }
                bool getExistFile = false;
                foreach (FileInfo file in prgDir.GetFiles())
                {
                    if (file.Name == newfilename)
                    {
                        getExistFile = true;
                        break;
                    }
                }
                if (getExistFile)
                {
                    File.Delete(prgDir + "/" + newfilename);
                }
                iprSave();
                File.Move(prgFileinfo.FullName, prgDir + "/" + newfilename);
                //setCurPrgnameFunc(newfilename);
                fsCurIprPrg = null;
                iprSaveCtrl1.init();
            }
        }

        bool flagSettingMousedown = false;
        private void imgSetting_MouseDown(object sender, MouseButtonEventArgs e)
        {
            flagSettingMousedown = true;
            imgSetting.Opacity = 1;
        }

        private void imgSetting_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (flagSettingMousedown)
            {
                flagSettingMousedown = false;
                if (tbMain.SelectedIndex != 3)
                {
                    tbMain.SelectedIndex = 3;
                    imgSetting.Opacity = 0.7;
                    imgLoad.Opacity = 0;
                    imgSave.Opacity = 0;
                    cvsHideModify.Visibility = Visibility.Visible;
                }
                else
                {
                    tbMain.SelectedIndex = 0;
                    imgSetting.Opacity = 0;
                    cvsHideModify.Visibility = Visibility.Hidden;
                }
            }
        }

        private void imgSetting_MouseLeave(object sender, MouseEventArgs e)
        {
            if (flagSettingMousedown)
            {
                flagSettingMousedown = false;
                if (tbMain.SelectedIndex != 3)
                {
                    tbMain.SelectedIndex = 3;
                    imgSetting.Opacity = 0.7;
                    imgLoad.Opacity = 0;
                    imgSave.Opacity = 0;
                    cvsHideModify.Visibility = Visibility.Visible;
                }
                else
                {
                    tbMain.SelectedIndex = 0;
                    imgSetting.Opacity = 0;
                    cvsHideModify.Visibility = Visibility.Hidden;
                }
            }
        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            iprCtrl1.iprUnitCtrl1.show();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            confirmPanel.hide();
        }

        public iprUnitObj getUnitObj(int num)
        {
            return iprCtrl1.getUnitObj(num);
        }
    }
}
