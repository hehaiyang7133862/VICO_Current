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
    /// moldMapRight.xaml 的交互逻辑
    /// </summary>
    public partial class moldMapRight : UserControl
    {
        double map1WidthValue = 0;
        private double map1HeightValue = 0;
        double map1W;
        double map1H2;
        double map1H;
        public bool useAble = false;
        public moldMapRight()
        {
            InitializeComponent();

            valmoWin.dv.MldPr[226].addHandle(handleMap1Value);
            map1H = map1.Height;
            map1H2 = map1H;
            map1W = map1.Width;
            valmoWin.dv.MldPr[7].addHandle(handleMoldPr_7);
            valmoWin.dv.MldPr[14].addHandle(handleRefreshMap1);
            valmoWin.dv.MldPr[15].addHandle(handleRefreshMap1);
            valmoWin.dv.MldPr[16].addHandle(handleRefreshMap1);
            valmoWin.dv.MldPr[17].addHandle(handleRefreshMap1);
            valmoWin.dv.MldPr[18].addHandle(handleRefreshMap1);
            valmoWin.dv.MldPr[19].addHandle(handleRefreshMap1);
            valmoWin.dv.MldPr[20].addHandle(handleRefreshMap1);
            valmoWin.dv.MldPr[21].addHandle(handleRefreshMap1);
            valmoWin.dv.MldPr[22].addHandle(handleRefreshMap1);
            valmoWin.dv.MldPr[23].addHandle(handleRefreshMap1);
            valmoWin.dv.MldPr[24].addHandle(handleRefreshMap1);
            valmoWin.dv.MldPr[25].addHandle(handleRefreshMap1);

            valmoWin.dv.PrdPr[232].addHandle(MoldOpenCurveRefush);
        }

        private void MoldOpenCurveRefush(objUnit obj)
        {
            int count = obj.value;
            int MaxMoldStroke = valmoWin.dv.MldPr[226].value;
            int MaxSpeed = valmoWin.dv.MldPr[232].value;

            if (MaxMoldStroke == 0 || MaxSpeed == 0)
            {
                return;
            }

            int[] MoldOpenData = new int[count * 3];
            Lasal32.GetData(MoldOpenData, (uint)valmoWin.dv.PrdPr[231].valueNew, count * 12);

            Point[] curveData_Current = new Point[count];
            Point[] curveData_Speed = new Point[count];

            for (int i = 0; i < count; i++)
            {
                double pos = MoldOpenData[i * 3 + 2] * 1.0 / MaxMoldStroke * 10000;
                double current = MoldOpenData[i * 3] * 1.0 / 1000 * 10000;
                double speed = Math.Abs(MoldOpenData[i * 3 + 1]) * 1.0 / MaxSpeed * 10000;

                curveData_Current[i] = new Point((10000 - pos), 10000 - (current + 10000) / 2);
                curveData_Speed[i] = new Point((10000 - pos), 10000 - speed);
            }

            MoldOpenSpeedCurve.refushCurve(curveData_Speed);
            MoldOpenCurrentCurve.refushCurve(curveData_Current);
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
                    Canvas.SetLeft(cvsCurR, pos + 159);
                }
                else
                {
                    Canvas.SetLeft(cvsCurR, 159);
                }
            }
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

        private void handleRefreshMap1(objUnit obj)
        {
            refreshMap1();
        }

        private void refreshMap1()
        {
            map1HeightValue = valmoWin.dv.MldPr[232].vDbl;
            map1WidthValue = valmoWin.dv.MldPr[226].vDbl;

            double h1 = valmoWin.dv.MldPr[14].vDbl;
            double h2 = valmoWin.dv.MldPr[15].vDbl;
            double h3 = valmoWin.dv.MldPr[16].vDbl;
            double h4 = valmoWin.dv.MldPr[17].vDbl;
            double h5 = valmoWin.dv.MldPr[18].vDbl;
            double h6 = valmoWin.dv.MldPr[19].vDbl;

            double v1 = valmoWin.dv.MldPr[20].vDbl;
            double v2 = valmoWin.dv.MldPr[21].vDbl;
            double v3 = valmoWin.dv.MldPr[22].vDbl;
            double v4 = valmoWin.dv.MldPr[23].vDbl;
            double v5 = valmoWin.dv.MldPr[24].vDbl;
            double v6 = valmoWin.dv.MldPr[25].vDbl;

            setMap1LinePosition(map1lnH1, h1, v1, 0, v1);
            setMap1LinePosition(map1lnH2, h2, v2, h1, v2);

            setMap1EllipsePosition(img1ep1v0, 0, v1);
            setMap1EllipsePosition(img1ep1v1, h1, v1);
            setMap1EllipsePosition(img1ep2v0, h1, v2);
            setMap1EllipsePosition(img1ep2v1, h2, v2);
            //setMap1EllipsePosition(img1ep6v6, h6, 0);
            setMap1EllipsePosition(img1ep6v1, h6, v6);

            setMap1LinePosition(map1lnV1, h1, v2, h1, v1);
            //setMap1LinePosition(map1lnV6, h6, v6, h6, 0);
            setMap1LinePosition(map1lnV6, h6, 100, h6, -9);
            //Canvas.SetLeft(imgEndLn, map1lnV6.X1 + 1);
            int num = valmoWin.dv.MldPr[13].value;
            if (num == 3)
            {
                setMap1LinePosition(map1lnH3, h6, v6, h2, v6);

                setMap1LinePosition(map1lnV2, h2, v3, h2, v2);

                setMap1LinePosition(map1lnV2, h2, v6, h2, v2);
                setMap1EllipsePosition(img1ep6v0, h2, v6);
                //setMap1EllipsePosition(img1ep3v1, h2, v3);
                img1ep3v0.Visibility = Visibility.Hidden;
                img1ep3v1.Visibility = Visibility.Hidden;
                img1ep4v0.Visibility = Visibility.Hidden;
                img1ep4v1.Visibility = Visibility.Hidden;
                img1ep5v0.Visibility = Visibility.Hidden;
                img1ep5v1.Visibility = Visibility.Hidden;
                //img1ep6v0.Visibility = Visibility.Hidden;

                map1lnH4.Visibility = Visibility.Hidden;
                map1lnH5.Visibility = Visibility.Hidden;
                map1lnH6.Visibility = Visibility.Hidden;

                map1lnV3.Visibility = Visibility.Hidden;
                map1lnV4.Visibility = Visibility.Hidden;
                map1lnV5.Visibility = Visibility.Hidden;
            }
            else if (num == 4)
            {
                setMap1LinePosition(map1lnH3, h3, v3, h2, v3);
                setMap1LinePosition(map1lnH4, h6, v6, h3, v6);

                setMap1LinePosition(map1lnV2, h2, v3, h2, v2);
                setMap1LinePosition(map1lnV3, h3, v6, h3, v3);

                setMap1EllipsePosition(img1ep3v0, h2, v3);
                setMap1EllipsePosition(img1ep3v1, h3, v3);
                //setMap1EllipsePosition(img1ep3v1, h3, v4);
                setMap1EllipsePosition(img1ep6v0, h3, v6);

                img1ep4v0.Visibility = Visibility.Hidden;
                img1ep4v1.Visibility = Visibility.Hidden;
                img1ep5v0.Visibility = Visibility.Hidden;
                img1ep5v1.Visibility = Visibility.Hidden;

                map1lnH5.Visibility = Visibility.Hidden;
                map1lnH6.Visibility = Visibility.Hidden;

                map1lnV4.Visibility = Visibility.Hidden;
                map1lnV5.Visibility = Visibility.Hidden;
            }
            else if (num == 5)
            {
                setMap1LinePosition(map1lnH3, h3, v3, h2, v3);
                setMap1LinePosition(map1lnH4, h4, v4, h3, v4);
                setMap1LinePosition(map1lnH5, h6, v6, h4, v6);

                setMap1LinePosition(map1lnV2, h2, v3, h2, v2);
                setMap1LinePosition(map1lnV3, h3, v4, h3, v3);
                setMap1LinePosition(map1lnV4, h4, v6, h4, v4);

                setMap1EllipsePosition(img1ep3v0, h2, v3);
                setMap1EllipsePosition(img1ep3v1, h3, v3);
                setMap1EllipsePosition(img1ep4v0, h3, v4);
                setMap1EllipsePosition(img1ep4v1, h4, v4);
                //setMap1EllipsePosition(img1ep5v1, h4, v5);
                setMap1EllipsePosition(img1ep6v0, h4, v6);
                img1ep5v0.Visibility = Visibility.Hidden;
                img1ep5v1.Visibility = Visibility.Hidden;

                map1lnH6.Visibility = Visibility.Hidden;

                map1lnV5.Visibility = Visibility.Hidden;
            }
            else if (num == 6)
            {
                setMap1LinePosition(map1lnH3, h3, v3, h2, v3);
                setMap1LinePosition(map1lnH4, h4, v4, h3, v4);
                setMap1LinePosition(map1lnH5, h5, v5, h4, v5);
                setMap1LinePosition(map1lnH6, h6, v6, h5, v6);

                setMap1LinePosition(map1lnV2, h2, v3, h2, v2);
                setMap1LinePosition(map1lnV3, h3, v4, h3, v3);
                setMap1LinePosition(map1lnV4, h4, v5, h4, v4);
                setMap1LinePosition(map1lnV5, h5, v6, h5, v5);

                setMap1EllipsePosition(img1ep3v0, h2, v3);
                setMap1EllipsePosition(img1ep3v1, h3, v3);
                setMap1EllipsePosition(img1ep4v0, h3, v4);
                setMap1EllipsePosition(img1ep4v1, h4, v4);
                setMap1EllipsePosition(img1ep5v0, h4, v5);
                setMap1EllipsePosition(img1ep5v1, h5, v5);
                setMap1EllipsePosition(img1ep6v0, h5, v6);

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
                    imgUnziped.Visibility = Visibility.Visible;
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
