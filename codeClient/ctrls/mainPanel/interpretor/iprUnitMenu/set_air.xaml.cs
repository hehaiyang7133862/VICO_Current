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
    /// Interaction logic for set_air.xaml
    /// </summary>
    public partial class set_air : UserControl
    {
        
        public set_air()
        {
            InitializeComponent();
        }
        public void fdFunc(double value)
        {
            btnB1.focusState = false;
        }
        private void numkeyDisposeFunc()
        {
            btnB1.focusState = false;
            btnKeepTm.focusState = false;
        }
        string curFuncName = "";
        public void setValue()
        {
            
            iprCtrl.curUnit.get_sFuncSelect();
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
            iprCtrl.curUnit.get_sl_setup();


            btn1.Visibility = iprCtrl.curUnit.flagAir1 ? Visibility.Visible : Visibility.Hidden;
            btn2.Visibility = iprCtrl.curUnit.flagAir2 ? Visibility.Visible : Visibility.Hidden;
            btn3.Visibility = iprCtrl.curUnit.flagAir3 ? Visibility.Visible : Visibility.Hidden;
            btn4.Visibility = iprCtrl.curUnit.flagAir4 ? Visibility.Visible : Visibility.Hidden;
            btn5.Visibility = iprCtrl.curUnit.flagAir5 ? Visibility.Visible : Visibility.Hidden;
            btn6.Visibility = iprCtrl.curUnit.flagAir6 ? Visibility.Visible : Visibility.Hidden;

            btn7.Visibility = iprCtrl.curUnit.flagAir7 ? Visibility.Visible : Visibility.Hidden;
            btn8.Visibility = iprCtrl.curUnit.flagAir8 ? Visibility.Visible : Visibility.Hidden;
            btn9.Visibility = iprCtrl.curUnit.flagAir9 ? Visibility.Visible : Visibility.Hidden;
            btn10.Visibility = iprCtrl.curUnit.flagAir10 ? Visibility.Visible : Visibility.Hidden;
            btn11.Visibility = iprCtrl.curUnit.flagAir11 ? Visibility.Visible : Visibility.Hidden;
            btn12.Visibility = iprCtrl.curUnit.flagAir12 ? Visibility.Visible : Visibility.Hidden;
            if (iprCtrl.curUnit.flagAir1 ||
                iprCtrl.curUnit.flagAir2 ||
                iprCtrl.curUnit.flagAir3 ||
                iprCtrl.curUnit.flagAir4 ||
                iprCtrl.curUnit.flagAir5 ||
                iprCtrl.curUnit.flagAir6 ||
                iprCtrl.curUnit.flagAir7 ||
                iprCtrl.curUnit.flagAir8 ||
                iprCtrl.curUnit.flagAir9 ||
                iprCtrl.curUnit.flagAir10 ||
                iprCtrl.curUnit.flagAir11 ||
                iprCtrl.curUnit.flagAir12
                )
            {
                cvsAirSet.Visibility = Visibility.Visible;
            }
            else
            {
                cvsAirSet.Visibility = Visibility.Hidden;
                return;
            }

            btn1.focusState = false;
            btn2.focusState = false;
            btn3.focusState = false;
            btn4.focusState = false;
            btn5.focusState = false;
            btn6.focusState = false;
            btn7.focusState = false;
            btn8.focusState = false;
            btn9.focusState = false;
            btn10.focusState = false;
            btn11.focusState = false;
            btn12.focusState = false;
            iprCtrl.curUnit.get_sNotReady();

            switch (iprCtrl.curUnit.sFuncSelect)
            {
                case 0:
                    {
                        btn1.focusState = true;
                        curFuncName = "吹气1";
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
                        curFuncName = "吹气2";
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
                        curFuncName = "吹气3";
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
                        curFuncName = "吹气4";
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
                        curFuncName = "吹气5";
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
                        curFuncName = "吹气6";
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
                case 6:
                    {
                        btn7.focusState = true;
                        curFuncName = "吹气7";
                        if (iprCtrl.curUnit.sErrFuncSelect)
                        {
                            btn7.setErrValue();
                        }
                        else
                        {
                            btn7.clearErrValue();
                        }
                    }
                    break;
                case 7:
                    {
                        btn8.focusState = true;
                        curFuncName = "吹气8";
                        if (iprCtrl.curUnit.sErrFuncSelect)
                        {
                            btn8.setErrValue();
                        }
                        else
                        {
                            btn8.clearErrValue();
                        }
                    }
                    break;
                case 8:
                    {
                        btn9.focusState = true;
                        curFuncName = "吹气9";
                        if (iprCtrl.curUnit.sErrFuncSelect)
                        {
                            btn9.setErrValue();
                        }
                        else
                        {
                            btn9.clearErrValue();
                        }
                    }
                    break;
                case 9:
                    {
                        btn10.focusState = true;
                        curFuncName = "吹气10";
                        if (iprCtrl.curUnit.sErrFuncSelect)
                        {
                            btn10.setErrValue();
                        }
                        else
                        {
                            btn10.clearErrValue();
                        }
                    }
                    break;
                case 10:
                    {
                        btn11.focusState = true;
                        curFuncName = "吹气11";
                        if (iprCtrl.curUnit.sErrFuncSelect)
                        {
                            btn11.setErrValue();
                        }
                        else
                        {
                            btn11.clearErrValue();
                        }
                    }
                    break;
                case 11:
                    {
                        btn12.focusState = true;
                        curFuncName = "吹气12";
                        if (iprCtrl.curUnit.sErrFuncSelect)
                        {
                            btn12.setErrValue();
                        }
                        else
                        {
                            btn12.clearErrValue();
                        }
                    }
                    break;
            }
            btnB1.dis = iprCtrl.curUnit.getStrValueB();
            if (iprCtrl.curUnit.sErrValueB)
            {
                btnB1.setErrValue();
            }
            else
            {
                btnB1.clearErrValue();
            }
            btnKeepTm.dis = iprCtrl.curUnit.getStrValueA();
        }

        private void btn1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //btn1.downState = true;
        }
        private void btn2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //btn2.downState = true;
        }
        private void btn3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //btn3.downState = true;
        }
        private void btn4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //btn4.downState = true;
        }
        private void btn5_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //btn5.downState = true;
        }
        private void btn6_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //btn6.downState = true;
        }
        private void btn7_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //btn7.downState = true;
        }
        private void btn8_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //btn8.downState = true;
        }
        private void btn9_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //btn9.downState = true;
        }
        private void btn10_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //btn10.downState = true;
        }
        private void btn11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //btn11.downState = true;
        }
        private void btn12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //btn12.downState = true;
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
        private void btn7_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sFuncSelect(6);
        }
        private void btn8_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sFuncSelect(7);
        }
        private void btn9_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sFuncSelect(8);
        }
        private void btn10_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sFuncSelect(9);
        }
        private void btn11_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sFuncSelect(10);
        }
        private void btn12_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sFuncSelect(11);
        }
        private void btnB1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //btnB1.downState = true;
        }
        private void btnB1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //if (btnB1.downState)
            //{
                Thickness margin = new Thickness(200, 200, 0, 0);
                iprCtrl.curUnit.get_sValueBObj();
                valmoWin.SNumKeyPanel.init(iprCtrl.curUnit.objValueB, curFuncName + valmoWin.dv.getCurDis("lanKey1021"),numkeyDisposeFunc);
                btnB1.focusState = false;
            //}
        }

        private void btnKeepTm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //btnKeepTm.downState = true;
        }

        private void btnKeepTm_MouseUp(object sender, MouseButtonEventArgs e)
        {
                Thickness margin = new Thickness(200, 200, 0, 0);
                iprCtrl.curUnit.get_sValueAObj();
                valmoWin.SNumKeyPanel.init(iprCtrl.curUnit.objValueA, valmoWin.dv.getCurDis("lanKey1020"), numkeyDisposeFunc);
                btnKeepTm.focusState = false;
        }
    }
}
