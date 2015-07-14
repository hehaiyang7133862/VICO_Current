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
    /// adjustBtn.xaml 的交互逻辑
    /// </summary>
    public partial class adjustBtn : UserControl
    {
        objUnit curObj;
        public adjustBtn()
        {
            InitializeComponent();
            curObj = valmoWin.dv.KeyPr[5];
            curObj.addHandle(handleAdjust);
        }

        private void handleAdjust(objUnit obj)
        {
            switch (obj.value)
            {
                case 0:
                    {
                        BtnForeActive.Visibility = Visibility.Hidden;
                        BtnFore.Visibility = Visibility.Visible;
                    }
                    break;
                case 1:
                    {
                        BtnForeActive.Visibility = Visibility.Visible;
                        BtnFore.Visibility = Visibility.Hidden;
                    }
                    break;
            }
        }
        bool isMousedown = false;
        private void cvsMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isMousedown = true;
            btnDown.Visibility = Visibility.Visible;
            curObj.valueNew = 1;
        }

        private void cvsMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isMousedown)
            {
                isMousedown = false;
                btnDown.Visibility = Visibility.Hidden;
                curObj.valueNew = 0;
            }
        }

        private void cvsMain_MouseLeave(object sender, MouseEventArgs e)
        {
            cvsMain_MouseUp(null, null);
        }
    }
}
