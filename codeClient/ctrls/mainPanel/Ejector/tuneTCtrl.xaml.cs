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
    /// tuneTCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class tuneTCtrl : UserControl
    {
        DispatcherTimer dt = new DispatcherTimer();
        int Angle = 0;
        public tuneTCtrl()
        {
            InitializeComponent();
            valmoWin.dv.MldPr[585].addHandle(handleRefresh);
            valmoWin.dv.MldPr[589].addHandle(handleRefresh589);
            valmoWin.dv.MldPr[588].addHandle(handleRefresh588);
            valmoWin.dv.MldPr[587].addHandle(handleRefresh587);
            valmoWin.dv.MldPr[586].addHandle(handleRefresh586);

            dt.Tick += new EventHandler(dtFunc);
            dt.Interval = new TimeSpan(0, 0, 0, 0, 100);
            //dt.Start();
        }

        private void handleRefresh(objUnit obj)
        {
            angle = obj.vDbl;
        }

        private void handleRefresh589(objUnit obj)
        {
            rct.Opacity = obj.value;
        }

        private void handleRefresh588(objUnit obj)
        {
            bdUp.Opacity = obj.value;
            if ((valmoWin.dv.MldPr[587].value ^ obj.value) == 0)
            {
                //if (obj.value == 1)
                //{
                //    tbMain.SelectedIndex = 0;
                //}
                //else
                //{
                    tbMain.SelectedIndex = 1;
                //}
            }
            else
            {
                if (obj.value == 1)
                {
                    tbMain.SelectedIndex = 0;
                }
                else
                {
                    tbMain.SelectedIndex = 2;
                }
            }
        }

        private void handleRefresh587(objUnit obj)
        {
            bdDown.Opacity = obj.value;
            if ((valmoWin.dv.MldPr[588].value ^ obj.value) == 0)
            {
                //if (obj.value == 1)
                //{
                //    tbMain.SelectedIndex = 2;
                //}
                //else
                //{
                    tbMain.SelectedIndex = 1;
                //}
            }
            else
            {
                if (obj.value == 1)
                {
                    tbMain.SelectedIndex = 2;
                }
                else
                {
                    tbMain.SelectedIndex = 0;
                }
                
            }
        }

        private void handleRefresh586(objUnit obj)
        {
            bdTuneLight.Opacity = obj.value;
        }
        public void dtFunc(object sender, EventArgs e)
        {
            Angle += 1;
            //imgTuneT.RenderTransform = new RotateTransform(Angle);
            angle = Angle;
            lbCtrlUnit1.dis = Angle.ToString("0.00");
            if (Angle == 360)
                Angle = 0;

        }

        public double angle
        {
            set
            {
                cvsTuneT.RenderTransform = new RotateTransform(value);
                lbCtrlUnit1.dis = value.ToString("0.00");
                //imgTuneT.RenderTransform = new RotateTransform(value);
            }
            get
            {
                return 0;
            }
        }
        public int stateNr
        {
            set
            {
                switch (value)
                {
                    case -1:
                        tbMain.SelectedIndex = 0;
                        break;
                    case 0:
                        tbMain.SelectedIndex = 1;
                        break;
                    case 1:
                        tbMain.SelectedIndex = 2;
                        break;
                }
            }
            get
            {
                return tbMain.SelectedIndex;
            }
        }
    }
}
