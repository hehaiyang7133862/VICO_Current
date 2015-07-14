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

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// Interaction logic for overViewPage.xaml
    /// </summary>
    public partial class overViewPage : UserControl
    {
        public overViewPage()
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
                        valmoWin.setPangetoNr(0);
                    }
                    break;
                case 1:
                    {
                        tbl_Switch(1);
                        valmoWin.setPangetoNr(1);
                    }
                    break;
                case 2:
                    {
                        tbl_Switch(2);
                        valmoWin.setPangetoNr(2);
                    }
                    break;
                case 3:
                    {
                        tbl_Switch(3);
                        valmoWin.setPangetoNr (3);
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
