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
    /// Interaction logic for tmpSetBar.xaml
    /// </summary>
    public partial class tmpSetBar : UserControl
    {
        private bool _isMouseDown = false;
        public bool IsMouseDown
        {
            set
            {
                _isMouseDown = value;

                cvsCursor.Visibility = (_isMouseDown == true) ? Visibility.Visible : Visibility.Hidden;
            }
            get
            {
                return _isMouseDown;
            }
        }

        private Point mousePoint;

        public tmpSetBar()
        {
            InitializeComponent();

            valmoWin.dv.TmpPr[10].addHandle(refreshTmpValue);
            valmoWin.dv.TmpPr[18].addHandle(refreshTmpValue);
            valmoWin.dv.TmpPr[26].addHandle(refreshTmpValue);
            valmoWin.dv.TmpPr[34].addHandle(refreshTmpValue);
            valmoWin.dv.TmpPr[42].addHandle(refreshTmpValue);
            valmoWin.dv.TmpPr[50].addHandle(refreshTmpValue);
        }

        private void btnSetting_MouseDown(object sender, MouseButtonEventArgs e)
        {
            IsMouseDown = true;

            mousePoint = e.GetPosition(this.cvsBackWin);
            lbHeatingValue.Content = ((279 - Canvas.GetTop(cvsHeatingLine)) * 400.0 / 253).ToString("0.0");
        }

        private void btnSetting_MouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            if (IsMouseDown == true)
            {
                IsMouseDown = false;
                updateTmpValue();
            }
        }

        private void btnSetting_MouseLeave(object sender, MouseEventArgs e)
        {
            e.Handled = true;

            if (IsMouseDown == true)
            {
                IsMouseDown = false;
                updateTmpValue();
            }
        }

        private void cvsBackWin_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseDown == true)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point theMousePoint = e.GetPosition(this.cvsBackWin);
                    double lnTop = Canvas.GetTop(cvsHeatingLine);
                    double tmpTop = lnTop + theMousePoint.Y - mousePoint.Y;

                    if (tmpTop <= 26)
                        tmpTop = 26;
                    else if (tmpTop >= 279)
                        tmpTop = 279;
                    double value = (279 - tmpTop) * 400.0 / 253;
                    if (value - (int)value > 0.25 && value - (int)value < 0.75)
                        setValue((int)value + 0.5);
                    else if (value - (int)value > 0.75)
                        setValue((int)value + 1);
                    else
                        setValue((int)value);

                    mousePoint = theMousePoint;
                }
            }
        }

        private void cvsBackWin_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (IsMouseDown == true)
            {
                IsMouseDown = false;
            }
        }

        private void cvsBackWin_MouseLeave(object sender, MouseEventArgs e)
        {
            if (IsMouseDown == true)
            {
                IsMouseDown = false;
            }
        }

        public void setSecLenth(int value)
        {
            if (value == 4)
            {
                lnFlag.X2 = 469;
                Canvas.SetLeft(cvsValue, 563);
            }
            else if (value == 5)
            {
                lnFlag.X2 = 571;
                Canvas.SetLeft(cvsValue, 665);
            }
            else
            {
                lnFlag.X2 = 674;
                Canvas.SetLeft(cvsValue, 768);
            }
        }

        private void setValue(double value)
        {
            Canvas.SetTop(cvsHeatingLine, 279 - value * 253.0 / 400);
            lbHeatingValue.Content = value.ToString("0.0");
            imgValueLn.Height = 270 - value * 253.0 / 400;
        }

        private void updateTmpValue()
        {
            double curValue = Double.Parse(lbHeatingValue.Content.ToString());
            valmoWin.dv.TmpPr[10].vDblNew = curValue;
            valmoWin.dv.TmpPr[18].vDblNew = curValue;
            valmoWin.dv.TmpPr[26].vDblNew = curValue;
            valmoWin.dv.TmpPr[34].vDblNew = curValue;
            valmoWin.dv.TmpPr[42].vDblNew = curValue;
            valmoWin.dv.TmpPr[50].vDblNew = curValue;
        }

        private void refreshTmpValue(objUnit obj)
        {
            double max = 0;

            double[] curValue = new double[6];
            curValue[0] = valmoWin.dv.TmpPr[10].vDbl;
            curValue[1] = valmoWin.dv.TmpPr[18].vDbl;
            curValue[2] = valmoWin.dv.TmpPr[26].vDbl;
            curValue[3] = valmoWin.dv.TmpPr[34].vDbl;
            curValue[4] = valmoWin.dv.TmpPr[42].vDbl;
            curValue[5] = valmoWin.dv.TmpPr[50].vDbl;

            max = curValue[0];
            for (int i = 1; i < 6; i++)
            {
                if (max < curValue[i])
                    max = curValue[i];
            }

            setValue(max);
        }
    }
}
