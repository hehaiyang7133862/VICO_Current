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

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// Interaction logic for activeErr1Ctrl.xaml
    /// </summary>
    public partial class activeErr1Ctrl : UserControl
    {
        public activeErr1Ctrl()
        {
            InitializeComponent();
        }
        public string dis
        {
            get
            {
                return lbErrContent.Content.ToString();
            }
            set
            {
                lbErrContent.Content = value;
            }
        }

    }
}
