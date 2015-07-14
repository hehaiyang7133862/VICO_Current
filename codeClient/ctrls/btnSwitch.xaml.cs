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
    public partial class btnSwitch : UserControl
    {
        private objUnit _curObj;
        private bool _state = false;
        private bool _bNeedConfirm = false;
        private bool _bIsMoseDown = false;
        private string _objDis = string.Empty;
        private bool _bIsOpposite = false;

        /// <summary>
        /// 设置是否需要确认
        /// </summary>
        public bool needConfirm
        {
            set
            {
                _bNeedConfirm = value;
            }
        }
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
        /// 设置描述信息
        /// </summary>
        public string dis
        {
            set
            {
                _objDis = value;
            }
        }
        public bool bIsOpposite
        {
            set
            {
                _bIsOpposite = value;
            }
        }
        public bool state
        {
            set
            {
                _state = value ^ _bIsOpposite;
            }
            get
            {
                return _state;
            }
        }

        public btnSwitch()
        {
            InitializeComponent();
        }

        private void refushState(objUnit obj)
        {
            if (obj.value  == 1)
            {
                state = true;
            }
            else
            {
                state = false;
            }

            tbState.SelectedIndex = (_state == true) ? 1 : 0;
        }

        private void cvsMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            _bIsMoseDown = true;
        }

        private void cvsMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            if (_bIsMoseDown == true)
            {
                _bIsMoseDown = false;
                if (_curObj != null)
                {
                    if (!valmoWin.dv.checkAccesslevel(_curObj.accessLevel))
                        return;
                    if (_bNeedConfirm)
                    {
                        valmoWin.SAttentionPanel.Show(_curObj, _objDis);
                    }
                    else
                    {
                        state = !state;
                        _curObj.setValue((state == true) ? 1 : 0);
                    }
                }
            }
        }
    }
}
