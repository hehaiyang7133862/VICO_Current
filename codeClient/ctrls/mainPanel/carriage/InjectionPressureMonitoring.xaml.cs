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
using System.Globalization;
using nsDataMgr;

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// InjectionPressureMonitoring.xaml 的交互逻辑
    /// </summary>
    public partial class InjectionPressureMonitoring : UserControl
    {
        private List<Point> historyData_Pos_Pressure = new List<Point>();
        public static double InjStroke = 0;
        public static double InjMaxPressure = 0;

        public InjectionPressureMonitoring()
        {
            InitializeComponent();

            valmoWin.dv.InjPr[49].addHandle(UpdateInjStroke);
            valmoWin.dv.InjPr[189].addHandle(UpdateInjMaxPressure);

            valmoWin.dv.PrdPr[252].addHandle(InjectionCurveRefush);
            valmoWin.dv.PrdPr[251].addHandle(InjectionCurveSave);
            valmoWin.dv.InjPr[68].addHandle(handle_Inj068);
        }

        private int startInjLastValue = 0;
        private bool bIsSaveCurve = false;

        private void InjectionCurveSave(objUnit obj)
        {
            if (startInjLastValue == 0)
            {
                if (obj.value == 1)
                {
                    bIsSaveCurve = true;
                }
            }

            startInjLastValue = obj.value;
        }

        private void InjectionCurveRefush(objUnit obj)
        {
            int count = obj.value;

            int stroke = valmoWin.dv.InjPr[49].value;
            int pressrue = valmoWin.dv.InjPr[189].value;

            if (pressrue <= 0 || stroke <= 0)
            {
                return;
            }

            int[] InjectionData = new int[count * 4];
            Lasal32.GetData(InjectionData, (uint)valmoWin.dv.PrdPr[253].valueNew, count * 16);

            Point[] curveData_Pos_Pressure = new Point[count];

            for (int i = 0; i < count; i++)
            {
                double pos = InjectionData[i * 4 + 3] * 1.0 / stroke * 10000;
                double pressure = InjectionData[i * 4 + 1] * 1.0 / pressrue * 10000;

                curveData_Pos_Pressure[i] = new Point(pos, 10000 - pressure);
            }

            lbCurPrs2.Content = ((10000 - refushPoint(Pos2.Pos / InjStroke * 10000, curveData_Pos_Pressure)) / 10000 * InjMaxPressure).ToString("0.00");
            lbCurPrs3.Content = ((10000 - refushPoint(Pos3.Pos / InjStroke * 10000, curveData_Pos_Pressure)) / 10000 * InjMaxPressure).ToString("0.00");
            lbCurPrs4.Content = ((10000 - refushPoint(Pos4.Pos / InjStroke * 10000, curveData_Pos_Pressure)) / 10000 * InjMaxPressure).ToString("0.00");
            lbCurPrs5.Content = ((10000 - refushPoint(Pos5.Pos / InjStroke * 10000, curveData_Pos_Pressure)) / 10000 * InjMaxPressure).ToString("0.00");
            lbCurPrs6.Content = ((10000 - refushPoint(Pos6.Pos / InjStroke * 10000, curveData_Pos_Pressure)) / 10000 * InjMaxPressure).ToString("0.00");

            if (!bIsSaveCurve)
            {
                curve_Pos_Pressure_Current.refushCurve(curveData_Pos_Pressure);

                historyData_Pos_Pressure.Clear();

                for (int i = 0; i < count; i++)
                {
                    historyData_Pos_Pressure.Add(curveData_Pos_Pressure[i]);
                }
            }
            else
            {
                curve_Pos_Pressure_History.SaveCurve(historyData_Pos_Pressure);

                bIsSaveCurve = false;
            }
        }

        private int InjStorkeRaw;
        private void UpdateInjStroke(objUnit obj)
        {
            InjStorkeRaw = obj.value;
            InjStroke = obj.vDbl;

            lbPos10.Content = (InjStroke * 1.0).ToString("0.00");
            lbPos9.Content = (InjStroke * 0.9).ToString("0.00");
            lbPos8.Content = (InjStroke * 0.8).ToString("0.00");
            lbPos7.Content = (InjStroke * 0.7).ToString("0.00");
            lbPos6.Content = (InjStroke * 0.6).ToString("0.00");
            lbPos5.Content = (InjStroke * 0.5).ToString("0.00");
            lbPos4.Content = (InjStroke * 0.4).ToString("0.00");
            lbPos3.Content = (InjStroke * 0.3).ToString("0.00");
            lbPos2.Content = (InjStroke * 0.2).ToString("0.00");
            lbPos1.Content = (InjStroke * 0.1).ToString("0.00");
        }

        private void UpdateInjMaxPressure(objUnit obj)
        {
            InjMaxPressure = obj.vDbl;

            lbPrs10.Content = (InjMaxPressure * 1.2 * 1.0).ToString("0");
            lbPrs9.Content = (InjMaxPressure * 1.2 * 0.9).ToString("0");
            lbPrs8.Content = (InjMaxPressure * 1.2 * 0.8).ToString("0");
            lbPrs7.Content = (InjMaxPressure * 1.2 * 0.7).ToString("0");
            lbPrs6.Content = (InjMaxPressure * 1.2 * 0.6).ToString("0");
            lbPrs5.Content = (InjMaxPressure * 1.2 * 0.5).ToString("0");
            lbPrs4.Content = (InjMaxPressure * 1.2 * 0.4).ToString("0");
            lbPrs3.Content = (InjMaxPressure * 1.2 * 0.3).ToString("0");
            lbPrs2.Content = (InjMaxPressure * 1.2 * 0.2).ToString("0");
            lbPrs1.Content = (InjMaxPressure * 1.2 * 0.1).ToString("0");
        }

        private void handle_Inj068(objUnit obj)
        {
            if (InjStroke > 0)
            {
                Canvas.SetLeft(cvsVP, obj.vDbl / InjStroke * 830);
            }
            else
            {
                Canvas.SetLeft(cvsVP, 0);
            }
        }

        private double refushPoint(double pos,Point[] lst)
        {
            double MaxPos = valmoWin.dv.InjPr[49].value;
            Point p1 = new Point(0, 0);
            Point p2 = new Point(MaxPos, 0);

            foreach (Point p in lst)
            {
                if ((p.X > pos) & (p.X < p2.X))
                {
                    p2 = p;
                }

                if ((p.X < pos) & (p.X > p1.X))
                {
                    p1 = p;
                }
            }

            if ((p1.X == 0) || (p2.X == MaxPos))
            {
                return 10000;
            }
            else
            {
                return Math.Abs(p2.Y - (p2.X - pos) * Math.Abs(p2.Y - p1.Y) / (p2.X - p1.X));
            }
        }
        private double refushPoint(double pos, List<Point> lst)
        {
            Point p1 = new Point(0, 0);
            Point p2 = new Point(valmoWin.dv.InjPr[49].value, 0);

            foreach (Point p in lst)
            {
                if ((p.X > pos) & (p.X < p2.X))
                {
                    p2 = p;
                }

                if ((p.X < pos) & (p.X > p1.X))
                {
                    p1 = p;
                }
            }

            return Math.Abs(p2.Y - (p2.X - pos) * Math.Abs(p2.Y - p1.Y) / (p2.X - p1.X));
        }

        private Border curP;
        private double dVMin = 0;
        private double dVMax = 208;
        private double dHMin = 0;
        private double dHMax = 0;
        private bool bIsMouseDown = false;
        private bool bIsMove = false;
        private Point pCurPosition;
        private Point pMouseDown;
        private VicoLabel curObjX;
        private VicoLabel curObjY;

        private void p_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender.GetType() == typeof(Border))
            {
                curP = (sender as Border);

                bIsMouseDown = true;
                (curP as Border).BorderBrush = Brushes.Red;

                pMouseDown = pCurPosition = e.GetPosition(cvsCurve);
                getLimit((sender as Border).Name);
            }
        }

        private void getLimit(string name)
        {
            switch (name)
            {
                case "Cvs1":
                    {
                        dHMin = 830;
                        dHMax = 830;
                        curObjX = Pos1;
                        curObjY = Pre1;
                    }
                    break;
                case "Cvs2":
                    {
                        dHMin = Canvas.GetLeft(Cvs3);
                        dHMax = Canvas.GetLeft(Cvs1);
                        curObjX = Pos2;
                        curObjY = Pre2;
                    }
                    break;
                case "Cvs3":
                    {
                        dHMin = Canvas.GetLeft(Cvs4);
                        dHMax = Canvas.GetLeft(Cvs2);
                        curObjX = Pos3;
                        curObjY = Pre3;
                    }
                    break;
                case "Cvs4":
                    {
                        dHMin = Canvas.GetLeft(Cvs5);
                        dHMax = Canvas.GetLeft(Cvs3);
                        curObjX = Pos4;
                        curObjY = Pre4;
                    }
                    break;
                case "Cvs5":
                    {
                        dHMin = Canvas.GetLeft(Cvs6);
                        dHMax = Canvas.GetLeft(Cvs4);
                        curObjX = Pos5;
                        curObjY = Pre5;
                    }
                    break;
                case "Cvs6":
                    {
                        dHMin = 0;
                        dHMax = 0;
                        curObjX = Pos6;
                        curObjY = Pre6;
                    }
                    break;
                default:
                    break;
            }
        }

        private void p_MouseMove(object sender, MouseEventArgs e)
        {
            e.Handled = true;

            if (bIsMouseDown == true)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point tmpPosition = e.GetPosition(cvsCurve);

                    if (bIsMove == false)
                    {
                        if ((Math.Abs(tmpPosition.Y - pMouseDown.Y) > 2) || (Math.Abs(tmpPosition.X - pMouseDown.X) > 2))
                        {
                            bIsMove = true;
                        }
                    }

                    double top = tmpPosition.Y - pCurPosition.Y + Canvas.GetTop(curP);
                    if (top > dVMax)
                    {
                        top = dVMax;
                    }
                    if (top < dVMin)
                    {
                        top = dVMin;
                    }
                    Canvas.SetTop(curP, top);

                    double left = tmpPosition.X - pCurPosition.X + Canvas.GetLeft(curP);
                    if (left > dHMax)
                    {
                        left = dHMax;
                    }
                    if (left < dHMin)
                    {
                        left = dHMin;
                    }
                    Canvas.SetLeft(curP, left);

                    pCurPosition = tmpPosition;
                }
            }
        }

        private double tempX;
        private double tempY;
        private void p_MouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            bIsMouseDown = false;
            if (curP != null)
            {
                (curP as Border).BorderBrush = Brushes.Transparent;
            }

            if (bIsMove == true)
            {
                bIsMove = false;

                if ((curObjX != null) && (curObjY != null))
                {
                    curObjX.Update(Canvas.GetLeft(curP) / 830 * InjStroke);
                    curObjY.Update((208 - Canvas.GetTop(curP)) / 208 * InjMaxPressure);
                }

                curP = null;
            }
        }

        private void rollBack()
        {
            if ((curObjX != null) && (curObjY != null))
            {
                curObjX.callBack();
                curObjY.callBack();
            }
        }

        private void p_MouseLeave(object sender, MouseEventArgs e)
        {
            e.Handled = true;

            bIsMouseDown = false;
            if (curP != null)
            {
                (curP as Border).BorderBrush = Brushes.Transparent;
            }

            bIsMove = false;
            if (bIsMove == true)
            {
                bIsMove = false;

                if ((curObjX != null) && (curObjY != null))
                {
                    curObjX.Update(Canvas.GetLeft(curP) / 830 * InjStroke);
                    curObjY.Update((208 - Canvas.GetTop(curP)) / 208 * InjMaxPressure);
                }

                curP = null;
            }
        }
    }

    public class PosConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double stroke = InjectionPressureMonitoring.InjStroke;

            if (stroke > 0)
            {
                return (double)value / stroke * 830;
            }
            else
            {
                return 0;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double stroke = InjectionPressureMonitoring.InjStroke;

            if (stroke > 0)
            {
                return (double)value / 830 * stroke;
            }
            else
            {
                return 0;
            }
        }
    }

    public class PrsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double MaxPressure = InjectionPressureMonitoring.InjMaxPressure;

            if (MaxPressure > 0)
            {
                return 208 - (double)value / MaxPressure * 208;
            }
            else
            {
                return 0;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double MaxPressure = InjectionPressureMonitoring.InjMaxPressure;

            return Math.Round((208 - (double)value) / 208 * MaxPressure, 2);
        }
    }
}
