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
    /// motorOffBtn.xaml 的交互逻辑
    /// </summary>
    public partial class motorOffBtn : UserControl
    {
        objUnit motorOffObj;
        public motorOffBtn()
        {
            InitializeComponent();
            motorOffObj = valmoWin.dv.KeyPr[17];
            motorOffObj.addHandle(handleState);
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

        bool isMousedown = false;
        private void cvsMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isMousedown = true;
            btnDown.Visibility = Visibility.Visible;
            motorOffObj.valueNew = 1;
        }

        private void cvsMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isMousedown)
            {
                isMousedown = false;
                btnDown.Visibility = Visibility.Hidden;
                motorOffObj.valueNew = 0;
            }
        }

        private void cvsMain_MouseLeave(object sender, MouseEventArgs e)
        {
            cvsMain_MouseUp(null, null);
        }
    }
}
