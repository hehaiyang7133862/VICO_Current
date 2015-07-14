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
using System.Threading;
using nsDataMgr;

namespace nsVicoClient.ctrls
{
    public delegate void changeItemEvent(int nr);
    /// <summary>
    /// Interaction logic for userCtrl.xaml
    /// </summary>
    public partial class userCtrl : UserControl
    {
        string userPassword = "";
        public userCtrl()
        {
            InitializeComponent();
            valmoWin.lstUsbCheckFunc.Add(checkUsb);

            this.Visibility = Visibility.Hidden;
        }

        private void charPanelDispose()
        {
            imgUserFocus.Visibility = Visibility.Hidden;
            imgSecretFocus.Visibility = Visibility.Hidden;
            bdLoader.Visibility = Visibility.Hidden;
        }

        public void loadCtrlInit()
        {
            userPassword = "";
            lbUserName.Content = "";
            lbSecret.Content = "";
            lbLoadOk.Visibility = Visibility.Hidden;
            lbLoadError.Visibility = Visibility.Hidden;
        }
        public void setCtrlInit()
        {
            userClass user = valmoWin.dv.users.curUser;
            lbCurUserName.Content = user.name;

            Application.Current.Resources["CurUserName"] = user.name;
            lbCurAccessLevel.Content = valmoWin.dv.getCurDis(Users.userTypeName[user.accessLevel]);
            DateTime dtnow = DateTime.Now;
            lbLoadTm.Content = dtnow.Year + "." + dtnow.Month + "." + dtnow.Day + "  " + dtnow.Hour + ":" + dtnow.Minute + ":" + dtnow.Second;
            mgrSetUserCtrl1.init();
        }

        public void checkUsb()
        {
            if (valmoWin.sUsbPath == null)
            {
                btnCtrl1.Visibility = Visibility.Hidden;
            }
            else
            {
                btnCtrl1.Visibility = Visibility.Visible;
            }
        }

        bool flagLoad = true;
        public void show()
        {
            flagLoad = true;
            tbMenu.Visibility = Visibility.Visible;

            if (valmoWin.dv.users.curUser.accessLevel >= 4)
            {
                btnExit.Visibility = Visibility.Visible;
            }
            else
            {
                btnExit.Visibility = Visibility.Hidden;
            }
            if (valmoWin.dv.users.curUser == valmoWin.dv.users.nullUser)
            {
                loadCtrlInit();
                tbMenu.SelectedIndex = 0;
            }
            else
            {
                setCtrlInit();
                tbMenu.SelectedIndex = 1;
            }
            flagPanelStateChange = false;
            getBasicPoint = false;
            if (valmoWin.sUsbPath == null)
            {
                btnCtrl1.Visibility = Visibility.Hidden;
            }
            else
            {
                btnCtrl1.Visibility = Visibility.Visible;
            }
            this.Opacity = 1;
            this.Visibility = Visibility.Visible;
        }

        public void hide()
        {
            this.Visibility = Visibility.Hidden;
        }

        private void lbBack_MouseDown(object sender, MouseButtonEventArgs e)
        {
            hide();
        }

        private void imgSwitchUser_MouseUp(object sender, MouseButtonEventArgs e)
        {
            valmoWin.dv.users.unload();
            changeItemInvoke(0);
        }

