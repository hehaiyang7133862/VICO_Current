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
    /// Interaction logic for btnHidenCtrl.xaml
    /// </summary>
    public partial class btnHidenCtrl : UserControl
    {
        MouseButtonEventHandler btnUpHandle;
        MouseButtonEventHandler btnDownHandle;
        public btnHidenCtrl()
        {
            InitializeComponent();
        }
        public MouseButtonEventHandler mouseUp
        {
            get
            {
                return btnUpHandle;
            }
            set
            {
                btnUpHandle = value;
            }
        }
        public MouseButtonEventHandler mouseDown
        {
            get
            {
                return btnDownHandle;
            }
            set
            {
                btnDownHandle = value;
            }
        }


        private void imgClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            imgCloseDown.Visibility = Visibility.Visible;
            if (btnDownHandle != null)
                btnDownHandle(sender,e);
        }

        private void imgClose_MouseUp(object sender, MouseButtonEventArgs e)
        {
            imgCloseDown.Visibility = Visibility.Hidden;
            if (btnUpHandle != null)
                btnUpHandle(sender, e);
        }
    }
}
