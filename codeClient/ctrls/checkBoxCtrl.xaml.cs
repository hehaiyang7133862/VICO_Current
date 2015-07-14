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
    public delegate bool checkBoxCtrlActiveEvent(bool isActived);

    /// <summary>
    /// 自定义选择框
    /// </summary>
    public partial class checkBoxCtrl : UserControl
    {
        private objUnit _curObj;
        /// <summary>
        /// 设置当前对象
        /// </summary>
        public string objName
        {
            set
            {
                _curObj = valmoWin.dv.getObj(value);
                if (_curObj != null)
                {
                    _curObj.addHandle(switchHandle);
                }
            }
        }
        /// <summary>
        /// 回调函数
        /// </summary>
        /// <param name="obj">对象</param>
        private void switchHandle(objUnit obj)
        {
            if (bitNr >= 0)
                bIsChecked = ((obj.value >> bitNr) & 0x01) == 1;
            else
                bIsChecked = obj.value == 1;
        }
        /// <summary>
        /// 对象中表示是否选中的二进制位
        /// </summary>
        private int _bitNr = -1;
        /// <summary>
        /// 设置二进制位
        /// </summary>
        public int bitNr
        {
            get
            {
                return _bitNr;
            }
            set
            {
                _bitNr = value;
            }
        }
        /// <summary>
        /// 是否只读
        /// </summary>
        private bool _readOnly = false;
        /// <summary>
        /// 设置是否只读
        /// </summary>
        public bool readOnly
        {
            get
            {
                return _readOnly;
            }
            set
            {
                _readOnly = value;
            }
        }
        /// <summary>
        /// 状态
        /// </summary>
        private bool _bIsChecked = false;
        /// <summary>
        /// 设置状态
        /// </summary>
        public bool bIsChecked
        {
            get
            {
                return _bIsChecked;
            }
            set
            {
                _bIsChecked = value;

                imgChecked.Visibility = _bIsChecked ? Visibility.Visible : Visibility.Hidden;
                imgUnChecked.Visibility = _bIsChecked ? Visibility.Hidden : Visibility.Visible;

                if (checkedChange != null)
                {
                    checkedChange();
                }
            }
        }
        /// <summary>
        /// 鼠标是否按下
        /// </summary>
        private bool _isMouseDown = false;

        public checkBoxCtrl()
        {
            bitNr = -1;

            InitializeComponent();
        }

        /// <summary>
        /// checkedChange 事件
        /// </summary>
        public nullEvent checkedChange;

        private void cvsMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (!_readOnly)
            {
                if (_curObj != null)
                {
                    if (!valmoWin.dv.checkAccesslevel(_curObj.accessLevel))
                        return;
                }
                _isMouseDown = true;
            }
        }
        private void cvsMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (_isMouseDown)
            {
                _isMouseDown = false;

                if (_curObj != null)
                {
                    if (bitNr >= 0)
                    {
                        _curObj.setValue(((_curObj.valueNew >> bitNr) & 0x01) == 1 ? ((~(1 << bitNr)) & _curObj.value) : (_curObj.value | (1 << bitNr)));
                    }
                    else
                        _curObj.setValue(_curObj.valueNew == 1 ? 0 : 1);
                }
                else
                    bIsChecked = !bIsChecked;
            }
        }

        private void cvsMain_MouseLeave(object sender, MouseEventArgs e)
        {
            e.Handled = true;
            if (_isMouseDown)
            {
                _isMouseDown = false;
            }
        }
    }
}
