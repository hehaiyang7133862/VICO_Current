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
    /// iprSetting.xaml 的交互逻辑
    /// </summary>
    public partial class iprSetting : UserControl
    {
        public iprSetting()
        {
            InitializeComponent();
            valmoWin.dv.IprPr[65].addHandle(handleIpr65);
            valmoWin.dv.IBTPr[4].addHandle(handleIBTPr_004);
            valmoWin.dv.IBTPr[8].addHandle(handleIBTPr_008);
        }

        private void handleIpr65(objUnit obj)
        {
            switch65_0.stateOn = ((obj.value >> 0) & 0x01) == 1;
            switch65_1.stateOn = ((obj.value >> 1) & 0x01) == 1;
            switch65_2.stateOn = ((obj.value >> 2) & 0x01) == 1;
            switch65_3.stateOn = ((obj.value >> 3) & 0x01) == 1;

            switch65_12.stateOn = ((obj.value >> 12) & 0x01) == 1;
            switch65_13.stateOn = ((obj.value >> 13) & 0x01) == 1;
            switch65_14.stateOn = ((obj.value >> 14) & 0x01) == 1;
            switch65_15.stateOn = ((obj.value >> 15) & 0x01) == 1;
        }

        private void handleIBTPr_004(objUnit obj)
        {
            lightState2Ctrl1.state = ((obj.value >> 4) & 0x01) == 1;
            lightState2Ctrl2.state = ((obj.value >> 5) & 0x01) == 1;
            lightState2Ctrl3.state = ((obj.value >> 6) & 0x01) == 1;
            lightState2Ctrl4.state = ((obj.value >> 7) & 0x01) == 1;
        }

        private void handleIBTPr_008(objUnit obj)
        {
            lightState2Ctrl5.state = ((obj.value >> 4) & 0x01) == 1;
            lightState2Ctrl6.state = ((obj.value >> 5) & 0x01) == 1;
            lightState2Ctrl7.state = ((obj.value >> 6) & 0x01) == 1;
            lightState2Ctrl8.state = ((obj.value >> 7) & 0x01) == 1;
        }

    }
}
