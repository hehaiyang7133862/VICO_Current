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
    /// Interaction logic for addNewInterPretorCtrl.xaml
    /// </summary>
    public partial class addNewInterPretorCtrl : UserControl
    {
        public addNewInterPretorCtrl()
        {
            InitializeComponent();
            this.Visibility = Visibility.Hidden;
        }

        private void lbCancle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            imgCancel.Visibility = Visibility.Hidden;
            imgCancelDown.Visibility = Visibility.Visible;
        }

        private void lbOK_MouseDown(object sender, MouseButtonEventArgs e)
        {
            imgOk.Visibility = Visibility.Hidden;
            imgOkDown.Visibility = Visibility.Visible;
        }

        private void lbOK_MouseUp(object sender, MouseButtonEventArgs e)
        {
            imgOk.Visibility = Visibility.Visible;
            imgOkDown.Visibility = Visibility.Hidden;
            //interpretorCtrl.addNewInterPretorHandle();
            this.Visibility = Visibility.Hidden;
        }

        private void lbCancle_MouseUp(object sender, MouseButtonEventArgs e)
        {
            imgCancel.Visibility = Visibility.Visible;
            imgCancelDown.Visibility = Visibility.Hidden ;
            this.Visibility = Visibility.Hidden;
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private void lbOK_MouseLeave(object sender, MouseEventArgs e)
        {
            imgOk.Visibility = Visibility.Visible;
            imgOkDown.Visibility = Visibility.Hidden;
        }

        private void lbCancle_MouseLeave(object sender, MouseEventArgs e)
        {
            imgCancel.Visibility = Visibility.Visible;
            imgCancelDown.Visibility = Visibility.Hidden;
        }
    }
}
