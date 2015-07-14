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
    public partial class Carriage_InjUnit : UserControl
    {
        private Point[] BWDSetPoints;
        private Point[] FWDSetPoints;
        private List<Ring> lstPointBWD = new List<Ring>();
        private List<Ring> lstPointFWD = new List<Ring>();
        private List<Line> lstLineBWD = new List<Line>();
        private List<Line> lstLineFWD = new List<Line>();

        public Carriage_InjUnit()
        {
            InitializeComponent();

            for (int i = 0; i < 5; i++)
            {
                Line l = new Line();
                l.Stroke = new SolidColorBrush(Color.FromArgb(0xFF, 0x33, 0x79, 0xE2));
                l.StrokeThickness = 2;
                l.ClipToBounds = true;
                l.Visibility = Visibility.Hidden;
                cvsMap.Children.Add(l);
                lstLineBWD.Add(l);
            } 
            for (int i = 0; i < 5; i++)
            {
                Line l = new Line();
                l.Stroke = new SolidColorBrush(Color.FromArgb(0xFF, 0x33, 0x79, 0xE2));
                l.StrokeThickness = 2;
                l.ClipToBounds = true;
                l.Visibility = Visibility.Hidden;
                cvsMap.Children.Add(l);
                lstLineFWD.Add(l);
            }
            for (int i = 0; i < 6; i++)
            {
                Ring r = new Ring();
                r.Color = new SolidColorBrush(Color.FromArgb(0xFF, 0x33, 0x79, 0xE2));
                r.Visibility = Visibility.Hidden;
                cvsMap.Children.Add(r);
                lstPointFWD.Add(r);
            }
            for (int i = 0; i < 6; i++)
            {
                Ring r = new Ring();
                r.Color = new SolidColorBrush(Color.FromArgb(0xFF, 0x33, 0x79, 0xE2));
                r.Visibility = Visibility.Hidden;
                cvsMap.Children.Add(r);
                lstPointBWD.Add(r);
            }

            valmoWin.dv.InjPr[296].addHandle(handleInjPr_296);
            valmoWin.dv.InjPr[306].addHandle(handleInjPr_306);
            valmoWin.dv.InjPr[307].addHandle(refush);
            valmoWin.dv.InjPr[308].addHandle(refush);
            valmoWin.dv.InjPr[309].addHandle(refush);
            valmoWin.dv.InjPr[310].addHandle(refush);
            valmoWin.dv.InjPr[311].addHandle(refush);
            valmoWin.dv.InjPr[312].addHandle(refush);
            valmoWin.dv.InjPr[297].addHandle(refush);
            valmoWin.dv.InjPr[298].addHandle(refush);
            valmoWin.dv.InjPr[299].addHandle(refush);
            valmoWin.dv.InjPr[300].addHandle(refush);
            valmoWin.dv.InjPr[301].addHandle(refush);
            valmoWin.dv.InjPr[302].addHandle(refush);
            valmoWin.dv.InjPr[278].addHandle(SettingStaff);
        }

        private void SettingStaff(objUnit obj)
        {
            double InjectionUnitMaxStroke = obj.vDbl;
            lbB1.Content = lbF1.Content = (InjectionUnitMaxStroke / 5 * 1).ToString("0.00");
            lbB2.Content = lbF2.Content = (InjectionUnitMaxStroke / 5 * 2).ToString("0.00");
            lbB3.Content = lbF3.Content = (InjectionUnitMaxStroke / 5 * 3).ToString("0.00");
            lbB4.Content = lbF4.Content = (InjectionUnitMaxStroke / 5 * 4).ToString("0.00");
            lbMax.Content = (InjectionUnitMaxStroke / 5 * 5).ToString("0.00");
        }

        private void refush(objUnit obj)
        {
            InitializeBWDSetPoints(valmoWin.dv.InjPr[306].value);
            InitializeFWDSetPoints(valmoWin.dv.InjPr[306].value);
        }

        private void nozzleBWDSec1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.InjPr[306].accessLevel))
                return;
            valmoWin.dv.InjPr[306].setValue(valmoWin.dv.InjPr[306].valueNew >= 2 ? 1 : 2);
        }

        private void nozzleBWDSec2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.InjPr[306].accessLevel))
                return;
            valmoWin.dv.InjPr[306].setValue(valmoWin.dv.InjPr[306].valueNew == 3 ? 2 : 3);
        }

        private void nozzleFWDSec1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.InjPr[296].accessLevel))
                return;
            valmoWin.dv.InjPr[296].setValue(valmoWin.dv.InjPr[296].valueNew >= 2 ? 1 : 2);
        }

        private void nozzleFWDSec2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.InjPr[296].accessLevel))
                return;
            valmoWin.dv.InjPr[296].setValue(valmoWin.dv.InjPr[296].valueNew == 3 ? 2 : 3);
        }

        private void handleInjPr_296(objUnit obj)
        {
            if (obj.value > 3)
                return;
            InitializeFWDSetPoints(obj.value);
            switch (obj.value)
            {
                case 1:
                    bdFWDSec1.Visibility = Visibility.Hidden;
                    bdFWDSec2.Visibility = Visibility.Hidden;
                    btnFWDPos1.Visibility = Visibility.Hidden;
                    btnFWDPos2.Visibility = Visibility.Hidden;
                    btnFWDSpd1.Visibility = Visibility.Hidden;
                    btnFWDSpd2.Visibility = Visibility.Hidden;
                    break;
                case 2:
                    {
                        bdFWDSec1.Visibility = Visibility.Visible;
                        bdFWDSec2.Visibility = Visibility.Hidden;
                        btnFWDPos1.Visibility = Visibility.Visible;
                        btnFWDPos2.Visibility = Visibility.Hidden;
                        btnFWDSpd1.Visibility = Visibility.Visible;
                        btnFWDSpd2.Visibility = Visibility.Hidden;
                    }
                    break;
                case 3:
                    {
                        bdFWDSec1.Visibility = Visibility.Visible;
                        bdFWDSec2.Visibility = Visibility.Visible;
                        btnFWDPos1.Visibility = Visibility.Visible;
                        btnFWDPos2.Visibility = Visibility.Visible;
                        btnFWDSpd1.Visibility = Visibility.Visible;
                        btnFWDSpd2.Visibility = Visibility.Visible;
                    }
                    break;
            }
        }
        private void handleInjPr_306(objUnit obj)
        {
            if (obj.value > 3)
                return;
            InitializeBWDSetPoints(obj.value);

            switch (obj.value)
            {
                case 1:
                    bdBWDSec1.Visibility = Visibility.Hidden;
                    bdBWDSec2.Visibility = Visibility.Hidden;
                    btnBWDPos1.Visibility = Visibility.Hidden;
                    btnBWDPos2.Visibility = Visibility.Hidden;
                    btnBWDSpd1.Visibility = Visibility.Hidden;
                    btnBWDSpd2.Visibility = Visibility.Hidden;
                    break;
                case 2:
                    {
                        bdBWDSec1.Visibility = Visibility.Visible;
                        bdBWDSec2.Visibility = Visibility.Hidden;
                        btnBWDPos1.Visibility = Visibility.Visible;
                        btnBWDPos2.Visibility = Visibility.Hidden;
                        btnBWDSpd1.Visibility = Visibility.Visible;
                        btnBWDSpd2.Visibility = Visibility.Hidden;
                    }
                    break;
                case 3:
                    {
                        bdBWDSec1.Visibility = Visibility.Visible;
                        bdBWDSec2.Visibility = Visibility.Visible;
                        btnBWDPos1.Visibility = Visibility.Visible;
                        btnBWDPos2.Visibility = Visibility.Visible;
                        btnBWDSpd1.Visibility = Visibility.Visible;
                        btnBWDSpd2.Visibility = Visibility.Visible;
                    }
                    break;
            }
        }

        private void InitializeBWDSetPoints(int num)
        {
            int count = num * 2;
            BWDSetPoints = new Point[count];

            if (num >= 2)
            {
                BWDSetPoints[0].X = 0;
                BWDSetPoints[0].Y = valmoWin.dv.InjPr[312].vDbl;
                BWDSetPoints[1].X = valmoWin.dv.InjPr[309].vDbl;
                BWDSetPoints[1].Y = valmoWin.dv.InjPr[312].vDbl;
            }

            if (num >= 3)
            {
                BWDSetPoints[2].X = valmoWin.dv.InjPr[309].vDbl;
                BWDSetPoints[2].Y = valmoWin.dv.InjPr[311].vDbl;
                BWDSetPoints[3].X = valmoWin.dv.InjPr[308].vDbl;
                BWDSetPoints[3].Y = valmoWin.dv.InjPr[311].vDbl;
            }

            BWDSetPoints[count - 1].X = valmoWin.dv.InjPr[307].vDbl;
            BWDSetPoints[count - 1].Y = valmoWin.dv.InjPr[310].vDbl;
            if (num == 1)
            {
                BWDSetPoints[count - 2].X = 0;
            }
            else if (num == 2)
            {
                BWDSetPoints[count - 2].X = valmoWin.dv.InjPr[309].vDbl;
            }
            else if (num == 3)
            {
                BWDSetPoints[count - 2].X = valmoWin.dv.InjPr[308].vDbl;
            }
            else
            {
                BWDSetPoints[count - 2].X = valmoWin.dv.InjPr[307].vDbl;
            }
            BWDSetPoints[count - 2].Y = valmoWin.dv.InjPr[310].vDbl;

            drawBWDSettingLine(BWDSetPoints);
        }
        private void InitializeFWDSetPoints(int num)
        {
            int count = num * 2;
            FWDSetPoints = new Point[count];

            if (num >= 2)
            {
                FWDSetPoints[0].X = 501;
                FWDSetPoints[0].Y = valmoWin.dv.InjPr[300].vDbl;
                FWDSetPoints[1].X = valmoWin.dv.InjPr[297].vDbl;
                FWDSetPoints[1].Y = valmoWin.dv.InjPr[300].vDbl;
            }

            if (num >= 3)
            {
                FWDSetPoints[2].X = valmoWin.dv.InjPr[297].vDbl;
                FWDSetPoints[2].Y = valmoWin.dv.InjPr[301].vDbl;
                FWDSetPoints[3].X = valmoWin.dv.InjPr[298].vDbl;
                FWDSetPoints[3].Y = valmoWin.dv.InjPr[301].vDbl;
            }

            FWDSetPoints[count - 1].X = valmoWin.dv.InjPr[299].vDbl;
            FWDSetPoints[count - 1].Y = valmoWin.dv.InjPr[302].vDbl;
            if (num == 1)
            {
                FWDSetPoints[count - 2].X = 501;
            }
            else if (num == 2)
            {
                FWDSetPoints[count - 2].X = valmoWin.dv.InjPr[297].vDbl;
            }
            else if (num == 3)
            {
                FWDSetPoints[count - 2].X = valmoWin.dv.InjPr[298].vDbl;
            }
            else
            {
                FWDSetPoints[count - 2].X = valmoWin.dv.InjPr[299].vDbl;
            }
            FWDSetPoints[count - 2].Y = valmoWin.dv.InjPr[302].vDbl;

            drawFWDSettingLine(FWDSetPoints);
        }
        private void drawBWDSettingLine(Point[] points)
        {
            double InjectionUnitMaxStroke = valmoWin.dv.InjPr[278].vDbl;

            if (InjectionUnitMaxStroke <= 0)
                return;

            int count = points.Length;

            for (int i = 0; i < count; i++)
            {
                lstPointBWD[i].Visibility = Visibility.Visible;
                Canvas.SetRight(lstPointBWD[i], 410 - points[i].X / InjectionUnitMaxStroke * 415);
                Canvas.SetBottom(lstPointBWD[i], points[i].Y / 100 * 250 - 5);

                if (i != count - 1)
                {
                    lstLineBWD[i].Visibility = Visibility.Visible;
                    Canvas.SetLeft(lstLineBWD[i], 430 + points[i].X / InjectionUnitMaxStroke * 415);
                    Canvas.SetTop(lstLineBWD[i], 250 - (points[i].Y > points[i + 1].Y ? points[i].Y : points[i + 1].Y) / 100 * 250);
                    lstLineBWD[i].X2 = Math.Abs(points[i + 1].X - points[i].X) / InjectionUnitMaxStroke * 415;
                    lstLineBWD[i].Y2 = Math.Abs(points[i + 1].Y - points[i].Y) / 100 * 250;
                }
            }

            for (int i = count; i < 6; i++)
            {
                lstPointBWD[i].Visibility = Visibility.Hidden;

                lstLineBWD[i - 1].Visibility = Visibility.Hidden;
            }
        }
        private void drawFWDSettingLine(Point[] points)
        {
            double InjectionUnitMaxStroke = valmoWin.dv.InjPr[278].vDbl;

            if (InjectionUnitMaxStroke <= 0)
                return;

            int count = points.Length;

            for (int i = 0; i < count; i++)
            {
                lstPointFWD[i].Visibility = Visibility.Visible;
                Canvas.SetLeft(lstPointFWD[i], points[i].X / InjectionUnitMaxStroke * 415 - 5);
                Canvas.SetBottom(lstPointFWD[i], points[i].Y / 100 * 250 - 5);

                if (i != count - 1)
                {
                    lstLineFWD[i].Visibility = Visibility.Visible;
                    Canvas.SetRight(lstLineFWD[i], 845 - points[i].X / InjectionUnitMaxStroke * 415);
                    Canvas.SetTop(lstLineFWD[i], 250 - (points[i].Y > points[i + 1].Y ? points[i].Y : points[i + 1].Y) / 100 * 250);
                    lstLineFWD[i].X2 = Math.Abs(points[i + 1].X - points[i].X) / InjectionUnitMaxStroke * 415;
                    lstLineFWD[i].Y2 = Math.Abs(points[i + 1].Y - points[i].Y) / 100 * 250;
                }
            }

            for (int i = count; i < 6; i++)
            {
                lstPointFWD[i].Visibility = Visibility.Hidden;

                lstLineFWD[i - 1].Visibility = Visibility.Hidden;
            }
        }

        private bool _bIsMouseMove = false;
        private void BSMouseMove(object sender, MouseEventArgs e)
        {
        }
        private void MBmouseMove(object sender, MouseEventArgs e)
        {
        }
    }
}
