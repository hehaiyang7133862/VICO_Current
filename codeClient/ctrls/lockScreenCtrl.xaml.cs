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
using System.Windows.Threading;
using nsDataMgr;

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// Interaction logic for lockScreenCtrl.xaml
    /// </summary>
    public partial class lockScreenCtrl : UserControl
    {
        public lockScreenCtrl()
        {
            InitializeComponent();

            InitDate();

            //机器模式
            valmoWin.dv.SysPr[105].addHandle(UpdateOPMode);
            //马达状态
            valmoWin.dv.KeyPr[16].addHandle(UpdateMotorState);
            //加热状态
            valmoWin.dv.KeyPr[14].addHandle(UpdateHeatingState);
            //热流道状态
            valmoWin.dv.KeyPr[52].addHandle(UpdateMoldHeatingState);

            valmoWin.BackstageClockTick += SystemClock;

            this.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// 更新热流道状态
        /// </summary>
        /// <param name="obj">Key52</param>
        private void UpdateMoldHeatingState(objUnit obj)
        {
            TabHotRunnerState.SelectedIndex = obj.value;
        }

        /// <summary>
        /// 更新加热状态
        /// </summary>
        /// <param name="obj">Key014</param>
        private void UpdateHeatingState(objUnit obj)
        {
            TabBarrelState.SelectedIndex = obj.value;
        }

        /// <summary>
        /// 更新马达状态
        /// </summary>
        /// <param name="obj">Key016</param>
        private void UpdateMotorState(objUnit obj)
        {
            TabMotorState.SelectedIndex = obj.value;
        }

        /// <summary>
        /// 更新机器模式
        /// </summary>
        /// <param name="obj">Sys105</param>
        private void UpdateOPMode(objUnit obj)
        {
            if (obj.value <= 3)
            {
                TabOPMode.SelectedIndex = obj.value;
            }
            else
            {
                TabOPMode.SelectedIndex = 4;
            }
        }        

        private void InitDate()
        {
            string strDate = string.Empty;

            strDate += DateTime.Now.ToString("yyyy-MM-dd");
            lbDate.Content = strDate;
        }

        private string getWeek(DateTime sd)
        {
            string s = string.Empty;

            switch (sd.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    s = "周一";
                    break;
                case DayOfWeek.Tuesday:
                    s = "周二";
                    break;
                case DayOfWeek.Wednesday:
                    s = "周三";
                    break;
                case DayOfWeek.Thursday:
                    s = "周四";
                    break;
                case DayOfWeek.Friday:
                    s = "周五";
                    break;
                case DayOfWeek.Saturday:
                    s = "周六";
                    break;
                case DayOfWeek.Sunday:
                    s = "周日";
                    break;
                default:
                    s = "周一";
                    break;
            }
            return s;
        }

        private bool flag = true;
        /// <summary>
        /// 系统时钟
        /// </summary>
        private void SystemClock()
        {
            DateTime dt = DateTime.Now;
            lbTime_min.Content = dt.Hour.ToString().PadLeft(2, '0');
            lbTime_sec.Content = dt.Minute.ToString().PadLeft(2, '0');

            if (flag == true)
            {
                flag = false;
                lbTime_mid.Content = ":";
            }
            else
            {
                flag = true;
                lbTime_mid.Content = " ";
            }
        }

        public void hide()
        {
            this.Opacity = 0;
            this.Visibility = Visibility.Hidden;
        }

        public void show()
        {
            this.Opacity = 1;
            this.Visibility = Visibility.Visible;
        }

        private bool _bIsMouseDown = false;
        private Point _currentPosition;
        private Point _mouseDownPosition;

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _bIsMouseDown = true;
            _mouseDownPosition = _currentPosition = e.GetPosition(cvsMain);
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_bIsMouseDown == true)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    _currentPosition = e.GetPosition(cvsMain);

                    Canvas.SetLeft(SlideDark, (_currentPosition.X - _mouseDownPosition.X) + 216);

                    if ((_currentPosition.X - _mouseDownPosition.X) > 540)
                        hide();
                }
            }
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_bIsMouseDown == true)
            {
                _bIsMouseDown = false;

                Canvas.SetLeft(SlideDark, 216);
            }
        }
    }
}
