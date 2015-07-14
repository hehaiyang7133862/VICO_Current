using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// pdfBtn.xaml 的交互逻辑
    /// </summary>
    public partial class pdfBtn : UserControl
    {
        public pdfBtn()
        {
            InitializeComponent();
        }

        bool isMousedown = false;
        private void cvsMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isMousedown = true;
            btnDown.Visibility = Visibility.Visible;
        }

        private void cvsMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isMousedown)
            {
                isMousedown = false;
                btnDown.Visibility = Visibility.Hidden;

                //instructionsPage ip = new instructionsPage(Environment.CurrentDirectory + @"\instructions\WPFLocalizationGuidance.pdf");
                //ip.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                //ip.ShowDialog();
            }
        }

        private void cvsMain_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isMousedown)
            {
                btnDown.Visibility = Visibility.Hidden;
                isMousedown = false;
            }
        }

    }
}
