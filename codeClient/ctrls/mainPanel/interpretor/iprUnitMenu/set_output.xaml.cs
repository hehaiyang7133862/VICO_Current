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
    /// Interaction logic for set_output.xaml
    /// </summary>
    public partial class set_output : UserControl
    {
        public set_output()
        {
            InitializeComponent();
        }
        public void setValue()
        {
            iprCtrl.curUnit.get_sNotReady();
            if (iprCtrl.curUnit.sErrLink)
            {
                activeErr1Ctrl1.Visibility = Visibility.Visible;
                activeErr1Ctrl1.dis = "触发错误";
            }
            else
            {
                activeErr1Ctrl1.Visibility = Visibility.Hidden;
            }
            if (iprCtrl.curUnit.sErrActName)
            {
                activeErr1Ctrl1.Visibility = Visibility.Visible;
                activeErr1Ctrl1.dis = "该功能无法在此位置执行";
            }
            else
            {
                if (!iprCtrl.curUnit.sErrLink)
                    activeErr1Ctrl1.Visibility = Visibility.Hidden;
            }
            if (iprCtrl.curUnit.sErrUndefined)
            {
                activeErr1Ctrl1.Visibility = Visibility.Visible;
                activeErr1Ctrl1.dis = "功能未定义";
            }
            else
            {
                if (!iprCtrl.curUnit.sErrLink && !iprCtrl.curUnit.sErrActName)
                    activeErr1Ctrl1.Visibility = Visibility.Hidden;
            }
            iprCtrl.curUnit.get_sFuncSelect();
            btn1.focusState = false;
            btn2.focusState = false;
            btn3.focusState = false;
            btn4.focusState = false;
            btn5.focusState = false;
            btn6.focusState = false;

            switch (iprCtrl.curUnit.sFuncSelect)
            {
                case 0:
                    {
                        btn1.focusState = true;
                        if (iprCtrl.curUnit.sErrFuncSelect)
                        {
                            btn1.setErrValue();
                        }
                        else
                        {
                            btn1.clearErrValue();
                        }
                    }
                    break;
                case 1:
                    {
                        btn2.focusState = true;
                        if (iprCtrl.curUnit.sErrFuncSelect)
                        {
                            btn2.setErrValue();
                        }
                        else
                        {
                            btn2.clearErrValue();
                        }
                    }
                    break;
                case 2:
                    {
                        btn3.focusState = true;
                        if (iprCtrl.curUnit.sErrFuncSelect)
                        {
                            btn3.setErrValue();
                        }
                        else
                        {
                            btn3.clearErrValue();
                        }
                    }
                    break;
                case 3:
                    {
                        btn4.focusState = true;
                        if (iprCtrl.curUnit.sErrFuncSelect)
                        {
                            btn4.setErrValue();
                        }
                        else
                        {
                            btn4.clearErrValue();
                        }
                    }
                    break;
                case 4:
                    {
                        btn5.focusState = true;
                        if (iprCtrl.curUnit.sErrFuncSelect)
                        {
                            btn5.setErrValue();
                        }
                        else
                        {
                            btn5.clearErrValue();
                        }
                    }
                    break;
                case 5:
                    {
                        btn6.focusState = true;
                        if (iprCtrl.curUnit.sErrFuncSelect)
                        {
                            btn6.setErrValue();
                        }
                        else
                        {
                            btn6.clearErrValue();
                        }
                    }
                    break;
            }
            iprCtrl.curUnit.get_sValueA();
            btnDA0.focusState = false;
            btnDA1.focusState = false;
            switch (iprCtrl.curUnit.sValueA)
            {
                case 0:
                    {
                        btnDA0.focusState = true;
                        if (iprCtrl.curUnit.sErrValueA)
                        {
                            btnDA0.setErrValue();
                        }
                        else
                        {
                            btnDA0.clearErrValue();
                        }
                    }
                    break;
                case 1:
                    {
                        btnDA1.focusState = true;
                        if (iprCtrl.curUnit.sErrValueA)
                        {
                            btnDA1.setErrValue();
                        }
                        else
                        {
                            btnDA1.clearErrValue();
                        }

                    }
                    break;
            }
        }

        private void btn1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //btn1.focusState = true;
        }

        private void btn2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //btn2.focusState = true;
        }
        private void btn3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //btn3.focusState = true;
        }
        private void btn4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //btn4.focusState = true;
        }
        private void btn5_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //btn5.focusState = true;
        }
        private void btn6_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //btn6.focusState = true;
        }

        private void btn1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sFuncSelect(0);
        }
        private void btn2_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sFuncSelect(1);
        }
        private void btn3_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sFuncSelect(2);
        }
        private void btn4_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sFuncSelect(3);
        }
        private void btn5_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sFuncSelect(4);
        }
        private void btn6_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sFuncSelect(5);
        }

        private void btnDA1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //btnDA1.focusState = true;
        }

        private void btnDA0_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //btnDA0.focusState = true;
        }

        private void btnDA1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sValueA(1);
        }

        private void btnDA0_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sValueA(0);
        }
    }
}
