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
    /// Interaction logic for userMsgSet.xaml
    /// </summary>
    public partial class userMsgSet : UserControl
    {
        public delegate bool MdyUserMsgEvent(userClass user, UserHandleType type);

        MdyUserMsgEvent mdyUserMsgHandle;
        userClass curUser;
        string password = "";
        string newPassword = "";
        public userMsgSet()
        {
            InitializeComponent();
            lbPasswordOk.Background = Brushes.Silver;
            this.Visibility = Visibility.Hidden;
        }
        public void show()
        {
            if (valmoWin.dv.users.curUser.accessLevel < 1 || valmoWin.dv.users.curUser == curUser || curUser == valmoWin.dv.users.op || curUser == valmoWin.dv.users.mt || curUser == valmoWin.dv.users.mgr || curUser == valmoWin.dv.users.service || curUser == valmoWin.dv.users.root)
            {
                lbDelUser.Background = Brushes.Silver;
            }
            else
            {
                lbDelUser.Background = Brushes.Transparent;
            }
            this.Visibility = Visibility.Visible;
        }
        public void hide()
        {
            this.Visibility = Visibility.Hidden;
        }
        public void init(userClass user, MdyUserMsgEvent mdyUserMsgHandle)
        {
            this.mdyUserMsgHandle = mdyUserMsgHandle;

            curUser = user;
            if (curUser != null)
            {
                lbUserName.Content = curUser.name;
                switch (curUser.accessLevel)
                {
                    case 1:
                        lbAccessLevel.Content = valmoWin.dv.getCurDis("userOp");
                        break;
                    case 2:
                        lbAccessLevel.Content = valmoWin.dv.getCurDis("userMt");
                        break;
                    case 3:
                        lbAccessLevel.Content = valmoWin.dv.getCurDis("userMgr");
                        break;
                    case 4:
                        lbAccessLevel.Content = valmoWin.dv.getCurDis("userSer");
                        break;
                    case 5:
                        lbAccessLevel.Content = valmoWin.dv.getCurDis("userRoot");
                        break;
                }
                setLbPassword(curUser.password);
                setLbNewPassword(curUser.password);
            }
        }
        public void init(userClass user)
        {
            try
            {
                curUser = user;
                if (curUser != null)
                {
                    lbUserName.Content = curUser.name;
                    lbDelUserName.Content = curUser.name;
                    lbAccessLevel.Content = valmoWin.dv.getCurDis(Users.userTypeName[curUser.accessLevel]);
                    lbDelAccessLevel.Content = valmoWin.dv.getCurDis(Users.userTypeName[curUser.accessLevel]);
                    setLbPassword(curUser.password);
                    setLbNewPassword(curUser.password);
                    password = curUser.password;
                    newPassword = curUser.password;

                    string strTmp = "";
                    for (int i = 0; i < password.Length; i++)
                        strTmp += "*";
                    lbPasswordValue.Content = strTmp;
                    lbNewPasswordValue.Content = strTmp;
                    lbPasswordOk.Background = Brushes.Silver;
                }
                if (valmoWin.dv.users.curUser.accessLevel < 1 || valmoWin.dv.users.curUser == curUser || curUser == valmoWin.dv.users.op || curUser == valmoWin.dv.users.mt || curUser == valmoWin.dv.users.mgr || curUser == valmoWin.dv.users.service || curUser == valmoWin.dv.users.root)
                {
                    lbDelUser.Background = Brushes.Silver;
                }
                else
                {
                    lbDelUser.Background = Brushes.Transparent;
                }
            }
            catch
            {

            }
        }

        private void lbDelUser_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //cvsDelUser.Visibility = Visibility.Visible;
            if (lbDelUser.Background != Brushes.Silver)
                tbUseSet.SelectedItem = itemDel;
        }

        private void lbDelUserOk_MouseUp(object sender, MouseButtonEventArgs e)
        {
            valmoWin.dv.users.delUser(curUser);
            userSetPanelCtrl.refreshBarsNewHandle();
        }

        private void lbDelUserCancle_MouseUp(object sender, MouseButtonEventArgs e)
        {
            tbUseSet.SelectedItem = itemMain;
            //cvsDelUser.Visibility = Visibility.Hidden;
        }

        private void lbUserName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                lbUserName.Background = Brushes.Silver;
                valmoWin.SCharKeyPanel.dis = valmoWin.dv.getCurDis("lanKey1023");
                valmoWin.SCharKeyPanel.init(false, lbUserName.Content.ToString(), setLbNameOk, charPanelDispose, new Thickness(30, 1000, 0, 0));
            }
            catch
            {

            }

        }
        public void setUserName(string str)
        {
            //lbUserName.Content = str;
        }
        public void charPanelDispose()
        {
            lbUserName.Background = Brushes.Transparent;
            lbPasswordValue.Background = Brushes.Transparent;
            lbNewPasswordValue.Background = Brushes.Transparent;
        }
        void setLbNameOk(string newName)
        {
            try
            {
                valmoWin.dv.users.mdyUser(newName, curUser.name, curUser.password, curUser.accessLevel, curUser.language);
                userSetPanelCtrl.refreshBarsHandle();
            }
            catch
            {

            }

        }
        void setLbPassword(string str)
        {
            try
            {
                string strTmp = "";
                for (int i = 0; i < str.Length; i++)
                    strTmp += "*";
                lbPasswordValue.Content = strTmp;
                password = str;
                lbPasswordOk.Background = Brushes.Transparent;
            }
            catch
            {

            }
        }
        void setLbNewPassword(string str)
        {
            try
            {
                string strTmp = "";
                for (int i = 0; i < str.Length; i++)
                    strTmp += "*";
                lbNewPasswordValue.Content = strTmp;
                newPassword = str;
                lbPasswordOk.Background = Brushes.Transparent;
            }
            catch
            {

            }
        }

        private void lbPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                lbPasswordValue.Background = Brushes.Silver;
                valmoWin.SCharKeyPanel.dis = valmoWin.dv.getCurDis("Secret");
                valmoWin.SCharKeyPanel.init(true, "", setLbPassword, charPanelDispose, new Thickness(30, 1000, 0, 0));
            }
            catch
            {

            }
        }

        private void lbNewPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                lbNewPasswordValue.Background = Brushes.Silver;
                valmoWin.SCharKeyPanel.dis = valmoWin.dv.getCurDis("Secret");
                valmoWin.SCharKeyPanel.init(true, "", setLbNewPassword, charPanelDispose, new Thickness(30, 1000, 0, 0));
            }
            catch
            {

            }
        }

        private void lbPasswordOk_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (lbPasswordOk.Background != Brushes.Silver)
                {
                    tbUseSet.SelectedItem = itemSectet;
                    if (password == newPassword)
                    {
                        valmoWin.dv.users.mdyUser(null, curUser.name, password, curUser.accessLevel, curUser.language);

                        lbPwOK.Visibility = Visibility.Visible;
                        lbPwError.Visibility = Visibility.Hidden;
                        lbPasswordOk.Background = Brushes.Silver;
                    }
                    else
                    {
                        lbPwError.Visibility = Visibility.Visible;
                        lbPwOK.Visibility = Visibility.Hidden;
                    }
                }
            }
            catch
            {

            }

        }

        private void Label_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void cvsPasswordOk_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tbUseSet.SelectedItem = itemMain;
            //cvsPasswordOk.Visibility = Visibility.Hidden;
        }

        private void cvsPasswordOk_MouseUp(object sender, MouseButtonEventArgs e)
        {
            tbUseSet.SelectedItem = itemMain;
            //cvsPasswordOk.Visibility = Visibility.Hidden;
        }

        private void lbOpSelected_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                cvsAccessLevel.Visibility = Visibility.Hidden;
                valmoWin.dv.users.mdyUser(null, curUser.name, curUser.password, 1, curUser.language);
                userSetPanelCtrl.refreshBarsNewHandle();
            }
            catch
            {

            }
        }
        private void lbMtSelected_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                cvsAccessLevel.Visibility = Visibility.Hidden;
                valmoWin.dv.users.mdyUser(null, curUser.name, curUser.password, 2, curUser.language);
                userSetPanelCtrl.refreshBarsNewHandle();
            }
            catch
            {

            }
        }

        private void lbAccessLevel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (valmoWin.dv.users.curUser.accessLevel > 2 && curUser != valmoWin.dv.users.op && curUser != valmoWin.dv.users.mt && curUser != valmoWin.dv.users.mgr)
            {
                cvsAccessLevel.Visibility = Visibility.Visible;
                if (curUser != null)
                {
                    if (valmoWin.dv.users.curUser.accessLevel > 2)
                    {
                        imgMtSelected.Visibility = Visibility.Visible;
                        lbMtSelected.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        imgMtSelected.Visibility = Visibility.Hidden;
                        lbMtSelected.Visibility = Visibility.Hidden;
                    }
                }
            }
        }

        private void cvsAccessLevelBackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            cvsAccessLevel.Visibility = Visibility.Hidden;
        }
    }
}

