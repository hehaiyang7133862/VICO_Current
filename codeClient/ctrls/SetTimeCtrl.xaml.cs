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
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using nsDataMgr;

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// SetTimeCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class SetTimeCtrl : UserControl
    {
        private intEvent confirmEvent;
        private nullEvent cancelEvent;
        private DateTime dtMax;
        private DateTime dtMin;

        public SetTimeCtrl()
        {
            InitializeComponent();

            this.Visibility = Visibility.Hidden;
        }

        private DateTime dtNow;
        public void Show(intEvent Event1, nullEvent Event2, DateTime dt, DateTime min, DateTime max)
        {
            confirmEvent = Event1;
            cancelEvent = Event2;
            dtMax = max;
            dtMin = min;

            resetTime(dt);

            lbNotice.Content = "";
            this.Visibility = Visibility.Visible;

            trySetCtrlPos(mainPanelCtrl.PointMouseDown);
        }

        public void trySetCtrlPos(Point pos)
        {
            if (pos.X > 540)
            {
                pos.X -= 360;
            }

            if (pos.Y < 190)
            {
                pos.Y = 0;
            }
            else if (pos.Y > valmoWin.MainPanelHeight - 190)
            {
                pos.Y = valmoWin.MainPanelHeight - 380;
            }
            else
            {
                pos.Y -= 190;
            }
            pos.Y += 240;

            Canvas.SetLeft(cvsBg, pos.X);
            Canvas.SetTop(cvsBg, pos.Y);
        }

        private void resetTime(DateTime dt)
        {
            dtNow = dt;

            DateTime dtStartHour = dt - TimeSpan.FromHours(7);
            DateTime dtStartMin = dt - TimeSpan.FromMinutes(7);

            foreach (object obj in spHour.Children)
            {
                if (obj.GetType() == typeof(Label))
                {
                    (obj as Label).Content = (dtStartHour += TimeSpan.FromHours(1)).Hour;
                }
            }

            foreach (object obj in spMin.Children)
            {
                if (obj.GetType() == typeof(Label))
                {
                    (obj as Label).Content = (dtStartMin += TimeSpan.FromMinutes(1)).Minute;
                }
            }

            lbCurHour.Foreground = lbCurMin.Foreground = new SolidColorBrush(Colors.Green);
        }

        private bool bIsMinMouseDown = false;
        private bool bIsHourMouseDown = false;
        private Point pMouseDown;
        private Point pLastPos;
        private DateTime dtMouseDown;

        private void cvsMin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            bIsMinMouseDown = true;

            dtMouseDown = DateTime.Now;
            pLastPos = pMouseDown = e.GetPosition(gMain);
        }

        private void cvsHour_MouseDown(object sender, MouseButtonEventArgs e)
        {
            bIsHourMouseDown = true;

            dtMouseDown = DateTime.Now;
            pLastPos = pMouseDown = e.GetPosition(gMain);
        }

        private void gMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (bIsMinMouseDown == true)
            {
                lbCurMin.Foreground = new SolidColorBrush(Colors.Black);

                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point pCurPos = e.GetPosition(gMain);

                    double top = Canvas.GetTop(spMin);
                    double topNew = pCurPos.Y - pLastPos.Y + top;

                    Canvas.SetTop(spMin, topNew);
                    pLastPos = pCurPos;
                }
            }

            if (bIsHourMouseDown == true)
            {
                lbCurHour.Foreground = new SolidColorBrush(Colors.Black);

                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point pCurPos = e.GetPosition(gMain);

                    double top = Canvas.GetTop(spHour);
                    double topNew = pCurPos.Y - pLastPos.Y + top;

                    Canvas.SetTop(spHour, topNew);
                    pLastPos = pCurPos;
                }
            }
        }

        private void gMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            int offset;

            if (bIsMinMouseDown == true)
            {
                bIsMinMouseDown = false;

                double curTop = Canvas.GetTop(spMin);

                if (DateTime.Now - dtMouseDown < TimeSpan.FromMilliseconds(250))
                {
                    if (curTop > -358)
                    {
                        DoubleAnimation TopAnimationDown = new DoubleAnimation();
                        TopAnimationDown.To = -33;
                        TopAnimationDown.Duration = TimeSpan.FromMilliseconds(300);
                        TopAnimationDown.Completed += new EventHandler(TopAnimation_Completed);
                        spMin.BeginAnimation((Canvas.TopProperty), TopAnimationDown);
                    }
                    else
                    {
                        DoubleAnimation TopAnimationUp = new DoubleAnimation();
                        TopAnimationUp.To = -688;
                        TopAnimationUp.Duration = TimeSpan.FromMilliseconds(300);
                        TopAnimationUp.Completed += new EventHandler(TopAnimationUp_Completed);
                        spMin.BeginAnimation((Canvas.TopProperty), TopAnimationUp);
                    }

                    return;
                }

                if (curTop > -358)
                {
                    offset = (int)((curTop + 358 + 40) / 65);
                }
                else
                {
                    offset = (int)((curTop + 358 - 40) / 65);
                }

                DateTime dt;
                dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt32(lbCurHour.Content), Convert.ToInt32(lbCurMin.Content), 0);
                resetTime(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt32(lbCurHour.Content), (dt + TimeSpan.FromMinutes(-offset)).Minute, 0));
                Canvas.SetTop(spMin, -358);
            }

            if (bIsHourMouseDown == true)
            {
                bIsHourMouseDown = false;

                double curTop = Canvas.GetTop(spHour);

                if (DateTime.Now - dtMouseDown < TimeSpan.FromMilliseconds(250))
                {
                    if (curTop > -358)
                    {
                        DoubleAnimation TopAnimationDown3 = new DoubleAnimation();
                        TopAnimationDown3.To = -33;
                        TopAnimationDown3.Duration = TimeSpan.FromMilliseconds(300);
                        TopAnimationDown3.Completed += new EventHandler(TopAnimationDown3_Completed);
                        spHour.BeginAnimation((Canvas.TopProperty), TopAnimationDown3);
                    }
                    else
                    {
                        DoubleAnimation TopAnimationDown4 = new DoubleAnimation();
                        TopAnimationDown4.To = -688;
                        TopAnimationDown4.Duration = TimeSpan.FromMilliseconds(300);
                        TopAnimationDown4.Completed += new EventHandler(TopAnimationDown4_Completed);
                        spHour.BeginAnimation((Canvas.TopProperty), TopAnimationDown4);
                    }

                    return;
                }

                if (curTop > -358)
                {
                    offset = (int)((curTop + 358 + 40) / 65);
                }
                else
                {
                    offset = (int)((curTop + 358 - 40) / 65);
                }

                DateTime dt;
                dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt32(lbCurHour.Content), Convert.ToInt32(lbCurMin.Content), 0);
                resetTime(dt + TimeSpan.FromHours(-offset));
                Canvas.SetTop(spHour, -358);
            }
        }

        void TopAnimationDown4_Completed(object sender, EventArgs e)
        {
            spHour.BeginAnimation((Canvas.TopProperty), null);
            DateTime dt4;
            dt4 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt32(lbCurHour.Content), Convert.ToInt32(lbCurMin.Content), 0);
            resetTime(dt4 + TimeSpan.FromHours(5));
            Canvas.SetTop(spHour, -358);
        }

        void TopAnimationDown3_Completed(object sender, EventArgs e)
        {
            spHour.BeginAnimation((Canvas.TopProperty), null);
            DateTime dt3;
            dt3 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt32(lbCurHour.Content), Convert.ToInt32(lbCurMin.Content), 0);
            resetTime(dt3 + TimeSpan.FromHours(-5));
            Canvas.SetTop(spHour, -358);
        }

        void TopAnimationUp_Completed(object sender, EventArgs e)
        {
            spMin.BeginAnimation((Canvas.TopProperty), null);
            DateTime dt2;
            dt2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt32(lbCurHour.Content), Convert.ToInt32(lbCurMin.Content), 0);
            resetTime(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt32(lbCurHour.Content), (dt2 + TimeSpan.FromMinutes(5)).Minute, 0));
            Canvas.SetTop(spMin, -358);
        }

        void TopAnimation_Completed(object sender, EventArgs e)
        {
            spMin.BeginAnimation((Canvas.TopProperty), null);
            DateTime dt1;
            dt1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt32(lbCurHour.Content), Convert.ToInt32(lbCurMin.Content), 0);
            resetTime(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt32(lbCurHour.Content), (dt1 + TimeSpan.FromMinutes(-5)).Minute, 0));
            Canvas.SetTop(spMin, -358);
        }

        private void gMain_MouseLeave(object sender, MouseEventArgs e)
        {
            int offset;

            if (bIsMinMouseDown == true)
            {
                bIsMinMouseDown = false;

                double curTop = Canvas.GetTop(spMin);

                if (curTop > -358)
                {
                    offset = (int)((curTop + 358 + 40) / 65);
                }
                else
                {
                    offset = (int)((curTop + 358 - 40) / 65);
                }

                DateTime dt;
                dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt32(lbCurHour.Content), Convert.ToInt32(lbCurMin.Content), 0);
                resetTime(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt32(lbCurHour.Content), (dt + TimeSpan.FromMinutes(-offset)).Minute, 0));
                Canvas.SetTop(spMin, -358);
            }

            if (bIsHourMouseDown == true)
            {
                bIsHourMouseDown = false;

                double curtop = Canvas.GetTop(spHour);

                if (curtop > -358)
                {
                    offset = (int)((curtop + 358 + 40) / 65);
                }
                else
                {
                    offset = (int)((curtop + 358 - 40) / 65);
                }

                DateTime dt;
                dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt32(lbCurHour.Content), Convert.ToInt32(lbCurMin.Content), 0);
                resetTime(dt + TimeSpan.FromHours(-offset));
                Canvas.SetTop(spHour, -358);
            }
        }

        private void btnOK_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int time = (Convert.ToInt32(lbCurHour.Content) << 24) | (Convert.ToInt32(lbCurMin.Content) << 16) | 00 << 8;

            DateTime dtZero = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

            if (dtMax != dtZero)
            {
                if (dtNow > dtMax)
                {
                    lbNotice.Content = "时间不能大于" + dtMax.Hour.ToString().PadLeft(2, '0') + ":" + dtMax.Minute.ToString().PadLeft(2, '0');
                    return;
                }
            }

            if (dtMin != dtZero)
            {
                if (dtNow < dtMin)
                {
                    lbNotice.Content = "时间不能小于" + dtMin.Hour.ToString().PadLeft(2, '0') + ":" + dtMin.Minute.ToString().PadLeft(2, '0');
                    return;
                }
            }

            if (confirmEvent != null)
            {
                confirmEvent(time);
            }
        }

        private void btnCancel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (cancelEvent != null)
            {
                cancelEvent();
            }
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (cancelEvent != null)
            {
                cancelEvent();
            }
        }

        private void gMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private Point pLastPos2;
        private bool bIsTitleMouseDown = false;
        private void lbTitle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            bIsTitleMouseDown = true;
            pLastPos2 = e.GetPosition(cvsMain);
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (bIsTitleMouseDown == true)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point curPos = e.GetPosition(cvsMain);

                    double top = Canvas.GetTop(cvsBg);
                    double newTop = curPos.Y - pLastPos2.Y + top;
                    if (newTop < 240)
                        newTop = 240;
                    if (newTop > valmoWin.MainPanelHeight + 240 - 380)
                        newTop = valmoWin.MainPanelHeight + 240 - 380;
                    Canvas.SetTop(cvsBg, newTop);

                    double left = Canvas.GetLeft(cvsBg);
                    double newLeft = curPos.X - pLastPos2.X + left;
                    if (newLeft < 0)
                    {
                        newLeft = 0;
                    }
                    if (newLeft > 720)
                    {
                        newLeft = 720;
                    }
                    Canvas.SetLeft(cvsBg, newLeft);

                    pLastPos2 = curPos;
                }
            }
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            bIsTitleMouseDown = false;
        }
    }
}
