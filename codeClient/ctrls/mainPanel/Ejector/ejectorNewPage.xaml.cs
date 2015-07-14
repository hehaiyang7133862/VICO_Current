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
    public partial class ejectorNewPage : UserControl
    {
        public ejectorNewPage()
        {
            InitializeComponent();

            valmoWin.dv.MldPr[560].addHandle(updateTunetableControlMode);

            valmoWin.dv.MldPr[262].addHandle(updateCore1State);
            valmoWin.dv.MldPr[275].addHandle(updateCore1State);
            valmoWin.dv.MldPr[288].addHandle(updateCore1State);
            valmoWin.dv.MldPr[301].addHandle(updateCore2State);
            valmoWin.dv.MldPr[314].addHandle(updateCore2State);
            valmoWin.dv.MldPr[327].addHandle(updateCore2State);
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
                        tblBtnCtrl.focusNr = 0;
                        valmoWin.setPangetoNr(30);
                    }
                    break;
                case 1:
                    {
                        tbl_Switch(1);
                        tblBtnCtrl.focusNr = 1;
                        valmoWin.setPangetoNr(31);
                    }
                    break;
                case 2:
                    {
                        tbl_Switch(2);
                        tblBtnCtrl.focusNr = 2;
                        valmoWin.setPangetoNr(32);
                    }
                    break;
                case 3:
                    {
                        tbl_Switch(3);
                        if (_bIsCore1Visiable == true)
                        {
                            tblBtnCtrl.focusNr = 3;
                        }
                        else
                        {
                            tblBtnCtrl.focusNr = 2;
                        }
                        valmoWin.setPangetoNr(33);
                    }
                    break;
                case 4:
                    {
                        tbl_Switch(4);
                        tblBtnCtrl.focusNr = 4 - (5 - pageCount);
                        valmoWin.setPangetoNr(34);
                    }
                    break;
                default:
                    break;
            }
        }

        private void tbl_Switch(int selectedIndex)
        {
            tbMain.SelectedIndex = selectedIndex;
        }

        private bool _bIsCore1Visiable = false;
        private void updateCore1State(objUnit obj)
        {
            if (valmoWin.dv.MldPr[262].value == 1 || valmoWin.dv.MldPr[275].value == 1 || valmoWin.dv.MldPr[288].value == 1)
            {
                _bIsCore1Visiable = true;
            }
            else
            {
                _bIsCore1Visiable = false;
            }

            refush();
        }
        private bool _bIsCore2Visiable = false;
        private void updateCore2State(objUnit obj)
        {
            if (valmoWin.dv.MldPr[301].value == 1 || valmoWin.dv.MldPr[314].value == 1 || valmoWin.dv.MldPr[327].value == 1)
            {
                _bIsCore2Visiable = true;
            }
            else
            {
                _bIsCore2Visiable = false;
            }

            refush();
        }
        private bool _bIsTuneTableVisiable = false;
        private void updateTunetableControlMode(objUnit obj)
        {
            if (obj.value == 1 || obj.value == 2)
            {
                _bIsTuneTableVisiable = true;
            }
            else
            {
                _bIsTuneTableVisiable = false;
            }

            refush();
        }

        private int pageCount = 0;
        private void refush()
        {
            int count = 2;

            if (_bIsCore1Visiable == true)
            {
                count++;
                cvsCore1.Visibility = Visibility.Visible;
                Canvas.SetLeft(cvsCore1, (count - 3) * 85 + 310);
            }
            else
            {
                cvsCore1.Visibility = Visibility.Hidden;
            }

            if (_bIsCore2Visiable == true)
            {
                count++;
                cvsCore2.Visibility = Visibility.Visible;
                Canvas.SetLeft(cvsCore2, (count - 3) * 85 + 310);
            }
            else
            {
                cvsCore2.Visibility = Visibility.Hidden;
            }

            if (_bIsTuneTableVisiable == true)
            {
                count++;
                cvsTunetable.Visibility = Visibility.Visible;
                Canvas.SetLeft(cvsTunetable, (count - 3) * 85 + 310);
            }
            else
            {
                cvsTunetable.Visibility = Visibility.Hidden;
            }

            tblBtnCtrl.itemCount = count;
            tblBtnCtrl.focusNr = 0;
            tbMain.SelectedIndex = 0;

            pageCount = count;
        }
    }
}
