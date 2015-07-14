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
    /// Interaction logic for testLanCtrl.xaml
    /// </summary>
    public partial class testLanCtrl : UserControl
    {
        public testLanCtrl()
        {
            InitializeComponent();
        }
        public static DependencyProperty disProperty = DependencyProperty.Register(
          "dis",                                                    // Property name
          typeof(Object),                                           // Property type
          typeof(testLanCtrl),                                      // Type of the dependency property provider
          new PropertyMetadata("",                                // 默认的值
          new PropertyChangedCallback(OnUriChanged)             // Callback invoked on property value has changes
          )
        );

        private static void OnUriChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as testLanCtrl).lbDis.Content = e.NewValue;
        }



        public object dis
        {
            get
            {
                return lbDis.Content;
            }
            set
            {
                lbDis.Content = value;
            }
        }
    }
}
