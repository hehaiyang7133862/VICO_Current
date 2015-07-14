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
using nsDataMgr;
namespace nsVicoClient.ctrls
{
    /// <summary>
    /// Interaction logic for setIPAddrCtrl.xaml
    /// </summary>
    public partial class setIPAddrCtrl : UserControl
    {
        Label curCallbackLb;
        objUnit ipObj = valmoWin.dv.ItlPr[0];
        public setIPAddrCtrl()
        {
            InitializeComponent();
            this.Visibility = Visibility.Hidden;
        }
        public void init()
        {
            curCallbackLb = null;            
            this.Visibility = Visibility.Visible;
            string ipAddr = Properties.Settings.Default.IPAddr;
            string[] str = ipAddr.Split('.');
            if(str.Length == 4)
            {
                lbTmYear.Content = str[0];
                lbTmMonth.Content = str[1];
                lbTmDay.Content = str[2];
                lbTmHour.Content = str[3];
            }
        }
        public void init(Label lb)
        {
            curCallbackLb = lb;
            this.Visibility = Visibility.Visible;
            string ipAddr = Properties.Settings.Default.IPAddr;
            string[] str = ipAddr.Split('.');
            if (str.Length == 4)
            {
                lbTmYear.Content = str[0];
                lbTmMonth.Content = str[1];
                lbTmDay.Content = str[2];
                lbTmHour.Content = str[3];
            }
        }
        public void setCallbackLabel(Label lbCallback)
        {
            curCallbackLb = lbCallback;
        }
        private void fdCallBack(double value)
        {
            if ((int)value == 0)
            {
                if (lbCurFocus != null)
                {
                    lbCurFocus.Background = Brushes.Transparent;
                }
                if (lbCurFocus.Name == lbTmYear.Name)
                {
                    lbCurFocus = lbTmMonth;
                    //valmoWin.getIntValueFromNumKey(255, 0, " ", lbCurFocus, new Thickness(290, 810, 0, 0), fdobj);

                }
                else if (lbCurFocus.Name == lbTmMonth.Name)
                {
                    lbCurFocus = lbTmDay;
                    //valmoWin.getIntValueFromNumKey(255, 0, " ", lbCurFocus, new Thickness(290, 810, 0, 0), fdobj);
                }
                else if (lbCurFocus.Name == lbTmDay.Name)
                {
                    lbCurFocus = lbTmHour;
                    //valmoWin.getIntValueFromNumKey(255, 0, " ", lbCurFocus, new Thickness(290, 810, 0, 0), fdobj);
                }
                else if (lbCurFocus.Name == lbTmHour.Name)
                {
                    lbTmYear.Background = Brushes.Transparent;
                    lbTmMonth.Background = Brushes.Transparent;
                    lbTmDay.Background = Brushes.Transparent;
                    lbTmHour.Background = Brushes.Transparent;
                    return;
                }
                lbCurFocus.Background = Brushes.Green;
            }
            else
            {
                lbTmYear.Background = Brushes.Transparent;
                lbTmMonth.Background = Brushes.Transparent;
                lbTmDay.Background = Brushes.Transparent;
                lbTmHour.Background = Brushes.Transparent;
            }

        }

        Label lbCurFocus;
        private void mouseEnterFunc()
        {
        
        }
        private void lbTmYear_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lbCurFocus = lbTmYear;
            ipObj.vMaxStrNew = "255";
            ipObj.vMinStrNew = "0";
            //valmoWin.getIntValueFromNumKey(255, 0, " ", lbCurFocus, new Thickness(290, 810, 0, 0), fdobj);
            lbCurFocus.Background = Brushes.Green;
        }
        private void lbTmMonth_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lbCurFocus = lbTmMonth;
            //valmoWin.getIntValueFromNumKey(255, 0, " ", lbCurFocus, new Thickness(290, 810, 0, 0), fdobj);
            lbCurFocus.Background = Brushes.Green;

        }

        private void lbTmDay_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lbCurFocus = lbTmDay;
            //valmoWin.getIntValueFromNumKey(255, 0, " ", lbCurFocus, new Thickness(290, 810, 0, 0), fdobj);
            lbCurFocus.Background = Brushes.Green;

        }

        private void lbTmHour_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lbCurFocus = lbTmHour;
            //valmoWin.getIntValueFromNumKey(255, 0, " ", lbCurFocus, new Thickness(290, 810, 0, 0), fdobj);
            lbCurFocus.Background = Brushes.Green;

        }

        private void lbTmSetOk_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string ipAddr = lbTmYear.Content.ToString() + "." + lbTmMonth.Content.ToString() + "." + lbTmDay.Content.ToString() + "." + lbTmHour.Content.ToString();
            Properties.Settings.Default.IPAddr = ipAddr;
            Properties.Settings.Default.Save();
            if (curCallbackLb != null)
            {
                curCallbackLb.Content = Properties.Settings.Default.IPAddr;
            }
            this.Visibility = Visibility.Hidden;
        }
        private void lbTmSetCancle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

    }
}
