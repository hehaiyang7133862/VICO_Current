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
using nsDataMgr;
//using interfaceMgr;
namespace nsVicoClient.ctrls
{
    /// <summary>
    /// Interaction logic for mgrSetUserCtrl.xaml
    /// </summary>
    public partial class userSetPanelCtrl : UserControl
    {
        //userMsgSet.MdyUserMsgEvent mdyUserMsgHandle;
        public static  nullEvent removeAllUserBarFocusHandle;
        public static nullEvent refreshBarsHandle;
        public static nullEvent refreshBarsNewHandle;
             

        userMsgSet userMs = new userMsgSet();
        userOwnMsgSet userOms = new userOwnMsgSet();
        userMsgAddSet userAs = new userMsgAddSet();
        //int curListNum = 1;
        //userMsg curUserMsg = null;
        //double topPos = 0;
        List<userMsg> userMsgList = new List<userMsg>();
        public userSetPanelCtrl()
        {
            InitializeComponent();
            //mdyUserMsgHandle = new userMsgSet.MdyUserMsgEvent(mdyUserMsgFunc);
            removeAllUserBarFocusHandle = new nullEvent(removeAllUserBarFocus);
            refreshBarsHandle = new nullEvent(refresh);
            refreshBarsNewHandle = new nullEvent(refreshNew);
        }
        public void init()
        {
            userClass curUser = valmoWin.dv.users.curUser;
            cvsUserPanel.Children.Clear();
            userMsgList.Clear();
            userMsg userBar = new userMsg(curUser);
            cvsUserPanel.Children.Add(userBar);
            List<userClass> lstUser = valmoWin.dv.users.getUserLst();
            if (lstUser.Count > 0)
            {
                for(int i = 0;i< lstUser.Count;i++)
                {
                    if (lstUser[i] != curUser)
                    {
                        userBar = new userMsg(lstUser[i]);
                        cvsUserPanel.Children.Add(userBar);
                        Canvas.SetTop(userBar, cvsUserPanel.Children.Count * 40 - 40);
                    }
                }
            }
            if (! valmoWin.dv.users.checkFull())
            {
                userBar = new userMsg(valmoWin.dv.users.nullUser);
                cvsUserPanel.Children.Add(userBar);
                Canvas.SetTop(userBar, cvsUserPanel.Children.Count * 40 - 40);
            }
        }
        public void refreshNew()
        {
            try
            {
                init();
                return;

                //int focusNum = -1;
                //int childNum = cvsUserPanel.Children.Count;
                //if (((cvsUserPanel.Children[cvsUserPanel.Children.Count - 1]) as userMsg).user == valmoWin.dv.users.nullUser)
                //    childNum -= 1;
                //for (int i = 0; i < cvsUserPanel.Children.Count; i++)
                //    if (((cvsUserPanel.Children[i]) as userMsg).focus)
                //        focusNum = i;
                //userClass curUser = valmoWin.dv.users.curUser;
                //cvsUserPanel.Children.Clear();
                //userMsg userBar = new userMsg(curUser);
                //cvsUserPanel.Children.Add(userBar);
                //List<userClass> lstUser = valmoWin.dv.users.getUserLst();

                //if (lstUser.Count > 0)
                //{
                //    for (int i = 0; i < lstUser.Count; i++)
                //    {
                //        if (lstUser[i] != curUser)
                //        {
                //            userBar = new userMsg(lstUser[i]);
                //            cvsUserPanel.Children.Add(userBar);
                //            //Canvas.SetTop(userBar, cvsUserPanel.Children.Count * 40 - 40);
                //        }
                //    }
                //}
                //if (!valmoWin.dv.users.checkFull())
                //{
                //    userBar = new userMsg(valmoWin.dv.users.nullUser);
                //    cvsUserPanel.Children.Add(userBar);
                //    //Canvas.SetTop(userBar, cvsUserPanel.Children.Count * 40 - 40);
                //}

                //if (childNum == lstUser.Count)
                //{
                //    ((cvsUserPanel.Children[focusNum]) as userMsg).focus = true;
                //}
                //refresh();
            }
            catch
            {

            }
        }
        public void refresh()
        {
            double top = 0;
            if ((cvsUserPanel.Children[0] as userMsg).focus)
            {
                (cvsUserPanel.Children[0] as userMsg).refresh();
                top += 237;
            }
            top += 40;
            for (int i = 1; i < cvsUserPanel.Children.Count; i++)
            {
                userMsg userBar = cvsUserPanel.Children[i] as userMsg;
                Canvas.SetTop(userBar, top);
                if (userBar.focus)
                {
                    userBar.refresh();
                    top += 237;
                }
                top += 40;
            }
        }

