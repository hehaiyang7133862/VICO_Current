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
    /// Maintain.xaml 的交互逻辑
    /// </summary>
    public partial class Maintain : UserControl
    {
        public Maintain()
        {
            InitializeComponent();

            valmoWin.dv.SysPr[391].addHandle(refush1);
            valmoWin.dv.SysPr[394].addHandle(refush2);
            valmoWin.dv.SysPr[397].addHandle(refush3);
            valmoWin.dv.SysPr[400].addHandle(refush4);
        }

        private void refush1(objUnit obj)
        {
            double totalTime = valmoWin.dv.SysPr[390].vDbl + obj.vDbl;
            if (totalTime != 0)
            {
                erMaintain1Cr.rateValue = valmoWin.dv.SysPr[390].vDbl / totalTime * 100;
                lbMaintain1Cr.Content = (valmoWin.dv.SysPr[390].vDbl / totalTime * 100).ToString("0.0");
            }
        }
        private void refush2(objUnit obj)
        {
            double totalTime = valmoWin.dv.SysPr[393].vDbl + obj.vDbl;
            if (totalTime != 0)
            {
                erMaintain2Cr.rateValue = valmoWin.dv.SysPr[393].vDbl / totalTime * 100;
                lbMaintain2Cr.Content = (valmoWin.dv.SysPr[393].vDbl / totalTime * 100).ToString("0.0");
            }
        }
        private void refush3(objUnit obj)
        {
            double totalTime = valmoWin.dv.SysPr[396].vDbl + obj.vDbl;
            if (totalTime != 0)
            {
                erMaintain3Cr.rateValue = valmoWin.dv.SysPr[396].vDbl / totalTime * 100;
                lbMaintain3Cr.Content = (valmoWin.dv.SysPr[396].vDbl / totalTime * 100).ToString("0.0");
            }
        }
        private void refush4(objUnit obj)
        {
            double totalTime = valmoWin.dv.SysPr[399].vDbl + obj.vDbl;
            if (totalTime != 0)
            {
                erMaintain4Cr.rateValue = valmoWin.dv.SysPr[399].vDbl / totalTime * 100;
                lbMaintain4Cr.Content = (valmoWin.dv.SysPr[399].vDbl / totalTime * 100).ToString("0.0");
            }
        }

        private void btnMaintain1_Click(object sender, RoutedEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.SysPr[392].accessLevel))
                return;
            valmoWin.dv.SysPr[392].valueNew = 0;
        }

        private void btnMaintain2_Click(object sender, RoutedEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.SysPr[395].accessLevel))
                return;
            valmoWin.dv.SysPr[395].valueNew = 0;
        }

        private void btnMaintain3_Click(object sender, RoutedEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.SysPr[398].accessLevel))
                return;
            valmoWin.dv.SysPr[398].valueNew = 0;
        }

        private void btnMaintain4_Click(object sender, RoutedEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.SysPr[401].accessLevel))
                return;
            valmoWin.dv.SysPr[401].valueNew = 0;
        }
    }
}
