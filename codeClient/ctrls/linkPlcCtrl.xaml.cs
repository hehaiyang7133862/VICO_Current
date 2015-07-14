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
using System.Threading;
using System.Windows.Threading;
using nsDataMgr;

namespace nsVicoClient.ctrls
{
    public partial class linkPlcCtrl : UserControl
    {
        public bool flagForceClose = false;
        public bool active
        {
            get
            {
                return true;
            }
            set
            {
                if (value)
                {
                    tbIPAddr.Text = "";
                    lbDis.Foreground = Brushes.LightGreen;
                    lbDis.Content = valmoWin.dv.getCurDis("LanKey1209");
                    btnLink.Visibility = Visibility.Hidden;
                }
                else
                {
                    btnLink.Visibility = Visibility.Visible;
                    lbDis.Foreground = Brushes.Red;
                    lbDis.Content = valmoWin.dv.getCurDis("LanKey1211");
                    List<string> ipLst = valmoWin.dv.getLocalIpLst();
                    if(ipLst.Count == 0)
                    {
                        tbIPAddr.Text = valmoWin.dv.getCurDis("LanKey1212");
                    }
                    else
                    {
                        tbIPAddr.Text = "PC IP Addr: \n";
                        foreach(string ipAddr in ipLst)
                        {
                            tbIPAddr.Text += ipAddr + "\n";
                        }
                    }
                    
                }
            }
        }

        public linkPlcCtrl()
        {
            InitializeComponent();

            valmoWin.dv.ItlPr[29].addHandle(ipChangeFunc);
            this.Visibility = Visibility.Hidden;
        }
        private void ipChangeFunc(objUnit obj)
        {
            lbIpAddr.Content = valmoWin.dv.getPlcIpAddr();
        }
        private void disposeFunc()
        {
            lbIpAddr.Content = valmoWin.dv.getPlcIpAddr();
        }
        public void init()
        {

            if (LinkMgr.isOnLine())
            {
                active = true;
            }
            else
            {
                active = false;
            }
            this.Visibility = Visibility.Visible;
        }
        public void show()
        {
            if (LinkMgr.isOnLine())
            {
                active = true;
            }
            else
            {
                active = false;
            }
            lbIpAddr.Content = valmoWin.dv.getPlcIpAddr();
            this.Opacity = 1;
            this.Visibility = Visibility.Visible;
            //errNr = 0;
        }
        public void hide()
        {
            this.Visibility = Visibility.Hidden;
        }
        private void cvsBackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (LinkMgr.isOnLine())
            {
                this.Visibility = Visibility.Hidden;
            }
        }

        private void lbIpAddr_MouseDown(object sender, MouseButtonEventArgs e)
        {
            valmoWin.dv.ItlPr[29].note = valmoWin.dv.getPlcIpAddr();
            valmoWin.SIpSetPanel.show(valmoWin.dv.ItlPr[29], disposeFunc);
        }

        private void imgClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (active == false)
            {
                return;
            }

            flagForceClose = true;
            this.Visibility = Visibility.Hidden;
        }

        private void btnLink_MouseUp(object sender, MouseButtonEventArgs e)
        {
            btnLink.Visibility = Visibility.Hidden;
            //vm.getTm();

            //if (!valmoWin.dv.getLink2())
            //{
            //    //vm.getTm();
            //    btnLink.Visibility = Visibility.Visible;
            //}
            //else
            //{

            //}
            valmoWin.dv.relink();
        }

        private void btnLink_Loaded(object sender, RoutedEventArgs e)
        {

        }

    }
}
