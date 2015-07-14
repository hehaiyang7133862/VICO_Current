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
    /// Interaction logic for set_carriagebwd.xaml
    /// </summary>
    public partial class set_carriagebwd : UserControl
    {
        public set_carriagebwd()
        {
            InitializeComponent();
        }
        //public void fdFunc(double value)
        //{
        //    btnPA.focusState = false;
        //    btnPD.focusState = false;
        //}

        public void setValue()
        {
            iprCtrl.curUnit.get_sOperateType();
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
            //btn1.focusState = false;
            //btn2.focusState = false;
            //btn3.focusState = false;
            //bool flagVisible = ((valmoWin.dv.IprPr[18].valueNew >> 18) & 0x01) == 1;

            //if (flagVisible)
            //{
            //    btn2.Visibility = Visibility.Visible;
            //    btn3.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    btn2.Visibility = Visibility.Hidden;
            //    btn3.Visibility = Visibility.Hidden;
            //}
            //iprCtrl.curUnit.get_sNotReady();
            //switch (iprCtrl.curUnit.sOperateType)
            //{
            //    case 0:
            //        {
            //            btn1.focusState = true;
            //            tbValue.SelectedIndex = 0;
            //            if (iprCtrl.curUnit.sErrOpSelect)
            //            {
            //                btn1.setErrValue();
            //            }
            //            else
            //            {
            //                btn1.clearErrValue();
            //            }
            //        }
            //        break;
            //    case 1:
            //        {
            //            if (flagVisible)
            //            {
            //                btn2.focusState = true;
            //                tbValue.SelectedItem = menu_pos;
            //                iprCtrl.curUnit.get_sValueA();
            //                btnPD.dis = iprCtrl.curUnit.getStrValueD();
            //                if (iprCtrl.curUnit.sErrOpSelect)
            //                {
            //                    btn2.setErrValue();
            //                }
            //                else
            //                {
            //                    btn2.clearErrValue();
            //                }
            //            }

            //        }
            //        break;
            //    case 2:
            //        {
            //            if (flagVisible)
            //            {
            //                btn3.focusState = true;
            //                tbValue.SelectedItem = menu_prs;
            //                btnPA.dis = iprCtrl.curUnit.getStrValueA();
            //                if (iprCtrl.curUnit.sErrOpSelect)
            //                {
            //                    btn3.setErrValue();
            //                }
            //                else
            //                {
            //                    btn3.clearErrValue();
            //                }
            //            }
            //        }
            //        break;
            //}
        }

        //private void btn1_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    //btn1.downState = true;
        //}
        //private void btn2_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    //btn2.downState = true;
        //}
        //private void btn3_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    //btn3.downState = true;
        //}

        //private void btn1_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    iprCtrl.curUnit.set_sOperateType(0);
        //}
        //private void btn2_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    iprCtrl.curUnit.set_sOperateType(1);
        //}
        //private void btn3_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    iprCtrl.curUnit.set_sOperateType(2);
        //}

        //private void btnPA_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    //btnPA.downState = true;
        //}

        //private void btnPA_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //        Thickness margin = new Thickness(200, 200, 0, 0);
        //        iprCtrl.curUnit.get_sValueAObj();
        //        valmoWin.SNumKeyPanel.init(iprCtrl.curUnit.objValueA, numkeyDisposeFunc);
        //        btnPA.focusState = true;
        //}

        //private void btnPD_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    //btnPD.downState = true;
        //}
        //private void numkeyDisposeFunc()
        //{
        //    btnPA.focusState = false;
        //    btnPD.focusState = false;
        //}
        //private void btnPD_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //        Thickness margin = new Thickness(200, 200, 0, 0);
        //        iprCtrl.curUnit.get_sValueDObj();
        //        valmoWin.SNumKeyPanel.init(iprCtrl.curUnit.objValueD,numkeyDisposeFunc);
        //        btnPD.downState = true;
        //}
    }
}
