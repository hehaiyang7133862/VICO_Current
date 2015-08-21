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
    /// <summary>
    /// UserCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class UserCtrl : UserControl
    {
        public static nullEvent refushUser;
        public static nullEvent LogOut;

        DispatcherTimer dt = new DispatcherTimer();

        public UserCtrl()
        {
            InitializeComponent();

            this.Visibility = Visibility.Hidden;
            lbFailed.Visibility = Visibility.Hidden;

            UsernameInit();

            refushUser += UserInit;
            LogOut += logOut;


            dt.Tick += new EventHandler(dt_Tick);
            dt.Interval = new TimeSpan(0, 0, 2);
        }

        private void logOut()
        {
            tbMain.SelectedIndex = 0;

            lbPassword.Content = Password = "";
            lbUsername.Content = Username = "";
        }

        void dt_Tick(object sender, EventArgs e)
        {
            lbFailed.Visibility = Visibility.Hidden;

            dt.Stop();
        }

        private void UserInit()
        {
            List<userClass> lstUser = valmoWin.dv.users.getUserLst(5);

            spUserMgr.Children.Clear();
            foreach (userClass uc in lstUser)
            {
                if (uc.accessLevel < 4)
                {
                    UserManageUnit um = new UserManageUnit();
                    um.Init(uc);
                    um.Margin = new Thickness(0, 0, 0, 2);

                    spUserMgr.Children.Add(um);
                }
            }

            UserCreateCtrl uAdd = new UserCreateCtrl();
            spUserMgr.Children.Add(uAdd);
        }

        private void UsernameInit()
        {
            List<userClass> lstUser = valmoWin.dv.users.getUserLst(5);

            cbUserName.SelectionChanged -= new SelectionChangedEventHandler(cbUserName_SelectionChanged);

            cbUserName.Items.Clear();
            foreach (userClass uc in lstUser)
            {
                if (uc.accessLevel < 4)
                {
                    ComboBoxItem cbItem = new ComboBoxItem();
                    cbItem.Content = uc.name;
                    cbItem.Style = (Style)TryFindResource("SimpleComboBoxItem");
                    cbUserName.Items.Add(cbItem);
                }
            }

            cbUserName.SelectionChanged += new SelectionChangedEventHandler(cbUserName_SelectionChanged);
        }

        public void show()
        {
            this.Opacity = 1;
            this.Visibility = Visibility.Visible;

            UsernameInit();
        }
        public void hide()
        {
            this.Opacity = 0;
            this.Visibility = Visibility.Hidden;
        }

        private void Dispose()
        {
            lbUsername.BorderBrush = new SolidColorBrush(Colors.Transparent);
            lbPassword.BorderBrush = new SolidColorBrush(Colors.Transparent);
        }

        private void lbPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lbPassword.BorderBrush = new SolidColorBrush(Color.FromRgb(0x00, 0xE1, 0xBF));

            Thickness margin = new Thickness(108, 685, 0, 0);
            valmoWin.SCharKeyPanel.dis = valmoWin.dv.getCurDis("Secret");
            valmoWin.SCharKeyPanel.init(true, "", setSecretFunc, Dispose, margin, lbPassword);
        }
        string Password;
        void setSecretFunc(string str)
        {
            Password = str.Trim();
            string strTmp = "";
            for (int i = 0; i < Password.Length; i++)
                strTmp += "*";
            lbPassword.Content = strTmp;

            if (valmoWin.dv.users.setCurUser(Username, Password))
            {
                tbMain.SelectedIndex = 1;

                lbUser.Content = valmoWin.dv.users.curUser.name;
                UserInit();
            }
            else
            {
                lbFailed.Visibility = Visibility.Visible;

                dt.Start();
            }
        }

        private void lbUsername_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lbUsername.BorderBrush = new SolidColorBrush(Color.FromRgb(0x00, 0xE1, 0xBF));

            Thickness margin = new Thickness(108, 600, 0, 0);
            valmoWin.SCharKeyPanel.dis = valmoWin.dv.getCurDis("UserName");
            valmoWin.SCharKeyPanel.init(false, "", setUserName, Dispose, margin, lbUsername);
        }
        string Username;
        void setUserName(string str)
        {
            Username = str.Trim();
            lbUsername.Content = Username;
        }

        private void btnlock_Click(object sender, RoutedEventArgs e)
        {
            valmoWin.execHandle(opeOrderType.lockSysPanelShow);

            hide();
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (valmoWin.dv.users.setCurUser(Username, Password))
            {
                tbMain.SelectedIndex = 1;
                
                UserInit();
                lbUser.Content = valmoWin.dv.users.curUser.name;
            }
            else
            {
                lbFailed.Visibility = Visibility.Visible;

                dt.Start();
            }
        }

        private void cbUserName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem cbi = (ComboBoxItem)(sender as ComboBox).SelectedItem;

            Username = cbi.Content.ToString();
            lbUsername.Content = Username;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            valmoWin.SSysExitPanel.show();
        }

        private void btnSwitch_Click(object sender, RoutedEventArgs e)
        {
            valmoWin.dv.users.unload();

            tbMain.SelectedIndex = 0;
            UsernameInit();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            valmoWin.dv.users.unload();

            tbMain.SelectedIndex = 0;
            UsernameInit();

            this.hide();
        }

        private void btnUSB_Click(object sender, RoutedEventArgs e)
        {
            valmoWin.SLoadFromUsbPanel.show();
        }

        private void tbMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void Backgound_MouseDown(object sender, MouseButtonEventArgs e)
        {
            hide();
        }
    }
}
