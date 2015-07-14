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
    /// axisStateItemCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class axisStateItemCtrl : UserControl
    {

        private UnitType _unitType = UnitType.Temp_C;
        /// <summary>
        /// 获取或设置单位
        /// </summary>
        public UnitType unitType
        {
            get
            {
                return _unitType;
            }
            set
            {
                _unitType = value;
                lbUnit.Content = "[" + objUnit.unitBase[_unitType] + "]";
            }
        }
        private double _basicValue = 100;
        /// <summary>
        /// 获取或设置量程
        /// </summary>
        public double basicValue
        {
            get
            {
                return _basicValue;
            }
            set
            {
                _basicValue = value;
            }
        }
        private double _value = 0;
        /// <summary>
        /// 获取或设置控件的当前数量
        /// </summary>
        public double Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                _value = value * 100 / _basicValue;

                pValue.Value = (int)_value;
                if (unitType == UnitType.Temp_C || unitType == UnitType.Temp_F)
                    lbValue.Content = value.ToString("0");
                else if (unitType == UnitType.Per)
                    lbValue.Content = value.ToString("0.0");
            }
        }

        private objUnit _curObj;
        /// <summary>
        /// 设置控件的对象名称
        /// </summary>
        public string objName
        {
            set
            {
                _curObj = valmoWin.dv.getObj(value);
                if (_curObj != null)
                {
                    _curObj.addHandle(handleSetLb);
                }
            }
        }
        public axisStateItemCtrl()
        {
            InitializeComponent();
        }

        public static DependencyProperty disProperty = DependencyProperty.Register(
            "dis",                                                   // Property name
            typeof(Object),                                          // Property type
            typeof(axisStateItemCtrl),                               // Type of the dependency property provider
            new PropertyMetadata("",                                 // 默认的值
            new PropertyChangedCallback(OnUriChanged)                // Callback invoked on property value has changes
            )
        );
        private static void OnUriChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as axisStateItemCtrl).lbDis.Content = e.NewValue;
        }
        /// <summary>
        /// 设置控件的描述信息
        /// </summary>
        public object dis
        {
            set
            {
                lbDis.Content = value;
            }
        }

        private void handleSetLb(objUnit obj)
        {
            this.Value = (int)obj.vDbl;
        }

    }
}
