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
    /// IpSerSetCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class IpSerSetCtrl : UserControl
    {
        public IpSerSetCtrl()
        {
            InitializeComponent();
            lbInput_numKey.Content = "0";
            this.Visibility = Visibility.Hidden;
        }
        objUnit curObj;
        string numInput;
        bool flagFirstInputValue = true;

        public void setCtrlPos(Thickness margin)
        {
            if (margin.Left < 0)
                margin.Left = 0;
            else if (margin.Left > ctrlWidth - cvsMain.Width)
                margin.Left = ctrlWidth - cvsMain.Width;
            if (margin.Top < 0)
                margin.Top = 0;
            else if (margin.Top > ctrlHeight - cvsMain.Height)
                margin.Top = ctrlHeight - cvsMain.Height;
            Thickness newMargin = new Thickness(margin.Left, margin.Top, margin.Right, margin.Bottom);
            cvsMain.Margin = newMargin;
        }
        public void setPos(double left, double top)
        {
            if (left < 0)
                left = 0;
            else if (left > ctrlWidth - cvsMain.Width)
                left = ctrlWidth - cvsMain.Width;
            if (top < 0)
                top = 0;
            else if (top > ctrlHeight - cvsMain.Height)
                top = ctrlHeight - cvsMain.Height;
            Thickness newMargin = new Thickness(left, top, 0, 0);
            cvsMain.Margin = newMargin;
        }
        public void setWidth(double width)
        {
            cvsBackPanel.Width = width;
            lbPanelBack.Width = width;
        }
        public void setHeight(double height)
        {
            cvsBackPanel.Height = height;
        }
        public double ctrlWidth
        {
            get
            {
                return cvsBackPanel.Width;
            }
            set
            {
                cvsBackPanel.Width = value;
                lbPanelBack.Width = value;
            }
        }
        public double ctrlHeight
        {
            get
            {
                return cvsBackPanel.Height;
            }
            set
            {
                cvsBackPanel.Height = value;
                lbPanelBack.Height = value;
            }
        }
        nullEvent disposeHandle;
        public void show(objUnit obj, nullEvent disposeFunc)
        {
            curObj = obj;
            lbCur_numKey.Content = curObj.note;
            numInput = curObj.note;
            lbInput_numKey.Content = numInput;
            disposeHandle = disposeFunc;
            this.Opacity = 1;
            this.Visibility = Visibility.Visible;
        }
        public void hide()
        {
            this.Visibility = Visibility.Hidden;
        }
        bool isKeyMouseDown = false;
        private void lbNum_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isKeyMouseDown = true;
            (sender as Image).Opacity = 1;
        }
        private void lbNum_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Image curImg = sender as Image;
                switch (curImg.Name)
                {
                    case "img1":
                    case "img2":
                    case "img3":
                    case "img4":
                    case "img5":
                    case "img6":
                    case "img7":
                    case "img8":
                    case "img9":
                    case "img0":
                        if (curImg.Opacity == 1)
                        {
                            addStr(curImg.Tag.ToString());
                        }
                        break;
                    case "imgDot":
                        if (imgDot.Opacity == 1)
                        {
                            int dotNum = 0;
                            for (int i = 0; i < numInput.Length; i++)
                            {
                                if (numInput[i] == '.')
                                {
                                    dotNum++;
                                }
                            }
                            if (dotNum < 3 && numInput[numInput.Length - 1] != '.')
                                addStr(".");
                        }
                        break;
                    case "imgBack":
                        if (imgBack.Opacity == 1)
                        {
                            if (flagFirstInputValue)
                            {
                                flagFirstInputValue = false;
                            }
                            if (numInput.Length == 1)
                            {
                                numInput = "0";
                            }
                            else if (numInput.Length == 2 && numInput[0] == '-')
                            {
                                numInput = "0";
                            }
                            else
                            {
                                string strTmp = null;
                                for (int i = 0; i < numInput.Length - 1; i++)
                                {
                                    strTmp += numInput[i];
                                }
                                numInput = strTmp;
                            }
                            lbInput_numKey.Content = numInput;
                        }
                        break;
                    case "imgSub":
                        break;
                    case "imgOk":
                        if (imgOk.Opacity == 1)
                        {
                            if (flagFirstInputValue)
                            {
                                flagFirstInputValue = false;
                            }

                            if (disposeHandle != null)
                            {
                                string strTmp = lbInput_numKey.Content.ToString();
                                string[] str = strTmp.Split('.');
                                if (str.Length != 4)
                                {
                                    return;
                                }
                                for (int i = 0; i < 4; i++)
                                {
                                    if (str[i].Length > 3)
                                    {
                                        return;
                                    }
                                    for (int j = 0; j < str[i].Length; j++)
                                    {
                                        if (str[i][j] < '0' || str[i][j] > '9')
                                            return;
                                    }
                                }
                                Properties.Settings.Default.serIPAddr = strTmp;
                                Properties.Settings.Default.Save();
                                curObj.refresh();
                                disposeHandle();
                                hide();
                            }
                        }
                        break;
                }
                curImg.Opacity = 0;
                isKeyMouseDown = false;
            }
            catch (Exception ex)
            {
                vm.perror("[lbNum_MouseUp." + (sender as Label).Name + "]" + ex.ToString());
            }
        }
        private void addStr(string str)
        {
            if (numInput == "0")
            {
                if (str == ".")
                    numInput = "192.";
                else
                    numInput = str;

            }
            else
                numInput += str;
            lbInput_numKey.Content = numInput;
        }

        private void lbNum_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!isKeyMouseDown)
                return;
            (sender as Image).Opacity = 0;
        }

        bool isMouseDown = false;
        Point mousePoint;
        object mouseCtrl = null;

        private void cvsMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = true;
            mousePoint = e.GetPosition(this.cvsBackPanel);
            mouseCtrl = sender;
            try
            {
                double a = cvsMain.Margin.Left;
                double b = cvsMain.Margin.Top;
                lbL.Content = a;
                lbR.Content = b;
            }
            catch (Exception ex)
            {
                vm.perror(ex.ToString());
            }
        }

        private void lbPanelBack_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown == true)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point theMousePoint = e.GetPosition(this.cvsBackPanel);
                    double tmpLeft = cvsMain.Margin.Left + theMousePoint.X - mousePoint.X;
                    double tmpTop = cvsMain.Margin.Top + theMousePoint.Y - mousePoint.Y;
                    if (tmpLeft < 0)
                        tmpLeft = 0;
                    else if (tmpLeft > ctrlWidth - cvsMain.Width)
                        tmpLeft = ctrlWidth - cvsMain.Width;
                    if (tmpTop < 0)
                        tmpTop = 0;
                    else if (tmpTop > ctrlHeight - cvsMain.Height)
                        tmpTop = ctrlHeight - cvsMain.Height;
                    cvsMain.Margin = new Thickness(tmpLeft, tmpTop, 0, 0);
                    lbL.Content = tmpLeft;
                    lbR.Content = tmpTop;
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
            disposeHandle();
        }
    }
}
