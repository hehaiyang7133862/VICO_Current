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
    public partial class multiInOneCtrl2 : UserControl
    {
        Label oldLb;
        Thickness oldThickness = new Thickness();
        SolidColorBrush basicColor = new SolidColorBrush(Color.FromRgb(0, 195, 147));
        public multiInOneCtrl2()
        {
            InitializeComponent();
            //valmoWin.addLanRefreshHandle(refreshLan);
            secNum = 4;
        }

        int _secNum = 4;
        public int secNum
        {
            set
            {
                if (value == 3)
                {
                    lb3.BorderThickness = new Thickness(1);
                    lb4.Visibility = Visibility.Hidden;
                    cvsMain.Width = 222;
                    _secNum = 3;
                }
                else if (value == 4)
                {
                    lb3.BorderThickness = new Thickness(1,1,0,1);
                    lb4.BorderThickness = new Thickness(1);
                    lb4.Visibility = Visibility.Visible;
                    cvsMain.Width = 296;
                    _secNum = 4;
                }
                else
                {
                    throw (new Exception("[secNum] Error Num in " + this.Name));
                }
            }
            get
            {
                return _secNum;
            }
        }

        private void lb_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (curObj != null)
            {
                curObj.valueNew = Int32.Parse((sender as Label).Tag.ToString());
            }
        }
        private objUnit curObj;
        public string objName
        {
            set
            {
                curObj = valmoWin.dv.getObj(value);
                if (curObj != null)
                {
                    curObj.addHandle(handleRefresh);
                }
                else
                {
                    throw (new Exception("[multiInOneCtrl2.objName] object " + value + "is null !"));
                }
            }
        }
        public Object dis1
        {
            get;
            set;
        }
        public static DependencyProperty dis1Property = DependencyProperty.Register(
            "dis1",                                                    // Property name
            typeof(Object),                                           // Property type
            typeof(multiInOneCtrl2),                                      // Type of the dependency property provider
            new PropertyMetadata("",                                // 默认的值
            new PropertyChangedCallback(OnUriChanged1)             // Callback invoked on property value has changes
            )
        );

        private static void OnUriChanged1(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            multiInOneCtrl2 ctrl = d as multiInOneCtrl2;
            ctrl.lb1.Content = e.NewValue;
        }

        public Object dis2
        {
            get;
            set;
        }
        public static DependencyProperty dis2Property = DependencyProperty.Register(
            "dis2",                                                    // Property name
            typeof(Object),                                           // Property type
            typeof(multiInOneCtrl2),                                      // Type of the dependency property provider
            new PropertyMetadata("",                                // 默认的值
            new PropertyChangedCallback(OnUriChanged2)             // Callback invoked on property value has changes
            )
        );

        private static void OnUriChanged2(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            multiInOneCtrl2 ctrl = d as multiInOneCtrl2;
            ctrl.lb2.Content = e.NewValue;
        }

        public Object dis3
        {
            get;
            set;
        }
        public static DependencyProperty dis3Property = DependencyProperty.Register(
            "dis3",                                                    // Property name
            typeof(Object),                                           // Property type
            typeof(multiInOneCtrl2),                                      // Type of the dependency property provider
            new PropertyMetadata("",                                // 默认的值
            new PropertyChangedCallback(OnUriChanged3)             // Callback invoked on property value has changes
            )
        );

        private static void OnUriChanged3(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            multiInOneCtrl2 ctrl = d as multiInOneCtrl2;
            ctrl.lb3.Content = e.NewValue;
        }

        public Object dis4
        {
            get;
            set;
        }
        public static DependencyProperty dis4Property = DependencyProperty.Register(
            "dis4",                                                    // Property name
            typeof(Object),                                           // Property type
            typeof(multiInOneCtrl2),                                      // Type of the dependency property provider
            new PropertyMetadata("",                                // 默认的值
            new PropertyChangedCallback(OnUriChanged4)             // Callback invoked on property value has changes
            )
        );

        private static void OnUriChanged4(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            multiInOneCtrl2 ctrl = d as multiInOneCtrl2;
            ctrl.lb4.Content = e.NewValue;
        }


        private void handleRefresh(objUnit obj)
        {
            //lb1.BorderThickness = new Thickness(1, 1, 0, 1);
            //lb2.BorderThickness = new Thickness(1, 1, 0, 1);
            //lb2.BorderThickness = new Thickness(1, 1, 0, 1);
            //lb3.BorderThickness = new Thickness(1, 1, 0, 1);
            if (oldLb != null)
            {
                oldLb.BorderBrush = Brushes.Silver;
                oldLb.BorderThickness = oldThickness;
                oldLb.Background = Brushes.White;
            }

            switch (obj.value)
            {
                case 0:
                    {
                        oldLb = lb1;
                        oldThickness = lb1.BorderThickness;
                        
                    }
                    break;
                case 1:
                    {
                        oldLb = lb2;
                        oldThickness = lb2.BorderThickness;
                    }
                    break;
                case 2:
                    {
                        oldLb = lb3;
                        oldThickness = lb3.BorderThickness;
                    }
                    break;
                case 3:
                    {
                        oldLb = lb4;
                        oldThickness = lb4.BorderThickness;
                    }
                    break;
            }
            oldLb.Background = new SolidColorBrush(Color.FromRgb(234, 234, 234));
            oldLb.BorderBrush = basicColor;
            oldLb.BorderThickness = new Thickness(2);
            
        }
    }
}
