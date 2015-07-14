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
    /// motroOnlineCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class motroOnlineCtrl : UserControl
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

        public motroOnlineCtrl()
        {
            InitializeComponent();
        }

        private void refushState(objUnit obj)
        {
            if (obj.value == 1)
            {
                _state = true;
            }
            else
            {
                _state = false;
            }

            tbOnline.SelectedIndex = (_state == true) ? 1 : 0;
            tbOnline2.SelectedIndex = (_state == true) ? 1 : 0;
        }
    }
}
