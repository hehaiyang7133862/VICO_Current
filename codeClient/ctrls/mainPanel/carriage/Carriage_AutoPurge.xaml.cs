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
    /// Carriage_AutoPurge.xaml 的交互逻辑
    /// </summary>
    public partial class Carriage_AutoPurge : UserControl
    {
        public Carriage_AutoPurge()
        {
            InitializeComponent();

            valmoWin.dv.KeyPr[9].addHandle(updateAutoPurgeState);
        }

        private bool _bIsMouseMove = false;
        private void MBmouseMove(object sender, MouseEventArgs e)
        {
        }

        private void BSMouseMove(object sender, MouseEventArgs e)
        {
        }

        private void updateAutoPurgeState(objUnit obj)
        {
            lbStart.Background = (obj.value == 1) ?
                new SolidColorBrush(Color.FromArgb(0xFF, 0x3C, 0xE1, 0x00)) :
                new SolidColorBrush(Colors.Transparent);
        }

        private bool _bIsMouseDown = false;
        private void lbStart_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _bIsMouseDown = true;
            valmoWin.dv.KeyPr[9].valueNew = 1;
        }

        private void lbStart_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_bIsMouseDown == true)
            {
                _bIsMouseDown = false;
                valmoWin.dv.KeyPr[9].valueNew = 0;
            }
        }

        private void lbStart_MouseLeave(object sender, MouseEventArgs e)
        {
            if (_bIsMouseDown == true)
            {
                _bIsMouseDown = false;
                valmoWin.dv.KeyPr[9].valueNew = 0;
            }
        }

    }
}
