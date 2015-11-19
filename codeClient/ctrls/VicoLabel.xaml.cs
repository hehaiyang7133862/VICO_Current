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
    public partial class VicoLabel : UserControl
    {
        private string _Description = string.Empty;
        public string Description
        {
            set
            {
                _Description = value;
            }
            get
            {
                object des = App.Current.TryFindResource("TP_" + _curObj.serialNum);
                object desOld = App.Current.TryFindResource(_curObj.serialNum);
                string description;

                if (des != null)
                {
                    description = des.ToString();
                }
                else if (desOld != null)
                {
                    description = desOld.ToString();
                }
                else
                {
                    description = _curObj.serialNum;
                }

                return description;
            }
        }
        public double myHeight
        {
            set
            {
                if (value > 0)
                {
                    this.Height = value;
                    lbValue.Height = value;
                    lbUnit.Height = value;
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
        public double UnitFontSize
        {
            set
            {
                if (value > 0)
                {
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
        private Brush _myBorderBrush = Brushes.Transparent;
        public Brush myBorderBrush
        {
            set
            {
                _myBorderBrush = value;
                if (_bIsReadOnly != true)
                {
                    lbValue.BorderBrush = _myBorderBrush;
                }
            }
        }
        private Brush _myBackground = Brushes.White;
        public Brush myBackground
        {
            set
            {
                _myBackground = value;
                if (_bIsReadOnly != true)
                {
                    lbValue.Background = _myBackground;
                }
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

        public void Update(double tempValue)
        {
            _curObj.setValue(tempValue);
        }

        public void callBack()
        {
            Pos = _curObj.vDbl;
        }

        public static readonly DependencyProperty PosProperty =
    DependencyProperty.Register(
        "Pos", typeof(double),
        typeof(VicoLabel),
        new FrameworkPropertyMetadata(0.0,
            OnDescriptionPropertyChanged));


        private static void OnDescriptionPropertyChanged(DependencyObject source,
        DependencyPropertyChangedEventArgs e)
        {
            VicoLabel ctrl = source as VicoLabel;

            ctrl.lbValue.Content = ctrl._curObj.getStrValue((double)e.NewValue);
        }

        public double Pos
        {
            get
            {
                return (double)GetValue(PosProperty);
            }
            set
            {
                SetValue(PosProperty, value);
            }
        }

        public VicoLabel()
        {
            InitializeComponent();
        }

        private void UpdateValue(objUnit obj)
        {
            Pos = obj.vDblNew;

            lbValue.Content = obj.vDblStr;
            lbUnit.Content = _curObj.unit;
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

                        string desc = getDescription(_curObj.serialNum);

                        PreValue = _curObj.vDbl;

                        valmoWin.SNumInput.init(_curObj, desc, numkeyDisposeFunc, confirmFunc, true);
                    }
                    else
                    {
                        MessageBox.Show("对象未定义！");
                    }
                }
                bIsMouseDown = false;
            }
        }

        private string getDescription(string SerialNum)
        {
            if (Description.Length > 0)
            {
                return Description;
            }

            object obj = TryFindResource("TP_" + SerialNum);
            if (obj != null)
            {
                return obj.ToString();
            }

            object obj2 = TryFindResource(SerialNum);
            if (obj2 != null)
            {
                return obj2.ToString();
            }
            return SerialNum + " Undefined!";
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
            valmoWin.eventMgr.addParamMsg(_curObj.serialNum, DateTime.Now, PreValue, newValue);
            valmoWin.refresh();

        }
    }
}
