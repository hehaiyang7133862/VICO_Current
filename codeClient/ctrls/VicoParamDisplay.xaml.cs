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
    public partial class VicoParamDisplay : UserControl
    {
        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register(
                "Description", typeof(string),
                typeof(VicoParamDisplay),
                new FrameworkPropertyMetadata(null,
                    OnDescriptionPropertyChanged));


        private static void OnDescriptionPropertyChanged(DependencyObject source,
        DependencyPropertyChangedEventArgs e)
        {
            VicoParamDisplay ctrl = source as VicoParamDisplay;
            ctrl.lbDescription.Content = (string)e.NewValue;
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

        private objUnit _curObj;
        private string _unit = string.Empty;
        public string objName
        {
            set
            {
                objUnit obj = valmoWin.dv.getObj(value);
                if (obj != null)
                {
                    _curObj = obj;
                    _curObj.addHandle(UpdateValue);

                    _unit = _curObj.unit;
                    if (_unit.Length > 0)
                    {
                        lbUnit.Content = "[" + _unit + "]";
                    }
                    else
                    {
                        lbUnit.Content = null;
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

        public VicoParamDisplay()
        {
            InitializeComponent();
        }

        private void UpdateValue(objUnit obj)
        {
            lbValue.Content = _curObj.vDblStr;

            lbUnit.Content = _curObj.unit;
        }
    }
}
