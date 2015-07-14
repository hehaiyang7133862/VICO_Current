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
using System.IO;
using nsDataMgr;
using System.Xml;
namespace nsVicoClient.ctrls
{
    public delegate void saveFileEvent(string pathAndName);
    /// <summary>
    /// Interaction logic for iptCtrl.xaml
    /// </summary>
    public partial class iprCtrl : UserControl
    {
        public static iprUnitObj curUnit = new iprUnitObj();
        public delegate void reDrawCtrlsEvent();

        public static saveFileEvent saveFileHandle;
        public static reDrawCtrlsEvent reDrawCtrlsHandle;
        public iprCtrl()
        {
            vm.testInitTmStart("\t\t\t[iprCtrl]");
            try
            {
                InitializeComponent();

            }
            catch (Exception ex)
            {
                vm.perror("[iprCtrl] " + ex.Message);
            }
            saveFileHandle = new saveFileEvent(saveFileFunc);
            reDrawCtrlsHandle = new reDrawCtrlsEvent(reDrawCtrlsFunc);
            vm.testInitTmEnd("\t\t\t[iprCtrl]");
            init();
        }
        public void init()
        {
            valmoWin.dv.IprPr[80].addHandle(handleIprPr_80);
            valmoWin.dv.IprPr[81].addHandle(handleIprPr_81);
            valmoWin.dv.IprPr[82].addHandle(handleIprPr_82);
            valmoWin.dv.IprPr[83].addHandle(handleIprPr_83);
            valmoWin.dv.IprPr[84].addHandle(handleIprPr_84);
            valmoWin.dv.IprPr[85].addHandle(handleIprPr_85);
            valmoWin.dv.IprPr[86].addHandle(handleIprPr_86);
        }
        private void handleIprPr_80(objUnit obj)
        {
            iprUnitCtrl1.setRunState(obj.value);
        }
        private void handleIprPr_81(objUnit obj)
        {
            iprUnitCtrl2.setRunState(obj.value);
        }
        private void handleIprPr_82(objUnit obj)
        {
            iprUnitCtrl3.setRunState(obj.value);
        }
        private void handleIprPr_83(objUnit obj)
        {
            iprUnitCtrl4.setRunState(obj.value);
        }
        private void handleIprPr_84(objUnit obj)
        {
            iprUnitCtrl5.setRunState(obj.value);
        }
        private void handleIprPr_85(objUnit obj)
        {
            iprUnitCtrl6.setRunState(obj.value);
        }
        private void handleIprPr_86(objUnit obj)
        {
            iprUnitCtrl7.setRunState(obj.value);
        }
        private void reDrawCtrlsFunc()
        {
            double leftWidth = 0;
            double width1 = iprUnitCtrl1.width;
            double width2 = iprUnitCtrl2.width;
            double width3 = iprUnitCtrl3.width;
            double width4 = iprUnitCtrl4.width;
            double width5 = iprUnitCtrl5.width;
            double width6 = iprUnitCtrl6.width;
            double width7 = iprUnitCtrl7.width;
            Canvas.SetLeft(iprUnitCtrl1, leftWidth);
            Canvas.SetLeft(iprUnitCtrl2, width1 + leftWidth);
            Canvas.SetLeft(iprUnitCtrl3, width1 + width2 + leftWidth);
            Canvas.SetLeft(iprUnitCtrl4, width1 + width2 + width3 + leftWidth);
            Canvas.SetLeft(iprUnitCtrl5, width1 + width2 + width3 + width4 + leftWidth);
            Canvas.SetLeft(iprUnitCtrl6, width1 + width2 + width3 + width4 + width5 + leftWidth);
            Canvas.SetLeft(iprUnitCtrl7, width1 + width2 + width3 + width4 + width5 + width6 + leftWidth);

            cvsGrp.Width = width1 + width2 + width3 + width4 + width5 + width6 + width7 + leftWidth * 2;

            lbBack.Width = cvsGrp.Width + 2160;
            lbBack2.Width = cvsGrp.Width + 2160;

            double left = Canvas.GetLeft(cvsGrp);
            if ( left < gdMain.Width - cvsGrp.Width - 100)
                left = gdMain.Width - cvsGrp.Width - 100;
            Canvas.SetLeft(cvsGrp, left);
        }
        public void refreshCtrlWidth()
        {
            iprUnitCtrl1.refreshCtrlWidth();
            iprUnitCtrl2.refreshCtrlWidth();
            iprUnitCtrl3.refreshCtrlWidth();
            iprUnitCtrl4.refreshCtrlWidth();
            iprUnitCtrl5.refreshCtrlWidth();
            iprUnitCtrl6.refreshCtrlWidth();
            iprUnitCtrl7.refreshCtrlWidth();
            iprUnitCtrl1.setFocusPos(1, 0);
            reDrawCtrlsFunc();
        }
        public void setCloseState()
        {
            iprUnitCtrl1.setCloseState();
            iprUnitCtrl2.setCloseState();
            iprUnitCtrl3.setCloseState();
            iprUnitCtrl4.setCloseState();
            iprUnitCtrl5.setCloseState();
            iprUnitCtrl6.setCloseState();
            iprUnitCtrl7.setCloseState();

        }
        public void showFocusMsg()
        {
            iprUnitCtrl1.removefocus();
            iprUnitCtrl2.removefocus();
            iprUnitCtrl3.removefocus();
            iprUnitCtrl4.removefocus();
            iprUnitCtrl5.removefocus();
            iprUnitCtrl6.removefocus();
            iprUnitCtrl7.removefocus();

            switch (iprCtrl.curUnit.sPos & 0xff)
            {
                case 1: iprUnitCtrl1.showFocusMsg(); break;
                case 2: iprUnitCtrl2.showFocusMsg(); break;
                case 3: iprUnitCtrl3.showFocusMsg(); break;
                case 4: iprUnitCtrl4.showFocusMsg(); break;
                case 5: iprUnitCtrl5.showFocusMsg(); break;
                case 6: iprUnitCtrl6.showFocusMsg(); break;
                case 7: iprUnitCtrl7.showFocusMsg(); break;
            }
        }
        public void saveFileFunc(string pathAndName)
        {
            try
            {
                if (File.Exists(pathAndName))
                    File.Delete(pathAndName);
                FileStream fsSave = new FileStream(pathAndName, FileMode.OpenOrCreate);
                vm.printLn("save Ipr File start !");
                save(fsSave);
                vm.printLn("save Ipr File Ok !");
                fsSave.Close();
            }
            catch (Exception ex)
            {
                vm.perror(ex.ToString());
            }
        }
        public  void save(FileStream fs)
        {
            iprUnitCtrl1.save(fs);
            iprUnitCtrl2.save(fs);
            iprUnitCtrl3.save(fs);
            iprUnitCtrl4.save(fs);
            iprUnitCtrl5.save(fs);
            iprUnitCtrl6.save(fs);
            iprUnitCtrl7.save(fs);
        }
        public void save(XmlTextWriter writer)
        {
            iprUnitCtrl1.save(writer);
            iprUnitCtrl2.save(writer);
            iprUnitCtrl3.save(writer);
            iprUnitCtrl4.save(writer);
            iprUnitCtrl5.save(writer);
            iprUnitCtrl6.save(writer);
            iprUnitCtrl7.save(writer);
        }
        public void save(XmlNode xn,XmlDocument xmlDoc)
        {
            iprUnitCtrl1.save(xn, xmlDoc);
            iprUnitCtrl2.save(xn, xmlDoc);
            iprUnitCtrl3.save(xn, xmlDoc);
            iprUnitCtrl4.save(xn, xmlDoc);
            iprUnitCtrl5.save(xn, xmlDoc);
            iprUnitCtrl6.save(xn, xmlDoc);
            iprUnitCtrl7.save(xn, xmlDoc);
        }
        public void saveUnit(FileStream fs, int nr)
        {
            switch (nr / 30)
            {
                case 0:
                    iprUnitCtrl1.saveUnit(fs, nr % 30);
                    break;
                case 1:
                    iprUnitCtrl2.saveUnit(fs, nr % 30);
                    break;
                case 2:
                    iprUnitCtrl3.saveUnit(fs, nr % 30);
                    break;
                case 3:
                    iprUnitCtrl4.saveUnit(fs, nr % 30);
                    break;
                case 4:
                    iprUnitCtrl5.saveUnit(fs, nr % 30);
                    break;
                case 5:
                    iprUnitCtrl6.saveUnit(fs, nr % 30);
                    break;
                case 6:
                    iprUnitCtrl7.saveUnit(fs, nr % 30);
                    break;
            }
        }
        public iprUnitObj getUnitObj(int nr)
        {
            iprUnitObj retUnitObj = null;
            switch (nr / 30)
            {
                case 0:
                    retUnitObj = iprUnitCtrl1.getUnitObj(nr % 30);
                    break;
                case 1:
                    retUnitObj = iprUnitCtrl2.getUnitObj(nr % 30);
                    break;
                case 2:
                    retUnitObj = iprUnitCtrl3.getUnitObj(nr % 30);
                    break;
                case 3:
                    retUnitObj = iprUnitCtrl4.getUnitObj(nr % 30);
                    break;
                case 4:
                    retUnitObj = iprUnitCtrl5.getUnitObj(nr % 30);
                    break;
                case 5:
                    retUnitObj = iprUnitCtrl6.getUnitObj(nr % 30);
                    break;
                case 6:
                    retUnitObj = iprUnitCtrl7.getUnitObj(nr % 30);
                    break;
            }
            return retUnitObj;
        }
        public void cleanMsg()
        {
            iprUnitCtrl1.cleanMsg();
            iprUnitCtrl2.cleanMsg();
            iprUnitCtrl3.cleanMsg();
            iprUnitCtrl4.cleanMsg();
            iprUnitCtrl5.cleanMsg();
            iprUnitCtrl6.cleanMsg();
            iprUnitCtrl7.cleanMsg();
        }

