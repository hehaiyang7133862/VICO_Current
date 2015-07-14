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
    /// myMessageBox.xaml 的交互逻辑
    /// </summary>
    public partial class myMessageBox : UserControl
    {
        private nullEvent ConfirmHandle;
        private nullEvent CancelHandle;

        public myMessageBox()
        {
            InitializeComponent();

            this.Visibility = Visibility.Hidden;
        }

        public void Show(string caption, string text, nullEvent Confirm, nullEvent Cancel)
        {
            ConfirmHandle = Confirm;
            CancelHandle = Cancel;

            lbCaption.Content = caption;
            lbText.Content = text;

            this.Visibility = Visibility.Visible;
        }

        private bool _bIsMouseDown = false;

        private void lbConfirm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            _bIsMouseDown = true;
            lbConfirm.Background = new SolidColorBrush(Color.FromArgb(255, 234, 234, 234));
        }

        private void lbConfirm_MouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            if (_bIsMouseDown == true)
            {
                _bIsMouseDown = false;
                lbConfirm.Background = new SolidColorBrush(Colors.Transparent);

                if (ConfirmHandle != null)
                {
                    ConfirmHandle();
                }

                this.Visibility = Visibility.Hidden;
            }
        }

        private void lbConfirm_MouseLeave(object sender, MouseEventArgs e)
        {
            e.Handled = true;

            _bIsMouseDown = false;
            lbConfirm.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void lbCancel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            _bIsMouseDown = true;
            lbCancel.Background = new SolidColorBrush(Color.FromArgb(255, 234, 234, 234));
        }

        private void lbCancel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            if (_bIsMouseDown == true)
            {
                _bIsMouseDown = false;
                lbCancel.Background = new SolidColorBrush(Colors.Transparent);

                if (CancelHandle != null)
                {
                    CancelHandle();
                }

                this.Visibility = Visibility.Hidden;
            }
        }

        private void lbCancel_MouseLeave(object sender, MouseEventArgs e)
        {
            e.Handled = true;

            _bIsMouseDown = false;
            lbCancel.Background = new SolidColorBrush(Colors.Transparent);
        }
    }
}
