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
    /// Interaction logic for btnFunc1.xaml
    /// </summary>
    public partial class btnFunc1 : UserControl
    {
        public MouseButtonEventHandler mouseDownHandle
        {
            get;
            set;
        }
        public MouseButtonEventHandler mouseUpHandle
        {
            get;
            set;
        }
        public MouseEventHandler mouseLeaveHandle
        {
            get;
            set;
        }
        public btnFunc1()
        {
            InitializeComponent();
        }
        bool _downState = false;
        public bool downState
        {
            get
            {
                return _downState;
            }
            set
            {
                if (value)
                {
                    imgUp.Opacity = 0;
                    imgDown.Opacity = 1;
                }
                else
                {
                    imgUp.Opacity = 1;
                    imgDown.Opacity = 0;
                }
                _downState = value;
            }
        }

        public bool focusState
        {
            get
            {
                return lbFocus.Opacity == 1;
            }
            set
            {
                lbFocus.Opacity = value ? 1 : 0;
            }
        }

        private void Dis_MouseDown(object sender, MouseButtonEventArgs e)
        {
            downState = true;
            if (mouseDownHandle != null)
                mouseDownHandle(sender,e);
        }

        private void Dis_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (downState)
            {
                if (mouseUpHandle != null)
                    mouseUpHandle(sender, e);
                downState = false;
            }
        }

        private void Dis_MouseLeave(object sender, MouseEventArgs e)
        {
            if (downState)
            {
                if (mouseLeaveHandle != null)
                    mouseLeaveHandle(sender, e);
                downState = false;
            }
        }
        public MouseButtonEventHandler mouseDown
        {
            get
            {
                return mouseDownHandle;
            }
            set
            {
                mouseDownHandle = value;
            }
        }
        public MouseButtonEventHandler mouseUp
        {
            get
            {
                return mouseUpHandle;
            }
            set
            {
                mouseUpHandle = value;
            }
        }
        public MouseEventHandler mouseLeave
        {
            get
            {
                return mouseLeaveHandle;
            }
            set
            {
                mouseLeaveHandle = value;
            }
        }

        public static DependencyProperty disProperty = DependencyProperty.Register(
         "dis",                                                    // Property name
         typeof(Object),                                           // Property type
         typeof(btnFunc1),                                      // Type of the dependency property provider
         new PropertyMetadata("",                                // 默认的值
         new PropertyChangedCallback(OnUriChanged)             // Callback invoked on property value has changes
         )
     );

        private static void OnUriChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            btnFunc1 ctrl = d as btnFunc1;
            ctrl.Dis.Content = e.NewValue;

        }
        public string dis
        {
            get
            {
                return Dis.Content.ToString();
            }
            set
            {
                Dis.Content = value;
            }
        }
        public void setErrValue()
        {
            imgErr.Opacity = 1;
        }
        public void clearErrValue()
        {
            imgErr.Opacity = 0;
        }
    }
}
