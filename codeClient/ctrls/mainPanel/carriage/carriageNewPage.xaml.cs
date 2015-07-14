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
    public partial class carriageNewPage : UserControl
    {
        public carriageNewPage()
        {
            InitializeComponent();

            valmoWin.dv.InjPr[280].addHandle(updateInjUnitState);
            valmoWin.dv.InjPr[47].addHandle(updateNozzleState);
        }

        public void setPage(int pageNum)
        {
            valmoWin.dv.SysPr[11].valueNew = pageNum * 10 + tbMain.SelectedIndex;
        }

        private bool bIsNozzleOpen = true;
        private void updateNozzleState(objUnit obj)
        {
            if (obj.value == 1)
            {
                cvsNozzle.Width = 90;
                bIsNozzleOpen = true;
            }
            else
            {
                cvsNozzle.Width = 0;
                bIsNozzleOpen = false;
            }

            tblBtnCtrl.itemCount = 4 + (bIsInjUnitOpen ? 1 : 0) + (bIsNozzleOpen ? 1 : 0);
            tblBtnCtrl.focusNr = 0;
            tbMain.SelectedIndex = 0;
        }

        private bool bIsInjUnitOpen = true;
        private void updateInjUnitState(objUnit obj)
        {
            if (obj.value == 1)
            {
                cvsInjUnit.Width = 90;
                bIsInjUnitOpen = true;
            }
            else
            {
                cvsInjUnit.Width = 0;
                bIsInjUnitOpen = false;
            }

            tblBtnCtrl.itemCount = 4 + (bIsInjUnitOpen ? 1 : 0) + (bIsNozzleOpen ? 1 : 0);
            tblBtnCtrl.focusNr = 0;
            tbMain.SelectedIndex = 0;
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
                        valmoWin.setPangetoNr(60);
                        tbMain.SelectedIndex = 0;
                    }
                    break;
                case 1:
                    {
                        tbl_Switch(1);
                        valmoWin.setPangetoNr(61);
                        tbMain.SelectedIndex = 1;
                    }
                    break;
                case 2:
                    {
                        tbl_Switch(2);
                        valmoWin.setPangetoNr(62);
                        tbMain.SelectedIndex = 2;
                    }
                    break;
                case 3:
                    {
                        tbl_Switch(3);
                        valmoWin.setPangetoNr(63);
                        tbMain.SelectedIndex = 3;
                    }
                    break;
                case 4:
                    {
                        if (bIsInjUnitOpen == true)
                        {
                            tbl_Switch(4);
                        }
                        else
                        {
                            tbl_Switch(3);
                        }
                        valmoWin.setPangetoNr(64);
                        tbMain.SelectedIndex = 4;
                    }
                    break;
                case 5:
                    {
                        if (bIsInjUnitOpen == true)
                        {
                            tbl_Switch(5);
                        }
                        else
                        {
                            tbl_Switch(4);
                        }
                        valmoWin.setPangetoNr(65);
                        tbMain.SelectedIndex = 5;
                    }
                    break;
                default:
                    break;
            }
        }

        public void tbl_Switch(int selectedIndex)
        {
            tblBtnCtrl.focusNr = selectedIndex;
        }
    }
}