        private void cvsUserLock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            valmoWin.execHandle(opeOrderType.lockSysPanelShow);
            hide();
        }
        private void cvsQuit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            valmoWin.dv.users.unload();
            this.hide();
            changeItemInvoke(0);
        }

        private void lbUserName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            imgUserFocus.Visibility = Visibility.Visible;
            bdLoader.Visibility = Visibility.Visible;
            Canvas.SetTop(bdLoader, Canvas.GetTop(bdUserName));

            Thickness margin = new Thickness(108, 600, 0, 0);
            valmoWin.SCharKeyPanel.dis = valmoWin.dv.getCurDis("UserName");
            valmoWin.SCharKeyPanel.init(false, lbUserName.Content.ToString(), setUserFunc, charPanelDispose, margin, lbUserName);
            lbLoadOk.Visibility = Visibility.Hidden;
            lbLoadError.Visibility = Visibility.Hidden;
        }
        void setUserFunc(string str)
        {
            lbUserName.Content = str;
        }

        private void lbSecret_MouseDown(object sender, MouseButtonEventArgs e)
        {
            imgSecretFocus.Visibility = Visibility.Visible;
            bdLoader.Visibility = Visibility.Visible;
            Canvas.SetTop(bdLoader, Canvas.GetTop(bdSecret));
            Thickness margin = new Thickness(108, 600, 0, 0);
            valmoWin.SCharKeyPanel.dis = valmoWin.dv.getCurDis("Secret");
            valmoWin.SCharKeyPanel.init(true, "", setSecretFunc, charPanelDispose, margin, lbSecret);
            lbLoadOk.Visibility = Visibility.Hidden;
            lbLoadError.Visibility = Visibility.Hidden;
        }
        void setSecretFunc(string str)
        {
            userPassword = str;
            string strTmp = "";
            for (int i = 0; i < userPassword.Length; i++)
                strTmp += "*";
            lbSecret.Content = strTmp;
            if (valmoWin.dv.users.setCurUser(lbUserName.Content.ToString(), userPassword))
            {
                vm.printLn("load ok");
                changeItemInvoke(1);
            }
            else
            {
                lbLoadError.Visibility = Visibility.Visible;
            }
        }
        private void imglock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            valmoWin.execHandle(opeOrderType.lockSysPanelShow);
            hide();
        }

        private void imgLoad_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (valmoWin.dv.users.setCurUser(lbUserName.Content.ToString(), userPassword))
            {
                setCtrlInit();

                changeItemInvoke(1);
            }
            else
            {
                lbLoadError.Visibility = Visibility.Visible;
            }
        }
        public void changeItemInvoke(int nr)
        {
            this.Dispatcher.BeginInvoke(new changeItemEvent(changeItemFunc), new object[] { nr });
        }
        public void changeItemFunc(int nr)
        {
            switch (nr)
            {
                case 0:
                    loadCtrlInit();
                    break;
                case 1:
                    setCtrlInit();
                    break;
            }
            tbMenu.SelectedIndex = nr;
        }

        bool isSleeping = false;

        public void liuzsLoad()
        {
            valmoWin.dv.users.liuzsLoad();
        }
        public void quick(int nr)
        {
            valmoWin.dv.users.setCurUser(nr);
        }
        //private void btnSysClose_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    valmoWin.execHandle(opeOrderType.closeSysPanelShow);
        //}

        //private void cvsMain_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    //vm.debug("down");
        //    if (flagPanelStateChange)
        //    {
        //        valmoWin.execHandle(opeOrderType.layoutType, msg);
        //        this.Visibility = Visibility.Hidden;
        //    }
        //}

        //private void cvsMain_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    //vm.debug("up");
        //    if (flagPanelStateChange)
        //    {
        //        valmoWin.execHandle(opeOrderType.layoutType, msg);
        //        this.Visibility = Visibility.Hidden;
        //    }
        //}
        Point basicPoint;
        bool getBasicPoint = false;
        bool flagPanelStateChange = false;
        WinMsg msg = new WinMsg();

        private void btnCtrl1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            valmoWin.SLoadFromUsbPanel.show();
        }

        private void btnCtrl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            valmoWin.SSysExitPanel.show();
        }
        //private void cvsMain_MouseMove(object sender, MouseEventArgs e)
        //{
        //    if (e.LeftButton == MouseButtonState.Pressed)
        //    {
        //        Point theMousePoint = e.GetPosition(this.cvsMain);
        //        if (!getBasicPoint)
        //        {
        //            basicPoint = theMousePoint;
        //            getBasicPoint = true;
        //            Canvas.SetLeft(epBg, basicPoint.X);
        //            Canvas.SetTop(epBg, basicPoint.Y);
        //        }
        //    }
        //}

        //private void cvsMain_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    epBg.Width = 0;
        //    epBg.Height = 0;
        //}
    }


}