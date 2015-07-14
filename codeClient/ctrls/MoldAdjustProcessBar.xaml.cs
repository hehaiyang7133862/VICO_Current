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
    /// MoldAdjustProcessBar.xaml 的交互逻辑
    /// </summary>
    public partial class MoldAdjustProcessBar : UserControl
    {   /// <summary>
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
                }
            }
        }
        /// <summary>
        /// 前景色
        /// </summary>
        private SolidColorBrush _forceground = new SolidColorBrush(Color.FromArgb(0xFF, 0x0, 0x82, 0xc3));
        /// <summary>
        /// 设置前景色
        /// </summary>
        public SolidColorBrush ForceGround
        {
            set
            {
                _forceground = value;
                pathMain.Stroke = _forceground;
            }
            get
            {
                return _forceground;
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
                _curValue = value > 100 ? 100 : value;
                double _RotationAngle = _curValue * 360 / 100;
                if (_RotationAngle > 180)
                {
                    arc.IsLargeArc = true;
                }
                else
                {
                    arc.IsLargeArc = false;
                }
                arc.RotationAngle = _RotationAngle;
                arc.Point = new Point(-12 * Math.Sin(_RotationAngle / 180.0 * Math.PI), 12 * Math.Cos(_RotationAngle / 180.0 * Math.PI));
            }
        }

        public MoldAdjustProcessBar()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置前景色
        /// </summary>
        /// <param name="strBrush">色值</param>
        public void setForceGround(string strBrush)
        {
            if (strBrush != null)
            {
                byte a = Convert.ToByte(strBrush.Substring(1, 2));
                byte b = Convert.ToByte(strBrush.Substring(3, 2));
                byte c = Convert.ToByte(strBrush.Substring(5, 2));

                _forceground = new SolidColorBrush(Color.FromArgb(255, a, b, c));
                pathMain.Stroke = _forceground;
            }
        }

        /// <summary>
        /// 回调函数
        /// </summary>
        /// <param name="obj">对象</param>
        private void handleState(objUnit obj)
        {
            rateValue = 100.0 * obj.value / 2000;
        }
    }
}
