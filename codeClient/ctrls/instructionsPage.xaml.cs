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
    /// instructionsPage.xaml 的交互逻辑
    /// </summary>
    public partial class instructionsPage : Window
    {
        public instructionsPage(string fileName)
        {
            InitializeComponent();

            PDFReader pr = new PDFReader(fileName);
            wfh.Child = pr;
        }
    }
}
