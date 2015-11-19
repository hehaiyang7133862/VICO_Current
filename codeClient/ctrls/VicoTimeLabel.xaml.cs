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
using System.Windows.Controls.Primitives;
using nsDataMgr;

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// VicoTimeLabel.xaml 的交互逻辑
    /// </summary>
    public partial class VicoTimeLabel : UserControl
    {
        /// <summary>
        /// 控件高度
        /// </summary>
        public double CtrlHeight
        {
            set
            {
                if (value >= 0)
                {
                    lbValue.Height = value;
                }
            }
            get
            {
                return lbValue.Height;
            }
        }
        /// <summary>
        /// 控件宽度
        /// </summary>
        public double CtrlWidth
        {
            set
            {
                if (value > 0)
                {
                    lbValue.Width = value;
                }
            }
            get
            {
                return lbValue.Width;
            }
        }
        private Brush _CtrlBorderBrush;
        /// <summary>
        /// 控件边框颜色
        /// </summary>
        public Brush CtrlBorderBrush
        {
            set
            {
                if (_bIsReadOnly != true)
                {
                    lbValue.BorderBrush = _CtrlBorderBrush = value;
                }
            }
        }
        /// <summary>
        /// 控件背景
        /// </summary>
        public Brush CtrlBackground
        {
            set
            {
                if (_bIsReadOnly != true)
                {
                    lbValue.Background = value;
                }
            }
        }
        private bool _bIsReadOnly = false;
        /// <summary>
        /// 设置是否只读
        /// </summary>
        public bool ReadOnly
        {
            set
            {
                _bIsReadOnly = value;
                if (_bIsReadOnly == true)
                {
                    lbValue.BorderBrush = Brushes.Transparent;
                    lbValue.Background = Brushes.Transparent;
                }
            }
            get
            {
                return _bIsReadOnly;
            }
        }
        /// <summary>
        /// 当前对象
        /// </summary>
        private objUnit _curObj = null;
        /// <summary>
        /// 设置当前对象
        /// </summary>
        public string objName
        {
            set
            {
                objUnit obj = valmoWin.dv.getObj(value);
                if (obj != null)
                {
                    _curObj = obj;
                    _curObj.addHandle(UpdateValue);
                }
                else
                {
                    lbValue.Content = "--";
                }
            }
        }

        public VicoTimeLabel()
        {
            InitializeComponent();
        }

        private void UpdateValue(objUnit obj)
        {
            DateTime dt = new DateTime();

            try
            {
                dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                    Convert.ToInt32((obj.value >> 24) & 0x000000ff),
                    Convert.ToInt32((obj.value >> 16) & 0x000000ff),
                    Convert.ToInt32((obj.value >> 8) & 0x000000ff));
            }
            catch
            {
                dt = DateTime.Now;
            }

            lbValue.Content = dt.Hour.ToString().PadLeft(2, '0') + ":" + dt.Minute.ToString().PadLeft(2, '0');
        }

        private bool bIsMouseDown = false;
        private void lbValue_MouseDown(object sender, MouseButtonEventArgs e)
        {
            bIsMouseDown = true;
        }
        private void lbValue_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (bIsMouseDown && !mainPanelCtrl.bIsMouseMove)
            {
                if (!_bIsReadOnly)
                {
                    if (!valmoWin.dv.checkAccesslevel(_curObj.accessLevel))
                    {
                        return;
                    }

                    lbValue.BorderBrush = new SolidColorBrush(Color.FromArgb(0xff, 0x00, 0xb4, 0xe1));

                    double max = _curObj.vMaxDbl;
                    double min = _curObj.vMinDbl;

                    DateTime dtMax;
                    if (_curObj.vMaxDbl != Int32.MaxValue)
                    {
                        int time = Convert.ToInt32(_curObj.vMaxDbl);
                        dtMax = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                            Convert.ToInt32((time >> 24) & 0x000000ff),
                            Convert.ToInt32((time >> 16) & 0x000000ff),
                            Convert.ToInt32((time >> 8) & 0x000000ff));
                    }
                    else
                    {
                        dtMax = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                            23, 59, 0);
                    }
                    DateTime dtMin;
                    if (_curObj.vMinDbl != Int32.MinValue)
                    {
                        int time = Convert.ToInt32(_curObj.vMinDbl);
                        dtMin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                            Convert.ToInt32((time >> 24) & 0x000000ff),
                            Convert.ToInt32((time >> 16) & 0x000000ff),
                            Convert.ToInt32((time >> 8) & 0x000000ff));
                    }
                    else
                    {
                        dtMin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                            0, 0, 0);
                    }

                    valmoWin.sSetTimeCtrl.Show(ConfirmEvent, cancelEvent, Convert.ToDateTime(lbValue.Content), dtMin, dtMax);
                }

                bIsMouseDown = false;
            }
        }

        private void ConfirmEvent(int date)
        {
            _curObj.setValue(date);

            lbValue.BorderBrush = _CtrlBorderBrush;

            valmoWin.sSetTimeCtrl.Visibility = Visibility.Hidden;
        }

        private void cancelEvent()
        {
            lbValue.BorderBrush = _CtrlBorderBrush;

            valmoWin.sSetTimeCtrl.Visibility = Visibility.Hidden;
        }

        private void lbValue_MouseLeave(object sender, MouseEventArgs e)
        {
            bIsMouseDown = false;
        }
    }
}
