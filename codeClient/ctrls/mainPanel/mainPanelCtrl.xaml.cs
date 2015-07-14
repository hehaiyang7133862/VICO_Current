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
    /// Interaction logic for mainPanel.xaml
    /// </summary>
    public partial class mainPanelCtrl : UserControl
    {
        public delegate string getCurPageEvent();
        public delegate void lanSetEvent();
        public static getCurPageEvent getCurPageHandle;
        public static lanSetEvent lanSetHandle;
        public static int curPageIndex = 0;
        string[] pageName = new string[]
        {
            "Oview",
            "Mstate",
            "Mold",
            "Eje",
            "Ipr",
            "Inj",
            "Crg",
            "heat",
            "Das",
            "set",
            "robot"

        };
        private string getCurPage()
        {
            if(tbctlMain.SelectedIndex >= 0 && tbctlMain.SelectedIndex <= 10)
            {
                return pageName[tbctlMain.SelectedIndex];
            }
            else
            {
                return "Err";
            }
        }

        public mainPanelCtrl()
        {
            InitializeComponent();
            valmoWin.sMainPanelCtrl = this;
            valmoWin.SAttentionPanel = confirm;
            getCurPageHandle = getCurPage; 
        }

        /// <summary>
        /// 设置MainPanel高度
        /// </summary>
        /// <param name="height">高度</param>
        public void UpdateHeight(int height)
        {
            this.Height = height;
            cvsMain.Height = height;
            tbctlMain.Height = height;
        }

        public void changeToPage(int num)
        {
            cvsOverview.Opacity = 1;
            cvsMachionState.Opacity = 1;
            cvsMold.Opacity = 1;
            cvsEjector.Opacity = 1;
            cvsInterpreter.Opacity = 1;
            cvsInjection.Opacity = 1;
            cvsCharge.Opacity = 1;
            cvsHeating.Opacity = 1;
            cvsDataAnalysis.Opacity = 1;
            cvsSetting.Opacity = 1;
            cvsRobotPage.Opacity = 1;

            tbctlMain.SelectedIndex = num;
        }

        private void cvsFirstPage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            overViewPage1.setPage(0);
            changeToPage(0);
            cvsOverview.Opacity = 0;
        }
        private void cvsQuickSetPage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            valmoWin.setPangetoNr(10);
            changeToPage(1);
            cvsMachionState.Opacity = 0;
        }
        private void cvsMoldOnOffPage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            moldOnOffNewPage1.setPage(2);
            changeToPage(2);
            cvsMold.Opacity = 0;
        }
        private void cvsEjectorPage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ejectorNewPage1.setPage(3);
            changeToPage(3);
            cvsEjector.Opacity = 0;
        }
        private void cvsSettingInstructionPage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (valmoWin.dv.checkAccesslevel(3))
            {
                valmoWin.setPangetoNr(40);
                changeToPage(4);
                cvsInterpreter.Opacity = 0;
            }
        }
        private void cvsInjectionSettingsPage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            valmoWin.setPangetoNr(50);
            changeToPage(5);
            cvsInjection.Opacity = 0;
        }
        private void cvsCarriagePage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            carriageNewPage1.setPage(6);
            changeToPage(6);
            cvsCharge.Opacity = 0;
        }
        private void cvsBerrelHeatingPage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            heatingPage1.setPage(7);
            changeToPage(7);
            cvsHeating.Opacity = 0;
        }
        private void cvsDataPage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dataAnalysis1.setPage(8);
            changeToPage(8);
            cvsDataAnalysis.Opacity = 0;
        }
        private void cvsSettingPage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            settingPage1.setPage(9);
            changeToPage(9);
            cvsSetting.Opacity = 0;
        }
        private void cvsRobotPage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (valmoWin.dv.users.curUser.accessLevel >= 4)
            {
                tbctlMain.SelectedIndex = 10;
                changeToPage(10);
                cvsRobotPage.Opacity = 0;
            }
        }

        public void sendMsgToWinFunc(WinMsg msg)
        {
            if (!valmoWin.flagStartUpOk)
                return;
            switch (msg.type)
            {
                case WinMsgType.mwLogInOK:
                case WinMsgType.mwLogOut:
                case WinMsgType.mwMsg:
                    {
                        ////overViewPage1.eventRecord1.refreshListFunc();
                    }
                    break;
            }
        }

        public static Point PointMouseDown;
        public static bool bIsMouseMove = false;
        private bool _bIsMouseDown = false;
        private void cvsMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            _bIsMouseDown = true;
            PointMouseDown = e.GetPosition(cvsMain);
        }

        private void cvsMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (_bIsMouseDown == true)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point currentPoint = e.GetPosition(cvsMain);

                    if ((Math.Abs(currentPoint.X - PointMouseDown.X) > 3) || (Math.Abs(currentPoint.Y - PointMouseDown.Y) > 3))
                    {
                        bIsMouseMove = true;
                    }
                }
            }
        }

        private void cvsMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _bIsMouseDown = false;
            bIsMouseMove = false;
        }

        private void cvsMain_MouseLeave(object sender, MouseEventArgs e)
        {
            _bIsMouseDown = false;
            bIsMouseMove = false;
        }
    }
}
