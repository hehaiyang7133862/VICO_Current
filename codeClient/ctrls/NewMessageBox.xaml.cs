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
    /// NewMessageBox.xaml 的交互逻辑
    /// </summary>
    public partial class NewMessageBox : Window
    {
        private NewMessageBox()
        {
            InitializeComponent();
        }

          public new string Title
        {
            get { return this.lbTitle.Content.ToString(); }
            set { this.lbTitle.Content = value; }
        }

        public string Message
        {
            get { return this.lbText.Content.ToString(); }
            set { this.lbText.Content = value; }
        }

        /// <summary>
        /// 静态方法 模拟MESSAGEBOX.Show方法
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        public static bool? Show()
        {
            var msgBox = new NewMessageBox();
            return msgBox.ShowDialog();
        }

        private void btnConfirm_MouseUp(object sender, MouseButtonEventArgs e)
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
