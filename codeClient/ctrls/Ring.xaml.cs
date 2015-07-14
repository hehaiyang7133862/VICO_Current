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
    /// Ring.xaml 的交互逻辑
    /// </summary>
    public partial class Ring : UserControl
    {
        private SolidColorBrush _Color;
        /// <summary>
        /// 设置控件的颜色
        /// </summary>
        public SolidColorBrush Color
        {
            set
            {
                _Color = value;
                ellMain.Fill = _Color;
                ellMain.Stroke = _Color;
            }
        }

        public Ring()
        {
            InitializeComponent();
        }
    }
}
