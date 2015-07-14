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
using nsVicoClient;
using nsDataMgr;
//using interfaceMgr;
namespace nsVicoClient.ctrls
{
    /// <summary>
    /// Interaction logic for filterUserUnit.xaml
    /// </summary>
    public partial class filterUserUnit : UserControl
    {
        private userClass _curUser;
        /// <summary>
        /// 设置当前用户
        /// </summary>
        public userClass curUser
        {
            set
            {
                _curUser = value;
                lbUserName.Content = _curUser.name;
            }
            get
            {
                return _curUser;
            }
        }

        private bool _bIsChecked = true;
        /// <summary>
        /// 设置是否被选中
        /// </summary>
        public bool bIsChecked
        {
            set
            {
                _bIsChecked = value;
                lbUserName.Foreground = (_bIsChecked == true) ?
                    new SolidColorBrush(Color.FromArgb(255, 30, 225, 0))
                    : Brushes.Silver;
            }
            get
            {
                return _bIsChecked;
            }
        }

        public filterUserUnit()
        {
            InitializeComponent();

            checkBoxCtrl1.checkedChange = this.checkedChange;

        }

        private void checkedChange()
        {
            this.bIsChecked = checkBoxCtrl1.bIsChecked;
        }
        private bool bIsMouseDown = false;
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            bIsMouseDown = true;

            cvsMain.Background = new SolidColorBrush(Color.FromArgb(255, 234, 234, 234));
        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (bIsMouseDown == true)
            {
                cvsMain.Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
                bIsMouseDown = false;
                checkBoxCtrl1.bIsChecked = !_bIsChecked;

                valmoWin.refresh();
            }
        }
    }
}
