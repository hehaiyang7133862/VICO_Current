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
    /// tuneTableCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class tuneTableCtrl : UserControl
    {
         RotateTransform rotateTransform = new RotateTransform(50 * 180 / 3.142);
         SolidColorBrush btnUpColor = new SolidColorBrush(Color.FromRgb(0, 140, 165));
        public tuneTableCtrl()
        {
            InitializeComponent();
            //imgTuneT.RenderTransform = rotateTransform;
        }

        private void lbSetPCur_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lbSetPCur.Background = Brushes.Silver;
        }

        private void lbSetPCur_MouseLeave(object sender, MouseEventArgs e)
        {
            lbSetPCur.Background = btnUpColor;
        }

        private void lbSetPCur_MouseUp(object sender, MouseButtonEventArgs e)
        {
            valmoWin.dv.MldPr[593].valueNew = 1;
            lbSetPCur.Background = btnUpColor;
            
        }

        private void lbSetNCur_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lbSetNCur.Background = Brushes.Silver;
        }

        private void lbSetNCur_MouseLeave(object sender, MouseEventArgs e)
        {
            lbSetNCur.Background = btnUpColor;
        }

        private void lbSetNCur_MouseUp(object sender, MouseButtonEventArgs e)
        {
            lbSetNCur.Background = btnUpColor;
            valmoWin.dv.MldPr[592].valueNew = 1;
        }


    }
}
