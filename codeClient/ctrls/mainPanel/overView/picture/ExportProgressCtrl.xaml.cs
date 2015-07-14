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

namespace nsVicoClient.ctrls
{
    public partial class ExportProgressCtrl : UserControl
    {
        private List<string> lstExport = new List<string>();
        private string SavePath = string.Empty;
        DispatcherTimer TimerExprot = new DispatcherTimer();

        public ExportProgressCtrl()
        {
            InitializeComponent();

            TimerExprot.Interval = new TimeSpan(0, 0, 0, 0, 10);
            TimerExprot.Tick += new EventHandler(Export);

            this.Visibility = Visibility.Hidden;
        }

        public void Initialize(List<string> input, string inSavePath)
        {
            lstExport = input;
            SavePath = inSavePath;

            curNr = 0;
            this.Visibility = Visibility.Visible;

            log.Items.Clear();
            TimerExprot.Start();
        }

        private int curNr;
        private void Export(object sender, EventArgs e)
        {
            if (curNr == lstExport.Count)
            {
                TimerExprot.Stop();
                return;
            }

            int count = lstExport.Count;

            FileInfo fi = new FileInfo(lstExport[curNr]);

            System.Drawing.Image sourceImage = System.Drawing.Image.FromFile(lstExport[curNr]);
            sourceImage.Save(SavePath + fi.Name);

            pValue.Value = (curNr + 1) * 100 / count;
            lbValue.Content = ((curNr + 1) * 100 / count).ToString();
            log.Items.Add(App.Current.TryFindResource("lanKey2191") + fi.Name);
            curNr++;
        }

        private void btnConfirm_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}
