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
    /// Interaction logic for set_cores.xaml
    /// </summary>
    public partial class set_cores : UserControl
    {
        public set_cores()
        {
            InitializeComponent();
        }
        public void fdFunc(double value)
        {
            btnKeepTm.focusState = false;
            btnActTm.focusState = false;
        }
        private void numkeyDisposeFunc()
        {
            btnKeepTm.focusState = false;
            btnActTm.focusState = false;
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
            iprCtrl.curUnit.get_sl_setup();

            btn1.Visibility = iprCtrl.curUnit.flagCoreAVisible ? Visibility.Visible : Visibility.Hidden;
            btn2.Visibility = iprCtrl.curUnit.flagCoreBVisible ? Visibility.Visible : Visibility.Hidden;
            btn3.Visibility = iprCtrl.curUnit.flagCoreCVisible ? Visibility.Visible : Visibility.Hidden;
            btn4.Visibility = iprCtrl.curUnit.flagCoreDVisible ? Visibility.Visible : Visibility.Hidden;
            btn5.Visibility = iprCtrl.curUnit.flagCoreEVisible ? Visibility.Visible : Visibility.Hidden;
            btn6.Visibility = iprCtrl.curUnit.flagCoreFVisible ? Visibility.Visible : Visibility.Hidden;
            if (iprCtrl.curUnit.flagCoreAVisible ||
                iprCtrl.curUnit.flagCoreBVisible ||
                iprCtrl.curUnit.flagCoreCVisible ||
                iprCtrl.curUnit.flagCoreDVisible ||
                iprCtrl.curUnit.flagCoreEVisible ||
                iprCtrl.curUnit.flagCoreFVisible
                )
            {
                cvsOp.Visibility = Visibility.Visible;
                //cvsKeep.Visibility = Visibility.Visible;
            }
            else
            {
                cvsOp.Visibility = Visibility.Hidden;
                //cvsKeep.Visibility = Visibility.Hidden;
                return;
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
            iprCtrl.curUnit.get_sOperateType();
            btnOp1.focusState = false;
            btnOp2.focusState = false;
            switch (iprCtrl.curUnit.sOperateType)
            {
                case 0:
                    {
                        btnOp1.focusState = true;
                        //cvsKeep.Visibility = Visibility.Visible;
                        iprCtrl.curUnit.get_sValueC();
                        btnKeep.focusState = iprCtrl.curUnit.sValueC == 0;
                        if (iprCtrl.curUnit.sErrOpSelect)
                        {
                            btnOp1.setErrValue();
                        }
                        else
                        {
                            btnOp1.clearErrValue();
                        }
                        if (iprCtrl.curUnit.sErrValueC)
                        {
                            btnKeep.setErrValue();
                        }
                        else
                        {
                            btnKeep.clearErrValue();
                        }
                    }
                    break;
                case 1:
                    {
                        btnOp2.focusState = true;
                        //cvsKeep.Visibility = Visibility.Hidden;
                        iprCtrl.curUnit.get_sValueC();
                        btnKeep.focusState = iprCtrl.curUnit.sValueC == 0;
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
            iprCtrl.curUnit.get_sValueA();
            btnKeepTm.dis = iprCtrl.curUnit.getStrValueA();
            iprCtrl.curUnit.get_sValueB();
            btnActTm.dis = iprCtrl.curUnit.getStrValueB();

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
            //btnOp1.downState = true;
        }
        private void btnDA0_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //btnOp2.downState = true;
        }

        private void btnDA1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sOperateType(0);
        }
        private void btnDA0_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sOperateType(1);
        }

        private void btnKeep_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //btnKeep.downState = true;
        }

        private void btnKeep_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.get_sValueC();
            if(iprCtrl.curUnit.sValueC == 0)
                iprCtrl.curUnit.set_sValueC(1);
            else
                iprCtrl.curUnit.set_sValueC(0);
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
            btnKeepTm.focusState = true;
        }

        private void btnActTm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //btnActTm.downState = true;
        }

        private void btnActTm_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Thickness margin = new Thickness(200, 200, 0, 0);
            iprCtrl.curUnit.get_sValueBObj();
            valmoWin.SNumKeyPanel.init(iprCtrl.curUnit.objValueB, valmoWin.dv.getCurDis("lanKey1021"), numkeyDisposeFunc);
            btnActTm.focusState = true;
        }
    }
}
