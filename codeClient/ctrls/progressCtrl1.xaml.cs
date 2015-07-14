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
using System.Windows.Threading;


namespace nsVicoClient.ctrls
{
    /// <summary>
    /// progressCtrl1.xaml 的交互逻辑
    /// </summary>
    public partial class progressCtrl1 : UserControl
    {
        DispatcherTimer dtLoad = new DispatcherTimer();
        DblEvent dealHandle;
        nullEvent disposeHandle;
        public progressCtrl1()
        {
            InitializeComponent();
            dtLoad.Interval = new TimeSpan(0, 0, 0, 0, 100);
            dtLoad.Tick += new EventHandler(dtLoad_Tick);
            this.Visibility = Visibility.Hidden;
        }
        public void setInterval(int millSecond, int second = 0, int minutes = 0)
        {
            dtLoad.Interval = new TimeSpan(0, 0, minutes, second, millSecond);
        }

        void dtLoad_Tick(object sender, EventArgs e)
        {
            double curValue = 0;
            if (dealHandle != null)
                curValue = dealHandle();
            else
                curValue = pBar.Value + 1;
            if (curValue > 99.99)
            {
                dtLoad.Stop();
                if (disposeHandle != null)
                {
                    disposeHandle();
                }
                this.Visibility = Visibility.Hidden;
            }
            else
            {
                pBar.Value = curValue;
                dis = curValue.ToString("0.0") + "%";
            }
        }

        public void setPro(double a, double b)
        {
            pBar.Value = a;
            pBar2.Value = b;
            vm.printLn("[setPro]" + a + " " + b);
        }

        public void show(DblEvent dealFunc = null, nullEvent disposeFunc = null)
        {
            dealHandle = dealFunc;
            disposeHandle = disposeFunc;
            pBar.Value = 0;
            lbValue.Content = "0.0%";
            //if (dealHandle == null)
            //{
            //    throw (new Exception("<progressCtrl.show> DispatcherTimer dealHandle show not be null!"));
            //}
            dtLoad.Start();
            this.Visibility = Visibility.Visible;
        }

        public string dis
        {
            get
            {
                return lbValue.Content.ToString();
            }
            set
            {
                lbValue.Content = value;
            }
        }

        public void setDis(string str1, string str2)
        {
            lbDis.Content = str1;
            lbDis2.Content = str2;
        }

        public void start()
        {
            dtLoad.Start();
        }

        public double h
        {
            set
            {
                cvsMain.Height = value;
            }
            get
            {
                return cvsMain.Height;
            }
        }

        public double w
        {
            get
            {
                return cvsMain.Width;
            }
            set
            {
                cvsMain.Width = value;
            }
        }
    }
}
