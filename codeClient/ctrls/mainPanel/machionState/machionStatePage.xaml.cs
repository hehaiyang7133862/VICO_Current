using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
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
using System.Windows.Threading;
using nsDataMgr;

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// Interaction logic for machionStatePage.xaml
    /// </summary>
    public partial class machionStatePage : UserControl
    {
        objUnit objOpAction1;
        objUnit objOpAction2;
        private bool _bIsMouseMove = false;
        private bool _bIsInfoVisiable = false;
        private bool _bIsServoVisiable = true;
        private bool _bIsRobotVisiable = true;

        private bool _bIsMouseDown = false;
        private Point _pMouseDown;
        private Point curMousePos;

        string[] machionState1 = new string[32]{
            "lanKey063",//"合模" bit0
            "lanKey064",//"开模" bit1
            "lanKey065",//"等待机械手信号" bit2
            "",//周期间隔 bit3
            "lanKey067",//"调模前进" bit4
            "lanKey068",//"调模后退" bit5
            "lanKey070",//"顶针进" bit6
            "lanKey069",//"顶针退" bit7
            "lanKey071",//"等待产品掉落" bit8
            "lanKey072",//"转盘正转" bit9
             "lanKey073",//"转盘反转" bit10
             "",//"吹气1" bit11
             "",//"吹气2" bit12
             "",//"吹气3" bit13
             "",//"吹气4" bit14
             "lanKey078",//   "座台进",//bit15
             "lanKey079",//   "座台退",//bit16
             "lanKey080",//   "注射",//bit17
             "lanKey081",//   "塑化",//bit18
             "lanKey082",//   "松退",//bit19
             "lanKey083",//  "冷却",//bit20
             "",//   "吹气7",//bit21
             "",//   "吹气8",//bit22
             "lanKey086",//  "自动调模",//bit23
             "lanKey087",//   "自动清料",//bit24
             "",//   "	",//bit25
             "lanKey089",//   "中子A退",//bit26
             "lanKey090",//   "中子A进",//bit27
             "lanKey091",//   "中子B退",//bit28
             "lanKey092",//   "中子B进",//bit29
             "lanKey093",//   "中子C退",//bit30
             "lanKey094",//   "中子C进"//bit31
        };
        string[] machionState2 = new string[32]{
            "lanKey095",//     "丝杆润滑",//bit0
            "lanKey096",//     "吹气9",//bit1
            "lanKey097",//     "吹气10",//bit2
            "lanKey098",//     "吹气11",//bit3
            "lanKey099",//     "吹气12",//bit4
            "lanKey100",//     "中子D退",//bit5
            "lanKey101",//     "中子D进",//bit6
            "lanKey102",//     "中子E退",//bit7
            "lanKey103",//     "中子E进",//bit8
            "lanKey104",//     "中子F退",//bit9
            "lanKey105",//     "中子F进",//bit10
            "lanKey106",//     "机绞润滑",//bit11
            "lanKey107",//     "吹气1",//bit12
            "lanKey108",//     "吹气2",//bit13
            "lanKey109",//     "吹气3",//bit14
            "lanKey110",//     "吹气4",//bit15
            "lanKey111",//    "吹气5",//bit16
            "lanKey112",//    "吹气6",//bit17
            "lanKey113",//    "吹气7",//bit18
            "lanKey114",//    "吹气8",//bit19
            "",//    "",//bit20
            "",//    "",//bit21
            "",//    "",//bit22
            "",//    "",//bit23
            "",//    "",//bit24
            "",//    "",//bit25
            "",//   "",//bit26
            "",//   "",//bit27
            "",//   "",//bit28
            "",//   "",//bit29
            "",//   "",//bit30
            "Sys108",//   "模具开启时间",//bit31
        };
        
        public machionStatePage()
        {
            InitializeComponent();

            string addr = Properties.Settings.Default.IPAddr;
            Lasal32.Online("TCP:" + addr, 1, 0, 0, 0);
            MachineName.Content = Lasal32.getString("SR_MachineName");

            valmoWin.dv.SysPr[66].addHandle(handleSysPr_66);
            valmoWin.dv.SysPr[67].addHandle(handleSysPr_67);
            valmoWin.dv.SysPr[105].addHandle(handleSysPr_105);
            //valmoWin.dv.MldPr[560].addHandle(handleMldPr_560);

            valmoWin.dv.SysPr[106].addHandle(updateMachineAction1);
            valmoWin.dv.SysPr[107].addHandle(updateMachineAction2);

            valmoWin.dv.SysPr[152].addHandle(updateCore1);
            valmoWin.dv.SysPr[153].addHandle(updateCore2);

            valmoWin.dv.InjPr[280].addHandle(InjUnitVisiableOrHidden);
            valmoWin.dv.MldPr[560].addHandle(TuneTableVisiableOrHidden);
            valmoWin.dv.SysPr[176].addHandle(SetDEE_USE);
            
            objOpAction1 = valmoWin.dv.SysPr[106];
            objOpAction1.addHandle(handleSysPr_106);
            objOpAction2 = valmoWin.dv.SysPr[107];
            objOpAction2.addHandle(handleSysPr_106);
            reorder();

            valmoWin.lstLanRefresh.Add(refushlan);

            valmoWin.dv.SysPr[260].addHandle(InjectionSlveVisiableOrHidden);
            valmoWin.dv.SysPr[259].addHandle(InjectionSlve2VisiableOrHidden);
            valmoWin.dv.SysPr[261].addHandle(MoldSlveVisiableOrHidden);
            valmoWin.dv.SysPr[262].addHandle(PlasticSlveVisiableOrHidden);
            valmoWin.dv.SysPr[251].addHandle(SetMachineType);
            valmoWin.dv.SysPr[252].addHandle(SetAutoFeed);
            valmoWin.dv.SysPr[253].addHandle(setScrewType);
            valmoWin.dv.SysPr[254].addHandle(setInterRob);
            valmoWin.dv.MldPr[157].addHandle(SetSensorClamp);
        }

        private void SetSensorClamp(objUnit obj)
        {
            tbClampUse.SelectedIndex = obj.value;
        }
        private void SetDEE_USE(objUnit obj)
        {
            tbEnergyUse.SelectedIndex = obj.value;
        }
        private void SetMachineType(objUnit obj)
        {
            MachineType.SelectedIndex = obj.value;
        }
        private void SetAutoFeed(objUnit obj)
        {
            tbAutoFeedingUse.SelectedIndex = obj.value;
        }
        private void setScrewType(objUnit obj)
        {
            ScrewType.SelectedIndex = obj.value;
        }
        private void setInterRob(objUnit obj)
        {
            tbRobotUse.SelectedIndex = obj.value;
        }

        private void refushlan()
        {
            handleSysPr_105(valmoWin.dv.SysPr[105]);
        }

        private bool _bIsMoldSlveMotorOn = true;
        private void MoldSlveVisiableOrHidden(objUnit obj)
        {
            _bIsMoldSlveMotorOn = (obj.value == 1) ? true : false;
            MoldSlve.Height = (_bIsMoldSlveMotorOn == true) ? 115 : 0;
            recalculateHeightOfcvsServo();
        }

        private bool _bIsInjectionSlveMotorOn = true;
        private void InjectionSlveVisiableOrHidden(objUnit obj)
        {
            _bIsInjectionSlveMotorOn = (obj.value == 1) ? true : false;
            InjectionSlve.Height = (_bIsInjectionSlveMotorOn == true) ? 115 : 0;
            recalculateHeightOfcvsServo();
        }

        private bool _bIsInjectionSlve2MotorOn = true;
        private void InjectionSlve2VisiableOrHidden(objUnit obj)
        {
            _bIsInjectionSlve2MotorOn = (obj.value == 1) ? true : false;
            InjectionSlve2.Height = (_bIsInjectionSlve2MotorOn == true) ? 115 : 0;
            recalculateHeightOfcvsServo();
        }

        private bool _bIsPlasticSlveMotorOn = true;
        private void PlasticSlveVisiableOrHidden(objUnit obj)
        {
            _bIsPlasticSlveMotorOn = (obj.value == 1) ? true : false;
            PlasticSlve.Height = (_bIsPlasticSlveMotorOn == true) ? 115 : 0;
            recalculateHeightOfcvsServo();
        }

        private bool _bIsInjUnitMotorOn = true;
        private void InjUnitVisiableOrHidden(objUnit obj)
        {
            _bIsInjUnitMotorOn = (obj.value == 1) ? true : false;
            UnitControlMode.SelectedIndex = obj.value;
            cvsInjUnit.Height = (_bIsInjUnitMotorOn == true) ? 115 : 0;
            recalculateHeightOfcvsServo();
        }

        private bool _bIsTuneTableMotorOn = true;
        private void TuneTableVisiableOrHidden(objUnit obj)
        {
            _bIsTuneTableMotorOn = (obj.value == 1) ? true : false;
            cvsTuneTable.Height = (_bIsTuneTableMotorOn == true) ? 115 : 0;
            recalculateHeightOfcvsServo();
        }

        private double cvsServoHeight = 0;
        public void recalculateHeightOfcvsServo()
        {
            cvsServoHeight = 510 +
                (_bIsInjUnitMotorOn == true ? 115 : 0) +
                (_bIsTuneTableMotorOn == true ? 115 : 0) +
                (_bIsInjectionSlveMotorOn == true ? 115 : 0) +
                (_bIsInjectionSlve2MotorOn == true ? 115 : 0) +
                (_bIsMoldSlveMotorOn == true ? 115 : 0) +
                (_bIsPlasticSlveMotorOn == true ? 115 : 0);

            cvsServo.Height = cvsServoHeight;

            reorder();
        }

        public void getFocus()
        {
            valmoWin.setPangetoNr(10);
        }

        private void handleSysPr_106(objUnit obj)
        {
            strMachionState1 = "";
            for (int i = 0; i < 32; i++)
            {
                int nr = (objOpAction1.value >> i) & 0x01;
                if (nr == 1)
                {
                    if (machionState1[i] != "")
                    {
                        if (strMachionState1 == "")
                            strMachionState1 += valmoWin.dv.getCurDis(machionState1[i]);
                        else
                            strMachionState1 += "+" + valmoWin.dv.getCurDis(machionState1[i]);
                    }
                }

            }
            strMachionState2 = "";
            for (int i = 0; i < 32; i++)
            {
                if (((objOpAction2.value >> i) & 0x01) == 1)
                {
                    if (machionState2[i] != "")
                    {
                        if (strMachionState2 == "")
                            strMachionState2 += valmoWin.dv.getCurDis(machionState2[i]);
                        else
                            strMachionState2 += "+" + valmoWin.dv.getCurDis(machionState2[i]);
                    }
                }
            }
            if (strMachionState1 == "")
                lbCurFocus.Content = strMachionState2;
            else
            {
                if (strMachionState2 == "")
                    lbCurFocus.Content = strMachionState1;
                else
                    lbCurFocus.Content = strMachionState1 + "+" + strMachionState2;
            }
        }

        /// <summary>
        /// 机械手是否使用
        /// </summary>
        /// <param name="obj">Sys066</param>
        private void handleSysPr_66(objUnit obj)
        {
            if (obj.value == 0)
            {
                tbRobotUse.SelectedIndex = 1;
            }
            else
            {
                tbRobotUse.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// 模具加热是否使用
        /// </summary>
        /// <param name="obj">Sys067</param>
        private void handleSysPr_67(objUnit obj)
        {
            if (obj.value == 1)
            {
                tbMoldHeatingUse.SelectedIndex = 1;
            }
            else
            {
                tbMoldHeatingUse.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// 机器模式
        /// </summary>
        /// <param name="obj">Sys105</param>
        private void handleSysPr_105(objUnit obj)
        {
            switch (obj.value)
            {
                case 0:
                    tbMode.SelectedIndex = obj.value;
                    lbSys105.Content = valmoWin.dv.getCurDis("lanKey058");
                    break;
                case 1:
                    tbMode.SelectedIndex = obj.value;
                    lbSys105.Content = valmoWin.dv.getCurDis("lanKey059");
                    break;
                case 2:
                    tbMode.SelectedIndex = obj.value;
                    lbSys105.Content = valmoWin.dv.getCurDis("lanKey060");
                    break;
                case 3:
                    tbMode.SelectedIndex = obj.value;
                    lbSys105.Content = valmoWin.dv.getCurDis("lanKey061");
                    break;
                default:
                    lbSys105.Content = "ERROR";
                    break;
            }
        }

        private string strMachionState1 = string.Empty;
        private List<BitmapImage> bmpMachionState1 = new List<BitmapImage>();
        private void updateMachineAction1(objUnit obj)
        {
            strMachionState1 = string.Empty;
            bmpMachionState1.Clear();

            int tmp = obj.value;

            for (int i = 0; i < 32; i++)
            {
                if (((tmp >> i) & 0x01) == 1)
                {
                    if (Enum.GetName(typeof(MachionState), i) != null)
                    {
                        if (strMachionState1.Length == 0)
                        {
                            strMachionState1 += valmoWin.dv.getCurDis("strIpr" + i.ToString().PadLeft(3, '0'));
                        }
                        else
                        {
                            strMachionState1 += "+" + valmoWin.dv.getCurDis("strIpr" + i.ToString().PadLeft(3, '0'));
                        }
                    }
                }
            }

            refushMachineAction();
        }

        private string strMachionState2 = string.Empty;
        private List<BitmapImage> bmpMachionState2 = new List<BitmapImage>();
        private void updateMachineAction2(objUnit obj)
        {
            strMachionState2 = string.Empty;
            bmpMachionState2.Clear();

            int tmp = obj.value;

            for (int i = 0; i < 32; i++)
            {
                if (((tmp >> i) & 0x01) == 1)
                {
                    if (Enum.GetName(typeof(MachionState2), i) != null)
                    {
                        if (strMachionState2.Length == 0)
                        {
                            strMachionState2 += valmoWin.dv.getCurDis("strIpr" + i.ToString().PadLeft(3, '0'));
                        }
                        else
                        {
                            strMachionState2 += "+" + valmoWin.dv.getCurDis("strMachionState" + (i + 32).ToString());
                        }

                    }
                }
            }

            refushMachineAction();
        }

        private void refushMachineAction()
        {
            if (strMachionState1.Length != 0)
            {
                lbCurFocus.Content = strMachionState1 + "+" + strMachionState2;
            }
            else
            {
                lbCurFocus.Content = strMachionState2;
            }
        }

        /// <summary>
        /// 控制模式
        /// </summary>
        /// <param name="obj">Mld560</param>
        private void handleMldPr_560(objUnit obj)
        {
            UnitControlMode.SelectedIndex = obj.value;
        }

        public void lanRefresh()
        {
            handleSysPr_66(valmoWin.dv.SysPr[66]);
            handleSysPr_105(valmoWin.dv.SysPr[105]);
        }

        private void updateCore1(objUnit obj)
        {
            int tmp = obj.value;

            imgA1.Opacity = ((tmp & 0x01) == 1) ? 1 : 0;
            imgB1.Opacity = (((tmp >> 1) & 0x01) == 1) ? 1 : 0;
            imgC1.Opacity = (((tmp >> 2) & 0x01) == 1) ? 1 : 0;
            imgD1.Opacity = (((tmp >> 3) & 0x01) == 1) ? 1 : 0;
            imgE1.Opacity = (((tmp >> 4) & 0x01) == 1) ? 1 : 0;
            imgF1.Opacity = (((tmp >> 5) & 0x01) == 1) ? 1 : 0;
        }

        private void updateCore2(objUnit obj)
        {
            int tmp = obj.value;

            imgA2.Opacity = ((tmp & 0x01) == 1) ? 1 : 0;
            imgB2.Opacity = (((tmp >> 1) & 0x01) == 1) ? 1 : 0;
            imgC2.Opacity = (((tmp >> 2) & 0x01) == 1) ? 1 : 0;
            imgD2.Opacity = (((tmp >> 3) & 0x01) == 1) ? 1 : 0;
            imgE2.Opacity = (((tmp >> 4) & 0x01) == 1) ? 1 : 0;
            imgF2.Opacity = (((tmp >> 5) & 0x01) == 1) ? 1 : 0;
        }

        private void Core1Select(object sender, MouseButtonEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.SysPr[152].accessLevel))
                return;
            int Index = Convert.ToInt32((sender as Image).Tag.ToString());
            int value = valmoWin.dv.SysPr[152].value;
            valmoWin.dv.setValue(valmoWin.dv.SysPr[152], value ^ (1 << Index));
        }

        private void Core2Select(object sender, MouseButtonEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.SysPr[153].accessLevel))
                return;
            int Index = Convert.ToInt32((sender as Image).Tag.ToString());
            int value = valmoWin.dv.SysPr[153].value;
            valmoWin.dv.setValue(valmoWin.dv.SysPr[153], value ^ (1 << Index));
        }

        private void cvsHeadInfo_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!_bIsMouseMove)
            {
                if (_bIsInfoVisiable == true)
                {
                    _bIsInfoVisiable = false;
                    imgZipedMachionInfo.Visibility = Visibility.Visible;
                    imgUnzipedMachionInfo.Visibility = Visibility.Hidden;
                }
                else
                {
                    _bIsInfoVisiable = true;
                    imgZipedMachionInfo.Visibility = Visibility.Hidden;
                    imgUnzipedMachionInfo.Visibility = Visibility.Visible;
                }
            }

            reorder();
        }
        private void cvsServo_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!_bIsMouseMove)
            {
                if (_bIsServoVisiable == true)
                {
                    _bIsServoVisiable = false;
                    imgZipedServo.Visibility = Visibility.Visible;
                    imgUnzipedServo.Visibility = Visibility.Hidden;
                }
                else
                {
                    _bIsServoVisiable = true;
                    imgZipedServo.Visibility = Visibility.Hidden;
                    imgUnzipedServo.Visibility = Visibility.Visible;
                }
            }

            reorder();
        }
        private void cvsHeadRobot_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!_bIsMouseMove)
            {
                if (_bIsRobotVisiable == true)
                {
                    _bIsRobotVisiable = false;
                    imgZipedRobot.Visibility = Visibility.Visible;
                    imgUnzipedRobot.Visibility = Visibility.Hidden;
                }
                else
                {
                    _bIsRobotVisiable = true;
                    imgZipedRobot.Visibility = Visibility.Hidden;
                    imgUnzipedRobot.Visibility = Visibility.Visible;
                }
            }

            reorder();
        }

        /// <summary>
        /// 重新布局
        /// </summary>
        private void reorder()
        {
            cvsInfo.Height = _bIsInfoVisiable ? 450 : 50;
            cvsServo.Height = _bIsServoVisiable ? cvsServoHeight : 50;
            cvsRobot.Height = _bIsRobotVisiable ? 430 : 50;

            sPanel.Height = cvsInfo.Height + cvsServo.Height + cvsRobot.Height;
        }

        private void cvsmain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _bIsMouseDown = true;
            _pMouseDown = e.GetPosition(cvsMain);
            curMousePos = _pMouseDown;
        }
        private void cvsmain_MouseMove(object sender, MouseEventArgs e)
        {
            if (_bIsMouseDown == true)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point tempMousePos = e.GetPosition(cvsMain);
                    if (_bIsMouseMove == false)
                    {
                        if ((Math.Abs(tempMousePos.Y - _pMouseDown.Y) > 5) || (Math.Abs(tempMousePos.X - _pMouseDown.X) > 5))
                        {
                            _bIsMouseMove = true;
                        }
                    }
                    double oldTop = Canvas.GetTop(sPanel);
                    double newTop = tempMousePos.Y - curMousePos.Y + oldTop;

                    if (newTop <= -(sPanel.Height - (valmoWin.MainPanelHeight - 195)) - 20)
                        newTop = -(sPanel.Height - (valmoWin.MainPanelHeight - 195)) - 20;
                    if (newTop > 0)
                        newTop = 0;
                    Canvas.SetTop(sPanel, newTop);
                    curMousePos = tempMousePos;
                }
            }
        }
        private void cvsmain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _bIsMouseMove = false;
            _bIsMouseDown = false;
        }
        private void cvsmain_MouseLeave(object sender, MouseEventArgs e)
        {
            _bIsMouseDown = false;
            _bIsMouseMove = false;
        }
    }

    /// <summary>
    /// 5K型驱动器错误
    /// </summary>
    public class DriveErr5K
    {
        private objUnit ErrorCount;
        private objUnit ErrorAddress;

        public DriveErr5K(string iErrCount, string ErrAddr)
        {
            if (iErrCount != "null")
            {
                object obj = valmoWin.dv.getObj(iErrCount);
                if (obj != null)
                {
                    ErrorCount = (objUnit)obj;
                }
                else
                {
                    App.log.Error("对象" + iErrCount + "不存在");
                }
            }

            if (ErrAddr != "null")
            {
                object obj2 = valmoWin.dv.getObj(ErrAddr);
                if (obj2 != null)
                {
                    ErrorAddress = (objUnit)obj2;
                }
                else
                {
                    App.log.Error("对象" + ErrAddr + "不存在");
                }
            }
        }

        public List<string> GetErrList()
        {
            List<string> lstError = new List<string>();

            if ((ErrorCount != null) && (ErrorAddress != null))
            {
                int[] Err = new int[ErrorCount.valueNew];
                Lasal32.GetData(Err, (uint)(ErrorAddress.valueNew), (int)Err.Length * 4);

                foreach (uint u in Err)
                {
                    lstError.Add("5__" + u.ToString());
                }
            }
            else
            {
                App.log.Error("获取错误信息失败" + ErrorCount.ToString() + ErrorAddress.ToString());
            }

            return lstError;
        }
    }

    /// <summary>
    /// 4K型驱动器错误
    /// </summary>
    public class DriveErr4K
    {
        private objUnit SystemError;
        private objUnit PsuError;
        private objUnit PsuError2;
        private objUnit MotorError;

        public DriveErr4K(string iSystem, string iPsu, string iPsu2, string iMotor)
        {
            if (iSystem != "null")
            {
                object obj = valmoWin.dv.getObj(iSystem);
                if (obj != null)
                {
                    SystemError = (objUnit)obj;
                }
                else
                {
                    App.log.Error("对象" + SystemError + "不存在");
                }
            }

            if (iPsu != "null")
            {
                object obj2 = valmoWin.dv.getObj(iPsu);
                if (obj2 != null)
                {
                    PsuError = (objUnit)obj2;
                }
                else
                {
                    App.log.Error("对象" + SystemError + "不存在");
                }
            }

            if (iPsu2 != "null")
            {
                object obj3 = valmoWin.dv.getObj(iPsu2);
                if (obj3 != null)
                {
                    PsuError2 = (objUnit)obj3;
                }
                else
                {
                    App.log.Error("对象" + SystemError + "不存在");
                }
            }

            if (iMotor != "null")
            {
                object obj4 = valmoWin.dv.getObj(iMotor);
                if (obj4 != null)
                {
                    MotorError = (objUnit)obj4;
                }
                else
                {
                    App.log.Error("对象" + SystemError + "不存在");
                }
            }
        }

        public List<string> GetErrList()
        {
            List<string> lstErr = new List<string>();

            if ((SystemError != null) && (PsuError != null) && (PsuError2 != null) && (MotorError != null))
            {
                for (int i = 0; i < 16; i++)
                {
                    if (((SystemError.valueNew >> i) & 0x01) == 1)
                    {
                        lstErr.Add("4__200__" + i.ToString().PadLeft(2, '0'));
                    }
                    if (((PsuError.valueNew >> i) & 0x01) == 1)
                    {
                        lstErr.Add("4__205__" + i.ToString().PadLeft(2, '0'));
                    }
                    if (((PsuError2.valueNew >> i) & 0x01) == 1)
                    {
                        lstErr.Add("4__206__" + i.ToString().PadLeft(2, '0'));
                    }
                    if (((MotorError.valueNew >> i) & 0x01) == 1)
                    {
                        lstErr.Add("4__207__" + i.ToString().PadLeft(2, '0'));
                    }
                }
            }
            else
            {
                App.log.Error("获取错误信息失败" + SystemError.ToString() + PsuError.ToString() + PsuError2.ToString() + MotorError.ToString());
            }

            return lstErr;
        }
    }

    public class DriveErr
    {
        public DriveErr4K D4;
        public DriveErr5K D5;
        public objUnit DriveType;

        public List<string> GetErrList()
        {
            if (DriveType == null)
            {
                return D5.GetErrList();
            }
            else
            {
                if (DriveType.valueNew == 4)
                {
                    return D4.GetErrList();
                }
                else
                {
                    return D5.GetErrList();
                }  
            }
        }

        public DriveErr(string iType, string iErrCount, string iErrAddr, string iSystem, string iPsu, string iPsu2, string iMotor)
        {
            if (iType != "null")
            {
                object obj = valmoWin.dv.getObj(iType);

                if (obj != null)
                {
                    DriveType = (objUnit)obj;
                }
            }

            D5 = new DriveErr5K(iErrCount, iErrAddr);

            D4 = new DriveErr4K(iSystem, iPsu, iPsu2, iMotor);
        }
    }
}
