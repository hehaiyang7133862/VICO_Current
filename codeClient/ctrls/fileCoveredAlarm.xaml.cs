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
    /// fileCoveredAlarm.xaml 的交互逻辑
    /// </summary>
    public partial class fileCoveredAlarm : UserControl
    {
        nullEvent confirmHandle;
        public fileCoveredAlarm()
        {
            InitializeComponent();
            this.Visibility = Visibility.Hidden;
        }
        public void init(nullEvent confirmFunc,Point ctrlPos)
        {
            confirmHandle = confirmFunc;
            trySetCtrlPos(ctrlPos);
            this.Opacity = 1;
            this.Visibility = Visibility.Visible;
        }
        public void trySetCtrlPos(Point pos)
        {
            Canvas.SetLeft(cvsMain, pos.X);
            Canvas.SetTop(cvsMain, pos.Y);
            lbPos.Content = pos.X + "," + pos.Y;
        }
        public Point pos
        {
            get;
            set;
        }
        private void confirm_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (confirmHandle != null)
                confirmHandle();
            this.Visibility = Visibility.Hidden;
        }
        private void cancel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        bool isMouseDown = false;
        Point mousePoint;
        object mouseCtrl = null;


        private void cvsMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = true;
            mousePoint = e.GetPosition(this.cvsBackPanel);
            mouseCtrl = sender;
        }


        private void lbPanelBack_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown == true)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point theMousePoint = e.GetPosition(this.cvsBackPanel);
                    double tmpLeft = Canvas.GetLeft(cvsMain) + theMousePoint.X - mousePoint.X;
                    double tmpTop = Canvas.GetTop(cvsMain) + theMousePoint.Y - mousePoint.Y;
                    if (tmpLeft < 0)
                        tmpLeft = 0;
                    else if (tmpLeft > cvsBackPanel.Width - cvsMain.Width)
                        tmpLeft = cvsBackPanel.Width - cvsMain.Width;
                    if (tmpTop < 0)
                        tmpTop = 0;
                    else if (tmpTop > cvsBackPanel.Height - cvsMain.Height)
                        tmpTop = cvsBackPanel.Height - cvsMain.Height;
                    Canvas.SetLeft(cvsMain, tmpLeft);
                    Canvas.SetTop(cvsMain, tmpTop);
                    lbPos.Content = tmpLeft + "," + tmpTop;
                    mousePoint = theMousePoint;
                }

            }

        }

        private void lbPanelBack_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isMouseDown)
                isMouseDown = false;
        }

        private void lbPanelBack_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}
