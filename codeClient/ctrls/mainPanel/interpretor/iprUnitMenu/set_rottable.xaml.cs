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
    /// Interaction logic for set_rottable.xaml
    /// </summary>
    public partial class set_rottable : UserControl
    {
        public set_rottable()
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
            btnB1.dis = iprCtrl.curUnit.sFuncSelect.ToString();
            iprCtrl.curUnit.get_sOperateType();
            btnKeepTm.dis = iprCtrl.curUnit.sOperateType.ToString();
            iprCtrl.curUnit.get_sValueA();
            btnA.dis = iprCtrl.curUnit.sValueA.ToString();
            iprCtrl.curUnit.get_sValueB();
            btnB.dis = iprCtrl.curUnit.sValueB.ToString();
            iprCtrl.curUnit.get_sValueC();
            btnC.dis = iprCtrl.curUnit.sValueC.ToString();
        }

        private void btnB1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //btnB1.focusState = true;
        }
        private void btnB1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //if (btnB1.downState == true)
            //{
                Thickness margin = new Thickness(200, 200, 0, 0);
                iprCtrl.curUnit.get_sValueBObj();
                valmoWin.SNumKeyPanel.init(valmoWin.dv.IprPr[37], "自动识别目标位置", numkeyDisposeFunc);
                btnB1.focusState = true;
            //}
        }
        private void btnKeepTm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //btnKeepTm.focusState = true;
        }

        private void btnKeepTm_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Thickness margin = new Thickness(200, 200, 0, 0);
            iprCtrl.curUnit.get_sOperateType();
            valmoWin.SNumKeyPanel.init(valmoWin.dv.IprPr[38], "sOperateType", numkeyDisposeFunc);
            btnKeepTm.focusState = false;
        }
        private void btnA_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //btnA.focusState = true;
        }

        private void btnA_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Thickness margin = new Thickness(200, 200, 0, 0);
            iprCtrl.curUnit.get_sOperateType();
            valmoWin.SNumKeyPanel.init(iprCtrl.curUnit.objValueA, "ValueA", numkeyDisposeFunc);
            btnA.focusState = false;
        }
        private void btnC_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //btnC.focusState = true;
        }

        private void btnC_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //if (btnC.downState == true)
            //{
                Thickness margin = new Thickness(200, 200, 0, 0);
                iprCtrl.curUnit.get_sOperateType();
                valmoWin.SNumKeyPanel.init(iprCtrl.curUnit.objValueC, "ValueC", numkeyDisposeFunc);
                btnC.focusState = true;
            //}
        }
        private void btnB_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //btnB.focusState = true;
        }

        private void btnB_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //if (btnB.downState == true)
            //{
                Thickness margin = new Thickness(200, 200, 0, 0);
                //iprCtrl.curUnit.get_sOperateType();
                valmoWin.SNumKeyPanel.init(iprCtrl.curUnit.objValueB, "ValueB", numkeyDisposeFunc);
                btnB.focusState = true;
            //}
        }
        private void numkeyDisposeFunc()
        {
            btnKeepTm.focusState = false;
            btnB1.focusState = false;
            btnA.focusState = false;
            btnB.focusState = false;
            btnC.focusState = false;
        }

    }
}
