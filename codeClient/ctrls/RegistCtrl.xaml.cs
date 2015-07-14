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
    /// RegistCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class RegistCtrl : UserControl
    {
        private double v1;
        private double v2;
        private double v3;

        private NumericTouchpad NumInput;

        public RegistCtrl()
        {
            InitializeComponent();

            NumInput = new NumericTouchpad();
            NumInput.Visibility = Visibility.Hidden;
            cvsMain.Children.Add(NumInput);

            this.Visibility = Visibility.Hidden;
        }

        public void show()
        {
            if (SysBasicInfo.remainingTrialDays > 0)
            {
                btnClose.Visibility = Visibility.Visible;

                if ((SysBasicInfo.remainingTrialDays < 6))
                {
                    lbLastDays.Content = SysBasicInfo.remainingTrialDays + "Days";
                }
            }
            else
            {
                btnClose.Visibility = Visibility.Hidden;

                lbLastDays.Content = "Authorization has expired";

                resetBtn.bIsResetBtnEnable = false;

                if (valmoWin.dv.SysPr[105].value == 3)
                {
                    valmoWin.dv.KeyPr[3].valueNew = 1;
                    valmoWin.dv.KeyPr[3].valueNew = 0;
                }
            }

            this.Visibility = Visibility.Visible;

            lbV1.Content = valmoWin.dv.SysPr[110].value;
            lbV3.Content = valmoWin.dv.SysPr[112].value;
            lbV4.Content = valmoWin.dv.SysPr[13].value;

        }

        private Brush brushActive = new SolidColorBrush(Color.FromArgb(0xff, 0x00, 0xb4, 0xe1));
        private Brush brushNormal = new SolidColorBrush(Color.FromArgb(0xff, 0x30, 0x30, 0x30));

        private void input1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            input1.BorderBrush = brushActive;

            NumInput.init(999999, 0, "", "0", "", 0, cancel1, setInput1);
        }
        private void input2_MouseUp(object sender, MouseButtonEventArgs e)
        {
            input2.BorderBrush = brushActive;
            NumInput.init(999999, 0, "", "0", "", 0, cancel2, setInput2);
        }
        private void input3_MouseUp(object sender, MouseButtonEventArgs e)
        {
            input3.BorderBrush = brushActive;
            NumInput.init(999999, 0, "", "0", "", 0, cancel3, setInput3);
        }

        private void setInput1(double vNew)
        {
            v1 = vNew;
            input1.Content = v1;
            input1.BorderBrush = brushNormal;
        }
        private void cancel1()
        {
            input1.BorderBrush = brushNormal;
        }
        private void setInput2(double vNew)
        {
            v2 = vNew;
            input2.Content = v2;
            input2.BorderBrush = brushNormal;
        }
        private void cancel2()
        {
            input2.BorderBrush = brushNormal;
        }
        private void setInput3(double vNew)
        {
            v3 = vNew;
            input3.Content = v3;
            input3.BorderBrush = brushNormal;
        }
        private void cancel3()
        {
            input3.BorderBrush = brushNormal;
        }

        private void btnConfirm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            btnConfirm.Background = Brushes.Gray;
        }

        private void btnConfirm_MouseUp(object sender, MouseButtonEventArgs e)
        {
            btnConfirm.Background = Brushes.Transparent;

            int temp1 = Convert.ToInt32(lbV1.Content);
            int temp2 = Convert.ToInt32(lbV4.Content);

            int RawNum = (temp1 + (temp1 << 8) + temp2);

            if (v1 == (RawNum & 0x0000A4DA) && (v2 == (RawNum & 0x0000D17E)))
            {
                if (v3 == (RawNum & 0x00003F76))
                {
                    int[] date = new int[3];
                    date[0] = 2999;
                    date[1] = 1;
                    date[2] = 1;

                    Lasal32.SetData(date, (uint)valmoWin.dv.SysPr[226].valueNew, 12);
                    valmoWin.dv.SysPr[227].setValue(3);

                    this.Visibility = Visibility.Hidden;
                    resetBtn.bIsResetBtnEnable = true;
                }
                else
                {
                    int SystemData = valmoWin.dv.SysPr[13].valueNew;
                    int year = Convert.ToInt32((SystemData >> 16) & 0x0fff);
                    int month = Convert.ToInt32((SystemData >> 12) & 0x0000f);
                    int day = Convert.ToInt32((SystemData >> 4) & 0x00000ff);

                    DateTime dateNow;
                    try
                    {
                        dateNow = new DateTime(year, month, day);
                    }
                    catch
                    {
                        dateNow = DateTime.Now;
                        return;
                    }

                    DateTime dateLimit = dateNow + new TimeSpan(30, 0, 0, 0, 0);

                    int[] date = new int[3];

                    date[0] = dateLimit.Year;
                    date[1] = dateLimit.Month;
                    date[2] = dateLimit.Day;


                    Lasal32.SetData(date, (uint)valmoWin.dv.SysPr[226].valueNew, 12);
                    valmoWin.dv.SysPr[227].setValue(3);

                    this.Visibility = Visibility.Hidden;
                    resetBtn.bIsResetBtnEnable = true;
                }
            }
        }

        private void btnConfirm_MouseLeave(object sender, MouseEventArgs e)
        {
            btnConfirm.Background = Brushes.Transparent;
        }

        private void btnClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            btnClose.Background = Brushes.Gray;
        }

        private void btnClose_MouseLeave(object sender, MouseEventArgs e)
        {
            btnClose.Background = Brushes.Transparent;
        }

        private void btnClose_MouseUp(object sender, MouseButtonEventArgs e)
        {
            btnClose.Background = Brushes.Transparent;

            this.Visibility = Visibility.Hidden;
        }
    }
}
