using System;
using Microsoft.Win32;
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
using System.Windows.Media.Animation;
using nsVicoClient.ctrls;
using System.Windows.Threading;
using System.Threading;
using System.IO;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using System.Windows.Controls.Primitives;
using nsDataMgr;
using log4net;
using OpenHardwareMonitor;
using OpenHardwareMonitor.Hardware;

namespace nsVicoClient
{
    public delegate bool numEnterEvent(double value, objUnit obj = null);

    public delegate void ShowWinMsgEvent(WinMsgType msg, string str);

    public delegate void refreshLstHandleEvent(callBackLstEvent handle, uint dwId, int value);
    public delegate void refreshCallbackGrpEvent(objUnit obj, callbackGrp grp, int value);
    public delegate void sendMsgToWinEvent(WinMsg msg);

    public delegate void execEvent(opeOrderType opeType, WinMsg msg = null);
    public delegate void execMsgEvent(WinMsg msg);
   
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class valmoWin : Window
    {
        exportEventHistoryCtrl exportEventHistoryPanel = new exportEventHistoryCtrl();
        exportIprFileCtrl exportIprFilePanel = new exportIprFileCtrl();
        loadIprFileCtrl loadIprFilePanle = new loadIprFileCtrl();
        upgradeCtrl upgradePanel = new upgradeCtrl();

        public static int MainPanelHeight = 1100;
        //public static MachineType mcType = MachineType.RP3400;
        public static DateTime sNullTime = new DateTime(1, 1, 1, 0, 0, 0);
        public static bool reGetAddrs = false;
        public static string uiVerNum = "Program__EPI__130703__14.20";
        public static WinMsg defaultMsg = new WinMsg(WinMsgType.mwNull, null);
        public static List<nullEvent> lstSUnitChange = new List<nullEvent>();
        public static execEvent execHandle;
        public static execMsgEvent execMsg;
        public static intEvent RefushAuthorization;
        public static List<nullEvent> lstLanRefresh = new List<nullEvent>();
        public static List<nullEvent> lstUsbCheckFunc = new List<nullEvent>();
        public static DateTime tmNull = new DateTime(2012, 9, 4, 0, 0, 0);
        public static DateTime tmUserLoad = tmNull;
        public static dataView dv = new dataView();        //要求紧跟database初始化
        public static eventMgrObj eventMgr = new eventMgrObj();//要求在link 之前初始化，中间涉及alarm 开机的检测
        public static callBackObjEvent callbackObjLstHandle;
        /// <summary>
        /// 初始化时执行
        /// </summary>
        public static List<nullEvent> lstStartUpInit = new List<nullEvent>();
        public static intEvent setPageToNrHandle;
        public static strEvent setCurPageDisHandle;
        public static userClass SCurUser;
        public static intEvent setDataAnalysisPageHeight;
        public static intEvent setOverViewPageTblHeight;
        public static intEvent setLineChartPageHeight;
        /// <summary>
        /// 刷新操作记录
        /// </summary>
        public static nullEvent refresh;
        public static nullEvent update;
        public static setPbarValue setPBar;
        public static DirectoryInfo sUsbPath;
        
        /// <summary>
        /// 全局numKeyCtrl 控件
        /// </summary>
        public static NumericTouchpad SNumInput;
        public static myMessageBox sMsgBox;
        public static numKeyCtrl SNumKeyPanel;
        public static charKeyCtrl SCharKeyPanel;
        public static UserCtrl SUserPanel;
        public static lockScreenCtrl SLockScreenPanel;
        public static sysExitCtrl SSysExitPanel;
        public static calcCtrl SCalcPanel;
        public static btnAttentionCtrl SAttentionPanel;
        public static linkPlcCtrl SLinkPlcPanel;
        public static IpSetCtrl SIpSetPanel;
        public static IpSerSetCtrl SIpSerSetPanel;
        public static loadConfFileCtrl SLoadConfFilePanel;
        public static saveConfFileCtrl SSaveConfFilePanel;
        public static fileCoveredAlarm SFileCoveredAlarmPanel;
        public static interpretorPage SIprCtrl;
        public static exportEventHistoryCtrl SExportEventHistoryPanel;
        public static lineChart SLineChart;
        public static exportIprFileCtrl SExportFilePanel;
        public static loadIprFileCtrl SLoadIprFilePanel;
        public static upgradeCtrl SUpgradePanel;
        public static moldOnOffNewPage SMoldOnOffNewPagePanel;
        public static mainPanelCtrl sMainPanelCtrl;
        public static userBtn SUserSetPanel;
        public static loadFromUsbCtrl SLoadFromUsbPanel;
        public static RegistCtrl sRegistCtrl;
        public static SetTimeCtrl sSetTimeCtrl;

        public static nullEvent BackstageClockTick;

        private void ctrlsObjInit()
        {
            sSetTimeCtrl = new SetTimeCtrl();
            cvsMain.Children.Add(sSetTimeCtrl);
            SNumInput = new NumericTouchpad();
            cvsMain.Children.Add(SNumInput);
            SNumKeyPanel = new numKeyCtrl();
            cvsMain.Children.Add(SNumKeyPanel);
            sMsgBox = new myMessageBox();
            cvsMain.Children.Add(sMsgBox);

            SCharKeyPanel = charKeyPanel;
            SUserPanel = userPanel;
            SLockScreenPanel = lockScreenPanel;
            SSysExitPanel = sysExitPanel;
            SCalcPanel = calcPanel;
            SLinkPlcPanel = linkPlcPanel;
            SIpSetPanel = ipSetPanel;
            sRegistCtrl = registCtrl;

            SIpSerSetPanel = ipSerSetPanel;
            SLoadConfFilePanel = loadConfFilePanel;
            SSaveConfFilePanel = saveConfFilePanel;
            SFileCoveredAlarmPanel = fileCoveredAlarmPanel;
            SIprCtrl = mainPanel.interpretorPage1;
            cvsMain.Children.Add(exportEventHistoryPanel);
            SExportEventHistoryPanel = exportEventHistoryPanel;
            SLoadFromUsbPanel = loadFromUsbCtrl1;

            gdMain.Children.Add(exportIprFilePanel);
            SExportFilePanel = exportIprFilePanel;
            gdMain.Children.Add(loadIprFilePanle);
            SLoadIprFilePanel = loadIprFilePanle;
            gdMain.Children.Add(upgradePanel);
            SUpgradePanel = upgradePanel;
        }
        public static bool flagStartUpOk = false;

        /// <summary>
        /// 后台计时器
        /// </summary>
        private DispatcherTimer BackstageTimer;
        /// <summary>
        /// 最后动作时间
        /// </summary>
        private DateTime lastEventTime;

        public static SPCData ds = new SPCData();
        
        public valmoWin()
        {
            try
            {
                DirectoryInfo dirUpgrade = new DirectoryInfo("upgrade");
                DirectoryInfo dirBackup = new DirectoryInfo("backup");
                DirectoryInfo dirIpr = new DirectoryInfo("ipr");
                DirectoryInfo dirJpeg = new DirectoryInfo("jpeg");
                DirectoryInfo dirRec = new DirectoryInfo("rec");
                DirectoryInfo dirLns = new DirectoryInfo("lines"); 

                if (!dirUpgrade.Exists)
                {
                    dirUpgrade.Create();
                }
                if (!dirBackup.Exists)
                {
                    dirBackup.Create();
                }
                if (!dirJpeg.Exists)
                {
                    dirJpeg.Create();
                }
                if (!dirIpr.Exists)
                {
                    dirIpr.Create();
                }
                if (!dirRec.Exists)
                {
                    dirRec.Create();
                }
                if (!dirLns.Exists)
                {
                    dirLns.Create();
                }

                dv.feedbackHandle = callbackObjLstFunc;
                callbackObjLstHandle = new callBackObjEvent(callbackObjLstFunc);
                execHandle = new execEvent(execInvoke);
                execMsg = new execMsgEvent(execMsgFunc);
                setPageToNrHandle = setPangetoNr;
                setCurPageDisHandle = setCurPageDis;
                SCurUser = dv.users.curUser;

                InitializeComponent();

                ctrlsObjInit();

                setUnitResources(objUnit.unitBase[UnitType.Len_mm]);
                setUnitResources(objUnit.unitBase[UnitType.Temp_C]);
                setUnitResources(objUnit.unitBase[UnitType.Prs_Mpa]);
                setUnitResources(objUnit.unitBase[UnitType.Force_ton]);

                valmoWin.dv.SysPr[5].addHandle(handle_sys005, plcLstSpd.lowSpdType);

                vm.getTm("-------- 1 ------------");
                if (dv.getLink())
                {
                    vm.getTm("-------- 2 ------------");
                    //开机时，检测报警灯是否是开着的状态。如果开着，需要将其关闭
                    if (valmoWin.dv.SysPr[187].valueNew == 1)
                        valmoWin.dv.SysPr[187].valueNew = 0;

                    //unitInit();
                    setUnitResources(objUnit.lenUnitBasic);
                    setUnitResources(objUnit.tempUnitBasic);
                    setUnitResources(objUnit.prsUnitBasic);
                    setUnitResources(objUnit.forceUnitBasic);

                    foreach (nullEvent func in lstStartUpInit)
                    {
                        func();
                    }
                    setPangetoNr(0);
                }
                else
                {
                    execFunc(opeOrderType.winMsg, new WinMsg(WinMsgType.mwLinkPlcError));
                }
                vm.getTm("-------- 3 ------------");
                checkUsbFunc();
                lanCheck();
                checkAccredit();
            }
            catch (Exception ex)
            {
                vm.perror(ex.ToString());
            }

            dv.SysPr[14].add();

            BackstageTimer = new DispatcherTimer();
            BackstageTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            BackstageTimer.Tick += new EventHandler(ClockTick);
            BackstageTimer.Start();

            lastEventTime = DateTime.Now;
            valmoWin.BackstageClockTick += ScreenSaverTimer;
        }

        int PLCJump = 0;
        private void handle_sys005(objUnit obj)
        {
            this.Dispatcher.Invoke(DispatcherPriority.Send,(Action)delegate()
            {
                valmoWin.dv.SysPr[5].valueNew = PLCJump++;
            });
        }

        public void getCpuTemp()
        {
            Computer thisComputer = new Computer() { CPUEnabled = true };
            thisComputer.Open();

            string temp = "";

            foreach (var hardwareItem in thisComputer.Hardware)
            {
                if (hardwareItem.HardwareType == HardwareType.CPU)
                {
                    hardwareItem.Update();

                    foreach (IHardware subHardware in hardwareItem.SubHardware)
                    {
                        subHardware.Update();
                    }

                    foreach (var sensor in hardwareItem.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Temperature)
                        {
                            temp += (string.Format("{0} Temperature = {1} \r\n", sensor.Name, sensor.Value.HasValue ? sensor.Value.Value.ToString() : "no value"));
                        }
                    }
                }
            }
        }

