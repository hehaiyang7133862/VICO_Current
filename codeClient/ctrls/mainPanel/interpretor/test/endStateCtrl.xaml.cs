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
    /// Interaction logic for endStateCtrl.xaml
    /// </summary>
    public partial class endStateCtrl : UserControl
    {
        public endStateCtrl()
        {
            InitializeComponent();
        }
        public bool mldState
        {
            get
            {
                if (pgRight.Fill == Brushes.Brown)
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                {
                    pgRight.Opacity = 1;
                    rctRight.Opacity = 1;
                    pgRight.Fill = Brushes.Brown;
                    rctRight.Fill = Brushes.Brown;
                }
                else
                {
                    pgRight.Opacity = 0;
                    rctRight.Opacity = 0;
                    pgRight.Fill = Brushes.Red;
                    rctRight.Fill = Brushes.Red;
                }

            }
        }
        public bool upState
        {
            get
            {
                if (pgUp.Fill == Brushes.Brown)
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                {
                    pgUp.Opacity = 1;
                    rctUp.Opacity = 1;
                    pgUp.Fill = Brushes.Brown;
                    rctUp.Fill = Brushes.Brown;
                }
                else
                {
                    pgUp.Opacity = 0;
                    rctUp.Opacity = 0;
                    pgUp.Fill = Brushes.Red;
                    rctUp.Fill = Brushes.Red;
                }
            }
        }
        public bool downState
        {
            get
            {
                if (pgDown.Fill == Brushes.Brown)
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                {
                    pgDown.Opacity = 1;
                    rctDown.Opacity = 1;
                    pgDown.Fill = Brushes.Brown;
                    rctDown.Fill = Brushes.Brown;
                }
                else
                {
                    pgDown.Opacity = 0;
                    rctDown.Opacity = 0;
                    pgDown.Fill = Brushes.Red;
                    rctDown.Fill = Brushes.Red;
                }
            }
        }
    }
}
