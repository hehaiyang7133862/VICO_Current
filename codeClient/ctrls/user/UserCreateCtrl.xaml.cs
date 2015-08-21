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
    /// UserCreateCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class UserCreateCtrl : UserControl
    {
        private SolidColorBrush brushFocus = new SolidColorBrush(Color.FromRgb(184, 225, 195));
        private SolidColorBrush brushNormal = new SolidColorBrush(Color.FromRgb(184, 184, 184));

        DispatcherTimer dt = new DispatcherTimer();

        public UserCreateCtrl()
        {
            InitializeComponent();

            lbFailed.Visibility = Visibility.Hidden;
            cvsMain.Height = 46;

            dt.Tick += new EventHandler(dt_Tick);
            dt.Interval = new TimeSpan(0, 0, 2);
        }

        void dt_Tick(object sender, EventArgs e)
        {
            lbFailed.Visibility = Visibility.Hidden;

            dt.Stop();
        }

        private void Dispose()
        {
            lbUsername.BorderBrush = new SolidColorBrush(Colors.Transparent);
            lbPassword.BorderBrush = new SolidColorBrush(Colors.Transparent);
            lbPasswordConfirm.BorderBrush = new SolidColorBrush(Colors.Transparent);
        }

        private string getPasswordStr(int length)
        {
            string str = "";

            for (int i = 0; i < length; i++)
            {
                str += "*";
            }

            return str;
        }

        private void lbUsername_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lbUsername.BorderBrush = new SolidColorBrush(Color.FromRgb(0x00, 0xE1, 0xBF));

            Thickness margin = new Thickness(108, 700, 0, 0);
            valmoWin.SCharKeyPanel.dis = valmoWin.dv.getCurDis("UserName");
            valmoWin.SCharKeyPanel.init(true, "", setUsername, Dispose, margin, lbUsername);
        }

        userClass curUser;
        private void Init(userClass User)
        {
            lbUsername.Content = User.name;
            Password = PassowrdConfirm = User.password;
            lbPassword.Content = getPasswordStr(Password.Length);
            lbPasswordConfirm.Content = getPasswordStr(Password.Length);

            switch (User.accessLevel)
            {
                case 2:
                    cbLevel.SelectedIndex = 1;
                    break;
                default:
                    cbLevel.SelectedIndex = 0;
                    break;
            }

            lbError.Visibility = Visibility.Hidden;
        }

        string Username;
        void setUsername(string str)
        {
            Username = str.Trim();
            lbUsername.Content = Username;
        }

        private void lbPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lbPassword.BorderBrush = new SolidColorBrush(Color.FromRgb(0x00, 0xE1, 0xBF));

            Thickness margin = new Thickness(108, 700, 0, 0);
            valmoWin.SCharKeyPanel.dis = valmoWin.dv.getCurDis("Secret");
            valmoWin.SCharKeyPanel.init(true, "", setPassword, Dispose, margin, lbPassword);
        }

        string Password;
        void setPassword(string str)
        {
            Password = str.Trim();
            string strTmp = "";
            for (int i = 0; i < Password.Length; i++)
                strTmp += "*";
            lbPassword.Content = strTmp;
        }

        private void lbPasswordConfirm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lbPasswordConfirm.BorderBrush = new SolidColorBrush(Color.FromRgb(0x00, 0xE1, 0xBF));

            Thickness margin = new Thickness(108, 700, 0, 0);
            valmoWin.SCharKeyPanel.dis = valmoWin.dv.getCurDis("Secret");
            valmoWin.SCharKeyPanel.init(true, "", setPasswordConfirm, Dispose, margin, lbPasswordConfirm);
        }

        string PassowrdConfirm;
        void setPasswordConfirm(string str)
        {
            PassowrdConfirm = str.Trim();
            string strTmp = "";
            for (int i = 0; i < Password.Length; i++)
                strTmp += "*";
            lbPasswordConfirm.Content = strTmp;
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (PassowrdConfirm == Password)
            {
                valmoWin.dv.users.mdyUser(null, curUser.name, Password, cbLevel.SelectedIndex, curUser.language);
                lbError.Visibility = Visibility.Hidden;

                UserCtrl.refushUser();

                cvsMain.Height = 46;
            }
            else
            {
                lbError.Visibility = Visibility.Visible;
            }
        }
        
        private bool IsCollapsed = true;
        private void cvsHead_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (IsCollapsed == true)
            {
                IsCollapsed = false;

                curUser = valmoWin.dv.users.addUser();

                if (curUser != null)
                {
                    Init(curUser);

                    lbFailed.Visibility = Visibility.Hidden;
                }
                else
                {
                    lbFailed.Visibility = Visibility.Visible;

                    dt.Start();

                    return;
                }

                cvsHead.Background = brushFocus;
                cvsMain.Height = 510;
            }
            else
            {
                IsCollapsed = true;

                cvsHead.Background = brushNormal;
                cvsMain.Height = 46;
            }
        }
    }
}
