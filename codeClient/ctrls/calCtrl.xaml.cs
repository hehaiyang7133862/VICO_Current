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

namespace nsVicoClient.ctrls
{
    public enum CalType:byte
    {
        nullType = 0, //不显示日期和星期
        weekType = 1, //显示星期，不显示日期
        dateType = 2, //显示日期，不显示星期
        wdType = 3, //既显示星期，又显示日期

    }
    /// <summary>
    /// Interaction logic for calCtrl.xaml
    /// </summary>
    public partial class calCtrl : UserControl
    {
        public Label[] lb = new Label[42];
        Label[] lbDay = new Label[42];
        Label[] lbDayFocus = new Label[42];
        public Label[] lbMons = new Label[6];
        int[] lnM = new int[] { 7, 35, 64, 92, 121, 149, 178, 206, 235, 263, 292, 321 };
        DateTime curDate;
        bool flagDateShow = true;
        bool flagWeekShow = true;
        

        public calCtrl()
        {
            InitializeComponent();

            init();
            getCurDate(out curDate);
            //makeLabel();
            lbYear.Content = curDate.Year;
            lbMonth.Content = curDate.Month;
            makeCalender();
            setCurDate(curDate);
            
        }

        private void getCurDate(out DateTime dt)
        {
            dt = DateTime.Now;
        }
        private void setCurDate(DateTime dt)
        {
            for (int i = 0; i < 42; i++)
            {
                //if (lb[i].Background != lbBack.Background)
                //{
                //    lb[i].Background = lbBack.Background;
                //}
                if (lbDay[i].IsEnabled == false)
                {
                    lbDay[i].Background = Brushes.Silver;
                }
                else
                {
                    lbDay[i].Background = Brushes.Transparent;
                }
                if (Int32.Parse(lbYear.Content.ToString()) == dt.Year && Int32.Parse(lbMonth.Content.ToString()) == dt.Month)
                {
                    if (Int32.Parse(lbDay[i].Content.ToString()) == dt.Day && lbDay[i].IsEnabled == true)
                    {
                        lbDay[i].Background = new SolidColorBrush(Color.FromArgb(255,120,221,255));
                    }
                }
            }
        }
        void init()
        {
            lbDay[0] = lb1;
            lbDay[1] = lb2;
            lbDay[2] = lb3;
            lbDay[3] = lb4;
            lbDay[4] = lb5;
            lbDay[5] = lb6;
            lbDay[6] = lb7;
            lbDay[7] = lb8;
            lbDay[8] = lb9;
            lbDay[9] = lb10;
            lbDay[10] = lb11;
            lbDay[11] = lb12;
            lbDay[12] = lb13;
            lbDay[13] = lb14;
            lbDay[14] = lb15;
            lbDay[15] = lb16;
            lbDay[16] = lb17;
            lbDay[17] = lb18;
            lbDay[18] = lb19;
            lbDay[19] = lb20;
            lbDay[20] = lb21;
            lbDay[21] = lb22;
            lbDay[22] = lb23;
            lbDay[23] = lb24;
            lbDay[24] = lb25;
            lbDay[25] = lb26;
            lbDay[26] = lb27;
            lbDay[27] = lb28;
            lbDay[28] = lb29;
            lbDay[29] = lb30;
            lbDay[30] = lb31;
            lbDay[31] = lb32;
            lbDay[32] = lb33;
            lbDay[33] = lb34;
            lbDay[34] = lb35;
            lbDay[35] = lb36;
            lbDay[36] = lb37;
            lbDay[37] = lb38;
            lbDay[38] = lb39;
            lbDay[39] = lb40;
            lbDay[40] = lb41;
            lbDay[41] = lb42;

            lbDayFocus[0] = lb01;
            lbDayFocus[1] = lb02;
            lbDayFocus[2] = lb03;
            lbDayFocus[3] = lb04;
            lbDayFocus[4] = lb05;
            lbDayFocus[5] = lb06;
            lbDayFocus[6] = lb07;
            lbDayFocus[7] = lb08;
            lbDayFocus[8] = lb09;
            lbDayFocus[9] = lb010;
            lbDayFocus[10] = lb011;
            lbDayFocus[11] = lb012;
            lbDayFocus[12] = lb013;
            lbDayFocus[13] = lb014;
            lbDayFocus[14] = lb015;
            lbDayFocus[15] = lb016;
            lbDayFocus[16] = lb017;
            lbDayFocus[17] = lb018;
            lbDayFocus[18] = lb019;
            lbDayFocus[19] = lb020;
            lbDayFocus[20] = lb021;
            lbDayFocus[21] = lb022;
            lbDayFocus[22] = lb023;
            lbDayFocus[23] = lb024;
            lbDayFocus[24] = lb025;
            lbDayFocus[25] = lb026;
            lbDayFocus[26] = lb027;
            lbDayFocus[27] = lb028;
            lbDayFocus[28] = lb029;
            lbDayFocus[29] = lb030;
            lbDayFocus[30] = lb031;
            lbDayFocus[31] = lb032;
            lbDayFocus[32] = lb033;
            lbDayFocus[33] = lb034;
            lbDayFocus[34] = lb035;
            lbDayFocus[35] = lb036;
            lbDayFocus[36] = lb037;
            lbDayFocus[37] = lb038;
            lbDayFocus[38] = lb039;
            lbDayFocus[39] = lb040;
            lbDayFocus[40] = lb041;
            lbDayFocus[41] = lb042;
            for (int i = 0; i < 42; i++)
            {
                lbDayFocus[i].Tag = i;
                lbDayFocus[i].MouseDown += new MouseButtonEventHandler(lbDay_MouseDown);
            }

        }
        public void refresh()
        {
            curDate = DateTime.Now;
            lbYear.Content = curDate.Year;
            lbMonth.Content = curDate.Month;
            makeCalender();
            setCurDate(curDate);

            if (lbYear.Content.ToString() == curFocusYear && lbMonth.Content.ToString() == curFocusMonth)
            {
                bdFocus.Visibility = Visibility.Visible;
            }
            else
            {
                bdFocus.Visibility = Visibility.Hidden;
            }
        }
        private void makeLabel()
        {
            for (int i = 0; i < 42; i++)
            {
                lb[i] = new Label();
                cvsCal.Children.Add(lb[i]);
                Canvas.SetTop(lb[i], 160 + i / 7 * 41);
                Canvas.SetLeft(lb[i], 38 + i % 7 * 53);
                lb[i].Width = 55;
                lb[i].Height = 30;
                lb[i].Background = Brushes.Blue;
                lb[i].HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                lb[i].VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                lb[i].FontSize = 16;
                lb[i].FontFamily = new FontFamily("Sony Sketch EF");
            }
            for (int i = 0; i < 6; i++)
            {
                lbMons[i] = new Label();
                cvsCal.Children.Add(lbMons[i]);
                Canvas.SetTop(lbMons[i], 160 + i * 41);
                Canvas.SetLeft(lbMons[i], 5);
                lbMons[i].Width = 35;
                lbMons[i].Height = 30;
                //lbMons[i].Background = Brushes.Red;
                lbMons[i].Foreground = new SolidColorBrush(Color.FromArgb(255, 60, 206, 255));
                //lbMons[i].Background = lnMonth.Stroke;
                lbMons[i].HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                lbMons[i].VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                lbMons[i].FontSize = 16;
                lbMons[i].FontWeight = FontWeights.Bold;
                lbMons[i].Content = i;
                lbMons[i].FontFamily = new FontFamily("Michroma");

            }
            makeCalender();
        }
        void makeCalender()
        {
            int tmpCurMonth = Int32.Parse(lbMonth.Content.ToString());
            //Canvas.SetLeft(lnMonth, lnM[tmpCurMonth - 1]);
            int firstDay = WeekDay(Int32.Parse(lbYear.Content.ToString()), Int32.Parse(lbMonth.Content.ToString()), 1);
            if (firstDay == 0)
                firstDay = 7;
            int lastMonthNums = 0;
            if (lbMonth.Content.ToString() == "1")
                lastMonthNums = MonthDays(Int32.Parse(lbYear.Content.ToString()) - 1, 1);
            else
                lastMonthNums = MonthDays(Int32.Parse(lbYear.Content.ToString()), Int32.Parse(lbMonth.Content.ToString()) - 1);
            int curMonthNums = MonthDays(Int32.Parse(lbYear.Content.ToString()), Int32.Parse(lbMonth.Content.ToString()));
            for (int i = firstDay; i > 0; i--)
            {
                lbDay[i - 1].Content = lastMonthNums--;
                lbDay[i - 1].IsEnabled = false;
                lbDay[i - 1].FontWeight = FontWeights.Normal;
                //lb[i - 1].Foreground = Brushes.Red;
                //lbDay[i - 1].Background = Brushes.Blue;
            }
            for (int curMonth = 1; curMonth <= curMonthNums; curMonth++)
            {
                lbDay[curMonth + firstDay - 1].Content = curMonth;
                lbDay[curMonth + firstDay - 1].IsEnabled = true;
                //lbDay[curMonth + firstDay - 1].Background = Brushes.Transparent;
                lbDay[curMonth + firstDay - 1].FontWeight = FontWeights.Bold;
            }
            int nextMonthStartDate = firstDay + curMonthNums;
            int tmp = 1;
            while (nextMonthStartDate < 42)
            {
                lbDay[nextMonthStartDate].Content = tmp++;
                lbDay[nextMonthStartDate].IsEnabled = false;
                lbDay[nextMonthStartDate].FontWeight = FontWeights.Normal;
                //lbDay[nextMonthStartDate].Background = Brushes.Silver;
                nextMonthStartDate++;
            }


       }

