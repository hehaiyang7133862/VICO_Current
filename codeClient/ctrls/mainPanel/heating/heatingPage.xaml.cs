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
    public delegate void showPanelEvent1(int lstNr, intEvent handle);  
    /// <summary>
    /// Interaction logic for heatingPage.xaml
    /// </summary>
    public partial class heatingPage : UserControl
    {
        objUnit objSelect = valmoWin.dv.TmpPr[165];
        thermoSettingPanel thermoSetPanel = new thermoSettingPanel();
        public static showPanelEvent1 showPanelHanle; 

        public heatingPage()
        {
            InitializeComponent();
            cvsMain.Children.Add(thermoSetPanel);
            showPanelHanle = new showPanelEvent1(showPanelFunc);
            valmoWin.dv.SysPr[67].addHandle(updateMoldHeatingUse);

        }

        public void setPage(int pageNum)
        {
            valmoWin.dv.SysPr[11].valueNew = pageNum * 10 + tbMain.SelectedIndex;
        }

        public void showPanelFunc(int lstNr, intEvent handle)
        {
            if ((lstNr - 1) % 16 < 8)
                thermoSetPanel.setPos(546, 183);
            else
                thermoSetPanel.setPos(88, 183);
            objSelect.valueNew = lstNr;
            thermoSetPanel.show(lstNr, handle);
        }

        /// <summary>
        /// 更新热流道是否使用
        /// </summary>
        /// <param name="obj">Sys067</param>
        private void updateMoldHeatingUse(objUnit obj)
        {
            if (obj.value == 1)
            {
                cvsSelect.Height = 85;
            }
            else
            {
                cvsSelect.Height = 0;
            }

            tblBtnCtrl.focusNr = 0;
            tbMain.SelectedIndex = 0;
        }

        private void HeadSwitch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int Index =
                Convert.ToInt32((sender as Canvas).Tag.ToString());
            switch (Index)
            {
                case 0:
                    {
                        tbl_Switch(0);
                        valmoWin.setPangetoNr(70);
                    }
                    break;
                case 1:
                    {
                        tbl_Switch(1);
                        valmoWin.setPangetoNr(71);
                    }
                    break;
                default:
                    break;
            }
        }

        private void tbl_Switch(int selectedIndex)
        {
            tblBtnCtrl.focusNr = selectedIndex;
            tbMain.SelectedIndex = selectedIndex;
        }
    }
}
