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

namespace nsVicoClient.ctrls.mainPanel
{
    /// <summary>
    /// Interaction logic for sysCloseCtrl.xaml
    /// </summary>
    public partial class sysExitCtrl : UserControl
    {
        public sysExitCtrl()
        {
            InitializeComponent();
            this.Visibility = Visibility.Hidden;
        }

        public void show()
        {
            this.Opacity = 1;
            this.Visibility = Visibility.Visible;
        }
        private void btnOK_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnCancel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}
