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
    /// fileMsgBar.xaml 的交互逻辑
    /// </summary>
    public partial class fileMsgBar : UserControl
    {
        public fileMsg file = new fileMsg();
        public fileMsgBar()
        {
            InitializeComponent();
        }
        public fileMsgBar(string fileName, DateTime dtFileCreate)
        {
            InitializeComponent();
            file.fileName = fileName;
            file.DtCreate = dtFileCreate;
            lbFileName.Content = fileName;
            lbFileMdTm.Content = file.dtCreate;
        }
        public fileMsgBar(fileMsg file)
        {
            InitializeComponent();
            lbFileName.Content = file.fileName;
            lbFileMdTm.Content = file.dtCreate;
        }
        public bool focusState
        {
            get
            {
                return imgFocus.Visibility == Visibility.Visible;
            }
            set
            {
                //imgFocusOn.Visibility = value ? Visibility.Visible : Visibility.Hidden;
                //imgFocusOff.Visibility = value ? Visibility.Hidden : Visibility.Visible;
                imgFocus.Visibility = value ? Visibility.Visible : Visibility.Hidden;
            }
        }
        private void cvsMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            focusState = !focusState;
        }
        public void init(fileMsg file)
        {
            lbFileName.Content = file.fileName;
            lbFileMdTm.Content = file.dtCreate;
        }
    }

    public class fileMsg
    {
        public string fileName;
        public DateTime DtCreate;
        public DateTime dtEnd;
        public string fileCreate;
        public string dtCreate
        {
            get
            {
                
                return DtCreate.ToString("f");
            }
        }
    }
}
