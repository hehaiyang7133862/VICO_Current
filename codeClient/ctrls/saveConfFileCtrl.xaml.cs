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
using System.Windows.Threading;
using nsDataMgr;
using System.IO;

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// saveConfFileCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class saveConfFileCtrl : UserControl
    {
        confirmCtrl confirmPanel = new confirmCtrl();
        DispatcherTimer dtLoad = new DispatcherTimer();
        DispatcherTimer dtShow = new DispatcherTimer();
        //List<string> valueLst = new List<string>();
        //List<string> addrLst = new List<string>();
        List<string> itemLst = new List<string>();
        private List<string> lstBasic = new List<string>();
        List<loadFileItemCtrl> itemCtrlLst = new List<loadFileItemCtrl>();
        saveFileCtrl savePanel = new saveFileCtrl();
        int pageNr = 1;
        string fileType = "";
        public saveConfFileCtrl()
        {
            InitializeComponent();

            InitLstBasic();
            cvsMain.Children.Add(confirmPanel);
            Label lbNum = new Label();
            lbNum.Foreground = Brushes.White;
            cvsValueLst.Children.Add(lbNum);
            //for (int i = 0; i < 88; i++)
            //{
            //    loadFileItemCtrl valueItem = new loadFileItemCtrl();
            //    cvsValueLst.Children.Add(valueItem);
            //    Canvas.SetTop(valueItem, (cvsValueLst.Children.Count - 1) * 20);
            //}
            dtLoad.Interval = new TimeSpan(0, 0, 0, 0, 10);
            dtLoad.Tick += new EventHandler(dtLoad_Tick);

            dtShow.Interval = new TimeSpan(0, 0, 0, 0, 10);
            dtShow.Tick += new EventHandler(dtShow_Tick);
            cvsMain.Children.Add(savePanel);
            this.Visibility = Visibility.Hidden;
        }

        private void InitLstBasic()
        {
            lstBasic.Add("FileEvaluation1.PlastificationPoleNumber");
            lstBasic.Add("FileEvaluation1.sInjectionBallScrewPitch");
            lstBasic.Add("FileEvaluation1.sInjectionBallScrewPullyTeeth");
            lstBasic.Add("FileEvaluation1.sInjectionMotorOverspeedRatio");
            lstBasic.Add("FileEvaluation1.sInjectionMotorPullyTeeth");
            lstBasic.Add("FileEvaluation1.sInjectionMotorRatedSpeed");
            lstBasic.Add("FileEvaluation1.sLoadCellRange");
            lstBasic.Add("FileEvaluation1.sMaxHoldingForce");
            lstBasic.Add("FileEvaluation1.sMaxInjectionForce");
            lstBasic.Add("FileEvaluation1.sMaxInjectionStroke");
            lstBasic.Add("FileEvaluation1.sPlastificationBallScrewPullyTeeth");
            lstBasic.Add("FileEvaluation1.sPlastificationMotorPullyTeeth");
            lstBasic.Add("FileEvaluation1.sPlastificationMotorRatedSpeed");
            lstBasic.Add("FileEvaluation1.sPlastificationOverspeedRatio");
            lstBasic.Add("FileEvaluation1.sScrewDiameter");
            lstBasic.Add("FileEvaluation1.sLoadModeCellRange");
            lstBasic.Add("FileEvaluation1.sInjSsorTempLimit");
            lstBasic.Add("FileEvaluation1.sCPumpTempLimit");
            lstBasic.Add("FileEvaluation1.EjectorPoleNumber");
            lstBasic.Add("FileEvaluation1.MoldPoleNumber");
            lstBasic.Add("FileEvaluation1.sEjectBallScrewPitch");
            lstBasic.Add("FileEvaluation1.sEjectBallScrewPullyTeeth");
            lstBasic.Add("FileEvaluation1.sEjectMotorOverspeedRatio");
            lstBasic.Add("FileEvaluation1.sEjectMotorPullyTeeth");
            lstBasic.Add("FileEvaluation1.sEjectMotorRatedSpeed");
            lstBasic.Add("FileEvaluation1.sMoldBallScrewPitch");
            lstBasic.Add("FileEvaluation1.sMoldBallScrewPullyTeeth");
            lstBasic.Add("FileEvaluation1.sMoldMotorOverSpeedRatio");
            lstBasic.Add("FileEvaluation1.sMoldMotorPullyTeeth");
            lstBasic.Add("FileEvaluation1.sMoldMotorRatedSpeed");
            lstBasic.Add("FileEvaluation1.sMaxEjectStroke");
            lstBasic.Add("FileEvaluation1.sMaxMoldStroke");
            lstBasic.Add("FileEvaluation1.sMaxClampForce");
            lstBasic.Add("FileEvaluation1.sInjectionUnitRotedSpeed");
            lstBasic.Add("FileEvaluation1.sInjectionUnitMotorPullyTeeth");
            lstBasic.Add("FileEvaluation1.sInjectionUnitBallScrewTeeth");
            lstBasic.Add("FileEvaluation1.sInjectionUnitBallScrewPitch");
            lstBasic.Add("FileEvaluation1.sInjectionUnitMaxStroke");
            lstBasic.Add("FileEvaluation1.sInjectionUnitForceKN");
            lstBasic.Add("FileEvaluation1.PI_KpBp");
            lstBasic.Add("FileEvaluation1.PI_KpHolding");
            lstBasic.Add("FileEvaluation1.PI_KpInjection");
            lstBasic.Add("FileEvaluation1.PI_TnBp");
            lstBasic.Add("FileEvaluation1.PI_TnHolding");
            lstBasic.Add("FileEvaluation1.PI_TnInjection");
            lstBasic.Add("FileEvaluation1.PI_KpInjection2");
            lstBasic.Add("FileEvaluation1.PI_TnInjection2");
            lstBasic.Add("FileEvaluation1.OL2LimitFrequencyEjector");
            lstBasic.Add("FileEvaluation1.OL2LimitFrequencyInjection");
            lstBasic.Add("FileEvaluation1.OL2LimitFrequencyMold");
            lstBasic.Add("FileEvaluation1.OL2LimitFrequencyPlastification");
            lstBasic.Add("FileEvaSim1.sTtStroke");
            lstBasic.Add("FileEvaSim1.sTtMotGearbox");
            lstBasic.Add("FileEvaSim1.sTtMotPullyTeeth");
            lstBasic.Add("FileEvaSim1.sTtScwPullyTeeth");
            lstBasic.Add("FileEvaSim1.sTtScwPitch");
            lstBasic.Add("FileEvaSim1.sTtMotRatedSpd");
            lstBasic.Add("FileEvaSim1.Resolution");
            lstBasic.Add("CalcPressure1.sScale");
            lstBasic.Add("CalcPressure2.sScale");
            lstBasic.Add("CalcPressure3.sScale");
            lstBasic.Add("CalcPressure1.Offset");
            lstBasic.Add("CalcPressure2.Offset");
            lstBasic.Add("CalcPressure3.Offset");
            lstBasic.Add("MoldReferenceOffset.Data");
            lstBasic.Add("PlastificationReferenceOffset.Data");
            lstBasic.Add("EjectorReferenceOffset.Data");
            lstBasic.Add("InjUnitReferenceOffset.Data");
            lstBasic.Add("CsCurveTime.Data");
            lstBasic.Add("MoldAxisData.ForceReleaseAcc");
            lstBasic.Add("MoldAxisData.ForcereleaseForce");
            lstBasic.Add("MoldAxisData.ForceReleaseSpeed");
            lstBasic.Add("MoldAxisData.Ol2Force");
            lstBasic.Add("MoldAxisData.vKv");
            lstBasic.Add("MoldAxisData.vVu");
            lstBasic.Add("MoldAxisData.Ext_Units");
            lstBasic.Add("MoldAxisData.Int_Units");
            lstBasic.Add("MoldAxisData.V_Max");
            lstBasic.Add("MoldAxisData.A_Max");
            lstBasic.Add("MoldAxisData.OL2Speed");
            lstBasic.Add("MoldAxisData.Ol2Force");
            lstBasic.Add("ForceGuardMold.sRefMaxCurrent");
            lstBasic.Add("ForceGuardMold.sRefMaxTorque");
            lstBasic.Add("MoldAxisData.IncPerRot");
            lstBasic.Add("InjectionAxisData.ForceReleaseAcc");
            lstBasic.Add("InjectionAxisData.ForcereleaseForce");
            lstBasic.Add("InjectionAxisData.ForceReleaseSpeed");
            lstBasic.Add("InjectionAxisData.vKv");
            lstBasic.Add("InjectionAxisData.vVu");
            lstBasic.Add("InjectionAxisData.Ext_Units");
            lstBasic.Add("InjectionAxisData.Int_Units");
            lstBasic.Add("InjectionAxisData.V_Max");
            lstBasic.Add("InjectionAxisData.A_Max");
            lstBasic.Add("InjectionAxisData.OL2Speed");
            lstBasic.Add("InjectionAxisData.Ol2Force");
            lstBasic.Add("InjectionAxisData.IncPerRot");
            lstBasic.Add("ForceGuardInjection.sRefMaxCurrent");
            lstBasic.Add("ForceGuardInjection.sRefMaxTorque");
            lstBasic.Add("PlastificationAxisData.Ext_Units");
            lstBasic.Add("PlastificationAxisData.Int_Units");
            lstBasic.Add("PlastificationAxisData.V_Max");
            lstBasic.Add("PlastificationAxisData.A_Max");
            lstBasic.Add("PlastificationAxisData.OL2Speed");
            lstBasic.Add("PlastificationAxisData.Ol2Force");
            lstBasic.Add("PlastificationAxisData.vKv");
            lstBasic.Add("PlastificationAxisData.vVu");
            lstBasic.Add("PlastificationAxisData.IncPerRot");
            lstBasic.Add("ForceGuardScrew.sRefMaxCurrent");
            lstBasic.Add("ForceGuardScrew.sRefMaxTorque");
            lstBasic.Add("EjectorAxisData.ForceReleaseAcc");
            lstBasic.Add("EjectorAxisData.ForcereleaseForce");
            lstBasic.Add("EjectorAxisData.ForceReleaseSpeed");
            lstBasic.Add("EjectorAxisData.vKv");
            lstBasic.Add("EjectorAxisData.vVu");
            lstBasic.Add("EjectorAxisData.Ext_Units");
            lstBasic.Add("EjectorAxisData.Int_Units");
            lstBasic.Add("EjectorAxisData.V_Max");
            lstBasic.Add("EjectorAxisData.A_Max");
            lstBasic.Add("EjectorAxisData.OL2Speed");
            lstBasic.Add("EjectorAxisData.Ol2Force");
            lstBasic.Add("EjectorAxisData.IncPerRot");
            lstBasic.Add("ForceGuardEjector.sRefMaxCurrent");
            lstBasic.Add("ForceGuardEjector.sRefMaxTorque");
            lstBasic.Add("InjectionUnitAxisData.ForceReleaseSpeed");
            lstBasic.Add("InjectionUnitAxisData.ForceReleaseAcc");
            lstBasic.Add("InjectionUnitAxisData.ForcereleaseForce");
            lstBasic.Add("InjectionUnitAxisData.IncPerRot");
            lstBasic.Add("InjectionUnitAxisData.vKv");
            lstBasic.Add("InjectionUnitAxisData.vVu");
            lstBasic.Add("ForceGuardInjUnit.sRefMaxTorque");
            lstBasic.Add("ForceGuardInjUnit.sRefMaxCurrent");
            lstBasic.Add("InjectionUnitAxisData.Ext_Units");
            lstBasic.Add("InjectionUnitAxisData.Int_Units");
            lstBasic.Add("InjectionUnitAxisData.V_Max");
            lstBasic.Add("InjectionUnitAxisData.A_Max");
            lstBasic.Add("InjectionUnitAxisData.OL2Speed");
            lstBasic.Add("InjectionUnitAxisData.Ol2Force");
            lstBasic.Add("MainParameterCOMPL1\\MainParameter1.sDuoPlasMotor");
            lstBasic.Add("MSTCpara_Plas.ClassSvr");
            lstBasic.Add("Plastic_Axis.sInitEnc");
            lstBasic.Add("Plastic_Axis.sEncType");
            lstBasic.Add("PlasticSlve_Axis.sInitEnc");
            lstBasic.Add("PlasticSlve_Axis.sEncType");
            lstBasic.Add("MainParameterCOMPL1\\MainParameter1.sDuoInjMotor");
            lstBasic.Add("MSTCpara_INJ.ClassSvr");
            lstBasic.Add("MainParameterCOMPL1\\MainParameter1.sInjPosSync");
            lstBasic.Add("Injection_Axis.sInitEnc");
            lstBasic.Add("Injection_Axis.sEncType");
            lstBasic.Add("InjectionSlv_Axis.sInitEnc");
            lstBasic.Add("InjectionSlv_Axis.sEncType");
            lstBasic.Add("InjectionReferenceOffset.Data");
            lstBasic.Add("MainParameterCOMPL1\\MainParameter1.sDuoMoldMotor");
            lstBasic.Add("MSTCpara_Mold.ClassSvr");
            lstBasic.Add("MainParameterCOMPL1\\MainParameter1.sMoldPosSync");
            lstBasic.Add("Mold_Axis.sInitEnc");
            lstBasic.Add("Mold_Axis.sEncType");
            lstBasic.Add("MoldSlve_Axis.sInitEnc");
            lstBasic.Add("MoldSlve_Axis.sEncType");
            lstBasic.Add("DEE_Parameters1.DEE_USE");
            lstBasic.Add("DEE_Parameters1.Main_CurntCnver");
            lstBasic.Add("DEE_Parameters1.MachHeat_CurntCnver");
            lstBasic.Add("DEE_Parameters1.Driver_CurntCnver");
            lstBasic.Add("DEE_Parameters1.MoldHeat_CurntCnver");
            lstBasic.Add("DEE_Parameters1.sUeffThreshold");
            lstBasic.Add("Euromap12.sROBOTOFF");
            lstBasic.Add("Euromap12.sRobotMode");
            lstBasic.Add("Euromap12.sCycleEnableFunction");
            lstBasic.Add("TopGuardCheck1\\TopGuardUSE.Data");
            lstBasic.Add("InjGuardSelect.sSelect");
            lstBasic.Add("EjectorReferenceState.Data");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sSaftyTime");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sDelayTime");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sOutTime");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sStepBackward");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sS1Backward");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sS2Backward");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sSEBackward");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sV1Backward");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sV2Backward");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sVEBackward");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sStepForward");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sS1Forward");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sS2Forward");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sSEForward");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sV1Forward");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sV2Forward");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sVEForward");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sEjectorKeepOut");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sCheckHomeSensor");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sEjectorMode");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sCount");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sEjectorShakePosition");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sP1Forward");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sP2Forward");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sPEForward");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sP1Backward");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sP2Backward");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sPEBackward");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sAcceleration");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sDeceleration");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sEjectorAdjustForce");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sEjectorAdjustSpeed");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sP1Backward");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sP1Forward");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sP2Backward");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sP2Forward");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sPEBackward");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sPEForward");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sEjectorWhenMoldOpen");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sEjectorOpenPosition");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sEjectorA5FWDSpeed");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sEjectorA5FWDForce");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sEjectorA5BWDSpeed");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sEjectorA5BWDForce");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sEjectorA5FWDPosition");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sEjectorA5HoldTime");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sEjectorA5Delay");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sDuoEjector");
            lstBasic.Add("MainEjectorPara1\\EjectorParameter1.sEjectorWhenMoldClose");
            lstBasic.Add("Injection_Ref_Ok.Data");
            lstBasic.Add("SetPressureTime.Data");
            lstBasic.Add("Ram_SafetyPRtUse.Data");
            lstBasic.Add("MainInjectionControl1.sPeakPTime");
            lstBasic.Add("NozzleTouchChecker1\\Ram_sensor.Data");
            lstBasic.Add("MainInjectionParameter1\\TwoRankParameters1.sInjectionHoldingAcc1");
            lstBasic.Add("MainInjectionParameter1\\TwoRankParameters1.sInjectionHoldingAcc2");
            lstBasic.Add("MainInjectionParameter1\\TwoRankParameters1.sInjectionHoldingDec1");
            lstBasic.Add("MainInjectionParameter1\\TwoRankParameters1.sInjectionHoldingDec2");
            lstBasic.Add("MainInjectionParameter1\\TwoRankParameters1.sSwitch");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sMaxVelocity");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sMaxPressure");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sMaxPressureHolding");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sMaxStroke");
            lstBasic.Add("MainInjectionParameter1\\Injection1.ControllerType");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sHolding_Speedlimit");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sHPM_Dec");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sInj_ACC");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sInj_DEC");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sSetOffset");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sSetSafetyTime");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sSpeedLimitInjection");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sNozzleSelection");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sCompInjScrewCriterion");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sCompInjScrewPos");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sNozzleSwitchTime");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sNozzleChkBit");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sNozzleSelection");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sButtonState");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sSetOffset");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sCompInjScrewPosA4");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sMinCustionCtrl");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sMinCustionUp");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sMinCustionLow");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sCusionUp");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sCusionLow");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sCusionErrorOp");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sUsInjTimeProt");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sInjMinT");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sInjMaxT");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sHoldInManual");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sAdjSpeed");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sScrewTempLimit");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sScrewTempProtectTime");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sHolding_Stepnumber");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sHolding_P1");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sHolding_P2");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sHolding_P3");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sHolding_P4");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sHolding_T1");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sHolding_T2");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sHolding_T3");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sHolding_T4");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sInjPSpeedInjection");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sInj_Stepnumber");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sInj_S1");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sInj_S2");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sInj_S3");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sInj_S4");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sInj_S5");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sInj_S6");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sInj_S7");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sInj_S8");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sInj_S9");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sInj_V1");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sInj_V2");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sInj_V3");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sInj_V4");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sInj_V5");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sInj_V6");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sInj_V7");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sInj_V8");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sInj_V9");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sInj_VE");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sInj_P1");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sInj_P2");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sInj_P3");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sInj_P4");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sInj_P5");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sInj_P6");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sInj_P7");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sInj_P8");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sInj_P9");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sInj_PE");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sVP_Mode");
            lstBasic.Add("MainInjectionParameter1\\Injection1.Se_VP_Time");
            lstBasic.Add("MainInjectionParameter1\\Injection1.Sel_VP_Pos");
            lstBasic.Add("MainInjectionParameter1\\Injection1.Sel_VP_Spd");
            lstBasic.Add("MainInjectionParameter1\\Injection1.Sel_VP_Pres");
            lstBasic.Add("MainInjectionParameter1\\Injection1.Sel_VP_DigIn");
            lstBasic.Add("MainInjectionParameter1\\Injection1.Sel_VP_AnaIn");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sVP_Position");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sVP_Pressure");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sVP_Velocity");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sVP_Time");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sInjectionTime");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sCycleIntervalTime");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sCoolingTime");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sInjectionDelayTime");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sAPInjS1");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sAPInjV1");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sAPInjV2");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sAPInjP1");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sAPInjP2");
            lstBasic.Add("MainInjectionParameter1\\Injection1.sAPctr");
            lstBasic.Add("MainInjectionParameter1\\Plastification1.sMaxPressure");
            lstBasic.Add("MainInjectionParameter1\\Plastification1.sMaxVelocity");
            lstBasic.Add("MainInjectionParameter1\\Plastification1.sMaxStroke");
            lstBasic.Add("MainInjectionParameter1\\Plastification1.sBP_Injection_Acc");
            lstBasic.Add("MainInjectionParameter1\\Plastification1.sBP_Injection_Dec");
            lstBasic.Add("MainInjectionParameter1\\Plastification1.sBP_Injection_Speedlimit");
            lstBasic.Add("MainInjectionParameter1\\Plastification1.sChargeMaxSpeed");
            lstBasic.Add("MainInjectionParameter1\\Plastification1.sEmergencyPRPressre");
            lstBasic.Add("MainInjectionParameter1\\Plastification1.sEmergencyPRSpeed");
            lstBasic.Add("MainInjectionParameter1\\Plastification1.sPR_Injection_Force");
            lstBasic.Add("MainInjectionParameter1\\Plastification1.sPR_SB_Injection_Acc");
            lstBasic.Add("MainInjectionParameter1\\Plastification1.sPR_SB_Injection_Dec");
            lstBasic.Add("MainInjectionParameter1\\Plastification1.sSafetyPR_Pressure");
            lstBasic.Add("MainInjectionParameter1\\Plastification1.sSafetyPR_Speed");
            lstBasic.Add("MainInjectionParameter1\\Plastification1.sSB_Injection_Force");
            lstBasic.Add("MainInjectionParameter1\\Plastification1.sScrew_Acc");
            lstBasic.Add("MainInjectionParameter1\\Plastification1.sScrew_Dec");
            lstBasic.Add("MainInjectionParameter1\\Plastification1.sSBModePreUSE");
            lstBasic.Add("MainInjectionParameter1\\Plastification1.sStepNumber");
            lstBasic.Add("MainInjectionParameter1\\Plastification1.sSBModeAftUSE");
            lstBasic.Add("MainInjectionParameter1\\Plastification1.sPR_Position");
            lstBasic.Add("MainInjectionParameter1\\Plastification1.sPR_Velocity");
            lstBasic.Add("MainInjectionParameter1\\Plastification1.sBP_MinSafety");
            lstBasic.Add("MainInjectionParameter1\\Plastification1.sScrewStartOver");
            lstBasic.Add("MainInjectionParameter1\\Plastification1.sBP_S1");
            lstBasic.Add("MainInjectionParameter1\\Plastification1.sBP_S2");
            lstBasic.Add("MainInjectionParameter1\\Plastification1.sBP_S3");
            lstBasic.Add("MainInjectionParameter1\\Plastification1.sBP_V1");
            lstBasic.Add("MainInjectionParameter1\\Plastification1.sBP_V2");
            lstBasic.Add("MainInjectionParameter1\\Plastification1.sBP_V3");
            lstBasic.Add("MainInjectionParameter1\\Plastification1.sBP_P1");
            lstBasic.Add("MainInjectionParameter1\\Plastification1.sBP_P2");
            lstBasic.Add("MainInjectionParameter1\\Plastification1.sBP_P3");
            lstBasic.Add("MainInjectionParameter1\\Plastification1.sBP_BP1");
            lstBasic.Add("MainInjectionParameter1\\Plastification1.sBP_BP2");
            lstBasic.Add("MainInjectionParameter1\\Plastification1.sBP_BP3");
            lstBasic.Add("MainInjectionParameter1\\Plastification1.sSB_Position");
            lstBasic.Add("MainInjectionParameter1\\Plastification1.sSB_Velocity");
            lstBasic.Add("MainInjectionParameter1\\Plastification1.sPR_Safety");
            lstBasic.Add("MainInjectionParameter1\\Plastification1.sBP_Safety");
            lstBasic.Add("MainInjectionParameter1\\Plastification1.sSB_Safety");
            lstBasic.Add("MainInjectionParameter1\\Plastification1.sPR_BP_Delay");
            lstBasic.Add("MainInjectionParameter1\\Plastification1.sScrewBackMode");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.sMaxContactForceKN");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.sUnitControlMode");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.sUnitTouchSensor");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.sInjUnitMaxStroke");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.sMotorForceKN");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.sMotorWithoutBrake");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.sMotorMaxHoldingTime");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.sInjUnitFWDAcc");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.sInjUnitFWDDeacc");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.sInjUnitBWDAcc");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.sInjUnitBWDDeacc");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.sInjUnitAdjSpeed");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.sInjUnitAdjForce");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.sUnitBackwardDelayTime");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.sUnitMotorBrakeOpenTime");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.sUnitBackwardMode");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.sUnitBackwardTime");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.sUnitBackwardDelayTime");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.sUnitForwardSafety");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.sMinContactForcePercent");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.sUseContactMonitor");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.sOPENHoldingForceKN");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.sInjUnitReleasePosition");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.sTouchComformDelay");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.sContactForceKN");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.sSetFWDStep");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.sS1FWD");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.sS2FWD");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.sSEFWD");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.sV1FWD");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.sV2FWD");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.sVEFWD");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.sP1FWD");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.sP2FWD");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.sPEFWD");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.sSetBWDStep");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.sSEBWD");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.sS2BWD");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.sS1BWD");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.sVEBWD");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.sV2BWD");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.sV1BWD");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.sP1FWD");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.sP2FWD");
            lstBasic.Add("MainInjectionParameter1\\InjectionUnit1.sPEFWD");
            lstBasic.Add("CNC_VM1\\AxisData.ForceReleaseAcc");
            lstBasic.Add("CNC_VM1\\AxisData.ForcereleaseForce");
            lstBasic.Add("CNC_VM1\\AxisData.ForceReleaseSpeed");
            lstBasic.Add("CNC_VM1\\AxisData.Ol2Force");
            lstBasic.Add("CNC_VM1\\AxisData.vKv");
            lstBasic.Add("CNC_VM1\\AxisData.vVu");
            lstBasic.Add("CNC_VM1\\AxisData.Ext_Units");
            lstBasic.Add("CNC_VM1\\AxisData.Int_Units");
            lstBasic.Add("CNC_VM1\\AxisData.V_Max");
            lstBasic.Add("CNC_VM1\\AxisData.A_Max");
            lstBasic.Add("CNC_VM1\\AxisData.OL2Speed");
            lstBasic.Add("CNC_VM1\\AxisData.Ol2Force");
            lstBasic.Add("CNC_VM1\\AxisData.IncPerRot");
            lstBasic.Add("CNC_VM1\\Ram_scurve.Data");
            lstBasic.Add("CNC_VM1\\TuneTableReferenceOffset.Data");
            lstBasic.Add("LubricationControl1.sInjScrewLubrication");
            lstBasic.Add("LubricationControl1.sLubPumpMode");
            lstBasic.Add("LubricationControl1.sBlinkDelay");
            lstBasic.Add("LubricationControl1.sTotalCycleMoldCounter");
            lstBasic.Add("LubricationControl1.sInjScrewMoldCounter");
            lstBasic.Add("LubricationControl1.sRestPartLubricationCount");
            lstBasic.Add("LubricationControl1.sInjScrewCycleMold");
            lstBasic.Add("LubricationControl1.sInjScrewLubrication");
            lstBasic.Add("LubricationControl1.sInjScrewLubricationCount");
            lstBasic.Add("LubricationControl1.sInjScrewLubricationTime");
            lstBasic.Add("LubricationControl1.sRestPartsCycleMold");
            lstBasic.Add("LubricationControl1.sRestPartsLubrication");
            lstBasic.Add("LubricationControl1.sRestPartsLubricationTime");
            lstBasic.Add("LubricationControl1.sRestPartsMoldCounter");
            lstBasic.Add("LubricationControl1.sBlinkDelay");
            lstBasic.Add("LubricationControl1.sLubPumpMode");
            lstBasic.Add("MoldReferenceState.Data");
            lstBasic.Add("Ram_TouchPos.Data");
            lstBasic.Add("Ram_EjebackMoldclose.Data");
            lstBasic.Add("objMoldControl.sMoldSpringUse");
            lstBasic.Add("ClampPosSwitch1.PosWin");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sMaxMoldOpenPosition");
            lstBasic.Add("objMoldParameter\\TwoRankParameters_MoldClose.sInjectionHoldingAcc1");
            lstBasic.Add("objMoldParameter\\TwoRankParameters_MoldClose.sInjectionHoldingAcc2");
            lstBasic.Add("objMoldParameter\\TwoRankParameters_MoldClose.sInjectionHoldingDec1");
            lstBasic.Add("objMoldParameter\\TwoRankParameters_MoldClose.sInjectionHoldingDec2");
            lstBasic.Add("objMoldParameter\\TwoRankParameters_MoldClose.sSwitch");
            lstBasic.Add("objMoldParameter\\TwoRankParameters_MoldOpen.sInjectionHoldingAcc1");
            lstBasic.Add("objMoldParameter\\TwoRankParameters_MoldOpen.sInjectionHoldingAcc2");
            lstBasic.Add("objMoldParameter\\TwoRankParameters_MoldOpen.sInjectionHoldingDec1");
            lstBasic.Add("objMoldParameter\\TwoRankParameters_MoldOpen.sInjectionHoldingDec2");
            lstBasic.Add("objMoldParameter\\TwoRankParameters_MoldOpen.sSwitch");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sMoldCloseSafetyTime");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sMoldOpenSafetyTime");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sMoldOpenMaxAcc");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sMoldOpenMaxDec");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sMoldCloseMaxAcc");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sMoldCloseMaxDec");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sMoldCloseProtectedTime");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sMoldAdjustOpenPosition");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sMoldCloseMaxClampAcc");
            lstBasic.Add("objMoldParameter\\Moldparamter1.MoldCloseMaxClampAcc");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sMoldCloseMaxClampDec");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sMoldUnitMaxStepNumber");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sMoldUnitBlockSafetyTime");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sMoldUnitTouchConfirmTime");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sMoldAdjustWindowLockPos");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sMoldClampTons");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sTuneTMoldOpen");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sTuneTStartPos");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sInjUnitStartPos");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sInjUnitMoldClose");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sUseClampSensor");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sEmergencySpeed");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sEjectToqueLimit");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sMoldMonitiorTime");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sMoldAdjustSafetyTime");
            lstBasic.Add("objMoldParameter\\Moldparamter1.MoldCloseV5Maximum");
            lstBasic.Add("objMoldParameter\\Moldparamter1.CompInjMoldPosA2");
            lstBasic.Add("objMoldParameter\\Moldparamter1.CompInjMoldPosA1");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sMoldOpenAdjustForce");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sMoldOpenAdjustSpeed");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sMoldCloseAdjustForce");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sMoldCloseAdjustSpeed");
            lstBasic.Add("objMoldParameter\\Moldparamter1.MoldCloseV5Maximum");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sEjectToqueLimit");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sEmergencySpeed");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sMoldAdjustOpenPosition");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sMoldProtectedForce");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sMoldMonitiorTime");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sMoldOpenStepNumber");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sS1MoldOpen");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sS2MoldOpen");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sS3MoldOpen");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sS4MoldOpen");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sS5MoldOpen");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sS6MoldOpen");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sV1MoldOpen");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sV2MoldOpen");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sV3MoldOpen");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sV4MoldOpen");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sV5MoldOpen");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sV6MoldOpen");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sP1MoldOpen");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sP2MoldOpen");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sP3MoldOpen");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sP4MoldOpen");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sP5MoldOpen");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sP6MoldOpen");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sMoldCloseStepNumber");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sS1MoldClose");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sS2MoldClose");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sS3MoldClose");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sS4MoldClose");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sS5MoldClose");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sS6MoldClose");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sV1MoldClose");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sV2MoldClose");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sV3MoldClose");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sV4MoldClose");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sV5MoldClose");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sV6MoldClose");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sP1MoldClose");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sP2MoldClose");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sP3MoldClose");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sP4MoldClose");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sMoldProtectStart");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sMoldProtectEnd");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sEjectorMoldOpen");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sEjectorStartPos");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sEjeCheckPos");
            lstBasic.Add("objMoldParameter\\Moldparamter1.sEjectorMoldClose");
            lstBasic.Add("objMoldParameter\\MoldUnitParameter1.EncoderAdjustActiv");
            lstBasic.Add("objMoldParameter\\MoldUnitParameter1.Encoderincrements");
            lstBasic.Add("objMoldParameter\\MoldUnitParameter1.MotorPullyTeeth");
            lstBasic.Add("objMoldParameter\\MoldUnitParameter1.TiebarPitch");
            lstBasic.Add("objMoldParameter\\MoldUnitParameter1.TiebarPullyTeeth");
            lstBasic.Add("objMoldParameter\\MoldUnitParameter1.AdjustLockPosWindow");
            lstBasic.Add("objMoldParameter\\MoldUnitParameter1.MaxAdjustDistance");
            lstBasic.Add("objMoldParameter\\MoldUnitParameter1.sMoldUnitSaftyPosi");
            lstBasic.Add("objMoldParameter\\MoldUnitParameter1.MaxMoldHeight");
            lstBasic.Add("objMoldParameter\\MoldUnitParameter1.MinMoldHeight");
            lstBasic.Add("objMoldParameter\\MoldUnitParameter1.MoldHeight");
            lstBasic.Add("objMoldParameter\\MoldUnitParameter1.PreMotorOffTime");
            lstBasic.Add("objMoldParameter\\MoldUnitParameter1.MaxAdjustDistance");
            lstBasic.Add("objMoldParameter\\MoldUnitParameter1.sMoldUnitUnbrakeAheadTime");
            lstBasic.Add("TuneTabPara1\\TuneTableParameters1.sControlMode");
            lstBasic.Add("TuneTabPara1\\TuneTableParameters1.sSafetyConf");
            lstBasic.Add("TuneTabPara1\\TuneTableParameters1.sChkConf");
            lstBasic.Add("TuneTabPara1\\TuneTableParameters1.sSegments");
            lstBasic.Add("TuneTabPara1\\TuneTableParameters1.sFWDSafetyTime");
            lstBasic.Add("TuneTabPara1\\TuneTableParameters1.sBWDSafetyTime");
            lstBasic.Add("TuneTabPara1\\TuneTableParameters1.sMoveDelay");
            lstBasic.Add("TuneTabPara1\\TuneTableParameters1.sBrakeDelay");
            lstBasic.Add("TuneTabPara1\\TuneTableParameters1.sBrakeSafetyTime");
            lstBasic.Add("TuneTabPara1\\TuneTableParameters1.sBrakeCheck");
            lstBasic.Add("TuneTabPara1\\TuneTableParameters1.sTableStorke");
            lstBasic.Add("TuneTabPara1\\TuneTableParameters1.sTableMode");
            lstBasic.Add("TuneTabPara1\\TuneTableParameters1.sGetStart");
            lstBasic.Add("TuneTabPara1\\TuneTableParameters1.sPosStart");
            lstBasic.Add("TuneTabPara1\\TuneTableParameters1.sGetEnd");
            lstBasic.Add("TuneTabPara1\\TuneTableParameters1.sPosEnd");
            lstBasic.Add("TuneTabPara1\\TuneTableParameters1.sFWDAcc");
            lstBasic.Add("TuneTabPara1\\TuneTableParameters1.sFWDDacc");
            lstBasic.Add("TuneTabPara1\\TuneTableParameters1.sBWDAcc");
            lstBasic.Add("TuneTabPara1\\TuneTableParameters1.sBWDDacc");
            lstBasic.Add("TuneTabPara1\\TuneTableParameters1.sFWDSpeed");
            lstBasic.Add("TuneTabPara1\\TuneTableParameters1.sBWDSpeed");
            lstBasic.Add("TuneTabPara1\\TuneTableParameters1.sADJSpeed");
            lstBasic.Add("TuneTabPara1\\TuneTableParameters1.sADJForce");
            lstBasic.Add("KeyControl2.EmergencyStop");
            lstBasic.Add("InputSelect1.sSelect");
            lstBasic.Add("OffsetPosition.Data");
            lstBasic.Add("ReferenceOffset.Data");
            lstBasic.Add("objMoldParameter\\EmbossClampInj.sValue1");
            lstBasic.Add("objMoldParameter\\EmbossClampInj.sValue2");
            lstBasic.Add("objMoldParameter\\EmbossClampInj.sValue3");
            lstBasic.Add("objMoldParameter\\EmbossClampInj.sValue4");
            lstBasic.Add("objMoldParameter\\EmbossClampInj.sValue5");
            lstBasic.Add("objMoldParameter\\EmbossClampInj.sValue6");
            lstBasic.Add("objMoldParameter\\EmbossClampInj.sValue7");
            lstBasic.Add("objMoldParameter\\EmbossClampInj.sValue8");
            lstBasic.Add("objMoldParameter\\EmbossClampInj.sValue9");
            lstBasic.Add("objMoldParameter\\EmbossClampInj.sValue10");
            lstBasic.Add("MoldProtect1.sProtectUSE");
            lstBasic.Add("MoldProtect1.sCurrentLimit");
            lstBasic.Add("MoldProtect1.sCurrentLimit2");
            lstBasic.Add("MoldProtect1.sSpeedLimit");
            lstBasic.Add("MoldProtect1.sSpeedLimit2");
            lstBasic.Add("MoldProtect1.sPositionLimit");
            lstBasic.Add("MoldProtect1.sTonLimit");
            lstBasic.Add("ForceAndPos1.preTon");
            lstBasic.Add("ForceAndPos1.prePlus");
            lstBasic.Add("ForceAndPos1.P1000");
            lstBasic.Add("ForceAndPos1.P2000");
            lstBasic.Add("ForceAndPos1.P3000");
            lstBasic.Add("ForceAndPos1.P4000");
            lstBasic.Add("ForceAndPos1.P5000");
            lstBasic.Add("ForceAndPos1.P6000");
            lstBasic.Add("TonForceConvert1\\ClampTonsLinearTable1.sMinValue");
            lstBasic.Add("TonForceConvert1\\ClampTonsLinearTable1.sDate1");
            lstBasic.Add("TonForceConvert1\\ClampTonsLinearTable1.sDate2");
            lstBasic.Add("TonForceConvert1\\ClampTonsLinearTable1.sDate3");
            lstBasic.Add("TonForceConvert1\\ClampTonsLinearTable1.sDate4");
            lstBasic.Add("TonForceConvert1\\ClampTonsLinearTable1.sDate5");
            lstBasic.Add("TonForceConvert1\\ClampTonsLinearTable1.sDate6");
            lstBasic.Add("TonForceConvert1\\ClampTonsLinearTable1.sDate7");
            lstBasic.Add("TonForceConvert1\\ClampTonsLinearTable1.sDate8");
            lstBasic.Add("TonForceConvert1\\ClampTonsLinearTable1.sDate9");
            lstBasic.Add("TonForceConvert1\\ClampTonsLinearTable1.sDate10");
            lstBasic.Add("TonForceConvert1\\ClampTonsLinearTable1.sMaxValue");
            lstBasic.Add("SetMaxClampForce.Data");
            lstBasic.Add("R_MachineType.Data");
            lstBasic.Add("R_AutoFeed.Data");
            lstBasic.Add("R_ScrewType.Data");
            lstBasic.Add("R_InterRob.Data");
            lstBasic.Add("SafetyLockTime.Data");
            lstBasic.Add("SaftyMoldPosition.Data");
            lstBasic.Add("CheckLoadRam.Data");
            lstBasic.Add("RamSourceFileMoldLoaded.Data");
            lstBasic.Add("RamSourceFileInjection.Data");
            lstBasic.Add("RamScrewFileLoaded.Data");
            lstBasic.Add("OptionalFunction.sAdjustMaximum");
            lstBasic.Add("OptionalFunction.sAdjustMinimum");
            lstBasic.Add("OptionalFunction.sAirblowVisible");
            lstBasic.Add("OptionalFunction.sCoreVisible");
            lstBasic.Add("OptionalFunction.sInjCoolMotor");
            lstBasic.Add("OptionalFunction.sProgrammableIO");
            lstBasic.Add("OptionalFunction.sVisibleCheckbit");
            lstBasic.Add("OptionalFunction.sVolveGate");
            lstBasic.Add("OptionalFunction.sSensorClamp");
            lstBasic.Add("OptionalFunction.sAdjustSaftyTime");
            lstBasic.Add("OptionalFunction.sClmpForceOp");
            lstBasic.Add("OptionalFunction.sClmpForceCheck");
            lstBasic.Add("OptionalFunction.sCompresOpenChargeOFF");
            lstBasic.Add("_ColorLamp1.s_MultiColor");
            lstBasic.Add("OptionalFunction.sInjectionOff");
            lstBasic.Add("OptionalFunction.sCoolingMotor");
            lstBasic.Add("VICO_MAIN1.sMoldChanged");
            lstBasic.Add("MainParameterCOMPL1\\MainParameter1.CE_Machine");
            lstBasic.Add("MainParameterCOMPL1\\MainParameter1.sMoldHeatinguse");
            lstBasic.Add("MainParameterCOMPL1\\MainParameter1.sCycleIntervallMode");
            lstBasic.Add("MainParameterCOMPL1\\MainParameter1.sCycleIntervalSafetyTime");
            lstBasic.Add("MainParameterCOMPL1\\MainParameter1.sPhotoSensorMonitorTime");
            lstBasic.Add("MainParameterCOMPL1\\MainParameter1.sMechSafetyAlaTime");
            lstBasic.Add("MainParameterCOMPL1\\MainParameter1.CompInjSelect");
            lstBasic.Add("MainParameterCOMPL1\\MainParameter1.CompInjMode");
            lstBasic.Add("MainParameterCOMPL1\\MainParameter1.CompInjA4Time");
            lstBasic.Add("MainParameterCOMPL1\\MainParameter1.A5FunctionSwitch");
            lstBasic.Add("MainParameterCOMPL1\\MainParameter1.SpecialFrontDoorCheck");
            lstBasic.Add("MainParameterCOMPL1\\MainParameter1.sCoreSelectionForRobot");
            lstBasic.Add("MainParameterCOMPL1\\MainParameter1.sCore2SelectionForRobot");
            lstBasic.Add("MainParameterCOMPL1\\MainParameter1.AirBlowKeyDefine");
            lstBasic.Add("MainParameterCOMPL1\\MainParameter1.AirBlowKey2Define");
            lstBasic.Add("MainParameterCOMPL1\\MainParameter1.MoldOpenTime");
            lstBasic.Add("SafetyLock1.sReleasePosition");
            lstBasic.Add("ZoneType1.Data");
            lstBasic.Add("ZoneType2.Data");
            lstBasic.Add("ZoneType3.Data");
            lstBasic.Add("ZoneType4.Data");
            lstBasic.Add("ZoneType5.Data");
            lstBasic.Add("ZoneType6.Data");
            lstBasic.Add("ZoneType7.Data");
            lstBasic.Add("ZoneType8.Data");
            lstBasic.Add("Mold_Inside_Pressre1.sLoad_Cell_Range");
            lstBasic.Add("Mold_Inside_Pressre1.sVPSwitchForce");
            lstBasic.Add("Mold_Inside_Pressre1.sVpSelect");
            lstBasic.Add("Mold_Inside_Pressre1.sCurveSelect");
            lstBasic.Add("Mold_Inside_Pressre1.sVpAnalogSignal");
            lstBasic.Add("ReferenceSpeedandForce1.sReferenceForce");
            lstBasic.Add("ReferenceSpeedandForce1.sReferenceSpeed");
            lstBasic.Add("InjectionSlv_Axis\\TempControll.sDrivLimitedTemp");
            lstBasic.Add("InjectionSlv_Axis\\TempControll.sLimitedTemp");
            lstBasic.Add("Injection_Axis\\TempControll.sDrivLimitedTemp");
            lstBasic.Add("Injection_Axis\\TempControll.sLimitedTemp");
            lstBasic.Add("Mold_Axis\\TempControll.sDrivLimitedTemp");
            lstBasic.Add("Mold_Axis\\TempControll.sLimitedTemp");
            lstBasic.Add("MoldSlve_Axis\\TempControll.sDrivLimitedTemp");
            lstBasic.Add("MoldSlve_Axis\\TempControll.sLimitedTemp");
            lstBasic.Add("Ejector_Axis\\TempControll.sDrivLimitedTemp");
            lstBasic.Add("Ejector_Axis\\TempControll.sLimitedTemp");
            lstBasic.Add("InjUnit_Axis\\TempControll.sDrivLimitedTemp");
            lstBasic.Add("InjUnit_Axis\\TempControll.sLimitedTemp");
            lstBasic.Add("TuneTable_Axis\\TempControll.sDrivLimitedTemp");
            lstBasic.Add("TuneTable_Axis\\TempControll.sLimitedTemp");
            lstBasic.Add("Plastic_Axis\\TempControll.sDrivLimitedTemp");
            lstBasic.Add("Plastic_Axis\\TempControll.sLimitedTemp");
            lstBasic.Add("PlasticSlve_Axis\\TempControll.sDrivLimitedTemp");
            lstBasic.Add("PlasticSlve_Axis\\TempControll.sLimitedTemp");
            lstBasic.Add("HeatingZoneControl1.sHeatingLimitValue");
            lstBasic.Add("HeatingZoneControl1.sHeatingZoneSwitch");
            lstBasic.Add("HeatingZoneControl1\\Check_Heat_First_Rise.Data");
            lstBasic.Add("HeatingZoneControl1\\Kp_First_Rise.Data");
            lstBasic.Add("HeatingZoneControl1\\Tn_First_Rise.Data");
            lstBasic.Add("HeatingZoneControl1\\Tv_First_Rise.Data");
            lstBasic.Add("HeatingZoneControl1.sHeatingSet");
            lstBasic.Add("HeatingZoneControl1.sHeatingMax");
            lstBasic.Add("HeatingZoneControl1.sHeatingMin");
            lstBasic.Add("HeatingZoneControl1.sHeatingMode");
            lstBasic.Add("HeatingZoneControl1.sHeatingStandby");
            lstBasic.Add("HeatingZoneControl1.sHeatingRate");
            lstBasic.Add("HeatingZoneControl2.sHeatingLimitValue");
            lstBasic.Add("HeatingZoneControl2.sHeatingZoneSwitch");
            lstBasic.Add("HeatingZoneControl2\\Check_Heat_First_Rise.Data");
            lstBasic.Add("HeatingZoneControl2\\Kp_First_Rise.Data");
            lstBasic.Add("HeatingZoneControl2\\Tn_First_Rise.Data");
            lstBasic.Add("HeatingZoneControl2\\Tv_First_Rise.Data");
            lstBasic.Add("HeatingZoneControl2.sHeatingSet");
            lstBasic.Add("HeatingZoneControl2.sHeatingMax");
            lstBasic.Add("HeatingZoneControl2.sHeatingMin");
            lstBasic.Add("HeatingZoneControl2.sHeatingMode");
            lstBasic.Add("HeatingZoneControl2.sHeatingStandby");
            lstBasic.Add("HeatingZoneControl2.sHeatingRate");
            lstBasic.Add("HeatingZoneControl3.sHeatingLimitValue");
            lstBasic.Add("HeatingZoneControl3.sHeatingZoneSwitch");
            lstBasic.Add("HeatingZoneControl3\\Check_Heat_First_Rise.Data");
            lstBasic.Add("HeatingZoneControl3\\Kp_First_Rise.Data");
            lstBasic.Add("HeatingZoneControl3\\Tn_First_Rise.Data");
            lstBasic.Add("HeatingZoneControl3\\Tv_First_Rise.Data");
            lstBasic.Add("HeatingZoneControl3.sHeatingSet");
            lstBasic.Add("HeatingZoneControl3.sHeatingMax");
            lstBasic.Add("HeatingZoneControl3.sHeatingMin");
            lstBasic.Add("HeatingZoneControl3.sHeatingMode");
            lstBasic.Add("HeatingZoneControl3.sHeatingStandby");
            lstBasic.Add("HeatingZoneControl3.sHeatingRate");
            lstBasic.Add("HeatingZoneControl4.sHeatingLimitValue");
            lstBasic.Add("HeatingZoneControl4.sHeatingZoneSwitch");
            lstBasic.Add("HeatingZoneControl4\\Check_Heat_First_Rise.Data");
            lstBasic.Add("HeatingZoneControl4\\Kp_First_Rise.Data");
            lstBasic.Add("HeatingZoneControl4\\Tn_First_Rise.Data");
            lstBasic.Add("HeatingZoneControl4\\Tv_First_Rise.Data");
            lstBasic.Add("HeatingZoneControl4.sHeatingSet");
            lstBasic.Add("HeatingZoneControl4.sHeatingMax");
            lstBasic.Add("HeatingZoneControl4.sHeatingMin");
            lstBasic.Add("HeatingZoneControl4.sHeatingMode");
            lstBasic.Add("HeatingZoneControl4.sHeatingStandby");
            lstBasic.Add("HeatingZoneControl4.sHeatingRate");
            lstBasic.Add("HeatingZoneControl5.sHeatingLimitValue");
            lstBasic.Add("HeatingZoneControl5.sHeatingZoneSwitch");
            lstBasic.Add("HeatingZoneControl5\\Check_Heat_First_Rise.Data");
            lstBasic.Add("HeatingZoneControl5\\Kp_First_Rise.Data");
            lstBasic.Add("HeatingZoneControl5\\Tn_First_Rise.Data");
            lstBasic.Add("HeatingZoneControl5\\Tv_First_Rise.Data");
            lstBasic.Add("HeatingZoneControl5.sHeatingSet");
            lstBasic.Add("HeatingZoneControl5.sHeatingMax");
            lstBasic.Add("HeatingZoneControl5.sHeatingMin");
            lstBasic.Add("HeatingZoneControl5.sHeatingMode");
            lstBasic.Add("HeatingZoneControl5.sHeatingStandby");
            lstBasic.Add("HeatingZoneControl5.sHeatingRate");
            lstBasic.Add("HeatingZoneControl6.sHeatingLimitValue");
            lstBasic.Add("HeatingZoneControl6.sHeatingZoneSwitch");
            lstBasic.Add("HeatingZoneControl6\\Check_Heat_First_Rise.Data");
            lstBasic.Add("HeatingZoneControl6\\Kp_First_Rise.Data");
            lstBasic.Add("HeatingZoneControl6\\Tn_First_Rise.Data");
            lstBasic.Add("HeatingZoneControl6\\Tv_First_Rise.Data");
            lstBasic.Add("HeatingZoneControl6.sHeatingSet");
            lstBasic.Add("HeatingZoneControl6.sHeatingMax");
            lstBasic.Add("HeatingZoneControl6.sHeatingMin");
            lstBasic.Add("HeatingZoneControl6.sHeatingMode");
            lstBasic.Add("HeatingZoneControl6.sHeatingStandby");
            lstBasic.Add("HeatingZoneControl6.sHeatingRate");
            lstBasic.Add("HeatingZoneControl7.sHeatingLimitValue");
            lstBasic.Add("HeatingZoneControl7.sHeatingZoneSwitch");
            lstBasic.Add("HeatingZoneControl7\\Check_Heat_First_Rise.Data");
            lstBasic.Add("HeatingZoneControl7\\Kp_First_Rise.Data");
            lstBasic.Add("HeatingZoneControl7\\Tn_First_Rise.Data");
            lstBasic.Add("HeatingZoneControl7\\Tv_First_Rise.Data");
            lstBasic.Add("HeatingZoneControl7.sHeatingSet");
            lstBasic.Add("HeatingZoneControl7.sHeatingMax");
            lstBasic.Add("HeatingZoneControl7.sHeatingMin");
            lstBasic.Add("HeatingZoneControl7.sHeatingMode");
            lstBasic.Add("HeatingZoneControl7.sHeatingStandby");
            lstBasic.Add("HeatingZoneControl7.sHeatingRate");
            lstBasic.Add("HeatingZoneControl8.sHeatingLimitValue");
            lstBasic.Add("HeatingZoneControl8.sHeatingZoneSwitch");
            lstBasic.Add("HeatingZoneControl8\\Check_Heat_First_Rise.Data");
            lstBasic.Add("HeatingZoneControl8\\Kp_First_Rise.Data");
            lstBasic.Add("HeatingZoneControl8\\Tn_First_Rise.Data");
            lstBasic.Add("HeatingZoneControl8\\Tv_First_Rise.Data");
            lstBasic.Add("HeatingZoneControl8.sHeatingSet");
            lstBasic.Add("HeatingZoneControl8.sHeatingMax");
            lstBasic.Add("HeatingZoneControl8.sHeatingMin");
            lstBasic.Add("HeatingZoneControl8.sHeatingMode");
            lstBasic.Add("HeatingZoneControl8.sHeatingStandby");
            lstBasic.Add("HeatingZoneControl8.sHeatingRate");
            lstBasic.Add("BarrelControl1.sScrewPreventTemperature");
            lstBasic.Add("BarrelControl1.sScrewPreventTime");
            lstBasic.Add("BarrelControl1.sTuneTemp");
            lstBasic.Add("HopperControl1.sHopperChkBit");
            lstBasic.Add("HopperControl1.sHeatingSet");
            lstBasic.Add("HopperControl1.sHeatingMax");
            lstBasic.Add("HopperControl1.sHeatingMin");
            lstBasic.Add("HopperControl1.sHopperSwitch");
            lstBasic.Add("CoreParameter1.CoreASelection");
            lstBasic.Add("CoreParameter1.CoreAInMoldPosition");
            lstBasic.Add("CoreParameter1.CoreAInMode");
            lstBasic.Add("CoreParameter1.CoreAInDelayTime");
            lstBasic.Add("CoreParameter1.CoreAInTime");
            lstBasic.Add("CoreParameter1.CoreAInStandby");
            lstBasic.Add("CoreParameter1.CoreAInDuringMold");
            lstBasic.Add("CoreParameter1.CoreAOutMoldPosition");
            lstBasic.Add("CoreParameter1.CoreAOutMode");
            lstBasic.Add("CoreParameter1.CoreAOutDelayTime");
            lstBasic.Add("CoreParameter1.CoreAOutTime");
            lstBasic.Add("CoreParameter1.CoreAOutDuringMold");
            lstBasic.Add("CoreParameter1.CoreBSelection");
            lstBasic.Add("CoreParameter1.CoreBInMoldPosition");
            lstBasic.Add("CoreParameter1.CoreBInMode");
            lstBasic.Add("CoreParameter1.CoreBInDelayTime");
            lstBasic.Add("CoreParameter1.CoreBInTime");
            lstBasic.Add("CoreParameter1.CoreBInStandby");
            lstBasic.Add("CoreParameter1.CoreBInDuringMold");
            lstBasic.Add("CoreParameter1.CoreBOutMoldPosition");
            lstBasic.Add("CoreParameter1.CoreBOutMode");
            lstBasic.Add("CoreParameter1.CoreBOutDelayTime");
            lstBasic.Add("CoreParameter1.CoreBOutTime");
            lstBasic.Add("CoreParameter1.CoreBOutDuringMold");
            lstBasic.Add("CoreParameter1.CoreCSelection");
            lstBasic.Add("CoreParameter1.CoreCInMoldPosition");
            lstBasic.Add("CoreParameter1.CoreCInMode");
            lstBasic.Add("CoreParameter1.CoreCInDelayTime");
            lstBasic.Add("CoreParameter1.CoreCInTime");
            lstBasic.Add("CoreParameter1.CoreCInStandby");
            lstBasic.Add("CoreParameter1.CoreCInDuringMold");
            lstBasic.Add("CoreParameter1.CoreCOutMoldPosition");
            lstBasic.Add("CoreParameter1.CoreCOutMode");
            lstBasic.Add("CoreParameter1.CoreCOutDelayTime");
            lstBasic.Add("CoreParameter1.CoreCOutTime");
            lstBasic.Add("CoreParameter1.CoreCOutDuringMold");
            lstBasic.Add("CoreParameter1.CoreDSelection");
            lstBasic.Add("CoreParameter1.CoreDInMoldPosition");
            lstBasic.Add("CoreParameter1.CoreDInMode");
            lstBasic.Add("CoreParameter1.CoreDInDelayTime");
            lstBasic.Add("CoreParameter1.CoreDInTime");
            lstBasic.Add("CoreParameter1.CoreDInStandby");
            lstBasic.Add("CoreParameter1.CoreDInDuringMold");
            lstBasic.Add("CoreParameter1.CoreDOutMoldPosition");
            lstBasic.Add("CoreParameter1.CoreDOutMode");
            lstBasic.Add("CoreParameter1.CoreDOutDelayTime");
            lstBasic.Add("CoreParameter1.CoreDOutTime");
            lstBasic.Add("CoreParameter1.CoreDOutDuringMold");
            lstBasic.Add("CoreParameter1.CoreESelection");
            lstBasic.Add("CoreParameter1.CoreEInMoldPosition");
            lstBasic.Add("CoreParameter1.CoreEInMode");
            lstBasic.Add("CoreParameter1.CoreEInDelayTime");
            lstBasic.Add("CoreParameter1.CoreEInTime");
            lstBasic.Add("CoreParameter1.CoreEInStandby");
            lstBasic.Add("CoreParameter1.CoreEInDuringMold");
            lstBasic.Add("CoreParameter1.CoreEOutMoldPosition");
            lstBasic.Add("CoreParameter1.CoreEOutMode");
            lstBasic.Add("CoreParameter1.CoreEOutDelayTime");
            lstBasic.Add("CoreParameter1.CoreEOutTime");
            lstBasic.Add("CoreParameter1.CoreEOutDuringMold");
            lstBasic.Add("CoreParameter1.CoreFSelection");
            lstBasic.Add("CoreParameter1.CoreFInMoldPosition");
            lstBasic.Add("CoreParameter1.CoreFInMode");
            lstBasic.Add("CoreParameter1.CoreFInDelayTime");
            lstBasic.Add("CoreParameter1.CoreFInTime");
            lstBasic.Add("CoreParameter1.CoreFInStandby");
            lstBasic.Add("CoreParameter1.CoreFInDuringMold");
            lstBasic.Add("CoreParameter1.CoreFOutMoldPosition");
            lstBasic.Add("CoreParameter1.CoreFOutMode");
            lstBasic.Add("CoreParameter1.CoreFOutDelayTime");
            lstBasic.Add("CoreParameter1.CoreFOutTime");
            lstBasic.Add("CoreParameter1.CoreFOutDuringMold");
            lstBasic.Add("CoreParameter1.CoreAChkBit");
            lstBasic.Add("CoreParameter1.CoreBChkBit");
            lstBasic.Add("CoreParameter1.CoreCChkBit");
            lstBasic.Add("CoreParameter1.CoreDChkBit");
            lstBasic.Add("CoreParameter1.CoreEChkBit");
            lstBasic.Add("CoreParameter1.CoreFChkBit");
            lstBasic.Add("PumpSwitch1.sSwitch");
            lstBasic.Add("PumpSwitch2.sSwitch");
            lstBasic.Add("PumpSwitch3.sSwitch");
            lstBasic.Add("PumpSwitch4.sSwitch");
            lstBasic.Add("PumpSwitch5.sSwitch");
            lstBasic.Add("PumpSwitch6.sSwitch");
            lstBasic.Add("AirBlowMainParameter1\\AirBlowPara1.sChkBit");
            lstBasic.Add("AirBlowMainParameter1\\AirBlowPara1.sMode");
            lstBasic.Add("AirBlowMainParameter1\\AirBlowPara1.sStartCondition");
            lstBasic.Add("AirBlowMainParameter1\\AirBlowPara1.sDelayTime");
            lstBasic.Add("AirBlowMainParameter1\\AirBlowPara1.sTime");
            lstBasic.Add("AirBlowMainParameter2\\AirBlowPara1.sChkBit");
            lstBasic.Add("AirBlowMainParameter2\\AirBlowPara1.sMode");
            lstBasic.Add("AirBlowMainParameter2\\AirBlowPara1.sStartCondition");
            lstBasic.Add("AirBlowMainParameter2\\AirBlowPara1.sDelayTime");
            lstBasic.Add("AirBlowMainParameter2\\AirBlowPara1.sTime");
            lstBasic.Add("AirBlowMainParameter3\\AirBlowPara1.sMode");
            lstBasic.Add("AirBlowMainParameter3\\AirBlowPara1.sStartCondition");
            lstBasic.Add("AirBlowMainParameter3\\AirBlowPara1.sDelayTime");
            lstBasic.Add("AirBlowMainParameter3\\AirBlowPara1.sTime");
            lstBasic.Add("AirBlowMainParameter3\\AirBlowPara1.sChkBit");
            lstBasic.Add("AirBlowMainParameter4\\AirBlowPara1.sChkBit");
            lstBasic.Add("AirBlowMainParameter4\\AirBlowPara1.sMode");
            lstBasic.Add("AirBlowMainParameter4\\AirBlowPara1.sStartCondition");
            lstBasic.Add("AirBlowMainParameter4\\AirBlowPara1.sDelayTime");
            lstBasic.Add("AirBlowMainParameter4\\AirBlowPara1.sTime");
            lstBasic.Add("AirBlowMainParameter5\\AirBlowPara1.sChkBit");
            lstBasic.Add("AirBlowMainParameter5\\AirBlowPara1.sMode");
            lstBasic.Add("AirBlowMainParameter5\\AirBlowPara1.sStartCondition");
            lstBasic.Add("AirBlowMainParameter5\\AirBlowPara1.sDelayTime");
            lstBasic.Add("AirBlowMainParameter5\\AirBlowPara1.sTime");
            lstBasic.Add("AirBlowMainParameter6\\AirBlowPara1.sChkBit");
            lstBasic.Add("AirBlowMainParameter6\\AirBlowPara1.sMode");
            lstBasic.Add("AirBlowMainParameter6\\AirBlowPara1.sStartCondition");
            lstBasic.Add("AirBlowMainParameter6\\AirBlowPara1.sDelayTime");
            lstBasic.Add("AirBlowMainParameter6\\AirBlowPara1.sTime");
            lstBasic.Add("AirBlowMainParameter7\\AirBlowPara1.sChkBit");
            lstBasic.Add("AirBlowMainParameter7\\AirBlowPara1.sMode");
            lstBasic.Add("AirBlowMainParameter7\\AirBlowPara1.sStartCondition");
            lstBasic.Add("AirBlowMainParameter7\\AirBlowPara1.sDelayTime");
            lstBasic.Add("AirBlowMainParameter7\\AirBlowPara1.sTime");
            lstBasic.Add("AirBlowMainParameter8\\AirBlowPara1.sChkBit");
            lstBasic.Add("AirBlowMainParameter8\\AirBlowPara1.sMode");
            lstBasic.Add("AirBlowMainParameter8\\AirBlowPara1.sStartCondition");
            lstBasic.Add("AirBlowMainParameter8\\AirBlowPara1.sDelayTime");
            lstBasic.Add("AirBlowMainParameter8\\AirBlowPara1.sTime");
            lstBasic.Add("AirBlowMainParameter9\\AirBlowPara1.sChkBit");
            lstBasic.Add("AirBlowMainParameter9\\AirBlowPara1.sMode");
            lstBasic.Add("AirBlowMainParameter9\\AirBlowPara1.sStartCondition");
            lstBasic.Add("AirBlowMainParameter9\\AirBlowPara1.sDelayTime");
            lstBasic.Add("AirBlowMainParameter9\\AirBlowPara1.sTime");
            lstBasic.Add("AirBlowMainParameter10\\AirBlowPara1.sChkBit");
            lstBasic.Add("AirBlowMainParameter10\\AirBlowPara1.sMode");
            lstBasic.Add("AirBlowMainParameter10\\AirBlowPara1.sStartCondition");
            lstBasic.Add("AirBlowMainParameter10\\AirBlowPara1.sDelayTime");
            lstBasic.Add("AirBlowMainParameter10\\AirBlowPara1.sTime");
            lstBasic.Add("AirBlowMainParameter11\\AirBlowPara1.sChkBit");
            lstBasic.Add("AirBlowMainParameter11\\AirBlowPara1.sMode");
            lstBasic.Add("AirBlowMainParameter11\\AirBlowPara1.sStartCondition");
            lstBasic.Add("AirBlowMainParameter11\\AirBlowPara1.sDelayTime");
            lstBasic.Add("AirBlowMainParameter11\\AirBlowPara1.sTime");
            lstBasic.Add("AirBlowMainParameter12\\AirBlowPara1.sChkBit");
            lstBasic.Add("AirBlowMainParameter12\\AirBlowPara1.sMode");
            lstBasic.Add("AirBlowMainParameter12\\AirBlowPara1.sStartCondition");
            lstBasic.Add("AirBlowMainParameter12\\AirBlowPara1.sDelayTime");
            lstBasic.Add("AirBlowMainParameter12\\AirBlowPara1.sTime");
            lstBasic.Add("ValveGateControl1.sValveUse");
            lstBasic.Add("ValveGateControl1.OpenMode");
            lstBasic.Add("ValveGateControl1.OpenDelay");
            lstBasic.Add("ValveGateControl1.OpenPos");
            lstBasic.Add("ValveGateControl1.OpenPressure");
            lstBasic.Add("ValveGateControl1.CloseMode");
            lstBasic.Add("ValveGateControl1.CloseDelay");
            lstBasic.Add("ValveGateControl1.ClosePosition");
            lstBasic.Add("ValveGateControl2.sValveUse");
            lstBasic.Add("ValveGateControl2.OpenMode");
            lstBasic.Add("ValveGateControl2.OpenDelay");
            lstBasic.Add("ValveGateControl2.OpenPos");
            lstBasic.Add("ValveGateControl2.OpenPressure");
            lstBasic.Add("ValveGateControl2.CloseMode");
            lstBasic.Add("ValveGateControl2.CloseDelay");
            lstBasic.Add("ValveGateControl2.ClosePosition");
            lstBasic.Add("ValveGateControl3.sValveUse");
            lstBasic.Add("ValveGateControl3.OpenMode");
            lstBasic.Add("ValveGateControl3.OpenDelay");
            lstBasic.Add("ValveGateControl3.OpenPos");
            lstBasic.Add("ValveGateControl3.OpenPressure");
            lstBasic.Add("ValveGateControl3.CloseMode");
            lstBasic.Add("ValveGateControl3.CloseDelay");
            lstBasic.Add("ValveGateControl3.ClosePosition");
            lstBasic.Add("ValveGateControl4.sValveUse");
            lstBasic.Add("ValveGateControl4.OpenMode");
            lstBasic.Add("ValveGateControl4.OpenDelay");
            lstBasic.Add("ValveGateControl4.OpenPos");
            lstBasic.Add("ValveGateControl4.OpenPressure");
            lstBasic.Add("ValveGateControl4.CloseMode");
            lstBasic.Add("ValveGateControl4.CloseDelay");
            lstBasic.Add("ValveGateControl4.ClosePosition");
            lstBasic.Add("NozzleControl1.sOpenMode");
            lstBasic.Add("NozzleControl1.sOpenDelay");
            lstBasic.Add("NozzleControl1.sCloseMode");
            lstBasic.Add("NozzleControl1.sCloseDelay");
            lstBasic.Add("NozzleControl1.sClosePos");
            lstBasic.Add("NozzleControl1.sForceOpen");
            lstBasic.Add("NozzleControl1.sCheckRet");
            lstBasic.Add("MultiHeatControl16Group1.sTuneTemp");
            lstBasic.Add("MultiHeatControl16Group1.sSaveInMem");
            lstBasic.Add("MultiHeatControl16Group1.ModuleCheckbit");
            lstBasic.Add("MultiHeatControl16Group1.sMoldHeatingSelection");
            lstBasic.Add("MultiHeatControl16Group1.sTempSetAll");
            lstBasic.Add("MultiHeatControl16Group1.sUpoffsetAll");
            lstBasic.Add("MultiHeatControl16Group1.sLowoffsetAll");
            lstBasic.Add("MultiHeatControl16Group1.sStandbyAll");
            lstBasic.Add("MultiHeatControl16Group1.sMaxLimitAll");
            lstBasic.Add("MoldHeatingGlobalSet1.sTempSet");
            lstBasic.Add("MoldHeatingGlobalSet1.sUpOffset");
            lstBasic.Add("MoldHeatingGlobalSet1.sLowOffset");
            lstBasic.Add("MoldHeatingGlobalSet1.sStandby");
            lstBasic.Add("MoldHeatingGlobalSet1.sSwitch");
            lstBasic.Add("MoldHeatingGlobalSet2.sTempSet");
            lstBasic.Add("MoldHeatingGlobalSet2.sUpOffset");
            lstBasic.Add("MoldHeatingGlobalSet2.sLowOffset");
            lstBasic.Add("MoldHeatingGlobalSet2.sStandby");
            lstBasic.Add("MoldHeatingGlobalSet2.sSwitch");
            lstBasic.Add("MoldHeatingGlobalSet3.sTempSet");
            lstBasic.Add("MoldHeatingGlobalSet3.sUpOffset");
            lstBasic.Add("MoldHeatingGlobalSet3.sLowOffset");
            lstBasic.Add("MoldHeatingGlobalSet3.sStandby");
            lstBasic.Add("MoldHeatingGlobalSet3.sSwitch");
            lstBasic.Add("MoldHeatingGlobalSet4.sTempSet");
            lstBasic.Add("MoldHeatingGlobalSet4.sUpOffset");
            lstBasic.Add("MoldHeatingGlobalSet4.sLowOffset");
            lstBasic.Add("MoldHeatingGlobalSet4.sStandby");
            lstBasic.Add("MoldHeatingGlobalSet4.sSwitch");
            lstBasic.Add("MoldHeatingGlobalSet5.sTempSet");
            lstBasic.Add("MoldHeatingGlobalSet5.sUpOffset");
            lstBasic.Add("MoldHeatingGlobalSet5.sLowOffset");
            lstBasic.Add("MoldHeatingGlobalSet5.sStandby");
            lstBasic.Add("MoldHeatingGlobalSet5.sSwitch");
            lstBasic.Add("MoldHeatingGlobalSet6.sTempSet");
            lstBasic.Add("MoldHeatingGlobalSet6.sUpOffset");
            lstBasic.Add("MoldHeatingGlobalSet6.sLowOffset");
            lstBasic.Add("MoldHeatingGlobalSet6.sStandby");
            lstBasic.Add("MoldHeatingGlobalSet6.sSwitch");
            lstBasic.Add("MoldHeatZone1.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone1.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone1\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone1\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone1\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone1\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone2.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone2.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone2\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone2\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone2\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone2\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone3.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone3.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone3\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone3\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone3\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone3\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone4.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone4.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone4\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone4\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone4\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone4\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone5.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone5.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone5\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone5\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone5\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone5\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone6.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone6.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone6\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone6\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone6\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone6\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone7.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone7.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone7\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone7\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone7\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone7\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone8.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone8.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone8\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone8\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone8\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone8\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone9.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone9.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone9\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone9\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone9\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone9\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone10.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone10.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone10\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone10\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone10\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone10\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone11.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone11.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone11\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone11\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone11\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone11\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone12.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone12.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone12\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone12\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone12\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone12\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone13.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone13.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone13\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone13\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone13\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone13\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone14.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone14.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone14\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone14\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone14\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone14\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone15.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone15.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone15\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone15\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone15\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone15\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone16.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone16.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone16\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone16\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone16\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone16\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone17.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone17.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone17\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone17\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone17\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone17\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone18.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone18.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone18\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone18\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone18\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone18\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone19.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone19.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone19\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone19\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone19\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone19\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone20.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone20.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone20\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone20\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone20\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone20\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone21.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone21.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone21\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone21\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone21\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone21\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone22.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone22.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone22\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone22\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone22\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone22\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone23.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone23.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone23\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone23\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone23\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone23\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone24.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone24.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone24\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone24\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone24\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone24\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone25.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone25.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone25\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone25\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone25\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone25\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone26.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone26.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone26\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone26\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone26\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone26\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone27.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone27.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone27\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone27\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone27\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone27\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone28.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone28.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone28\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone28\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone28\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone28\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone29.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone29.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone29\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone29\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone29\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone29\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone30.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone30.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone30\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone30\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone30\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone30\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone31.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone31.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone31\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone31\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone31\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone31\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone32.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone32.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone32\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone32\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone32\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone32\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone33.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone33.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone33\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone33\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone33\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone33\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone34.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone34.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone34\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone34\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone34\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone34\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone35.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone35.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone35\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone35\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone35\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone35\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone36.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone36.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone36\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone36\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone36\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone36\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone37.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone37.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone37\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone37\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone37\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone37\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone38.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone38.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone38\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone38\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone38\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone38\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone39.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone39.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone39\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone39\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone39\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone39\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone40.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone40.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone40\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone40\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone40\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone40\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone41.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone41.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone41\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone41\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone41\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone41\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone42.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone42.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone42\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone42\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone42\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone42\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone43.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone43.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone43\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone43\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone43\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone43\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone44.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone44.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone44\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone44\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone44\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone44\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone45.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone45.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone45\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone45\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone45\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone45\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone46.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone46.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone46\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone46\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone46\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone46\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone47.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone47.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone47\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone47\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone47\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone47\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone48.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone48.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone48\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone48\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone48\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone48\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone49.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone49.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone49\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone49\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone49\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone49\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone50.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone50.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone50\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone50\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone50\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone50\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone51.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone51.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone51\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone51\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone51\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone51\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone52.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone52.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone52\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone52\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone52\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone52\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone53.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone53.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone53\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone53\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone53\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone53\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone54.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone54.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone54\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone54\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone54\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone54\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone55.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone55.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone55\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone55\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone55\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone55\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone56.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone56.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone56\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone56\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone56\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone56\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone57.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone57.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone57\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone57\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone57\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone57\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone58.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone58.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone58\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone58\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone58\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone58\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone59.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone59.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone59\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone59\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone59\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone59\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone60.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone60.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone60\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone60\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone60\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone60\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone61.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone61.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone61\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone61\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone61\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone61\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone62.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone62.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone62\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone62\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone62\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone62\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone63.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone63.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone63\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone63\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone63\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone63\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone64.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone64.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone64\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone64\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone64\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone64\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone65.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone65.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone65\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone65\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone65\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone65\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone66.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone66.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone66\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone66\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone66\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone66\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone67.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone67.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone67\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone67\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone67\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone67\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone68.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone68.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone68\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone68\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone68\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone68\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone69.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone69.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone69\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone69\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone69\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone69\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone70.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone70.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone70\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone70\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone70\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone70\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone71.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone71.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone71\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone71\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone71\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone71\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone72.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone72.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone72\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone72\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone72\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone72\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone73.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone73.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone73\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone73\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone73\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone73\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone74.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone74.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone74\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone74\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone74\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone74\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone75.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone75.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone75\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone75\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone75\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone75\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone76.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone76.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone76\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone76\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone76\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone76\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone77.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone77.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone77\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone77\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone77\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone77\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone78.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone78.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone78\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone78\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone78\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone78\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone79.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone79.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone79\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone79\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone79\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone79\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone80.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone80.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone80\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone80\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone80\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone80\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone81.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone81.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone81\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone81\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone81\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone81\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone82.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone82.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone82\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone82\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone82\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone82\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone83.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone83.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone83\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone83\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone83\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone83\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone84.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone84.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone84\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone84\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone84\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone84\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone85.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone85.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone85\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone85\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone85\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone85\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone86.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone86.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone86\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone86\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone86\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone86\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone87.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone87.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone87\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone87\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone87\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone87\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone88.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone88.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone88\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone88\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone88\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone88\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone89.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone89.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone89\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone89\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone89\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone89\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone90.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone90.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone90\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone90\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone90\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone90\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone91.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone91.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone91\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone91\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone91\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone91\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone92.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone92.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone92\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone92\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone92\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone92\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone93.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone93.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone93\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone93\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone93\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone93\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone94.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone94.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone94\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone94\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone94\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone94\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone95.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone95.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone95\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone95\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone95\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone95\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone96.sHeatingLimitValue");
            lstBasic.Add("MoldHeatZone96.sHeatingZoneSwitch");
            lstBasic.Add("MoldHeatZone96\\Check_Heat_First_Rise.Data");
            lstBasic.Add("MoldHeatZone96\\Kp_First_Rise.Data");
            lstBasic.Add("MoldHeatZone96\\Tn_First_Rise.Data");
            lstBasic.Add("MoldHeatZone96\\Tv_First_Rise.Data");
            lstBasic.Add("MoldHeatZone1.HeatingSet");
            lstBasic.Add("MoldHeatZone1.sHeatingRate");
            lstBasic.Add("MoldHeatZone2.HeatingSet");
            lstBasic.Add("MoldHeatZone2.sHeatingRate");
            lstBasic.Add("MoldHeatZone3.HeatingSet");
            lstBasic.Add("MoldHeatZone3.sHeatingRate");
            lstBasic.Add("MoldHeatZone4.HeatingSet");
            lstBasic.Add("MoldHeatZone4.sHeatingRate");
            lstBasic.Add("MoldHeatZone5.HeatingSet");
            lstBasic.Add("MoldHeatZone5.sHeatingRate");
            lstBasic.Add("MoldHeatZone6.HeatingSet");
            lstBasic.Add("MoldHeatZone6.sHeatingRate");
            lstBasic.Add("MoldHeatZone7.HeatingSet");
            lstBasic.Add("MoldHeatZone7.sHeatingRate");
            lstBasic.Add("MoldHeatZone8.HeatingSet");
            lstBasic.Add("MoldHeatZone8.sHeatingRate");
            lstBasic.Add("MoldHeatZone9.HeatingSet");
            lstBasic.Add("MoldHeatZone9.sHeatingRate");
            lstBasic.Add("MoldHeatZone10.HeatingSet");
            lstBasic.Add("MoldHeatZone10.sHeatingRate");
            lstBasic.Add("MoldHeatZone11.HeatingSet");
            lstBasic.Add("MoldHeatZone11.sHeatingRate");
            lstBasic.Add("MoldHeatZone12.HeatingSet");
            lstBasic.Add("MoldHeatZone12.sHeatingRate");
            lstBasic.Add("MoldHeatZone13.HeatingSet");
            lstBasic.Add("MoldHeatZone13.sHeatingRate");
            lstBasic.Add("MoldHeatZone14.HeatingSet");
            lstBasic.Add("MoldHeatZone14.sHeatingRate");
            lstBasic.Add("MoldHeatZone15.HeatingSet");
            lstBasic.Add("MoldHeatZone15.sHeatingRate");
            lstBasic.Add("MoldHeatZone16.HeatingSet");
            lstBasic.Add("MoldHeatZone16.sHeatingRate");
            lstBasic.Add("MoldHeatZone17.HeatingSet");
            lstBasic.Add("MoldHeatZone17.sHeatingRate");
            lstBasic.Add("MoldHeatZone18.HeatingSet");
            lstBasic.Add("MoldHeatZone18.sHeatingRate");
            lstBasic.Add("MoldHeatZone19.HeatingSet");
            lstBasic.Add("MoldHeatZone19.sHeatingRate");
            lstBasic.Add("MoldHeatZone20.HeatingSet");
            lstBasic.Add("MoldHeatZone20.sHeatingRate");
            lstBasic.Add("MoldHeatZone21.HeatingSet");
            lstBasic.Add("MoldHeatZone21.sHeatingRate");
            lstBasic.Add("MoldHeatZone22.HeatingSet");
            lstBasic.Add("MoldHeatZone22.sHeatingRate");
            lstBasic.Add("MoldHeatZone23.HeatingSet");
            lstBasic.Add("MoldHeatZone23.sHeatingRate");
            lstBasic.Add("MoldHeatZone24.HeatingSet");
            lstBasic.Add("MoldHeatZone24.sHeatingRate");
            lstBasic.Add("MoldHeatZone25.HeatingSet");
            lstBasic.Add("MoldHeatZone25.sHeatingRate");
            lstBasic.Add("MoldHeatZone26.HeatingSet");
            lstBasic.Add("MoldHeatZone26.sHeatingRate");
            lstBasic.Add("MoldHeatZone27.HeatingSet");
            lstBasic.Add("MoldHeatZone27.sHeatingRate");
            lstBasic.Add("MoldHeatZone28.HeatingSet");
            lstBasic.Add("MoldHeatZone28.sHeatingRate");
            lstBasic.Add("MoldHeatZone29.HeatingSet");
            lstBasic.Add("MoldHeatZone29.sHeatingRate");
            lstBasic.Add("MoldHeatZone30.HeatingSet");
            lstBasic.Add("MoldHeatZone30.sHeatingRate");
            lstBasic.Add("MoldHeatZone31.HeatingSet");
            lstBasic.Add("MoldHeatZone31.sHeatingRate");
            lstBasic.Add("MoldHeatZone32.HeatingSet");
            lstBasic.Add("MoldHeatZone32.sHeatingRate");
            lstBasic.Add("MoldHeatZone33.HeatingSet");
            lstBasic.Add("MoldHeatZone33.sHeatingRate");
            lstBasic.Add("MoldHeatZone34.HeatingSet");
            lstBasic.Add("MoldHeatZone34.sHeatingRate");
            lstBasic.Add("MoldHeatZone35.HeatingSet");
            lstBasic.Add("MoldHeatZone35.sHeatingRate");
            lstBasic.Add("MoldHeatZone36.HeatingSet");
            lstBasic.Add("MoldHeatZone36.sHeatingRate");
            lstBasic.Add("MoldHeatZone37.HeatingSet");
            lstBasic.Add("MoldHeatZone37.sHeatingRate");
            lstBasic.Add("MoldHeatZone38.HeatingSet");
            lstBasic.Add("MoldHeatZone38.sHeatingRate");
            lstBasic.Add("MoldHeatZone39.HeatingSet");
            lstBasic.Add("MoldHeatZone39.sHeatingRate");
            lstBasic.Add("MoldHeatZone40.HeatingSet");
            lstBasic.Add("MoldHeatZone40.sHeatingRate");
            lstBasic.Add("MoldHeatZone41.HeatingSet");
            lstBasic.Add("MoldHeatZone41.sHeatingRate");
            lstBasic.Add("MoldHeatZone42.HeatingSet");
            lstBasic.Add("MoldHeatZone42.sHeatingRate");
            lstBasic.Add("MoldHeatZone43.HeatingSet");
            lstBasic.Add("MoldHeatZone43.sHeatingRate");
            lstBasic.Add("MoldHeatZone44.HeatingSet");
            lstBasic.Add("MoldHeatZone44.sHeatingRate");
            lstBasic.Add("MoldHeatZone45.HeatingSet");
            lstBasic.Add("MoldHeatZone45.sHeatingRate");
            lstBasic.Add("MoldHeatZone46.HeatingSet");
            lstBasic.Add("MoldHeatZone46.sHeatingRate");
            lstBasic.Add("MoldHeatZone47.HeatingSet");
            lstBasic.Add("MoldHeatZone47.sHeatingRate");
            lstBasic.Add("MoldHeatZone48.HeatingSet");
            lstBasic.Add("MoldHeatZone48.sHeatingRate");
            lstBasic.Add("MoldHeatZone49.HeatingSet");
            lstBasic.Add("MoldHeatZone49.sHeatingRate");
            lstBasic.Add("MoldHeatZone50.HeatingSet");
            lstBasic.Add("MoldHeatZone50.sHeatingRate");
            lstBasic.Add("MoldHeatZone51.HeatingSet");
            lstBasic.Add("MoldHeatZone51.sHeatingRate");
            lstBasic.Add("MoldHeatZone52.HeatingSet");
            lstBasic.Add("MoldHeatZone52.sHeatingRate");
            lstBasic.Add("MoldHeatZone53.HeatingSet");
            lstBasic.Add("MoldHeatZone53.sHeatingRate");
            lstBasic.Add("MoldHeatZone54.HeatingSet");
            lstBasic.Add("MoldHeatZone54.sHeatingRate");
            lstBasic.Add("MoldHeatZone55.HeatingSet");
            lstBasic.Add("MoldHeatZone55.sHeatingRate");
            lstBasic.Add("MoldHeatZone56.HeatingSet");
            lstBasic.Add("MoldHeatZone56.sHeatingRate");
            lstBasic.Add("MoldHeatZone57.HeatingSet");
            lstBasic.Add("MoldHeatZone57.sHeatingRate");
            lstBasic.Add("MoldHeatZone58.HeatingSet");
            lstBasic.Add("MoldHeatZone58.sHeatingRate");
            lstBasic.Add("MoldHeatZone59.HeatingSet");
            lstBasic.Add("MoldHeatZone59.sHeatingRate");
            lstBasic.Add("MoldHeatZone60.HeatingSet");
            lstBasic.Add("MoldHeatZone60.sHeatingRate");
            lstBasic.Add("MoldHeatZone61.HeatingSet");
            lstBasic.Add("MoldHeatZone61.sHeatingRate");
            lstBasic.Add("MoldHeatZone62.HeatingSet");
            lstBasic.Add("MoldHeatZone62.sHeatingRate");
            lstBasic.Add("MoldHeatZone63.HeatingSet");
            lstBasic.Add("MoldHeatZone63.sHeatingRate");
            lstBasic.Add("MoldHeatZone64.HeatingSet");
            lstBasic.Add("MoldHeatZone64.sHeatingRate");
            lstBasic.Add("MoldHeatZone65.HeatingSet");
            lstBasic.Add("MoldHeatZone65.sHeatingRate");
            lstBasic.Add("MoldHeatZone66.HeatingSet");
            lstBasic.Add("MoldHeatZone66.sHeatingRate");
            lstBasic.Add("MoldHeatZone67.HeatingSet");
            lstBasic.Add("MoldHeatZone67.sHeatingRate");
            lstBasic.Add("MoldHeatZone68.HeatingSet");
            lstBasic.Add("MoldHeatZone68.sHeatingRate");
            lstBasic.Add("MoldHeatZone69.HeatingSet");
            lstBasic.Add("MoldHeatZone69.sHeatingRate");
            lstBasic.Add("MoldHeatZone70.HeatingSet");
            lstBasic.Add("MoldHeatZone70.sHeatingRate");
            lstBasic.Add("MoldHeatZone71.HeatingSet");
            lstBasic.Add("MoldHeatZone71.sHeatingRate");
            lstBasic.Add("MoldHeatZone72.HeatingSet");
            lstBasic.Add("MoldHeatZone72.sHeatingRate");
            lstBasic.Add("MoldHeatZone73.HeatingSet");
            lstBasic.Add("MoldHeatZone73.sHeatingRate");
            lstBasic.Add("MoldHeatZone74.HeatingSet");
            lstBasic.Add("MoldHeatZone74.sHeatingRate");
            lstBasic.Add("MoldHeatZone75.HeatingSet");
            lstBasic.Add("MoldHeatZone75.sHeatingRate");
            lstBasic.Add("MoldHeatZone76.HeatingSet");
            lstBasic.Add("MoldHeatZone76.sHeatingRate");
            lstBasic.Add("MoldHeatZone77.HeatingSet");
            lstBasic.Add("MoldHeatZone77.sHeatingRate");
            lstBasic.Add("MoldHeatZone78.HeatingSet");
            lstBasic.Add("MoldHeatZone78.sHeatingRate");
            lstBasic.Add("MoldHeatZone79.HeatingSet");
            lstBasic.Add("MoldHeatZone79.sHeatingRate");
            lstBasic.Add("MoldHeatZone80.HeatingSet");
            lstBasic.Add("MoldHeatZone80.sHeatingRate");
            lstBasic.Add("MoldHeatZone81.HeatingSet");
            lstBasic.Add("MoldHeatZone81.sHeatingRate");
            lstBasic.Add("MoldHeatZone82.HeatingSet");
            lstBasic.Add("MoldHeatZone82.sHeatingRate");
            lstBasic.Add("MoldHeatZone83.HeatingSet");
            lstBasic.Add("MoldHeatZone83.sHeatingRate");
            lstBasic.Add("MoldHeatZone84.HeatingSet");
            lstBasic.Add("MoldHeatZone84.sHeatingRate");
            lstBasic.Add("MoldHeatZone85.HeatingSet");
            lstBasic.Add("MoldHeatZone85.sHeatingRate");
            lstBasic.Add("MoldHeatZone86.HeatingSet");
            lstBasic.Add("MoldHeatZone86.sHeatingRate");
            lstBasic.Add("MoldHeatZone87.HeatingSet");
            lstBasic.Add("MoldHeatZone87.sHeatingRate");
            lstBasic.Add("MoldHeatZone88.HeatingSet");
            lstBasic.Add("MoldHeatZone88.sHeatingRate");
            lstBasic.Add("MoldHeatZone89.HeatingSet");
            lstBasic.Add("MoldHeatZone89.sHeatingRate");
            lstBasic.Add("MoldHeatZone90.HeatingSet");
            lstBasic.Add("MoldHeatZone90.sHeatingRate");
            lstBasic.Add("MoldHeatZone91.HeatingSet");
            lstBasic.Add("MoldHeatZone91.sHeatingRate");
            lstBasic.Add("MoldHeatZone92.HeatingSet");
            lstBasic.Add("MoldHeatZone92.sHeatingRate");
            lstBasic.Add("MoldHeatZone93.HeatingSet");
            lstBasic.Add("MoldHeatZone93.sHeatingRate");
            lstBasic.Add("MoldHeatZone94.HeatingSet");
            lstBasic.Add("MoldHeatZone94.sHeatingRate");
            lstBasic.Add("MoldHeatZone95.HeatingSet");
            lstBasic.Add("MoldHeatZone95.sHeatingRate");
            lstBasic.Add("MoldHeatZone96.HeatingSet");
            lstBasic.Add("MoldHeatZone96.sHeatingRate");
            lstBasic.Add("ProductCounter1.sTotalPartNumber");
            lstBasic.Add("ProductCounter1.sCavityNumber");
            lstBasic.Add("ProductCounter1.sProductWeight");
            lstBasic.Add("ProductCounter1.sColdRunnerWeight");
            lstBasic.Add("ProductCounter1.sProduceRatio");
            lstBasic.Add("ProductCounter1.sRejectcounter");
            lstBasic.Add("ProductCounter1.sConsecutiveRejectedMoldNumber");
            lstBasic.Add("ProductCounter1.sAlarmFunc");
            lstBasic.Add("ProductCounter1.sBDCheckFlag");
            lstBasic.Add("ProductCounter1.sUpperOffsetInjCompressTime");
            lstBasic.Add("ProductCounter1.sLowerOffsetInjCompressTime");
            lstBasic.Add("ProductCounter1.sUpperOffsetPlasticationTime");
            lstBasic.Add("ProductCounter1.sLowerOffsetPlasticationTime");
            lstBasic.Add("ProductCounter1.sUpperOffsetCycleTime");
            lstBasic.Add("ProductCounter1.sLowerOffsetCycleTime");
            lstBasic.Add("ProductCounter1.sUpperOffsetVPPosition");
            lstBasic.Add("ProductCounter1.sLowerOffsetVPPosition");
            lstBasic.Add("ProductCounter1.sUpperOffsetVPPressure");
            lstBasic.Add("ProductCounter1.sLowerOffsetVPPressure");
            lstBasic.Add("ProductCounter1.sUpperOffsetVPTime");
            lstBasic.Add("ProductCounter1.sLowerOffsetVPTime");
            lstBasic.Add("ProductCounter1.sUpperOffsetInjStartPosition");
            lstBasic.Add("ProductCounter1.sLowerOffsetInjStartPosition");
            lstBasic.Add("ProductCounter1.sUpperOffsetMinCushionPosition");
            lstBasic.Add("ProductCounter1.sLowerOffsetMinCushionPosition");
            lstBasic.Add("ProductCounter1.sUpperOffsetCushionCompletePosition");
            lstBasic.Add("ProductCounter1.sLowerOffsetCushionCompletePosition");
            lstBasic.Add("ProductCounter1.sUpperOffsetInjPeakPressure");
            lstBasic.Add("ProductCounter1.sLowerOffsetInjPeakPressure");
            lstBasic.Add("ProductCounter1.sProductRatio");
            lstBasic.Add("ProductCounter1.sRejectRatio");
            lstBasic.Add("ProductCounter1.sSetCycleTime");
            lstBasic.Add("ProductCounter1.sREC_TempTimer");
            lstBasic.Add("ProductCounter1.sREC_TempNumber");
            lstBasic.Add("ProductCounter1.sREC_TaskTimer");
            lstBasic.Add("ProductCounter1.sPowerCalcCounter");
        }

        void dtShow_Tick(object sender, EventArgs e)
        {
            this.Dispatcher.BeginInvoke(new nullEvent(itemAdd), null);
        }

        void dtLoad_Tick(object sender, EventArgs e)
        {
            this.Dispatcher.BeginInvoke(new nullEvent(btnLoadInvoke), null);
        }
        //int curItemNr = 1;
        FileStream fs = null;
        StreamReader sr = null;
        int fileItemNr = 0;
        public void show(string filename)
        {
            string[] strtmp = filename.Split('.');

            if (strtmp[strtmp.Length - 1] == "mold")
                fileType = "mold";
            else if (strtmp[strtmp.Length - 1] == "inj")
                fileType = "inj";
            else if (strtmp[strtmp.Length - 1] == "sys")
                fileType = "sys";
            else
                return;
            string[] strtmp2 = strtmp[strtmp.Length - 2].Split('\\');

            DateTime dt = DateTime.Now;
            lbNewFilename.Content = strtmp2[strtmp2.Length -1] + "_" + dt.Year.ToString("0000") + dt.Month.ToString("00") + dt.Day.ToString("00") + "." + fileType;
            btnLeft.IsEnabled = true;
            btnRight.IsEnabled = true;
            cvsValueLst.Children.Clear();
            cvsValueLst.Height = 85 * 20;
            Canvas.SetTop(cvsValueLst, 0);
            //pageStartNr = 1;
            curFocusNr = 0;
            pageNr = 0;
            fileItemNr = 0;
            //curItemNr = 1;
            try
            {
                fs = new FileStream(filename, FileMode.Open);
                sr = new StreamReader(fs);
                itemLst.Clear();
                string str = sr.ReadLine();
                while (str != null)
                {
                    itemLst.Add(str);
                    str = sr.ReadLine();
                }
                if (itemLst.Count < 85)
                {
                    btnLeft.Visibility = Visibility.Hidden;
                    btnRight.Visibility = Visibility.Hidden;

                }
                else
                {
                    btnRight.IsEnabled = false;
                    btnLeft.Visibility = Visibility.Visible;
                    btnRight.Visibility = Visibility.Visible;
                    
                }

                pageNr = itemLst.Count / 85 + 1;
                lbCurPage.Content = "1/" + pageNr;
                dtShow.Start();
                progressBarShow.Visibility = Visibility.Visible;
                lbPerShow.Visibility = Visibility.Visible;
                lbPerShow.Content = "0%";
            }
            catch (Exception ex)
            {
                vm.perror("" + ex.ToString());
            }
            finally
            {
                if (fs != null)
                    fs.Dispose();
                if (sr != null)
                    fs.Dispose();
            }
            this.Visibility = Visibility.Visible;
            this.Opacity = 1;
        }
        public void show()
        {
            lbNewFilename.Content = @"D:\Valmo\Config\" + DateTime.Now.ToString("yy_MM_dd") + ".config";
            btnLeft.IsEnabled = true;
            btnRight.IsEnabled = true;
            cvsValueLst.Children.Clear();
            cvsValueLst.Height = 85 * 20;
            Canvas.SetTop(cvsValueLst, 0);
            curFocusNr = 0;
            pageNr = 0;
            fileItemNr = 0;
            try
            {
                itemLst.Clear();

                foreach (string str in lstBasic)
                {
                    itemLst.Add(str);
                }

                if (itemLst.Count < 85)
                {
                    btnLeft.Visibility = Visibility.Hidden;
                    btnRight.Visibility = Visibility.Hidden;

                }
                else
                {
                    btnRight.IsEnabled = false;
                    btnLeft.Visibility = Visibility.Visible;
                    btnRight.Visibility = Visibility.Visible;

                }

                pageNr = itemLst.Count / 85 + 1;
                lbCurPage.Content = "1/" + pageNr;
                dtShow.Start();
                progressBarShow.Visibility = Visibility.Visible;
                lbPerShow.Visibility = Visibility.Visible;
                lbPerShow.Content = "0%";
            }
            catch (Exception ex)
            {
                vm.perror("" + ex.ToString());
            }
            this.Visibility = Visibility.Visible;
            this.Opacity = 1;
        }
        public void hide()
        {
            this.Visibility = Visibility.Hidden;
        }
        void itemAdd()
        {
            if(fileItemNr < itemLst.Count)
            {
                string str = itemLst[fileItemNr];
                string[] strTmp = str.Split('\t');
                if (strTmp.Length == 2)
                {
                    loadFileItemCtrl item = new loadFileItemCtrl((fileItemNr + 1).ToString(), strTmp[0], "--");
                    itemCtrlLst.Add(item);
                    cvsValueLst.Children.Add(item);
                    Canvas.SetTop(item, (cvsValueLst.Children.Count - 1) * 20);
                }
                else
                {
                    loadFileItemCtrl item = new loadFileItemCtrl(cvsValueLst.Children.Count.ToString(), str, "--");
                    itemCtrlLst.Add(item);
                    cvsValueLst.Children.Add(item);
                    Canvas.SetTop(item, (cvsValueLst.Children.Count - 1) * 20);
                }
                
                if (fileItemNr > 85)
                {
                    double newTop = Canvas.GetTop(cvsValueLst) - 20;
                    Canvas.SetTop(cvsValueLst, newTop);
                    lbCurPage.Content = ((int)(newTop / -85 / 20 + 1)).ToString() + "/" + pageNr;
                }
                fileItemNr++;
                progressBarShow.Value = fileItemNr * 100.0 / itemLst.Count;
                lbPerShow.Content = progressBarShow.Value.ToString("00.0") + "%";
            }
            else
            {
                dtShow.Stop();
                progressBarShow.Visibility = Visibility.Hidden;
                lbPerShow.Visibility = Visibility.Hidden;
                lbPerShow.Content = "0%";
                cvsValueLst.Height = fileItemNr * 20;
                Canvas.SetTop(cvsValueLst, 0);
                lbCurPage.Content = "1/" + pageNr;
            }
        }
        private void imgClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            DirectoryInfo dir = new DirectoryInfo("./conf");
            FileInfo[] files = dir.GetFiles();
            bool flagNeedConfirm = false;
            string fileName = lbNewFilename.Content.ToString();
            if (fileName.Substring(fileName.Length - fileType.Length, fileType.Length) != fileType)
            {
                fileName += "." + fileType;
            }
            foreach (FileInfo file in files)
            {
                if (file.Name == fileName)
                {
                    confirmPanel.show(startSaveFunc);
                    flagNeedConfirm = true;
                }
            }
            if (!flagNeedConfirm)
            {
                startSaveFunc();
            }

        }
        private void startSaveFunc()
        {
            Canvas.SetTop(cvsValueLst, 0);
            lbCurPage.Content = "1/" + pageNr;
            curFocusNr = 0;
            loadNrCount = 0;
            dtLoad.Start();
        }

        //int curStep = 0;
        private void btnLoadInvoke()
        {
            try
            {
                //if (curStep == 0)
                //{
                //    if (fileType == "mold")
                //        moldFileHeadCheck();
                //    else if (fileType == "inj")
                //        injFileHeadCheck();
                //    else if (fileType == "sys")
                //        sysFileHeadCheck();
                //    curStep = 1;
                //}
                //else if (curStep == 1)
                //{
                    loadParams();
                //}
                //else if (curStep == 2)
                //{
                //    if (fileType == "mold")
                //        moldFileTailCheck();
                //    else if (fileType == "inj")
                //        injFileTailCheck();
                //    else if (fileType == "sys")
                //        sysFileTailCheck();

                //    dtLoad.Stop();
                //    MessageBox.Show("Load " + loadNrCount + " values Over!");
                //    loadNrCount = 0;
                //}
            }
            catch (Exception ex)
            {
                vm.perror("btnLoadInvoke " + ex.ToString());
            }

        }

        //int curLstNr = 0;
        int curFocusNr = 0;
        loadFileItemCtrl curItem = null;
        int loadNrCount = 0;


        private void loadParams()
        {
            try
            {
                int addr_int = 0;
                if (curFocusNr < itemLst.Count)
                {
                    if (curItem != null)
                        curItem.focusState = false;
                    curItem = cvsValueLst.Children[curFocusNr] as loadFileItemCtrl;

                   curItem.focusState = true;
                   string[] strtmp = curItem.addr.Split('.');

                    if (strtmp.Length == 2)
                    {
                        addr_int = -1;
                        if (!LinkMgr.getObjPlcAddr(curItem.addr, ref addr_int, strtmp[0]))
                        {
                            curItem.flagLoadOk = false;
                            throw new Exception("get " + curItem.addr + " addr_int error!");
                        }
                        if (addr_int == -1 || addr_int == 0)
                        {
                            curItem.flagLoadOk = false;
                        }
                        int newValue = -1;
                        if (Lasal32.LslReadFromSvr(addr_int,ref newValue))
                        {
                            curItem.value = newValue.ToString();
                            curItem.flagLoadOk = true;
                            loadNrCount++;
                        }
                    }
                    //curLstNr++;
                    curFocusNr++;
                    if (curFocusNr > 85)
                    {
                        double newTop = Canvas.GetTop(cvsValueLst) - 20;
                        Canvas.SetTop(cvsValueLst, newTop);
                        lbCurPage.Content = ((int)(newTop / -85 / 20 + 1)).ToString() + "/" + pageNr;
                    }
                }
                else
                {
                    if (curItem != null)
                        curItem.focusState = false;
                    curItem = null;
                    curFocusNr = 0;
                    dtLoad.Stop();
                    string fileName = lbNewFilename.Content.ToString();

                    FileStream fsNew = null;
                    //if (valmoWin.sUsbPath != null)
                    //{
                    //    if (!Directory.Exists(valmoWin.sUsbPath.FullName + "\\conf"))
                    //        Directory.CreateDirectory(valmoWin.sUsbPath.FullName + "\\conf");
                    //    fsNew = new FileStream(valmoWin.sUsbPath.FullName + "\\conf" + fileName, FileMode.OpenOrCreate);

                    //}
                    //else
                    fsNew = new FileStream(fileName, FileMode.Create);
                    if (fsNew != null)
                    {
                        StreamWriter sw = new StreamWriter(fsNew);
                        foreach (loadFileItemCtrl item in cvsValueLst.Children)
                        {
                            sw.WriteLine(item.addr + "\t" + item.value);
                        }
                        sw.Close();
                        fsNew.Close();
                        MessageBox.Show("Save OK! Save " + loadNrCount + "values");
                        Canvas.SetTop(cvsValueLst, 0);
                        lbCurPage.Content = "1/" + pageNr;
                    }
                }
            }
            catch (Exception ex)
            {
                if (curItem != null)
                    curItem.focusState = false;
                //curLstNr = 0;
                //curItem = null;
                dtLoad.Stop();
                //pageStartNr = 1;
                //pageEndNr = 0;
                curFocusNr = 0;
                string fileName = lbNewFilename.Content.ToString();
                if (fileName.Substring(fileName.Length - fileType.Length, fileType.Length) != fileType)
                {
                    fileName += "." + fileType;
                }
                FileStream fsNew = new FileStream("./conf/" + fileName, FileMode.OpenOrCreate);
                StreamWriter sw = new StreamWriter(fsNew);
                foreach (loadFileItemCtrl item in cvsValueLst.Children)
                {
                    sw.WriteLine(item.addr + "\t" + item.value);
                }
                sw.Close();
                fsNew.Close();
                MessageBox.Show("Save error! Save " + loadNrCount + "values");
                Canvas.SetTop(cvsValueLst, 0);
                lbCurPage.Content = "1/" + pageNr;
                loadNrCount = 0;
                vm.perror("[saveConfFileCtrl.loadParams]" + ex.ToString());
            }
            finally
            {

            }
        }

        private void moldFileHeadCheck()
        {
            int addr_int = 0;
            int value = 1;
            if (!LinkMgr.getObjPlcAddr("MoldScrewFile1.sLoadMoldConfig", ref addr_int, "MoldScrewFile1"))
            {
                throw new Exception("get MoldScrewFile1.sLoadMoldConfig  addr_int error!");
            }
            if (Lasal32.LslWriteToSvr(addr_int, value))
            {
                int tmp = 2;
                if (Lasal32.LslReadFromSvr(addr_int, ref tmp))
                {
                    if (tmp == -1)
                    {
                        throw new Exception("write MoldScrewFile1.sLoadMoldConfig 1 error!");
                    }
                }
                else
                {
                    throw new Exception("get MoldScrewFile1.sLoadMoldConfig  value error!");
                }
            }
        }
        private void moldFileTailCheck()
        {
            int addr_int = 0;
            int value = 0;
            if (!LinkMgr.getObjPlcAddr("MoldScrewFile1.sLoadMoldConfig", ref addr_int, "MoldScrewFile1"))
            {
                throw new Exception("get MoldScrewFile1.sLoadMoldConfig  addr_int error!");
            }
            if (Lasal32.LslWriteToSvr(addr_int, value))
            {
                int tmp = 2;
                if (Lasal32.LslReadFromSvr(addr_int, ref tmp))
                {
                    if (tmp == -1)
                    {
                        throw new Exception("write MoldScrewFile1.sLoadMoldConfig 1 error!");
                    }
                }
                else
                {
                    throw new Exception("get MoldScrewFile1.sLoadMoldConfig  value error!");
                }
            }

            if (!LinkMgr.getObjPlcAddr("RamSourceFileMoldLoaded.Data", ref addr_int, "RamSourceFileMoldLoaded"))
            {
                throw new Exception("get RamSourceFileMoldLoaded.Data  addr_int error!");
            }
            if (Lasal32.LslWriteToSvr(addr_int, 1))
            {
                int tmp = 2;
                if (Lasal32.LslReadFromSvr(addr_int, ref tmp))
                {
                    if (tmp == 1)
                    {
                        //bdMoldConfig.Background = Brushes.Blue;
                    }
                    else
                    {
                        throw new Exception("write MoldScrewFile1.sLoadMoldConfig 1 error! return value is " + tmp);
                    }
                }
                else
                {
                    throw new Exception("get MoldScrewFile1.sLoadMoldConfig  value error!");
                }
                //curStep = 3;
            }
            else
            {
                throw new Exception("write " + "MoldScrewFile1.sLoadMoldConfig 1 error!");
            }
        }

        private void injFileHeadCheck()
        {
            int addr_int = -1;
            int value = 1;
            if (!LinkMgr.getObjPlcAddr("MoldScrewFile1.sLoadInjeConfig", ref addr_int, "MoldScrewFile1"))
            {
                throw new Exception("get MoldScrewFile1.sLoadInjeConfig  addr_int error!");
            }
            if (Lasal32.LslWriteToSvr(addr_int, value))
            {
                int tmp = 2;
                if (Lasal32.LslReadFromSvr(addr_int, ref tmp))
                {
                    if (tmp == -1)
                    {
                        throw new Exception("write MoldScrewFile1.sLoadInjeConfig 1 error!");
                    }
                }
                else
                {
                    throw new Exception("get MoldScrewFile1.sLoadInjeConfig  value error!");
                }
            }
            else
            {
                throw new Exception("write " + "MoldScrewFile1.sLoadInjeConfig 1 error!");

            }
        }
        private void injFileTailCheck()
        {
            int addr_int = -1;
            int value = 0;
            if (!LinkMgr.getObjPlcAddr("MoldScrewFile1.sLoadInjeConfig", ref addr_int, "MoldScrewFile1"))
            {
                throw new Exception("get MoldScrewFile1.sLoadInjeConfig  addr_int error!");
            }
            if (Lasal32.LslWriteToSvr(addr_int, value))
            {
                int tmp = 2;
                if (Lasal32.LslReadFromSvr(addr_int, ref tmp))
                {
                    if (tmp == -1)
                    {
                        throw new Exception("write MoldScrewFile1.sLoadInjeConfig 1 error!");
                    }
                }
                else
                {
                    throw new Exception("get MoldScrewFile1.sLoadInjeConfig  value error!");
                }
            }
            else
            {
                throw new Exception("write " + "MoldScrewFile1.sLoadInjeConfig 1 error!");

            }

            if (!LinkMgr.getObjPlcAddr("RamSourceFileInjection.Data", ref addr_int, "RamSourceFileInjection"))
            {
                throw new Exception("get RamSourceFileInjection.Data  addr_int error!");
            }
            if (Lasal32.LslWriteToSvr(addr_int, 1))
            {
                int tmp = 2;
                if (Lasal32.LslReadFromSvr(addr_int, ref tmp))
                {
                    if (tmp == 1)
                    {
                        //bdInjConfig.Background = Brushes.Blue;
                    }
                    else
                    {
                        throw new Exception("write RamSourceFileInjection.Data 1 error! return value is " + tmp);
                    }

                }
                else
                {
                    throw new Exception("get RamSourceFileInjection.Data  value error!");
                }
            }
            else
            {
                throw new Exception("write " + "RamSourceFileInjection.Data 1 error!");

            }
        }
        private void sysFileHeadCheck()
        {
            int addr_int = -1;
            int value = 1;
            if (!LinkMgr.getObjPlcAddr("MoldScrewFile1.sLoadSystemConfig", ref addr_int, "MoldScrewFile1"))
            {
                throw new Exception("get MoldScrewFile1.sLoadSystemConfig  addr_int error!");
            }
            if (Lasal32.LslWriteToSvr(addr_int, value))
            {
                int tmp = 2;
                if (Lasal32.LslReadFromSvr(addr_int, ref tmp))
                {
                    if (tmp == -1)
                    {
                        throw new Exception("write MoldScrewFile1.sLoadSystemConfig 1 error!");
                    }
                }
                else
                {
                    throw new Exception("get MoldScrewFile1.sLoadSystemConfig  value error!");
                }
            }
            else
            {
                throw new Exception("write " + "MoldScrewFile1.sLoadSystemConfig 1 error!");

            }
        }
        private void sysFileTailCheck()
        {
            int addr_int = -1;
            int value = 0;
            if (!LinkMgr.getObjPlcAddr("MoldScrewFile1.sLoadSystemConfig", ref addr_int, "MoldScrewFile1"))
            {
                throw new Exception("get MoldScrewFile1.sLoadSystemConfig  addr_int error!");
            }
            if (Lasal32.LslWriteToSvr(addr_int, value))
            {
                int tmp = 2;
                if (Lasal32.LslReadFromSvr(addr_int, ref tmp))
                {
                    if (tmp == -1)
                    {
                        throw new Exception("write MoldScrewFile1.sLoadSystemConfig 1 error!");
                    }
                }
                else
                {
                    throw new Exception("get MoldScrewFile1.sLoadSystemConfig  value error!");
                }
            }
            else
            {
                throw new Exception("write " + "MoldScrewFile1.sLoadSystemConfig 1 error!");

            }

            if (!LinkMgr.getObjPlcAddr("CheckLoadRam.Data", ref addr_int, "CheckLoadRam"))
            {
                throw new Exception("get CheckLoadRam.Data  addr_int error!");
            }
            if (Lasal32.LslWriteToSvr(addr_int, 1))
            {
                int tmp = 2;
                if (Lasal32.LslReadFromSvr(addr_int, ref tmp))
                {
                    if (tmp == 1)
                    {
                        //bdSysConfig.Background = Brushes.Blue;
                    }
                    else
                    {
                        throw new Exception("write CheckLoadRam.Data 1 error! return value is " + tmp);
                    }

                }
                else
                {
                    throw new Exception("get CheckLoadRam.Data  value error!");
                }
            }
            else
            {
                throw new Exception("write " + "CheckLoadRam.Data 1 error!");

            }
        }
        private void btnLeft_Click(object sender, RoutedEventArgs e)
        {
            double top = Canvas.GetTop(cvsValueLst);
            //if(top < 
            //if (pageEndNr - 85 > 0)
            //{
            //    pageEndNr -= 85;
            //}
            
            Canvas.SetTop(cvsValueLst, top - 85 * 20);
            top = Canvas.GetTop(cvsValueLst);
            if (top - 85 * 20 - 1 < -cvsValueLst.Height)
                btnLeft.IsEnabled = false;
            else
                btnLeft.IsEnabled = true;
            if (top + 1 > 0)
                btnRight.IsEnabled = false;
            else
                btnRight.IsEnabled = true;
            lbCurPage.Content = ((int)(top / -85 / 20 + 1)).ToString() + "/" + pageNr;
        }

        private void btnRight_Click(object sender, RoutedEventArgs e)
        {
            double top = Canvas.GetTop(cvsValueLst);

            Canvas.SetTop(cvsValueLst, top + 85 * 20);
            top = Canvas.GetTop(cvsValueLst);
            if (top - 85 * 20 - 1 < -cvsValueLst.Height)
                btnLeft.IsEnabled = false;
            else
                btnLeft.IsEnabled = true;
            if (top + 1 > 0)
                btnRight.IsEnabled = false;
            else
                btnRight.IsEnabled = true;
            lbCurPage.Content = ((int)(top / -85 / 20 + 1)).ToString() + "/" + pageNr;
        }

        private void lbNewFilename_MouseDown(object sender, MouseButtonEventArgs e)
        {
            valmoWin.SCharKeyPanel.dis = valmoWin.dv.getCurDis("lanKey1023");
            valmoWin.SCharKeyPanel.init(false, lbNewFilename.Content.ToString(), saveMoldFileFunc, null, new Thickness(0, 800, 0, 0), true);

            //savePanel.confirmHandle = saveMoldFileFunc;
            //savePanel.show("injConf" + lbNewFilename.Content.ToString());
        }
        private void saveMoldFileFunc(string fileName)
        {
            lbNewFilename.Content = fileName;
        }

        private void disposeFunc()
        {

        }
    }
}
