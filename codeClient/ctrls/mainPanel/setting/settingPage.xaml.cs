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
using nsVicoClient.ctrls.mainPanel;

namespace nsVicoClient.ctrls
{
    public partial class settingPage : UserControl
    {
        public settingPage()
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
                    if (valmoWin.dv.checkAccesslevel(4))
                    {
                        tbl_Switch(0);
                        valmoWin.setPangetoNr(90);
                    }
                    break;
                case 1:
                    if (valmoWin.dv.checkAccesslevel(4))
                    {
                        tbl_Switch(1);
                        valmoWin.setPangetoNr(91);
                    }
                    break;
                case 2:
                    if (valmoWin.dv.checkAccesslevel(4))
                    {
                        tbl_Switch(2);
                        valmoWin.setPangetoNr(92);
                    }
                    break;
                case 3:
                    if (valmoWin.dv.checkAccesslevel(3))
                    {
                        tbl_Switch(3);
                        valmoWin.setPangetoNr(93);
                    }
                    break;
                case 4:
                    if (valmoWin.dv.checkAccesslevel(4))
                    {
                        tbl_Switch(4);
                        valmoWin.setPangetoNr(94);
                    }
                    break;
                case 5:
                    if (valmoWin.dv.checkAccesslevel(4))
                    {
                        tbl_Switch(5);
                        valmoWin.setPangetoNr(95);
                    }
                    break;
                case 6:
                    if (valmoWin.dv.checkAccesslevel(4))
                    {
                        tbl_Switch(6);
                        valmoWin.setPangetoNr(96);
                    }
                    break;
                case 7:
                    if (valmoWin.dv.checkAccesslevel(3))
                    {
                        tbl_Switch(7);
                        valmoWin.setPangetoNr(97);
                    }
                    break;
                case 8:
                    {
                        tbl_Switch(8);
                        valmoWin.setPangetoNr(98);
                    }
                    break;
                case 9:
                    {
                        tbl_Switch(9);
                        valmoWin.setPangetoNr(99);
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
