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
    /// emStopBtn.xaml 的交互逻辑
    /// </summary>
    public partial class emStopBtn : UserControl
    {
        objUnit objKey;
        public emStopBtn()
        {
            InitializeComponent();
            objKey = valmoWin.dv.KeyPr[51];
            objKey.addHandle(btnStateFunc, plcLstSpd.mapType);
        }
        public void btnStateFunc(objUnit obj)
        {
            if (obj.value == 0)
            {
                cvsEmStopActive.Visibility = Visibility.Hidden;
                emStopActiveBg.Visibility = Visibility.Hidden;
                Canvas.SetLeft(cvsEmStopActive, 0);
            }
            else
            {
                cvsEmStopActive.Visibility = Visibility.Visible;
                emStopActiveBg.Visibility = Visibility.Visible;
            }
        }

        private void imgEmStop_MouseDown(object sender, MouseButtonEventArgs e)
        {
            objKey.valueNew = 1;
        }
        bool isMouseDown = false;
        Point mousePoint;
        private void imgEmStopActive_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = true;
            mousePoint = e.GetPosition(cvsMain);
        }

        private void cvsMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (objKey.value == 1)
            {
                if (isMouseDown == true)
                {
                    if (e.LeftButton == MouseButtonState.Pressed)
                    {

                        Point theMousePoint = e.GetPosition(cvsMain);
                        double tmpLeft = Canvas.GetLeft(cvsEmStopActive) + theMousePoint.X - mousePoint.X;
                        if (tmpLeft > 0)
                            tmpLeft = 0;
                        else if (tmpLeft < -150)
                        {
                            valmoWin.dv.KeyPr[51].valueNew = 0;
                            cvsEmStopActive.Visibility = Visibility.Hidden;
                            isMouseDown = false;
                        }
                        Canvas.SetLeft(cvsEmStopActive, tmpLeft);
                        mousePoint = theMousePoint;
                    }
                }
            }
        }

        private void cvsMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Canvas.GetLeft(cvsEmStopActive) > -149)
            {
                Canvas.SetLeft(cvsEmStopActive, 0);
            }
            isMouseDown = false;
        }

        private void cvsEmStopActive_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Canvas.GetLeft(cvsEmStopActive) > -149)
            {
                Canvas.SetLeft(cvsEmStopActive, 0);
            }
            isMouseDown = false;
        }
    }
}
