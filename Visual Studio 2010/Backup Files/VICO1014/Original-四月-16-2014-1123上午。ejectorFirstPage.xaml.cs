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
    /// Interaction logic for ejectorFirstPage.xaml
    /// </summary>
    public partial class ejectorFirstPage : UserControl
    {
        public airStartMenuCtrl airMenu = new airStartMenuCtrl();
        /// <summary>
        /// 鼠标是否移动
        /// </summary>
        private bool _bIsMouseMove = false;
        /// <summary>
        /// 鼠标是否按下
        /// </summary>
        private bool _bIsMouseDown = false;
        /// <summary>
        /// 鼠标按下位置
        /// </summary>
        private Point pMouseDown;
        /// <summary>
        /// 鼠标当前位置
        /// </summary>
        private Point curMousePos;
        /// <summary>
        /// 当前显示图表
        /// </summary>
        private int intCurMap = 1;
        /// <summary>
        /// 曲线是否折叠
        /// </summary>
        private bool _bIsChartVisiable = true;
        /// <summary>
        /// 顶针运行设定是否折叠
        /// </summary>
        private bool _bIsEjectorSetVisiable = true;
        /// <summary>
        /// 顶针启动是否折叠
        /// </summary>
        private bool _bIsEjectorStartVisiable = true;
        /// <summary>
        /// 吹气是否折叠
        /// </summary>
        private bool _bIsBlowingVisiable = true;

        //airSetGrpCtrl airSetPanel = new airSetGrpCtrl();
        public ejectorFirstPage()
        {
            InitializeComponent();

            this.cvsMain.Children.Add(airMenu);
            Canvas.SetLeft(airMenu, 0);
            Canvas.SetTop(airMenu, 0);
            valmoWin.dv.MldPr[86].addHandle(cvsEjectorSetHead_backgroundUpdate);
            valmoWin.dv.MldPr[84].addHandle(cvsEjectorStartHead_backgroundUpdate);

            vm.testInitTmEnd("\t[ejectorNewPage]");
            init();
        }
        
        private void cvsEjectorSetHead_backgroundUpdate(objUnit obj)
        {
            cvsEjectorSetHead.Background = obj.value == 1 ?
                new SolidColorBrush(Color.FromArgb(0xFF, 0xE1, 0xF8, 0xF1)) :
                new SolidColorBrush(Color.FromArgb(0xFF, 0xFC, 0xE6, 0xE6));
        }
        private void cvsEjectorStartHead_backgroundUpdate(objUnit obj)
        {
            cvsEjectorStartHead.Background = obj.value == 1 ?
                new SolidColorBrush(Color.FromArgb(0xFF, 0xFC, 0xE6, 0xE6)) :
                new SolidColorBrush(Color.FromArgb(0xFF, 0xE1, 0xF8, 0xF1));
        }

        public void init()
        {
            reorder();

            valmoWin.dv.MldPr[68].addHandle(handleMoldPr_68);
            valmoWin.dv.MldPr[75].addHandle(handleMoldPr_75);
            valmoWin.dv.MldPr[404].addHandle(chkHandle1);
            valmoWin.dv.MldPr[411].addHandle(chkHandle2);
            valmoWin.dv.MldPr[418].addHandle(chkHandle3);
            valmoWin.dv.MldPr[425].addHandle(chkHandle4);
            valmoWin.dv.MldPr[432].addHandle(chkHandle5);
            valmoWin.dv.MldPr[439].addHandle(chkHandle6);
            valmoWin.dv.MldPr[446].addHandle(chkHandle7);
            valmoWin.dv.MldPr[453].addHandle(chkHandle8);
            valmoWin.dv.MldPr[460].addHandle(chkHandle9);
            valmoWin.dv.MldPr[467].addHandle(chkHandle10);
            valmoWin.dv.MldPr[474].addHandle(chkHandle11);
            valmoWin.dv.MldPr[481].addHandle(chkHandle12);

        }

        public void setPageToNr(int nr)
        {
            switch (nr)
            {
                case 33:
                    //airSetPanel.show();
                    break;
                case 34:

                    break;
            }
        }
        private void handleMoldPr_68(objUnit obj)
        {
            switch (obj.value)
            {
                case 1:
                    {
                        imgMoldPr_68_20_pEjector.Visibility = Visibility.Hidden;
                        imgMoldPr_68_10_pEjector.Visibility = Visibility.Hidden;
                        btnMld070.Visibility = Visibility.Hidden;
                        btnMld069.Visibility = Visibility.Hidden;
                        btnMld073.Visibility = Visibility.Hidden;
                        btnMld072.Visibility = Visibility.Hidden;
                        btnMld209.Visibility = Visibility.Hidden;
                        btnMld210.Visibility = Visibility.Hidden;
                    }
                    break;
                case 2:
                    {
                        imgMoldPr_68_20_pEjector.Visibility = Visibility.Hidden;
                        imgMoldPr_68_10_pEjector.Visibility = Visibility.Visible;
                        btnMld070.Visibility = Visibility.Hidden;
                        btnMld069.Visibility = Visibility.Visible;
                        btnMld073.Visibility = Visibility.Hidden;
                        btnMld072.Visibility = Visibility.Visible;
                        btnMld209.Visibility = Visibility.Visible;
                        btnMld210.Visibility = Visibility.Hidden;
                    }
                    break;
                case 3:
                    {
                        imgMoldPr_68_20_pEjector.Visibility = Visibility.Visible;
                        imgMoldPr_68_10_pEjector.Visibility = Visibility.Visible;
                        btnMld070.Visibility = Visibility.Visible;
                        btnMld069.Visibility = Visibility.Visible;
                        btnMld073.Visibility = Visibility.Visible;
                        btnMld072.Visibility = Visibility.Visible;
                        btnMld209.Visibility = Visibility.Visible;
                        btnMld210.Visibility = Visibility.Visible;
                    }
                    break;
                default:

                    vm.perror("get wrong value!");
                    break;

            }
            //refreshMap3();
        }
        private void handleMoldPr_75(objUnit obj)
        {
            switch (obj.value)
            {
                case 1:
                    {
                        imgMoldPr_75_1_pEjector.Visibility = Visibility.Hidden;
                        imgMoldPr_75_2_pEjector.Visibility = Visibility.Hidden;
                        btnMld076.Visibility = Visibility.Hidden;
                        btnMld077.Visibility = Visibility.Hidden;

                        btnMld079.Visibility = Visibility.Hidden;
                        btnMld080.Visibility = Visibility.Hidden;

                        btnMld206.Visibility = Visibility.Hidden;
                        btnMld207.Visibility = Visibility.Hidden;
                    }
                    break;
                case 2:
                    {
                        imgMoldPr_75_1_pEjector.Visibility = Visibility.Visible;
                        imgMoldPr_75_2_pEjector.Visibility = Visibility.Hidden;
                        btnMld076.Visibility = Visibility.Visible;
                        btnMld077.Visibility = Visibility.Hidden;

                        btnMld079.Visibility = Visibility.Visible;
                        btnMld080.Visibility = Visibility.Hidden;

                        btnMld206.Visibility = Visibility.Visible;
                        btnMld207.Visibility = Visibility.Hidden;
                    }
                    break;
                case 3:
                    {
                        imgMoldPr_75_1_pEjector.Visibility = Visibility.Visible;
                        imgMoldPr_75_2_pEjector.Visibility = Visibility.Visible;
                        btnMld076.Visibility = Visibility.Visible;
                        btnMld077.Visibility = Visibility.Visible;

                        btnMld079.Visibility = Visibility.Visible;
                        btnMld080.Visibility = Visibility.Visible;

                        btnMld206.Visibility = Visibility.Visible;
                        btnMld207.Visibility = Visibility.Visible;
                    }
                    break;
                default:
                    vm.perror("get wrong value!");
                    break;

            }
            //refreshMap4();
        }
        private void chkHandle1(objUnit obj)
        {
            state1.Opacity = obj.value;
        }
        private void chkHandle2(objUnit obj)
        {
            state2.Opacity = obj.value;
        }
        private void chkHandle3(objUnit obj)
        {
            state3.Opacity = obj.value;
        }
        private void chkHandle4(objUnit obj)
        {
            state4.Opacity = obj.value;
        }
        private void chkHandle5(objUnit obj)
        {
            state5.Opacity = obj.value;
        }
        private void chkHandle6(objUnit obj)
        {
            state6.Opacity = obj.value;
        }
        private void chkHandle7(objUnit obj)
        {
            state7.Opacity = obj.value;
        }
        private void chkHandle8(objUnit obj)
        {
            state8.Opacity = obj.value;
        }
        private void chkHandle9(objUnit obj)
        {
            state9.Opacity = obj.value;
        }
        private void chkHandle10(objUnit obj)
        {
            state10.Opacity = obj.value;
        }
        private void chkHandle11(objUnit obj)
        {
            state11.Opacity = obj.value;
        }
        private void chkHandle12(objUnit obj)
        {
            state12.Opacity = obj.value;
        }
        private void lbMoldPr_68_1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.MldPr[68].accessLevel))
                return;
            valmoWin.dv.MldPr[68].setValue((valmoWin.dv.MldPr[68].value > 1) ? 1 : 2);
        }
        private void lbMoldPr_68_2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.MldPr[68].accessLevel))
                return;
            valmoWin.dv.MldPr[68].setValue((valmoWin.dv.MldPr[68].value > 2) ? 2 : 3);
        }

        private void lbMoldPr_75_1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.MldPr[75].accessLevel))
                return;
            valmoWin.dv.MldPr[75].setValue((valmoWin.dv.MldPr[75].value > 1) ? 1 : 2);
        }

        private void lbMoldPr_75_2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.MldPr[75].accessLevel))
                return;
            valmoWin.dv.MldPr[75].setValue((valmoWin.dv.MldPr[75].value > 2) ? 2 : 3);
        }

        public void lanRefresh()
        {
        }


        private void tblMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            _bIsMouseDown = true;
            pMouseDown = e.GetPosition(tblMap);
        }

        private void tblMain_MouseMove(object sender, MouseEventArgs e)
        {
            e.Handled = true;

            if (_bIsMouseDown == true)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point curMousePos = e.GetPosition(tblMap);
                    if ((Math.Abs(curMousePos.X - pMouseDown.X) > 5) || (Math.Abs(curMousePos.Y - pMouseDown.Y) > 5))
                    {
                        _bIsMouseMove = true;
                    }
                }
            }
        }

        private void tblMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            if (_bIsMouseDown == true)
            {
                if (_bIsMouseMove == true)
                {
                    if (pMouseDown.Y > 50)
                    {
                        Point curMousePos = e.GetPosition(tblMap);
                        double lenMove = curMousePos.X - pMouseDown.X;

                        if (lenMove > 0)
                        {
                            intCurMap += 1;
                            if (intCurMap > 2)
                            {
                                intCurMap = 2;
                            }
                        }
                        else
                        {
                            intCurMap -= 1;
                            if (intCurMap < 0)
                            {
                                intCurMap = 0;
                            }
                        }

                        turnMoldMap();
                    }

                    _bIsMouseMove = false;
                }
                else
                {
                    if (pMouseDown.Y < 50)
                    {
                        if (_bIsChartVisiable == true)
                        {
                            _bIsChartVisiable = false;
                        }
                        else
                        {
                            _bIsChartVisiable = true;
                        }
                        reorder();

                        setChartVisiable(_bIsChartVisiable);
                    }
                }
                _bIsMouseDown = false;
            }
        }

        /// <summary>
        /// 切换开合模曲线
        /// </summary>
        private void turnMoldMap()
        {
            tblMap.SelectedIndex = intCurMap;
        }

        private void setChartVisiable(bool visiable)
        {
            ml.setVisibility = visiable;
            mm.setVisibility = visiable;
            mr.setVisibility = visiable;

        }

        /// <summary>
        /// 重新布局
        /// </summary>
        private void reorder()
        {
            tblMap.Height = _bIsChartVisiable ? 382 : 52;
            cvsEjectorSet.Height = _bIsEjectorSetVisiable ? 270 : 50;
            cvsEjectorStart.Height = _bIsEjectorStartVisiable ? 190 : 50;
            cvsBlowing.Height = _bIsBlowingVisiable ? 830 : 50;

            sPanelMain.Height = tblMap.Height + cvsEjectorSet.Height + cvsEjectorStart.Height + cvsBlowing.Height + 280;
        }

        private void cvsEjectorSetHead_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!_bIsMouseMove)
            {
                if (_bIsEjectorSetVisiable == true)
                {
                    _bIsEjectorSetVisiable = false;
                    imgZipedEjectorSet.Visibility = Visibility.Visible;
                    imgUnzipedEjectorSet.Visibility = Visibility.Hidden;
                }
                else
                {
                    _bIsEjectorSetVisiable = true;
                    imgZipedEjectorSet.Visibility = Visibility.Hidden;
                    imgUnzipedEjectorSet.Visibility = Visibility.Visible;
                }
                reorder();
            }
        }

        private void cvsEjectorStartHead_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!_bIsMouseMove)
            {
                if (_bIsEjectorStartVisiable == true)
                {
                    _bIsEjectorStartVisiable = false;
                    imgZipedEjectorStart.Visibility = Visibility.Visible;
                    imgUnzipedEjectorStart.Visibility = Visibility.Hidden;
                }
                else
                {
                    _bIsEjectorStartVisiable = true;
                    imgZipedEjectorStart.Visibility = Visibility.Hidden;
                    imgUnzipedEjectorStart.Visibility = Visibility.Visible;
                }
            }
            reorder();
        }

        private void cvsBlowingHead_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!_bIsMouseMove)
            {
                if (_bIsBlowingVisiable == true)
                {
                    _bIsBlowingVisiable = false;
                    imgZipedBlowing.Visibility = Visibility.Visible;
                    imgUnzipedBlowing.Visibility = Visibility.Hidden;
                }
                else
                {
                    _bIsBlowingVisiable = true;
                    imgZipedBlowing.Visibility = Visibility.Hidden;
                    imgUnzipedBlowing.Visibility = Visibility.Visible;
                }
            }
            reorder();
        }

        private void cvsmain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _bIsMouseDown = true;
            pMouseDown = e.GetPosition(cvsMain);
            curMousePos = pMouseDown;
            valmoWin.SNumKeyPanel.curPos = pMouseDown;
            valmoWin.SAttentionPanel.CurPos = pMouseDown;
        }

        private void cvsmain_MouseMove(object sender, MouseEventArgs e)
        {
            if (_bIsMouseDown == true)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point tempMousePos = e.GetPosition(cvsMain);
                    if (_bIsMouseMove == false)
                    {
                        if ((Math.Abs(tempMousePos.Y - pMouseDown.Y) > 5) || (Math.Abs(tempMousePos.X - pMouseDown.X) > 5))
                        {
                            _bIsMouseMove = true;
                        }
                    }
                    double oldTop = Canvas.GetTop(sPanelMain);
                    double newTop = tempMousePos.Y - pMouseDown.Y + oldTop;

                    if (newTop < -(sPanelMain.Height - 1210))
                        newTop = -(sPanelMain.Height - 1210);
                    if (newTop > -2)
                        newTop = -2;
                    Canvas.SetTop(sPanelMain, newTop);
                    pMouseDown = tempMousePos;
                }
            }
        }

        private void cvsmain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _bIsMouseMove = false;
            _bIsMouseDown = false;
        }

        private void airSetUnitCtrl1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point mousePosition = e.GetPosition(cvsMain);
            if (mousePosition.X > 218 && mousePosition.X < 438)
            {
                airSetUnitCtrl airUint = (airSetUnitCtrl)sender;
                double top = Canvas.GetTop(airUint) + 434 + cvsEjectorSet.Height + cvsEjectorStart.Height + Canvas.GetTop(sPanelMain);
                airMenu.show(airUint.getObjMenu(), 218, top);
            }
        }

        private void MBmouseMove(object sender, MouseEventArgs e)
        {
            if (sender.GetType() == typeof(mapBtnCtrl2))
            {
                mapBtnCtrl2 mb2 = sender as mapBtnCtrl2;
                mb2.bIsMouseMove = _bIsMouseMove;
            }
        }

        private void BSMouseMove(object sender, MouseEventArgs e)
        {
            if (sender.GetType() == typeof(btnSetCtrl))
            {
                btnSetCtrl bs = sender as btnSetCtrl;
                bs.bIsMouseMove = _bIsMouseMove;
            }
        }
    }
}
