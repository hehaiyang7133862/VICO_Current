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

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// Interaction logic for calcCtrl.xaml
    /// </summary>
    public partial class calcCtrl : UserControl
    {
        //string num = "0";
        string odNum1 = "0";
        //string odNum2 = "0";
        string op = "";
        bool flagNewValue = false;
        public calcCtrl()
        {
            InitializeComponent();
            this.Visibility = Visibility.Hidden;
        }
        public void show()
        {
            odNum1 = "0";
            //odNum2 = "0";
            this.Visibility = Visibility.Visible;
            this.Opacity = 1;
        }

        public void hide()
        {
            this.Visibility = Visibility.Hidden;
        }

        private void imgRec_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (sender as Image).Opacity = 1;
            string str = lbResult.Content.ToString();
            if (str == "0" || str == "0.")
                lbResult.Content = "0";
            else
                lbResult.Content = 1 / Double.Parse(lbResult.Content.ToString());
            
        }

        private void imgSign_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (sender as Image).Opacity = 1;
        }

        private void imgClr_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (sender as Image).Opacity = 1;
            lbResult.Content = "0";
            odNum1 = "";
            op = "";
        }

        private void imgBack_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (sender as Image).Opacity = 1;
            string str = lbResult.Content.ToString();

            if (str.Length == 1)
                str = "0";
            else
            {
                str = str.Substring(0, str.Length - 1);
                    
            }
            lbResult.Content = str;
        }

        private void imgSquare_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (sender as Image).Opacity = 1;
            string str = lbResult.Content.ToString();
            if (str == "0.")
                lbResult.Content = 0;
            else
                lbResult.Content = Double.Parse(str) * Double.Parse(str);
        }

        private void imgSqRoot_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (sender as Image).Opacity = 1;
        }

        private void imgMlt_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (sender as Image).Opacity = 1;
            if (op == "")
            {
                
                string str = lbResult.Content.ToString();
                if (str == "0.")
                    odNum1 = "0";
                else
                    odNum1 = str;
            }
            else
            {
                calcResult();
                odNum1 = lbResult.Content.ToString();
            }
            op = "mlt";
            flagNewValue = true;
        }

        private void imgAdd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (sender as Image).Opacity = 1;
            if (op == "")
            {
                
                string str = lbResult.Content.ToString();
                if (str == "0.")
                    odNum1 = "0";
                else
                    odNum1 = str;
            }
            else
            {
                calcResult();
                odNum1 = lbResult.Content.ToString();
            }
            op = "add";
            flagNewValue = true;
        }

        private void imgDiv_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (sender as Image).Opacity = 1;
            if (op == "")
            {

                string str = lbResult.Content.ToString();
                if (str == "0.")
                    odNum1 = "0";
                else
                    odNum1 = str;
            }
            else
            {
                calcResult();
                odNum1 = lbResult.Content.ToString();
            }
            op = "div";
            flagNewValue = true;
        }

        private void imgSub_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (sender as Image).Opacity = 1;
            if (op == "")
            {
                string str = lbResult.Content.ToString();
                if (str == "0.")
                    odNum1 = "0";
                else
                    odNum1 = str;
            }
            else
            {
                calcResult();
                odNum1 = lbResult.Content.ToString();
            }
            op = "sub";
            flagNewValue = true;
        }

        private void imgEnter_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (sender as Image).Opacity = 1;
            calcResult();
            op = "";
            odNum1 = lbResult.Content.ToString();
            flagNewValue = true;
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (sender as Image).Opacity = 1;
            string str = lbResult.Content.ToString();
            string strNum = (sender as Image).Tag.ToString();
            if (flagNewValue)
            {
                if (strNum == ".")
                    str = "0.";
                else
                    str = strNum;
                flagNewValue = false;
            }
            else
            {
                if (strNum == ".")
                {
                    bool flagGetDotNum = false;
                    for (int i = 0; i < str.Length; i++)
                    {
                        if (str[i] == '.')
                        {
                            flagGetDotNum = true;
                            break;
                        }
                    }
                    if (!flagGetDotNum)
                    {
                        //if (op == "")
                        //    str = "0.";
                        //else
                            str += ".";
                    }
                }
                else
                {
                    if (str == "0")
                        str = strNum;
                    else
                        str += strNum;
                }
            }
            lbResult.Content = str;
        }

        private void lbPanelBack_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
        bool isMouseDown = false;
        Point mousePoint;
        object mouseCtrl = null;
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
                    mousePoint = theMousePoint;
                }

            }

        }

        private void lbPanelBack_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isMouseDown)
                isMouseDown = false;
        }

        private void cvs_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = true;
            mousePoint = e.GetPosition(this.cvsBackPanel);
            mouseCtrl = sender;
        }

        private void imgClr_MouseUp(object sender, MouseButtonEventArgs e)
        {
            lbResult.Content = "0";
            (sender as Image).Opacity = 0;
        }

        private void img_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Image).Opacity = 0;
        }

        private void imgBack_MouseUp(object sender, MouseButtonEventArgs e)
        {
            (sender as Image).Opacity = 0;
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {

            (sender as Image).Opacity = 0;
        }

        private void calcResult()
        {
            switch (op)
            {
                case "add":
                    {
                        lbResult.Content = Double.Parse(odNum1) + Double.Parse(lbResult.Content.ToString());
                    }
                    break;
                case "div":
                    if (lbResult.Content.ToString() == "0" || lbResult.Content.ToString() == "0.")
                    {
                        lbResult.Content = "0";
                    }
                    else
                        lbResult.Content = Double.Parse(odNum1) / Double.Parse(lbResult.Content.ToString());
                    break;
                case "mlt":
                    {
                        lbResult.Content = Double.Parse(odNum1) * Double.Parse(lbResult.Content.ToString());
                    }
                    break;
                case "sub":
                    {
                        lbResult.Content = Double.Parse(odNum1) - Double.Parse(lbResult.Content.ToString());
                    }
                    break;
            }
        }

        private void imgRadical_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (sender as Image).Opacity = 1;
            string str = lbResult.Content.ToString();
            if (str == "0.")
                lbResult.Content = 0;
            else
                lbResult.Content = Math.Sqrt(Double.Parse(lbResult.Content.ToString()));
        }

        private void imgSigh_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (sender as Image).Opacity = 1;
            string str = lbResult.Content.ToString();
            if (str[0] == '-')
                lbResult.Content = str.Substring(1, str.Length - 1);
            else
                lbResult.Content = "-" + str;
        }
    }
}
