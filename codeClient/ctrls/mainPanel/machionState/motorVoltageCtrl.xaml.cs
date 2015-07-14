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
    /// motorVoltageCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class motorVoltageCtrl : UserControl
    {
        private objUnit _curObj;
        private bool _state = false;

        /// <summary>
        /// 设置对象
        /// </summary>
        public string objName
        {
            set
            {
                _curObj = valmoWin.dv.getObj(value);
                if (_curObj != null)
                {
                    _curObj.addHandle(refushState);
                }
            }
        }
        public motorVoltageCtrl()
        {
            InitializeComponent();
        }

        private void refushState(objUnit obj)
        {
            lbVoltage.Content = obj.vDblStr;

            if (obj.vDbl > 100)
            {
                _state = true;
            }
            else
            {
                _state = false;
            }

            tbState2.SelectedIndex = (_state == true) ? 1 : 0;
        }
    }
}
