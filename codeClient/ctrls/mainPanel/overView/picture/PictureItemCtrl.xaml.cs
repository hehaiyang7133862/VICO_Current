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
    public partial class PictureItemCtrl : UserControl
    {
        private bool _bIsSelected = false;
        /// <summary>
        /// 设置或获取图片是否被选中
        /// </summary>
        public bool Selected
        {
            set
            {
                _bIsSelected = value;
                brdSelected.Visibility = (_bIsSelected == true) ? Visibility.Visible : Visibility.Hidden;
            }
            get
            {
                return _bIsSelected;
            }
        }

        private string _imageSource;
        public string ImageSource
        {
            set
            {
                _imageSource = value;
            }
            get
            {
                return _imageSource;
            }
        }

        public PictureItemCtrl()
        {
            InitializeComponent();
        }        
    }
}
