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

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// DeleteConfirmCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class DeleteConfirmCtrl : UserControl
    {
        private nullEvent _confirmHandle;

        public Visibility Visible
        {
            set
            {
                this.Visibility = value;
            }
        }

        public nullEvent ConfirmHandle
        {
            set
            {
                _confirmHandle = value;
            }
        }

        public DeleteConfirmCtrl()
        {
            InitializeComponent();
        }


        private void lbConfirm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lbConfirm.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xEA, 0xEA, 0xEA));
        }

        private void lbConfirm_MouseUp(object sender, MouseButtonEventArgs e)
        {
            lbConfirm.Background = Brushes.Transparent;

            if (_confirmHandle != null)
            {
                _confirmHandle();
            }

            this.Visibility = Visibility.Hidden;
        }

        private void lbCancel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lbCancel.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xEA, 0xEA, 0xEA));
        }

        private void lbCancel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            lbConfirm.Background = Brushes.Transparent;

            this.Visibility = Visibility.Hidden;
        }

        private void cvsBackground_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}
