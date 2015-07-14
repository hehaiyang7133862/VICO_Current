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
    /// Interaction logic for thermoSettingPanel.xaml
    /// </summary>
    public partial class thermoSettingPanel : UserControl
    {
        objUnit objSetUp = valmoWin.dv.TmpPr[166];
        objUnit objActrul = valmoWin.dv.TmpPr[167];
        objUnit objStatus = valmoWin.dv.TmpPr[168];
        objUnit objMaxLimit = valmoWin.dv.TmpPr[169];
        objUnit objMinLimit = valmoWin.dv.TmpPr[170];
        objUnit objHeatRate = valmoWin.dv.TmpPr[171];
        objUnit objSetStandby = valmoWin.dv.TmpPr[172];
        public intEvent callbackHandle;
        public thermoSettingPanel()
        {
            InitializeComponent();
            this.Visibility = Visibility.Hidden;
        }
        public void setPos(double left, double top)
        {
            Canvas.SetLeft(cvsPanel, left);
            Canvas.SetTop(cvsPanel, top);
        }
        public void setHeight(double height)
        {
            cvsMain.Height = height;
            lbPanelBack.Height = height;
        }
        public void setWidth(double width)
        {
            cvsMain.Width = width;
            lbPanelBack.Width = width;
        }
        public void show(int lstNr, intEvent handle)
        {
            this.Visibility = Visibility.Visible;
            lbLstNr.Content = lstNr.ToString();
            callbackHandle = handle;
        }
        bool isMouseDown = false;
        Point mousePoint;
        private void cvsPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //isMouseDown = true;
            //mousePoint = e.GetPosition(cvsMain);
            //vm.printLn(Canvas.GetLeft(cvsPanel) + "," + Canvas.GetTop(cvsPanel));
        }

        private void cvsMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = false;
        }

        private void cvsMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown == true)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point theMousePoint = e.GetPosition(this.cvsMain);
                    double tmpLeft = Canvas.GetLeft(cvsPanel) + theMousePoint.X - mousePoint.X;
                    double tmpTop = Canvas.GetTop(cvsPanel) + theMousePoint.Y - mousePoint.Y;
                    if (tmpLeft < -10)
                        tmpLeft = -10;
                    else if (tmpLeft > cvsMain.Width - cvsPanel.Width + 63)
                        tmpLeft = cvsMain.Width - cvsPanel.Width + 63;
                    if (tmpTop < 0)
                        tmpTop = 0;
                    else if (tmpTop > cvsMain.Height - cvsPanel.Height)
                        tmpTop = cvsMain.Height - cvsPanel.Height;
                    Canvas.SetLeft(cvsPanel, tmpLeft);
                    Canvas.SetTop(cvsPanel, tmpTop);
                    mousePoint = theMousePoint;
                }

            }
        }
        private void lbPanelBack_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            if (callbackHandle != null)
                callbackHandle(0);
        }


    }


}
