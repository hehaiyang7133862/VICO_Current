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
using nsDataMgr;

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// moldHeatingBtn.xaml 的交互逻辑
    /// </summary>
    public partial class moldHeatingBtn : UserControl
    {
        private objUnit curObj;
        public moldHeatingBtn()
        {
            InitializeComponent();
            curObj = valmoWin.dv.KeyPr[52];
            curObj.addHandle(btnStateFunc, plcLstSpd.mapType);
        }

        public void btnStateFunc(objUnit obj)
        {

            switch (obj.value)
            {
                case 1:
                    {
                        BtnActive.Visibility = Visibility.Visible;
                        btnBg.Visibility = Visibility.Hidden;
                    }
                    break;
                case 0:
                    {
                        BtnActive.Visibility = Visibility.Hidden;
                        btnBg.Visibility = Visibility.Visible;
                    }
                    break;
            }
        }


        bool isMousedown = false;
        private void focusCtrl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isMousedown = true;
            btnBg.Visibility = Visibility.Hidden;
            btnDown.Visibility = Visibility.Visible;
            if (curObj != null)
            {
                curObj.valueNew = 1;
            }
        }

        private void focusCtrl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isMousedown)
            {
                isMousedown = false;
                btnBg.Visibility = BtnActive.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
                btnDown.Visibility = Visibility.Hidden;
                if (curObj != null)
                {
                    curObj.valueNew = 0;
                }
            }
            
        }

        private void focusCtrl_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isMousedown)
            {
                isMousedown = false;
                btnBg.Visibility = BtnActive.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
                btnDown.Visibility = Visibility.Hidden;
                if (curObj != null)
                {
                    curObj.valueNew = 0;
                }
            }
            
        }
    }
}
