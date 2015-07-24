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
using System.Windows.Threading;
using nsDataMgr;

namespace nsVicoClient.ctrls
{
    public delegate void showSelectedPanelHandle();
    /// <summary>
    /// Interaction logic for lstAlmCtrl.xaml
    /// </summary>
    public partial class lstWinMsgCtrl : UserControl
    {
        private long dtStart = 0;
        DispatcherTimer dtMsg = new DispatcherTimer();
        public static List<recUnit> curActiveAlmObjLst = new List<recUnit>();
        string lastUserName = "";
        public lstWinMsgCtrl()
        {
            InitializeComponent();
            dtMsg.Interval = new TimeSpan(1000);
            dtMsg.Tick += new EventHandler(ctrlMsgFunc);

            for (int i = 0; i < valmoWin.dv.AlmPr.length; i++)
            {
                if (valmoWin.dv.AlmPr[i] != null)
                {
                    valmoWin.dv.AlmPr[i].addLstHandle(refreshAlmLst,plcLstSpd.mapType);
                }
            }
        }

        public void refreshAlmLst(objUnit obj)
        {
            for (int i = 0; i < 32; i++)
            {
                if (((obj.value >> i) & 0x01) == 1)
                {
                    bool bIsExist = false;

                    for (int j = 0; j < curActiveAlmObjLst.Count; j++)
                    {
                        if (curActiveAlmObjLst[j].serialNum == obj.bitSers[i])
                        {
                            bIsExist = true;
                            break;
                        }
                    }

                    if (bIsExist == false)
                    {
                        recUnit ergObj = null;

                        if (obj.objType == objectType.AlmPr)
                        {
                            ergObj = new recUnit(obj.bitSers[i], DateTime.Now, recType.alarmType);
                            curActiveAlmObjLst.Insert(0, ergObj);
                        }
                        else if (obj.objType == objectType.AlmSys)
                        {
                            ergObj = new recUnit(obj.bitSers[i], DateTime.Now, recType.sysType);
                            curActiveAlmObjLst.Insert(0, ergObj);

                            WinMsg msgSys = new WinMsg();
                            msgSys.type = WinMsgType.mwMsgSys;
                            sendMsgToWinFunc(msgSys);
                        }

                        if (ergObj != null)
                        {
                            valmoWin.eventMgr.msgSave(ergObj);
                        }

                        valmoWin.execHandle(opeOrderType.winMsg, new WinMsg(WinMsgType.mwMsg));

                        refreshAlarmList();
                    }
                }
                else
                {
                    for (int j = curActiveAlmObjLst.Count - 1; j >= 0; j--)
                    {
                        if (curActiveAlmObjLst[j].serialNum == obj.bitSers[i])
                        {
                            curActiveAlmObjLst.RemoveAt(j);

                            valmoWin.execHandle(opeOrderType.winMsg, new WinMsg(WinMsgType.mwMsg));
                            refreshAlarmList();
                        }
                    }
                }
            }

        }

