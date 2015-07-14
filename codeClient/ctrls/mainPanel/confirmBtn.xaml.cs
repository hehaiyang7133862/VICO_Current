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
    /// Interaction logic for confirmBtn.xaml
    /// </summary>
    public partial class confirmBtn : UserControl
    {
        MouseButtonEventHandler _downHandle;
        MouseButtonEventHandler _upHandle;
        MouseEventHandler _leaveHandle;
        public confirmBtn()
        {
            InitializeComponent();
        }
        public MouseButtonEventHandler downHandle
        {
            get
            {
                return _downHandle;
            }
            set
            {
                _downHandle = value;
            }
        }
        public MouseButtonEventHandler upHandle
        {
            get
            {
                return _upHandle;
            }
            set
            {
                _upHandle = value;
            }
        }
        public MouseEventHandler leaveHandle
        {
            get
            {
                return _leaveHandle;
            }
            set
            {
                _leaveHandle = value;
            }
        }
        private void img_MouseDown(object sender, MouseButtonEventArgs e)
        {
            img.Opacity = 0;
            if (_downHandle != null)
                _downHandle(sender, e);
        }

        private void img_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (img.Opacity == 0)
            {
                img.Opacity = 1;
                if (_upHandle != null)
                {
                    _upHandle(sender, e);
                }
            }
        }

        private void img_MouseLeave(object sender, MouseEventArgs e)
        {
            if (img.Opacity == 0)
            {
                img.Opacity = 1;
                if (_leaveHandle != null)
                {
                    _leaveHandle(sender, e);
                }

            }
        }
    }
}
