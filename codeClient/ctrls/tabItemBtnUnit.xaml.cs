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
    /// tabItemBtnUnit.xaml 的交互逻辑
    /// </summary>
    public partial class tabItemBtnUnit : UserControl
    {
        private bool _bIsFocus = false;
        /// <summary>
        /// 设置焦点
        /// </summary>
        public bool focus
        {
            get
            {
                return _bIsFocus;
            }
            set
            {
                _bIsFocus = value;
                if (_bIsFocus)
                {
                    btn.Background = new SolidColorBrush(Color.FromRgb(132, 221, 193));
                }
                else
                {
                    btn.Background = new SolidColorBrush(Color.FromRgb(244, 244, 244));
                }
            }
        }
        public tabItemBtnUnit()
        {
            InitializeComponent();
        }
    }
}
