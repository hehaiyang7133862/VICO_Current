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
//using interfaceMgr;
namespace nsVicoClient.ctrls
{
    /// <summary>
    /// Interaction logic for filterTimeCtrl.xaml
    /// </summary>
    public partial class filterTimeCtrl : UserControl
    {
        Label lbCurFocus;
        DateTime dtStart;
        DateTime dtEnd;
        DateTime dtTmpStart;
        DateTime dtTmpEnd;

        public filterTimeCtrl()
        {
            InitializeComponent();

            DateTime now = DateTime.Now;

            dtStart = new DateTime(now.Year - 1, now.Month, now.Day, now.Hour, 0, 0);
            dtEnd = new DateTime(now.Year + 1, now.Month, now.Day, now.Hour, 0, 0);
            lbTmYearEnd.Content = dtEnd.Year;
            lbTmMonthEnd.Content = dtEnd.Month;
            lbTmDayEnd.Content = dtEnd.Day;
            lbTmHourEnd.Content = dtEnd.Hour;

            lbTmYearStart.Content = dtStart.Year;
            lbTmMonthStart.Content = dtStart.Month;
            lbTmDayStart.Content = dtStart.Day;
            lbTmHourStart.Content = dtStart.Hour;
            dtTmpStart = dtStart;
            dtTmpEnd = dtEnd;
        }
        public nullEvent fdStateChange
        {
            get;
            set;
        }
        private void disposeFunc()
        {
            if (lbCurFocus != null)
                lbCurFocus.Background = Brushes.Transparent;
        }

        public bool check(recUnit ergObj)
        {
            if (ergObj.dtStart > dtStart && ergObj.dtStart < dtEnd)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void handleBeforeQuit()
        {
            lbTmYearStart.Background = Brushes.Transparent;
            lbTmMonthStart.Background = Brushes.Transparent;
            lbTmDayStart.Background = Brushes.Transparent;
            lbTmHourStart.Background = Brushes.Transparent;

            lbTmYearEnd.Background = Brushes.Transparent;
            lbTmMonthEnd.Background = Brushes.Transparent;
            lbTmDayEnd.Background = Brushes.Transparent;
            lbTmHourEnd.Background = Brushes.Transparent;
        }
        private void lbTmSetOk_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DateTime tmpDtStart = new DateTime(Int32.Parse(lbTmYearStart.Content.ToString()), Int32.Parse(lbTmMonthStart.Content.ToString()), Int32.Parse(lbTmDayStart.Content.ToString()), Int32.Parse(lbTmHourStart.Content.ToString()),0,0);
            DateTime tmpDtEnd = new DateTime(Int32.Parse(lbTmYearEnd.Content.ToString()), Int32.Parse(lbTmMonthEnd.Content.ToString()), Int32.Parse(lbTmDayEnd.Content.ToString()), Int32.Parse(lbTmHourEnd.Content.ToString()), 0, 0);
            if (tmpDtStart < tmpDtEnd)
            {
                dtStart = tmpDtStart;
                dtEnd = tmpDtEnd;
            }
            else
            {
                dtStart = tmpDtEnd;
                dtEnd = tmpDtStart;
            }
            if (fdStateChange != null)
                fdStateChange();

            cvsBg.Visibility = Visibility.Hidden;
        }
        private void lbTmSetCancle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            cvsBg.Visibility = Visibility.Hidden;
        }

        private void lbTmYearStart_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lbCurFocus = lbTmYearStart;

