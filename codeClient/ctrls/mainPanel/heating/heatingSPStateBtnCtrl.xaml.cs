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
    /// Interaction logic for heatingSPStateBtnCtrl.xaml
    /// </summary>
    public partial class heatingSPStateBtnCtrl : UserControl
    {
        objUnit curObj;
        public heatingSPStateBtnCtrl()
        {
            InitializeComponent();
        }
        public double h
        {
            set
            {
                if (value > 0)
                    lbMain.Height = value;
            }
            get
            {
                return lbMain.Height;
            }
        }
        public double w
        {
            set
            {
                lbMain.Width = value;
            }
            get
            {
                return lbMain.Width;
            }
        }
        public FontFamily fFamily
        {
            set
            {
                lbMain.FontFamily = value;
            }
            get
            {
                return lbMain.FontFamily;
            }
        }
        public double fSize
        {
            get
            {
                return lbMain.FontSize;
            }
            set
            {
                if (value > 0)
                    lbMain.FontSize = value;
            }
        }
        public string objName
        {
            set
            {
                curObj = valmoWin.dv.getObj(value);
                if (curObj != null)
                {
                    curObj.addHandle(stateHandle);
                }
            }
        }
        public objUnit obj
        {
            get
            {
                return curObj;
            }
        }
        private void stateHandle(objUnit obj)
        {
            switch (obj.value)
            {
                case 0:
                    lbMain.Content = "加热禁用";
                    break;
                case 1:
                    lbMain.Content = "加热启用";
                    break;
                case 2:
                    lbMain.Content = "关闭";
                    break;
                case 3:
                    lbMain.Content = "自动加热";
                    break;
                case 4:
                    lbMain.Content = "比例加热";
                    break;
                case 5:
                    lbMain.Content = "自整定";
                    break;
                case 6:
                    lbMain.Content = "待机";
                    break;
                case 7:
                    lbMain.Content = "加热异常";
                    break;
            }
        }
        public static DependencyProperty disProperty = DependencyProperty.Register(
            "dis",                                                    // Property name
            typeof(Object),                                           // Property type
            typeof(heatingSPStateBtnCtrl),                                      // Type of the dependency property provider
            new PropertyMetadata("",                                // 默认的值
            new PropertyChangedCallback(OnUriChanged)             // Callback invoked on property value has changes
            )
        );

        private static void OnUriChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as heatingSPStateBtnCtrl).lbMain.Content = e.NewValue;
        }
        public string dis
        {
            get
            {
                return lbMain.Content.ToString();
            }
            set
            {
                lbMain.Content = value;
            }
        }
    }
}
