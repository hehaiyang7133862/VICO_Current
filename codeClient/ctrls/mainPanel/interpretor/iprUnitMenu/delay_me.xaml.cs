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
    /// Interaction logic for delay_me.xaml
    /// </summary>
    public partial class delay_me : UserControl
    {
        public delay_me()
        {
            InitializeComponent();
        }
        public void setValue()
        {
            btnValueA.dis = iprCtrl.curUnit.getStrValueA();
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
            if (iprCtrl.curUnit.sErrValueA)
            {
                btnValueA.setErrValue();
            }
            else
            {
                btnValueA.clearErrValue();
            }
        }

        private void btnValueA_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //btnValueA.downState = true;
        }

        private void btnValueA_MouseUp(object sender, MouseButtonEventArgs e)
        {
                Thickness margin = new Thickness(200, 200, 0, 0);
                iprCtrl.curUnit.get_sValueAObj();
                btnValueA.focusState = true;
                valmoWin.SNumInput.init(iprCtrl.curUnit.objValueA, valmoWin.dv.getCurDis("lanKey966"), numkeyPanelDisposeFunc, confirmFunc);
        }
        public void numkeyPanelDisposeFunc()
        {
            btnValueA.focusState = false;
        }
        private void confirmFunc(double newValue)
        {
            btnValueA.focusState = false;
            iprCtrl.curUnit.objValueA.setValue(newValue);
        }
    }
}
