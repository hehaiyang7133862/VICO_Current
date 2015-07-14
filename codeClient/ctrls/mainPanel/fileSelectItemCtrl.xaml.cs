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
    /// Interaction logic for fileSelectItemCtrl.xaml
    /// </summary>
    public partial class fileSelectItemCtrl : UserControl
    {
        public fileSelectItemCtrl()
        {
            InitializeComponent();
        }

        public bool state
        {
            get
            {
                return imgAct.Opacity == 1;
            }
            set
            {
                imgAct.Opacity = value ? 1 : 0;
                //checkBox.active = true;
            }
        }

        private void bdMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            state = !state;
        }
    }
}
