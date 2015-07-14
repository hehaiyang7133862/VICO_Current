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
    /// Interaction logic for set_moldclose.xaml
    /// </summary>
    public partial class set_moldclose : UserControl
    {
        public set_moldclose()
        {
            InitializeComponent();
        }
        public void fdFunc(double value)
        {
            btnPA.focusState = false;
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
            iprCtrl.curUnit.get_sOperateType();
            btn1.focusState = false;
            btn2.focusState = false;
            switch (iprCtrl.curUnit.sOperateType)
            {
                case 0:
                    {
                        btn1.focusState = true;
                        tbValue.SelectedIndex = 0;
                        if (iprCtrl.curUnit.sErrOpSelect)
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
                        tbValue.SelectedIndex = 1;
                        btnPA.dis = iprCtrl.curUnit.getStrValueA();
                        if (iprCtrl.curUnit.sErrOpSelect)
                        {
                            btn2.setErrValue();
                        }
                        else
                        {
                            btn2.clearErrValue();
                        }
                        if (iprCtrl.curUnit.sErrValueA)
                        {
                            btnPA.setErrValue();
                        }
                        else
                        {
                            btnPA.clearErrValue();
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

        private void btn1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sOperateType(0);
        }
        private void btn2_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sOperateType(1);
        }
        private void btnPA_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //btnPA.focusState = true;

        }
        private void btnPA_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //if (btnPA.downState)
            //{
                Thickness margin = new Thickness(200, 200, 0, 0);
                iprCtrl.curUnit.get_sValueAObj();
                //valmoWin.getNumValueHandle(iprCtrl.curUnit.objValueA, margin, fdObj);
                valmoWin.SNumKeyPanel.init(iprCtrl.curUnit.objValueA, numkeyDisposeFunc);
                btnPA.focusState = true;
            //}
        }
        private void numkeyDisposeFunc()
        {
            btnPA.focusState = false;
        }
    }
}
