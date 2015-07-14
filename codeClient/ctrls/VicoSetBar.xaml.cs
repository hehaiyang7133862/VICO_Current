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
    public partial class VicoSetBar : UserControl
    {
        public string objName
        {
            set
            {
                objUnit obj = valmoWin.dv.getObj(value);
                if (obj != null)
                {
                    lbValue.obj = obj;
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

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register(
                "Description", typeof(string),
                typeof(VicoSetBar),
                new FrameworkPropertyMetadata(null,
                    OnDescriptionPropertyChanged));


        private static void OnDescriptionPropertyChanged(DependencyObject source,
        DependencyPropertyChangedEventArgs e)
        {
            VicoSetBar ctrl = source as VicoSetBar;
            ctrl.lbDescription.Content = (string)e.NewValue;
            ctrl.lbValue.Description = (string)e.NewValue;
        }

        public string Description
        {
            get
            {
                return (string)GetValue(DescriptionProperty);
            }
            set
            {
                SetValue(DescriptionProperty, value);
            }
        }
        public Visibility UnitVisiable
        {
            set
            {
                lbValue.UnitVisiable = value;
            }
        }
        public Visibility Value2Visiable
        {
            set
            {
                lbValue2.Visibility = value;
            }
        }
        public double myWidth
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
        public double myHeight
        {
            set
            {
                cvsMain.Height = value;
                lbValue.myHeight = value;
            }
        }
        public bool ReadOnly
        {
            set
            {
                lbValue.ReadOnly = value;
            }
        }

        public VicoSetBar()
        {
            InitializeComponent();
        }

        private void UpdateValue2(objUnit obj)
        {
            lbValue2.Content = obj.vDblStr;
        }
    }
}
