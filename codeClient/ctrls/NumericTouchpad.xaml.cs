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
using System.Windows.Threading;
using nsDataMgr;

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// NumericTouchpad.xaml 的交互逻辑
    /// </summary>
    public partial class NumericTouchpad : UserControl
    {
        private Brush bMouseDown = new SolidColorBrush(Color.FromRgb(0x65, 0x65, 0x65));

        private double max;
        private double min;
        private double FtOffset;
        private nullEvent Dispose;
        private VoidDblEvent Confirm;

        private string _curValue;
        public string CurrentValue
        {
            set
            {
                _curValue = value;
                lbActValue.Content = _curValue;

                double curValue = Convert.ToDouble(_curValue);
                if (curValue > max)
                {
                    curValue = max;
                }
                if (curValue < min)
                {
                    curValue = min;
                }

                if (max != min)
                {
                    lPer.Y2 = (curValue - min) / (max - min) * 150;
                    Canvas.SetBottom(ePer, 405 + lPer.Y2);
                }
            }
            get
            {
                return _curValue;
            }
        }

        private bool bIsChkInput = false;
        private bool bIsNewValue = false;
        private int _flagMath = 0;
        public int Flag
        {
            set
            {
                _flagMath = value;

                btnSub.BorderBrush = Brushes.Transparent;
                btnAdd.BorderBrush = Brushes.Transparent;

                if (_flagMath == 1)
                {
                    btnAdd.BorderBrush = new SolidColorBrush(Color.FromRgb(250, 140, 17));
                }
                else if (_flagMath == 2)
                {
                    btnSub.BorderBrush = new SolidColorBrush(Color.FromRgb(250, 140, 17));
                }
            }
            get
            {
                return _flagMath;
            }
        }

        public NumericTouchpad()
        {
            InitializeComponent();

            this.Visibility = Visibility.Hidden;
        }

        public void init(objUnit obj, nullEvent disposeHandle,VoidDblEvent cfHandle)
        {
            Dispose = disposeHandle;
            Confirm = cfHandle;

            Flag = 0;

            if (valmoWin.dv.users.curUser.accessLevel >= 4)
            {
                lbSerNum.Visibility = Visibility.Visible;
            }
            else
            {
                lbSerNum.Visibility = Visibility.Hidden;
            }
            lbSerNum.Content = obj.serialNum;

            object objDis = App.Current.TryFindResource(obj.serialNum);
            if (objDis == null)
            {
                lbTitle.Content = "对象未定义说明";
            }
            else
            {
                lbTitle.Content = objDis;
            }

            max = obj.vMaxDbl;
            lbMax.Content = obj.vMaxStr;
            lbMax.Foreground = Brushes.White;

            min = obj.vMinDbl;
            lbMin.Content = obj.vMinStr;
            lbMin.Foreground = Brushes.White;

            CurrentValue = obj.vDblStr;
            lbPreValue.Content = CurrentValue;
            lbActValue.Content = CurrentValue;

            lbUint.Content = obj.unit;

            FtOffset = obj.getPrecision();
            btnAdd_FT.Content = "+" + FtOffset;
            btnSub_FT.Content = "-" + FtOffset;

            bIsNewValue = true;

            show();
        }

        public void init(objUnit obj, string dis, nullEvent disposeHandle, VoidDblEvent cfHandle,bool InputChk)
        {
            bIsChkInput = InputChk;

            Dispose = disposeHandle;
            Confirm = cfHandle;

            Flag = 0;

            if (valmoWin.SCurUser.accessLevel > 4)
            {
                lbSerNum.Visibility = Visibility.Visible;
            }
            else
            {
                lbSerNum.Visibility = Visibility.Hidden;
            }
            lbSerNum.Content = obj.serialNum;

            lbTitle.Content = dis;

            max = obj.vMaxDbl;
            lbMax.Content = obj.vMaxStr;
            lbMax.Foreground = Brushes.White;

            min = obj.vMinDbl;
            lbMin.Content = obj.vMinStr;
            lbMin.Foreground = Brushes.White;

            CurrentValue = obj.vDblStr;
            lbPreValue.Content = CurrentValue;
            lbActValue.Content = CurrentValue;

            lbUint.Content = obj.unit;

            FtOffset = obj.getPrecision();
            btnAdd_FT.Content = "+" + FtOffset;
            btnSub_FT.Content = "-" + FtOffset;

            bIsNewValue = true;

            show();
        }

        public void init(objUnit obj,string dis, nullEvent disposeHandle, VoidDblEvent cfHandle)
        {
            Dispose = disposeHandle;
            Confirm = cfHandle;

            Flag = 0;

            if (valmoWin.SCurUser.accessLevel > 4)
            {
                lbSerNum.Visibility = Visibility.Visible;
            }
            else
            {
                lbSerNum.Visibility = Visibility.Hidden;
            }
            lbSerNum.Content = obj.serialNum;

            lbTitle.Content = dis;

            max = obj.vMaxDbl;
            lbMax.Content = obj.vMaxStr;
            lbMax.Foreground = Brushes.White;

            min = obj.vMinDbl;
            lbMin.Content = obj.vMinStr;
            lbMin.Foreground = Brushes.White;

            CurrentValue = obj.vDblStr;
            lbPreValue.Content = CurrentValue;
            lbActValue.Content = CurrentValue;

            lbUint.Content = obj.unit;

            FtOffset = obj.getPrecision();
            btnAdd_FT.Content = "+" + FtOffset;
            btnSub_FT.Content = "-" + FtOffset;

            bIsNewValue = true;

            show();
        }
        
        public void init(double imax,double imin, string dis,string curValue, string unit, double offset, nullEvent disposeHandle, VoidDblEvent cfHandle)
        {
            Dispose = disposeHandle;
            Confirm = cfHandle;

            Flag = 0;

            if (valmoWin.SCurUser.accessLevel > 4)
            {
                lbSerNum.Visibility = Visibility.Visible;
            }
            else
            {
                lbSerNum.Visibility = Visibility.Hidden;
            }
            lbSerNum.Content = "";

            lbTitle.Content = dis;

            max = imax;
            lbMax.Content = max.ToString();
            lbMax.Foreground = Brushes.White;

            min = imin;
            lbMin.Content = min.ToString();
            lbMin.Foreground = Brushes.White;

            CurrentValue = curValue;
            lbPreValue.Content = CurrentValue;
            lbActValue.Content = CurrentValue;

            lbUint.Content = unit;

            FtOffset = offset;
            btnAdd_FT.Content = "+" + FtOffset;
            btnSub_FT.Content = "-" + FtOffset;

            bIsNewValue = true;

            show();
        }

        public void show()
        {
            this.Visibility = Visibility.Visible;

            trySetCtrlPos(mainPanelCtrl.PointMouseDown);
        }

        public void trySetCtrlPos(Point pos)
        {
            if (pos.X > 540)
            {
                pos.X -= 447;
            }

            if (pos.Y< 362)
            {
                pos.Y = 0;
            }
            else if (pos.Y > valmoWin.MainPanelHeight - 362)
            {
                pos.Y = valmoWin.MainPanelHeight - 724;
            }
            else
            {
                pos.Y -= 362;
            }

            Canvas.SetLeft(cvsBg, pos.X);
            Canvas.SetTop(cvsBg, pos.Y);
        }

        private bool bIsBtnDown = false;
        private void btnMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            if (sender.GetType() == typeof(Label))
            {
                (sender as Label).Background = bMouseDown;

                bIsBtnDown = true;
            }
        }
        private void btnMouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            if (sender.GetType() == typeof(Label))
            {
                (sender as Label).Background = Brushes.Transparent;

                if (bIsMove == true || bIsBtnDown == false)
                {
                    return;
                }

                bIsBtnDown = false;

                string strBtn = (sender as Label).Name;

                switch (strBtn)
                {
                    case "btn0":
                    case "btn1":
                    case "btn2":
                    case "btn3":
                    case "btn4":
                    case "btn5":
                    case "btn6":
                    case "btn7":
                    case "btn8":
                    case "btn9":
                        if (bIsNewValue != true)
                        {
                            if (CurrentValue == "0")
                            {
                                CurrentValue = strBtn.Substring(3, 1);
                            }
                            else
                            {
                                if (CurrentValue.Length < 8)
                                {
                                    CurrentValue += strBtn.Substring(3, 1);
                                }
                            }
                        }
                        else
                        {
                            bIsNewValue = false;

                            CurrentValue = strBtn.Substring(3, 1);
                        }
                        break;
                    case "btnDot":
                        if (bIsNewValue == true)
                        {
                            bIsNewValue = false;
                            CurrentValue = "0.";
                        }
                        else if (CurrentValue.IndexOf(".") == -1)
                        {
                            bIsNewValue = false;
                            CurrentValue += ".";
                        }
                        break;
                    case "btnBack":
                        bIsNewValue = false;
                        if (CurrentValue.Length == 1)
                        {
                            CurrentValue = "0";
                        }
                        else
                        {
                            if (CurrentValue.Length == 2 && CurrentValue.IndexOf("-") != -1)
                            {
                                CurrentValue = "0";
                            }
                            else
                            {
                                CurrentValue = CurrentValue.Substring(0, CurrentValue.Length - 1);
                            }
                        }
                        break;
                    case "btnSign":
                        bIsNewValue = false;
                        if (CurrentValue.IndexOf("-") == -1)
                        {
                            CurrentValue = "-" + CurrentValue;
                        }
                        else
                        {
                            CurrentValue = CurrentValue.Substring(1, CurrentValue.Length - 1);
                        }
                        break;
                    case "btnAdd":
                        {
                            Flag = 1;
                            bIsNewValue = true;
                        }
                        break;
                    case "btnSub":
                        {
                            Flag = 2;
                            bIsNewValue = true;
                        }
                        break;
                    case "btnAdd_FT":
                        bIsNewValue = false;
                        CurrentValue = (Convert.ToDouble(CurrentValue) + FtOffset).ToString();
                        break;
                    case "btnSub_FT":
                        bIsNewValue = false;
                        CurrentValue = (Convert.ToDouble(CurrentValue) - FtOffset).ToString();
                        break;
                    case "btnClear":
                        bIsNewValue = true;
                        CurrentValue = "0";
                        break;
                    case "btnEqual":
                        if (Flag == 1)
                        {
                            CurrentValue = (Convert.ToDouble(CurrentValue) + Convert.ToDouble(lbPreValue.Content)).ToString();
                            Flag = 0;
                        }
                        else if (Flag == 2)
                        {
                            CurrentValue = (Convert.ToDouble(lbPreValue.Content) - Convert.ToDouble(CurrentValue)).ToString();
                            Flag = 0;
                        }
                        break;
                    case "btnEnter":
                        {
                            double curValue;
                            if (Flag == 1)
                            {
                                curValue = Convert.ToDouble(CurrentValue) + Convert.ToDouble(lbPreValue.Content);
                            }
                            else if (Flag == 2)
                            {
                                curValue = Convert.ToDouble(lbPreValue.Content) - Convert.ToDouble(CurrentValue);
                            }
                            else
                            {
                                curValue = Convert.ToDouble(CurrentValue);
                            }

                            if (curValue > max)
                            {
                                lbMax.Foreground = Brushes.Red;
                                return;
                            }
                            else if (curValue < min)
                            {
                                lbMin.Foreground = Brushes.Red;
                                return;
                            }

                            if (Confirm != null)
                            {
                                Confirm(curValue);
                            }

                            this.Visibility = Visibility.Hidden;
                        }
                        break;
                }
            }
        }
        private void btnMouseLeave(object sender, MouseEventArgs e)
        {
            if (sender.GetType() == typeof(Label))
            {
                (sender as Label).Background = Brushes.Transparent;

                bIsBtnDown = false;
            }
        }

        private bool bIsMouseDown = false;
        private bool bIsMove = false;
        private Point pMouseDown;
        private Point pCurPosition;

        private void cvsOp_MouseDown(object sender, MouseButtonEventArgs e)
        {
            bIsMouseDown = true;

            pMouseDown = pCurPosition = e.GetPosition(cvsMain);
        }

        private void cvsMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (bIsMouseDown == true && bIsBtnDown == false)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point tempPos = e.GetPosition(cvsMain);

                    if ((Math.Abs(tempPos.Y - pMouseDown.Y) > 5) || (Math.Abs(tempPos.X - pMouseDown.X) > 5))
                    {
                        bIsMove = true;
                    }

                    double top = tempPos.Y - pCurPosition.Y + Canvas.GetTop(cvsBg);
                    if (top > 1196)
                    {
                        top = 1196;
                    }
                    if (top < 0)
                    {
                        top = 0;
                    }
                    Canvas.SetTop(cvsBg, top);

                    double left = tempPos.X - pCurPosition.X + Canvas.GetLeft(cvsBg);
                    if (left > 633)
                    {
                        left = 633;
                    }
                    if (left < 0)
                    {
                        left = 0;
                    }
                    Canvas.SetLeft(cvsBg, left);

                    pCurPosition = tempPos;
                }
            }
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            if (bIsMouseDown == false)
            {
                this.Visibility = Visibility.Hidden;

                if (Dispose != null)
                {
                    Dispose();
                }
            }

            bIsMouseDown = false;
            bIsMove = false;
        }

        private void cvsMain_MouseLeave(object sender, MouseEventArgs e)
        {
            e.Handled = true;

            bIsMouseDown = false;
            bIsMove = false;
        }
    }
}
