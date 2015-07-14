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
    /// Interaction logic for ergTimeInterval.xaml
    /// </summary>
    public partial class ergTimeInterval : UserControl
    {
        public delegate void timeDel();

        timeSelect.callbackHandle timeCalcHandle; 
        public ergTimeInterval()
        {
            InitializeComponent();
            timeCalcHandle = new timeSelect.callbackHandle(timeCalc);
            timeSelect1.setHandle(timeCalcHandle);
            timeSelect2.setHandle(timeCalcHandle);
        }
        void timeCalc()
        {
            int year1 = timeSelect1.year;
            int year2 = timeSelect2.year;
            int month1 = timeSelect1.month;
            int month2 = timeSelect2.month;
            int day1 = timeSelect1.day;
            int day2 = timeSelect2.day;
            int hour1 = timeSelect1.hour;
            int hour2 = timeSelect2.hour;

            int totalDays = 0;
            int totalHours = 0;
            if (year2 < year1)
            {
                timeSelect2.setTimeSelectAlarm(tmSelectAlarmType.year);
                return;
            }
            else if (year1 == year2)
            {
                if (month2 < month1)
                {
                    timeSelect2.setTimeSelectAlarm(tmSelectAlarmType.month);
                    return;
                }
                else if (month2 == month1)
                {
                    if (day2 < day1)
                    {
                        timeSelect2.setTimeSelectAlarm(tmSelectAlarmType.day);
                        return;
                    }
                    else if (day2 == day1)
                    {
                        if (hour2 < hour1)
                        {
                            timeSelect2.setTimeSelectAlarm(tmSelectAlarmType.hour);
                            return;
                        }
                    }
                    else
                    {
                        totalDays += day2 - day1;
                    }
                }
                else
                {
                    for (int i = 1; i < month2 - month1; i++)  //month
                    {
                        totalDays += daysOfMonth(year1, month1 + i);
                    }
                    totalDays += daysOfMonth(year1, month1) - day1 + 1; // month1,days
                    totalDays += day2;

                }

            }
            else
            {
                for (int i = 1; i < year2 - year1; i++)
                {
                    for (int j = 1; j < 12; j++)
                    {
                        totalDays += daysOfMonth(year1 + i, j);
                    }
                }
                for (int i = month1 + 1; i < 13; i++)   // year1 ,months
                {
                    totalDays += daysOfMonth(year1, i);
                }
                totalDays += daysOfMonth(year1, month1) - day1 + 1;  //year1,days
                for (int i = 1; i < month2; i++)
                {
                    totalDays += daysOfMonth(year2, i);
                }
                totalDays += day2;

            }
            if (hour2 < hour1)
            {

                totalDays -= 1;
                totalHours = 24 + hour2 - hour1;
            }
            lbTotalDays.Content = totalDays;
            lbTotalHours.Content = totalHours;
        }
        int countDaysByYear(int year)
        {
            int days = 0;
            for (int i = 1; i < 13; i++)
            {
                days += daysOfMonth(year, i);
            }
            return days;
        }
        int daysOfMonth(int year, int month)//根据输入的年号和月份，返回该月的天数    
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
        void test()
        {
            
        }
    }
}
