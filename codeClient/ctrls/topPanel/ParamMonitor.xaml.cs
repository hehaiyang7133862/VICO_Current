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
    /// ParamMonitor.xaml 的交互逻辑
    /// </summary>
    public partial class ParamMonitor : UserControl
    {
        public ParamMonitor()
        {
            InitializeComponent();

            valmoWin.dv.PrdPr[1].addMap();
            valmoWin.dv.PrdPr[2].addMap();
            valmoWin.dv.PrdPr[4].addMap();
            valmoWin.dv.PrdPr[96].addMap();
            valmoWin.dv.PrdPr[171].addMap();
        }

        private bool bIsFolding = true;
        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (bIsFolding == true)
            {
                bIsFolding = false;
                cvsMain.Height = 140;
            }
            else
            {
                bIsFolding = true;
                cvsMain.Height = 86;
            }
        }
    }
}
