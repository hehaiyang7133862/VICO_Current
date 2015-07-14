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
    /// Interaction logic for motionPart.xaml
    /// </summary>
    public partial class motionPart : UserControl
    {
        public motionPart()
        {
            InitializeComponent();
        }


        //归零设定的四个值，直接写将对应地址的值写为1就可以，具体判断由底层plc来处理
        private void lbSysPr_90_MouseDown(object sender, MouseButtonEventArgs e)
        {
            valmoWin.dv.SysPr[90].setValue(1);
        }
        private void lbSysPr_91_MouseDown(object sender, MouseButtonEventArgs e)
        {
            valmoWin.dv.SysPr[91].setValue(1);
        }
        private void lbSysPr_92_MouseDown(object sender, MouseButtonEventArgs e)
        {
            valmoWin.dv.SysPr[92].setValue(1);
        }
        private void lbSysPr_93_MouseDown(object sender, MouseButtonEventArgs e)
        {
            valmoWin.dv.SysPr[93].setValue(1);
        }
        private void lbSysPr_94_MouseDown(object sender, MouseButtonEventArgs e)
        {
            valmoWin.dv.SysPr[94].setValue(1);
        }

        private void lbSysPr_207_MouseDown(object sender, MouseButtonEventArgs e)
        {
            valmoWin.dv.SysPr[207].setValue(1);
        }
    }
}
