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
    /// motorStateCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class motorStateCtrl : UserControl
    {
        private objUnit _curObj;
        private int _bitNr = -1;
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
        /// <summary>
        /// 设置马达状态位
        /// </summary>
        public int stateBit
        {
            set
            {
                string tmp = Enum.GetName(typeof(motorState),value);
                if (tmp != null)
                {
                    _bitNr = value;
                    lbStateBit.Content = tmp;
                }
            }
        }

        public motorStateCtrl()
        {
            InitializeComponent();
        }

        private void refushState(objUnit obj)
        {
            if (_bitNr != -1)
            {
                if (((obj.value >> _bitNr) & 0x01) == 1)
                {
                    _state = true;
                }
                else
                {
                    _state = false;
                }

                tblState.SelectedIndex = (_state == true) ? 1 : 0;
            }
        }
    }
}
