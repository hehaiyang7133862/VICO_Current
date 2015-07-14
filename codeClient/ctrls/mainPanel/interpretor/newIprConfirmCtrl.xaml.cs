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
//using interfaceMgr;
namespace nsVicoClient.ctrls
{
    /// <summary>
    /// Interaction logic for newIprConfirmCtrl.xaml
    /// </summary>
    public partial class newIprConfirmCtrl : UserControl
    {
        public newIprConfirmCtrl()
        {
            InitializeComponent();
            this.Visibility = Visibility.Hidden;
        }
        public nullEvent newFileHandle
        {
            get;
            set;
        }
    }
}
