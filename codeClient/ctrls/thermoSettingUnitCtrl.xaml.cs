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
    public partial class thermoSettingUnitCtrl : UserControl
    {
        objUnit curObj;
        public thermoSettingUnitCtrl()
        {
            InitializeComponent();
        }

        public string objName
        {
            set
            {
                curObj = valmoWin.dv.getObj(value);
                if (curObj != null)
                {
                    curObj.addHandle(handleCurAndSetting);
                    lbSer.Content =  (Int32.Parse(value.Substring(3,3)) - 259).ToString();
                }
            }
        }
        public int serNr
        {
            get
            {
                return Int32.Parse(lbSer.Content.ToString());
            }
        }
        private void handleCurAndSetting(objUnit obj)
        {
            lbCur.Content = valmoWin.dv.tempTypeObj.getStrValue((obj.value >> 16) & 0xffff);
            lbSetting.Content = valmoWin.dv.tempTypeObj.getStrValue(obj.value & 0xffff);
        }
        public bool focus
        {
            get
            {
                return bdMain.BorderBrush == Brushes.Green;
            }
            set
            {
                bdMain.BorderBrush = value ? Brushes.Green : Brushes.Silver;
            }
        }
    }
}
