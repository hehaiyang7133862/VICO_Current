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
    /// progressCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class progressCtrl : UserControl
    {
        DispatcherTimer dtLoad = new DispatcherTimer();
        DblEvent dealHandle;
        nullEvent disposeHandle;
        public progressCtrl()
        {
            InitializeComponent();
            dtLoad.Interval = new TimeSpan(0, 0, 0, 0, 10);
            dtLoad.Tick += new EventHandler(dtLoad_Tick);
            this.Visibility = Visibility.Hidden;
        }
        public void setInterval(int millSecond,int second = 0,int minutes = 0)
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
                if (UpanPath != null)
                {
                    lbDisOk.Visibility = Visibility.Visible;
                    btnConfirm.Visibility = Visibility.Visible;
                    lbDisOk.Content = "已成功导出文件到U盘" + UpanPath + "目录";
                }
                else
                {
                    this.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                //pBar.Value = curValue;
                dis = curValue.ToString("0.0") + "%";
            }
        }

        public void setPro(double a, double b)
        {
            pBar.Value = a;
            pBar2.Value = b;
            //vm.printLn("[setPro]" + a + " " + b);
        }

        string UpanPath = null;
        public void show(DblEvent dealFunc = null, nullEvent disposeFunc = null,string path = null)
        {
            UpanPath = path;
            lbDisOk.Visibility = Visibility.Hidden;
            btnConfirm.Visibility = Visibility.Hidden;
            dealHandle = dealFunc;
            disposeHandle = disposeFunc;
            pBar.Value = 0;
            lbValue.Content = "0.0%";
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

        private void btnConfirm_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

    }
}
