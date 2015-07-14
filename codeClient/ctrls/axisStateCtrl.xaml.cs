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
    /// axisStateCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class axisStateCtrl : UserControl
    {
        public axisStateCtrl()
        {
            InitializeComponent();
            valmoWin.lstStartUpInit.Add(startUpInit);
        }
        public string objName1
        {
            set
            {
                MotoCtrl.objName = value;
            }
        }
        public string objName2
        {
            set
            {
                axisStateItemCtrl2.objName = value;
            }
        }
        public string objName3
        {
            set
            {
                ServoCtrl.objName = value;
            }
        }
        public string objName4
        {
            set
            {
                axisStateItemCtrl4.objName = value;
            }
        }
        objUnit _objServoLimit;
        public string objServoLimit
        {
            set
            {
                _objServoLimit = valmoWin.dv.getObj(value);
            }
        }
        objUnit _objMotoLimit;
        public string objMotoLimit
        {
            set
            {
                _objMotoLimit = valmoWin.dv.getObj(value);
            }
        }

        public void startUpInit()
        {
            if (_objServoLimit != null)
            {
                ServoCtrl.basicValue = _objServoLimit.vDblNew;
            }
            if (_objMotoLimit != null)
            {
                MotoCtrl.basicValue = _objMotoLimit.vDblNew;
            }
        }

        
    }
}
