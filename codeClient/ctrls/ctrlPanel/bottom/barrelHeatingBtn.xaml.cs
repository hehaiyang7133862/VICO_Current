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
    /// barrelHeatingBtn.xaml 的交互逻辑
    /// </summary>
    public partial class barrelHeatingBtn : UserControl
    {
        private objUnit curObj;
        public barrelHeatingBtn()
        {
            InitializeComponent();
            curObj = valmoWin.dv.KeyPr[14];
            curObj.addHandle(handleState);
        }

        private void handleState(objUnit obj)
        {
            switch (obj.value)
            {
                case 0:

                    break;
                case 1:

                    break;
            }
        }

        bool isMousedown = false;
        private void focusCtrl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isMousedown = true;
        }

        private void focusCtrl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isMousedown)
            {
                isMousedown = false;
                btnBg.Visibility = Visibility.Hidden;
                btnDown.Visibility = Visibility.Visible;
            }
            
        }

        private void focusCtrl_MouseLeave(object sender, MouseEventArgs e)
        {
            focusCtrl_MouseUp(null, null);
        }
    }
}