        //public void refreshPanel()
        //{
        //    topPos = 0;
        //    userOms.Visibility = Visibility.Hidden;
        //    userMs.Visibility = Visibility.Hidden;
        //    userAs.Visibility = Visibility.Hidden;
        //    //cvsUserPanel.Children.Clear();
        //    for (int i = 0; i < userMsgList.Count; i++)
        //    {
        //        //cvsUserPanel.Children.Add(userMsgList[i]);
        //        userMsgList[i].Margin = new Thickness(0, topPos, 0, 0);
        //        topPos += 40 + 2;

        //        if (userMsgList[i].focus == true)
        //        {
        //            if (userMsgList[i].user == valmoWin.dv.users.nullUser)
        //            {
        //                userAs.Visibility = Visibility.Visible;
        //                userAs.Margin = new Thickness(0, topPos, 0, 0);
        //                topPos += 205 + 2;
        //            }
        //            else if (userMsgList[i].user.name == valmoWin.dv.users.curUser.name)
        //            {
        //                userOms.Visibility = Visibility.Visible;
        //                userOms.init(userMsgList[i].user, mdyUserMsgHandle);
        //                userOms.Margin = new Thickness(0, topPos, 0, 0);
        //                topPos += 155 + 2;
        //            }
        //            else
        //            {
        //                userMs.Visibility = Visibility.Visible;
        //                userMs.init(userMsgList[i].user,mdyUserMsgHandle);
        //                userMs.Margin = new Thickness(0, topPos, 0, 0);
        //                topPos += 237 + 2;
        //            }
        //        }
                
        //    }

        //}

        //bool mdyUserMsgFunc(userObj user,UserHandleType type)
        //{
        //    switch (type)
        //    {
        //        case UserHandleType.DEL:
        //            {
        //                for (int i = 0; i < userMsgList.Count; i++)
        //                {
        //                    if (userMsgList[i].user.name == user.name)
        //                    {
        //                        userMsgList.RemoveAt(i);
        //                    }
        //                }
        //                for (int i = 3; i < cvsUserPanel.Children.Count; i++)
        //                {
        //                    if ((cvsUserPanel.Children[i] as userMsg ).user.name == user.name)
        //                    {
        //                        cvsUserPanel.Children.RemoveAt(i);
        //                    }
        //                }
        //                valmoWin.dv.users.delUser(user);
        //            }
        //            refreshPanel();
        //            curUserMsg = null;
        //            break;
        //        case UserHandleType.NAME:
        //            //curUserMsg.lbName.Content = user.name;
        //            curUserMsg.setUserName(user.name);
        //        break;
        //        case UserHandleType.PASSWORD:
        //            //valmoWin.dv.users.mdyUser(null,user.name,
        //        break;
        //        case UserHandleType.ACCESSLEVEL:
        //        switch (user.accessLevel)
        //        {
        //            case 1:
        //                curUserMsg.lbAccessLevel.Content = "操作员";
        //                break;
        //            case 2:
        //                curUserMsg.lbAccessLevel.Content = "工艺员";
        //                break;
        //            case 3:
        //                curUserMsg.lbAccessLevel.Content = "经理";
        //                break;
        //            case 4:
        //                curUserMsg.lbAccessLevel.Content = "Service";
        //                break;
        //            case 5:
        //                curUserMsg.lbAccessLevel.Content = "Root";
        //                break;
        //        }
        //        break;
        //    }
        //    return false;
        //}
        public void removeAllUserBarFocus()
        {
            for (int i = 0; i < cvsUserPanel.Children.Count; i++)
            {
                (cvsUserPanel.Children[i] as userMsg).focus = false;
            }

        }

    }
}
