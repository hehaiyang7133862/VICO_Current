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
    /// Interaction logic for threeTypeSwitchCtrl.xaml
    /// </summary>
    public partial class threeTypeSwitchCtrl : UserControl
    {
        objUnit curObj;
        public threeTypeSwitchCtrl()
        {
            InitializeComponent();
        }
        public string objName
        {
            set
            {
                curObj = valmoWin.dv.getObj(value);
                if (curObj != null)
                {
                    curObj.addHandle(handleState);
                }
            }
        }
        private void handleState(objUnit obj)
        {
            switch (obj.value)
            {
                case 0:
                    imgL.Visibility = Visibility.Visible;
                    imgM.Visibility = Visibility.Hidden;
                    imgR.Visibility = Visibility.Hidden;
                    break;
                case 1:
                    imgL.Visibility = Visibility.Hidden;
                    imgM.Visibility = Visibility.Visible;
                    imgR.Visibility = Visibility.Hidden;
                    break;
                case 2:
                    imgL.Visibility = Visibility.Hidden;
                    imgM.Visibility = Visibility.Hidden;
                    imgR.Visibility = Visibility.Visible;
                    break;
            }
        }
        public static DependencyProperty dis1Property = DependencyProperty.Register(
            "dis1",                                                    // Property name
            typeof(Object),                                           // Property type
            typeof(threeTypeSwitchCtrl),                                      // Type of the dependency property provider
            new PropertyMetadata("",                                // 默认的值
            new PropertyChangedCallback(OnUriChanged1)             // Callback invoked on property value has changes
            )
        );

        private static void OnUriChanged1(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as threeTypeSwitchCtrl).lbL.Content = e.NewValue;
        }
        public string dis1
        {
            get;
            set;
        }
        public static DependencyProperty dis2Property = DependencyProperty.Register(
            "dis2",                                                   // Property name
            typeof(Object),                                           // Property type
            typeof(threeTypeSwitchCtrl),                              // Type of the dependency property provider
            new PropertyMetadata("",                                // 默认的值
            new PropertyChangedCallback(OnUriChanged2)                // Callback invoked on property value has changes
            )
        );

        private static void OnUriChanged2(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as threeTypeSwitchCtrl).lbM.Content = e.NewValue;
        }
        public string dis2
        {
            get;
            set;
        }
        public static DependencyProperty dis3Property = DependencyProperty.Register(
            "dis3",                                                    // Property name
            typeof(Object),                                           // Property type
            typeof(threeTypeSwitchCtrl),                                      // Type of the dependency property provider
            new PropertyMetadata("",                                // 默认的值
            new PropertyChangedCallback(OnUriChanged3)             // Callback invoked on property value has changes
            )
        );

        private static void OnUriChanged3(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as threeTypeSwitchCtrl).lbR.Content = e.NewValue;
        }
        public string dis3
        {
            get;
            set;
        }

        private void lbL_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (curObj != null)
            {
                curObj.setValue(0);
            }
        }

        private void lbM_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (curObj != null)
            {
                curObj.setValue(1);
            }
        }

        private void lbR_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (curObj != null)
            {
                curObj.setValue(2);
            }
        }
    }
}
