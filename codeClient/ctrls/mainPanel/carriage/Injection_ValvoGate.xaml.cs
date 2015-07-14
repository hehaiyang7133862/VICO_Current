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
    public partial class Injection_ValvoGate : UserControl
    {
        public Injection_ValvoGate()
        {
            InitializeComponent();
        }
        private bool _bIsMouseMove = false;
        private void MBmouseMove(object sender, MouseEventArgs e)
        {
        }

        private void BSMouseMove(object sender, MouseEventArgs e)
        {
        }
    }
}
