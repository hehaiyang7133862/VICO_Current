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
    /// Interaction logic for lbSwitchCtrl.xaml
    /// </summary>
    public partial class lbSwitchCtrl : UserControl
    {
        objUnit curObj;

        public static DependencyProperty disPropety3 = DependencyProperty.Register(
    "dis3",
    typeof(Object),
    typeof(lbSwitchCtrl),
    new PropertyMetadata("",
        new PropertyChangedCallback(OnStateChanged3)
    )
   );

        public static DependencyProperty disPropety2 = DependencyProperty.Register(
            "dis2",
            typeof(Object),
            typeof(lbSwitchCtrl),
            new PropertyMetadata("",
                new PropertyChangedCallback(OnStateChanged2)
            )
        );

        public static DependencyProperty disPropety = DependencyProperty.Register(
            "dis1",
            typeof(Object),
            typeof(lbSwitchCtrl),
            new PropertyMetadata("",
                new PropertyChangedCallback(OnStateChanged1)
            )
        );

        private static void OnStateChanged1(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as lbSwitchCtrl)._dis1 = e.NewValue.ToString();
        }

        private static void OnStateChanged2(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as lbSwitchCtrl)._dis2 = e.NewValue.ToString();
        }

        private static void OnStateChanged3(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as lbSwitchCtrl)._dis3 = e.NewValue.ToString();
        }

        string _dis1;
        public string dis1
        {
            set
            {
                _dis1 = value;
            }
        }
        string _dis2;
        public string dis2
        {
            set
            {
                _dis2 = value;
            }
        }
        string _dis3;
        public string dis3
        {
            set
            {
                _dis3 = value;
            }
        }

        public lbSwitchCtrl()
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
                    lbMain.Content = _dis1;
                    break;
                case 1:
                    lbMain.Content = _dis2;
                    break;
                case 2:
                    lbMain.Content = _dis3;
                    break;
            }
        }

    }
}
