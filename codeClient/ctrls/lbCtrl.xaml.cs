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
    public partial class lbCtrl : UserControl
    {
        public double myHeight
        {
            set
            {
                if (value > 0)
                {
                    this.Height = value;
                    lbValue.Height = value;
                }
            }
            get
            {
                return lbValue.Height;
            }
        }
        public double myWidth
        {
            set
            {
                if (value > 0)
                {
                    this.Width = value;
                    lbValue.Width = value;
                    Canvas.SetLeft(lbUnit, value);
                }
            }
            get
            {
                return lbValue.Width;
            }
        }
        public FontFamily myFontFamily
        {
            set
            {
                lbValue.FontFamily = value;
            }
            get
            {
                return lbValue.FontFamily;
            }
        }
        public FontWeight myFontWeight
        {
            set
            {
                lbValue.FontWeight = value;
            }
            get
            {
                return lbValue.FontWeight;
            }

        }
        public double myFontSize
        {
            set
            {
                if (value > 0)
                {
                    lbValue.FontSize = value;
                    lbUnit.FontSize = value;
                }
            }
        }
        public HorizontalAlignment myHorizontalAlignment
        {
            get
            {
                return lbValue.HorizontalContentAlignment;
            }
            set
            {
                lbValue.HorizontalContentAlignment = value;
            }
        }
        public VerticalAlignment myVerticalAlignment
        {
            get
            {
                return lbValue.VerticalContentAlignment;
            }
            set
            {
                lbValue.VerticalContentAlignment = value;
            }
        }
        private Brush _myBorderBrush = new SolidColorBrush(Color.FromArgb(0xff, 0xd4, 0xd4, 0xd4));
        public Brush myBorderBursh
        {
            set
            {
                _myBorderBrush = value;
                lbValue.BorderBrush = _myBorderBrush;
            }
        }
        private Thickness _myBorderThickness = new Thickness(1, 1, 1, 1);
        public Thickness myBorderThickness
        {
            set
            {
                _myBorderThickness = value;
            }
        }
        private Brush _myBackground = Brushes.White;
        public Brush myBackground
        {
            set
            {
                _myBackground = value;
                lbValue.Background = _myBackground;
            }
        }
        public Brush myForeground
        {
            set
            {
                lbValue.Foreground = value;
                lbUnit.Foreground = value;
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
                else
                {
                    lbValue.BorderBrush = _myBorderBrush;
                    lbValue.Background = _myBackground;
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

                    _unit = _curObj.unit;
                    if (_unit.Length > 0)
                    {
                        lbUnit.Content = "[" + _unit + "]";
                    }
                    else
                    {
                        lbUnit.Content = null;
                    }
                }
            }
        }
        /// <summary>
        /// 返回当前对象
        /// </summary>
        public objUnit obj
        {
            set
            {
                _curObj = value;

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
            }
            get
            {
                return _curObj;
            }
        }
        private string _unit = string.Empty;
        public Visibility UnitVisiable
        {
            set
            {
                lbUnit.Visibility = value;
            }
            get
            {
                return lbUnit.Visibility;
            }
        }
        public lbCtrl()
        {
            InitializeComponent();

        }

        private void UpdateValue(objUnit obj)
        {
            lbValue.Content = _curObj.vDblStr;

            if (_unit.Length > 0)
            {
                lbUnit.Content = "[" + _unit + "]";
            }
            else
            {
                lbUnit.Content = null;
            }
        }

        public string dis
        {
            set
            {
                lbValue.Content = value;
            }
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
                    if (_curObj != null)
                    {
                        if (!valmoWin.dv.checkAccesslevel(_curObj.accessLevel))
                        {
                            return;
                        }

                        lbValue.BorderBrush = new SolidColorBrush(Color.FromArgb(0xff, 0x00, 0xb4, 0xe1));

                        string description = TryFindResource(_curObj.serialNum).ToString();
                        if (description == null)
                        {
                            description = "对象" + _curObj.serialNum + "未定义描述";
                        }
                        PreValue = _curObj.vDbl;
                        valmoWin.SNumInput.init(_curObj, description, numkeyDisposeFunc, confirmFunc);
                    }
                    else
                    {
                        MessageBox.Show("对象未定义！");
                    }
                }
                bIsMouseDown = false;
            }
        }

        private void lbValue_MouseLeave(object sender, MouseEventArgs e)
        {
            bIsMouseDown = false;
        }
        private void numkeyDisposeFunc()
        {
            lbValue.BorderBrush = _myBorderBrush;
        }
        private double PreValue;
        private void confirmFunc(double newValue)
        {
            lbValue.BorderBrush = _myBorderBrush;

            _curObj.setValue(newValue);
            valmoWin.eventMgr.addParamMsg(_curObj.description, DateTime.Now, PreValue, newValue);
            valmoWin.refresh();

        }
    }
}
