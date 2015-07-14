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
    public partial class multipleChoiceCtrl : UserControl
    {
        private objUnit _ctrlObj;
        /// <summary>
        /// 设置对象
        /// </summary>
        public string objName
        {
            set
            {
                _ctrlObj = valmoWin.dv.getObj(value);
                if (_ctrlObj != null)
                {
                    _ctrlObj.addHandle(refushState);
                }
            }
        }
        private int _selected = 0;
        /// <summary>
        /// 设置对象当前选中的值
        /// </summary>
        public int Selected
        {
            set
            {
                if (value >= 0 && value <= _itemCount - 1)
                {
                    _selected = value;

                    Canvas.SetLeft(bdSelected, _selected * _unitWidth);
                }
            }
        }
        private int _itemCount = 1;
        /// <summary>
        /// 设置选项个数
        /// </summary>
        public int ItemCount
        {
            set
            {
                if (value > 0)
                {
                    _itemCount = value;

                    for (int i = 0; i < _itemCount; i++)
                    {
                        Border bd = new Border();
                        bd.Height = _unitHeight;
                        bd.Width = _unitWidth;
                        bd.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xD4, 0xD4, 0xD4));
                        bd.BorderThickness = new Thickness(0, 1, 1, 1);
                        bd.Tag = i;

                        sPanel.Children.Add(bd);
                    }
                }
            }
        }
        private int _unitHeight = 32;
        /// <summary>
        /// 设置空间高度
        /// </summary>
        public int UnitHeight
        {
            set
            {
            }
        }
        private int _unitWidth = 100;
        /// <summary>
        /// 设置控件宽度
        /// </summary>
        public int UnitWidth
        {
            set
            {
 
            }
        }

        public multipleChoiceCtrl()
        {
            InitializeComponent();
        }

        private void refushState(objUnit obj)
        {
            if (obj.value >= 0 && obj.value <= _itemCount - 1)
            {
                Selected = obj.value;
            }
        }
    }
}
