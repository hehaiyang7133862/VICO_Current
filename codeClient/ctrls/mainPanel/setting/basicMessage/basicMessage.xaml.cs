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
using System.Diagnostics;
using AForge.Video.DirectShow;
using nsDataMgr;

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// Interaction logic for basicMessage.xaml
    /// </summary>
    public partial class basicMessage : UserControl
    {
        public basicMessage()
        {
            valmoWin.dv.SysPr[13].addHandle(checkAccredit);
            try
            {
                InitializeComponent();

                string strType = Properties.Settings.Default.MachineType;
                lbMode.Content = strType;
                lbModeBg.Content = strType;
                //lbUIVersion.Content = valmoWin.uiVerNum;
                string machineNr = Properties.Settings.Default.MachineNr;
                lbMachineNr.Content = machineNr;

                lbIPAddr1.Content = Properties.Settings.Default.IPAddr;
                init();
                //lbType.Content = strType;
                valmoWin.lstUsbCheckFunc.Add(checkUseFunc);

                if (VideoSource.getInstance().bInitState == true)
                {
                    GetDevicePerformance();
                }
                else
                {
                    cbVideoFormat.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                vm.perror("[basicMessage] " + ex.Message);
            }
        }

        private void GetDevicePerformance()
        {
            if (VideoSource.getInstance().captureAForge != null)
            {
                //清除设备能力
                cbVideoFormat.Items.Clear();

                //原来选择的设备能力的新索引
                VideoCaptureDevice video = VideoSource.getInstance().captureAForge;
                for (int i = 0; i < video.VideoCapabilities.Length; i++)
                {
                    VideoCapabilities cap = video.VideoCapabilities[i];
                    DeviceCapabilityInfo capInfo = new DeviceCapabilityInfo(cap.FrameSize, cap.FrameRate);
                    cbVideoFormat.Items.Add(capInfo);
                }

                cbVideoFormat.SelectedIndex = 0;
            }
        }

        private void checkAccredit(objUnit obj)
        {
            DateTime datelimit;
            DateTime dateNow;

            valmoWin.dv.SysPr[227].setValue(2);
            uint addr = (uint)valmoWin.dv.SysPr[226].valueNew;

            if (addr != 0)
            {
                int[] date = new int[3];

                if (Lasal32.GetData(date, addr, 12) == true)
                {
                    try
                    {
                        datelimit = new DateTime(date[0], date[1], date[2]);
                    }
                    catch
                    {
                        date[0] = 2000;
                        date[1] = 1;
                        date[2] = 1;

                        datelimit = new DateTime(2000, 1, 1);

                        Lasal32.SetData(date, addr, 12);
                        valmoWin.dv.SysPr[227].setValue(3);

                        App.log.Info("读取使用截止日期出错，试用期截止日期重置为：2000_01_01");
                    }
                }
                else
                {
                    valmoWin.sRegistCtrl.show();
                    App.log.Error("GetData函数出错 \t 位置：BasicMessage \t 功能：读取授权时间");
                    return;
                }
            }
            else
            {
                return;
            }

            int systemDateField = obj.value;
            int year = Convert.ToInt32((systemDateField >> 16) & 0x0fff);
            int month = Convert.ToInt32((systemDateField >> 12) & 0x0000f);
            int day = Convert.ToInt32((systemDateField >> 4) & 0x00000ff);

            try
            {
                dateNow = new DateTime(year, month, day);
            }
            catch
            {
                SysBasicInfo.remainingTrialDays = 0;
                valmoWin.sRegistCtrl.show();
                App.log.Error("读取PLC时间出错");
                return;
            }

            TimeSpan tSpan = datelimit - dateNow;
            SysBasicInfo.remainingTrialDays = tSpan.Days;

            valmoWin.RefushAuthorization(SysBasicInfo.remainingTrialDays);

            if (tSpan.Days > 3000)
            {
                lbDate.Content = "Authorized";
            }
            else
            {
                lbDate.Content = tSpan.Days + "Days";

                if (tSpan.Days < 6)
                {
                    valmoWin.sRegistCtrl.show();
                }
            }
        }

        private void checkUseFunc()
        {
            //btnUpgrade.Visibility = valmoWin.sUsbPath != null ? Visibility.Visible : Visibility.Hidden;

        }

        public void init()
        {
            if (objUnit.lenUnitBasic == UnitType.Len_mm)
            {
                imgUnitMm.Visibility = Visibility.Visible;
                imgUnitInch.Visibility = Visibility.Hidden;
            }
            else
            {
                imgUnitMm.Visibility = Visibility.Hidden;
                imgUnitInch.Visibility = Visibility.Visible;
            }

            if (objUnit.tempUnitBasic == UnitType.Temp_C)
            {
                imgUnitF.Visibility = Visibility.Hidden;
                imgUnitC.Visibility = Visibility.Visible;
            }
            else
            {
                imgUnitF.Visibility = Visibility.Visible;
                imgUnitC.Visibility = Visibility.Hidden;
            }

            if (objUnit.prsUnitBasic == UnitType.Prs_Mpa)
            {
                imgUnitMpa.Visibility = Visibility.Visible;
                imgUnitBar.Visibility = Visibility.Hidden;
                imgUnitPsi.Visibility = Visibility.Hidden;
            }
            else if (objUnit.prsUnitBasic == UnitType.Prs_Bar)
            {
                imgUnitMpa.Visibility = Visibility.Hidden;
                imgUnitBar.Visibility = Visibility.Visible;
                imgUnitPsi.Visibility = Visibility.Hidden;
            }
            else if (objUnit.prsUnitBasic == UnitType.Prs_PSI)
            {
                imgUnitMpa.Visibility = Visibility.Hidden;
                imgUnitBar.Visibility = Visibility.Hidden;
                imgUnitPsi.Visibility = Visibility.Visible;
            }

            if (objUnit.forceUnitBasic == UnitType.Force_ton)
            {
                imgUnitTon.Visibility = Visibility.Visible;
                imgUnitKn.Visibility = Visibility.Hidden;
                imgUnitUsTon.Visibility = Visibility.Hidden;
            }
            else if (objUnit.forceUnitBasic == UnitType.Force_KN)
            {
                imgUnitTon.Visibility = Visibility.Hidden;
                imgUnitKn.Visibility = Visibility.Visible;
                imgUnitUsTon.Visibility = Visibility.Hidden;
            }
            else if (objUnit.forceUnitBasic == UnitType.Force_USton)
            {
                imgUnitTon.Visibility = Visibility.Hidden;
                imgUnitKn.Visibility = Visibility.Hidden;
                imgUnitUsTon.Visibility = Visibility.Visible;
            }
            valmoWin.dv.ItlPr[29].addHandle(ipChangeFunc);
        }

        private void ipChangeFunc(objUnit obj)
        {
            lbIPAddr1.Content = Properties.Settings.Default.IPAddr;
        }

        private void lbUnitMm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            imgUnitMm.Visibility = Visibility.Visible;
            imgUnitInch.Visibility = Visibility.Hidden;
            valmoWin.execHandle(opeOrderType.unitChange, new WinMsg(WinMsgType.mwUnitChange, "mm"));
        }

        private void lbUnitInch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            imgUnitMm.Visibility = Visibility.Hidden;
            imgUnitInch.Visibility = Visibility.Visible;
            valmoWin.execHandle(opeOrderType.unitChange, new WinMsg(WinMsgType.mwUnitChange, "inch"));
        }

        private void lbUnitC_MouseDown(object sender, MouseButtonEventArgs e)
        {
            imgUnitF.Visibility = Visibility.Hidden;
            imgUnitC.Visibility = Visibility.Visible;
            valmoWin.execHandle(opeOrderType.unitChange, new WinMsg(WinMsgType.mwUnitChange, objUnit.unit_C));
            
        }

        private void lbUnitF_MouseDown(object sender, MouseButtonEventArgs e)
        {
            imgUnitF.Visibility = Visibility.Visible;
            imgUnitC.Visibility = Visibility.Hidden;
            valmoWin.execHandle(opeOrderType.unitChange, new WinMsg(WinMsgType.mwUnitChange, objUnit.unit_F));
        }

        private void lbUnitMpa_MouseDown(object sender, MouseButtonEventArgs e)
        {
            imgUnitMpa.Visibility = Visibility.Visible;
            imgUnitBar.Visibility = Visibility.Hidden;
            imgUnitPsi.Visibility = Visibility.Hidden;
            valmoWin.execHandle(opeOrderType.unitChange, new WinMsg(WinMsgType.mwUnitChange, objUnit.unit_Mpa));
        }

        private void lbUnitBar_MouseDown(object sender, MouseButtonEventArgs e)
        {

            imgUnitMpa.Visibility = Visibility.Hidden;
            imgUnitBar.Visibility = Visibility.Visible;
            imgUnitPsi.Visibility = Visibility.Hidden;
            valmoWin.execHandle(opeOrderType.unitChange, new WinMsg(WinMsgType.mwUnitChange, "Bar"));
        }

        private void lbUnitPsi_MouseDown(object sender, MouseButtonEventArgs e)
        {
            imgUnitMpa.Visibility = Visibility.Hidden;
            imgUnitBar.Visibility = Visibility.Hidden;
            imgUnitPsi.Visibility = Visibility.Visible;
            valmoWin.execHandle(opeOrderType.unitChange, new WinMsg(WinMsgType.mwUnitChange, "PSI"));
        }

        private void lbUnitTon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            imgUnitTon.Visibility = Visibility.Visible;
            imgUnitKn.Visibility = Visibility.Hidden;
            imgUnitUsTon.Visibility = Visibility.Hidden;
            valmoWin.execHandle(opeOrderType.unitChange, new WinMsg(WinMsgType.mwUnitChange, "ton"));
        }

        private void lbUnitKn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            imgUnitTon.Visibility = Visibility.Hidden;
            imgUnitKn.Visibility = Visibility.Visible;
            imgUnitUsTon.Visibility = Visibility.Hidden;
            valmoWin.execHandle(opeOrderType.unitChange, new WinMsg(WinMsgType.mwUnitChange, "KN"));
        }

        private void lbUnitUsTon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            imgUnitTon.Visibility = Visibility.Hidden;
            imgUnitKn.Visibility = Visibility.Hidden;
            imgUnitUsTon.Visibility = Visibility.Visible;
            valmoWin.execHandle(opeOrderType.unitChange, new WinMsg(WinMsgType.mwUnitChange, "US-ton"));
        }

        public void disposeFunc()
        {
            lbIPAddr1.Background = Brushes.Transparent;
            lbMode.Background = Brushes.Transparent;
            lbMachineNr.Background = Brushes.Transparent;
        }

        private void lbIPAddr1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lbIPAddr1.Background = Brushes.Green;
            valmoWin.dv.ItlPr[29].note = Properties.Settings.Default.IPAddr;
            valmoWin.SIpSetPanel.show(valmoWin.dv.ItlPr[29], disposeFunc);
        }

        public void lanRefresh()
        {
            //lbType.Content = "RE1550";
        }

        private void lbMode_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (valmoWin.dv.checkAccesslevel(4))
            {
                lbMode.Background = Brushes.Green;
                valmoWin.SCharKeyPanel.init(false, lbMode.Content.ToString(), enterFunc, disposeFunc, new Thickness(108, 600, 0, 0));
            }
        }
        private void enterFunc(string value)
        {
            lbMode.Content = value;
            lbModeBg.Content = value;
            //lbType.Content = value;
            Properties.Settings.Default.MachineType = value;
        }

        private void setMachineNr(string str)
        {
            lbMachineNr.Content = str;
            Properties.Settings.Default.MachineNr = str;
        }

        private void lbMachineNr_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (valmoWin.dv.checkAccesslevel(3))
            {
                lbMachineNr.Background = Brushes.Green;
                valmoWin.SCharKeyPanel.init(false, lbMachineNr.Content.ToString(), setMachineNr, disposeFunc, new Thickness(108, 600, 0, 0));
            }

        }

        private void btnUpgrade_MouseUp(object sender, MouseButtonEventArgs e)
        {
            valmoWin.SUpgradePanel.show();
        }

        private void btnAccredit_MouseUp(object sender, MouseButtonEventArgs e)
        {
            valmoWin.sRegistCtrl.show();
        }

        private void cbVideoFormat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ( cbVideoFormat.SelectedItem != null)
            {
                VideoSource.getInstance().DesiredFrameSize = ((DeviceCapabilityInfo)cbVideoFormat.SelectedItem).FrameSize;
            }
        }

    }

    public class SysBasicInfo
    {
        public static int remainingTrialDays;
    }
}
