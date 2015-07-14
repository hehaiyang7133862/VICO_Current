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
    /// Interaction logic for productMsg.xaml
    /// </summary>
    public partial class productMsg : UserControl
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
        /// 鼠标按下位置
        /// </summary>
        Point pMouseDown;
        /// <summary>
        /// 生产监控是否折叠
        /// </summary>
        private bool _bIsMonitorVisiable = true;
        /// <summary>
        /// 电能统计是否折叠
        /// </summary>
        private bool _bIsCountVisiable = true;

        public productMsg()
        {
            InitializeComponent();

            reorder();

            valmoWin.dv.PrdPr[10].addHandle(CalculateShotWeight);
            valmoWin.dv.PrdPr[11].addHandle(CalculateShotWeight);
            valmoWin.dv.PrdPr[17].addHandle(updateAlarmFunc);
        }



        private void updateAlarmFunc(objUnit obj)
        {
            lbClose.Background = new SolidColorBrush(Colors.Transparent);
            lbAlm.Background = new SolidColorBrush(Colors.Transparent);
            lbStop.Background = new SolidColorBrush(Colors.Transparent);

            if (obj.value == 0)
            {
                lbClose.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x3C, 0xE1, 0x00));
            }
            if (obj.value == 1)
            {
                lbAlm.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x3C, 0xE1, 0x00));
            }
            if (obj.value == 2)
            {
                lbStop.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x3C, 0xE1, 0x00));
            }
        }

        private void handlePrdPr_17(objUnit obj)//值已删除
        {
            switch (obj.value)
            {
                case 0:
                    lbClose.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xEA, 0xEA, 0xEA));
                    lbAlm.Background = Brushes.White;
                    lbStop.Background = Brushes.White;
                    break;
                case 1:
                    lbClose.Background = Brushes.White;
                    lbAlm.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xEA, 0xEA, 0xEA));
                    lbStop.Background = Brushes.White;
                    break;
                case 2:
                    lbClose.Background = Brushes.White;
                    lbAlm.Background = Brushes.White;
                    lbStop.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xEA, 0xEA, 0xEA));
                    break;
            }
        }

        private void lbResetPrdMsg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.PrdPr[21].accessLevel))
                return;
            lbReset.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xEA, 0xEA, 0xEA));
        }

        private void lbResetPrdMsg_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.PrdPr[21].accessLevel))
                return;
            lbReset.Background = new SolidColorBrush(Colors.Transparent);
            valmoWin.dv.PrdPr[21].setValue(1);
        }

        private void lbResetPrdMsg_MouseLeave(object sender, MouseEventArgs e)
        {
        }

        private void cvsHeadCount_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!_bIsMouseMove)
            {
                if (_bIsCountVisiable == true)
                {
                    _bIsCountVisiable = false;
                    imgZipedCount.Visibility = Visibility.Visible;
                    imgUnzipedCount.Visibility = Visibility.Hidden;
                }
                else
                {
                    _bIsCountVisiable = true;
                    imgZipedCount.Visibility = Visibility.Hidden;
                    imgUnzipedCount.Visibility = Visibility.Visible;
                }

                reorder();
            }
        }

        private void CalculateShotWeight(objUnit obj)
        {
            lbShotWeight.Content = (valmoWin.dv.PrdPr[11].vDbl + valmoWin.dv.PrdPr[9].vDbl * valmoWin.dv.PrdPr[10].vDbl).ToString("0.0");
        }

        private void cvsHeadMonitor_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!_bIsMouseMove)
            {
                if (_bIsMonitorVisiable == true)
                {
                    _bIsMonitorVisiable = false;
                    imgZipedMonitor.Visibility = Visibility.Visible;
                    imgUnzipedMonitor.Visibility = Visibility.Hidden;
                }
                else
                {
                    _bIsMonitorVisiable = true;
                    imgZipedMonitor.Visibility = Visibility.Hidden;
                    imgUnzipedMonitor.Visibility = Visibility.Visible;
                }

                reorder();
            }
        }

        /// <summary>
        /// 重新计算高度
        /// </summary>
        private void reorder()
        {
            cvsMonitor.Height = _bIsMonitorVisiable ? 560 : 50;
            cvsCount.Height = _bIsCountVisiable ? 280 : 50;

            sPanelMain.Height = cvsMonitor.Height + cvsCount.Height + 580;
        }

        private Point curMousePos;
        private void cvsmain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _bIsMouseDown = true;
            pMouseDown = e.GetPosition(cvsMain);
            curMousePos = pMouseDown;
        }

        private void cvsmain_MouseMove(object sender, MouseEventArgs e)
        {
            if (_bIsMouseDown == true )
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

        private void cvsmain_MouseLeave(object sender, MouseEventArgs e)
        {
            _bIsMouseMove = false;
            _bIsMouseDown = false;
        }

        private void lbClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.PrdPr[17].accessLevel))
                return;
            valmoWin.dv.PrdPr[17].setValue(0);
        }

        private void lbAlm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.PrdPr[17].accessLevel))
                return;
            valmoWin.dv.PrdPr[17].setValue(1);
        }

        private void lbStop_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.PrdPr[17].accessLevel))
                return;
            valmoWin.dv.PrdPr[17].setValue(2);
        }

        private void lbSetBase_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lbSetBase.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void lbSetBase_MouseUp(object sender, MouseButtonEventArgs e)
        {
            lbSetBase.Background = new SolidColorBrush(Colors.White);
            valmoWin.dv.PrdPr[18].setValue(1);
        }

        private void lbSetBase_MouseLeave(object sender, MouseEventArgs e)
        {
            lbSetBase.Background = new SolidColorBrush(Colors.White);
        }
    }
}
