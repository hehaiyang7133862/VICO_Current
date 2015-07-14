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

        public ejectorFirstPage()
        {
            InitializeComponent();

            valmoWin.dv.MldPr[86].addHandle(cvsEjectorSetHead_backgroundUpdate);
            valmoWin.dv.MldPr[84].addHandle(cvsEjectorStartHead_backgroundUpdate);

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
     
        private void lbMoldPr_68_1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.MldPr[68].accessLevel) || _bIsMouseMove)
                return;
            valmoWin.dv.MldPr[68].setValue((valmoWin.dv.MldPr[68].value > 1) ? 1 : 2);
        }
        private void lbMoldPr_68_2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.MldPr[68].accessLevel) || _bIsMouseMove)
                return;
            valmoWin.dv.MldPr[68].setValue((valmoWin.dv.MldPr[68].value > 2) ? 2 : 3);
        }

        private void lbMoldPr_75_1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.MldPr[75].accessLevel) || _bIsMouseMove)
                return;
            valmoWin.dv.MldPr[75].setValue((valmoWin.dv.MldPr[75].value > 1) ? 1 : 2);
        }
        private void lbMoldPr_75_2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.MldPr[75].accessLevel) || _bIsMouseMove)
                return;
            valmoWin.dv.MldPr[75].setValue((valmoWin.dv.MldPr[75].value > 2) ? 2 : 3);
        }

        private bool _bIsMouseDownMap = false;
        private void tblMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            _bIsMouseDownMap = true;
            pMouseDown = e.GetPosition(cvsMain);
        }

        private void tblMain_MouseMove(object sender, MouseEventArgs e)
        {
            e.Handled = true;
            if (_bIsMouseDownMap == true)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point curMousePos = e.GetPosition(cvsMain);
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
            if (_bIsMouseDownMap == true)
            {
                _bIsMouseDownMap = false;
                if (_bIsMouseMove == true)
                {
                    _bIsMouseMove = false;
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
            tblMap.Height = _bIsChartVisiable ? 380 : 50;
            cvsEjectorSet.Height = _bIsEjectorSetVisiable ? 280 : 50;
            cvsEjectorStart.Height = _bIsEjectorStartVisiable ? 200 : 50;
            cvsSpecial.Height = _bIsSpecialVisiable ? 200 : 50;

            sPanelMain.Height = tblMap.Height + cvsEjectorSet.Height + cvsEjectorStart.Height + cvsSpecial.Height + 280;
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

        private void cvsmain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _bIsMouseDown = true;
            pMouseDown = e.GetPosition(cvsMain);
            curMousePos = pMouseDown;
        }

        private void cvsmain_MouseMove(object sender, MouseEventArgs e)
        {
            if (_bIsMouseDown == true)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point tempMousePos = e.GetPosition(cvsMain);
                        if ((Math.Abs(tempMousePos.Y - pMouseDown.Y) > 5) || (Math.Abs(tempMousePos.X - pMouseDown.X) > 5))
                        {
                            _bIsMouseMove = true;
                        }

                    double oldTop = Canvas.GetTop(sPanelMain);
                    double newTop = tempMousePos.Y - curMousePos.Y + oldTop;

                    if (newTop <= -(sPanelMain.Height - (valmoWin.MainPanelHeight - 195)) - 20)
                        newTop = -(sPanelMain.Height - (valmoWin.MainPanelHeight - 195)) - 20;
                    if (newTop > 0)
                        newTop = 0;
                    Canvas.SetTop(sPanelMain, newTop);
                    curMousePos = tempMousePos;
                }
            }
        }

        private void cvsmain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _bIsMouseMove = false;
            _bIsMouseDown = false;
        }

        private void cvsMain_MouseLeave(object sender, MouseEventArgs e)
        {
            _bIsMouseDown = false;
            _bIsMouseMove = false;
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
        }

        private bool _bIsSpecialVisiable = true;
        private void cvsSpecialHead_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!_bIsMouseMove)
            {
                if (_bIsSpecialVisiable == true)
                {
                    _bIsSpecialVisiable = false;
                    imgZipedSpecial.Visibility = Visibility.Visible;
                    imgUnzipedSpecial.Visibility = Visibility.Hidden;
                }
                else
                {
                    _bIsSpecialVisiable = true;
                    imgZipedSpecial.Visibility = Visibility.Hidden;
                    imgUnzipedSpecial.Visibility = Visibility.Visible;
                }
            }
            reorder();

        }

    }
}
