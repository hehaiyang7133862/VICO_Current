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
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Threading;

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// startWindow.xaml 的交互逻辑
    /// </summary>
    public partial class startWindow : Window
    {
        DispatcherTimer dtQuit = new DispatcherTimer();
        DateTime dtNow;
        public startWindow()
        {
            InitializeComponent();
            dtQuit.Interval = new TimeSpan(0, 0, 1);
            dtQuit.Tick += new EventHandler(dtQuit_Tick);
            dtNow = DateTime.Now;
            //dtQuit.Start();
        }

        void dtQuit_Tick(object sender, EventArgs e)
        {
            lbPer.Content = (DateTime.Now - dtNow).Seconds;
            if (DateTime.Now - dtNow > new TimeSpan(0, 0, 0,30))
            {
                this.Visibility = Visibility.Hidden;
                Console.WriteLine("Quit.....................................");
            }
        }

        public double per
        {
            set
            {
                lbPer.Content = value.ToString("0.0") + "%";
            }
        }
    }
}
