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
    /// Interaction logic for lightRightCtrl.xaml
    /// </summary>
    public partial class lightRightCtrl : UserControl
    {
        public lightRightCtrl()
        {
            InitializeComponent();
        }
        public bool state
        {
            get
            {
                if (pg.Fill == Brushes.Brown)
                    return true;
                else
                    return false;
            }

            set
            {
                if (value)
                {
                    //pg.Opacity = 1;
                    //rct.Opacity = 1;
                    pg.Fill = Brushes.Brown;
                    rct.Fill = Brushes.Brown;
                }
                else
                {
                    //pg.Opacity = 0;
                    //rct.Opacity = 0;
                    pg.Fill = Brushes.Silver;
                    rct.Fill = Brushes.Silver;
                }
            }
        }

    }
}