        public static bool isMouseDown = false;
        public static bool isFocusMouseDown = false;
        Point mousePoint;
        //object mouseCtrl = null;
        private void cvsGrp_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = true;
            mousePoint = e.GetPosition(this.gdMain);
            //mouseCtrl = cvsGrp;
            vm.printLn(Canvas.GetLeft(cvsGrp).ToString());
        }

        private void gdMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isMouseDown)
            {
                isMouseDown = false;
            }
            isFocusMouseDown = false;
        }

        private void gdMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown == true && !isFocusMouseDown)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point theMousePoint = e.GetPosition(this.gdMain);
                    double tmpLeft = Canvas.GetLeft(cvsGrp);
                    double left = tmpLeft + theMousePoint.X - mousePoint.X;
                    if (left > 0)
                        left = 0;
                    else if (left < gdMain.Width - cvsGrp.Width - 100)
                        left = gdMain.Width - cvsGrp.Width - 100;
                    Canvas.SetLeft(cvsGrp, left);
                    mousePoint = theMousePoint;
                }
            }

        }

        public void refreshLan()
        {
            iprUnitCtrl1.refreshLan();
            iprUnitCtrl2.refreshLan();
            iprUnitCtrl3.refreshLan();
            iprUnitCtrl4.refreshLan();
            iprUnitCtrl5.refreshLan();
            iprUnitCtrl6.refreshLan();
            iprUnitCtrl7.refreshLan();

        }
    }
    public class iprUnitObj
    {
        public objUnit objValueA = valmoWin.dv.IprPr[39];
        public objUnit objValueB = valmoWin.dv.IprPr[40];
        public objUnit objValueC = valmoWin.dv.IprPr[41];
        public objUnit objValueD = valmoWin.dv.IprPr[67];


        public int get_sPos()
        {
            sPos = valmoWin.dv.IprPr[17].valueNew;
            return sPos;
        }
        public int get_sActName()
        {
            sActName = valmoWin.dv.IprPr[36].valueNew;
            return sActName;
        }
        public int get_sLinkNode()
        {
            sLinkNode = valmoWin.dv.IprPr[28].valueNew;
            return sLinkNode;
        }
        public int get_sValueA()
        {
            sValueA = valmoWin.dv.IprPr[39].valueNew;
            return sValueA;
        }
        //public objUnit objValueA
        //{
        //    get
        //    {
        //        return valmoWin.dv.iprPr[39];
        //    }
        //    set
        //    {
        //        valmoWin.dv.iprPr[39] = value;
        //    }
        //}
        public void get_sValueAObj()
        {
            UnitType type = get_sValueAUnit();
            valmoWin.dv.IprPr[45].unitType = type;
            valmoWin.dv.IprPr[46].unitType = type;
            objValueA.unitType = type;
            get_sValueA();
        }
        public void get_sValueBObj()
        {
            UnitType type = (UnitType)get_sValueBUnit();
            valmoWin.dv.IprPr[48].unitType = type;
            valmoWin.dv.IprPr[49].unitType = type;
            objValueB.unitType = type;
            get_sValueB();
        }
        public void get_sValueCObj()
        {
            UnitType type = (UnitType)get_sValueCUnit();
            valmoWin.dv.IprPr[51].unitType = type;
            valmoWin.dv.IprPr[51].unitType = type;
            objValueC.unitType = type;
            get_sValueC();
        }
        public void get_sValueDObj()
        {
            UnitType type = (UnitType)get_sValueDUnit();
            valmoWin.dv.IprPr[62].unitType = type;
            valmoWin.dv.IprPr[63].unitType = type;
            objValueD.unitType = type;
            get_sValueD();
        }
        public int get_sValueB()
        {
            sValueB = valmoWin.dv.IprPr[40].valueNew;
            return sValueB;
        }
        public int get_sValueC()
        {
            sValueC = valmoWin.dv.IprPr[41].valueNew;
            return sValueC;
        }
        public int get_sNotReady()
        {
            sNotReady = valmoWin.dv.IprPr[43].valueNew;
            return sNotReady;
        }
        public int get_sFuncSelect()
        {
            sFuncSelect = valmoWin.dv.IprPr[37].valueNew;
            return sFuncSelect;
                 
        }
        public int get_sValueD()
        {
            sValueD = valmoWin.dv.IprPr[67].valueNew;
            return sValueD;
        }
        public int get_sValueE()
        {
            sValueE = valmoWin.dv.IprPr[68].valueNew;
            return sValueE;
        }
        public int get_sValueF()
        {
            sValueF = valmoWin.dv.IprPr[69].valueNew;
            return sValueF;
        }
        public int get_sOperateType()
        {
            sOperateType = valmoWin.dv.IprPr[38].valueNew;
            return sOperateType;
        }
        public int get_sMinLimitA()
        {
            sMinLimitA = valmoWin.dv.IprPr[45].valueNew;
            return sMinLimitA;
        }
        public int get_sMaxLimitA()
        {
            sMaxLimitA = valmoWin.dv.IprPr[46].valueNew;
            return sMaxLimitA;
        }
        public UnitType get_sValueAUnit()
        {
            sValueAUnit = valmoWin.dv.IprPr[47].valueNew;
            return getUnit(sValueAUnit);
        }
        public string getStr_sValueAUnit()
        {
            string str_ret = "";
            switch (valmoWin.dv.IprPr[47].valueNew)
            {
                case 0:
                    str_ret = "";
                    break;
                case 1:
                    str_ret = "";
                    break;
                case 2:
                    str_ret = objUnit.unitBase[UnitType.Len_mm];// objUnit.unitBase[UnitType.LengthType];//objUnit.unitBase[UnitType.LengthType];
                    break;
                case 3:
                    str_ret = objUnit.unitBase[UnitType.Prs_Mpa];//objUnit.unitBase[UnitType.PressureType];
                    break;
                case 4:
                    str_ret = objUnit.unitBase[UnitType.Tm_s];// valueConvert.TimeUnit;
                    break;
                case 5:
                    str_ret = objUnit.unitBase[UnitType.Per];//objUnit.unitBase[UnitType.PercentageType];
                    break;
                case 6:
                    str_ret = objUnit.unitBase[UnitType.Force_KN];//valueConvert.ForceKNUnit;
                    break;
                case 7:
                    str_ret = objUnit.unitBase[UnitType.ForceTon];//valueConvert.ForceTonsUnit;
                    break;
                case 8:
                    str_ret = objUnit.unitBase[UnitType.Degree];//valueConvert.ForceTonsUnit;
                    break;
            }
            return str_ret;
        }
        public int get_sMinLimitB()
        {
            sMinLimitB = valmoWin.dv.IprPr[48].valueNew;
            return sMinLimitB;
        }
        public int get_sMaxLimitB()
        {
            sMaxLimitB = valmoWin.dv.IprPr[49].valueNew;
            return sMaxLimitB;
        }
        public UnitType get_sValueBUnit()
        {
            sValueBUnit = valmoWin.dv.IprPr[50].valueNew;
            return getUnit(sValueBUnit);
        }
        public int get_sMinLimitC()
        {
            sMinLimitC = valmoWin.dv.IprPr[51].valueNew;
            return sMinLimitC;
        }
        public int get_sMaxLimitC()
        {
            sMaxLimitC = valmoWin.dv.IprPr[52].valueNew;
            return sMaxLimitC;
        }
        public UnitType get_sValueCUnit()
        {
            sValueCUnit = valmoWin.dv.IprPr[53].valueNew;
            return getUnit(sValueCUnit);
        }
        public int get_sMinLimitD()
        {
            sMinLimitD = valmoWin.dv.IprPr[62].valueNew;
            return sMinLimitD;
        }
        public int get_sMaxLimitD()
        {
            sMaxLimitD = valmoWin.dv.IprPr[63].valueNew;
            return sMaxLimitD;
        }
        public UnitType get_sValueDUnit()
        {
            sValueDUnit = valmoWin.dv.IprPr[64].valueNew;
            return getUnit(sValueDUnit);
        }
        public int get_sMinLimitE()
        {
            sMinLimitE = valmoWin.dv.IprPr[22].valueNew;
            return sMinLimitE;
        }
        public int get_sMaxLimitE()
        {
            sMaxLimitE = valmoWin.dv.IprPr[23].valueNew;
            return sMaxLimitE;
        }
        public UnitType get_sValueEUnit()
        {
            sValueEUnit = valmoWin.dv.IprPr[24].valueNew;
            return getUnit(sValueEUnit);
        }
        public int get_sMinLimitF()
        {
            sMinLimitF = valmoWin.dv.IprPr[25].valueNew;
            return sMinLimitF;
        }
        public int get_sMaxLimitF()
        {
            sMaxLimitF = valmoWin.dv.IprPr[26].valueNew;
            return sMaxLimitF;
        }
        public UnitType get_sValueFUnit()
        {
            sValueFUnit = valmoWin.dv.IprPr[27].valueNew;
            return getUnit(sValueFUnit);
        }

        public int get_sShowStepNr()
        {
            sShowStepNr = valmoWin.dv.IprPr[34].valueNew;
            return sShowStepNr;
        }
        public int get_sUnCheckRet()
        {
            sUnCheckRet = valmoWin.dv.IprPr[42].valueNew;
            return sUnCheckRet;
        }
        public int get_sSyncMode()
        {
            sSyncMode = valmoWin.dv.IprPr[44].valueNew;
            return sSyncMode;
        }
        public int get_sLineInfo()
        {
            sLineInfo = valmoWin.dv.IprPr[65].valueNew;
            return sLineInfo;
        }
        public int get_sl_setup()
        {
            sl_setup = valmoWin.dv.IprPr[8].valueNew;
            return sl_setup;
        }
        public UnitType getUnit(int unitValue)
        {
            UnitType retUnitType = UnitType.DgtType;
            switch (unitValue)
            {
                case 0:
                case 1:
                    break;
                case 2:
                    retUnitType = objUnit.lenUnitBasic;
                    break;
                case 3:
                    retUnitType = objUnit.prsUnitBasic;
                    break;
                case 4:
                    retUnitType = UnitType.Tm_s;
                    break;
                case 5:
                    retUnitType = UnitType.Per;
                    break;
                case 6:
                    retUnitType = UnitType.Force_KN;
                    break;
                case 7:
                    retUnitType = UnitType.Force_ton;
                    break;
                case 8:
                    retUnitType = UnitType.Degree;
                    break;
            }
            return retUnitType;
        }
        public string getStrValue(int value, int unitType)
        {
            string str_ret = "";
            switch (unitType)
            {
                case 0:
                case 1:
                    str_ret = value.ToString();
                    break;
                case 2://len
                    str_ret = valmoWin.dv.lenTypeObj.getStrValue(value) + "  " + objUnit.unitBase[objUnit.lenUnitBasic];
                    break;
                case 3://prs
                    str_ret = valmoWin.dv.prsTypeObj.getStrValue(value) + "  " + objUnit.unitBase[objUnit.prsUnitBasic];
                    break;
                case 4://tms
                    str_ret = valmoWin.dv.tmTypeObj.getStrValue(value) + "  " + objUnit.unit_s;
                    break;
                case 5://per
                    str_ret = valmoWin.dv.perTypeObj.getStrValue(value) + "  " + objUnit.unitBase[UnitType.Per];
                    break;
                case 6://focKN
                    {
                        //valmoWin.dv.forceTypeObj.value = value;
                        str_ret = objUnit.getStrValue(UnitType.Force_KN,value) + "  "+objUnit.unit_KN;
                    }
                    break;
                case 7://focTon
                    {
                        //valmoWin.dv.forceTypeObj.value = value;
                        str_ret = objUnit.getStrValue(UnitType.Force_ton, value) + "  "+ objUnit.unit_ton;
                    }
                    break;
                case 8: //degree
                    {
                        str_ret = objUnit.getStrValue(UnitType.Degree, value) + "  " + objUnit.unit_Degree;
                    }
                    break;
            }
            return str_ret;
        }
        public string getStrValueA()
        {
            get_sValueAUnit();
            get_sValueA();
            //string str_ret = "";
            //switch (sValueAUnit)
            //{
            //    case 0:
            //    case 1:
            //        str_ret = iprCtrl.curUnit.sValueA.ToString();
            //        break;
            //    case 2://len
            //        str_ret = valmoWin.dv.lenTypeObj.getStrValue(sValueA) + "  " + objUnit.unitBase[objUnit.lenUnitBasic];
            //        break;
            //    case 3://prs
            //        str_ret = valmoWin.dv.prsTypeObj.getStrValue(sValueA) + "  " + objUnit.unitBase[objUnit.prsUnitBasic];
            //        break;
            //    case 4://tms
            //        str_ret = valmoWin.dv.tmTypeObj.getStrValue(sValueA) + "  s";
            //        break;
            //    case 5://per
            //        str_ret = valmoWin.dv.perTypeObj.getStrValue(sValueA) + "  " + objUnit.unitBase[UnitType.Per];
            //        break;
            //    case 6://focKN
            //        {
            //            valmoWin.dv.forceTypeObj.value = sValueA;
            //            str_ret = valmoWin.dv.forceTypeObj.getStrValue(UnitType.Force_KN) + "  KN";
            //        }
            //        break;
            //    case 7://focTon
            //        {
            //            valmoWin.dv.forceTypeObj.value = sValueA;
            //            str_ret = valmoWin.dv.forceTypeObj.getStrValue(UnitType.Force_ton) + "  ton";
            //        }
            //        break;
            //}
            return getStrValue(sValueA,sValueAUnit);
        }
        public string getStrValueB()
        {
            get_sValueBUnit();
            get_sValueB();
            //string str_ret = "";
            //switch (sValueBUnit)
            //{
            //    case 0:
            //        str_ret = iprCtrl.curUnit.sValueB.ToString();
            //        break;
            //    case 1:
            //        str_ret = iprCtrl.curUnit.sValueB.ToString();
            //        break;
            //    case 2://len
            //        str_ret = valmoWin.dv.lenTypeObj.getStrValue(sValueB) + "  " + objUnit.unitBase[UnitType.Len_mm];
            //        break;
            //    case 3://prs
            //        str_ret = valmoWin.dv.prsTypeObj.getStrValue(sValueB) + "  " + objUnit.unitBase[UnitType.Prs_Mpa];
            //        break;
            //    case 4://tms
            //        str_ret = valmoWin.dv.tmTypeObj.getStrValue(sValueB) + "  s";
            //        break;
            //    case 5://per
            //        str_ret = valmoWin.dv.perTypeObj.getStrValue(sValueB) + "  " + objUnit.unitBase[UnitType.Per];
            //        break;
            //    case 6://focKN
            //        {
            //            valmoWin.dv.forceTypeObj.value = sValueB;
            //            str_ret = valmoWin.dv.forceTypeObj.getStrValue(UnitType.Force_KN) + "  KN";
            //        }
            //        break;
            //    case 7://focTon
            //        {
            //            valmoWin.dv.forceTypeObj.value = sValueB;
            //            str_ret = valmoWin.dv.forceTypeObj.getStrValue(UnitType.Force_ton) + "  ton";
            //        }
            //        break;
            //}
            return getStrValue(sValueB, sValueBUnit);
        }
        public string getStrValueC()
        {
            get_sValueCUnit();
            get_sValueC();
            //string str_ret = "";
            //switch (sValueCUnit)
            //{
            //    case 0:
            //        str_ret = iprCtrl.curUnit.sValueC.ToString();
            //        break;
            //    case 1:
            //        str_ret = iprCtrl.curUnit.sValueC.ToString();
            //        break;
            //    case 2://len
            //        str_ret = valmoWin.dv.lenTypeObj.getStrValue(sValueC) + "  " + objUnit.unitBase[UnitType.Len_mm];
            //        break;
            //    case 3://prs
            //        str_ret = valmoWin.dv.prsTypeObj.getStrValue(sValueC) + "  " + objUnit.unitBase[UnitType.Prs_Mpa];
            //        break;
            //    case 4://tms
            //        str_ret = valmoWin.dv.tmTypeObj.getStrValue(sValueC) + "  s";
            //        break;
            //    case 5://per
            //        str_ret = valmoWin.dv.perTypeObj.getStrValue(sValueC) + "  " + objUnit.unitBase[UnitType.Per];
            //        break;
            //    case 6://focKN
            //        {
            //            valmoWin.dv.forceTypeObj.value = sValueC;
            //            str_ret = valmoWin.dv.forceTypeObj.getStrValue(UnitType.Force_KN) + "  KN";
            //        }
            //        break;
            //    case 7://focTon
            //        {
            //            valmoWin.dv.forceTypeObj.value = sValueC;
            //            str_ret = valmoWin.dv.forceTypeObj.getStrValue(UnitType.Force_ton) + "  ton";
            //        }
            //        break;
            //}
            return getStrValue(sValueC, sValueCUnit);
        }
        public string getStrValueD()
        {
            get_sValueDUnit();
            get_sValueD();
            //string str_ret = "";
            //switch (sValueDUnit)
            //{
            //    case 0:
            //        str_ret = iprCtrl.curUnit.sValueD.ToString();
            //        break;
            //    case 1:
            //        str_ret = iprCtrl.curUnit.sValueD.ToString();
            //        break;
            //    case 2://len
            //        str_ret = valmoWin.dv.lenTypeObj.getStrValue(sValueD) + "  " + objUnit.unitBase[UnitType.Len_mm];
            //        break;
            //    case 3://prs
            //        str_ret = valmoWin.dv.prsTypeObj.getStrValue(sValueD) + "  " + objUnit.unitBase[UnitType.Prs_Mpa];
            //        break;
            //    case 4://tms
            //        str_ret = valmoWin.dv.tmTypeObj.getStrValue(sValueD) + "  s";
            //        break;
            //    case 5://per
            //        str_ret = valmoWin.dv.perTypeObj.getStrValue(sValueD) + "  " + objUnit.unitBase[UnitType.Per];
            //        break;
            //    case 6://focKN
            //        {
            //            valmoWin.dv.forceTypeObj.value = sValueD;
            //            str_ret = valmoWin.dv.forceTypeObj.getStrValue(UnitType.Force_KN) + "  KN";
            //        }
            //        break;
            //    case 7://focTon
            //        {
            //            valmoWin.dv.forceTypeObj.value = sValueD;
            //            str_ret = valmoWin.dv.forceTypeObj.getStrValue(UnitType.Force_ton) + "  ton";
            //        }
            //        break;
            //}
            return getStrValue(sValueD, sValueDUnit);
        }

        public bool sErrLink
        {
            get
            {
                return (iprCtrl.curUnit.sNotReady & 0x07) != 0;
            }
        }
        public bool sErrActName
        {
            get
            {
                return ((iprCtrl.curUnit.sNotReady>>9)&0x01) == 1;
            }
        }
        public bool sErrFuncSelect
        {
            get
            {
                return ((iprCtrl.curUnit.sNotReady >> 10) & 0x01) == 1;
            }
        }
        public bool sErrOpSelect
        {
            get
            {
                return ((iprCtrl.curUnit.sNotReady >> 11) & 0x01) == 1;
            }
        }
        public bool sErrValueA
        {
            get
            {
                return ((iprCtrl.curUnit.sNotReady >> 12) & 0x01) == 1;
            }
        }
        public bool sErrValueB
        {
            get
            {
                return ((iprCtrl.curUnit.sNotReady >> 13) & 0x01) == 1;
            }
        }
        public bool sErrValueC
        {
            get
            {
                return ((iprCtrl.curUnit.sNotReady >> 14) & 0x01) == 1;
            }
        }
        public bool sErrValueD
        {
            get
            {
                return ((iprCtrl.curUnit.sNotReady >> 15) & 0x01) == 1;
            }
        }
        public bool sErrValueE
        {
            get
            {
                return ((iprCtrl.curUnit.sNotReady >> 16) & 0x01) == 1;
            }
        }
        public bool sErrValueF
        {
            get
            {
                return ((iprCtrl.curUnit.sNotReady >> 17) & 0x01) == 1;
            }
        }
        public bool sErrUndefined
        {
            get
            {
                return ((iprCtrl.curUnit.sNotReady >> 4) & 0x01) == 1;
            }
        }

        public bool flagCoreAVisible
        {
            get
            {
                return ((sl_setup >> 0) & 0x01) == 1;
            }
        }
        public bool flagCoreBVisible
        {
            get
            {
                return ((sl_setup >> 1) & 0x01) == 1;
            }
        }
        public bool flagCoreCVisible
        {
            get
            {
                return ((sl_setup >> 2) & 0x01) == 1;
            }
        }
        public bool flagCoreDVisible
        {
            get
            {
                return ((sl_setup >> 3) & 0x01) == 1;
            }
        }
        public bool flagCoreEVisible
        {
            get
            {
                return ((sl_setup >> 4) & 0x01) == 1;
            }
        }
        public bool flagCoreFVisible
        {
            get
            {
                return ((sl_setup >> 5) & 0x01) == 1;
            }
        }
        public bool flagAir1
        {
            get
            {
                return ((sl_setup >> 6) & 0x01) == 1;
            }
        }
        public bool flagAir2
        {
            get
            {
                return ((sl_setup >> 7) & 0x01) == 1;
            }
        }
        public bool flagAir3
        {
            get
            {
                return ((sl_setup >> 8) & 0x01) == 1;
            }
        }
        public bool flagAir4
        {
            get
            {
                return ((sl_setup >> 9) & 0x01) == 1;
            }
        }
        public bool flagAir5
        {
            get
            {
                return ((sl_setup >> 10) & 0x01) == 1;
            }
        }
        public bool flagAir6
        {
            get
            {
                return ((sl_setup >> 11) & 0x01) == 1;
            }
        }
        public bool flagAir7
        {
            get
            {
                return ((sl_setup >> 12) & 0x01) == 1;
            }
        }
        public bool flagAir8
        {
            get
            {
                return ((sl_setup >> 13) & 0x01) == 1;
            }
        }
        public bool flagAir9
        {
            get
            {
                return ((sl_setup >> 14) & 0x01) == 1;
            }
        }
        public bool flagAir10
        {
            get
            {
                return ((sl_setup >> 15) & 0x01) == 1;
            }
        }
        public bool flagAir11
        {
            get
            {
                return ((sl_setup >> 16) & 0x01) == 1;
            }
        }
        public bool flagAir12
        {
            get
            {
                return ((sl_setup >> 17) & 0x01) == 1;
            }
        }
        public bool flagSaftyDoor
        {
            get
            {
                return ((sl_setup >> 29) & 0x01) == 1;
            }
        }
        public bool flagRotateTable
        {
            get
            {
                return ((sl_setup >> 31) & 0x01) == 1;
            }
        }
        public bool flagNozzle
        {
            get
            {
                return ((sl_setup >> 26) & 0x01) == 1;
            }
        }
        public bool flagValveGate1
        {
            get
            {
                return ((sl_setup >> 27) & 0x01) == 1;
            }
        }
        public bool flagValveGate2
        {
            get
            {
                return ((sl_setup >> 28) & 0x01) == 1;
            }
        }
        public bool flagValveGate3
        {
            get
            {
                return ((sl_setup >> 24) & 0x01) == 1;
            }
        }
        public bool flagValveGate4
        {
            get
            {
                return ((sl_setup >> 25) & 0x01) == 1;
            }
        }


        public void set_sl_setup(int value)
        {
            valmoWin.dv.IprPr[8].valueNew = value;
        }
        public void set_sShowStepNr(int value)
        {
            valmoWin.dv.IprPr[34].valueNew = value;
        }





        public void set_sNotReady(int value)
        {
            valmoWin.dv.IprPr[43].valueNew = value;
        }
        public void set_sSyncMode(int value)
        {
            valmoWin.dv.IprPr[44].valueNew = value;
        }
        //public void set_sMinLimitA(int value)
        //{
        //    valmoWin.dv.iprPr[45].valueNew = value;
        //}
        //public void set_sMaxLimitA(int value)
        //{
        //    valmoWin.dv.iprPr[46].valueNew = value;
        //}
        //public void set_sValueAUnit(int value)
        //{
        //    valmoWin.dv.iprPr[47].valueNew = value;
        //}
        //public void set_sMinLimitB(int value)
        //{
        //    valmoWin.dv.iprPr[48].valueNew = value;
        //}
        //public void set_sMaxLimitB(int value)
        //{
        //    valmoWin.dv.iprPr[49].valueNew = value;
        //}
        //public void set_sValueBUnit(int value)
        //{
        //    valmoWin.dv.iprPr[50].valueNew = value;
        //}
        //public void set_sMinLimitC(int value)
        //{
        //    valmoWin.dv.iprPr[51].valueNew = value;
        //}
        //public void set_sMaxLimitC(int value)
        //{
        //    valmoWin.dv.iprPr[52].valueNew = value;
        //}
        //public void set_sValueCUnit(int value)
        //{
        //    valmoWin.dv.iprPr[53].valueNew = value;
        //}
        public void set_sLineInfo(int value)
        {
            valmoWin.dv.IprPr[65].valueNew = value;
        }

        //public void set_sMinLimitD(int value)
        //{
        //    valmoWin.dv.iprPr[62].valueNew = value;
        //}
        //public void set_sMaxLimitD(int value)
        //{
        //    valmoWin.dv.iprPr[63].valueNew = value;
        //}
        //public void set_sValueDUnit(int value)
        //{
        //    valmoWin.dv.iprPr[64].valueNew = value;
        //}
        //public void set_sMinLimitE(int value)
        //{
        //    valmoWin.dv.iprPr[22].valueNew = value;
        //}
        //public void set_sMaxLimitE(int value)
        //{
        //    valmoWin.dv.iprPr[23].valueNew = value;
        //}
        //public void set_sValueEUnit(int value)
        //{
        //    valmoWin.dv.iprPr[24].valueNew = value;
        //}
        //public void set_sMinLimitF(int value)
        //{
        //    valmoWin.dv.iprPr[25].valueNew = value;
        //}
        //public void set_sMaxLimitF(int value)
        //{
        //    valmoWin.dv.iprPr[26].valueNew = value;
        //}
        //public void set_sValueFUnit(int value)
        //{
        //    valmoWin.dv.iprPr[27].valueNew = value;
        //}

        /// <summary>
        /// 当前教导程序中焦点的位置 (Block 0-7位 编号从 1到7) (Line  8-15位 编号 从0到29) (Row 16-23位 编号从0到3) 
        /// 位置编号id(范围0 - 209） = (Block - 1) * 30 + Line * 10 + Row 
        /// </summary>
        public int sPos ;
        public int sl_setup ;
        public int sShowStepNr;
        public int sActName;
        public int sFuncSelect;
        public int sOperateType;
        public int sValueA;
        public int sValueB;
        public int sValueC;
        public int sValueD;
        public int sValueE;
        public int sValueF;
        public int sUnCheckRet;
        public int sNotReady;
        public int sSyncMode;
        public int sMinLimitA ;
        public int sMaxLimitA ;
        public int sValueAUnit ;
        public int sMinLimitB;
        public int sMaxLimitB ;
        public int sValueBUnit;
        public int sMinLimitC ;
        public int sMaxLimitC ;
        public int sValueCUnit;
        public int sLineInfo;
        public int sLinkNode ;
        public int sMinLimitD;
        public int sMaxLimitD;
        public int sValueDUnit ;
        public int sMinLimitE;
        public int sMaxLimitE;
        public int sValueEUnit;
        public int sMinLimitF;
        public int sMaxLimitF;
        public int sValueFUnit;

        public void getValue(iprUnitObj obj)
        {
            this.sPos = obj.sPos;
            this.sActName = obj.sActName;
            this.sl_setup = obj.sl_setup;
            this.sShowStepNr = obj.sShowStepNr;
            this.sFuncSelect = obj.sFuncSelect;
            this.sOperateType = obj.sOperateType;
            this.sValueA = obj.sValueA;
            this.sValueB = obj.sValueB;
            this.sValueC = obj.sValueC;
            this.sUnCheckRet = obj.sUnCheckRet;
            this.sNotReady = obj.sNotReady;
            this.sSyncMode = obj.sSyncMode;
            this.sMinLimitA = obj.sMinLimitA;
            this.sMaxLimitA = obj.sMaxLimitA;
            this.sValueAUnit = obj.sValueAUnit;
            this.sMinLimitB = obj.sMinLimitB;
            this.sMaxLimitB = obj.sMaxLimitB;
            this.sValueBUnit = obj.sValueBUnit;
            this.sMinLimitC = obj.sMinLimitC;
            this.sMaxLimitC = obj.sMaxLimitC;
            this.sLineInfo = obj.sLineInfo;
            this.sLinkNode = obj.sLinkNode;
            this.sMinLimitD = obj.sMinLimitD;
            this.sMaxLimitD = obj.sMaxLimitD;
            this.sValueDUnit = obj.sValueDUnit;
            this.sMinLimitE = obj.sMinLimitE;
            this.sMaxLimitE = obj.sMaxLimitE;
            this.sValueEUnit = obj.sValueEUnit;
            this.sMinLimitF = obj.sMinLimitF;
            this.sMaxLimitF = obj.sMaxLimitF;
            this.sValueFUnit = obj.sValueFUnit;
        }

        public string toString()
        {
            string strSave = sPos + "," +
                  sActName + "," +
                  sFuncSelect + "," +
                  sOperateType + "," +
                  sValueA + "," +
                  sValueB + "," +
                  sValueC + "," +
                  sValueD + "," +
                  sValueE + "," +
                  sValueF + "," +
                  sLinkNode + "," +
                  sUnCheckRet + ",";
            return strSave;
        }

        public void setValue(string[] data)
        {
            try
            {
                //if (data.Length < 22)
                //{
                //    string tmp = data[0];
                //    if (data.Length == 1 && tmp[0] == 0)
                //    {
                //        sPos = 0;
                //        sShowStepNr = 0;
                //        sActName = 0;
                //        sFuncSelect = 0;
                //        sOperateType = 0;
                //        sValueA = 0;
                //        sValueB = 0;
                //        sValueC = 0;
                //        sUnCheckRet = 0;
                //        sNotReady = 0;
                //        sSyncMode = 0;
                //        sMinLimitA = 0;
                //        sMaxLimitA = 0;
                //        sValueAUnit = 0;
                //        sMinLimitB = 0;
                //        sMaxLimitB = 0;
                //        sValueBUnit = 0;
                //        sMinLimitC = 0;
                //        sMaxLimitC = 0;
                //        sValueCUnit = 0;
                //        sMinLimitD = 0;
                //        sMaxLimitD = 0;
                //        sValueDUnit = 0;
                //        sLineInfo = 0;
                //    }
                //    return;

                //}
                //sPos = Int32.Parse(data[0]);
                //sShowStepNr = Int32.Parse(data[3]);
                //sActName = Int32.Parse(data[4]);
                //sFuncSelect = Int32.Parse(data[5]);
                //sOperateType = Int32.Parse(data[6]);
                //sValueA = Int32.Parse(data[7]);
                //sValueB = Int32.Parse(data[8]);
                //sValueC = Int32.Parse(data[9]);
                //sUnCheckRet = Int32.Parse(data[10]);
                //sNotReady = Int32.Parse(data[11]);
                //sSyncMode = Int32.Parse(data[12]);
                //sMinLimitA = Int32.Parse(data[13]);
                //sMaxLimitA = Int32.Parse(data[14]);
                //sValueAUnit = Int32.Parse(data[15]);
                //sMinLimitB = Int32.Parse(data[16]);
                //sMaxLimitB = Int32.Parse(data[17]);
                //sValueBUnit = Int32.Parse(data[18]);
                //sMinLimitC = Int32.Parse(data[19]);
                //sMaxLimitC = Int32.Parse(data[20]);
                //sValueCUnit = Int32.Parse(data[21]);
                //sMinLimitD = Int32.Parse(data[19]);
                //sMaxLimitD = Int32.Parse(data[20]);
                //sValueDUnit = Int32.Parse(data[21]);
                //sLineInfo = Int32.Parse(data[22]);

            }
            catch (Exception ex)
            {
                vm.perror(ex.ToString());
            }
        }
        public void set_sPos(int pos)
        {
            valmoWin.dv.IprPr[17].valueNew = pos;
        }
        public void set_sActName(int value)
        {
            valmoWin.dv.IprPr[36].valueNew = value;
        }
        public void set_sFuncSelect(int value)
        {
            valmoWin.dv.IprPr[37].valueNew = value;
        }
        public void set_sOperateType(int value)
        {
            valmoWin.dv.IprPr[38].valueNew = value;
        }
        public void set_sValueA(int value)
        {
            valmoWin.dv.IprPr[39].valueNew = value;
        }
        public void set_sValueB(int value)
        {
            valmoWin.dv.IprPr[40].valueNew = value;
        }
        public void set_sValueC(int value)
        {
            valmoWin.dv.IprPr[41].valueNew = value;
        }
        public void set_sValueD(int value)
        {
            valmoWin.dv.IprPr[67].valueNew = value;
        }
        public void set_sValueE(int value)
        {
            valmoWin.dv.IprPr[68].valueNew = value;
        }
        public void set_sValueF(int value)
        {
            valmoWin.dv.IprPr[69].valueNew = value;
        }
        public void set_sLinkNode(int value)
        {
            valmoWin.dv.IprPr[28].valueNew = value;
        }
        public void set_sUnCheckRet(int value)
        {
            valmoWin.dv.IprPr[42].valueNew = value;
        }
        public void saveConf(FileStream fs)
        {

        }
            
        public void save(FileStream fs)
        {
            
            string strSave =  sPos         + "," +
                              sActName     + "," +
                              sFuncSelect  + "," +
                              sOperateType + "," +
                              sValueA      + "," +
                              sValueB      + "," +
                              sValueC      + "," +
                              sValueD      + "," +
                              sValueE      + "," +
                              sValueF      + "," +
                              sLinkNode    + "," +
                              sUnCheckRet  + ",";

            byte[] curData = System.Text.Encoding.Default.GetBytes(strSave);
            fs.Write(curData, 0, curData.Length);
            for (int i = curData.Length; i < 500; i++)
            {
                fs.WriteByte(0);
            }
            if(sLinkNode != 0 && sLinkNode != 66)
                vm.printLn(sLinkNode.ToString());
        }

        public void save(XmlTextWriter writer)
        {
            if (sActName != 0)
            {
                int id = ((sPos & 0xff) - 1) * 30 + ((sPos >> 8) & 0xff) * 10 + (sPos >> 16) & 0xff;
                writer.WriteStartElement("item");
                writer.WriteAttributeString("id", id.ToString());
                writer.WriteAttributeString("sPos", sPos.ToString());
                writer.WriteAttributeString("sActName", sActName.ToString());
                writer.WriteAttributeString("sLinkNode", sLinkNode.ToString());


                if (sFuncSelect != 0)
                    writer.WriteElementString("sFuncSelect", sFuncSelect.ToString());
                if (sOperateType != 0)
                    writer.WriteElementString("sOperateType", sOperateType.ToString());
                if (sValueA != 0)
                    writer.WriteElementString("sValueA", sValueA.ToString());
                if (sValueB != 0)
                    writer.WriteElementString("sValueB", sValueB.ToString());
                if (sValueC != 0)
                    writer.WriteElementString("sValueC", sValueC.ToString());
                if (sValueD != 0)
                    writer.WriteElementString("sValueD", sValueD.ToString());
                if (sValueE != 0)
                    writer.WriteElementString("sValueE", sValueE.ToString());
                if (sValueF != 0)
                    writer.WriteElementString("sValueF", sValueF.ToString());

                if (sUnCheckRet != 0)
                    writer.WriteElementString("sUnCheckRet", sUnCheckRet.ToString());
                writer.WriteEndElement();
            }
        }
        public void save(XmlNode xn,XmlDocument xmlDoc)
        {
            if (sActName != 0)
            {
                XmlElement item = xmlDoc.CreateElement("item");
                int id = ((sPos & 0xff) - 1) * 30 + ((sPos >> 8) & 0xff) * 10 + (sPos >> 16) & 0xff;
                item.SetAttribute("id", id.ToString());
                item.SetAttribute("sPos", sPos.ToString());
                item.SetAttribute("sActName", sActName.ToString());
                item.SetAttribute("sLinkNode", sLinkNode.ToString());
                if (sFuncSelect != 0)
                {
                    XmlElement subItem = xmlDoc.CreateElement("sFuncSelect");
                    subItem.InnerText = sFuncSelect.ToString();
                    item.AppendChild(subItem);
                }
                if (sOperateType != 0)
                {
                    XmlElement subItem = xmlDoc.CreateElement("sOperateType");
                    subItem.InnerText = sOperateType.ToString();
                    //subItem.Value = sOperateType.ToString();
                    item.AppendChild(subItem);
                }
                if (sValueA != 0)
                {
                    XmlElement subItem = xmlDoc.CreateElement("sValueA");
                    subItem.InnerText = sValueA.ToString();
                    //subItem.Value = sValueA.ToString();
                    item.AppendChild(subItem);
                }
                if (sValueB != 0)
                {
                    XmlElement subItem = xmlDoc.CreateElement("sValueB");
                    subItem.InnerText = sValueB.ToString();
                    item.AppendChild(subItem);
                }
                if (sValueC != 0)
                {
                    XmlElement subItem = xmlDoc.CreateElement("sValueC");
                    subItem.InnerText = sValueC.ToString();
                    item.AppendChild(subItem);
                }
                if (sValueD != 0)
                {
                    XmlElement subItem = xmlDoc.CreateElement("sValueD");
                    subItem.InnerText = sValueD.ToString();
                    item.AppendChild(subItem);
                }
                if (sValueE != 0)
                {
                    XmlElement subItem = xmlDoc.CreateElement("sValueE");
                    subItem.InnerText = sValueE.ToString();
                    item.AppendChild(subItem);
                }
                if (sValueF != 0)
                {
                    XmlElement subItem = xmlDoc.CreateElement("sValueF");
                    subItem.InnerText = sValueF.ToString();
                    item.AppendChild(subItem);
                }
                if (sUnCheckRet != 0)
                {
                    XmlElement subItem = xmlDoc.CreateElement("sUnCheckRet");
                    subItem.InnerText = sUnCheckRet.ToString();
                    item.AppendChild(subItem);
                }
                xn.AppendChild(item);

            }
        }
        public void load(XmlNode node)
        {
            try
            {
                XmlElement xe = (XmlElement)node;
                vm.print("<item id=\"" + xe.GetAttribute("id") + "\"");
                string strId = xe.GetAttribute("id");
                if (strId != "")
                {
                    int id = Int32.Parse(strId);
                    int pos = (id / 30 + 1) | ((id % 30 / 10) << 8) | ((id % 10) << 16);
                    vm.print("new Pos : " + pos + " \t " + xe.GetAttribute("sPos"));
                    set_sPos(pos);
                }
                else
                {
                    set_sPos(Int32.Parse(xe.GetAttribute("sPos")));
                }
                vm.print(" sActName=\"" + xe.GetAttribute("sActName") + "\"");
                set_sActName(Int32.Parse(xe.GetAttribute("sActName")));
                vm.printLn(" sLinkNode=\"" + xe.GetAttribute("sLinkNode") + "\">");
                set_sLinkNode(Int32.Parse(xe.GetAttribute("sLinkNode")));
                foreach (XmlNode xn1 in node.ChildNodes)//遍历
                {
                    XmlElement xe2 = (XmlElement)xn1;//转换类型
                    switch (xe2.Name)
                    {
                        case "sFuncSelect":
                            vm.printLn("<sFuncSelect>" + xe2.InnerText + "</sFuncSelect>");
                            set_sFuncSelect(Int32.Parse(xe2.InnerText));
                            break;
                        case "sOperateType":
                            vm.printLn("<sOperateType>" + xe2.InnerText + "</sOperateType>");
                            set_sOperateType(Int32.Parse(xe2.InnerText));
                            break;
                        case "sValueA":
                            vm.printLn("<sValueA>" + xe2.InnerText + "<sValueA>");
                            set_sValueA(Int32.Parse(xe2.InnerText));
                            break;
                        case "sValueB":
                            vm.printLn("<sValueB>" + xe2.InnerText + "<sValueB>");
                            set_sValueB(Int32.Parse(xe2.InnerText));
                            break;
                        case "sValueC":
                            vm.printLn("<sValueC>" + xe2.InnerText + "<sValueC>");
                            set_sValueC(Int32.Parse(xe2.InnerText));
                            break;
                        case "sValueD":
                            vm.printLn("<sValueD>" + xe2.InnerText + "<sValueD>");
                            set_sValueD(Int32.Parse(xe2.InnerText));
                            break;
                        case "sValueE":
                            vm.printLn("<sValueE>" + xe2.InnerText + "<sValueE>");
                            set_sValueE(Int32.Parse(xe2.InnerText));
                            break;
                        case "sValueF":
                            vm.printLn("<sValueF>" + xe2.InnerText + "<sValueF>");
                            set_sValueF(Int32.Parse(xe2.InnerText));
                            break;
                        case "sUnCheckRet":
                            vm.printLn("<sUnCheckRet>" + xe2.InnerText + "<sUnCheckRet>");
                            set_sUnCheckRet(Int32.Parse(xe2.InnerText));
                            break;
                    }
                    //if(xe2.Name=="author")//如果找到
                    //{
                    //xe2.InnerText="亚胜";//则修改
                    //break;//找到退出来就可以了
                }
                vm.printLn("</item>");
            }
            catch(Exception ex)
            {
                vm.perror("< load file error > " + ex.ToString());
            }
        }

        public void load(string str)
        {
            string[] strValue = str.Split(',');
            if (strValue.Length >= 12)
            {
                set_sPos(Int32.Parse(strValue[0]));
                set_sActName(Int32.Parse(strValue[1]));
                set_sFuncSelect(Int32.Parse(strValue[2]));
                set_sOperateType(Int32.Parse(strValue[3]));
                set_sValueA(Int32.Parse(strValue[4]));
                set_sValueB(Int32.Parse(strValue[5]));
                set_sValueC(Int32.Parse(strValue[6]));
                set_sValueD(Int32.Parse(strValue[7]));
                set_sValueE(Int32.Parse(strValue[8]));
                set_sValueF(Int32.Parse(strValue[9]));
                set_sLinkNode(Int32.Parse(strValue[10]));
                //if (strValue[10] != "0" && strValue[10] != "66")
                //    vm.printLn(strValue[10]);
                set_sUnCheckRet(Int32.Parse(strValue[11]));

            }
            else
            {
                if (strValue.Length == 1)
                {
                    set_sPos(0);
                    set_sActName(0);
                    set_sFuncSelect(0);
                    set_sOperateType(0);
                    set_sValueA(0);
                    set_sValueB(0);
                    set_sValueC(0);
                    set_sValueD(0);
                    set_sValueE(0);
                    set_sValueF(0);
                    set_sLinkNode(0);
                    set_sUnCheckRet(0);
                }
                else
                    vm.perror("length error!");
            }


        }
        public void init()
        {
            sPos = 0;
            sShowStepNr = 0;
            sActName = 0;
            sFuncSelect = 0;
            sOperateType = 0;
            sValueA = 0;
            sValueB = 0;
            sValueC = 0;
            sValueD = 0;
            sValueE = 0;
            sValueF = 0;
            sLinkNode = 0;
            sUnCheckRet = 0;
            sNotReady = 0;
            sSyncMode = 0;
            sMinLimitA = 0;
            sMaxLimitA = 0;
            sValueAUnit = 0;
            sMinLimitB = 0;
            sMaxLimitB = 0;
            sValueBUnit = 0;
            sMinLimitC = 0;
            sMaxLimitC = 0;
            sValueCUnit = 0;
            sMinLimitD = 0;
            sMaxLimitD = 0;
            sValueDUnit = 0;
            sMinLimitE = 0;
            sMaxLimitE = 0;
            sValueEUnit = 0;
            sMinLimitF = 0;
            sMaxLimitF = 0;
            sValueFUnit = 0;
            sLineInfo = 0;
        }
    }
}
