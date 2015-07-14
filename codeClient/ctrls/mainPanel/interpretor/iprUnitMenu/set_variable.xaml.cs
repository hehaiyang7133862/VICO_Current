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
    /// Interaction logic for set_variable.xaml
    /// </summary>
    public partial class set_variable : UserControl
    {
        string curFuncName = "";
        public set_variable()
        {
            InitializeComponent();
        }
        private void numkeyDisposeFunc()
        {
            btnVB1.focusState = false;
        }
        public void setValue()
        {
            iprCtrl.curUnit.get_sNotReady();
            if (iprCtrl.curUnit.sErrLink)
            {
                activeErr1Ctrl1.Visibility = Visibility.Visible;
                

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
            btn7.focusState = false;
            switch (iprCtrl.curUnit.sFuncSelect)
            {
                case 0:
                    {
                        btn1.focusState = true;
                        curFuncName = "变量A";
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
                        curFuncName = "变量B";
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
                        curFuncName = "变量C";
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
                        curFuncName = "变量D";
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
                        curFuncName = "标记模数";
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
                        curFuncName = "不良品数";
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
                        curFuncName = "生产模数";
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
            }
            iprCtrl.curUnit.get_sOperateType();
            btnOp0.focusState = false;
            btnOp1.focusState = false;
            btnOp2.focusState = false;
            switch (iprCtrl.curUnit.sOperateType)
            {
                case 0:
                    {
                        btnOp0.focusState = true;
                        if (iprCtrl.curUnit.sErrOpSelect)
                        {
                            btnOp0.setErrValue();
                        }
                        else
                        {
                            btnOp0.clearErrValue();
                        }
                    }
                    break;
                case 1:
                    {
                        btnOp1.focusState = true;
                        if (iprCtrl.curUnit.sErrOpSelect)
                        {
                            btnOp1.setErrValue();
                        }
                        else
                        {
                            btnOp1.clearErrValue();
                        }
                    }
                    break;
                case 2:
                    {
                        btnOp2.focusState = true;
                        if (iprCtrl.curUnit.sErrOpSelect)
                        {
                            btnOp2.setErrValue();
                        }
                        else
                        {
                            btnOp2.clearErrValue();
                        }
                    }
                    break;
            }

            btnVB1.dis = iprCtrl.curUnit.getStrValueA();
            if (iprCtrl.curUnit.sErrValueA)
            {
                btnVB1.setErrValue();
            }
            else
            {
                btnVB1.clearErrValue();
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
        private void btn7_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //btn7.focusState = true;
        }

        private void btnOp0_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //btnOp0.focusState = true;
        }
        private void btnOp1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //btnOp1.focusState = true;
        }
        private void btnOp2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //btnOp2.focusState = true;
        }

        private void btnOp0_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sOperateType(0);
        }
        private void btnOp1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sOperateType(1);
        }
        private void btnOp2_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sOperateType(2);
        }

        private void btnVB1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //btnVB1.focusState = true;
        }

        private void btnVB1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Thickness margin = new Thickness(200, 200, 0, 0);
            iprCtrl.curUnit.get_sValueAObj();
            valmoWin.SNumKeyPanel.init(iprCtrl.curUnit.objValueA, curFuncName, numkeyDisposeFunc);
            btnVB1.focusState = true;
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

        private void btnOp1_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            //btnOp1.focusState = true;
        }
        private void btnOp2_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            //btnOp2.focusState = true;
        }

        private void btnOp1_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sOperateType(1);
        }
        private void btnOp2_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sOperateType(2);
        }
    }
}
