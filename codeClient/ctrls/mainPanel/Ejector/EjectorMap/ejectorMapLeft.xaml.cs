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
    /// ejectorMapLeft.xaml 的交互逻辑
    /// </summary>
    public partial class ejectorMapLeft : UserControl
    {
        public double map3Width = 1;
        public double mapHeightValue = 1;
        public bool useable = false;
        public ejectorMapLeft()
        {
            InitializeComponent();

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
        }

        private void EjectorFWDCurveRefush(objUnit obj)
        {
            int count = obj.value;
            int MaxStroke = valmoWin.dv.MldPr[234].value;
            int MaxSpeed = valmoWin.dv.MldPr[240].value;
            int[] EjectorFWDData = new int[count * 3];

            if (MaxStroke == 0 || MaxSpeed == 0)
            {
                return;
            }

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
            lbMax.Content = obj.getStrValue(map3Width);
            lbMax20.Content = obj.getStrValue(map3Width * 0.2);
            lbMax40.Content = obj.getStrValue(map3Width * 0.4);
            lbMax60.Content = obj.getStrValue(map3Width * 0.6);
            lbMax80.Content = obj.getStrValue(map3Width * 0.8);

            lbMax10.Content = obj.getStrValue(map3Width * 0.1);
            lbMax30.Content = obj.getStrValue(map3Width * 0.3);
            lbMax50.Content = obj.getStrValue(map3Width * 0.5);
            lbMax70.Content = obj.getStrValue(map3Width * 0.7);
            lbMax90.Content = obj.getStrValue(map3Width * 0.9);

        }
        private void handleMoldPr_2(objUnit obj)
        {
            map3Width = valmoWin.dv.MldPr[132].vDbl;
            mapHeightValue = valmoWin.dv.MldPr[240].vDbl;
            double curPos = obj.vDbl;
            string curPosStr = obj.vDblStr;
            lbPos3.Content = curPosStr;
            if (map3Width != 0)
            {
                double pos = map4Panel.Width / map3Width * curPos;
                if (pos > 0)
                {
                    Canvas.SetLeft(cvsPos3, pos + 159);
                }
                else
                {
                    Canvas.SetLeft(cvsPos3, 159);
                }
            }

        }

        void handleRefreshMap2(objUnit obj)
        {
            refreshMap4();
        }

        private void setMapLinePosition(Line ln, double x1, double y1, double x2, double y2)
        {
            if (map3Width > 0.0001 || map3Width < -0.0001)
            {
                ln.X1 = map4Panel.Width / map3Width * x1;
                ln.Y1 = map4Panel.Height - (map4Panel.Height) / 100.0 * y1;
                ln.X2 = map4Panel.Width / map3Width * x2;
                ln.Y2 = map4Panel.Height - (map4Panel.Height) / 100.0 * y2;
                ln.Visibility = Visibility.Visible;
            }
        }
        private void setMapEllipsePosition(Image img, double x, double y)
        {
            if (map3Width > 0.0001 || map3Width < -0.0001)
            {
                Canvas.SetLeft(img, map4Panel.Width / map3Width * x - 5.5);
                Canvas.SetTop(img, map4Panel.Height - (map4Panel.Height) / 100.0 * y - 5.5);
                img.Visibility = Visibility.Visible;
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
