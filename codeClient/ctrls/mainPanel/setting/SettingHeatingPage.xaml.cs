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
    /// SettingHeatingPage.xaml 的交互逻辑
    /// </summary>
    public partial class SettingHeatingPage : UserControl
    {
        objUnit objSelect = valmoWin.dv.TmpPr[165];
        thermoSettingPanel settingPanel = new thermoSettingPanel();
        public SettingHeatingPage()
        {
            InitializeComponent();

            cvsMain.Children.Add(settingPanel);
            settingPanel.setPos(-10, 121);
            settingPanel.Visibility = Visibility.Hidden;
            reorder();
        }

        thermoSettingUnitCtrl curUnit;
        private void thermoSettingUnitCtrl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_bIsMouseMove == false)
            {
                curUnit = sender as thermoSettingUnitCtrl;
                curUnit.focus = true;
                objSelect.setValue((sender as thermoSettingUnitCtrl).serNr);
                settingPanel.show((sender as thermoSettingUnitCtrl).serNr, hideFocus);
            }
        }

        private void hideFocus(int value)
        {
            if (curUnit != null)
                curUnit.focus = false;
        }
        private bool _bIsMouseDown = false;
        private void btnTune_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _bIsMouseDown = true;
            btnTune.Background = Brushes.Blue;
        }

        private void btnTune_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_bIsMouseDown == true)
            {
                _bIsMouseDown = false;
                btnTune.Background = new SolidColorBrush(Color.FromArgb(0xff, 0x00, 0xb4, 0xe1));
                valmoWin.dv.TmpPr[71].setValue((valmoWin.dv.TmpPr[71].valueNew == 0) ? 1 : 0);
            }
        }
                
        /// <summary>
        /// 重新布局
        /// </summary>
        private void reorder()
        {
            cvsHeatingZoneControl1.Height = (_bIsHeatingZoneControl1Visiable == true) ? 150 : 50;
            cvsHeatingZoneControl2.Height = (_bIsHeatingZoneControl2Visiable == true) ? 150 : 50;
            cvsHeatingZoneControl3.Height = (_bIsHeatingZoneControl3Visiable == true) ? 150 : 50;
            cvsHeatingZoneControl4.Height = (_bIsHeatingZoneControl4Visiable == true) ? 150 : 50;
            cvsHeatingZoneControl5.Height = (_bIsHeatingZoneControl5Visiable == true) ? 150 : 50;
            cvsHeatingZoneControl6.Height = (_bIsHeatingZoneControl6Visiable == true) ? 150 : 50;
            cvsMachineTempLimit.Height = (_bIsMachineTempLimitVisiable == true) ? 660 : 50;
            cvsMoldHeatZone.Height = (_bIsMoldHeatZoneVisiable == true) ? 1400 : 50;

            sPanelMain.Height = cvsHeatingZoneControl1.Height + cvsHeatingZoneControl1.Height + cvsHeatingZoneControl1.Height +
                cvsHeatingZoneControl1.Height + cvsHeatingZoneControl1.Height + cvsHeatingZoneControl1.Height +
                cvsMachineTempLimit.Height + cvsMoldHeatZone.Height + 150;
        }

        private bool _bIsHeatingZoneControl1Visiable = false;
        private void cvsHeatingZoneControl1Head_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!_bIsMouseMove)
            {
                if (_bIsHeatingZoneControl1Visiable == true)
                {
                    _bIsHeatingZoneControl1Visiable = false;
                    imgZipedHeatingZoneControl1.Visibility = Visibility.Visible;
                    imgUnzipedHeatingZoneControl1.Visibility = Visibility.Hidden;
                }
                else
                {
                    _bIsHeatingZoneControl1Visiable = true;
                    imgZipedHeatingZoneControl1.Visibility = Visibility.Hidden;
                    imgUnzipedHeatingZoneControl1.Visibility = Visibility.Visible;
                }
            }
            reorder();
        }
        private bool _bIsHeatingZoneControl2Visiable = false;
        private void cvsHeatingZoneControl2Head_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!_bIsMouseMove)
            {
                if (_bIsHeatingZoneControl2Visiable == true)
                {
                    _bIsHeatingZoneControl2Visiable = false;
                    imgZipedHeatingZoneControl2.Visibility = Visibility.Visible;
                    imgUnzipedHeatingZoneControl2.Visibility = Visibility.Hidden;
                }
                else
                {
                    _bIsHeatingZoneControl2Visiable = true;
                    imgZipedHeatingZoneControl2.Visibility = Visibility.Hidden;
                    imgUnzipedHeatingZoneControl2.Visibility = Visibility.Visible;
                }
            }
            reorder();
        }
        private bool _bIsHeatingZoneControl3Visiable = false;
        private void cvsHeatingZoneControl3Head_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!_bIsMouseMove)
            {
                if (_bIsHeatingZoneControl3Visiable == true)
                {
                    _bIsHeatingZoneControl3Visiable = false;
                    imgZipedHeatingZoneControl3.Visibility = Visibility.Visible;
                    imgUnzipedHeatingZoneControl3.Visibility = Visibility.Hidden;
                }
                else
                {
                    _bIsHeatingZoneControl3Visiable = true;
                    imgZipedHeatingZoneControl3.Visibility = Visibility.Hidden;
                    imgUnzipedHeatingZoneControl3.Visibility = Visibility.Visible;
                }
            }
            reorder();
        }
        private bool _bIsHeatingZoneControl4Visiable = false;
        private void cvsHeatingZoneControl4Head_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!_bIsMouseMove)
            {
                if (_bIsHeatingZoneControl4Visiable == true)
                {
                    _bIsHeatingZoneControl4Visiable = false;
                    imgZipedHeatingZoneControl4.Visibility = Visibility.Visible;
                    imgUnzipedHeatingZoneControl4.Visibility = Visibility.Hidden;
                }
                else
                {
                    _bIsHeatingZoneControl4Visiable = true;
                    imgZipedHeatingZoneControl4.Visibility = Visibility.Hidden;
                    imgUnzipedHeatingZoneControl4.Visibility = Visibility.Visible;
                }
            }
            reorder();
        }
        private bool _bIsHeatingZoneControl5Visiable = false;
        private void cvsHeatingZoneControl5Head_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!_bIsMouseMove)
            {
                if (_bIsHeatingZoneControl5Visiable == true)
                {
                    _bIsHeatingZoneControl5Visiable = false;
                    imgZipedHeatingZoneControl5.Visibility = Visibility.Visible;
                    imgUnzipedHeatingZoneControl5.Visibility = Visibility.Hidden;
                }
                else
                {
                    _bIsHeatingZoneControl5Visiable = true;
                    imgZipedHeatingZoneControl5.Visibility = Visibility.Hidden;
                    imgUnzipedHeatingZoneControl5.Visibility = Visibility.Visible;
                }
            }
            reorder();
        }
        private bool _bIsHeatingZoneControl6Visiable = false;
        private void cvsHeatingZoneControl6Head_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!_bIsMouseMove)
            {
                if (_bIsHeatingZoneControl6Visiable == true)
                {
                    _bIsHeatingZoneControl6Visiable = false;
                    imgZipedHeatingZoneControl6.Visibility = Visibility.Visible;
                    imgUnzipedHeatingZoneControl6.Visibility = Visibility.Hidden;
                }
                else
                {
                    _bIsHeatingZoneControl6Visiable = true;
                    imgZipedHeatingZoneControl6.Visibility = Visibility.Hidden;
                    imgUnzipedHeatingZoneControl6.Visibility = Visibility.Visible;
                }
            }
            reorder();
        }
        private bool _bIsMachineTempLimitVisiable = false;
        private void cvsMachineTempLimitHead_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!_bIsMouseMove)
            {
                if (_bIsMachineTempLimitVisiable == true)
                {
                    _bIsMachineTempLimitVisiable = false;
                    imgZipedMachineTempLimit.Visibility = Visibility.Visible;
                    imgUnzipedMachineTempLimit.Visibility = Visibility.Hidden;
                }
                else
                {
                    _bIsMachineTempLimitVisiable = true;
                    imgZipedMachineTempLimit.Visibility = Visibility.Hidden;
                    imgUnzipedMachineTempLimit.Visibility = Visibility.Visible;
                }
            }
            reorder();
        }
        private bool _bIsMoldHeatZoneVisiable = true;
        private void cvsMoldHeatZoneHead_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!_bIsMouseMove)
            {
                if (_bIsMoldHeatZoneVisiable == true)
                {
                    _bIsMoldHeatZoneVisiable = false;
                    imgZipedMoldHeatZone.Visibility = Visibility.Visible;
                    imgUnzipedMoldHeatZone.Visibility = Visibility.Hidden;
                }
                else
                {
                    _bIsMoldHeatZoneVisiable = true;
                    imgZipedMoldHeatZone.Visibility = Visibility.Hidden;
                    imgUnzipedMoldHeatZone.Visibility = Visibility.Visible;
                }
            }
            reorder();
        }

        private Point _lastMousePosition;
        private Point _mouseDownPosition;
        private bool _bIsMouseMove;
        private void cvsMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _bIsMouseDown = true;
            _lastMousePosition = _mouseDownPosition = e.GetPosition(cvsMain);
        }

        private void cvsMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (_bIsMouseDown == true)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point curMousePos = e.GetPosition(cvsMain);
                    if (Math.Abs(curMousePos.Y - _mouseDownPosition.Y) > 5)
                        _bIsMouseMove = true;

                    double dOld = Canvas.GetTop(sPanelMain);
                    double dNew = curMousePos.Y - _lastMousePosition.Y + dOld;

                    if (dNew <= -(sPanelMain.Height - (valmoWin.MainPanelHeight - 195)) - 20)
                        dNew = -(sPanelMain.Height - (valmoWin.MainPanelHeight - 195)) - 20;
                    if (dNew > -0)
                        dNew = -0;
                    Canvas.SetTop(sPanelMain, dNew);
                    _lastMousePosition = curMousePos;
                }
            }
        }

        private void cvsMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _bIsMouseMove = false;
            _bIsMouseDown = false;
        }

        private void cvsMain_MouseLeave(object sender, MouseEventArgs e)
        {
            _bIsMouseMove = false;
            _bIsMouseDown = false;
        }

        private void btnTune2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _bIsMouseDown = true;
            btnTune2.Background = Brushes.Blue;
        }

        private void btnTune2_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_bIsMouseDown == true)
            {
                _bIsMouseDown = false;
                btnTune2.Background = new SolidColorBrush(Color.FromArgb(0xff, 0x00, 0xb4, 0xe1));
                valmoWin.dv.TmpPr[158].setValue((valmoWin.dv.TmpPr[158].valueNew == 0) ? 1 : 0);
            }
        }
    }
}
