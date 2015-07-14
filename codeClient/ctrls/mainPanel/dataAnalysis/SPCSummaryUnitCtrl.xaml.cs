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
    public partial class SPCSummaryUnitCtrl : UserControl
    {
        private SPCVariable _CurData;
        public SPCVariable CurData
        {
            set
            {
                _CurData = value;

                lbVariableName.SetResourceReference(Label.ContentProperty, "TP_" + _CurData.CurObj.serialNum);
                lbUnit.Content = _CurData.Unit;
                lbUSL.Content = _CurData.USL;
                lbLSL.Content = _CurData.LSL;

                CurData.ReSampleHandle += UpdateCpk;
                CurData.ReSampleHandle += UpdateSTDEV;
                CurData.LSLUpdate += UpdateLSL;
                CurData.USLUpdate += UpdateUSL;
                CurData.CurValueChanged += UpdateCurValue;
                CurData.SPCStateChanged += UpdateState;
            }
            get
            {
                return _CurData;
            }
        }
        public bool Selected
        {
            set
            {
                cvsMain.Background = value == true ? new SolidColorBrush(Color.FromArgb(0xFF, 0xA6, 0xDD, 0xDE)) 
                    : new SolidColorBrush(Color.FromArgb(0xFF, 0xEA, 0xEA, 0xEA));
            }
        }
        public int SerialNum;

        public SPCSummaryUnitCtrl(SPCVariable spcV)
        {
            InitializeComponent();

            CurData = spcV;
        }

        private void UpdateSTDEV()
        {
            lbSTDEV.Content = CurData.CurSample.STDEV.ToString("0.00");
        }
        private void UpdateCpk()
        {
            lbCpk.Content = CurData.CurSample.Cpk.ToString("0.00");
        }
        private void UpdateLSL()
        {
            lbLSL.Content = CurData.LSL.ToString("0.00");
        }
        private void UpdateUSL()
        {
            lbUSL.Content = CurData.USL.ToString("0.00");
        }
        private void UpdateCurValue()
        {
            lbCurValue.Content = CurData.CurValue.ToString("0.00");
        }
        private void UpdateState()
        {
            indicator.State = CurData.SPCState;
        }

        private void lbLSL_MouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            valmoWin.SNumInput.init(100, 0, CurData.Discription + "LSL", CurData.LSL.ToString(), "", 1, null, LSLReset);
        }
        private void lbUSL_MouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            valmoWin.SNumInput.init(100, 0, CurData.Discription + " USL", CurData.USL.ToString(), "", 1, null, USLReset);
        }
        private void USLReset(double newValue)
        {
            CurData.USL = newValue;
        }
        private void LSLReset(double newValue)
        {
            CurData.LSL = newValue;
        }
    }
}
