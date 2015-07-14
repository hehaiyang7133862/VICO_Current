using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using nsVicoClient;

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// Interaction logic for ergUnitCtrl.xaml
    /// </summary>
    public partial class ergUnitCtrl : UserControl
    {
        public recUnit erObj;
        public int lstNr = 0;
        SolidColorBrush Bg2 = new SolidColorBrush(Color.FromArgb(255, 230, 230, 230));

        private Popup pop;
        private AlarmHelpCtrl ahCtrl;
        public ergUnitCtrl()
        {
            InitializeComponent();

            pop = new Popup();
            pop.AllowsTransparency = true;
            ahCtrl = new AlarmHelpCtrl();
            pop.Child = ahCtrl;
            pop.PlacementTarget = cvsMain;
            pop.Placement = PlacementMode.Relative;
            pop.VerticalOffset = 40;
            pop.HorizontalOffset = 0;
            pop.StaysOpen = false;
        }
        public void setValue(recUnit ErObj,int lstNo)
        {
            btnHelp.Visibility = Visibility.Hidden;
            lstNr = lstNo;
            lbSerialNum.Foreground = Brushes.Black;
            //if (lstNo % 2 == 1)
            //    cvsMain.Background = Bg2;
            //else
            //    cvsMain.Background = Brushes.Transparent;
            if (ErObj != null)
            {
                switch (ErObj.type)
                {
                    case recType.alarmType:
                        lbSerialNum.Content = "A" + ErObj.serialNum.Substring(3,3);
                        imgType.Source = (BitmapImage) App.Current.TryFindResource("imgA");
                        btnHelp.Visibility = Visibility.Visible;
                        break;
                    case recType.logType:
                        lbSerialNum.Content = ErObj.serialNum;
                        imgType.Source = (BitmapImage)App.Current.TryFindResource("imgL");
                        break;
                    case recType.operateType:
                        lbSerialNum.Content = "M" + ErObj.serialNum.Substring(3,3);
                        imgType.Source = (BitmapImage)App.Current.TryFindResource("imgM");
                        break;
                    case recType.sysType:
                        lbSerialNum.Content = "S" + ErObj.serialNum.Substring(3,3);
                        imgType.Source = (BitmapImage)App.Current.TryFindResource("imgS");
                        break;
                }
                lbUserName.Content = ErObj.userName;
                if (ErObj.serialNum != null)
                {
                    string objDis = valmoWin.dv.getCurDis(ErObj.serialNum,(int)ErObj.oldValue,(int)ErObj.newValue); 
                    if (objDis != "null" && objDis != null)
                    {
                        lbDis.Content = objDis;
                    }
                    else
                    {
                        lbDis.Content = "";
                    }
                }
                else
                    lbDis.Content = "";
                lbDtStart.Content = ErObj.dtStart.ToString("yyyy.MM.dd hh:mm:ss");
                if (ErObj.type == recType.operateType || ErObj.type == recType.logType)
                    lbDtEnd.Content = "";
                else
                {
                    if (ErObj.dtEnd.Year == 1)
                    {
                        for (int i = 0; i < lstWinMsgCtrl.curActiveAlmObjLst.Count; i++)
                        {
                            if (lstWinMsgCtrl.curActiveAlmObjLst[i].dtStart == ErObj.dtStart && ErObj.type == lstWinMsgCtrl.curActiveAlmObjLst[i].type)
                            {
                                lbSerialNum.Foreground = Brushes.Red;
                                break;
                            }
                        }
                        lbDtEnd.Content = "";
                    }
                    else
                    {
                        lbDtEnd.Content = ErObj.dtEnd.ToString("yyyy.MM.dd hh:mm:ss");
                    }
                }
                lbPalteNum.Content = ErObj.plateNums;
                if (ErObj.type == recType.operateType)
                {
                    lbOldValue.Content = ErObj.oldValue;
                    lbNewValue.Content = ErObj.newValue;
                }
                else
                {
                    lbOldValue.Content = "";
                    lbNewValue.Content = "";
                }
            }
            else
            {
                lbSerialNum.Content = "";
                lbUserName.Content = "";
                lbDis.Content = "";
                lbDtStart.Content = "";
                lbDtEnd.Content = "";
                lbPalteNum.Content = "";
                lbOldValue.Content = "";
                lbNewValue.Content = "";
                imgType.Source = null;
            }

            erObj = ErObj;
        }
        public void setFocus()
        {
            cvsMain.Background = Brushes.Red;
        }
        public void removeFocus()
        {
            if (lstNr % 2 == 1)
                cvsMain.Background = Bg2;
            else
                cvsMain.Background = Brushes.Transparent;
        }

        private void btnHelp_MouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            if (erObj.type == recType.alarmType)
            {
                string serNum = erObj.serialNum.Substring(3, 3);
                try
                {
                    Convert.ToInt32(serNum);
                }
                catch
                {
                    MessageBox.Show("Error!");

                    return;
                }

                ahCtrl.init("AA_C" + serNum, "AA_E" + serNum, "AA_R" + serNum);
                pop.IsOpen = true;
            }
        }

        private void btnHelp_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
    }
}
