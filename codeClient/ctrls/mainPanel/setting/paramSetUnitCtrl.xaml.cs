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
    /// Interaction logic for paramSetUnitCtrl.xaml
    /// </summary>
    public partial class paramSetUnitCtrl : UserControl
    {
        objUnit curObj;
        public static List<paramSetUnitCtrl> lstCtrls = new List<paramSetUnitCtrl>();
        SolidColorBrush focusColor;
        public paramSetUnitCtrl()
        {
            InitializeComponent();
            focusColor = (SolidColorBrush)App.Current.TryFindResource("focusColor");
            lstCtrls.Add(this);
            lbValue.Content = "";
            readOnly = true;
        }
        public static void sLanRefresh()
        {
            for (int i = 0; i < lstCtrls.Count; i++)
            {
                lstCtrls[i].lanRefresh();
            }
        }
        public string objName
        {
            set
            {
                curObj = valmoWin.dv.getObj(value);
                if (curObj != null)
                {
                    readOnly = false;
                    if (curObj.unitType == UnitType.DgtType)
                        lbUnit.Content = "";
                    else
                        lbUnit.Content = "[" + curObj.unit + "]";
                    curObj.addHandle(handleValue);
                    object strDis = App.Current.TryFindResource(curObj.serialNum);
                    if (strDis != null)
                        lbDis.Content = strDis.ToString();
                }
                else
                {
                    lbDis.Content = "";
                    lbUnit.Content = "";
                    lbValue.Content = "";
                }
            }
        }
        private void handleValue(objUnit obj)
        {
            lbValue.Content = obj.vDblStr;
            if (curObj.unitType == UnitType.DgtType)
                lbUnit.Content = "";
            else
                lbUnit.Content = "[" + curObj.unit + "]";
        }
        public void lanRefresh()
        {
            if (curObj != null)
            {
                object strDis = App.Current.TryFindResource(curObj.serialNum);
                if (strDis != null)
                    lbDis.Content = strDis.ToString();
            }
            //lbDis.Content = 
        }
        public bool readOnly
        {
            set
            {
                lbValue.Background = value ? new SolidColorBrush(Color.FromRgb(214, 214, 214)) : Brushes.Transparent;
            }
            get
            {
                return lbValue.Background != Brushes.Transparent;
            }
        }
        public Point pos
        {
            get;
            set;
        }
        public double disLen
        {
            set
            {
                lbDis.Width = value;
                Canvas.SetLeft(lbValue, value);
                Canvas.SetLeft(lbUnit, value + 100);
                cvsMain.Width = value + 160;
            }
        }
        public bool flagNoUnit
        {
            set
            {
                lbUnit.Visibility = value ? Visibility.Hidden : Visibility.Visible;
            }
        }

        bool isMousedown = false;
        private void lbValue_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isMousedown = true;
        }

        private void lbValue_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isMousedown)
            {
                isMousedown = false;
                if (!readOnly)
                {
                    if (!valmoWin.dv.checkAccesslevel(curObj.accessLevel))
                        return;
                    lbValue.BorderBrush = focusColor;
                    lbValue.BorderThickness = new Thickness(2);
                    Thickness margin = new Thickness(pos.X, pos.Y, 0, 0);
                    valmoWin.SNumKeyPanel.init(curObj, disposeFunc);
                }
            }
        }

        private void lbValue_MouseLeave(object sender, MouseEventArgs e)
        {
            isMousedown = false;
        }

        public void disposeFunc()
        {
            lbValue.BorderBrush = Brushes.Silver;
            lbValue.BorderThickness = new Thickness(1);
        }

    }
}