        string curFocusYear = "";
        string curFocusMonth = "";
        private void lbDay_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Label lb = sender as Label;

            if (lbDay[Int32.Parse(lb.Tag.ToString())].IsEnabled == false)
            {
                if (Int32.Parse(lbDay[Int32.Parse(lb.Tag.ToString())].Content.ToString()) > 15)
                {
                    bdBack_MouseDown(null, null);
                }
                else
                {
                    bdNext_MouseDown(null, null);
                }
            }
            else
            {
                bdFocus.Visibility = Visibility.Visible;

                Canvas.SetLeft(bdFocus, Canvas.GetLeft(lb));
                Canvas.SetTop(bdFocus, Canvas.GetTop(lb));
                curFocusYear = lbYear.Content.ToString();
                curFocusMonth = lbMonth.Content.ToString();
                //curFocusDay = lb.Tag.ToString();
            }
        }
        private void bdBack_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            int curMonth = Int32.Parse(lbMonth.Content.ToString());
            if (curMonth == 1)
            {
                lbYear.Content = Int32.Parse(lbYear.Content.ToString()) - 1;
                lbMonth.Content = 12;
            }
            else
                lbMonth.Content = Int32.Parse(lbMonth.Content.ToString()) - 1;
            getCurDate(out curDate);
            makeCalender();
            setCurDate(curDate);
            if (lbYear.Content.ToString() == curFocusYear && lbMonth.Content.ToString() == curFocusMonth)
            {
                bdFocus.Visibility = Visibility.Visible;
            }
            else
            {
                bdFocus.Visibility = Visibility.Hidden;
            }
        }
        private void bdNext_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int curMonth = Int32.Parse(lbMonth.Content.ToString());
            if (curMonth == 12)
            {
                lbYear.Content = Int32.Parse(lbYear.Content.ToString()) + 1;
                lbMonth.Content = 1;
            }
            else
                lbMonth.Content = Int32.Parse(lbMonth.Content.ToString()) + 1;
            getCurDate(out curDate);
            makeCalender();
            setCurDate(curDate);
            if (lbYear.Content.ToString() == curFocusYear && lbMonth.Content.ToString() == curFocusMonth)
            {
                bdFocus.Visibility = Visibility.Visible;
            }
            else
            {
                bdFocus.Visibility = Visibility.Hidden;
            }
        }
        int WeekDay(int year, int month, int day) //根据输入的日期，返回对应的星期
        {
            int i;
            int run = 0, ping = 0;
            long sum;

            for (i = 1; i < year; i++)
            {
                if (i % 4 == 0 && i % 100 != 0 || i % 400 == 0)
                    run++;
                else
                    ping++;
            }
            sum = 366 * run + 365 * ping;
            for (i = 1; i < month; i++)
                sum += MonthDays(year, i);
            sum += day;   //计算总天数 
            return (int)sum % 7;
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
                    vm.printLn("这是一个错误的月份！");
                    return 0;
            }
        }

        private void imgDate_MouseDown(object sender, MouseButtonEventArgs e)
        {

            //if (imgDateNo.Visibility == Visibility.Visible)
            //{
            //    flagDateShow = true;
            //    imgDateNo.Visibility = Visibility.Hidden;
            //    imgDateYes.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    flagDateShow = false;
            //    imgDateYes.Visibility = Visibility.Hidden;
            //    imgDateNo.Visibility = Visibility.Visible;
            //}
        }

        private void imgWeek_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //if (imgWeekNo.Visibility == Visibility.Visible)
            //{
            //    flagWeekShow = true;
            //    imgWeekNo.Visibility = Visibility.Hidden;
            //    imgWeekYes.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    flagWeekShow = false;
            //    imgWeekYes.Visibility = Visibility.Hidden;
            //    imgWeekNo.Visibility = Visibility.Visible;
            //}

        }
        public string getCurDate(out string strDate,out string strWeek)
        {
            DateTime dt = DateTime.Now;
            strWeek = "";
            strDate = "";
            if (flagDateShow)
            {
                strDate = dt.Year + "年" + dt.Month + "月" + dt.Day + "日";
            }
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    strWeek = "周一   ";
                    break;
                case DayOfWeek.Tuesday:
                    strWeek = "周二   ";
                    break;
                case DayOfWeek.Wednesday:
                    strWeek = "周三   ";
                    break;
                case DayOfWeek.Thursday:
                    strWeek = "周四   ";
                    break;
                case DayOfWeek.Friday:
                    strWeek = "周五   ";
                    break;
                case DayOfWeek.Saturday:
                    strWeek = "周六   ";
                    break;
                case DayOfWeek.Sunday:
                    strWeek = "周日   ";
                    break;
            }
            //lbtmWeek.Content = strWeek;
            //lbtmWeek2.Content = strWeek;
            if (!flagWeekShow)
            {
                strWeek = "";
            }
            //lbtmYear.Content = dt.Year;
            //lbtmMonth.Content = dt.Month;
            //lbtmDay.Content = dt.Day;

            //lbtmYear2.Content = dt.Year;
            //lbtmMonth2.Content = dt.Month;
            //lbtmDay2.Content = dt.Day;
            return strDate + strWeek;
        }

    }
}
