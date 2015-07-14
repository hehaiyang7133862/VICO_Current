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
    /// Interaction logic for userMsg.xaml
    /// </summary>
    public partial class userMsg : UserControl
    {
        public delegate void getUserSetEvent(userClass user);

        getUserSetEvent getUserSetHandle;
        userClass curUser;
        userMsgSet curMsg = new userMsgSet();
        public userClass user
        {
            get
            {
                return curUser;
            }
        }
        public userMsg(userClass user, getUserSetEvent getUserSetHandle, int tag)
        {
            InitializeComponent();
            this.curUser = user;
            this.getUserSetHandle = getUserSetHandle;
            if (curUser != null)
            {
                if (this.curUser == valmoWin.dv.users.nullUser)
                    lbName.Content = "Add User";
                else
                    lbName.Content = curUser.name;
                switch (curUser.accessLevel)
                {
                    case 0:
                        lbAccessLevel.Content = "...";
                        break;
                    case 1:
                        lbAccessLevel.Content = "操作员";
                        break;
                    case 2:
                        lbAccessLevel.Content = "工艺员";
                        break;
                    case 3:
                        lbAccessLevel.Content = "经理";
                        break;
                    case 4:
                        lbAccessLevel.Content = "Service";
                        break;
                    case 5:
                        lbAccessLevel.Content = "Root";
                        break;
                }
                lbFocus.Content = "";
                focus = false;
            }
            this.Tag = tag;
        }
        public userMsg(userClass user)
        {
            InitializeComponent();
            cvsMain.Children.Add(curMsg);
            Canvas.SetTop(curMsg, 40);
            this.curUser = user;
            if (curUser != null)
            {
                if (this.curUser == valmoWin.dv.users.nullUser)
                    lbName.Content = "Add User";
                else
                    lbName.Content = curUser.name;
                lbAccessLevel.Content = valmoWin.dv.getCurDis(Users.userTypeName[curUser.accessLevel]);
                lbFocus.Content = "";
                focus = false;
            }
            curMsg.init(user);
        }
        public void refresh()
        {
            if (this.curUser == valmoWin.dv.users.nullUser)
                lbName.Content = "Add User";
            else
                lbName.Content = curUser.name;
            lbAccessLevel.Content = valmoWin.dv.getCurDis(Users.userTypeName[curUser.accessLevel]);
            curMsg.init(user);
        }
        bool _focus = false;
        public bool focus
        {
            get
            {
                return _focus;
            }
            set
            {

                if (value)
                {
                    if (curUser != valmoWin.dv.users.nullUser)
                    {
                        curMsg.show();
                        lbFocus.Content = ">";
                        _focus = value;
                    }
                }
                else
                {
                    curMsg.hide();
                    lbFocus.Content = "";
                    _focus = value;
                }

            }
        }
        public void setUserName(string name)
        {
            this.lbName.Content = name;
        }

        private void lbName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (curUser != valmoWin.dv.users.nullUser)
            {
                bool newState = !focus;
                userSetPanelCtrl.removeAllUserBarFocusHandle();
                focus = newState;
                userSetPanelCtrl.refreshBarsHandle();
            }
            else
            {
                valmoWin.dv.users.addUser();
                userSetPanelCtrl.refreshBarsNewHandle();

            }
        }
    }
}