﻿using System;
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
using System.Runtime.InteropServices;
using nsDataMgr;
using nsVicoClient;

namespace nsVicoClient.ctrls
{
    public partial class dateCtrl : UserControl
    {
        private Label lbCurFocus;

        public dateCtrl()
        {
            InitializeComponent();

            this.Visibility = Visibility.Hidden;
        }
        public void show()
        {
            this.Visibility = Visibility.Visible;
            cvsTimeSet.Visibility = Visibility.Hidden;

            calCtrl1.refresh();
        }
        private void lbTmSet_MouseDown(object sender, MouseButtonEventArgs e)
        {
            cvsTimeSet.Visibility = Visibility.Visible;

            DateTime dt = valmoWin.SysTime;

            lbTmYear.Content = dt.Year;
            lbTmMonth.Content = dt.Month;
            lbTmDay.Content = dt.Day;

            lbTmHour.Content = dt.Hour.ToString("00");
            lbTmMin.Content = dt.Minute.ToString("00");
            lbTmSec.Content = dt.Second.ToString("00");
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct SystemTime
        {
            public ushort wYear;
            public ushort wMonth;
            public ushort wDayOfWeek;
            public ushort wDay;
            public ushort wHour;
            public ushort wMinute;
            public ushort wSecond;
            public ushort wMilliseconds;
        } 
        [DllImport("kernel32.dll",SetLastError=true)]
        public static extern bool SetLocalTime(ref SystemTime st);

        private void lbTmSetOk_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int time = (Convert.ToInt32(lbTmHour.Content) << 24) | (Convert.ToInt32(lbTmMin.Content) << 16) | (Convert.ToInt32(lbTmSec.Content) << 8);
            int date = (Convert.ToInt32(lbTmYear.Content) << 16) | (Convert.ToInt32(lbTmMonth.Content) << 12) | (Convert.ToInt32(lbTmDay.Content) << 4);

            valmoWin.dv.SysPr[13].setValue(date);
            valmoWin.dv.SysPr[14].setValue(time);

            SystemTime newTm = new SystemTime();
            newTm.wYear = Convert.ToUInt16(valmoWin.SysTime.Year);
            newTm.wMonth = Convert.ToUInt16(valmoWin.SysTime.Month);
            newTm.wDay = Convert.ToUInt16(valmoWin.SysTime.Day);
            newTm.wHour = Convert.ToUInt16(valmoWin.SysTime.Hour);
            newTm.wMinute = Convert.ToUInt16(valmoWin.SysTime.Minute);
            newTm.wSecond = Convert.ToUInt16(valmoWin.SysTime.Day);
            newTm.wMilliseconds = 0;

            SetLocalTime(ref newTm);

            calCtrl1.refresh();
            cvsTimeSet.Visibility = Visibility.Hidden;
        }
        private void lbTmSetCancle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            cvsTimeSet.Visibility = Visibility.Hidden;
        }

        private void disposeFunc()
        {
            if (lbCurFocus != null)
            {
                lbCurFocus.Background = Brushes.Transparent;
            }
        }
        private void confirmFunc(double newValue)
        {
            if (lbCurFocus != null)
            {
                lbCurFocus.Background = Brushes.Transparent;
                lbCurFocus.Content = newValue;
            }
        }

        private void lbTmYear_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lbCurFocus = lbTmYear;
            lbCurFocus.Background = Brushes.Green;
            valmoWin.SNumInput.init(2020, 2010, "year", lbCurFocus.Content.ToString(), "", 1, disposeFunc, confirmFunc);
        }
        private void lbTmMonth_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lbCurFocus = lbTmMonth;
            lbCurFocus.Background = Brushes.Green;
            valmoWin.SNumInput.init(12, 1, "month", lbCurFocus.Content.ToString(), "", 1, disposeFunc, confirmFunc);

        }
        private void lbTmDay_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lbCurFocus = lbTmDay;
            string strMon = lbTmMonth.Content.ToString();
            int curYear = Int32.Parse(lbTmYear.Content.ToString());
            int dayMax = 0;
            switch (strMon)
            {
                case "1":
                case "3":
                case "5":
                case "7":
                case "8":
                case "10":
                case "12":
                    dayMax = 31;
                    break;
                case "2":
                    {
                    if (curYear % 4 == 0 && curYear % 100 != 0 || curYear % 400 == 0)
                        dayMax = 29;
                    else
                        dayMax = 28;
                    }
                    break;
                case "4":
                case "6":
                case "9":
                case "11":
                    dayMax = 30;
                    break;
            }  
            lbCurFocus.Background = Brushes.Green;
            valmoWin.SNumInput.init(dayMax, 1, "day", lbCurFocus.Content.ToString(), "", 1, disposeFunc, confirmFunc);

        }
        private void lbTmHour_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lbCurFocus = lbTmHour;
            lbCurFocus.Background = Brushes.Green;
            valmoWin.SNumInput.init(21, 1, "hour", lbCurFocus.Content.ToString(), "", 1, disposeFunc, confirmFunc);
        }
        private void lbTmMin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lbCurFocus = lbTmMin;
            lbCurFocus.Background = Brushes.Green;
            valmoWin.SNumInput.init(60, 1, "min", lbCurFocus.Content.ToString(), "", 1, disposeFunc, confirmFunc);
        }
        private void lbTmSec_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lbCurFocus = lbTmSec;
            lbCurFocus.Background = Brushes.Green;
            valmoWin.SNumInput.init(60, 1, "second", lbCurFocus.Content.ToString(), "", 1, disposeFunc, confirmFunc);
        }

        private void cvsBackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}
