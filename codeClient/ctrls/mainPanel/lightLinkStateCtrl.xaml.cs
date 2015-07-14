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
    public partial class lightLinkStateCtrl : UserControl
    {
        private objUnit curObj;
        /// <summary>
        /// 设置对象
        /// </summary>
        public string objName
        {
            set
            {
                curObj = valmoWin.dv.getObj(value);
                if (curObj != null)
                {
                    curObj.addHandle(refreshState);
                }
            }
        }             
         private bool _state = false;
        /// <summary>
        /// 获取或设置指示灯状态
        /// </summary>
        public bool state
        {
            set
            {
                _state = value;
            }
            get
            {
                return _state;
            }
        }
        private int _bitNr = -1;
        /// <summary>
        /// 设置bit位
        /// </summary>
        public int bitNr
        {
            set
            {
                _bitNr = value;
            }
        }

        public lightLinkStateCtrl()
        {
            InitializeComponent();
        }

        private void refreshState(objUnit obj)
        {
            if (_bitNr == -1)
            {
                if (obj.value == 1)
                {
                    _state = true;
                }
                else
                {
                    _state = false;
                }
            }
            else
            {
                if (((obj.value >> _bitNr) & 0x01) == 1)
                {
                    _state = true;
                }
                else
                {
                    _state = false;
                }
            }

            tbState1.SelectedIndex = (_state == true) ? 1 : 0;
        }
    }
}
