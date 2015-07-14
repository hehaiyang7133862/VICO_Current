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
    /// motorOnBtn.xaml 的交互逻辑
    /// </summary>
    public partial class motorOnBtn : UserControl
    {
        objUnit rateObj;
        objUnit motorOnObj;
        public motorOnBtn()
        {
            InitializeComponent();

            rateObj = valmoWin.dv.SysPr[186];
            motorOnObj = valmoWin.dv.KeyPr[16];
            rateObj.addHandle(handleSysPr186);
            rate.Opacity = 0;
            motorOnObj.addHandle(handleState);
        }

        private void handleState(objUnit obj)
        {
            switch (obj.value)
            {
                case 0:
                    BtnFore.Visibility = Visibility.Visible;
                    BtnForeActive.Visibility = Visibility.Hidden;
                    break;
                case 1:
                    BtnFore.Visibility = Visibility.Hidden;
                    BtnForeActive.Visibility = Visibility.Visible;
                    break;
            }
        }
        private void handleSysPr186(objUnit obj)
        {

            this.rate.rateValue = 100.0 * obj.value / 2000;
        }

        bool isMousedown = false;
        private void cvsMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isMousedown = true;
            btnDown.Visibility = Visibility.Visible;
            motorOnObj.valueNew = 1;
            rate.rateValue = 0;
            rate.Opacity = 1;
        }

        private void cvsMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isMousedown)
            {
                isMousedown = false;
                btnDown.Visibility = Visibility.Hidden;
                motorOnObj.valueNew = 0;
                rate.Opacity = 0;

                if (motorOnObj.lstValue != 1 && motorOnObj.lstValue != 11)
                {
                    valmoWin.dv.KeyPr[17].valueNew = 1;
                    valmoWin.dv.KeyPr[17].valueNew = 0;
                }
            }
        }

        private void cvsMain_MouseLeave(object sender, MouseEventArgs e)
        {
            cvsMain_MouseUp(null, null);
        }
    }
}
