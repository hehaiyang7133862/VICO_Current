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
    /// Interaction logic for userOwnMsgSet.xaml
    /// </summary>
    public partial class userOwnMsgSet : UserControl
    {
        userMsgSet.MdyUserMsgEvent mdyUserMsgHandle;
        userClass curUser;
        public userOwnMsgSet()
        {
            InitializeComponent();
        }

        public void init(userClass user, userMsgSet.MdyUserMsgEvent mdyUserMsgHandle)
        {
            this.mdyUserMsgHandle = mdyUserMsgHandle;
            curUser = user;
            lbUserName.Content = curUser.name;
            setLbPassword(curUser.password);
            setLbNewPassword(curUser.password);
        }
        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lbUserName.Background = Brushes.Silver;
            //fd.charKeyHideHandle = new charKeyHideEvent(charKeyEnterFunc);
            //fd.handle = new feedbackObject.callback(setLbNameOk);
            //valmoWin.getStrFromCharKey(false, setUserName, new Thickness(30, 1300, 0, 0), fd);
            //valmoWin.getStrFromCharkeyHandle(false, lbUserName.Content.ToString(), null, setLbNameOk, charKeyHideFunc, new Thickness(30, 1300, 0, 0));
            valmoWin.SCharKeyPanel.dis = valmoWin.dv.getCurDis("lanKey1023");
            valmoWin.SCharKeyPanel.init(false, lbUserName.Content.ToString(), setLbNameOk, charPanelDispose, new Thickness(30, 1300, 0, 0));
            //getStrFromCharKeyEvent(bool isSecretChars, string oldValue, charKeyChangeEvent changeHandle, charKeyEnterEvent enterHandle, charKeyHideEvent hideHandle, Thickness margin);
        }
        public void setUserName(string str)
        {
            lbUserName.Content = str;
        }
        public void charPanelDispose()
        {
            lbUserName.Background = Brushes.Transparent;
            lbPasswordValue.Background = Brushes.Transparent;
            lbNewPasswordValue.Background = Brushes.Transparent;


        }
        void setLbNameOk(string newName)
        {
            valmoWin.dv.users.mdyUser(newName, curUser.name, curUser.password, curUser.accessLevel, curUser.language);
            mdyUserMsgHandle(curUser, UserHandleType.NAME);
            lbUserName.Content = newName;
        }
        void setLbPassword(string str)
        {
            //valmoWin.dv.users.mdyUser(newName, curUser.name, curUser.password, curUser.accessLevel, curUser.language);
            //mdyUserMsgHandle(curUser, UserHandleType.NAME);
            //string strTmp = "";
            //for (int i = 0; i < str.Length; i++)
            //    strTmp += "*";
            //lbPasswordValue.Content = strTmp;
            //lbPasswordValue.Tag = str;
        }
        void setLbNewPassword(string str)
        {
            string strTmp = "";
            for (int i = 0; i < str.Length; i++)
                strTmp += "*";
            lbNewPasswordValue.Content = strTmp;
            lbNewPasswordValue.Tag = str;
        }

        private void lbPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lbPasswordValue.Background = Brushes.Silver;
            //feedbackObject fd = new feedbackObject();
            //fd.charKeyHideHandle = new charKeyHideEvent(charKeyHideFunc);
            //fd.handle = new feedbackObject.callback(setLbNameOk);
            //valmoWin.getStrFromCharKey(false, new setLabelContent(setLbPassword), new Thickness(30, 1300, 0, 0), fd);

            //valmoWin.getStrFromCharkeyHandle(true, lbUserName.Content.ToString(), null, setLbNameOk, charKeyHideFunc, new Thickness(30, 1300, 0, 0));
            valmoWin.SCharKeyPanel.dis = valmoWin.dv.getCurDis("Secret");
            valmoWin.SCharKeyPanel.init(true, lbUserName.Content.ToString(), setLbNameOk, charPanelDispose, new Thickness(30, 1300, 0, 0));
        }

        private void lbNewPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lbNewPasswordValue.Background = Brushes.Silver;
            //feedbackObject fd = new feedbackObject();
            //fd.charKeyHideHandle = new charKeyHideEvent(charKeyHideFunc);
            //fd.handle = new feedbackObject.callback(setLbNameOk);
            //valmoWin.getStrFromCharKey(false, new setLabelContent(setLbNewPassword), new Thickness(30, 1200, 0, 0), fd);
        }

        private void lbPasswordOk_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (lbPasswordValue.Tag.ToString() == lbNewPasswordValue.Tag.ToString())
            {
                valmoWin.dv.users.mdyUser(null, curUser.name, lbPasswordValue.Tag.ToString(), curUser.accessLevel, curUser.language);
                lbPwOK.Visibility = Visibility.Visible;
                lbPwError.Visibility = Visibility.Hidden;
            }
            else
            {
                lbPwError.Visibility = Visibility.Visible;
                lbPwOK.Visibility = Visibility.Hidden;
            }
            cvsPasswordOk.Visibility = Visibility.Visible;

        }

        private void Label_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void cvsPasswordOk_MouseDown(object sender, MouseButtonEventArgs e)
        {
            cvsPasswordOk.Visibility = Visibility.Hidden;
        }

        private void cvsPasswordOk_MouseUp(object sender, MouseButtonEventArgs e)
        {
            cvsPasswordOk.Visibility = Visibility.Hidden;
        }

        //private void lbOpSelected_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    cvsAccessLevel.Visibility = Visibility.Hidden;
        //    valmoWin.dv.users.mdyUser(null, curUser.name, curUser.password, 1, curUser.language);
        //    mdyUserMsgHandle(curUser, UserHandleType.ACCESSLEVEL);
        //}
        //private void lbMtSelected_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    cvsAccessLevel.Visibility = Visibility.Hidden;
        //    valmoWin.dv.users.mdyUser(null, curUser.name, curUser.password, 2, curUser.language);
        //    mdyUserMsgHandle(curUser, UserHandleType.ACCESSLEVEL);
        //}

        //private void Label_MouseDown_1(object sender, MouseButtonEventArgs e)
        //{
        //    cvsAccessLevel.Visibility = Visibility.Visible;
        //    if (curUser != null)
        //    {
        //        if (valmoWin.dv.users.curUser.accessLevel > 2)
        //        {
        //            imgMtSelected.Visibility = Visibility.Visible;
        //            lbMtSelected.Visibility = Visibility.Visible;
        //        }
        //        else
        //        {
        //            imgMtSelected.Visibility = Visibility.Hidden;
        //            lbMtSelected.Visibility = Visibility.Hidden;
        //        }
        //    }
        //}
    }
}
