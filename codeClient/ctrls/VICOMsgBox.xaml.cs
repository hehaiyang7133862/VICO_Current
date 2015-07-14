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
using System.Windows.Shapes;

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// VICOMsgBox.xaml 的交互逻辑
    /// </summary>
    public partial class VICOMsgBox : Window
    {
        public string msgTitle
        {
            set
            {
                lbTitle.Content = value;
            }
            get
            {
                return lbTitle.Content.ToString();
            }
        }

        public string msgContent
        {
            set
            {
                lbContent.Text = value;
            }
            get
            {
                return lbContent.Text;
            }
        }

        private VICOMsgBox()
        {
            InitializeComponent();
        }

        public static bool? Show(string title, string msg)
        {
            var msgBox = new VICOMsgBox();
            msgBox.msgTitle = title;
            msgBox.msgContent = msg;
            return msgBox.ShowDialog();
        }

        private void btnOK_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
