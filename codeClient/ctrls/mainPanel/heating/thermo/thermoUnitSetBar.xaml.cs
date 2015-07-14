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
    /// Interaction logic for thermoUnitSetBar.xaml
    /// </summary>
    public partial class thermoUnitSetBar : UserControl
    {
        objUnit curObj;
        public thermoUnitSetBar()
        {
            InitializeComponent();
        }
        public string objName
        {
            set
            {
                curObj = valmoWin.dv.getObj(value);
                if (curObj != null)
                {
                    curObj.addHandle(handleRefresh);
                }
            }
        }
        private void handleRefresh(objUnit obj)
        {
            tempValueSet = 1.0 * obj.value / objUnit.rate[UnitType.Temp_C];
            double tmpTop = 169 - obj.value / 10.0 * 160.0 / 400;
            if (tmpTop <= 15)
                imgValueLnL.Height = 0;
            else
                imgValueLnL.Height = tmpTop - 15;
            lbLnValue.Content = tempValueSet.ToString("0.0") + objUnit.unit_C;
            Canvas.SetTop(cvsKeeping, tmpTop);
        }
        bool isImgKeepingMouseDown = false;
        Point mousePointKeeping;
        private void cvsKeeping_MouseDown(object sender, MouseButtonEventArgs e)
        {
            imgLn.Visibility = Visibility.Visible;
            lbLnValue.Visibility = Visibility.Visible;
            imgKeepingAct.Opacity = 1;

            isImgKeepingMouseDown = true;
            mousePointKeeping = e.GetPosition(cvsMain);
        }

        private void cvsKeeping_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (imgLn.Visibility == Visibility.Visible)
            {
                imgLn.Visibility = Visibility.Hidden;
                lbLnValue.Visibility = Visibility.Hidden;
                imgKeepingAct.Opacity = 0;
                if (isImgKeepingMouseDown)
                    isImgKeepingMouseDown = false;
                if (curObj != null)
                    curObj.vDblNew = tempValueSet;
            }
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            if (imgLn.Visibility == Visibility.Visible)
            {
                imgLn.Visibility = Visibility.Hidden;
                lbLnValue.Visibility = Visibility.Hidden;
                imgKeepingAct.Opacity = 0;
                if (isImgKeepingMouseDown)
                    isImgKeepingMouseDown = false;
                if (curObj != null)
                    curObj.vDblNew = tempValueSet;
            }
        }
        double tempValueSet = 0;
        private void cvsMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (isImgKeepingMouseDown)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point theMousePoint = e.GetPosition(this.cvsMain);
                    double tmpTop = Canvas.GetTop(cvsKeeping) + theMousePoint.Y - mousePointKeeping.Y;
                    if (tmpTop < 9)
                        tmpTop = 9;
                    else if (tmpTop > 169)
                        tmpTop = 169;
                    Canvas.SetTop(cvsKeeping, tmpTop);
                    if (tmpTop <= 15)
                        imgValueLnL.Height = 0;
                    else
                        imgValueLnL.Height = tmpTop - 15;
                    tempValueSet = (169 - tmpTop) * 400.0 / 160;
                    lbLnValue.Content = ((169 - tmpTop) * 400.0 / 160).ToString("0.0") + objUnit.unit_C;
                    mousePointKeeping = theMousePoint;
                }
            }

        }

    }
}
