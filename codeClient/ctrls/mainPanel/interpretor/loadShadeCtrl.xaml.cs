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
    /// Interaction logic for loadShadeCtrl.xaml
    /// </summary>
    public partial class loadShadeCtrl : UserControl
    {
        public loadShadeCtrl()
        {
            InitializeComponent();
        }

        private void cvsMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if( valmoWin.dv.checkAccesslevel(3))
                this.Visibility = Visibility.Hidden;
        }
        public void hide()
        {
            this.Visibility = Visibility.Hidden;
        }
        public void show()
        {
            this.Opacity = 1;
            this.Visibility = Visibility.Visible;

        }
    }
}
