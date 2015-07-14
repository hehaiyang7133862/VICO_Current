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
    /// ejectorMapMiddle.xaml 的交互逻辑
    /// </summary>
    public partial class ejectorMapMiddle : UserControl
    {
        public bool useable = false;
        public ejectorMapMiddle()
        {
            InitializeComponent();

            valmoWin.dv.MldPr[68].addHandle(handleRefreshMap1);
            valmoWin.dv.MldPr[69].addHandle(handleRefreshMap1);
            valmoWin.dv.MldPr[70].addHandle(handleRefreshMap1);
            valmoWin.dv.MldPr[71].addHandle(handleRefreshMap1);
            valmoWin.dv.MldPr[72].addHandle(handleRefreshMap1);
            valmoWin.dv.MldPr[73].addHandle(handleRefreshMap1);
            valmoWin.dv.MldPr[74].addHandle(handleRefreshMap1);

            valmoWin.dv.MldPr[75].addHandle(handleRefreshMap2);
            valmoWin.dv.MldPr[76].addHandle(handleRefreshMap2);
            valmoWin.dv.MldPr[77].addHandle(handleRefreshMap2);
            valmoWin.dv.MldPr[78].addHandle(handleRefreshMap2);
            valmoWin.dv.MldPr[79].addHandle(handleRefreshMap2);
            valmoWin.dv.MldPr[80].addHandle(handleRefreshMap2);
            valmoWin.dv.MldPr[81].addHandle(handleRefreshMap2);
            valmoWin.dv.MldPr[2].addHandle(handleMoldPr_2);

            valmoWin.lstStartUpInit.Add(startUpInit);

            valmoWin.dv.PrdPr[242].addHandle(EjectorFWDCurveRefush);
            valmoWin.dv.PrdPr[247].addHandle(EjectorBWDCurveRefush);
        }

        private void EjectorBWDCurveRefush(objUnit obj)
        {
            int count = obj.value;
            int MaxStroke = valmoWin.dv.MldPr[234].value;
            int MaxSpeed = valmoWin.dv.MldPr[240].value;

            if (MaxStroke == 0 || MaxSpeed == 0)
            {
                return;
            }

            int[] EjectorBWDData = new int[count * 3];
            Lasal32.GetData(EjectorBWDData, (uint)valmoWin.dv.PrdPr[246].valueNew, count * 12);

            Point[] curveData_Current = new Point[count];
            Point[] curveData_Speed = new Point[count];

            for (int i = 0; i < count; i++)
            {
                double pos = EjectorBWDData[i * 3 + 2] * 1.0 / MaxStroke * 10000;
                double current = EjectorBWDData[i * 3] * 1.0 / 1000 * 10000;
                double speed = Math.Abs(EjectorBWDData[i * 3 + 1]) * 1.0 / MaxSpeed * 10000;

                curveData_Current[i] = new Point(pos, 10000 - (current + 10000) / 2);
                curveData_Speed[i] = new Point(pos, 10000 - speed);
            }

            EjectorBWDSpeedCurve.refushCurve(curveData_Speed);
            EjectorBWDCurrentCurve.refushCurve(curveData_Current);
        }
        private void EjectorFWDCurveRefush(objUnit obj)
        {
            int count = obj.value;
            int MaxStroke = valmoWin.dv.MldPr[234].value;
            int MaxSpeed = valmoWin.dv.MldPr[240].value;
            int[] EjectorFWDData = new int[count * 3];
            Lasal32.GetData(EjectorFWDData, (uint)valmoWin.dv.PrdPr[241].valueNew, count * 12);

            Point[] curveData_Current = new Point[count];
            Point[] curveData_Speed = new Point[count];

            for (int i = 0; i < count; i++)
            {
                double pos = EjectorFWDData[i * 3 + 2] * 1.0 / MaxStroke * 10000;
                double current = EjectorFWDData[i * 3] * 1.0 / 1000 * 10000;
                double speed = Math.Abs(EjectorFWDData[i * 3 + 1]) * 1.0 / MaxSpeed * 10000;

                curveData_Current[i] = new Point(pos, 10000 - (current + 10000) / 2);
                curveData_Speed[i] = new Point(pos, 10000 - speed);
            }

            EjectorFWDSpeedCurve.refushCurve(curveData_Speed);
            EjectorFWDCurrentCurve.refushCurve(curveData_Current);
        }

        private void startUpInit()
        {
            objUnit obj = valmoWin.dv.MldPr[132];
            map3Width = obj.vDblNew;
            lbMaxIn.Content = obj.getStrValue(map3Width);
            lbMaxIn20.Content = obj.getStrValue(map3Width * 0.2);
            lbMaxIn40.Content = obj.getStrValue(map3Width * 0.4);
            lbMaxIn60.Content = obj.getStrValue(map3Width * 0.6);
            lbMaxIn80.Content = obj.getStrValue(map3Width * 0.8);

            lbMaxOut.Content = lbMaxIn.Content;
            lbMaxOut20.Content = lbMaxIn20.Content;
            lbMaxOut40.Content = lbMaxIn40.Content;
            lbMaxOut60.Content = lbMaxIn60.Content;
            lbMaxOut80.Content = lbMaxIn80.Content;
        }
        private void handleMoldPr_2(objUnit obj)
        {
            map3Width = valmoWin.dv.MldPr[132].vDbl;
            mapHeightValue = valmoWin.dv.MldPr[240].vDbl;
            double curPos = obj.vDbl;
            string curPosStr = obj.vDblStr;
            lbPos3.Content = curPosStr;
            lbPos4.Content = curPosStr;
            if (map3Width != 0)
            {
                double pos = map3Panel.Width / map3Width * curPos;
                if (pos > 0)
                {
                    Canvas.SetLeft(cvsPos3, pos + 159);
                    Canvas.SetLeft(cvsPos4, pos + 589);
                }
                else
                {
                    Canvas.SetLeft(cvsPos3, 159);
                    Canvas.SetLeft(cvsPos4, 589);
                }
            }

        }


        void handleRefreshMap1(objUnit obj)
        {
            refreshMap3();
        }
        void handleRefreshMap2(objUnit obj)
        {
            refreshMap4();
        }

        private void setMapLinePosition(Line ln, double x1, double y1, double x2, double y2)
        {
            if (map3Width > 0.0001 || map3Width < -0.0001)
            {
                ln.X1 = map3Panel.Width / map3Width * x1;
                ln.Y1 = map3Panel.Height - (map3Panel.Height) / 100.0 * y1;
                ln.X2 = map3Panel.Width / map3Width * x2;
                ln.Y2 = map3Panel.Height - (map3Panel.Height) / 100.0 * y2;
                ln.Visibility = Visibility.Visible;
            }
        }
        private void setMapEllipsePosition(Image img, double x, double y)
        {
            if (map3Width > 0.0001 || map3Width < -0.0001)
            {
                Canvas.SetLeft(img, map3Panel.Width / map3Width * x - 5.5);
                Canvas.SetTop(img, map3Panel.Height - (map3Panel.Height) / 100.0 * y - 5.5);
                img.Visibility = Visibility.Visible;
            }
        }

        public double map3Width = 1;
        public double mapHeightValue = 1;
        private void refreshMap3()
        {
            double map3H4 = valmoWin.dv.MldPr[78].vDbl;
            double map3H3 = valmoWin.dv.MldPr[69].vDbl;
            double map3H2 = valmoWin.dv.MldPr[70].vDbl;
            double map3H1 = valmoWin.dv.MldPr[71].vDbl;

            double map3V4 = valmoWin.dv.MldPr[81].vDbl;
            double map3V3 = valmoWin.dv.MldPr[72].vDbl;
            double map3V2 = valmoWin.dv.MldPr[73].vDbl;
            double map3V1 = valmoWin.dv.MldPr[74].vDbl;



            //setMapEllipsePosition(img1ep1v0, h1, 0);
            setMapEllipsePosition(img1ep1v1, map3H1, map3V1);
            setMapEllipsePosition(img1ep4v0, map3H4, 0);

            setMapLinePosition(map3lnV1, map3H1, 100, map3H1, -9);
            //setMapLinePosition(map3lnV1, h1, v1, h1, 0);
            //Canvas.SetLeft(imgEndLn, map3lnV1.X1);
            int num = valmoWin.dv.MldPr[68].value;
            if (num == 1)
            {
                setMapLinePosition(map3lnH1, map3H1, map3V1, map3H4, map3V1);
                setMapLinePosition(map3lnV4, map3H4, 100, map3H4, -9);

                setMapEllipsePosition(img1ep4v1, map3H4, map3V1);

                img1ep2v0.Visibility = Visibility.Hidden;
                img1ep2v1.Visibility = Visibility.Hidden;
                img1ep3v0.Visibility = Visibility.Hidden;
                img1ep3v1.Visibility = Visibility.Hidden;

                map3lnH2.Visibility = Visibility.Hidden;
                map3lnH3.Visibility = Visibility.Hidden;

                map3lnV2.Visibility = Visibility.Hidden;
                map3lnV3.Visibility = Visibility.Hidden;
            }
            else if (num == 2)
            {
                setMapLinePosition(map3lnH1, map3H1, map3V1, map3H3, map3V1);
                setMapLinePosition(map3lnH3, map3H3, map3V3, map3H4, map3V3);

                setMapLinePosition(map3lnV3, map3H3, map3V1, map3H3, map3V3);
                setMapLinePosition(map3lnV4, map3H4, 100, map3H4, -9);

                setMapEllipsePosition(img1ep3v0, map3H3, map3V3);
                setMapEllipsePosition(img1ep3v1, map3H3, map3V1);
                setMapEllipsePosition(img1ep4v1, map3H4, map3V3);

                img1ep2v0.Visibility = Visibility.Hidden;
                img1ep2v1.Visibility = Visibility.Hidden;


                map3lnH2.Visibility = Visibility.Hidden;
                map3lnV2.Visibility = Visibility.Hidden;

            }
            else if (num == 3)
            {
                setMapLinePosition(map3lnH1, map3H1, map3V1, map3H2, map3V1);
                setMapLinePosition(map3lnH2, map3H2, map3V2, map3H3, map3V2);
                setMapLinePosition(map3lnH3, map3H3, map3V3, map3H4, map3V3);

                setMapLinePosition(map3lnV2, map3H2, map3V1, map3H2, map3V2);
                setMapLinePosition(map3lnV3, map3H3, map3V2, map3H3, map3V3);
                setMapLinePosition(map3lnV4, map3H4, 100, map3H4, -9);

                setMapEllipsePosition(img1ep2v1, map3H2, map3V1);
                setMapEllipsePosition(img1ep2v0, map3H2, map3V2);
                setMapEllipsePosition(img1ep3v1, map3H3, map3V2);
                setMapEllipsePosition(img1ep3v0, map3H3, map3V3);
                setMapEllipsePosition(img1ep4v1, map3H4, map3V3);
            }

        }

        private void refreshMap4()
        {

            double map4H3 = valmoWin.dv.MldPr[78].vDbl;
            double map4H2 = valmoWin.dv.MldPr[77].vDbl;
            double map4H1 = valmoWin.dv.MldPr[76].vDbl;

            double map4V3 = valmoWin.dv.MldPr[81].vDbl;
            double map4V2 = valmoWin.dv.MldPr[80].vDbl;
            double map4V1 = valmoWin.dv.MldPr[79].vDbl;


            setMapEllipsePosition(imgep3v0, map4H3, map4V3);

            setMapLinePosition(map4lnV3, map4H3, 100, map4H3, -9);
            //Canvas.SetLeft(imgEndLn2, map4lnV3.X1);
            int num = valmoWin.dv.MldPr[75].value;
            if (num == 1)
            {
                setMapLinePosition(map4lnH3, 0, map4V3, map4H3, map4V3);

                setMapEllipsePosition(imgep3v1, 0, map4V3);

                imgep1v0.Visibility = Visibility.Hidden;
                imgep1v1.Visibility = Visibility.Hidden;
                imgep2v0.Visibility = Visibility.Hidden;
                imgep2v1.Visibility = Visibility.Hidden;

                map4lnH2.Visibility = Visibility.Hidden;
                map4lnH1.Visibility = Visibility.Hidden;
                map4lnV1.Visibility = Visibility.Hidden;
                map4lnV2.Visibility = Visibility.Hidden;

            }
            else if (num == 2)
            {
                setMapLinePosition(map4lnH3, map4H1, map4V3, map4H3, map4V3);
                setMapLinePosition(map4lnH1, 0, map4V1, map4H1, map4V1);
                setMapLinePosition(map4lnV1, map4H1, map4V1, map4H1, map4V3);
                setMapEllipsePosition(imgep3v1, map4H1, map4V3);
                setMapEllipsePosition(imgep1v0, map4H1, map4V1);
                setMapEllipsePosition(imgep1v1, 0, map4V1);
                //setMapEllipsePosition(imgep0v0, 0, v1);

                imgep2v0.Visibility = Visibility.Hidden;
                imgep2v1.Visibility = Visibility.Hidden;


                map4lnH2.Visibility = Visibility.Hidden;
                map4lnV2.Visibility = Visibility.Hidden;

            }
            else if (num == 3)
            {
                setMapLinePosition(map4lnH2, map4H1, map4V2, map4H2, map4V2);
                setMapLinePosition(map4lnH3, map4H2, map4V3, map4H3, map4V3);
                setMapLinePosition(map4lnH1, 0, map4V1, map4H1, map4V1);

                setMapLinePosition(map4lnV1, map4H1, map4V1, map4H1, map4V2);
                setMapLinePosition(map4lnV2, map4H2, map4V2, map4H2, map4V3);
                //setMapEllipsePosition(imgep0v0, 0, v1);

                setMapEllipsePosition(imgep1v1, 0, map4V1);
                setMapEllipsePosition(imgep1v0, map4H1, map4V1);
                setMapEllipsePosition(imgep2v1, map4H1, map4V2);
                setMapEllipsePosition(imgep2v0, map4H2, map4V2);
                setMapEllipsePosition(imgep3v1, map4H2, map4V3);
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
