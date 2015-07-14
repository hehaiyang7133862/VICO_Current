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
using System.Threading;
using System.Windows.Threading;

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class watchCtl : UserControl
    {
        double[] wsX1 = new double[360];
        double[] wsY1 = new double[360];
        double[] wsX2 = new double[360];
        double[] wsY2 = new double[360];

        double[] wmX1 = new double[60];
        double[] wmY1 = new double[60];
        double[] wmX2 = new double[60];
        double[] wmY2 = new double[60];

        double[] whX1 = new double[60];
        double[] whY1 = new double[60];
        double[] whX2 = new double[60];
        double[] whY2 = new double[60];
        Thread tdWatch;
        DispatcherTimer dtDraw = new DispatcherTimer();
        DateTime td;
        string curTime = "";
        private void getWatchArr(double x, double y, double l1, double l2, double lm, double lh)
        {
            for (int i = 0; i < 60; i++)
            {
                wsX1[i] = x + l1 * Math.Cos((6 * (90 - i) - 90) * Math.PI / 180);
                wsX2[i] = x - l2 * Math.Cos((6 * (90 - i) - 90) * Math.PI / 180);
                wsY1[i] = y - l1 * Math.Sin((6 * (90 - i) - 90) * Math.PI / 180);
                wsY2[i] = y + l2 * Math.Sin((6 * (90 - i) - 90) * Math.PI / 180);
            }
            for (int i = 0; i < 60; i++)
            {
                wmX1[i] = x - lm * 0.1 * Math.Cos((6 * (90 - i) - 90) * Math.PI / 180);
                wmX2[i] = x - lm * Math.Cos((6 * (90 - i) + 90) * Math.PI / 180);
                wmY1[i] = y + lm * 0.1 * Math.Sin((6 * (90 - i) - 90) * Math.PI / 180);
                wmY2[i] = y + lm * Math.Sin((6 * (90 - i) + 90) * Math.PI / 180);
            }
            for (int i = 0; i < 60; i++)
            {
                whX1[i] = x - lh * 0.1 * Math.Cos((6 * (90 - i) - 90) * Math.PI / 180);
                whX2[i] = x - lh * Math.Cos((6 * (90 - i) + 90) * Math.PI / 180);
                whY1[i] = y + lh * 0.1 * Math.Sin((6 * (90 - i) - 90) * Math.PI / 180);
                whY2[i] = y + lh * Math.Sin((6 * (90 - i) + 90) * Math.PI / 180);
            }
        }
        int curSec = 15;
        int curMin = 15;
        int curHour = 0;
        bool firstStart = true;
        private delegate void dg();
        private void drawWatch()
        {

            td = valmoWin.SysTime;
            curSec = td.Second;
            curMin = td.Minute;
            curHour = (td.Hour * 5 + curMin / 12) % 60;

            while (true)
            {
                this.Dispatcher.BeginInvoke(new dg(drawWatchDispatcher2), null);
                Thread.Sleep(500);
            }
        }
        private void drawWatchDispatcher()
        {

            if (firstStart)
            {
                watchMin.X1 = wmX1[curMin];
                watchMin.Y1 = wmY1[curMin];
                watchMin.X2 = wmX2[curMin];
                watchMin.Y2 = wmY2[curMin];

                watchSec.X1 = wsX1[curSec];
                watchSec.Y1 = wsY1[curSec];
                watchSec.X2 = wsX2[curSec];
                watchSec.Y2 = wsY2[curSec];

                watchHour.X1 = whX1[curHour];
                watchHour.Y1 = whY1[curHour];
                watchHour.X2 = whX2[curHour];
                watchHour.Y2 = whY2[curHour];

                firstStart = false;
            }
            if (curSec >= 60)
            {
                curSec = 0;
                curMin++;


                if (curMin >= 60)
                {
                    curMin = 0;
                    curHour++;
                    if (curHour >= 60)
                        curHour = 0;

                    watchHour.X1 = whX1[curHour];
                    watchHour.Y1 = whY1[curHour];
                    watchHour.X2 = whX2[curHour];
                    watchHour.Y2 = whY2[curHour];
                }
                watchMin.X1 = wmX1[curMin];
                watchMin.Y1 = wmY1[curMin];
                watchMin.X2 = wmX2[curMin];
                watchMin.Y2 = wmY2[curMin];
            }
            watchSec.X1 = wsX1[curSec];
            watchSec.Y1 = wsY1[curSec];
            watchSec.X2 = wsX2[curSec];
            watchSec.Y2 = wsY2[curSec];

            //lbtm.Content = ((curHour - 15) / 12).ToString() + ":" + curMin.ToString() + ":" + curSec.ToString();

            curSec++;
            curTime = curHour.ToString() + ":" + curMin.ToString() + ":" + curSec.ToString();
            //lbtmTime.Content = curTime;
        }
        private void drawWatchDispatcher2()
        {
            td = valmoWin.SysTime;
            curSec = td.Second;
            curMin = td.Minute;
            curHour = (td.Hour * 5 + curMin / 12) % 60;
            //Console.WriteLine(td.Hour.ToString() + "," + curHour.ToString());
            watchSec.X1 = wsX1[curSec];
            watchSec.Y1 = wsY1[curSec];
            watchSec.X2 = wsX2[curSec];
            watchSec.Y2 = wsY2[curSec];

            watchMin.X1 = wmX1[curMin];
            watchMin.Y1 = wmY1[curMin];
            watchMin.X2 = wmX2[curMin];
            watchMin.Y2 = wmY2[curMin];

            watchHour.X1 = whX1[curHour];
            watchHour.Y1 = whY1[curHour];
            watchHour.X2 = whX2[curHour];
            watchHour.Y2 = whY2[curHour];
            //lbtm.Content = ((curHour - 15) / 12).ToString() + ":" + curMin.ToString() + ":" + curSec.ToString();

            //curSec++;
            //lbtmTime.Content = curTime = td.Hour.ToString() + ":" + curMin.ToString("00") + ":" + curSec.ToString("00");
            if (td.Hour > 12)
                lbPmAm.Content = "PM";
            //lbTimeAP.Content = "PM";
            else
                lbPmAm.Content = "AM";
            //lbTimeAP.Content = "AM";
            curTime = td.Hour.ToString() + ":" + curMin.ToString("00");
            //curTime = td.Hour.ToString("00") + ":" + curMin.ToString("00") + ":" + curSec.ToString("00");
        }
        private void dtDraw_Tick(object obj, EventArgs e)
        {
            this.Dispatcher.BeginInvoke(new dg(drawWatchDispatcher2), null);
        }
        public watchCtl()
        {
            tdWatch = new Thread(new ThreadStart(drawWatch));
            tdWatch.Name = "tdWatch";
            InitializeComponent();
            dtDraw.Interval = new TimeSpan(0, 0, 0, 0, 500);
            dtDraw.Tick += new EventHandler(dtDraw_Tick);
            getWatchArr(143, 143, 110, 15, 100, 75);
            //tdWatch.Start();
            dtDraw.Start();
        }
        public string getCurTime()
        {
            td = DateTime.Now;
            curSec = td.Second;
            curMin = td.Minute;
            curHour = (td.Hour * 5 + curMin / 12) % 60;
            //Console.WriteLine(td.Hour.ToString() + "," + curHour.ToString());
            watchSec.X1 = wsX1[curSec];
            watchSec.Y1 = wsY1[curSec];
            watchSec.X2 = wsX2[curSec];
            watchSec.Y2 = wsY2[curSec];

            watchMin.X1 = wmX1[curMin];
            watchMin.Y1 = wmY1[curMin];
            watchMin.X2 = wmX2[curMin];
            watchMin.Y2 = wmY2[curMin];

            watchHour.X1 = whX1[curHour];
            watchHour.Y1 = whY1[curHour];
            watchHour.X2 = whX2[curHour];
            watchHour.Y2 = whY2[curHour];
            return curTime;
        }
    }
}
