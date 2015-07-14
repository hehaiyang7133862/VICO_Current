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
    /// Interaction logic for errorAlarmCtrl.xaml
    /// </summary>
    public partial class errorAlarmCtrl : UserControl
    {
        nullEvent confirmHandle;
          public errorAlarmCtrl()
        {
            InitializeComponent();
            this.Visibility = Visibility.Hidden;
        }
          public void init(nullEvent handle)
        {
            confirmHandle = handle;
        }
        public void show()
        {
            this.Visibility = Visibility.Visible;
        }
        public void setHeight(double height)
        {
            cvsBackPanel.Height = height;
            lbPanelBack.Height = height;
        }
        public void setWidth(double width)
        {
            cvsBackPanel.Width = width;
            lbPanelBack.Width = width;
        }
        public double h
        {
            set
            {
                cvsBackPanel.Height = value;
                lbPanelBack.Height = value;
            }
            get
            {
                return lbPanelBack.Height;
            }
        }
        public double w
        {
            set
            {
                cvsBackPanel.Width = value;
                lbPanelBack.Width = value;
            }
            get
            {
                return lbPanelBack.Width;
            }
        }
        bool isMouseDown = false;
        Point mousePoint;
        private void cvsChar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = true;
            mousePoint = e.GetPosition(cvsBackPanel);
            //vm.printLn(Canvas.GetLeft(cvsBackPanel) + "," + Canvas.GetTop(cvsBackPanel));
        }

        private void cvsBackPanel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = false;
        }

        private void cvsBackPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point theMousePoint = e.GetPosition(this.cvsBackPanel);
                    double tmpLeft = Canvas.GetLeft(cvsChar) + theMousePoint.X - mousePoint.X;
                    double tmpTop = Canvas.GetTop(cvsChar) + theMousePoint.Y - mousePoint.Y;
                    trySetPos(tmpLeft, tmpTop);
                    mousePoint = theMousePoint;
                }
            }
        }
        private void trySetPos(double tmpLeft, double tmpTop)
        {
            if (tmpLeft < 0)
                tmpLeft = 0;
            else if (tmpLeft > cvsBackPanel.Width - cvsChar.Width)
                tmpLeft = cvsBackPanel.Width - cvsChar.Width;
            if (tmpTop < 0)
                tmpTop = 0;
            else if (tmpTop > cvsBackPanel.Height - cvsChar.Height)
                tmpTop = cvsBackPanel.Height - cvsChar.Height;
            Canvas.SetLeft(cvsChar, tmpLeft);
            Canvas.SetTop(cvsChar, tmpTop);
        }
        public void lbPanelBack_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //if (charKeyHideHandle != null)
            //    charKeyHideHandle();
            

        }

        public void confirmBtn1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (confirmHandle != null)
                confirmHandle();
            this.Visibility = Visibility.Hidden;
        }

        public void confirmBtn1_MouseLeave(object sender, MouseEventArgs e)
        {

        }
    }
}
