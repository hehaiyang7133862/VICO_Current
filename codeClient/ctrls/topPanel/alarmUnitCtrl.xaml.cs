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

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// Interaction logic for alarmUnitCtrl.xaml
    /// </summary>
    public partial class alarmUnitCtrl : UserControl
    {
        public recUnit erObj;

        public alarmUnitCtrl()
        {
            InitializeComponent();
        }
        public alarmUnitCtrl(recUnit ErObj, int no)
        {
            InitializeComponent();

            if (ErObj != null)
            {
                if (ErObj.type == recType.alarmType)
                {
                    tbMenu.SelectedItem = menu_Alm;
                    valmoWin.sMainPanelCtrl.cvsMain.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0x8F, 0x8F));
                    lbSerialNum.Content = "A" + ErObj.serialNum.Substring(3, 3);
                }
                else
                {
                    lbSerialNum.Content = "M" + ErObj.serialNum.Substring(3, 3);
                    tbMenu.SelectedItem = menu_Msg;
                    valmoWin.sMainPanelCtrl.cvsMain.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xFF, 0x00));
                }
                //lbSerialNum.Content = ErObj.serialNum;
                lbDis.Content = valmoWin.dv.getCurDis(ErObj.serialNum);
                lbDtStart.Content = ErObj.dtStart.ToString("yyyy.MM.dd hh:mm:ss");
                lbNo.Content = no;

                erObj = ErObj;
            }
            
        }
        public void setValue(recUnit ErObj, int no)
        {
            if (ErObj != null)
            {
                lbDis.Content = valmoWin.dv.getCurDis(ErObj.serialNum);
                lbDtStart.Content = ErObj.dtStart.ToString("yyyy.MM.dd hh:mm:ss");
                lbNo.Content = no;
                if (ErObj.type == recType.alarmType)
                {
                    tbMenu.SelectedItem = menu_Alm;
                    valmoWin.sMainPanelCtrl.cvsMain.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0x8F, 0x8F));
                    lbSerialNum.Content = "A" + ErObj.serialNum.Substring(3, 3);
                }
                else
                {
                    lbSerialNum.Content = "M" + ErObj.serialNum.Substring(3, 3);
                    tbMenu.SelectedItem = menu_Msg;
                    valmoWin.sMainPanelCtrl.cvsMain.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xFF, 0x00));
                }
                erObj = ErObj;
            }
            
        }
        public void setValueByLan(recUnit ErObj, int no)
        {
            if (ErObj != null)
            {
                lbDis.Content = valmoWin.dv.getCurDis(ErObj.serialNum);
                lbDtStart.Content = ErObj.dtStart.ToString("yyyy.MM.dd hh:mm:ss");
                lbNo.Content = no;
                if (ErObj.type == recType.alarmType)
                {
                    tbMenu.SelectedItem = menu_Alm;
                    valmoWin.sMainPanelCtrl.cvsMain.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0x8F, 0x8F));
                    lbSerialNum.Content = "A" + ErObj.serialNum.Substring(3, 3);
                }
                else
                {
                    lbSerialNum.Content = "M" + ErObj.serialNum.Substring(3, 3);
                    tbMenu.SelectedItem = menu_Msg;
                    valmoWin.sMainPanelCtrl.cvsMain.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xFF, 0x00));
                }
                erObj = ErObj;
            }

        }

    }
}
