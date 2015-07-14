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
    /// wait_startSignal.xaml 的交互逻辑
    /// </summary>
    public partial class wait_startSignal : UserControl
    {
        private string curOpeName = string.Empty;

        public wait_startSignal()
        {
            InitializeComponent();

            cvsValue.Opacity = 0;
        }

        public void setValue()
        {
            //获取功能错误信息
            iprCtrl.curUnit.get_sNotReady();

            if (iprCtrl.curUnit.sNotReady != 0)
            {
                if (iprCtrl.curUnit.sErrLink)
                {
                    activeErr1Ctrl1.Visibility = Visibility.Visible;
                    activeErr1Ctrl1.dis = "触发异常";
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
                    activeErr1Ctrl1.Visibility = Visibility.Hidden;
                }
                if (iprCtrl.curUnit.sErrUndefined)
                {
                    activeErr1Ctrl1.Visibility = Visibility.Visible;
                    activeErr1Ctrl1.dis = "功能未定义";
                }
                else
                {
                    activeErr1Ctrl1.Visibility = Visibility.Hidden;
                }
            }
            //特性选择
            iprCtrl.curUnit.get_sFuncSelect();

            btn1.focusState = false;
            btn2.focusState = false;
            btn3.focusState = false;
            btn4.focusState = false;

            switch (iprCtrl.curUnit.sFuncSelect)
            {
                case 0:
                    {
                        if (iprCtrl.curUnit.sErrFuncSelect)
                        {
                            btn1.setErrValue();
                        }
                        else
                        {
                            btn1.clearErrValue();
                        }

                        curOpeName = valmoWin.dv.getCurDis("ipr_waitStartSignal_MoldClose");
                        btn1.focusState = true;
                        cvsValue.Opacity = 0;
                    }
                    break;
                case 1:
                    {
                        if (iprCtrl.curUnit.sErrFuncSelect)
                        {
                            btn2.setErrValue();
                        }
                        else
                        {
                            btn2.clearErrValue();
                        }

                        curOpeName = valmoWin.dv.getCurDis("ipr_waitStartSignal_MoldOpen");
                        btn2.focusState = true;
                        cvsValue.Opacity = 0;
                    }
                    break;
                case 2:
                    {
                        if (iprCtrl.curUnit.sErrFuncSelect)
                        {
                            btn3.setErrValue();
                        }
                        else
                        {
                            btn3.clearErrValue();
                        }

                        curOpeName = valmoWin.dv.getCurDis("ipr_waitStartSignal_EjectorBWD");
                        btn3.focusState = true;
                        cvsValue.Opacity = 1;
                        btnVC.dis = iprCtrl.curUnit.getStrValueC();
                    }
                    break;
                case 3:
                    {
                        if (iprCtrl.curUnit.sErrFuncSelect)
                        {
                            btn4.setErrValue();
                        }
                        else
                        {
                            btn4.clearErrValue();
                        }

                        curOpeName = valmoWin.dv.getCurDis("ipr_waitStartSignal_EjectorFWD");
                        btn4.focusState = true;
                        cvsValue.Opacity = 1;
                        btnVC.dis = iprCtrl.curUnit.getStrValueC();
                    }
                    break;
                default:
                    break;
            }
        }

        private void btn1_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void btn1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sFuncSelect(0);
        }

        private void btn2_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void btn2_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sFuncSelect(1);
        }
        private void btn3_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void btn3_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sFuncSelect(2);
        }

        private void btn4_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void btn4_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sFuncSelect(3);
        }

        private void btnVC_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnVC_MouseUp(object sender, MouseButtonEventArgs e)
        {
            btnVC.focusState = true;
            Thickness margin = new Thickness(200, 200, 0, 0);
            iprCtrl.curUnit.get_sValueCObj();
            valmoWin.SNumKeyPanel.init(iprCtrl.curUnit.objValueC, "跳转至", numkeyDisposeFunc);
        }
        private void numkeyDisposeFunc()
        {
            btnVC.focusState = false;
        }
    }
}
