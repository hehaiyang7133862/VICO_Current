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
using System.IO;
using nsDataMgr;

namespace nsVicoClient.ctrls
{
    public partial class DeletePictureProgressCtrl : UserControl
    {
        private List<string> lstDelete = new List<string>();
        private DispatcherTimer TimerDelete = new DispatcherTimer();
        private nullEvent _exitEvent;
        public nullEvent ExitEvent
        {
            set
            {
                _exitEvent = value;
            }
        }

        public DeletePictureProgressCtrl()
        {
            InitializeComponent();

            TimerDelete.Interval = new TimeSpan(0, 0, 0, 0, 10);
            TimerDelete.Tick += new EventHandler(Delete);

            this.Visibility = Visibility.Hidden;
        }

        public void Initialize(List<string> input)
        {
            lstDelete = input;

            curNr = 0;
            this.Visibility = Visibility.Visible;

            log.Items.Clear();
            TimerDelete.Start();
        }

        private int curNr;
        private void Delete(object sender, EventArgs e)
        {
            if (curNr == lstDelete.Count)
            {
                TimerDelete.Stop();
                return;
            }

            int count = lstDelete.Count;

            FileInfo fi = new FileInfo(lstDelete[curNr]);

            if (fi.Exists == true)
            {
                fi.Delete();
            }

            pValue.Value = (curNr + 1) * 100 / count;
            lbValue.Content = ((curNr + 1) * 100 / count).ToString();
            log.Items.Add(App.Current.TryFindResource("lanKey2189") + fi.Name);
            curNr++;
        }

        private void btnConfirm_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_exitEvent != null)
            {
                _exitEvent();
            }
            this.Visibility = Visibility.Hidden;
        }
    }
}
