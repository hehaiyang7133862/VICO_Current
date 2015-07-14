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
    /// fileItemBar.xaml 的交互逻辑
    /// </summary>
    public partial class fileItemBar : UserControl
    {

        static GradientStopCollection focusGradientCollection = new GradientStopCollection() { new GradientStop(Colors.White, 0.25),  new GradientStop(Color.FromRgb(220, 220, 220), 1) };
        LinearGradientBrush focusBrushe = new LinearGradientBrush(focusGradientCollection,new Point(0,0.5),new Point(1,0.5));
        public fileMsg fMsg = new fileMsg();
        public fileItemBar()
        {
            InitializeComponent();
            focusState = false;
            cvsMain.Background = Brushes.Transparent;
        }
        public fileItemBar(string fileName, DateTime dtFileCreate)
        {
            InitializeComponent();
            fMsg.fileName = fileName;
            fMsg.DtCreate = dtFileCreate;
            lbFilename.Content = fileName;
            lbDt_Len.Content = fMsg.dtCreate;
            focusState = false;
            cvsMain.Background = Brushes.Transparent;
        }
        public fileItemBar(fileMsg file)
        {
            InitializeComponent();
            lbFilename.Content = file.fileName;
            lbDt_Len.Content = file.dtCreate ;
            focusState = false;
            cvsMain.Background = Brushes.Transparent;
        }
        public fileItemBar(FileInfo file)
        {
            InitializeComponent();
            lbFilename.Content = file.Name;
            fMsg.fileName = file.Name;
            lbDt_Len.Content = file.CreationTime + " " + (file.Length /1024).ToString() + "kb";
            fMsg.DtCreate = file.CreationTime;
            
            focusState = false;
            
            cvsMain.Background = Brushes.Transparent;
        }
        public bool focusState
        {
            get
            {
                return imgFocusOn.Visibility == Visibility.Visible;
            }
            set
            {
                if (value)
                {
                    imgFocusOn.Visibility = Visibility.Visible;
                }
                else
                {
                    imgFocusOn.Visibility = Visibility.Hidden;
                }
            }
        }
        bool isMousedown = false;
        private void cvsMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            cvsMain.Background = focusBrushe;
            isMousedown = true;
        }
        public bool isMoved
        {
            get;
            set;
        }
        private void cvsMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isMousedown)
            {
                isMousedown = false;

                if (!isMoved)
                {
                    focusState = !focusState;
                }
                else
                    isMoved = false;
            }
            cvsMain.Background = Brushes.Transparent;
        }

        private void cvsMain_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isMousedown)
            {
                isMousedown = false;
                cvsMain.Background = Brushes.Transparent;
            }
        }

        public double value
        {
            set
            {
                if (value > 99.999)
                    cvsProcess.Width = 0;
                else
                    cvsProcess.Width = value * 5;
                vm.debug(value.ToString());
            }
            get
            {
                return cvsProcess.Width / 5;
            }
        }
    }
}
