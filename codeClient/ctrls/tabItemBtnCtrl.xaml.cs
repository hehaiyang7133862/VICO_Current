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

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// tabItemBtnCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class tabItemBtnCtrl : UserControl
    {
        private int _curFocus = 0;
        /// <summary>
        /// 设置焦点
        /// </summary>
        public int focusNr
        {
            get
            {
                return _curFocus;
            }
            set
            {
                _curFocus = value;
                if (cvsMain.Children.Count > 0)
                {
                    foreach (tabItemBtnUnit itemBtn in cvsMain.Children)
                    {
                        itemBtn.focus = false;
                    }
                    if (_curFocus < 0 || _curFocus >= cvsMain.Children.Count)
                    {
                        _curFocus = 0;
                        (cvsMain.Children[0] as tabItemBtnUnit).focus = true;
                    }
                    else
                        (cvsMain.Children[_curFocus] as tabItemBtnUnit).focus = true;

                }
            }
        }
        private int _itemCount;
        /// <summary>
        /// 设置菜单数
        /// </summary>
        public int itemCount
        {
            get
            {
                return _itemCount;
            }
            set
            {
                cvsMain.Children.Clear();
                for (int i = 0; i < value; i++)
                {
                    tabItemBtnUnit itemBtn = new tabItemBtnUnit();
                    cvsMain.Children.Add(itemBtn);
                    Canvas.SetLeft(itemBtn, 85 * i);
                }
            }
        }

        public tabItemBtnCtrl()
        {
            InitializeComponent();
        }

    }
}
