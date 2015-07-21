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
using System.Threading;
using System.Windows.Threading;
using System.IO;
using nsDataMgr;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Drawing;

namespace nsVicoClient.ctrls
{
    public partial class topPanelCtrl : UserControl
    {
        private dateCtrl dtCtrl = new dateCtrl();
        private lanSelecCtrl lanSelectPanel = new lanSelecCtrl();
        private linkPlcCtrl linkPanel
        {
            get
            {
                return valmoWin.SLinkPlcPanel;
            }
        }

		private bool _PLCLinkState = false;
		public bool PLCLinkState {
			get {
				return _PLCLinkState;
			}
			set {
				_PLCLinkState = value;
				ellipsePLCLinkState.Opacity = _PLCLinkState == true ? 0 : 1;
			}
		}
        public bool VideoState
        {
            set
            {
                if (value == true)
                {
                    VideoStateLight.Visibility = Visibility.Hidden;
                }
                else
                {
                    VideoStateLight.Visibility = Visibility.Visible;
                }
            }
        }

        public topPanelCtrl()
        {
            try
            {
                InitializeComponent();

                cvsMain.Children.Add(dtCtrl);
                cvsMain.Children.Add(lanSelectPanel);

                valmoWin.lstLanRefresh.Add(setCtrlsLanFunc);
                valmoWin.BackstageClockTick += SystemClock;
                valmoWin.RefushAuthorization += this.RefushAuthorizationTime;

                if (VideoSource.getInstance().bInitState == true)
                {
                    VideoState = true;

                    VideoSource.getInstance().NewFarmeEnvent = captureAForge_NewFrame;
                }
                else
                {
                    VideoState = false;
                }

            }
            catch (Exception ex)
            {
                vm.perror(ex.ToString());
            }
        }

        public void RefushAuthorizationTime(int days)
        {
            if (days < 6)
            {
                this.cvsAuthorization.Visibility = Visibility.Visible;

                lbAuthorizationDays.Content = days;
            }
            else
            {
                this.cvsAuthorization.Visibility = Visibility.Hidden;
            }
        }

        void captureAForge_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            vBox.BackgroundImage = (Bitmap)eventArgs.Frame.Clone();
            vBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        }

        public void setCtrlsLanFunc()
        {
            switch (Common.lan)
            {
                case lanType.lanCN:
                    tbLan.SelectedIndex = 0;
                    break;
                case lanType.lanEN:
                    tbLan.SelectedIndex = 1;

                    break;
            }

            lstWinMsgCtrl1.refreshAlarmList();
            valmoWin.execHandle(opeOrderType.winMsg, new WinMsg(WinMsgType.mwMsg));
            paramSetUnitCtrl.sLanRefresh();
        }
       
        public bool linkState = false;
        public void sendMsgToWinFunc(WinMsg msg)
        {
            //if (!valmoWin.flagMainWindowInitOk)
            //    return;
            switch (msg.type)
            {
                case WinMsgType.mwLinkPlcOK:
                    {
                        PLCLinkState = true;
                        linkPanel.flagForceClose = false;
                        linkPanel.active = true;
                        linkPanel.Visibility = Visibility.Hidden;

                        linkState = true;
                        recUnit obj = new recUnit("Sys000", DateTime.Now, recType.sysType);
                        valmoWin.eventMgr.msgSave(obj);
                        //WinMsg mwMsg = new WinMsg();
                        //mwMsg.type = WinMsgType.mwMsg;
                        //valmoWin.sendMsgToWinHandle(mwMsg);
                    }
                    break;
                case WinMsgType.mwLinkPlcError:
                    {
                        PLCLinkState = false;
                        if (!linkPanel.flagForceClose)
                            linkPanel.show();
                        if (linkState)
                        {
                            linkState = false;
                            recUnit obj = new recUnit("Sys001", DateTime.Now, recType.sysType);
                            valmoWin.eventMgr.msgSave(obj);
                        }
                        //WinMsg mwMsg = new WinMsg();
                        //mwMsg.type = WinMsgType.mwMsg;
                        //valmoWin.sendMsgToWinHandle(mwMsg);
                    }
                    break;
                case WinMsgType.mwLogInOK:
                    {
                        PLCLinkState = true;
                        //imgUserNull.Opacity = 0;
                        //imgUserOn.Opacity = 1;
                        lbUserName.Content = valmoWin.dv.users.curUser.name;
                    }
                    break;
                case WinMsgType.mwLogOut:
                    {
                        //imgUserNull.Opacity = 1;
                        //imgUserOn.Opacity = 0;
                        lbUserName.Content = "null";
                    }
                    break;
                case WinMsgType.mwPidClear:
                    {
                    }
                    break;
                case WinMsgType.mwJumpHeartErr:
                    {
                        //imgLineOn.Visibility = Visibility.Hidden;
                    }
                    break;
                case WinMsgType.mwJumpHeartRetart:
                    {

                    }
                    break;
                case WinMsgType.mwRelink:
                    {
                        PLCLinkState = true;
                        linkPanel.flagForceClose = false;
                        linkPanel.active = true;

                        recUnit obj = new recUnit("Sys000", DateTime.Now, recType.sysType);
                        valmoWin.eventMgr.msgSave(obj);
                    }
                    break;
            }
            lstWinMsgCtrl1.sendMsgToWinFunc(msg);
        }