        private void checkAccredit()
        {
            DateTime datelimit;
            DateTime dateNow;

            valmoWin.dv.SysPr[227].setValue(2);
            uint addr = (uint)valmoWin.dv.SysPr[226].valueNew;


            if (addr != 0)
            {
                int[] date = new int[3];

                if (Lasal32.GetData(date, addr, 12) == true)
                {
                    try
                    {
                        datelimit = new DateTime(date[0], date[1], date[2]);
                    }
                    catch
                    {
                        date[0] = 2000;
                        date[1] = 1;
                        date[2] = 1;

                        datelimit = new DateTime(2000, 1, 1);

                        Lasal32.SetData(date, addr, 12);
                        valmoWin.dv.SysPr[227].setValue(3);

                        App.log.Info("读取使用截止日期出错，试用期截止日期重置为：2000_01_01");
                    }
                }
                else
                {
                    valmoWin.sRegistCtrl.show();
                    App.log.Error("GetData函数出错 \t 位置：BasicMessage \t 功能：读取授权时间");
                    return;
                }
            }
            else
            {
                return;
            }

            int systemDateField = dv.SysPr[13].value;
            int year = Convert.ToInt32((systemDateField >> 16) & 0x0fff);
            int month = Convert.ToInt32((systemDateField >> 12) & 0x0000f);
            int day = Convert.ToInt32((systemDateField >> 4) & 0x00000ff);

            try
            {
                dateNow = new DateTime(year, month, day);
            }
            catch
            {
                SysBasicInfo.remainingTrialDays = 0;
                valmoWin.sRegistCtrl.show();
                App.log.Error("读取PLC时间出错");
                return;
            }

            TimeSpan tSpan = datelimit - dateNow;
            SysBasicInfo.remainingTrialDays = tSpan.Days;

            valmoWin.RefushAuthorization(SysBasicInfo.remainingTrialDays);

            if (tSpan.Days < 6)
            {
                valmoWin.sRegistCtrl.show();
            }
        }

