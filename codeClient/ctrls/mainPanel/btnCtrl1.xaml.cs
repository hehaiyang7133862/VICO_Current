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
    /// btnCtrl1.xaml 的交互逻辑
    /// </summary>
    public partial class btnCtrl1 : UserControl
    {
        public nullEvent mouseUpHandle
        {
            get;
            set;
        }
        public btnCtrl1()
        {
            InitializeComponent();
        }

        private void cvsMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            imgDown.Opacity = 1;
        }

        private void cvsMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (imgDown.Opacity == 1)
            {
                imgDown.Opacity = 0;
                if (mouseUpHandle != null)
                    mouseUpHandle();
            }
        }

        private void cvsMain_MouseLeave(object sender, MouseEventArgs e)
        {
            imgDown.Opacity = 0;
        }

        public static DependencyProperty disProperty = DependencyProperty.Register(
            "dis",                                                    // Property name
            typeof(Object),                                           // Property type
            typeof(btnCtrl1),                                      // Type of the dependency property provider
            new PropertyMetadata("",                                // 默认的值
            new PropertyChangedCallback(OnUriChanged)             // Callback invoked on property value has changes
            )
        );

        private static void OnUriChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as btnCtrl1).lbDis.Content = e.NewValue;
        }

        public object dis
        {
            set
            {
                lbDis.Content = value;
            }
            get
            {
                return lbDis.Content;
            }
        }

    }
}
