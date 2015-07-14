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
    public partial class SPCStateCtrl : UserControl
    {
        private int _state = 0;
        public int State
        {
            set
            {
                _state = value;

                switch (_state)
                {
                    //可控
                    case 1:
                        tblState.SelectedIndex = 1;
                        break;
                    //不可控
                    case 2:
                        tblState.SelectedIndex = 2;
                        break;
                    //离线
                    default:
                        tblState.SelectedIndex = 0;
                        break;
                }
            }
        }

        public SPCStateCtrl()
        {
            InitializeComponent();
        }
    }
}
