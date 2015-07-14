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
    /// Interaction logic for cmpResultCtrl.xaml
    /// </summary>
    public partial class cmpResultCtrl : UserControl
    {
        Label[] lbLst = new Label[10];
        List<string> strLst = new List<string>(); 
        public cmpResultCtrl()
        {
            try
            {
                InitializeComponent();
                //textBlock1.Text = "111111111111111111111111111111111111111111\n222222222222222222222222222222222222222\n";
                for (int i = 0; i < lbLst.Length; i++)
                {
                    lbLst[i] = new Label();
                    lbLst[i].Background = Brushes.White;
                    lbLst[i].Width = 376;
                    cvsMain.Children.Add(lbLst[i]);
                    Canvas.SetTop(lbLst[i], 49 + i * 27);
                    Canvas.SetLeft(lbLst[i], 12);
                }
                this.Visibility = Visibility.Hidden;
            }
            catch
            {

            }
        }
        public void show()
        {
            this.Opacity = 1;
            this.Visibility = Visibility.Visible;
        }
        public void hide()
        {
            this.Visibility = Visibility.Hidden;
                
        }
        public void add(string str)
        {
            if (strLst.Count < lbLst.Length - 1)
            {
                

                lbLst[strLst.Count].Content = str;
                lbLst[strLst.Count].Visibility = Visibility.Visible;
                strLst.Add(str);
            }
            else
            {
                strLst.Add("...   ...");
                lbLst[lbLst.Length - 1].Content = "...   ...";
                lbLst[lbLst.Length - 1].Visibility = Visibility.Visible;
            }
        }
        public void clear()
        {
            strLst.Clear();
            for (int i = 0; i < lbLst.Length; i++)
            {
                lbLst[i].Content = "";
                lbLst[i].Visibility = Visibility.Hidden;
            }
        }

        private void btnHidenCtrl1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

    }
}