        private void ClockTick(object sender, EventArgs e)
        {
            valmoWin.BackstageClockTick();
        }

        [StructLayout(LayoutKind.Sequential)]
        struct LASTINPUTINFO
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cbSize;
            [MarshalAs(UnmanagedType.U4)]
            public uint dwTime;
        }

        [DllImport("user32.dll")]
        static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        static long GetLastInputTime()
        {
            LASTINPUTINFO vLastInputInfo = new LASTINPUTINFO();
            vLastInputInfo.cbSize = Marshal.SizeOf(vLastInputInfo);
            if (!GetLastInputInfo(ref vLastInputInfo)) return 0;
            return Environment.TickCount - (long)vLastInputInfo.dwTime;
        }

        /// <summary>
        /// 屏幕保护
        /// </summary>
        private void ScreenSaverTimer()
        {
            //时间 20×60 S
            if (GetLastInputTime() / 1000 > 1200)
            {
                valmoWin.execHandle(opeOrderType.lockSysPanelShow);
            }
        }

        /// <summary>
        /// 检查关机前保存的语言选项，并恢复
        /// </summary>
        private void lanCheck()
        {
            switch (Properties.Settings.Default.lan)
            {
                case 1:
                    Common.lan = lanType.lanCN;
                    break;
                case 2:
                    Common.lan = lanType.lanEN;
                    break;
            }
            execFunc(opeOrderType.lanChange);

            foreach (nullEvent func in lstLanRefresh)
            {
                func();
            }
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            HwndSource hwndSource = PresentationSource.FromVisual(this) as HwndSource;
            if (hwndSource != null)
                hwndSource.AddHook(new HwndSourceHook(this.WndProc));
        }
        protected virtual IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {

                case 0x0011: 
                    {
                        valmoWin.execMsg(new WinMsg(WinMsgType.mwMsgSave));
                    }
                    break;
                case 0x219:
                    {
                        switch (wParam.ToInt32())
                        {
                            case 0x8000:
                            case 0x8004:
                                {
                                    checkUsbFunc();
                                    handled = true;
                                    break;
                                }
                        }
                        break;
                    }
            }
            return IntPtr.Zero;
        }
        public static int UsbAcc = 0;
        DriveInfo targetDrive = null;
        private void checkUsbFunc()
        {
            sUsbPath = null;
            DriveInfo[] uin = DriveInfo.GetDrives();
            targetDrive = null;
            foreach (DriveInfo drive in uin)
            {
                if (drive.DriveType == DriveType.Removable)
                {
                    targetDrive = drive;
                    sUsbPath = new DirectoryInfo(drive.Name);
                    break;
                }
            }
            if (sUsbPath == null)
            {
                if (UsbAcc != 0)
                {
                    targetDrive = null;
                    UsbAcc = 0;
                    tmUserLoad = DateTime.Now + new TimeSpan(0,1,0) - Properties.Settings.Default.dtUserUnload;
                    //valmoWin.dv.users.unload();
                }
            }
            else
            {
                Thread tdUsb = new Thread(new ThreadStart(refreshUsb));
                tdUsb.Start();
            }
            foreach (nullEvent func in lstUsbCheckFunc)
            {
                func();
            }
        }
        private void refreshUsb()
        {
            FileStream fsUsb = null;
            StreamReader srUsb = null;
            try
            {
                while (!targetDrive.IsReady)
                {
                    Thread.Sleep(10);
                }

                if (UsbAcc == 0)
                {
                    if (File.Exists(valmoWin.sUsbPath + "\\Acc.vm"))
                    {
                        fsUsb = new FileStream(valmoWin.sUsbPath + "\\Acc.vm", FileMode.Open);
                        if (fsUsb != null)
                        {
                            srUsb = new StreamReader(fsUsb);
                            string str = srUsb.ReadLine();
                            if (str != null)
                            {
                                string strDecrypt = valmoWin.dv.DecryptDES(str, "Valmo076");
                                string[] strTmp = strDecrypt.Split('#');
                                List<string> lstSer = hardware.GetUsbSerial();
                                if (lstSer.Contains(strTmp[0]))
                                {
                                    userClass user = valmoWin.dv.users.getUserObjByName(strTmp[1]);
                                    if (user != null)
                                    {
                                        if (user.password == strTmp[2] && user.accessLevel.ToString() == strTmp[3])
                                            UsbAcc = user.accessLevel;

                                    }
                                }
                            }
                            srUsb.Close();
                            fsUsb.Close();
                        }
                    }
                }
                if (UsbAcc > 0)
                    valmoWin.dv.users.setCurUser(UsbAcc);
            }
            catch (System.Exception ex)
            {
                if (srUsb != null)
                    srUsb.Close();
                if (fsUsb != null)
                    fsUsb.Close();
                vm.perror(ex.Message);
            }
            //hardware.GetUsbSerial();
        }
        public void execInvoke(opeOrderType opeType, WinMsg msg = null)
        {
            this.Dispatcher.BeginInvoke(new execEvent(execFunc), new object[] { opeType, msg });
        }
        public void execFunc(opeOrderType opeType, WinMsg msg = null)
        {
            switch (opeType)
            {
                case opeOrderType.closeSysPanelShow:
                    #region 弹出系统关闭窗口
                    {
                        SSysExitPanel.show();
                        break;
                    }
                    #endregion
                case opeOrderType.lockSysPanelShow:
                    #region 弹出系统锁定窗口
                    {
                        if (valmoWin.dv.users.curUser.accessLevel > 2)
                        {
                            valmoWin.dv.users.unload();
                        }

                        SLockScreenPanel.show();
                        break;
                    }
                    #endregion
                case opeOrderType.userLoadPanelShow:
                    #region 显示用户登录界面
                    {
                        SUserPanel.show();
                        //ctrlType = 0;
                        break;
                    }
                    #endregion
                case opeOrderType.calcPanelShow:
                    #region 显示计算器界面
                    {
                        SCalcPanel.show();
                        break;
                    }
                    #endregion
                case opeOrderType.lanChange:
                    #region 显示节目语言切换
                    {
                        //SIprCtrl.refreshLan();
                        ////topPanelCtrl1.setCtrlsLanFunc();
                        foreach (nullEvent func in lstLanRefresh)
                        {
                            func();
                        }
                        break;
                    }
                    #endregion
                case opeOrderType.userSleep:
                    #region //用户进入sleep状态
                    {
                        //toolPanel.userLoad.state = nsVicoClient.ctrls.userLoadState.USERBEGINTOQUIT;
                        SUserSetPanel.state = nsVicoClient.ctrls.userLoadState.USERBEGINTOQUIT;
                        break;
                    }
                    #endregion
                case opeOrderType.userUnSleep:
                    #region //用户进入unsleep状态
                    {
                        //toolPanel.userLoad.state = nsVicoClient.ctrls.userLoadState.USERLOAD;
                        SUserSetPanel.state = nsVicoClient.ctrls.userLoadState.USERLOAD;
                        break;
                    }
                    #endregion
                case opeOrderType.winMsg:
                    #region //系统消息
                    {
                        execMsgFunc(msg);
                        break;
                    }
                    #endregion
                case opeOrderType.unitChange:
                    #region 单位切换
                    {
                        unitChangeToFunc(msg.pStr);
                        break;
                    }
                    #endregion
                case opeOrderType.clientLinkOk:
                    #region 客户端成功连接到服务器端
                    {
                        //topPanelCtrl1.helpStateOn = true;
                        //if (SHelpSerPanel != null)
                        //{

                        //    SHelpSerPanel.clientIpAddr = serObj.clientAddr;
                        //    SHelpSerPanel.clientLinkState = true;

                        //}
                    }
                    break;
                    #endregion
                case opeOrderType.clientLinkBreak:
                    #region 客户端断开连接
                    {
                        //topPanelCtrl1.helpStateOn = false;
                        //if(SHelpSerPanel != null)
                        //    SHelpSerPanel.clientLinkState = false;
                        //MessageBox.Show("client Quit!");
                    }
                    break;
                    #endregion
                case opeOrderType.ipLinkOk:
                    #region 外网连接成功
                    //topPanelCtrl1.ipLinkState = true;
                    break;
                    #endregion
                case opeOrderType.ipLinkBreak:
                    #region 外网连接断开
                    //topPanelCtrl1.ipLinkState = false;
                    break;
                    #endregion
                case opeOrderType.layoutType:
                    break;

            }
        }
        public void execMsgFunc(WinMsg msg)
        {
            switch (msg.type)
            {
                case WinMsgType.msJumpHeartStart:

                    break;
                case WinMsgType.mwLinkPlcOK:
                    {
                        topPanel.sendMsgToWinFunc(msg);
                    }
                    break;
                case WinMsgType.mwLinkPlcError:
                    {
                        SCharKeyPanel.hide();
                        SUserPanel.hide();
                        SCalcPanel.hide();
                        SLoadConfFilePanel.hide();
                        SSaveConfFilePanel.hide();
                        topPanel.sendMsgToWinFunc(msg);
                    }
                    break;
                case WinMsgType.mwAccessLower:
                    {
                        topPanel.sendMsgToWinFunc(msg);
                    }
                    break;
                case WinMsgType.mwUserNull:
                    {
                        //toolPanel.getWinMsgHandle(WinMsgType.mwUserNull, null);
                        topPanel.sendMsgToWinFunc(msg);
                    }
                    break;
                case WinMsgType.mwLogInOK:
                    {
                        //toolPanel.getWinMsgHandle(WinMsgType.mwLogInOK, null);
                        //toolPanel.setCurUser();
                        SUserSetPanel.setUserState();
                        topPanel.sendMsgToWinFunc(msg);
                        recUnit ergObj = new recUnit("LogIn", DateTime.Now, recType.logType);
                        valmoWin.eventMgr.msgSave(ergObj);
                        valmoWin.refresh();
                        //if (dv.users.curUser.accessLevel >= 3)
                        //    mainPanel.interpretorPage1.getAccessFunc();
                        //Program.ctrls.mainPanel.interpretor.interpretorPage.loadOkHandle();
                    }
                    break;
                case WinMsgType.mwNoDog:
                    {
                        MessageBox.Show("请插入超级狗！");
                    }
                    break;
                case WinMsgType.mwLogOut:
                    {
                        recUnit ergObj = new recUnit("LogOut", msg.pStr, DateTime.Now, recType.logType);
                        valmoWin.eventMgr.msgSave(ergObj);
                        //toolPanel.setCurUser();
                        SUserSetPanel.setUserState();
                        topPanel.sendMsgToWinFunc(msg);
                        valmoWin.refresh();
                        //mainPanel.quitToNewPage();
                        UserCtrl.LogOut();
                    }
                    break;
                case WinMsgType.mwPidClear:
                    {

                    }
                    break;
                case WinMsgType.mwMsgNull:
                    {

                    }
                    break;
                case WinMsgType.mwJumpHeartErr:
                    {
                        topPanel.sendMsgToWinFunc(msg);
                    }
                    break;
                case WinMsgType.mwJumpHeartRetart:
                    {
                        topPanel.sendMsgToWinFunc(msg);
                    }
                    break;
                case WinMsgType.mwMsg:
                    {
                        mainPanel.overViewPage1.eventRecord.refreshEvent();
                    }
                    break;
                case WinMsgType.mwRelink:
                    linkPlcPanel.active = true;
                    topPanel.sendMsgToWinFunc(msg);
                    break;
                #region //保存信息
                case WinMsgType.mwMsgSave:
                    {
                        eventMgr.saveToFile();
                        //valmoWin.ds.SPCSave();
                    }
                    break;
                #endregion
            }
        }
        private void unitInit()
        {
            setUnitResources(objUnit.lenUnitBasic);
            setUnitResources(objUnit.tempUnitBasic);
            setUnitResources(objUnit.prsUnitBasic);
            setUnitResources(objUnit.forceUnitBasic);
        }
        public void setUnitResources(UnitType type)
        {
            setUnitResources(objUnit.unitBase[type]);
        }
        public void setUnitResources(string newUnitName)
        {
            switch (newUnitName)
            {
                case objUnit.unit_mm:
                    this.Resources["lenUnitKey"] = "[" + newUnitName + "]";
                    this.Resources["speedUnitKey"] = "[" + newUnitName + "/s]";
                    this.Resources["accelerationUnitKey"] = "[" + newUnitName + "/s²]";

                    this.Resources["lenUnitNullKey"] = newUnitName;
                    this.Resources["speedUnitNullKey"] = newUnitName + "/s";
                    this.Resources["accelerationUnitNullKey"] = newUnitName + "/s²";
                    break;
                case objUnit.unit_inch:
                    this.Resources["lenUnitKey"] = "[" + newUnitName + "]";
                    this.Resources["speedUnitKey"] = "[" + newUnitName + "/s]";
                    this.Resources["accelerationUnitKey"] = "[" + newUnitName + "/s²]";

                    this.Resources["lenUnitNullKey"] = newUnitName;
                    this.Resources["speedUnitNullKey"] = newUnitName + "/s";
                    this.Resources["accelerationUnitNullKey"] = newUnitName + "/s²";
                    break;
                case objUnit.unit_C:
                    this.Resources["tempUnitKey"] = "[" + newUnitName + "]";
                    this.Resources["tempUnitNullKey"] = newUnitName;
                    break;
                case objUnit.unit_F:
                    this.Resources["tempUnitKey"] = "[" + newUnitName + "]";
                    this.Resources["tempUnitNullKey"] = newUnitName;
                    break;
                case objUnit.unit_ton:
                    this.Resources["forceUnitKey"] = "[" + newUnitName + "]";
                    this.Resources["forceUnitNullKey"] = newUnitName;
                    break;
                case objUnit.unit_KN:
                    this.Resources["forceUnitKey"] = "[" + newUnitName + "]";
                    this.Resources["forceUnitNullKey"] = newUnitName;
                    break;
                case objUnit.unit_Us_ton:
                    this.Resources["forceUnitKey"] = "[" + newUnitName + "]";
                    this.Resources["forceUnitNullKey"] = newUnitName;
                    break;
                case objUnit.unit_Mpa:
                    this.Resources["pressUnitKey"] = "[" + newUnitName + "]";
                    this.Resources["pressUnitNullKey"] = newUnitName;
                    break;
                case objUnit.unit_Bar:
                    this.Resources["pressUnitKey"] = "[" + newUnitName + "]";
                    this.Resources["pressUnitNullKey"] = newUnitName;
                    break;
                case objUnit.unit_PSI:
                    this.Resources["pressUnitKey"] = "[" + newUnitName + "]";
                    this.Resources["pressUnitNullKey"] = newUnitName;
                    break;
            }
        }
        void unitChangeToFunc(string newUnitName)
        {
            objUnit.setUnitTo(newUnitName);
            UnitType curUnitType = UnitType.DgtType;
            switch (newUnitName)
            {
                case objUnit.unit_mm:
                    this.Resources["lenUnitKey"] = "[" + newUnitName + "]";
                    this.Resources["speedUnitKey"] = "[" + newUnitName + "/s]";
                    this.Resources["accelerationUnitKey"] = "[" + newUnitName + "/s²]";

                    this.Resources["lenUnitNullKey"] = newUnitName;
                    this.Resources["speedUnitNullKey"] = newUnitName + "/s";
                    this.Resources["accelerationUnitNullKey"] = newUnitName + "/s²";
                    curUnitType = UnitType.Len_mm;
                    break;
                case objUnit.unit_inch:
                    this.Resources["lenUnitKey"] = "[" + newUnitName + "]";
                    this.Resources["speedUnitKey"] = "[" + newUnitName + "/s]";
                    this.Resources["accelerationUnitKey"] = "[" + newUnitName + "/s²]";

                    this.Resources["lenUnitNullKey"] = newUnitName;
                    this.Resources["speedUnitNullKey"] = newUnitName + "/s";
                    this.Resources["accelerationUnitNullKey"] = newUnitName + "/s²";
                    curUnitType = UnitType.Len_inch;
                    break;
                case objUnit.unit_C:
                    this.Resources["tempUnitKey"] = "[" + newUnitName + "]";
                    this.Resources["tempUnitNullKey"] = newUnitName;
                    curUnitType = UnitType.Temp_C;
                    break;
                case objUnit.unit_F:
                    this.Resources["tempUnitKey"] = "[" + newUnitName + "]";
                    this.Resources["tempUnitNullKey"] = newUnitName;
                    curUnitType = UnitType.Temp_F;
                    break;
                case objUnit.unit_ton:
                    this.Resources["forceUnitKey"] = "[" + newUnitName + "]";
                    this.Resources["forceUnitNullKey"] = newUnitName;
                    curUnitType = UnitType.Force_ton;
                    break;
                case objUnit.unit_KN:
                    this.Resources["forceUnitKey"] = "[" + newUnitName + "]";
                    this.Resources["forceUnitNullKey"] = newUnitName;
                    curUnitType = UnitType.Force_KN;
                    break;
                case objUnit.unit_Us_ton:
                    this.Resources["forceUnitKey"] = "[" + newUnitName + "]";
                    this.Resources["forceUnitNullKey"] = newUnitName;
                    curUnitType = UnitType.Force_USton;
                    break;
                case objUnit.unit_Mpa:
                    this.Resources["pressUnitKey"] = "[" + newUnitName + "]";
                    this.Resources["pressUnitNullKey"] = newUnitName;
                    curUnitType = UnitType.Prs_Mpa;
                    break;
                case objUnit.unit_Bar:
                    this.Resources["pressUnitKey"] = "[" + newUnitName + "]";
                    this.Resources["pressUnitNullKey"] = newUnitName;
                    curUnitType = UnitType.Prs_Bar;
                    break;
                case objUnit.unit_PSI:
                    this.Resources["pressUnitKey"] = "[" + newUnitName + "]";
                    this.Resources["pressUnitNullKey"] = newUnitName;
                    curUnitType = UnitType.Prs_PSI;
                    break;
            }

            if (curUnitType == UnitType.Len_mm || curUnitType == UnitType.Len_inch)
            {
                for (int i = 0; i < valmoWin.dv.length; i++)
                {
                    objUnit curObj = valmoWin.dv[i];
                    if (curObj.unitType == UnitType.Len_inch || curObj.unitType == UnitType.LenInj_inch || curObj.unitType == UnitType.Spd_inch || curObj.unitType == UnitType.SpdInj_inch || curObj.unitType == UnitType.AccInj_inch || curObj.unitType == UnitType.Acc_inch
                        || curObj.unitType == UnitType.Len_mm || curObj.unitType == UnitType.LenInj_mm || curObj.unitType == UnitType.Spd_mm || curObj.unitType == UnitType.SpdInj_mm || curObj.unitType == UnitType.AccInj_mm || curObj.unitType == UnitType.Acc_mm)
                    {
                        curObj.refresh();
                    }
                }
            }
            else if (curUnitType == UnitType.Temp_C || curUnitType == UnitType.Temp_F)
            {
                for (int i = 0; i < valmoWin.dv.length; i++)
                {
                    objUnit curObj = valmoWin.dv[i];
                    if (curObj.unitType == UnitType.Temp_F || curObj.unitType == UnitType.Temp_C)
                    {
                        curObj.refresh();
                    }
                }
            }
            else if (curUnitType == UnitType.Force_ton || curUnitType == UnitType.Force_KN || curUnitType == UnitType.Force_USton)
            {
                for (int i = 0; i < valmoWin.dv.length; i++)
                {
                    objUnit curObj = valmoWin.dv[i];
                    if (curObj.unitType == UnitType.Force_KN || curObj.unitType == UnitType.Force_ton || curObj.unitType == UnitType.Force_USton)
                    {
                        curObj.refresh();
                    }
                }
            }
            else if (curUnitType == UnitType.Prs_Mpa || curUnitType == UnitType.Prs_Bar || curUnitType == UnitType.Prs_PSI)
            {
                for (int i = 0; i < valmoWin.dv.length; i++)
                {
                    objUnit curObj = valmoWin.dv[i];
                    if (curObj.unitType == UnitType.Prs_Mpa || curObj.unitType == UnitType.Prs_Bar || curObj.unitType == UnitType.Prs_PSI)
                    {
                        curObj.refresh();
                    }
                }
                //////mainPanel.dataAnalysis1.productMessageList1.unitRefresh();
            }
            //btnSetCtrl.sUnitRefresh();
            //////mainPanel.dataAnalysis1.lineChart1.unitRefresh();
            //foreach (nullEvent func in lstSUnitChange)
            //{
            //    func();
            //}
        }
        public static void addUnitChangeHandle(nullEvent handle)
        {
            lstSUnitChange.Add(handle);
        }

        private void callbackObjLstFunc(objUnit obj)
        {
            try
            {
                foreach (Label lb in obj.lbGrp)
                {
                    //lb.Content = obj.vDblStr;
                    this.Dispatcher.BeginInvoke(new nullEvent(() =>
                    {
                        lb.Content = obj.vDblStr;
                    }));
                }
                foreach (callBackObjEvent handle in obj.handleGrp)
                {
                    this.Dispatcher.BeginInvoke(new callBackObjEvent(handle), new object[] { obj });
                    //handle(obj);
                }
            }
            catch (Exception ex)
            {
                vm.printLn("[valmoWin.callbackObjLstFunc] " + ex.ToString());
            }
        }
        public static void addLanRefreshHandle(nullEvent handle)
        {
            lstLanRefresh.Add(handle);
        }
        public static void setPangetoNr(int nr)
        {
            dv.SysPr[11].valueNew = nr;
        }
        private void setCurPageDis(string msg)
        {
            //topPanel.setCurPageDis(msg);
        }

        private const int WM_SYSCOMMAND = 0x112;    //系统消息
        private const int SC_SCREENSAVE = 0xf140;   // 启动屏幕保护消息
        private const int SC_MONITORPOWER = 0xF170; //关闭显示器的系统命令
        private const int POWER_OFF = 2;            //2 为关闭, 1为省电状态，-1为开机
        private static readonly IntPtr HWND_BROADCAST = new IntPtr(0xffff); //广播消息，所有顶级窗体都会接收

        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                cvsMain.Margin = new Thickness(cvsMain.Margin.Left, cvsMain.Margin.Top - 100, cvsMain.Margin.Right, cvsMain.Margin.Bottom);
            }
            else if (e.Key == Key.Up)
            {
                cvsMain.Margin = new Thickness(cvsMain.Margin.Left, cvsMain.Margin.Top + 100, cvsMain.Margin.Right, cvsMain.Margin.Bottom);
            }
            else if (e.Key == Key.Escape)
            {
                SSysExitPanel.show();
            }


            else if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && e.Key == Key.F6)
            {
                valmoWin.dv.users.setCurUser(643);
            }
            else if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && e.Key == Key.F3)
            {
                valmoWin.dv.users.setCurUser(3);
            }
            else if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && e.Key == Key.F4)
            {
                valmoWin.dv.users.setCurUser(4);
            }
            else if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && e.Key == Key.F5)
            {
                valmoWin.dv.users.setCurUser(5);
            }
            else if ((Keyboard.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt && e.Key == Key.F2)
            {
                valmoWin.dv.users.setCurUser(2);
            }
            else if ((Keyboard.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt && e.Key == Key.F1)
            {
                valmoWin.dv.users.setCurUser(1);
            }
        }

        private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                cvsMain.Margin = new Thickness(cvsMain.Margin.Left, cvsMain.Margin.Top + 100, cvsMain.Margin.Right, cvsMain.Margin.Bottom);
            }
            else
            {
                cvsMain.Margin = new Thickness(cvsMain.Margin.Left, cvsMain.Margin.Top - 100, cvsMain.Margin.Right, cvsMain.Margin.Bottom);
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tmUserLoad = DateTime.Now;
        }

        private void Program_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lastEventTime = DateTime.Now;
        }

    }

    public enum opeOrderType : byte
    {
        closeSysPanelShow,   //弹出系统关闭窗口
        lockSysPanelShow,    //弹出系统锁定窗口
        userLoadPanelShow,   //弹出用户登录窗口
        calcPanelShow,       //显示计算器窗口
        lanChange,           //窗体显示界面语言切换

        userSleep,           //用户进入sleep状态
        userUnSleep,         //用户进入sleep状态苏醒
        winMsg,              //系统消息
        msgSave,             //保存信息
        unitChange,          //单位切换

        clientLinkOk,        //客户端成功连接到服务器端，可以开始接收数据
        clientLinkBreak,     //客户端断开连接

        ipLinkOk,            //外网连接成功
        ipLinkBreak,         //外网连接断开
        layoutType
    }

    public class WinMsg
    {
        public WinMsgType type;
        public string pStr;
        public int pInt1;
        public int pInt2;
        public int pInt3;
        public bool flag;
        public WinMsg()
        {
            type = WinMsgType.mwNull;
            pStr = null;
            pInt1 = 0;
            pInt2 = 0;
            pInt3 = 0;
            flag = false;
        }
        public WinMsg(WinMsgType type)
        {
            this.type = type;
            this.pStr = null;
            pInt1 = 0;
            pInt2 = 0;
            pInt3 = 0;
            flag = false;
        }
        public WinMsg(WinMsgType type, string str)
        {
            this.type = type;
            this.pStr = str;
            pInt1 = 0;
            pInt2 = 0;
            pInt3 = 0;
            flag = false;
        }

    }

    public enum WinMsgType : int
    {
        mwNull,             //空值
        mwNoDog,
        //SysMsg
        msMainCtrlInitOK,    //主窗体初始化完成
        msLinkPlcState,     //执行连接plc 后的状态
        msJumpHeartStart,   //启动心跳
        msRefreshInteretorFile,//更新教导文件
        msPlcInitOk,        //plc初始化完成
        //WinMsg
        mwMsgNull,           //消息及警报清除状态
        mwUserNull,          //当前登录用户为空，用户未登录
        mwLogInOK,           //用户登录成功
        mwLogOut,            //用户退出登录
        mwLinkPlcError,      //与plc 连接失败
        mwLinkPlcOK,         //与plc 连接成功
        mwRelink,          //与plc 重新连接
        //mwAlarm,             //警报
        //mwAlarmClear,        //警报清除
        mwPidClear,          //pid清除
        mwShowCurAlarmList,  //
        mwJumpHeartErr,       //心跳出错
        mwJumpHeartRetart, //心跳重新连接
        mwMsgSys,              //系统消息
        //SYSMSGCLEAN,         //系统消息被清除
        mwMsgAlm,              //警报消息
        mwMsg,
        mwAccessLower,          //权限过低
        mwUnitChange,            //单位切换
        mwMsgSave
    }

}