        private void ctrlMsgFunc(object sender, EventArgs e)
        {
            this.Dispatcher.BeginInvoke(new showSelectedPanelHandle(ctrlMsg), null);
        }
        private void ctrlMsg()
        {
            if (DateTime.Now.Ticks - dtStart > 30000000)
            {
                dtMsg.Stop();

                if (curActiveAlmObjLst.Count > 0)
                {
                    refreshAlarmList();
                }
                else
                {
                    tbMenu.SelectedItem = Menu_Null;
                    valmoWin.sMainPanelCtrl.cvsMain.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xF4, 0xF4, 0xF4));
                }
            }
        }

        public void sendMsgToWinFunc(WinMsg msg)
        {
            if (!valmoWin.flagStartUpOk)
                return;
            switch (msg.type)
            {
                case WinMsgType.mwLinkPlcOK:
                    {
                        tbMenu.SelectedItem = Menu_Msg;
                        valmoWin.sMainPanelCtrl.cvsMain.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x78, 0xDD, 0xFF));
                        lbMsg.Content = "linkPlcOK !";
                        dtStart = DateTime.Now.Ticks;
                        if (dtMsg.IsEnabled == false && valmoWin.flagStartUpOk)
                            dtMsg.Start();
                    }
                    break;
                case WinMsgType.mwLinkPlcError:
                    {
                        tbMenu.SelectedItem = Menu_Msg;
                        valmoWin.sMainPanelCtrl.cvsMain.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x78, 0xDD, 0xFF));
                        lbMsg.Content = "linkPlcError !";
                    }
                    break;
                case WinMsgType.mwLogInOK:
                    {

                        lbMsg.Content = valmoWin.dv.getCurDis("lanKey035") + valmoWin.dv.users.curUser.name + valmoWin.dv.getCurDis("lanKey1104");
                        lastUserName = valmoWin.dv.users.curUser.name;
                        tbMenu.SelectedItem = Menu_Msg;
                        valmoWin.sMainPanelCtrl.cvsMain.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x78, 0xDD, 0xFF));
                        dtStart = DateTime.Now.Ticks;
                        if (dtMsg.IsEnabled == false && valmoWin.flagStartUpOk)
                            dtMsg.Start();
                    }
                    break;
                case WinMsgType.mwLogOut:
                    {

                        lbMsg.Content = valmoWin.dv.getCurDis("lanKey035") + msg.pStr + valmoWin.dv.getCurDis("lanKey1103");
                        tbMenu.SelectedItem = Menu_Msg;
                        valmoWin.sMainPanelCtrl.cvsMain.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x78, 0xDD, 0xFF));
                        dtStart = DateTime.Now.Ticks;
                        if (dtMsg.IsEnabled == false && valmoWin.flagStartUpOk)
                            dtMsg.Start();
                    }
                    break;
                case WinMsgType.mwAccessLower:
                    {
                        lbMsg.Content = valmoWin.dv.getCurDis("lanKey1102");
                        tbMenu.SelectedItem = Menu_Msg;
                        valmoWin.sMainPanelCtrl.cvsMain.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x78, 0xDD, 0xFF));
                        
                        dtStart = DateTime.Now.Ticks;
                        if (dtMsg.IsEnabled == false && valmoWin.flagStartUpOk)
                            dtMsg.Start();
                    }
                    break;
                case WinMsgType.mwUserNull:
                    {
                        lbMsg.Content = valmoWin.dv.getCurDis("lanKey1100");
                        tbMenu.SelectedItem = Menu_Msg;
                        valmoWin.sMainPanelCtrl.cvsMain.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x78, 0xDD, 0xFF));
                        dtStart = DateTime.Now.Ticks;
                        if (dtMsg.IsEnabled == false && valmoWin.flagStartUpOk)
                            dtMsg.Start();
                    }
                    break;
                case WinMsgType.mwJumpHeartErr:
                    {
                        lbMsg.Content = valmoWin.dv.getCurDis("lanKey1101");
                        tbMenu.SelectedItem = Menu_Msg;
                        valmoWin.sMainPanelCtrl.cvsMain.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x78, 0xDD, 0xFF));
                    }
                    break;
                case WinMsgType.mwMsgSys:
                    {
                        dtStart = DateTime.Now.Ticks;
                        if (dtMsg.IsEnabled == false && valmoWin.flagStartUpOk)
                            dtMsg.Start();
                    }
                    break;
                case WinMsgType.mwMsgAlm:
                    {
                        dtMsg.Stop();
                    }
                    break;
                case WinMsgType.mwRelink:
                    {
                        curActiveAlmObjLst.Clear();
                        tbMenu.SelectedItem = Menu_Null;
                        valmoWin.sMainPanelCtrl.cvsMain.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xF4, 0xF4, 0xF4));
                    }
                    break;
            }
        }

        public void refreshAlarmList()
        {
            if (curActiveAlmObjLst.Count > 0)
            {
                tbMenu.SelectedItem = Menu_Alm;
                bool getAlmValue = false;
                for (int i = 0; i < curActiveAlmObjLst.Count; i++)
                {
                    if (curActiveAlmObjLst[i].type == recType.alarmType)
                    {
                        if (!dtMsg.IsEnabled)
                        {
                            curAlmPanel.setValue(curActiveAlmObjLst[i], curActiveAlmObjLst.Count);
                            getAlmValue = true;
                        }
                        valmoWin.dv.SysPr[187].valueNew = 1;
                        break;
                    }
                }

                if (!getAlmValue)
                {
                    curAlmPanel.setValue(curActiveAlmObjLst[0], curActiveAlmObjLst.Count);
                    valmoWin.dv.SysPr[187].valueNew = 0;
                }

                if (cvsListAlarm.Visibility == Visibility.Visible)
                {
                    cvsListAlarm.Children.Clear();
                    cvsListAlarm.Height = curActiveAlmObjLst.Count * 35;
                    for (int i = 0; i < curActiveAlmObjLst.Count; i++)
                    {
                        alarmUnitCtrl almPanel = new alarmUnitCtrl(curActiveAlmObjLst[i], i + 1);
                        cvsListAlarm.Children.Add(almPanel);
                    }
                }
            }
            else
            {
                valmoWin.dv.SysPr[187].valueNew = 0;
                tbMenu.SelectedItem = Menu_Null;
                valmoWin.sMainPanelCtrl.cvsMain.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xF4, 0xF4, 0xF4));
                cvsListAlarm.Children.Clear();
                cvsListAlarm.Height = 0;
            }
        }

        private void refreshAlarmListByLan()
        {
            if (curActiveAlmObjLst.Count > 0)
            {
                tbMenu.SelectedItem = Menu_Alm;
                bool getAlmValue = false;
                for (int i = 0; i < curActiveAlmObjLst.Count; i++)
                {
                    if (curActiveAlmObjLst[i].type == recType.alarmType)
                    {
                        if (!dtMsg.IsEnabled)
                        {
                            curAlmPanel.setValueByLan(curActiveAlmObjLst[i], curActiveAlmObjLst.Count);
                            getAlmValue = true;
                        }
                        valmoWin.dv.SysPr[187].valueNew = 1;
                        break;
                    }
                }

                if (!getAlmValue)
                {
                    curAlmPanel.setValueByLan(curActiveAlmObjLst[0], curActiveAlmObjLst.Count);
                    valmoWin.dv.SysPr[187].valueNew = 0;
                }

                if (cvsListAlarm.Visibility == Visibility.Visible)
                {
                    cvsListAlarm.Children.Clear();
                    cvsListAlarm.Height = curActiveAlmObjLst.Count * 34;
                    for (int i = 0; i < curActiveAlmObjLst.Count; i++)
                    {
                        alarmUnitCtrl almPanel = new alarmUnitCtrl(curActiveAlmObjLst[i], i + 1);
                        cvsListAlarm.Children.Add(almPanel);
                        Canvas.SetTop(almPanel, 35 * i);
                    }
                }
            }
            else
            {
                valmoWin.dv.SysPr[187].valueNew = 0;
                tbMenu.SelectedItem = Menu_Null;
                valmoWin.sMainPanelCtrl.cvsMain.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xF4, 0xF4, 0xF4));
                cvsListAlarm.Children.Clear();
                cvsListAlarm.Height = 0;
            }
        }

        private void cvsMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (cvsListAlarm.Visibility == Visibility.Hidden)
            {
                cvsListAlarm.Visibility = Visibility.Visible;
            }
            else
            {
                cvsListAlarm.Visibility = Visibility.Hidden;
            }
            refreshAlarmList();
        }
    }

    public class AlmUnitPanel
    {
        public recUnit erObj;
        public Canvas cvs = new Canvas();
        public int no;
        //public string strTest = "";
        public Label lbTest = new Label();

        Label lbSerialNum;
        Label lbDis;
        Label lbDtStart;
        Label lbNo;
        //Image imgBK;
        public AlmUnitPanel()
        {
            lbSerialNum = new Label();
            lbSerialNum.Width = 80;
            lbSerialNum.Height = 34;
            lbSerialNum.FontSize = 18;
            lbSerialNum.VerticalContentAlignment = VerticalAlignment.Center;
            lbSerialNum.HorizontalContentAlignment = HorizontalAlignment.Center;

            lbDis = new Label();
            lbDis.Width = 707;
            lbDis.Height = 34;
            lbDis.FontSize = 18;
            lbDis.VerticalContentAlignment = VerticalAlignment.Center;
            lbDis.HorizontalContentAlignment = HorizontalAlignment.Left;

            lbDtStart = new Label();
            lbDtStart.Width = 300;
            lbDtStart.Height = 34;
            lbDtStart.FontSize = 18;
            lbDtStart.VerticalContentAlignment = VerticalAlignment.Center;
            lbDtStart.HorizontalContentAlignment = HorizontalAlignment.Center;

            lbNo = new Label();
            lbNo.Width = 55;
            lbNo.Height = 34;
            lbNo.FontSize = 18;
            lbNo.HorizontalContentAlignment = HorizontalAlignment.Right;
            lbNo.VerticalContentAlignment = VerticalAlignment.Center;

            cvs.Children.Add(lbSerialNum);
            Canvas.SetLeft(lbSerialNum, 5);
            cvs.Children.Add(lbDis);
            Canvas.SetLeft(lbDis, 385);
            cvs.Children.Add(lbDtStart);
            Canvas.SetLeft(lbDtStart, 85);
            cvs.Children.Add(lbNo);
            Canvas.SetLeft(lbNo, 992);

            cvs.Height = 34;
            cvs.Width = 1053;

            //imgBK = new Image();
            //imgBK.Source = 

            //if (ErObj != null)
            //{
            //    lbSerialNum.Content = ErObj.serialNum;
            //    lbDis.Content = ErObj.discription;
            //    lbDtStart.Content = ErObj.dtStart.ToString("yyyy/MM/dd HH:mm:ss");
            //    lbNo.Content = no;
            //}
            //erObj = ErObj;

        }


        public AlmUnitPanel(recUnit ErObj, int no)
        {

            cvs = new Canvas();

            lbSerialNum = new Label();
            lbSerialNum.Width = 80;
            lbSerialNum.Height = 34;
            lbSerialNum.FontSize = 18;
            lbSerialNum.VerticalContentAlignment = VerticalAlignment.Center;
            lbSerialNum.HorizontalContentAlignment = HorizontalAlignment.Center;

            lbDis = new Label();
            lbDis.Width = 707;
            lbDis.Height = 34;
            lbDis.FontSize = 18;
            lbDis.VerticalContentAlignment = VerticalAlignment.Center;
            lbDis.HorizontalContentAlignment = HorizontalAlignment.Left;

            lbDtStart = new Label();
            lbDtStart.Width = 300;
            lbDtStart.Height = 34;
            lbDtStart.FontSize = 18;
            lbDtStart.VerticalContentAlignment = VerticalAlignment.Center;
            lbDtStart.HorizontalContentAlignment = HorizontalAlignment.Center;

            lbNo = new Label();
            lbNo.Width = 55;
            lbNo.Height = 34;
            lbNo.FontSize = 18;
            lbNo.HorizontalContentAlignment = HorizontalAlignment.Right;
            lbNo.VerticalContentAlignment = VerticalAlignment.Center;

            cvs.Children.Add(lbSerialNum);
            Canvas.SetLeft(lbSerialNum, 5);
            cvs.Children.Add(lbDis);
            Canvas.SetLeft(lbDis, 385);
            cvs.Children.Add(lbDtStart);
            Canvas.SetLeft(lbDtStart, 85);
            cvs.Children.Add(lbNo);
            Canvas.SetLeft(lbNo, 992);

            cvs.Height = 34;
            cvs.Width = 1053;

            //imgBK = new Image();
            //imgBK.Source = 

            if (ErObj != null)
            {
                lbSerialNum.Content = ErObj.serialNum;
                lbDis.Content = ErObj.discription;
                lbDtStart.Content = ErObj.dtStart.ToString("yyyy.MM.dd hh:mm:ss");
                lbNo.Content = no;
            }
            erObj = ErObj;

        }

        public void setValue(recUnit ErObj, int no)
        {
            if (ErObj != null)
            {
                lbSerialNum.Content = ErObj.serialNum;
                lbDis.Content = ErObj.discription;
                lbDtStart.Content = ErObj.dtStart.ToString("yyyy.MM.dd hh:mm:ss");
                lbNo.Content = no;
            }
            erObj = ErObj;
        }

        private string getString(string str, int width)
        {
            string strRet = null;
            strRet = "  " + str;
            if (str != null)
            {
                for (int i = 0; i < width / 25 - 2 - str.Length; i++)
                    strRet += " ";
            }
            return strRet;
        }
    }
}
