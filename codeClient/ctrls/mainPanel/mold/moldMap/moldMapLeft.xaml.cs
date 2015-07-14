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
    /// moldMapLeft.xaml 的交互逻辑
    /// </summary>
    public partial class moldMapLeft : UserControl
    {
        double map1WidthValue = 0;
        private double map1HeightValue = 0;
        double map1W;
        double map1H2;
        double map1H;
        public bool useAble = false;
        public moldMapLeft()
        {
            InitializeComponent();
            map1H = map2.Height;
            map1H2 = map1H;
            map1W = map2.Width;

            valmoWin.dv.MldPr[226].addHandle(handleMap1Value);
            valmoWin.dv.MldPr[7].addHandle(handleMoldPr_7);

            valmoWin.dv.MldPr[19].addHandle(handleRefreshMap2);
            valmoWin.dv.MldPr[26].addHandle(handleRefreshMap2);
            valmoWin.dv.MldPr[27].addHandle(handleRefreshMap2);
            valmoWin.dv.MldPr[28].addHandle(handleRefreshMap2);
            valmoWin.dv.MldPr[29].addHandle(handleRefreshMap2);
            valmoWin.dv.MldPr[30].addHandle(handleRefreshMap2);
            valmoWin.dv.MldPr[31].addHandle(handleRefreshMap2);
            valmoWin.dv.MldPr[32].addHandle(handleRefreshMap2);
            valmoWin.dv.MldPr[33].addHandle(handleRefreshMap2);
            valmoWin.dv.MldPr[34].addHandle(handleRefreshMap2);
            valmoWin.dv.MldPr[35].addHandle(handleRefreshMap2);
            valmoWin.dv.MldPr[36].addHandle(handleRefreshMap2);
            valmoWin.dv.MldPr[37].addHandle(handleRefreshMap2);
            valmoWin.dv.MldPr[38].addHandle(handleRefreshMap2);

            valmoWin.dv.PrdPr[237].addHandle(MoldCloseCurveRefush);
        }

        private void MoldCloseCurveRefush(objUnit obj)
        {
            int count = obj.value;
            int MaxMoldStroke = valmoWin.dv.MldPr[226].value;
            int MaxSpeed = valmoWin.dv.MldPr[232].value;

            if (MaxMoldStroke == 0 || MaxSpeed == 0)
            {
                return;
            }

            int[] MoldCloseData = new int[count * 3];
            Lasal32.GetData(MoldCloseData, (uint)valmoWin.dv.PrdPr[236].valueNew, count * 12);

            Point[] curveData_Current = new Point[count];
            Point[] curveData_Speed = new Point[count];

            for (int i = 0; i < count; i++)
            {
                double pos = MoldCloseData[i * 3 + 2] * 1.0 / MaxMoldStroke * 10000;
                double current = MoldCloseData[i * 3] * 1.0 / 1000 * 10000;
                double speed = Math.Abs(MoldCloseData[i * 3 + 1]) * 1.0 / MaxSpeed * 10000;

                curveData_Current[i] = new Point((10000 - pos), 10000 - (current + 10000) / 2);
                curveData_Speed[i] = new Point((10000 - pos), 10000 - speed);
            }

            MoldCloseSpeedCurve.refushCurve(curveData_Speed);
            MoldCloseCurrentCurve.refushCurve(curveData_Current);
        }

        private void handleMap1Value(objUnit obj)
        {
            map1MaxPos.Content = obj.vDblStr;
            double value = obj.vDbl;
            map1MaxPos20.Content = obj.getStrValue(obj.vDbl * 0.2);
            map1MaxPos40.Content = obj.getStrValue(obj.vDbl * 0.4);
            map1MaxPos60.Content = obj.getStrValue(obj.vDbl * 0.6);
            map1MaxPos80.Content = obj.getStrValue(obj.vDbl * 0.8);

            map1MaxPos10.Content = obj.getStrValue(obj.vDbl * 0.1);
            map1MaxPos30.Content = obj.getStrValue(obj.vDbl * 0.3);
            map1MaxPos50.Content = obj.getStrValue(obj.vDbl * 0.5);
            map1MaxPos70.Content = obj.getStrValue(obj.vDbl * 0.7);
            map1MaxPos90.Content = obj.getStrValue(obj.vDbl * 0.9);
        }

        private void handleMoldPr_7(objUnit obj)
        {
            double map1WidthValue = valmoWin.dv.MldPr[226].vDbl;
            double curPos = obj.vDbl;
            string posStr = obj.vDblStr;
            lbPosR.Content = posStr;
            if (map1WidthValue != 0)
            {
                double pos = map1W - map1W / map1WidthValue * curPos;
                if (pos > 0)
                {
                    Canvas.SetLeft(cvsCurR, pos + 174);
                }
                else
                {
                    Canvas.SetLeft(cvsCurR, 174);
                }
            }
        }

        private void handleRefreshMap2(objUnit obj)
        {
            refreshMap2();
        }

        private void refreshMap2()
        {
            map1HeightValue = valmoWin.dv.MldPr[232].vDbl;
            map1WidthValue = valmoWin.dv.MldPr[226].vDbl;

            double h6 = valmoWin.dv.MldPr[31].vDbl;
            double h5 = valmoWin.dv.MldPr[30].vDbl;
            double h4 = valmoWin.dv.MldPr[29].vDbl;
            double h3 = valmoWin.dv.MldPr[28].vDbl;
            double h2 = valmoWin.dv.MldPr[27].vDbl;
            double h1 = valmoWin.dv.MldPr[19].vDbl;

            double v6 = valmoWin.dv.MldPr[38].vDbl;
            double v5 = valmoWin.dv.MldPr[37].vDbl;
            double v4 = valmoWin.dv.MldPr[36].vDbl;
            double v3 = valmoWin.dv.MldPr[35].vDbl;
            double v2 = valmoWin.dv.MldPr[34].vDbl;
            double v1 = valmoWin.dv.MldPr[33].vDbl;

            setMap1LinePosition(map2lnH1, h1, v1, h2, v1);
            setMap1LinePosition(map2lnH6, h6, v6, 0, v6);

            setMap1EllipsePosition(imgep1v0, h2, v1);
            setMap1EllipsePosition(imgep1v1, h1, v1);
            setMap1EllipsePosition(imgep5v0, h6, v5);
            setMap1EllipsePosition(imgep6v0, 0, v6);
            setMap1EllipsePosition(imgep6v1, h6, v6);
            //setMap1EllipsePosition(imgep6v6, h1, 0);
            setMap1LinePosition(map2lnV1, h1, 100, h1, -9);
            //setMap1LinePosition(map2lnV1, h1, v1, h1, 0);
            setMap1LinePosition(map2lnV6, h6, v5, h6, v6);
            //Canvas.SetLeft(imgEndLn2, map2lnV1.X1 + 1);
            int num = valmoWin.dv.MldPr[26].value;
            if (num == 3)
            {
                setMap1LinePosition(map2lnH2, h2, v5, h6, v5);

                setMap1LinePosition(map2lnV2, h2, v1, h2, v5);

                setMap1EllipsePosition(imgep2v1, h2, v5);

                imgep2v0.Visibility = Visibility.Hidden;
                imgep3v0.Visibility = Visibility.Hidden;
                imgep3v1.Visibility = Visibility.Hidden;
                imgep4v0.Visibility = Visibility.Hidden;
                imgep4v1.Visibility = Visibility.Hidden;
                imgep5v1.Visibility = Visibility.Hidden;


                map2lnH3.Visibility = Visibility.Hidden;
                map2lnH4.Visibility = Visibility.Hidden;
                map2lnH5.Visibility = Visibility.Hidden;

                map2lnV3.Visibility = Visibility.Hidden;
                map2lnV4.Visibility = Visibility.Hidden;
                map2lnV5.Visibility = Visibility.Hidden;
            }
            else if (num == 4)
            {
                setMap1LinePosition(map2lnH2, h2, v2, h3, v2);
                setMap1LinePosition(map2lnH3, h3, v5, h6, v5);

                setMap1LinePosition(map2lnV2, h2, v1, h2, v2);
                setMap1LinePosition(map2lnV3, h3, v2, h3, v5);

                setMap1EllipsePosition(imgep2v0, h3, v2);
                setMap1EllipsePosition(imgep2v1, h2, v2);
                setMap1EllipsePosition(imgep3v1, h3, v5);

                imgep3v0.Visibility = Visibility.Hidden;
                imgep4v0.Visibility = Visibility.Hidden;
                imgep4v1.Visibility = Visibility.Hidden;
                imgep5v1.Visibility = Visibility.Hidden;

                map2lnH4.Visibility = Visibility.Hidden;
                map2lnH5.Visibility = Visibility.Hidden;

                map2lnV4.Visibility = Visibility.Hidden;
                map2lnV5.Visibility = Visibility.Hidden;
            }
            else if (num == 5)
            {
                setMap1LinePosition(map2lnH2, h2, v2, h3, v2);
                setMap1LinePosition(map2lnH3, h3, v3, h4, v3);
                setMap1LinePosition(map2lnH4, h4, v5, h6, v5);

                setMap1LinePosition(map2lnV3, h3, v2, h3, v3);
                setMap1LinePosition(map2lnV4, h4, v3, h4, v5);
                setMap1LinePosition(map2lnV2, h2, v1, h2, v2);

                setMap1EllipsePosition(imgep2v0, h3, v2);
                setMap1EllipsePosition(imgep2v1, h2, v2);
                setMap1EllipsePosition(imgep3v1, h3, v3);
                setMap1EllipsePosition(imgep3v0, h4, v3);
                setMap1EllipsePosition(imgep4v1, h4, v5);

                imgep4v0.Visibility = Visibility.Hidden;
                imgep5v1.Visibility = Visibility.Hidden;

                map2lnH5.Visibility = Visibility.Hidden;

                map2lnV5.Visibility = Visibility.Hidden;
            }
            else if (num == 6)
            {
                setMap1LinePosition(map2lnH2, h2, v2, h3, v2);
                setMap1LinePosition(map2lnH3, h3, v3, h4, v3);
                setMap1LinePosition(map2lnH4, h4, v4, h5, v4);
                setMap1LinePosition(map2lnH5, h5, v5, h6, v5);

                setMap1LinePosition(map2lnV3, h3, v2, h3, v3);
                setMap1LinePosition(map2lnV4, h4, v3, h4, v4);
                setMap1LinePosition(map2lnV5, h5, v4, h5, v5);
                setMap1LinePosition(map2lnV2, h2, v1, h2, v2);

                setMap1EllipsePosition(imgep2v0, h3, v2);
                setMap1EllipsePosition(imgep2v1, h2, v2);
                setMap1EllipsePosition(imgep3v1, h3, v3);
                setMap1EllipsePosition(imgep3v0, h4, v3);
                setMap1EllipsePosition(imgep4v1, h4, v4);
                setMap1EllipsePosition(imgep4v0, h5, v4);
                setMap1EllipsePosition(imgep5v1, h5, v5);
                setMap1EllipsePosition(imgep5v0, h6, v5);

            }

        }


        private void setMap1LinePosition(Line ln, double x1, double y1, double x2, double y2)
        {
            if (map1WidthValue > 0.0001 || map1WidthValue < -0.0001)
            {
                ln.X1 = (int)(map1W - map1W / map1WidthValue * x1);
                ln.Y1 = (int)(map1H - map1H2 / 100.0 * y1);
                ln.X2 = (int)(map1W - map1W / map1WidthValue * x2);
                ln.Y2 = (int)(map1H - map1H2 / 100.0 * y2);
                ln.Visibility = Visibility.Visible;
            }
        }
        private void setMap1EllipsePosition(Image img, double x, double y)
        {
            if (map1WidthValue > 0.0001 || map1WidthValue < -0.0001)
            {
                Canvas.SetLeft(img, (int)(map1W - map1W / map1WidthValue * x) - 5.5);
                Canvas.SetTop(img, (int)(map1H - map1H2 / 100.0 * y) - 5.5);
                img.Visibility = Visibility.Visible;
            }
        }


        public bool setVisibility
        {
            set
            {
                if (!value)
                {
                    imgZiped.Visibility = Visibility.Visible;
                    imgUnziped.Visibility = Visibility.Hidden;
                }
                else
                {
                    imgZiped.Visibility = Visibility.Hidden;
                    imgUnziped.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
