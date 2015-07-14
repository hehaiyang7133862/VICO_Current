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
    /// Interaction logic for ellipeRateCtrl.xaml
    /// </summary>
    public partial class ellipeRateCtrl : UserControl
    {
        /// <summary>
        /// 对象
        /// </summary>
        private objUnit _curObj;
        /// <summary>
        /// 设置对象
        /// </summary>
        public string objName
        {
            set
            {
                _curObj = valmoWin.dv.getObj(value);
                if (_curObj != null)
                {
                    _curObj.addHandle(handleState);
                    lbValue.obj = _curObj;
                }
            }
        }
        public string objNameBack
        {
            set
            {
                _curObj = valmoWin.dv.getObj(value);
                if (_curObj != null)
                {
                    _curObj.addHandle(handeStateBack);
                }
            }
        }

        public ellipeRateCtrl()
        {
            InitializeComponent();
        }

        public static DependencyProperty disProperty = DependencyProperty.Register(
           "dis",                                                    // Property name
           typeof(Object),                                           // Property type
           typeof(ellipeRateCtrl),                                      // Type of the dependency property provider
           new PropertyMetadata("",                                // 默认的值
           new PropertyChangedCallback(OnUriChanged)             // Callback invoked on property value has changes
           )
       );

        private static void OnUriChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ellipeRateCtrl).lbDis.Content = e.NewValue;
        }
        public object dis
        {
            get
            {
                return lbDis.Content;
            }
            set
            {
                lbDis.Content = value.ToString();
            }
        }
        string curUnit = "";
        public string unit
        {
            set
            {
                curUnit = value;
                lbUnit.Content = value;
            }
        }

        /// <summary>
        /// 当前值
        /// </summary>
        private double _curValue = 0.0;
        /// <summary>
        /// 设置当前值
        /// </summary>
        public double rateValue
        {
            get
            {
                return _curValue;
            }
            set
            {
                _curValue = Math.Abs(value);
                if (_curValue > 100)
                    _curValue = 100;
                double _RotationAngle = _curValue * 360 / 100;
                if (_RotationAngle > 180)
                {
                    arcForeGround.IsLargeArc = true;
                }
                else
                {
                    arcForeGround.IsLargeArc = false;
                }
                arcForeGround.RotationAngle = _RotationAngle;
                arcForeGround.Point = new Point(-40 * Math.Sin(_RotationAngle / 180.0 * Math.PI), 40 * Math.Cos(_RotationAngle / 180.0 * Math.PI));
            }
        }

        private double _curValueBack = 0.0;
        public double rateValueBack
        {
            set
            {
                _curValueBack = Math.Abs(value);
                if (_curValueBack > 100)
                    _curValueBack = 100;
                double _RotationAngle = _curValueBack * 360 / 100;
                if (_RotationAngle > 180)
                {
                    arcBackGround.IsLargeArc = true;
                }
                else
                {
                    arcBackGround.IsLargeArc = false;
                }
                arcBackGround.RotationAngle = _RotationAngle;
                arcBackGround.Point = new Point(-40 * Math.Sin(_RotationAngle / 180.0 * Math.PI), 40 * Math.Cos(_RotationAngle / 180.0 * Math.PI));
            }
        }

        /// <summary>
        /// 回调函数
        /// </summary>
        /// <param name="obj">对象</param>
        private void handleState(objUnit obj)
        {
            rateValue = obj.vDbl;
        }
        private void handeStateBack(objUnit obj)
        {
            rateValueBack = obj.vDbl;
        }
    }
}
