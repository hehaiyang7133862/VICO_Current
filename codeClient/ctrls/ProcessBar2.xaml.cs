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
    /// 自定义进度条
    /// </summary>
    public partial class ProcessBar2 : UserControl
    {
        private int _height = 10;
        /// <summary>
        /// 获取或设置控件的高度
        /// </summary>
        public int pHeight
        {
            set
            {
                _height = value;

                this.Height = _height;
                lValue.StrokeThickness = _height * 2;
                lRange.StrokeThickness = _height * 2 / 4;
            }
        }
        private int _width = 300;
        /// <summary>
        /// 获取或设置控件的宽度
        /// </summary>
        public int pWidth
        {
            set
            {
                _width = value;

                this.Width = _width;
                lRange.X2 = _width;
            }
        }
        private int _value;
        /// <summary>
        /// 获取或设置控件的当前数量
        /// </summary>
        public int Value
        {
            set
            {
                _value = value;

                lValue.X2 = _value * _width / 100;
            }
        }

        public ProcessBar2()
        {
            InitializeComponent();
        }
    }
}
