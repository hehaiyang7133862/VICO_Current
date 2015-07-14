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
    /// Interaction logic for filterUserCtrl.xaml
    /// </summary>
    public partial class filterUserCtrl : UserControl
    {
        /// <summary>
        /// 当前登录用户可修改的用户列表
        /// </summary>
        private List<userClass> lstUser = new List<userClass>();
        /// <summary>
        /// 之前加载的控件
        /// </summary>
        private List<filterUserUnit> lstFilterUserPre = new List<filterUserUnit>();
        /// <summary>
        /// 当前加载的控件
        /// </summary>
        private List<filterUserUnit> lstFilterUserCur = new List<filterUserUnit>();

        public filterUserCtrl()
        {
            InitializeComponent();

            valmoWin.update += update;

            init();
        }

        private void init()
        {
            lstUser.Clear();
            if (valmoWin.dv.users.curUser.accessLevel > 3)
            {
                lstUser = valmoWin.dv.users.getUserLst(valmoWin.dv.users.curUser.accessLevel);
            }
            else
            {
                lstUser = valmoWin.dv.users.getUserLst(valmoWin.dv.users.mgr.accessLevel);
            }
            lstUser.Add(valmoWin.dv.users.nullUser);


            cvsView.Children.Clear();
            for (int i = 0; i < lstUser.Count; i++)
            {
                filterUserUnit ctrlUserUnit = new filterUserUnit();
                ctrlUserUnit.curUser = lstUser[i];
                lstFilterUserCur.Add(ctrlUserUnit);

                cvsView.Children.Add(ctrlUserUnit);
            }

            lstFilterUserPre.Clear();
            for (int i = 0; i < lstFilterUserCur.Count; i++)
            {
                lstFilterUserPre.Add(lstFilterUserCur[i]);
            }
        }


        private void update()
        {
            lstFilterUserCur.Clear();
            cvsView.Children.Clear();

            lstUser = valmoWin.dv.users.getUserLst(4);
            lstUser.Add(valmoWin.dv.users.nullUser);

            for (int i = 0; i < lstUser.Count; i++)
            {
                int j;
                for (j = 0; j < lstFilterUserPre.Count; j++)
                {
                    if (lstFilterUserPre[j].curUser.name == lstUser[i].name)
                    {
                        filterUserUnit ctrlUserUnit = new filterUserUnit();
                        ctrlUserUnit.curUser = lstUser[i];
                        ctrlUserUnit.checkBoxCtrl1.bIsChecked = lstFilterUserPre[j].bIsChecked;

                        lstFilterUserCur.Add(ctrlUserUnit);
                        cvsView.Children.Add(ctrlUserUnit);
                        break;
                    }
                }
                if (j == lstFilterUserPre.Count)
                {
                    filterUserUnit ctrlUserUnit = new filterUserUnit();
                    ctrlUserUnit.curUser = lstUser[i];

                    lstFilterUserCur.Add(ctrlUserUnit);
                    cvsView.Children.Add(ctrlUserUnit);
                }

            }

            lstFilterUserPre.Clear();
            for (int i = 0; i < lstFilterUserCur.Count; i++)
            {
                lstFilterUserPre.Add(lstFilterUserCur[i]);
            }

            brd.Height = (lstUser.Count + 1) * 40;
        }

        public bool check(string userName)
        {
            for (int i = 0; i < lstUser.Count; i++)
            {
                if (userName == lstUser[i].name)
                {
                    for (int j = 0; j < lstFilterUserCur.Count; j++)
                    {
                        if (lstFilterUserCur[j].curUser.name == lstUser[i].name)
                        {
                            if (lstFilterUserCur[j].bIsChecked == true)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private bool bIsMouseDown = false;

        private void cvsMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            bIsMouseDown = true;
            cvsMain.Background = new SolidColorBrush(Color.FromArgb(255, 234, 234, 234)); 
        }
        private void cvsMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (bIsMouseDown == true)
            {
                update();

                valmoWin.refresh();

                cvsUser.Visibility = Visibility.Visible;
                lbBj.Visibility = Visibility.Visible;

                cvsMain.Background = new SolidColorBrush(Colors.Transparent);
                bIsMouseDown = false;
            }
        }

        private void cvsMain_MouseLeave(object sender, MouseEventArgs e)
        {
            bIsMouseDown = false;
        }

        private void lbBj_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            cvsUser.Visibility = Visibility.Hidden;
            lbBj.Visibility = Visibility.Hidden;
        }
    }
}
