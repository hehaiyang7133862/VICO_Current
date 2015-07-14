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
using System.IO;

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// fileItemCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class fileItemCtrl : UserControl
    {
        public MouseButtonEventHandler mouseUp
        {
            set
            {
                btnDel.MouseUp += value;
            }
        }
        public FileInfo fileInfo = null;
        public fileItemCtrl()
        {
            InitializeComponent();
        }
        public fileItemCtrl(FileInfo file)
        {
            InitializeComponent();
            init(file);
        }
        public void init(FileInfo file)
        {
            fileInfo = file;
            lbFilename.Content = file.Name;
            lbDis.Content = file.LastWriteTime + "  " + file.Length;
            if (file.DirectoryName == interpretorPage.prgDir.FullName && file.Name == valmoWin.SIprCtrl.prgFileName)
            {
                getFlag = true;
            }
            else
            {
                getFlag = false;
            }
        }
        /// <summary>
        /// 
        /// 只是当前项是否为选焦点状态
        /// </summary>
        public bool focusState
        {
            get
            {
                return imgFocus.Opacity == 1;// imgFocusOn.Opacity == 1;
            }
            set
            {
                imgFocus.Opacity = value ? 1 : 0;
                btnDel.Visibility = value ? Visibility.Visible : Visibility.Hidden;
                //imgFocusOn.Opacity = value ? 1 : 0;
            }
        }
        /// <summary>
        /// 表示当前文件为正在使用的项目文件
        /// </summary>
        public bool getFlag
        {
            get
            {
                return imgFocusOn.Opacity == 1;
            }
            set
            {
                imgFocusOn.Opacity = value ? 1 : 0;
            }
        }
        //bool isMousedown = false;
        private void cvsMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //imgFocus.Opacity = 1;
            //isMousedown = true;
        }

        private void cvsMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //if (isMousedown)
            //{
            //    imgFocus.Opacity = 1;
            //    isMousedown = false;
            //    //focusState = !focusState;
            //}
        }

        private void cvsMain_MouseLeave(object sender, MouseEventArgs e)
        {
            //isMousedown = false;
            //imgFocus.Opacity = 0;
        }


    }
}
