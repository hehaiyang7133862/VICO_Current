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
    /// Interaction logic for testObjCtrl.xaml
    /// </summary>
    public partial class dbgObjCtrl : UserControl
    {
        private objUnit curObj = null;

        public dbgObjCtrl()
        {
            InitializeComponent();
        }

        private void lbSer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lbSer.Background = Brushes.LightBlue;

            valmoWin.SNumInput.init(objCtrlInit(), 1, "",lbSer.Content.ToString(), "", 1, disposeFunc, confirmFunc);
        }

        private void lbVDbl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (curObj != null)
            {
                PreValue = curObj.vDbl;
                valmoWin.SNumInput.init(curObj, disposeFunc, setValueFunc);
            }
        }

        //int plcAddr = 0;
        //int plcValue = -1;
        //private void lbLocalAddr_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    lbLocalAddr.Background = Brushes.LightBlue;
        //    valmoWin.SCharKeyPanel.init(false, lbAddrValue.Content.ToString(), charEnterFunc, disposeCharFunc, new Thickness(300, 300,0,0)); 
        //}
        //private void charEnterFunc(string str)
        //{
        //    try
        //    {
        //        lbLocalAddr.Content = str;
        //        string[] strTmp = str.Split('.');
        //        if(strTmp.Length >= 2)
        //        {
        //            plcAddr = 0;
        //            plcValue = -1;
        //            if (LinkMgr.getObjPlcAddr(str, ref plcAddr, strTmp[0]))
        //            {
        //                if (Lasal32.LslReadFromSvr(plcAddr, ref plcValue))
        //                {
        //                    lbAddrValue.Content = plcValue;
        //                }
        //                else
        //                {
        //                    plcAddr = 0;
        //                    plcValue = -1;
        //                    lbAddrValue.Content = "";
        //                }
        //            }
        //            else
        //            {
        //                lbAddrValue.Content = "";
        //            }

        //        }
        //    }
        //    catch
        //    {

        //    }
        //}
        //private void lbAddrValue_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    if (lbAddrValue.Content.ToString() != "")
        //    {
        //        slcObj.vMinStrNew = "noMin";
        //        slcObj.vMaxStrNew = "noMax";
        //        slcObj.value = plcValue;
        //        slcObj.unitType = UnitType.DgtType;
        //        slcObj.addHandle(handleAddrValue);
        //        lbAddrValue.Background = Brushes.LightBlue;
        //        valmoWin.SNumInput.init(slcObj, disposeFunc);
        //    }
        //}
        //private void handleAddrValue(objUnit obj)
        //{
        //    if (Lasal32.LslWriteToSvr(plcAddr, obj.value))
        //    {
        //        if (Lasal32.LslReadFromSvr(plcAddr, ref plcValue))
        //        {
        //            lbAddrValue.Content = plcValue;
        //        }
        //    }

        //}


        private double objCtrlInit()
        {
            switch ((objectType)(tbLst.SelectedIndex + 1))
            {
                case objectType.IprPr:
                    {
                        return (valmoWin.dv.IprPr.length);
                    }
                case objectType.SysPr:
                    {
                        return (valmoWin.dv.SysPr.length);
                    }
                case objectType.MldPr:
                    {
                        return (valmoWin.dv.MldPr.length);
                    }
                case objectType.InjPr:
                    {
                        return (valmoWin.dv.InjPr.length);
                    }
                case objectType.TmpPr:
                    {
                        return (valmoWin.dv.TmpPr.length);
                    }
                case objectType.PrdPr:
                    {
                        return (valmoWin.dv.PrdPr.length);
                    }
                case objectType.AlmPr:
                    {
                        return (valmoWin.dv.AlmPr.length);
                    }
                case objectType.KeyPr:
                    {
                        return (valmoWin.dv.KeyPr.length);
                    }
                default:
                    {
                        return 0;
                    }
            }
        }
        private void handleSerGet(double ser)
        {
            switch ((objectType)(tbLst.SelectedIndex + 1))
            {
                case objectType.IprPr:
                    {
                        curObj = valmoWin.dv.IprPr[(int)ser];
                    }
                    break;
                case objectType.SysPr:
                    {
                        curObj = valmoWin.dv.SysPr[(int)ser];
                    }
                    break;
                case objectType.MldPr:
                    {
                        curObj = valmoWin.dv.MldPr[(int)ser];
                    }
                    break;
                case objectType.InjPr:
                    {
                        curObj = valmoWin.dv.InjPr[(int)ser];
                    }
                    break;
                case objectType.TmpPr:
                    {
                        curObj = valmoWin.dv.TmpPr[(int)ser];
                    }
                    break;
                case objectType.PrdPr:
                    {
                        curObj = valmoWin.dv.PrdPr[(int)ser];
                    }
                    break;
                case objectType.AlmPr:
                    {
                        curObj = valmoWin.dv.AlmPr[(int)ser];
                    }
                    break;
                case objectType.KeyPr:
                    {
                        curObj = valmoWin.dv.KeyPr[(int)ser];
                    }
                    break;
            }

            if (curObj != null)
            {
                lbDis.Content = valmoWin.dv.getCurDis(curObj.serialNum);
                lbAddr.Content = curObj.addrLocal;
                lbUnit.Content = curObj.unit;
                lbVDbl.Content = curObj.vDblStrNew;
                lbValue.Content = curObj.value;
                lbSer.Content = ser;
            }
        }
        private void disposeFunc()
        {
            lbSer.Background = Brushes.Silver;
        }
        private void confirmFunc(double newValue)
        {
            lbSer.Background = Brushes.Silver;

            handleSerGet(newValue);

        }
        private double PreValue;
        private void setValueFunc(double newValue)
        {
            curObj.setValue(newValue);

            lbVDbl.Content = curObj.vDblStrNew;
            lbValue.Content = curObj.value;

            valmoWin.eventMgr.addParamMsg(curObj.serialNum, DateTime.Now, PreValue, newValue);
        }
    }
}
