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
    /// resetBtn.xaml 的交互逻辑
    /// </summary>
    public partial class resetBtn : UserControl
    {
        public static bool bIsResetBtnEnable = true;
        objUnit objKey;
        public resetBtn()
        {
            InitializeComponent();
            objKey = valmoWin.dv.KeyPr[1];
        }

        bool isMousedown = false;
        private void cvsMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (bIsResetBtnEnable == true)
            {
                isMousedown = true;
                btnBg.Visibility = Visibility.Hidden;
                btnActive.Visibility = Visibility.Visible;
                objKey.valueNew = 1;
            }
        }

        private void cvsMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isMousedown)
            {
                isMousedown = false;
                btnBg.Visibility = Visibility.Visible;
                btnActive.Visibility = Visibility.Hidden;
                objKey.valueNew = 0;
            }
        }

        private void cvsMain_MouseLeave(object sender, MouseEventArgs e)
        {
            cvsMain_MouseUp(null, null);
        }

    }
}
