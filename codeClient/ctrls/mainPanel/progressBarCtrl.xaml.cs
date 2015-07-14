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
using System.IO;
using System.Windows.Threading;
using System.Threading;

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// Interaction logic for progressBarCtrl.xaml
    /// </summary>
    public partial class progressBarCtrl : UserControl
    {
        public delegate void disposeEvent();
        DispatcherTimer dtRate = new DispatcherTimer();
        double curRate = 0;
        double rateSetted = 0;
        public disposeEvent disposeHandle;
        public progressBarCtrl()
        {
            InitializeComponent();
            dtRate.Tick += new EventHandler(progRateFunc);
            dtRate.Interval = new TimeSpan(0, 0, 0, 0, 1);
        }
        public void setDisposeHandle(disposeEvent handle)
        {

        }
        public bool state
        {
            get
            {
                return dtRate.IsEnabled;
            }
        }
        public void reStart()
        {
            init();
            this.Visibility = Visibility.Visible;
            dtRate.Start();
        }
        public void start()
        {
            setCurPer(5.0);
            
            curRate = 0;
            lbPer.Content = "0%";
            progressBar1.Value = 0;
            this.Visibility = Visibility.Visible;
            dtRate.Start();
        }
        public void setCurRate(double per)
        {
            rateSetted = per;
        }
        public void setRate(double per)
        {
            progressBar1.Value = per;
        }
        double curStepSpeed = 0.5;
        public void setSpeed(int stepNr)
        {
            switch (stepNr)
            {
                case 0:
                    curStepSpeed = 0.05;
                    break;
                case 1:
                    curStepSpeed = 0.2;
                    break;
                case 2:
                    curStepSpeed = 0.5;
                    break;
                case 3:
                    curStepSpeed = 1;
                    break;
            }
        }
        public void stop()
        {
            dtRate.Stop();
            this.Visibility = Visibility.Hidden;
        }
        private void progRateFunc(object obj,EventArgs e)
        {
            if (curRate < 100 && curRate < rateSetted)
            {
                curRate += curStepSpeed;
                setCurPer(curRate);
                progressBar1.Value = curRate;
            }
            if (curRate + 0.05 > 100)
            {
                curRate = 100;
                //dtRate.Stop();
                //Thread.Sleep(new TimeSpan(0,0,0,0,10));
                //this.Visibility = Visibility.Hidden;
            }
            
        }
        public void init()
        {
            curRate = 0;
            lbPer.Content = "0%";
            progressBar1.Value = 0;
        }
        private delegate void setCurPerEvent(double per);
        private void setCurPer(double per)
        {
            this.Dispatcher.BeginInvoke(new setCurPerEvent(setCurPerFunc), per);
        }
        private void setCurPerFunc(double per)
        {
            if (per > 100 || per < 0)
            {
                return;
            }
            else
            {
                lbPer.Content = per.ToString("0.0") + "%";
                progressBar1.Value = per;
            }
        }
        public bool copy(string sourse,string dst)
        {
            try
            {
                DirectoryInfo di_sourse = new DirectoryInfo(sourse);
                DirectoryInfo di_dst = new DirectoryInfo(dst);
                if (di_sourse.Exists == false)
                {
                    return false;
                }
                FileStream fsSourse = new FileStream(sourse, FileMode.Open);
                FileStream fsDest = new FileStream(dst, FileMode.Create);
                if (fsSourse == null || fsDest == null)
                {
                    return false;
                }
                //valmoWin.showWinMsgHandle();
                return true;
            }
            catch 
            {
                return false;
            }
        }
    }
}
