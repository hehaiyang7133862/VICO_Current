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
using nsVicoClient;
using nsDataMgr;

namespace nsVicoClient.ctrls
{
    public partial class btnStateCtrl3 : UserControl
    {
        public string objName
        {
            set
            {
                objUnit obj = valmoWin.dv.getObj(value);
                if (obj != null)
                {
                    lbValue.obj = obj;

                    object str = TryFindResource(obj.serialNum);
                    if (str == null)
                    {
                        lbDescription.Content = "对象" + obj.serialNum + "未定义描述";
                    }
                    else
                    {
                        lbDescription.Content = str.ToString();
                    }

                    object img = TryFindResource("k" + obj.serialNum);
                    if (obj != null)
                    {
                        PictureBox.Source = img as BitmapImage;
                    }
                }
                else
                {
                    lbDescription.Content = "对象" + value + "未定义";
                }
            }
        }
        public btnStateCtrl3()
        {
            InitializeComponent();
        }

        public static DependencyProperty disProperty = DependencyProperty.Register(
            "dis",                                                    // Property name
            typeof(Object),                                           // Property type
            typeof(btnSetCtrl),                                      // Type of the dependency property provider
            new PropertyMetadata("",                                // 默认的值
            new PropertyChangedCallback(OnUriChanged)             // Callback invoked on property value has changes
            )
        );

        private static void OnUriChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            btnSetCtrl ctrl = d as btnSetCtrl;

        }
        public Object dis
        {
            get;
            set;
        }
    }
}
