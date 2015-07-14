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
    public partial class btnSetCtrl : UserControl
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
                }
                else
                {
                    lbDescription.Content = "对象" + value + "未定义";
                }
            }
        }
        public string objName2
        {
            set
            {
                objUnit obj = valmoWin.dv.getObj(value);
                if (obj != null)
                {
                    obj.addHandle(UpdateValue2);
                }
            }
        }

        public Visibility UnitVisiable
        {
            set
            {
                lbValue.UnitVisiable = value;
            }
        }
        public Visibility flagNoTiming
        {
            set
            {
                lbValue2.Visibility = value;
            }
        }
        public double w
        {
            set
            {
                cvsMain.Width = value;
            }
            get
            {
                return cvsMain.Width;
            }
        }
        public double h
        {
            set
            {
                cvsMain.Height = value;
                lbValue.myHeight = value;
            }
        }
        public bool readOnly
        {
            set
            {
                lbValue.ReadOnly = value;
            }
        }
        public btnSetCtrl()
        {
            InitializeComponent();
            
        }

        private void UpdateValue2(objUnit obj)
        {
            lbValue2.Content = obj.vDblStr;
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
            ctrl.lbDescription.Content = e.NewValue;

        }
        public Object dis
        {
            get;
            set;
        }
        public bool flagNoUnit
        {
            set
            {
            }
        }
        public bool flagUnitNull
        {
            set
            {
            }
        }
        public bool flagUnitS
        {
            set
            {
                //lbUnit.Width = 45;
                //Canvas.SetLeft(lbUnit, 230);
                //lbCtrl.w = 230;
            }
        }
        public bool flagUnitMin
        {
            set
            {
                //lbUnit.Width = 45;
                //Canvas.SetLeft(lbUnit, 230);
                //lbCtrl.w = 230;
            }
        }
    }
}
