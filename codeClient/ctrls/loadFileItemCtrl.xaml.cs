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
    /// loadFileItemCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class loadFileItemCtrl : UserControl
    {
        public loadFileItemCtrl()
        {
            InitializeComponent();
            cvsMain.Background = Brushes.Transparent;
            this.nr = "";
            this.addr = "";
            this.value = "";
            lbState.Content = "";
        }
        public loadFileItemCtrl(string nr, string addr, string value)
        {
            InitializeComponent();
            cvsMain.Background = Brushes.Transparent;
            this.nr = nr;
            this.addr = addr;
            this.value = value;
            lbState.Content = "";
        }
        public void init(string nr, string addr, string value)
        {
            cvsMain.Background = Brushes.Transparent;
            this.nr = nr;
            this.addr = addr;
            this.value = value;
            lbState.Content = "";
        }
        public string nr
        {
            get
            {
                return lbNr.Content.ToString();
            }
            set
            {
                lbNr.Content = value;
            }
        }
        public string addr
        {
            get
            {
                return lbAddr.Content.ToString();
            }
            set
            {
                lbAddr.Content = value;
            }
        }
        public string value
        {
            get
            {
                return lbValue.Content.ToString();
            }
            set
            {
                lbValue.Content = value;
            }
        }
        public bool focusState
        {
            get
            {
               return  cvsMain.Background == Brushes.LightBlue;
            }
            set
            {
                cvsMain.Background = value ? Brushes.LightBlue : Brushes.Transparent;
            }
        }
        public bool flagLoadOk
        {
            get
            {
                return lbState.Content.ToString() == "√";
            }
            set
            {
                if (value)
                {
                    lbAddr.Foreground = Brushes.White;
                    lbState.Foreground = Brushes.GreenYellow;
                    lbState.Content = "√";
                }
                else
                {
                    lbAddr.Foreground = Brushes.Red;
                    lbState.Foreground = Brushes.Red;
                    lbState.Content = "×";
                }
                //lbState.Content = value ? "√" : "×";
            }

        }
       
    }
}
