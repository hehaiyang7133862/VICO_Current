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
    /// Interaction logic for timeSelect.xaml
    /// </summary>
    public partial class timeSelect : UserControl
    {
        double leftOfCvsHour = 0;
        double leftOfCvsYear = 0;
        double leftOfCvsMonth = 0;
        double leftOfCvsDay = 0;
        public timeSelect()
        {
            InitializeComponent();
            leftOfCvsHour = this.cvsHour.Margin.Left;
            leftOfCvsYear = this.cvsYear.Margin.Left;
            leftOfCvsMonth = this.cvsMonth.Margin.Left;
            leftOfCvsDay = this.cvsDay.Margin.Left;
            DateTime dtNow = DateTime.Now;
            setTime(dtNow.Year, dtNow.Month, dtNow.Day, dtNow.Hour);
        }

        bool isHourMouseDown = false;
        Point mousePointHour;
        object mouseCtrlHour;
        public delegate void callbackHandle();
        callbackHandle callBack;
        public void setHandle(callbackHandle handle)
        {
            callBack = handle;
        }
        private void cvsHour_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.isHourMouseDown = true;
            this.mousePointHour = e.GetPosition(this.cvsBack);
            this.mouseCtrlHour = this.cvsHour;
        }

        private void cvsRecord_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.isHourMouseDown)
            {
                checkHours();
            }
            if (isMouseDownYear)
            {
                checkYears();
            }
            if (this.isMouseDownMonth)
            {
                checkMonths();
            }
            if (this.isMouseDownDay)
            {
                checkDays();
            }
        }
        private void checkYears()
        {
            this.isMouseDownYear = false;
            int topSetted = 0;
            int top = (int)(this.cvsYear as Canvas).Margin.Top;
            double selectedLenth = top % 35;
            if (selectedLenth < 0)
            {
                if (selectedLenth < -18)
                {
                    topSetted = (top / 35 - 1) * 35;
                }
                else
                {
                    topSetted = top / 35 * 35;
                }
            }
            else
            {
                if (selectedLenth > 17)
                {
                    topSetted = (top / 35 + 1) * 35;
                }
                else
                {
                    topSetted = top / 35 * 35;
                }
            }

            int year = Int32.Parse((cvsYear.Children[6] as Label).Content.ToString()) - topSetted / 35;
            int yearFirst = year - 6;

            for (int i = 0; i < 13; i++)
            {
                (cvsYear.Children[i] as Label).Content = yearFirst + i;
            }
            lbYear.Content = (cvsYear.Children[6] as Label).Content;
            (this.cvsYear as Canvas).Margin = new Thickness(leftOfCvsYear, 0, 0, 0);
            checkDays();

        }
        private void checkMonths()
        {
            this.isMouseDownMonth = false;
            int topSetted = 0;
            int top = (int)(this.cvsMonth as Canvas).Margin.Top;
            double selectedLenth = top % 35;
            if (selectedLenth < 0)
            {
                if (selectedLenth < -18)
                {
                    topSetted = (top / 35 - 1) * 35;
                }
                else
                {
                    topSetted = top / 35 * 35;
                }
            }
            else
            {
                if (selectedLenth > 17)
                {
                    topSetted = (top / 35 + 1) * 35;
                }
                else
                {
                    topSetted = top / 35 * 35;
                }
            }
            //int top = (int)(this.cvsMonth as Canvas).Margin.Top;
            int month = Int32.Parse((cvsMonth.Children[6] as Label).Content.ToString()) - topSetted / 35;

            int monthFirst = (month + 6) % 12;
            if (monthFirst == 0)
                monthFirst = 12;

            for (int i = 0; i < 13; i++)
            {
                (cvsMonth.Children[i] as Label).Content = monthFirst;
                if (monthFirst == 12)
                    monthFirst = 1;
                else
                    monthFirst++;
            }
            vm.printLn(month.ToString());

            lbMonth.Content = (cvsMonth.Children[6] as Label).Content;
            (this.cvsMonth as Canvas).Margin = new Thickness(leftOfCvsMonth, 0, 0, 0);

            checkDays();
        }
        private void checkDays()
        {
            this.isMouseDownDay = false;
            int topSetted = 0;
            int top = (int)(this.cvsDay as Canvas).Margin.Top;
            double selectedLenth = top % 35;
            if (selectedLenth < 0)
            {
                if (selectedLenth < -18)
                {
                    topSetted = (top / 35 - 1) * 35;
                }
                else
                {
                    topSetted = top / 35 * 35;
                }
            }
            else
            {
                if (selectedLenth > 17)
                {
                    topSetted = (top / 35 + 1) * 35;
                }
                else
                {
                    topSetted = top / 35 * 35;
                }
            }
            //int top = (int)(this.cvsDay as Canvas).Margin.Top;
            int day = Int32.Parse((cvsDay.Children[6] as Label).Content.ToString()) - top / 35;
            int year = Int32.Parse((cvsYear.Children[6] as Label).Content.ToString());
            int month = Int32.Parse((cvsMonth.Children[6] as Label).Content.ToString());
            int totalDays = MonthDays(year, month);
            int dayFirst = (day + totalDays - 6) % totalDays;
            if (dayFirst == 0)
                dayFirst = totalDays;

            for (int i = 0; i < 13; i++)
            {

                (cvsDay.Children[i] as Label).Content = dayFirst;
                if (dayFirst == totalDays)
                    dayFirst = 1;
                else
                    dayFirst++;
            }
            vm.printLn(day.ToString());
            lbDay.Content = (cvsDay.Children[6] as Label).Content;
            (this.cvsDay as Canvas).Margin = new Thickness(leftOfCvsDay, 0, 0, 0);

            if (callBack != null)
                callBack();

        }
        private void checkHours()
        {
            this.isHourMouseDown = false;
            int topSetted = 0;
            int top = (int)(this.cvsHour as Canvas).Margin.Top;
            double selectedLenth = top % 35;
            if (selectedLenth < 0)
            {
                if (selectedLenth < -18)
                {
                    topSetted = (top / 35 - 1) * 35;
                }
                else
                {
                    topSetted = top / 35 * 35;
                }
            }
            else
            {
                if (selectedLenth > 17)
                {
                    topSetted = (top / 35 + 1) * 35;
                }
                else
                {
                    topSetted = top / 35 * 35;
                }
            }

            //int top = (int)(this.cvsHour as Canvas).Margin.Top;
            int hour = Int32.Parse((cvsHour.Children[6] as Label).Content.ToString()) - topSetted / 35;
            int hourFirst = (hour + 18) % 24;
            //if (hourFirst == 0)
            //    hourFirst = 24;

            for (int i = 0; i < 13; i++)
            {
                (cvsHour.Children[i] as Label).Content = hourFirst;
                if (hourFirst == 23)
                    hourFirst = 0;
                else
                    hourFirst++;
            }
            vm.printLn(hour.ToString());
            lbHour.Content = (cvsHour.Children[6] as Label).Content;
            (this.cvsHour as Canvas).Margin = new Thickness(leftOfCvsHour, 0, 0, 0);
            if (callBack != null)
                callBack();
        }
        private void cvsRecord_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.isHourMouseDown == true)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point theMousePoint = e.GetPosition(this.cvsBack);
                    //Console.WriteLine("{0},{1}", theMousePoint.X, theMousePoint.Y);
                    double tmpTop = (this.cvsHour as Canvas).Margin.Top + theMousePoint.Y - this.mousePointHour.Y;
                    //Console.WriteLine("left: {0}", tmpLeft);
                    (this.cvsHour as Canvas).Margin = new Thickness(leftOfCvsHour, tmpTop, 0, 0);
                    this.mousePointHour = theMousePoint;
                }
            }
            if (this.isMouseDownYear == true)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point theMousePoint = e.GetPosition(this.cvsBack);
                    //Console.WriteLine("{0},{1}", theMousePoint.X, theMousePoint.Y);
                    double tmpTop = (this.cvsYear as Canvas).Margin.Top + theMousePoint.Y - this.mousePointYear.Y;
                    //Console.WriteLine("left: {0}", tmpLeft);
                    //if (tmpTop > 0)
                    //    tmpTop = 0;
                    //else 
                        
                        //if (tmpTop < -280)
                        //tmpTop = -280;
                    (this.cvsYear as Canvas).Margin = new Thickness(leftOfCvsYear, tmpTop, 0, 0);
                    this.mousePointYear = theMousePoint;
                }

            }
            if (this.isMouseDownMonth == true)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point theMousePoint = e.GetPosition(this.cvsBack);
                    double tmpTop = (this.cvsMonth as Canvas).Margin.Top + theMousePoint.Y - this.mousePointMonth.Y;
                    //Console.WriteLine("left: {0}", tmpLeft);
                    //if (tmpTop > 0)
                    //    tmpTop = 0;
                    //else 

                    //if (tmpTop < -280)
                    //tmpTop = -280;
                    (this.cvsMonth as Canvas).Margin = new Thickness(leftOfCvsMonth, tmpTop, 0, 0);
                    this.mousePointMonth = theMousePoint;
                }
            }
            if (this.isMouseDownDay == true)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point theMousePoint = e.GetPosition(this.cvsBack);
                    double tmpTop = (this.cvsDay as Canvas).Margin.Top + theMousePoint.Y - this.mousePointDay.Y;
                    //Console.WriteLine("left: {0}", tmpLeft);
                    //if (tmpTop > 0)
                    //    tmpTop = 0;
                    //else 

                    //if (tmpTop < -280)
                    //tmpTop = -280;
                    (this.cvsDay as Canvas).Margin = new Thickness(leftOfCvsDay, tmpTop, 0, 0);
                    this.mousePointDay = theMousePoint;
                }
            }

        }

        bool isMouseDownYear = false;
        Point mousePointYear;
        object mouseCtrlYear;
        private void cvsYear_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.isMouseDownYear = true;
            this.mousePointYear = e.GetPosition(this.cvsBack);
            this.mouseCtrlYear = this.cvsYear;
        }

        bool isMouseDownMonth = false;
        Point mousePointMonth;
        object mouseCtrlMonth;
        private void cvsMonth_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.isMouseDownMonth = true;
            this.mousePointMonth = e.GetPosition(this.cvsBack);
            this.mouseCtrlMonth = this.cvsMonth;
        }
        bool isMouseDownDay = false;
        Point mousePointDay;
        object mouseCtrlDay;
        private void cvsDay_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.isMouseDownDay = true;
            this.mousePointDay = e.GetPosition(this.cvsBack);
            this.mouseCtrlDay = this.cvsDay;
        }
        int MonthDays(int year, int month)//根据输入的年号和月份，返回该月的天数    
        {
            switch (month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12: return 31;
                case 4:
                case 6:
                case 9:
                case 11: return 30;
                case 2: if (year % 4 == 0 && year % 100 != 0 || year % 400 == 0)
                        return 29;
                    else
                        return 28;
                default:
                    vm.perror("这是一个错误的月份！");
                    return 0;
            }
        }

        private void cvsRecord_MouseLeave(object sender, MouseEventArgs e)
        {
            if (this.isHourMouseDown)
            {
                checkHours();
            }
            if (isMouseDownYear)
            {
                checkYears();
            }
            if (this.isMouseDownMonth)
            {
                checkMonths();
            }
            if (this.isMouseDownDay)
            {
                checkDays();
            }
        }
        private void setTime(int year, int month, int day, int hour)
        {
            int yearFirst = year - 6;
            for (int i = 0; i < 13; i++)
            {
                (cvsYear.Children[i] as Label).Content = yearFirst + i;
            }
            lbYear.Content = year;
            int monthFirst = (month + 6) % 12;
            if (monthFirst == 0)
                monthFirst = 12;
            for (int i = 0; i < 13; i++)
            {
                (cvsMonth.Children[i] as Label).Content = monthFirst;
                if (monthFirst == 12)
                    monthFirst = 1;
                else
                    monthFirst++;
            }
            lbMonth.Content = month;
            int totalDays = MonthDays(year, month);
            int dayFirst = (day + totalDays - 6) % totalDays;
            if (dayFirst == 0)
                dayFirst = totalDays;

            for (int i = 0; i < 13; i++)
            {

                (cvsDay.Children[i] as Label).Content = dayFirst;
                if (dayFirst == totalDays)
                    dayFirst = 1;
                else
                    dayFirst++;
            }
            lbDay.Content = day;

            int hourFirst = (hour + 18) % 24;
            //if (hourFirst == 0)
            //    hourFirst = 24;

            for (int i = 0; i < 13; i++)
            {
                (cvsHour.Children[i] as Label).Content = hourFirst;
                if (hourFirst == 23)
                    hourFirst = 0;
                else
                    hourFirst++;
            }
            lbHour.Content = hour;
        }
        public int year
        {
            get
            {
                return Int32.Parse(lbYear.Content.ToString());
            }
        }
        public int month
        {
            get
            {
                return Int32.Parse(lbMonth.Content.ToString());
            }
        }
        public int day
        {
            get
            {
                return Int32.Parse(lbDay.Content.ToString());
            }
        }
        public int hour
        {
            get
            {
                return Int32.Parse(lbHour.Content.ToString());
            }
        }
        public void setTimeSelectAlarm(tmSelectAlarmType type)
        {
            switch (type)
            {
               case tmSelectAlarmType.year:
                    lbYear.Foreground = Brushes.Red;
                break;
               case tmSelectAlarmType.month:
                lbMonth.Foreground = Brushes.Red;
                break;
               case tmSelectAlarmType.day:
                lbDay.Foreground = Brushes.Red;
                break;
               case tmSelectAlarmType.hour:
                lbHour.Foreground = Brushes.Red;
                break;

            }
        }
        public void clearTimeSelectAlarm()
        {
            lbYear.Foreground = Brushes.Black;
            lbMonth.Foreground = Brushes.Black;
            lbDay.Foreground = Brushes.Black;
            lbHour.Foreground = Brushes.Black;
        }            
    }
    public enum tmSelectAlarmType : byte
    {
        year,
        month,
        day,
        hour
    }
}
