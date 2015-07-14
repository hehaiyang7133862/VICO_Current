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

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// userBtn.xaml 的交互逻辑
    /// </summary>
    public partial class userBtn : UserControl
    {
        DispatcherTimer dtExit = new DispatcherTimer();
        //DispatcherTimer dtUserUnload = new DispatcherTimer();
        DateTime tmExit;
        long intervalTm = 15000000;//new TimeSpan(0, 0, 0, 1, 500).Ticks;

        TimeSpan userUnloadInterval = new TimeSpan(0, 10, 0);//Properties.Settings.Default.dtUserUnload;
        /// <summary> 
        /// 用户退出前的休眠时间
        /// </summary>
        TimeSpan userUnloadSleepTm = new TimeSpan(0, 0, 30);
        public userBtn()
        {
            InitializeComponent();
            dtExit.Interval = new TimeSpan(0, 0, 0, 0, 500);
            dtExit.Tick += new EventHandler(dtExit_Tick);

            valmoWin.SUserSetPanel = this;
        }

        bool isSleeping = false;
        void dtExit_Tick(object sender, EventArgs e)
        {
            DateTime tmNow = DateTime.Now;
            
            if (isMousedown)
            {
                vm.printLn("[dtExit_Tick] " + (tmNow.Ticks - tmExit.Ticks - intervalTm));
                if (tmNow.Ticks - tmExit.Ticks > intervalTm)
                {
                    if (valmoWin.dv.users.curUser.accessLevel >= 4)
                    {
                    }
                    intervalTm = long.MaxValue;
                    vm.printLn("[dtExit_Tick] valmoWin.SSysExitPanel.show");

                }
            }
            if (!isSleeping && tmNow - valmoWin.tmUserLoad > userUnloadInterval - userUnloadSleepTm && valmoWin.UsbAcc == 0)
            {
                isSleeping = true;
                valmoWin.execHandle(opeOrderType.userSleep);
            }
            if (isSleeping && tmNow - valmoWin.tmUserLoad < userUnloadInterval - userUnloadSleepTm && valmoWin.UsbAcc == 0)
            {
                isSleeping = false;
                valmoWin.execHandle(opeOrderType.userUnSleep);
            }
            if (tmNow - valmoWin.tmUserLoad > userUnloadInterval && valmoWin.UsbAcc == 0)
            {
                dtExit.Stop();
                valmoWin.dv.users.unload();
                isSleeping = false;
            }
        }
        public void setUserState()
        {
            if(valmoWin.dv.users.curUser.accessLevel > 0)
            {
                state = userLoadState.USERLOAD;
                valmoWin.tmUserLoad = DateTime.Now;
                dtExit.Start();
            }
            else
            {
                state = userLoadState.USERNULL;
                dtExit.Stop();
            }

            lbAccLevel.Content = valmoWin.dv.users.curUser.accessLevel;

        }

        public userLoadState state
        {
            set
            {
                switch (value)
                {
                    case userLoadState.USERNOACCESS:
                    case userLoadState.USERNULL:
                        {
                            BtnFore.Source = (BitmapImage)App.Current.TryFindResource("imgKeyUser_AcessLow");
                        }
                        break;
                    case userLoadState.USERLOAD:
                        {
                            BtnFore.Source = (BitmapImage)App.Current.TryFindResource("imgKeyUser_AcessWake");
                        }
                        break;
                    case userLoadState.USERBEGINTOQUIT:
                        {
                            BtnFore.Source = (BitmapImage)App.Current.TryFindResource("imgKeyUser_AcessSleep");
                        } 
                        break;
                }
            }
        }


        bool isMousedown = false;
        private void cvsMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isMousedown = true;
            btnDown.Visibility = Visibility.Visible;
            tmExit = DateTime.Now;
            intervalTm = 15000000;//new TimeSpan(0,0,0,1,500).Ticks;
        }

        private void cvsMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isMousedown)
            {
                isMousedown = false;
                btnDown.Visibility = Visibility.Hidden;
                //valmoWin.execHandle(opeOrderType.userLoadPanelShow);
                valmoWin.SUserPanel.show();
            }
        }

        private void cvsMain_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isMousedown)
            {
                btnDown.Visibility = Visibility.Hidden;
                isMousedown = false;
            }
        }
    }

    public enum userLoadState : byte
    {
        USERNOACCESS,
        USERNULL,
        USERLOAD,
        USERBEGINTOQUIT
    }
}
