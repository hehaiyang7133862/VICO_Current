using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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
    /// Ejector_Core.xaml 的交互逻辑
    /// </summary>
    public partial class Ejector_Core : UserControl
    {
        public Ejector_Core()
        {
            InitializeComponent();
        }

        private void CoreASelection(object sender, MouseButtonEventArgs e)
        {
            Label lb = (Label)sender;
            valmoWin.dv.MldPr[250].setValue(Convert.ToDouble(lb.Tag));
        }
        private void CoreBSelection(object sender, MouseButtonEventArgs e)
        {
            Label lb = (Label)sender;
            valmoWin.dv.MldPr[263].setValue(Convert.ToDouble(lb.Tag));
        }
        private void CoreCSelection(object sender, MouseButtonEventArgs e)
        {
            Label lb = (Label)sender;
            valmoWin.dv.MldPr[276].setValue(Convert.ToDouble(lb.Tag));
        }
        private void CoreDSelection(object sender, MouseButtonEventArgs e)
        {
            Label lb = (Label)sender;
            valmoWin.dv.MldPr[289].setValue(Convert.ToDouble(lb.Tag));
        }
        private void CoreESelection(object sender, MouseButtonEventArgs e)
        {
            Label lb = (Label)sender;
            valmoWin.dv.MldPr[302].setValue(Convert.ToDouble(lb.Tag));
        }
        private void CoreFSelection(object sender, MouseButtonEventArgs e)
        {
            Label lb = (Label)sender;
            valmoWin.dv.MldPr[315].setValue(Convert.ToDouble(lb.Tag));
        }
    }
}
