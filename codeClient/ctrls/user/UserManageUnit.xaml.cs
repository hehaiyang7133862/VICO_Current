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

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// UserManageUnit.xaml 的交互逻辑
    /// </summary>
    public partial class UserManageUnit : UserControl
    {
        private SolidColorBrush brushFocus = new SolidColorBrush(Color.FromRgb(184, 225, 195));
        private SolidColorBrush brushNormal = new SolidColorBrush(Color.FromRgb(184, 184, 184));
        private userClass curUser;

        public UserManageUnit()
        {
            InitializeComponent();

            cvsMain.Height = 46;
            lbError.Visibility = Visibility.Hidden;
        }

        public void Init(userClass curUser)
        {
            this.curUser = curUser;

            switch (curUser.accessLevel)
            {
                case 1:
                    lbContentBing(lbLevel, "userOp");
                    LevelIcon.SelectedIndex = 1;
                    cbLevel.SelectedIndex = 0;
                    break;
                case 2:
                    lbContentBing(lbLevel, "userMt");
                    LevelIcon.SelectedIndex = 2;
                    cbLevel.SelectedIndex = 1;
                    break;
                case 3:
                    lbContentBing(lbLevel, "userMgr");
                    LevelIcon.SelectedIndex = 3;
                    lbContentBing(lbLevel2, "userMgr");
                    break;
                case 4:
                    lbContentBing(lbLevel, "userSer");
                    LevelIcon.SelectedIndex = 4;
                    lbContentBing(lbLevel2, "userSer");
                    break;
                case 5:
                    lbContentBing(lbLevel, "userRoot");
                    LevelIcon.SelectedIndex = 5;
                    lbContentBing(lbLevel2, "userRoot");
                    break;
                default:
                    lbLevel.Content = "unDefined";
                    LevelIcon.SelectedIndex = 0;
                    lbContentBing(lbLevel2, "unDefined");
                    break;
            }

            if (curUser.accessLevel > 2)
            {
                btnDelete.IsEnabled = false;
                cbLevel.IsEnabled = false;
                lbLevel2.Visibility = Visibility.Visible;
            }
            else
            {
                btnDelete.IsEnabled = true;
                cbLevel.IsEnabled = true;
                lbLevel2.Visibility = Visibility.Hidden;

            }

            lbUsername.Content = curUser.name;

            lbError.Visibility = Visibility.Hidden;

            Password = PassowrdConfirm = curUser.password;
            lbPassword.Content = getPasswordStr(Password.Length);
            lbPasswordConfirm.Content = getPasswordStr(Password.Length);
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

        private void lbContentBing(Label lb,string sourceKey)
        {
            object obj_Item = TryFindResource(sourceKey);

            if (obj_Item != null)
            {
                lb.SetResourceReference(Label.ContentProperty, sourceKey);
            }
            else
            {
                lb.Content = sourceKey + "_undefined";
            }
        }

        private bool IsCollapsed = true;
        private void cvsHead_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (IsCollapsed == true)
            {
                IsCollapsed = false;

                cvsHead.Background = brushFocus;
                cvsMain.Height = 500;
            }
            else
            {
                IsCollapsed = true;

                cvsHead.Background = brushNormal;
                cvsMain.Height = 46;
            }
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

        string PassowrdConfirm;

        private void lbPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lbPassword.BorderBrush = new SolidColorBrush(Color.FromRgb(0x00, 0xE1, 0xBF));

            Thickness margin = new Thickness(108, 400, 0, 0);
            valmoWin.SCharKeyPanel.dis = valmoWin.dv.getCurDis("Secret");
            valmoWin.SCharKeyPanel.init(true, "", setPassword, Dispose, margin, lbPassword);
        }

        void setPasswordConfirm(string str)
        {
            PassowrdConfirm = str.Trim();
            string strTmp = "";
            for (int i = 0; i < Password.Length; i++)
                strTmp += "*";
            lbPasswordConfirm.Content = strTmp;
        }
        private void lbPasswordConfirm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lbPasswordConfirm.BorderBrush = new SolidColorBrush(Color.FromRgb(0x00, 0xE1, 0xBF));

            Thickness margin = new Thickness(108, 500, 0, 0);
            valmoWin.SCharKeyPanel.dis = valmoWin.dv.getCurDis("Secret");
            valmoWin.SCharKeyPanel.init(true, "", setPasswordConfirm, Dispose, margin, lbPasswordConfirm);
        }

        private void Dispose()
        {
            lbPasswordConfirm.BorderBrush = new SolidColorBrush(Colors.Transparent);
            lbPassword.BorderBrush = new SolidColorBrush(Colors.Transparent);
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (Password == PassowrdConfirm)
            {
                curUser.password = Password;

                lbError.Visibility = Visibility.Hidden;
            }
            else
            {
                lbError.Visibility = Visibility.Visible;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            valmoWin.dv.users.delUser(curUser);

            UserCtrl.refushUser();
        }

        private void cbLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (curUser.accessLevel < 3)
            {
                if (cbLevel.SelectedIndex == 1)
                {
                    curUser.accessLevel = 2;
                    lbContentBing(lbLevel2, "userMt");
                    lbContentBing(lbLevel, "userMt");
                }
                else
                {
                    curUser.accessLevel = 1;
                    lbContentBing(lbLevel2, "userOp");
                    lbContentBing(lbLevel, "userOp");
                }
            }
        }
    }
}
