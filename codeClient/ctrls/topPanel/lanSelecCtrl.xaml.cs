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
    /// Interaction logic for lanSelecCtrl.xaml
    /// </summary>
    public partial class lanSelecCtrl : UserControl
    {
        public lanSelecCtrl()
        {
            InitializeComponent();
            this.Visibility = Visibility.Hidden;
        }

        public void show()
        {
            this.Visibility = Visibility.Visible;
        }

        private void Lan_cn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            lb_cn.Foreground = Brushes.Blue;
        }

        private void Lan_cn_MouseLeave(object sender, MouseEventArgs e)
        {
            lb_cn.Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0x44, 0x44, 0x44));
        }

        private void Lan_cn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            lb_cn.Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0x44, 0x44, 0x44));

            Common.lan = lanType.lanCN;
            valmoWin.execHandle(opeOrderType.lanChange);
            this.Visibility = Visibility.Hidden;
        }

        private void Lan_us_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            lb_us.Foreground = Brushes.Blue;
        }

        private void Lan_us_MouseLeave(object sender, MouseEventArgs e)
        {
            lb_us.Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0x44, 0x44, 0x44));
        }

        private void Lan_us_MouseUp(object sender, MouseButtonEventArgs e)
        {
            lb_us.Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0x44, 0x44, 0x44));

            Common.lan = lanType.lanEN;
            valmoWin.execHandle(opeOrderType.lanChange);
            this.Visibility = Visibility.Hidden;
        }
    }
}
