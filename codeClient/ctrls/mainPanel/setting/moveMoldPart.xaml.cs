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
using nsVicoClient;
using nsDataMgr;

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// Interaction logic for moveMoldPart.xaml
    /// </summary>
    public partial class moveMoldPart : UserControl
    {
        private List<Rectangle> lstRec = new List<Rectangle>();
        private List<Line> lstLine = new List<Line>();
        private Line LineRef;

        public moveMoldPart()
        {
            InitializeComponent();

            valmoWin.dv.MldPr[621].addHandle(refush);
            valmoWin.dv.SysPr[109].addHandle(MoldChanged);

            LineRef = new Line();
            LineRef.Stroke = new SolidColorBrush(Colors.Red);
            LineRef.X1 = 0;
            LineRef.X2 = 400;
            LineRef.Y1 = 400;
            LineRef.SnapsToDevicePixels = true;
            LineRef.ClipToBounds = true;
            LineRef.StrokeThickness = 1;
            DoubleCollection DashArray = new DoubleCollection();
            DashArray.Add(2);
            DashArray.Add(2);
            LineRef.StrokeDashArray = DashArray;
            cvsCurve.Children.Add(LineRef);

            for (int i = 0; i < 11; i++)
            {
                Rectangle rec = new Rectangle();
                rec.Height = 6;
                rec.Width = 6;
                rec.Fill = new SolidColorBrush(Colors.Black);
                cvsCurve.Children.Add(rec);
                lstRec.Add(rec);
                Canvas.SetLeft(rec, i * 40 - 3);
                Canvas.SetBottom(rec, -3);
            }

            for (int i = 0; i < 10; i++)
            {
                Line line = new Line();
                line.Stroke = new SolidColorBrush(Colors.Black);
                line.X1 = i * 40;
                line.X2 = (i + 1) * 40;
                line.SnapsToDevicePixels = true;
                line.ClipToBounds = true;
                line.StrokeThickness = 1;
                lstLine.Add(line);
                cvsCurve.Children.Add(line);
            }

            lstLine[0].Y1 = 400;

            valmoWin.dv.MldPr[170].addHandle(refushPoint1);
            valmoWin.dv.MldPr[171].addHandle(refushPoint2);
            valmoWin.dv.MldPr[172].addHandle(refushPoint3);
            valmoWin.dv.MldPr[173].addHandle(refushPoint4);
            valmoWin.dv.MldPr[174].addHandle(refushPoint5);
            valmoWin.dv.MldPr[175].addHandle(refushPoint6);
            valmoWin.dv.MldPr[176].addHandle(refushPoint7);
            valmoWin.dv.MldPr[177].addHandle(refushPoint8);
            valmoWin.dv.MldPr[178].addHandle(refushPoint9);
            valmoWin.dv.MldPr[179].addHandle(refushPoint10);
        }

        private void MoldChanged(objUnit obj)
        {
            btnUnlock.Visibility = (obj.value == 1) ? Visibility.Visible : Visibility.Hidden;
        }

        private void refush(objUnit obj)
        {
            double cf;


            if (valmoWin.dv.MldPr[184].vDbl > 100)
            {
                cf = obj.vDbl / 100 * valmoWin.dv.MldPr[184].vDbl;
                LineRef.Y2 = 400 - 400 * 100 / valmoWin.dv.MldPr[184].vDbl;
            }
            else
            {
                cf = obj.vDbl;
                LineRef.Y2 = 0;
            }

            CF11.Content = (cf / 10 * 10).ToString("0.0");
            CF10.Content = (cf / 10 * 9).ToString("0.0");
            CF9.Content = (cf / 10 * 8).ToString("0.0");
            CF8.Content = (cf / 10 * 7).ToString("0.0");
            CF7.Content = (cf / 10 * 6).ToString("0.0");
            CF6.Content = (cf / 10 * 5).ToString("0.0");
            CF5.Content = (cf / 10 * 4).ToString("0.0");
            CF4.Content = (cf / 10 * 3).ToString("0.0");
            CF3.Content = (cf / 10 * 2).ToString("0.0");
            CF2.Content = (cf / 10 * 1).ToString("0.0");
        }

        private void refushPoint1(objUnit objValue)
        {
            if (lstRec.Count > 0)
            {
                Canvas.SetTop(lstRec[1], 397 - objValue.vDbl / 100 * 400);
                lstLine[0].Y2 = 400 - objValue.vDbl / 100 * 400;
                lstLine[1].Y1 = 400 - objValue.vDbl / 100 * 400;
            }
        }

        private void refushPoint2(objUnit objValue)
        {
            if (lstRec.Count > 0)
            {
                Canvas.SetTop(lstRec[2], 397 - objValue.vDbl / 100 * 400);
                lstLine[1].Y2 = 400 - objValue.vDbl / 100 * 400;
                lstLine[2].Y1 = 400 - objValue.vDbl / 100 * 400;
            }
        }

        private void refushPoint3(objUnit objValue)
        {
            if (lstRec.Count > 0)
            {
                Canvas.SetTop(lstRec[3], 397 - objValue.vDbl / 100 * 400);
                lstLine[2].Y2 = 400 - objValue.vDbl / 100 * 400;
                lstLine[3].Y1 = 400 - objValue.vDbl / 100 * 400;
            }
        }

        private void refushPoint4(objUnit objValue)
        {
            if (lstRec.Count > 0)
            {
                Canvas.SetTop(lstRec[4], 397 - objValue.vDbl / 100 * 400);
                lstLine[3].Y2 = 400 - objValue.vDbl / 100 * 400;
                lstLine[4].Y1 = 400 - objValue.vDbl / 100 * 400;
            }
        }

        private void refushPoint5(objUnit objValue)
        {
            if (lstRec.Count > 0)
            {
                Canvas.SetTop(lstRec[5], 397 - objValue.vDbl / 100 * 400);
                lstLine[4].Y2 = 400 - objValue.vDbl / 100 * 400;
                lstLine[5].Y1 = 400 - objValue.vDbl / 100 * 400;
            }
        }

        private void refushPoint6(objUnit objValue)
        {
            if (lstRec.Count > 0)
            {
                Canvas.SetTop(lstRec[6], 397 - objValue.vDbl / 100 * 400);
                lstLine[5].Y2 = 400 - objValue.vDbl / 100 * 400;
                lstLine[6].Y1 = 400 - objValue.vDbl / 100 * 400;
            }
        }

        private void refushPoint7(objUnit objValue)
        {
            if (lstRec.Count > 0)
            {
                Canvas.SetTop(lstRec[7], 397 - objValue.vDbl / 100 * 400);
                lstLine[6].Y2 = 400 - objValue.vDbl / 100 * 400;
                lstLine[7].Y1 = 400 - objValue.vDbl / 100 * 400;
            }
        }

        private void refushPoint8(objUnit objValue)
        {
            if (lstRec.Count > 0)
            {
                Canvas.SetTop(lstRec[8], 397 - objValue.vDbl / 100 * 400);
                lstLine[7].Y2 = 400 - objValue.vDbl / 100 * 400;
                lstLine[8].Y1 = 400 - objValue.vDbl / 100 * 400;
            }
        }

        private void refushPoint9(objUnit objValue)
        {
            if (lstRec.Count > 0)
            {
                Canvas.SetTop(lstRec[9], 397 - objValue.vDbl / 100 * 400);
                lstLine[8].Y2 = 400 - objValue.vDbl / 100 * 400;
                lstLine[9].Y1 = 400 - objValue.vDbl / 100 * 400;
            }
        }

        private void refushPoint10(objUnit objValue)
        {
            if (lstRec.Count > 0)
            {
                Canvas.SetTop(lstRec[10], 397 - objValue.vDbl / 100 * 400);
                lstLine[9].Y2 = 400 - objValue.vDbl / 100 * 400;
            }
        }

        private bool _bIsMouseDown = false;
        private Point _curMousePos;

        private void sPanlMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _bIsMouseDown = true;
            _curMousePos = e.GetPosition(cvsBack);
        }

        private void sPanlMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (_bIsMouseDown == true)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point tempMousePos = e.GetPosition(cvsBack);
                    double oldTop = Canvas.GetTop(sPanlMain);
                    double newTop = tempMousePos.Y - _curMousePos.Y + oldTop;

                    if (newTop <= -(2270 - (valmoWin.MainPanelHeight - 175)) - 20)
                        newTop = -(2270 - (valmoWin.MainPanelHeight - 175)) - 20;
                    if (newTop > 0)
                        newTop = 0;
                    Canvas.SetTop(sPanlMain, newTop);
                    _curMousePos = tempMousePos;
                }
            }
        }

        private void sPanlMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _bIsMouseDown = false;
        }

        private void sPanlMain_MouseLeave(object sender, MouseEventArgs e)
        {
            _bIsMouseDown = false;
        }

        private void btnCalc_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            _bIsMouseDown = true;
            btnCalc.Background = new SolidColorBrush(Color.FromRgb(234, 234, 234));
        }

        private void btnCalc_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_bIsMouseDown == true)
            {
                valmoWin.dv.MldPr[620].setValue(0);
            }

            _bIsMouseDown = false;
            btnCalc.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void btnCalc_MouseLeave(object sender, MouseEventArgs e)
        {
            _bIsMouseDown = false;
            btnCalc.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void btnUnlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            valmoWin.dv.SysPr[109].setValue(0);
        }
    }
}
