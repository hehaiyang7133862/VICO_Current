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
    /// wait_EMSignal.xaml 的交互逻辑
    /// </summary>
    public partial class wait_EMSignal : UserControl
    {
        private string curOpeName = string.Empty;

        public wait_EMSignal()
        {
            InitializeComponent();
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
            btn5.focusState = false;
            btn6.focusState = false;

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

                        curOpeName = valmoWin.dv.getCurDis("ipr_waitEmSignal_MoldClose");
                        btn1.focusState = true;

                        btnVC.dis = iprCtrl.curUnit.getStrValueA();
                        if (iprCtrl.curUnit.sErrValueA)
                        {
                            btnVC.setErrValue();
                        }
                        else
                        {
                            btnVC.clearErrValue();
                        }
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

                        curOpeName = valmoWin.dv.getCurDis("ipr_waitEmSignal_MoldOpen");
                        btn2.focusState = true;

                        btnVC.dis = iprCtrl.curUnit.getStrValueA();
                        if (iprCtrl.curUnit.sErrValueA)
                        {
                            btnVC.setErrValue();
                        }
                        else
                        {
                            btnVC.clearErrValue();
                        }
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

                        curOpeName = valmoWin.dv.getCurDis("ipr_waitEmSignal_EjectorBWD");
                        btn3.focusState = true;

                        btnVC.dis = iprCtrl.curUnit.getStrValueA();
                        if (iprCtrl.curUnit.sErrValueA)
                        {
                            btnVC.setErrValue();
                        }
                        else
                        {
                            btnVC.clearErrValue();
                        }
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

                        curOpeName = valmoWin.dv.getCurDis("ipr_waitEmSignal_EjectorFWD");
                        btn4.focusState = true;

                        btnVC.dis = iprCtrl.curUnit.getStrValueA();
                        if (iprCtrl.curUnit.sErrValueA)
                        {
                            btnVC.setErrValue();
                        }
                        else
                        {
                            btnVC.clearErrValue();
                        }
                    }
                    break;
                case 4:
                    {
                        if (iprCtrl.curUnit.sErrFuncSelect)
                        {
                            btn5.setErrValue();
                        }
                        else
                        {
                            btn5.clearErrValue();
                        }

                        curOpeName = valmoWin.dv.getCurDis("ipr_waitEmSignal_CoreFWD");
                        btn5.focusState = true;

                        iprCtrl.curUnit.getStrValueA();


                        btnVC.dis = iprCtrl.curUnit.getStrValueA();
                        if (iprCtrl.curUnit.sErrValueA)
                        {
                            btnVC.setErrValue();
                        }
                        else
                        {
                            btnVC.clearErrValue();
                        }
                    }
                    break;
                case 5:
                    {
                        if (iprCtrl.curUnit.sErrFuncSelect)
                        {
                            btn6.setErrValue();
                        }
                        else
                        {
                            btn6.clearErrValue();
                        }

                        curOpeName = valmoWin.dv.getCurDis("ipr_waitEmSignal_CoreBWD");
                        btn6.focusState = true;

                        iprCtrl.curUnit.getStrValueA();


                        btnVC.dis = iprCtrl.curUnit.getStrValueA();
                        if (iprCtrl.curUnit.sErrValueA)
                        {
                            btnVC.setErrValue();
                        }
                        else
                        {
                            btnVC.clearErrValue();
                        }
                    }
                    break;
                default:
                    break;
            }
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

        private void btnVC_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Thickness margin = new Thickness(200, 200, 0, 0);
            iprCtrl.curUnit.get_sValueAObj();
            btnVC.focusState = true;
            valmoWin.SNumKeyPanel.init(iprCtrl.curUnit.objValueA, curOpeName, numkeyDisposeFunc);
        }

        private void numkeyDisposeFunc()
        {
            btnVC.focusState = false;
        }
    }
}