        private void imgLinkOn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PLCLinkState = false;
        }
        private void imgLinkOff_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PLCLinkState = true;
        }

        private void imgLineOn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (linkPanel.Visibility == Visibility.Hidden)
            {
                valmoWin.SLinkPlcPanel.show();
            }
            else
                valmoWin.SLinkPlcPanel.hide();
        }

        private void SystemClock()
        {
            int sysDate = valmoWin.dv.SysPr[13].valueNew;
            int sysTime = valmoWin.dv.SysPr[14].valueNew;

            try
            {
                valmoWin.SysTime = new DateTime(Convert.ToInt32((sysDate >> 16) & 0x0fff), Convert.ToInt32((sysDate >> 12) & 0x0000f), Convert.ToInt32((sysDate >> 4) & 0x00000ff),
                     Convert.ToInt32((sysTime >> 24) & 0x000000ff), Convert.ToInt32((sysTime >> 16) & 0x000000ff), Convert.ToInt32((sysTime >> 8) & 0x000000ff));
            }
            catch
            {
                return;
            }

            lbDate.Content = valmoWin.SysTime.Month.ToString().PadLeft(2, '0') + "-" + valmoWin.SysTime.Day.ToString().PadLeft(2, '0') + " " + valmoWin.SysTime.Year.ToString();
            lbTime.Content = valmoWin.SysTime.Hour.ToString().PadLeft(2, '0') + " " + valmoWin.SysTime.Minute.ToString().PadLeft(2, '0');

            lbFlag.Visibility = lbFlag.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;

            switch (valmoWin.SysTime.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    lbWeek.SetResourceReference(Label.ContentProperty, "Monday");
                    break;
                case DayOfWeek.Tuesday:
                    lbWeek.SetResourceReference(Label.ContentProperty, "Tuesday");
                    break;
                case DayOfWeek.Wednesday:
                    lbWeek.SetResourceReference(Label.ContentProperty, "Wednesday");
                    break;
                case DayOfWeek.Thursday:
                    lbWeek.SetResourceReference(Label.ContentProperty, "Thursday");
                    break;
                case DayOfWeek.Friday:
                    lbWeek.SetResourceReference(Label.ContentProperty, "Friday");
                    break;
                case DayOfWeek.Saturday:
                    lbWeek.SetResourceReference(Label.ContentProperty, "Saturday");
                    break;
                case DayOfWeek.Sunday:
                    lbWeek.SetResourceReference(Label.ContentProperty, "Sunday");
                    break;
                default:
                    lbWeek.SetResourceReference(Label.ContentProperty, "Monday");
                    break;
            }
        }

        private void lbBack_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //cvsTime.Visibility = Visibility.Hidden;
        }
        private void tbLan_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(4))
                return;
            lanSelectPanel.show();
        }

        private void btnSetTime_MouseUp(object sender, MouseButtonEventArgs e)
        {
            dtCtrl.show();
        }

        private void btnCammra_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            btnCammra.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(140, 140, 140));
        }

        private void btnCammra_MouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            btnCammra.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Transparent);

            if (cvsVideo.Visibility == Visibility.Hidden)
            {
                if (VideoSource.getInstance().bInitState == true)
                {
                    VideoSource.getInstance().Start();

                    cvsVideo.Visibility = Visibility.Visible;
                }
            }
            else
            {
                if (VideoSource.getInstance().bInitState == true)
                {
                    VideoSource.getInstance().Stop();

                    cvsVideo.Visibility = Visibility.Hidden;
                }
            }
        }

        private void StopVideo()
        {
            VideoSource.getInstance().Stop();
        }

        private void btnCammra_MouseLeave(object sender, MouseEventArgs e)
        {
            e.Handled = true;

            btnCammra.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Transparent);
        }

        private bool bIsMouseDown = false;
        private System.Windows.Point pLastMousePos;

        private void cvsVideoHead_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            bIsMouseDown = true;
            pLastMousePos = e.GetPosition(cvsMain);
        }

        private void cvsVideoHead_MouseMove(object sender, MouseEventArgs e)
        {
            if (bIsMouseDown == true)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    System.Windows.Point pCurMousePos = e.GetPosition(cvsMain);

                    double top = Canvas.GetTop(cvsVideo);
                    double topNew = top + pCurMousePos.Y - pLastMousePos.Y;
                    Canvas.SetTop(cvsVideo, topNew);

                    double left = Canvas.GetLeft(cvsVideo);
                    double leftNew = left + pCurMousePos.X - pLastMousePos.X;
                    Canvas.SetLeft(cvsVideo, leftNew);

                    pLastMousePos = pCurMousePos;
                }
            }
        }

        private void cvsVideoHead_MouseUp(object sender, MouseButtonEventArgs e)
        {
            bIsMouseDown = false;
        }

        private void cvsVideoHead_MouseLeave(object sender, MouseEventArgs e)
        {
            bIsMouseDown = false;
        }

        private void btnClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            btnClose.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0x1C, 0x08, 0x7f));
        }

        private void btnClose_MouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            btnClose.Background = new SolidColorBrush(Colors.Transparent);

            if (VideoSource.getInstance().bInitState == true)
            {
                VideoSource.getInstance().Stop();

                cvsVideo.Visibility = Visibility.Hidden;
            }
        }

        private void btnClose_MouseLeave(object sender, MouseEventArgs e)
        {
            btnClose.Background = new SolidColorBrush(Colors.Transparent);
        }
    }

    public class alarmPanel
    {
        public recUnit erObj;
        public Canvas cvs;
        public int no;
        //public string strTest = "";
        public Label lbTest = new Label();

        Label lbSerialNum;
        Label lbDis;
        Label lbDtStart;
        Label lbNo;
        //Image imgBK;
        public alarmPanel(recUnit ErObj,int no)
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

            if(ErObj != null)
            {
                lbSerialNum.Content = ErObj.serialNum;
                lbDis.Content = ErObj.discription;
                lbDtStart.Content = ErObj.dtStart.ToString("yyyy.MM.dd hh:mm:ss");
                lbNo.Content = no;
            }
            erObj = ErObj;

        }

        public void setValue(recUnit ErObj,int no)
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
