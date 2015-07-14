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
    public partial class dataAnalysis : UserControl
    {
        public dataAnalysis()
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
                        valmoWin.setPangetoNr(80);
                    }
                    break;
                case 1:
                    {
                        tbl_Switch(1);
                        valmoWin.setPangetoNr(81);
                    }
                    break;
                case 2:
                    {
                        tbl_Switch(2);
                        valmoWin.setPangetoNr(82);
                    }
                    break;
                case 3:
                    {
                        tbl_Switch(3);
                        valmoWin.setPangetoNr(83);
                    }
                    break;
                case 4:
                    {
                        tbl_Switch(4);
                        valmoWin.setPangetoNr(84);
                    }
                    break;
                case 5:
                    {
                        tbl_Switch(5);
                        valmoWin.setPangetoNr(85);
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