            valmoWin.dv.ItlPr[0].vMaxDblNew = DateTime.Now.Year + 1;
            valmoWin.dv.ItlPr[0].vMinDblNew = 2010;
            valmoWin.dv.ItlPr[0].value = Int32.Parse(lbCurFocus.Content.ToString());
            valmoWin.dv.ItlPr[0].unitType = UnitType.DgtType;
            valmoWin.dv.ItlPr[0].clear();
            valmoWin.dv.ItlPr[0].addLb(lbCurFocus);
            valmoWin.SNumKeyPanel.init(valmoWin.dv.ItlPr[0], objUnit.unit_year, disposeFunc);
            lbCurFocus.Background = Brushes.Green;

        }

        private void lbTmMonthStart_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lbCurFocus = lbTmMonthStart;
            int monthMax = 1;
            if (dtTmpStart.Year == dtTmpEnd.Year)
            {
                monthMax = Int32.Parse(lbTmMonthEnd.Content.ToString());
            }
            valmoWin.dv.ItlPr[0].clear();
            valmoWin.dv.ItlPr[0].vMaxDblNew = 12;
            valmoWin.dv.ItlPr[0].vMinDblNew = 1;
            valmoWin.dv.ItlPr[0].value = Int32.Parse(lbCurFocus.Content.ToString());
            valmoWin.dv.ItlPr[0].unitType = UnitType.Tm_month;
            valmoWin.dv.ItlPr[0].clear();
            //valmoWin.dv.ItlPr[0].addLb(lbTmMonthStart);
            valmoWin.dv.ItlPr[0].addHandle(handleStartMonth);
            valmoWin.SNumKeyPanel.init(valmoWin.dv.ItlPr[0], objUnit.unit_year, disposeFunc);
            //valmoWin.getIntValueFromNumKey(12, 1, "月", lbCurFocus, new Thickness(300, 600, 0, 0), fdobj);
            lbCurFocus.Background = Brushes.Green;

        }
        private void handleStartMonth(objUnit obj)
        {
            if (lbCurFocus != null)
                lbCurFocus.Content = obj.value;
            int dayMax = 31;
            int curYear = Int32.Parse(lbTmYearStart.Content.ToString());
            switch (obj.value)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    dayMax = 31;
                    break;
                case 2:
                    {
                        if (curYear % 4 == 0 && curYear % 100 != 0 || curYear % 400 == 0)
                            dayMax = 29;
                        else
                            dayMax = 28;
                    }
                    break;
                case 4:
                case 6:
                case 9:
                case 11:
                    dayMax = 30;
                    break;
            }
            if (Int32.Parse(lbTmDayStart.Content.ToString()) > dayMax)
                lbTmDayStart.Content = 1;
        }
        private void lbTmDayStart_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lbCurFocus = lbTmDayStart;
            valmoWin.dv.ItlPr[0].clear();
            int dayMax = 0;
            string strMon = lbTmMonthStart.Content.ToString();
            int curYear = Int32.Parse(lbTmYearStart.Content.ToString());

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
            valmoWin.dv.ItlPr[0].vMaxDblNew = dayMax;
            valmoWin.dv.ItlPr[0].vMinDblNew = 1;
            valmoWin.dv.ItlPr[0].value = Int32.Parse(lbCurFocus.Content.ToString());
            valmoWin.dv.ItlPr[0].unitType = UnitType.Tm_day;
            valmoWin.dv.ItlPr[0].clear();
            valmoWin.dv.ItlPr[0].addLb(lbCurFocus);
            valmoWin.SNumKeyPanel.init(valmoWin.dv.ItlPr[0], objUnit.unit_year, disposeFunc);
            lbCurFocus.Background = Brushes.Green;
        }

        private void lbTmHourStart_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lbCurFocus = lbTmHourStart;
            int hourMax = 24;
            valmoWin.dv.ItlPr[0].clear();

            valmoWin.dv.ItlPr[0].vMinDblNew = 0;
            valmoWin.dv.ItlPr[0].value = Int32.Parse(lbCurFocus.Content.ToString());
            valmoWin.dv.ItlPr[0].unitType = UnitType.Tm_hour;
            valmoWin.dv.ItlPr[0].clear();
            valmoWin.dv.ItlPr[0].addLb(lbCurFocus);

            valmoWin.dv.ItlPr[0].vMaxDblNew = hourMax;
            valmoWin.SNumKeyPanel.init(valmoWin.dv.ItlPr[0], objUnit.unit_year, disposeFunc);
            lbCurFocus.Background = Brushes.Green;
        }

        private void lbTmYearEnd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lbCurFocus = lbTmYearEnd;

            valmoWin.dv.ItlPr[0].vMaxDblNew = DateTime.Now.Year + 1;
            valmoWin.dv.ItlPr[0].vMinDblNew = 2010;
            valmoWin.dv.ItlPr[0].value = Int32.Parse(lbCurFocus.Content.ToString());
            valmoWin.dv.ItlPr[0].unitType = UnitType.Tm_year;
            valmoWin.dv.ItlPr[0].clear();
            valmoWin.dv.ItlPr[0].addLb(lbCurFocus);
            valmoWin.SNumKeyPanel.init(valmoWin.dv.ItlPr[0],objUnit.unit_year, disposeFunc);
            lbCurFocus.Background = Brushes.Green;
        }

        private void lbTmMonthEnd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lbCurFocus = lbTmMonthEnd;

            valmoWin.dv.ItlPr[0].vMaxDblNew = 12;
            valmoWin.dv.ItlPr[0].vMinDblNew = 1;
            valmoWin.dv.ItlPr[0].value = Int32.Parse(lbCurFocus.Content.ToString());
            valmoWin.dv.ItlPr[0].unitType = UnitType.Tm_month;
            valmoWin.dv.ItlPr[0].clear();
            valmoWin.dv.ItlPr[0].addHandle(handleEndMonth);
            valmoWin.SNumKeyPanel.init(valmoWin.dv.ItlPr[0], objUnit.unit_year, disposeFunc);
            lbCurFocus.Background = Brushes.Green;
        }

        private void handleEndMonth(objUnit obj)
        {
            if (lbCurFocus != null)
                lbCurFocus.Content = obj.value;
            int dayMax = 31;
            int curYear = Int32.Parse(lbTmYearEnd.Content.ToString());
            switch (obj.value)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    dayMax = 31;
                    break;
                case 2:
                    {
                        if (curYear % 4 == 0 && curYear % 100 != 0 || curYear % 400 == 0)
                            dayMax = 29;
                        else
                            dayMax = 28;
                    }
                    break;
                case 4:
                case 6:
                case 9:
                case 11:
                    dayMax = 30;
                    break;
            }
            if (Int32.Parse(lbTmDayEnd.Content.ToString()) > dayMax)
                lbTmDayEnd.Content = 1;
        }

        private void lbTmDayEnd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lbCurFocus = lbTmDayEnd;
            int dayMin = 1;
            string strMon = lbTmMonthEnd.Content.ToString();
            int curYear = Int32.Parse(lbTmYearEnd.Content.ToString());
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
            valmoWin.dv.ItlPr[0].clear();
            valmoWin.dv.ItlPr[0].vMaxDblNew = dayMax;
            valmoWin.dv.ItlPr[0].vMinDblNew = dayMin;
            valmoWin.dv.ItlPr[0].value = Int32.Parse(lbCurFocus.Content.ToString());
            valmoWin.dv.ItlPr[0].unitType = UnitType.Tm_day;
            valmoWin.dv.ItlPr[0].clear();
            valmoWin.dv.ItlPr[0].addLb(lbCurFocus);
            valmoWin.SNumKeyPanel.init(valmoWin.dv.ItlPr[0], disposeFunc);
            lbCurFocus.Background = Brushes.Green;
        }

        private void lbTmHourEnd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lbCurFocus = lbTmHourEnd;
            int hourMin = 1;
            valmoWin.dv.ItlPr[0].clear();
            valmoWin.dv.ItlPr[0].vMaxDblNew = 24;
            valmoWin.dv.ItlPr[0].vMinDblNew = hourMin;
            valmoWin.dv.ItlPr[0].value = Int32.Parse(lbCurFocus.Content.ToString());
            valmoWin.dv.ItlPr[0].unitType = UnitType.Tm_hour;
            valmoWin.dv.ItlPr[0].clear();
            valmoWin.dv.ItlPr[0].addLb(lbCurFocus);
            valmoWin.SNumKeyPanel.init(valmoWin.dv.ItlPr[0], disposeFunc);
            lbCurFocus.Background = Brushes.Green;
        }

        private bool isMouseDown = false;
        private Point mousePoint;

        private void cvsUser_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            Point pMouseDown = e.GetPosition(cvsBg);
            valmoWin.SNumKeyPanel.curPos = pMouseDown;

            isMouseDown = true;
            mousePoint = e.GetPosition(cvsBg);
        }
        private void cvsBg_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown == true)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point theMousePoint = e.GetPosition(cvsBg);
                    double tmpLeft = Canvas.GetLeft(cvsTime) + theMousePoint.X - mousePoint.X;
                    double tmpTop = Canvas.GetTop(cvsTime) + theMousePoint.Y - mousePoint.Y;
                    Canvas.SetLeft(cvsTime, tmpLeft);
                    Canvas.SetTop(cvsTime, tmpTop);
                    mousePoint = theMousePoint;
                }
            }

        }
        private void cvsBg_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = false;
        }

        private void cvsBg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            cvsBg.Visibility = Visibility.Hidden;
        }

        private bool bIsMouseDown = false;
        private void cvsMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            bIsMouseDown = true;
            cvsMain.Background = new SolidColorBrush(Color.FromArgb(255, 234, 234, 234));
        }
        private void cvsMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (bIsMouseDown == true)
            {
                bIsMouseDown = false;
                cvsMain.Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));

                valmoWin.refresh();

                cvsBg.Visibility = Visibility.Visible;
            }
        }
        private void cvsMain_MouseLeave(object sender, MouseEventArgs e)
        {
            e.Handled = true;
            bIsMouseDown = false;
            cvsMain.Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
        }
    }
}
