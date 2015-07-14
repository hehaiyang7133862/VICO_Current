using System;
using System.Runtime.InteropServices;
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
using nsVicoClient;
using nsDataMgr;

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// Interaction logic for moldOnOffNewPage.xaml
    /// </summary>
    public partial class moldOnOffNewPage : UserControl
    {
        public moldOnOffNewPage()
        {
            InitializeComponent();
        }

        public void setPage(int pageNum)
        {
            valmoWin.dv.SysPr[11].valueNew = pageNum * 10 + tbMain.SelectedIndex;
        }

        private void HeadSwitch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int Index =
                Convert.ToInt32((sender as Canvas).Tag.ToString());
            switch (Index)
            {
                case 0:
                    {
                        tbl_Switch(0);
                        valmoWin.setPangetoNr(20);
                    }
                    break;
                case 1:
                    {
                        tbl_Switch(1);
                        valmoWin.setPangetoNr(21);
                    }
                    break;
                case 2:
                    {
                        tbl_Switch(2);
                        valmoWin.setPangetoNr(22);
                    }
                    break;
                default:
                    break;
            }
        }

        private void tbl_Switch(int selectedIndex)
        {
            tblBtnCtrl.focusNr = selectedIndex;
            tbMain.SelectedIndex = selectedIndex;
        }
    }
}
