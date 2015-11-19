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
    /// Mold_Curve.xaml 的交互逻辑
    /// </summary>
    public partial class Mold_Curve : UserControl
    {
        /// <summary>
        /// 鼠标是否按下
        /// </summary>
        private bool _bIsMouseDown = false;
        /// <summary>
        /// 鼠标是否移动
        /// </summary>
        private bool _bIsMouseMove = false;
        /// <summary>
        /// 鼠标在曲线区域按下
        /// </summary>
        private bool _bIsMouseDownCurve = false;
        /// <summary>
        /// 鼠标按下位置
        /// </summary>
        private Point _pMouseDown;
        /// <summary>
        /// 当前鼠标位置
        /// </summary>
        private Point curMousePos;
        /// <summary>
        /// 当前显示图表
        /// </summary>
        private int intCurMap = 1;
        /// <summary>
        /// 开合模曲线是否折叠
        /// </summary>
        private bool _bIsChartVisiable = true;
        /// <summary>
        /// 模保吨位
        /// </summary>
        private List<int> lst_Tonnage_MoldClamp = new List<int>();
        /// <summary>
        /// 模保时间
        /// </summary>
        private List<int> lst_Time_MoldClamp = new List<int>();

        public Mold_Curve()
        {
            InitializeComponent();     

            valmoWin.dv.MldPr[13].addHandle(handleMoldPr_13);
            valmoWin.dv.MldPr[26].addHandle(handleMoldPr_26);

            valmoWin.dv.PrdPr[282].addHandle(MoldClampRefush);
            valmoWin.dv.MldPr[225].addHandle(handleMldPr_225);
            valmoWin.dv.MldPr[150].addHandle(updateAutoMoldAdjustState);
        }

        private void handleMldPr_225(objUnit obj)
        {
            setClampForceStaff(obj.vDbl * 1.1);
        }
        private void setClampForceStaff(double tmp)
        {
            lbStaff_moldClamp_F1.Content = (tmp * 1 / 11).ToString("0.00");
            lbStaff_moldClamp_F2.Content = (tmp * 2 / 11).ToString("0.00");
            lbStaff_moldClamp_F3.Content = (tmp * 3 / 11).ToString("0.00");
            lbStaff_moldClamp_F4.Content = (tmp * 4 / 11).ToString("0.00");
            lbStaff_moldClamp_F5.Content = (tmp * 5 / 11).ToString("0.00");
            lbStaff_moldClamp_F6.Content = (tmp * 6 / 11).ToString("0.00");
            lbStaff_moldClamp_F7.Content = (tmp * 7 / 11).ToString("0.00");
            lbStaff_moldClamp_F8.Content = (tmp * 8 / 11).ToString("0.00");
            lbStaff_moldClamp_F9.Content = (tmp * 9 / 11).ToString("0.00");
            lbStaff_moldClamp_F10.Content = (tmp * 10 / 11).ToString("0.00");
            lbStaff_moldClamp_F11.Content = (tmp * 11 / 11).ToString("0.00");

        }

        private void MoldClampRefush(objUnit obj)
        {
            int maxMoldClamp = valmoWin.dv.MldPr[225].value;

            if (maxMoldClamp > 0)
            {

                int count = obj.value;

                int[] RawData = new int[count * 4];
                Lasal32.GetData(RawData, (uint)valmoWin.dv.PrdPr[281].valueNew, count * 16);

                List<Point> curveData_Time_Ton = new List<Point>();

                for (int i = 0; i < count; i++)
                {
                    double time = RawData[i * 4 + 3] * 1.0 / 1000 / 5 * 10000;
                    double Ton = RawData[i * 4] * 1.0 / maxMoldClamp * 10000;

                    curveData_Time_Ton.Add(new Point(time, 10000 - Ton));
                }
                curveMoldClamp.refushCurve(curveData_Time_Ton);
            }
        }

        private void handleMoldPr_13(objUnit obj)
        {
            switch (obj.value)
            {
                case 3:
                    {

                        imgMoldPr_13_30_pMoldOnOff.Visibility = Visibility.Hidden;
                        imgMoldPr_13_40_pMoldOnOff.Visibility = Visibility.Hidden;
                        imgMoldPr_13_50_pMoldOnOff.Visibility = Visibility.Hidden;

                        lbMld018.Visibility = Visibility.Hidden;
                        lbMld017.Visibility = Visibility.Hidden;
                        lbMld016.Visibility = Visibility.Hidden;

                        lbMld022.Visibility = Visibility.Hidden;
                        lbMld024.Visibility = Visibility.Hidden;
                        lbMld023.Visibility = Visibility.Hidden;
                    }
                    break;
                case 4:
                    {

                        imgMoldPr_13_30_pMoldOnOff.Visibility = Visibility.Visible;
                        imgMoldPr_13_40_pMoldOnOff.Visibility = Visibility.Hidden;
                        imgMoldPr_13_50_pMoldOnOff.Visibility = Visibility.Hidden;

                        lbMld018.Visibility = Visibility.Hidden;
                        lbMld017.Visibility = Visibility.Hidden;
                        lbMld016.Visibility = Visibility.Visible;

                        lbMld023.Visibility = Visibility.Hidden;
                        lbMld024.Visibility = Visibility.Hidden;
                        lbMld022.Visibility = Visibility.Visible;
                    }
                    break;
                case 5:
                    {
                        imgMoldPr_13_30_pMoldOnOff.Visibility = Visibility.Visible;
                        imgMoldPr_13_40_pMoldOnOff.Visibility = Visibility.Visible;
                        imgMoldPr_13_50_pMoldOnOff.Visibility = Visibility.Hidden;

                        lbMld018.Visibility = Visibility.Hidden;
                        lbMld017.Visibility = Visibility.Visible;
                        lbMld016.Visibility = Visibility.Visible;

                        lbMld024.Visibility = Visibility.Hidden;
                        lbMld022.Visibility = Visibility.Visible;
                        lbMld023.Visibility = Visibility.Visible;
                    }
                    break;
                case 6:
                    {

                        imgMoldPr_13_30_pMoldOnOff.Visibility = Visibility.Visible;
                        imgMoldPr_13_40_pMoldOnOff.Visibility = Visibility.Visible;
                        imgMoldPr_13_50_pMoldOnOff.Visibility = Visibility.Visible;

                        lbMld018.Visibility = Visibility.Visible;
                        lbMld017.Visibility = Visibility.Visible;
                        lbMld016.Visibility = Visibility.Visible;

                        lbMld022.Visibility = Visibility.Visible;
                        lbMld024.Visibility = Visibility.Visible;
                        lbMld023.Visibility = Visibility.Visible;
                    }
                    break;
            }
        }
        private void handleMoldPr_26(objUnit obj)
        {
            switch (obj.value)
            {
                case 3:
                    {
                        imgMoldPr_26_20_pMoldOnOff.Visibility = Visibility.Hidden;
                        imgMoldPr_26_30_pMoldOnOff.Visibility = Visibility.Hidden;
                        imgMoldPr_26_40_pMoldOnOff.Visibility = Visibility.Hidden;

                        lbMld027.Visibility = Visibility.Hidden;
                        lbMld028.Visibility = Visibility.Hidden;
                        lbMld029.Visibility = Visibility.Hidden;

                        lbMld033.Visibility = Visibility.Hidden;
                        lbMld034.Visibility = Visibility.Hidden;
                        lbMld035.Visibility = Visibility.Hidden;
                    }
                    break;
                case 4:
                    {
                        imgMoldPr_26_20_pMoldOnOff.Visibility = Visibility.Visible;
                        imgMoldPr_26_30_pMoldOnOff.Visibility = Visibility.Hidden;
                        imgMoldPr_26_40_pMoldOnOff.Visibility = Visibility.Hidden;

                        lbMld027.Visibility = Visibility.Visible;
                        lbMld028.Visibility = Visibility.Hidden;
                        lbMld029.Visibility = Visibility.Hidden;

                        lbMld033.Visibility = Visibility.Visible;
                        lbMld034.Visibility = Visibility.Hidden;
                        lbMld035.Visibility = Visibility.Hidden;
                    }
                    break;
                case 5:
                    {
                        imgMoldPr_26_20_pMoldOnOff.Visibility = Visibility.Visible;
                        imgMoldPr_26_30_pMoldOnOff.Visibility = Visibility.Visible;
                        imgMoldPr_26_40_pMoldOnOff.Visibility = Visibility.Hidden;

                        lbMld027.Visibility = Visibility.Visible;
                        lbMld028.Visibility = Visibility.Visible;
                        lbMld029.Visibility = Visibility.Hidden;

                        lbMld033.Visibility = Visibility.Visible;
                        lbMld034.Visibility = Visibility.Visible;
                        lbMld035.Visibility = Visibility.Hidden;
                    }
                    break;
                case 6:
                    {
                        imgMoldPr_26_20_pMoldOnOff.Visibility = Visibility.Visible;
                        imgMoldPr_26_30_pMoldOnOff.Visibility = Visibility.Visible;
                        imgMoldPr_26_40_pMoldOnOff.Visibility = Visibility.Visible;

                        lbMld027.Visibility = Visibility.Visible;
                        lbMld028.Visibility = Visibility.Visible;
                        lbMld029.Visibility = Visibility.Visible;

                        lbMld033.Visibility = Visibility.Visible;
                        lbMld034.Visibility = Visibility.Visible;
                        lbMld035.Visibility = Visibility.Visible;
                    }
                    break;
            }
        }

        private void lbMoldPr_13_3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.MldPr[13].accessLevel) || _bIsMouseMove)
                return;
            if (NewMessageBox.Show() == true)
            {
                valmoWin.dv.MldPr[13].setValue((valmoWin.dv.MldPr[13].valueNew <= 3) ? 4 : 3);
            }
        }
        private void lbMoldPr_13_4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.MldPr[13].accessLevel) || _bIsMouseMove)
                return;
            if (NewMessageBox.Show() == true)
            {
                valmoWin.dv.MldPr[13].setValue((valmoWin.dv.MldPr[13].valueNew <= 4) ? 5 : 4);
            }
        }
        private void lbMoldPr_13_5_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.MldPr[13].accessLevel) || _bIsMouseMove)
                return;
            if (NewMessageBox.Show() == true)
            {
                valmoWin.dv.MldPr[13].setValue((valmoWin.dv.MldPr[13].valueNew <= 5) ? 6 : 5);
            }
        }
        private void lbMoldPr_26_2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.MldPr[26].accessLevel) || _bIsMouseMove)
                return;
            if (NewMessageBox.Show() == true)
            {
                valmoWin.dv.MldPr[26].setValue((valmoWin.dv.MldPr[26].valueNew <= 3) ? 4 : 3);
            }
        }
        private void lbMoldPr_26_3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.MldPr[26].accessLevel) || _bIsMouseMove)
                return;
            if (NewMessageBox.Show() == true)
            {
                valmoWin.dv.MldPr[26].setValue((valmoWin.dv.MldPr[26].valueNew <= 4) ? 5 : 4);
            }
        }
        private void lbMoldPr_26_4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.MldPr[26].accessLevel) || _bIsMouseMove)
                return;
            if (NewMessageBox.Show() == true)
            {
                valmoWin.dv.MldPr[26].setValue((valmoWin.dv.MldPr[26].valueNew <= 5) ? 6 : 5);
            }
        }

        private void tblMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            _bIsMouseDownCurve = true;
            _pMouseDown = e.GetPosition(tblMap);
        }
        private void tblMain_MouseMove(object sender, MouseEventArgs e)
        {
            e.Handled = true;

            if (_bIsMouseDownCurve == true)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point curMousePos = e.GetPosition(tblMap);
                    if ((Math.Abs(curMousePos.X - _pMouseDown.X) > 5) || (Math.Abs(curMousePos.Y - _pMouseDown.Y) > 5))
                    {
                        _bIsMouseMove = true;
                    }
                }
            }
        }
        private void tblMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            if (_bIsMouseDownCurve == true)
            {
                if (_bIsMouseMove == true)
                {
                    if (_pMouseDown.Y > 50)
                    {
                        Point curMousePos = e.GetPosition(tblMap);
                        double lenMove = curMousePos.X - _pMouseDown.X;

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
                    if (_pMouseDown.Y < 50)
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
                _bIsMouseDownCurve = false;
            }
        }

        private void cvsmain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _bIsMouseDown = true;
            _pMouseDown = e.GetPosition(cvsMain);
            curMousePos = _pMouseDown;
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
                        if ((Math.Abs(tempMousePos.Y - _pMouseDown.Y) > 5) || (Math.Abs(tempMousePos.X - _pMouseDown.X) > 5))
                        {
                            _bIsMouseMove = true;
                        }
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
        private void cvsmain_MouseLeave(object sender, MouseEventArgs e)
        {
            _bIsMouseDown = false;
            _bIsMouseMove = false;
        }

        /// <summary>
        /// 重新计算高度
        /// </summary>
        private void reorder()
        {
            tblMap.Height = _bIsChartVisiable ? 380 : 50;
            cvsMoldAjust.Height = _bIsMoldAjustVisiable ? 280 : 50;
            cvsMoldclamp.Height = _bIsMoldclampVisiable ? 520 : 50;

            sPanelMain.Height = tblMap.Height + cvsMoldAjust.Height + cvsMoldclamp.Height + 330;
        }
        /// <summary>
        /// 切换开合模曲线
        /// </summary>
        private void turnMoldMap()
        {
            tblMap.SelectedIndex = intCurMap;
            switch (intCurMap)
            {
                case 0:
                    {
                        ml.useAble = true;
                        mm.useAble = false;
                        mr.useAble = false;
                    }
                    break;
                case 1:
                    {
                        ml.useAble = false;
                        mm.useAble = true;
                        mr.useAble = false;
                    }
                    break;
                case 2:
                    {
                        ml.useAble = false;
                        mm.useAble = false;
                        mr.useAble = true;
                    }
                    break;
            }

        }

        private void setChartVisiable(bool visiable)
        {
            ml.setVisibility = visiable;
            mm.setVisibility = visiable;
            mr.setVisibility = visiable;
        }

        private void MBmouseMove(object sender, MouseEventArgs e)
        {
        }
        private void BSMouseMove(object sender, MouseEventArgs e)
        {
        }

        private bool bIsMouseDownAutoMoldAdjust = false;

        private void AutoMoldAdjust_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            bIsMouseDownAutoMoldAdjust = true;
        }

        private void updateAutoMoldAdjustState(objUnit obj)
        {
            if (obj.value == 1)
            {
                lbAutoMoldAdjut.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                lbAutoMoldAdjut.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0xE1, 0x5A));
            }
        }

        private void AutoMoldAdjust_MouseUp(object sender, MouseButtonEventArgs e)
        {

            if (bIsMouseDownAutoMoldAdjust == true)
            {
                valmoWin.dv.MldPr[150].setValue(valmoWin.dv.MldPr[150].valueNew == 1 ? 0 : 1);
            }
            bIsMouseDownAutoMoldAdjust = false;
        }

        private void AutoMoldAdjust_MouseLeave(object sender, MouseEventArgs e)
        {
            bIsMouseDownAutoMoldAdjust = false;
        }

        private bool _bIsMoldAjustVisiable = true;
        private void cvsMoldAjustHead_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!_bIsMouseMove)
            {
                if (_bIsMoldAjustVisiable == true)
                {
                    _bIsMoldAjustVisiable = false;
                    imgZipedMoldAjust.Visibility = Visibility.Visible;
                    imgUnzipedMoldAjust.Visibility = Visibility.Hidden;
                }
                else
                {
                    _bIsMoldAjustVisiable = true;
                    imgZipedMoldAjust.Visibility = Visibility.Hidden;
                    imgUnzipedMoldAjust.Visibility = Visibility.Visible;
                }
            }
            reorder();
        }

        private bool _bIsMoldclampVisiable = true;
        private void cvsHeadMoldclamp_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!_bIsMouseMove)
            {
                if (_bIsMoldclampVisiable == true)
                {
                    _bIsMoldclampVisiable = false;
                    imgZipedMoldclamp.Visibility = Visibility.Visible;
                    imgUnzipedMoldclamp.Visibility = Visibility.Hidden;
                }
                else
                {
                    _bIsMoldclampVisiable = true;
                    imgZipedMoldclamp.Visibility = Visibility.Hidden;
                    imgUnzipedMoldclamp.Visibility = Visibility.Visible;
                }
            }
            reorder();
        }

        private void tblMap_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch ((sender as TabControl).SelectedIndex)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                default:
                    break;
            }
        }
    }
}
