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
    /// Interaction logic for set_ejectorOUT.xaml
    /// </summary>
    public partial class set_ejectorOUT : UserControl
    {
        public set_ejectorOUT()
        {
            InitializeComponent();
        }
        public void fdFunc(double value)
        {
            btnPos.focusState = false;
            btnSpd.focusState = false;
            btnTor.focusState = false;
        }
        private void numkeyDisposeFunc()
        {
            btnPos.focusState = false;
            btnSpd.focusState = false;
            btnTor.focusState = false;
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
            btnFunc0.focusState = false;
            btnFunc1.focusState = false;
            bool flagVisible = ((valmoWin.dv.IprPr[18].valueNew >> 1) & 0x01) == 1;
            switch (iprCtrl.curUnit.sFuncSelect)
            {
                case 0:
                    {
                        btnFunc0.focusState = true;
                        if (iprCtrl.curUnit.sErrFuncSelect)
                        {
                            btnFunc0.setErrValue();
                        }
                        else
                        {
                            btnFunc0.clearErrValue();
                        }
                    }
                    break;
                case 1:
                    {
                        btnFunc1.focusState = true;
                        if (iprCtrl.curUnit.sErrFuncSelect)
                        {
                            btnFunc1.setErrValue();
                        }
                        else
                        {
                            btnFunc1.clearErrValue();
                        }
                    }
                    break;
            }
            if (flagVisible)
            {
                btnFunc1.Visibility = Visibility.Visible;
            }
            else
            {
                btnFunc1.Visibility = Visibility.Hidden;
            }
            iprCtrl.curUnit.get_sOperateType();
            btnOp0.focusState = false;
            btnOp1.focusState = false;
            switch (iprCtrl.curUnit.sOperateType)
            {
                case 0:
                    {
                        btnOp0.focusState = true;
                        cvsOp1.Visibility = Visibility.Hidden;
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
                        cvsOp1.Visibility = Visibility.Visible;
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
            }
            btnPos.dis = iprCtrl.curUnit.getStrValueA();
            if (iprCtrl.curUnit.sErrValueA)
            {
                btnPos.setErrValue();
            }
            else
            {
                btnPos.clearErrValue();
            }
            btnSpd.dis = iprCtrl.curUnit.getStrValueB();
            if (iprCtrl.curUnit.sErrValueB)
            {
                btnSpd.setErrValue();
            }
            else
            {
                btnSpd.clearErrValue();
            }
            btnTor.dis = iprCtrl.curUnit.getStrValueC();
            if (iprCtrl.curUnit.sErrValueC)
            {
                btnTor.setErrValue();
            }
            else
            {
                btnTor.clearErrValue();
            }
        }

        private void btnFunc0_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //btnFunc0.focusState = true;
        }

        private void btnFunc1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //btnFunc1.focusState = true;
        }

        private void btnFunc0_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sFuncSelect(0);
        }

        private void btnFunc1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sFuncSelect(1);
        }

        private void btnOp0_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //btnOp0.focusState = true;
        }

        private void btnOp1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //btnOp1.focusState = true;
        }

        private void btnOp0_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sOperateType(0);
        }

        private void btnOp1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sOperateType(1);
        }

        private void btnPos_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //btnPos.focusState = true;
        }

        private void btnSpd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //btnSpd.focusState = true;
        }

        private void btnTor_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //btnTor.focusState = true;
        }

        private void btnPos_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Thickness margin = new Thickness(200, 200, 0, 0);
            iprCtrl.curUnit.get_sValueAObj();
            valmoWin.SNumKeyPanel.init(iprCtrl.curUnit.objValueA, "目标位置", numkeyDisposeFunc);
            btnPos.focusState = true;
        }

        private void btnSpd_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Thickness margin = new Thickness(200, 200, 0, 0);
            iprCtrl.curUnit.get_sValueBObj();
            btnSpd.focusState = true;
            valmoWin.SNumKeyPanel.init(iprCtrl.curUnit.objValueB, "设定速度", numkeyDisposeFunc);
        }

        private void btnTor_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Thickness margin = new Thickness(200, 200, 0, 0);
            iprCtrl.curUnit.get_sValueCObj();
            btnTor.focusState = true;
            valmoWin.SNumKeyPanel.init(iprCtrl.curUnit.objValueC, "设定扭力", numkeyDisposeFunc);
        }
    }
}
