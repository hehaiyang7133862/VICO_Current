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
    public partial class SPCDetailCtrl : UserControl
    {
        private SPCVariable _spcObject;

        public SPCDetailCtrl()
        {
            InitializeComponent();
        }

        public void Initialize(SPCVariable spcV)
        {
            _spcObject = spcV;

            _spcObject.ReSampleHandle += refush;

            lbVariableName.SetResourceReference(Label.ContentProperty, "TP_" + _spcObject.CurObj.serialNum);
            lbSampleSize.Content = _spcObject.SampleSize.ToString();

            lbUSL.Content = tbUSL.Content = _spcObject.USL.ToString("0.00");
            lbLSL.Content = tbLSL.Content = _spcObject.LSL.ToString("0.00");
            lbUnitUSL.Content = lbUnitLSL.Content = _spcObject.Unit;

            tbSampleSize.Content = _spcObject.SampleSize.ToString();
            tbSamplePeriod.Content = _spcObject.SamplePeriod.ToString();
            tbStatisticsSampleCount.Content = _spcObject.StatisticsSampleCount.ToString();
            tbCpkThreshold.Content = _spcObject.CpkWarningThreshold.ToString();
            tbAlarmDelay.Content = _spcObject.AlarmDelay.ToString();
        }

        private double[] distributionDataX = new double [26];

        private void refush()
        {
            lbStartTime.Content = _spcObject.StartTime.ToString();
            lbEndTime.Content = _spcObject.EndTime.ToString();
            lbCpk.Content = _spcObject.CurSample.Cpk.ToString("0.00");

            if (_spcObject.UCLx > _spcObject.USL)
            {
                Canvas.SetTop(cvsUclx, 82);
            }
            else
            {
                Canvas.SetTop(cvsUclx, (_spcObject.USL - _spcObject.UCLx) / (_spcObject.USL - _spcObject.LSL) * 192 + 82);
            }
            lbUCLx.Content = _spcObject.UCLx.ToString("0.00");

            if (_spcObject.LCLx < _spcObject.LSL)
            {
                Canvas.SetTop(cvsLclx, 274);
            }
            else
            {
                Canvas.SetTop(cvsLclx, (_spcObject.USL - _spcObject.LCLx) / (_spcObject.USL - _spcObject.LSL) * 192 + 82);
            }
            lbLCLx.Content = _spcObject.LCLx.ToString("0.00");

            if (_spcObject.Average2 > _spcObject.USL)
            {
                Canvas.SetTop(cvsXBar, 82);
            }
            else if (_spcObject.Average2 < _spcObject.LSL)
            {
                Canvas.SetTop(cvsXBar, 274);
            }
            else
            {
                Canvas.SetTop(cvsXBar, (_spcObject.USL - _spcObject.Average2) / (_spcObject.USL - _spcObject.LSL) * 192 + 82);
            }
            lbXBar.Content = _spcObject.Average2.ToString("0.00");

            lbLCLr.Content = _spcObject.Lclr.ToString("0.00");
            lbUCLr.Content = _spcObject.Uclr.ToString("0.00");
            lbRBar.Content = _spcObject.AverageRange.ToString("0.00");

            for (int i = 0; i < 26; i++)
            {
                distributionDataX[i] = 0;
            }

            Point[] curveData = new Point[_spcObject.historySample.Count];
            for (int i = 0; i < _spcObject.historySample.Count; i++)
            {
                curveData[i] = new Point(i * 10000 /( _spcObject.StatisticsSampleCount - 1),
                    (_spcObject.historySample[i].Average - _spcObject.LSL) / (_spcObject.USL - _spcObject.LSL) * 10000);
                distributionDataAnalysis(_spcObject.historySample[i].Average - _spcObject.LSL, _spcObject.USL - _spcObject.LSL);
            }

            updateDistribution();

            TrendCurve.ClearCurve();
            TrendCurve.refushCurve(curveData);

            Point[] curveDataR = new Point[_spcObject.historySample.Count];
            for (int i = 0; i < _spcObject.historySample.Count; i++)
            {
                curveDataR[i] = new Point(i * 10000 / (_spcObject.StatisticsSampleCount - 1),
                    (_spcObject.historySample[i].Range - _spcObject.Lclr) / (_spcObject.Uclr - _spcObject.Lclr) * 10000);
            }
            ControlCurveR.ClearCurve();
            ControlCurveR.refushCurve(curveDataR);
        }

        private void distributionDataAnalysis(double rawdata, double staff)
        {
            int tmp = (int)(rawdata / (staff / 26));
            if (tmp >= 0 && tmp < 26)
            {
                distributionDataX[tmp]++;
            }
        }

        private void updateDistribution()
        {
            lx0.X2 = distributionDataX[0] * 5;
            lx1.X2 = distributionDataX[1] * 5;
            lx2.X2 = distributionDataX[2] * 5;
            lx3.X2 = distributionDataX[3] * 5;
            lx4.X2 = distributionDataX[4] * 5;
            lx5.X2 = distributionDataX[5] * 5;
            lx6.X2 = distributionDataX[6] * 5;
            lx7.X2 = distributionDataX[7] * 5;
            lx8.X2 = distributionDataX[8] * 5;
            lx9.X2 = distributionDataX[9] * 5;
            lx10.X2 = distributionDataX[10] * 5;
            lx11.X2 = distributionDataX[11] * 5;
            lx12.X2 = distributionDataX[12] * 5;
            lx13.X2 = distributionDataX[13] * 5;
            lx14.X2 = distributionDataX[14] * 5;
            lx15.X2 = distributionDataX[15] * 5;
            lx16.X2 = distributionDataX[16] * 5;
            lx17.X2 = distributionDataX[17] * 5;
            lx18.X2 = distributionDataX[18] * 5;
            lx19.X2 = distributionDataX[19] * 5;
            lx20.X2 = distributionDataX[20] * 5;
            lx21.X2 = distributionDataX[21] * 5;
            lx22.X2 = distributionDataX[22] * 5;
            lx23.X2 = distributionDataX[23] * 5;
            lx24.X2 = distributionDataX[24] * 5;
            lx25.X2 = distributionDataX[25] * 5;
        }

        private void ReSetCpkThreshold(double newValue)
        {
            _spcObject.CpkWarningThreshold = newValue;
            tbCpkThreshold.Content = _spcObject.CpkWarningThreshold.ToString();

            valmoWin.ds.SPCSave();
        }

        private void tbCpkThreshold_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tbCpkThreshold.BorderBrush = 
                new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0xB4, 0xE1));
        }

        private void tbCpkThreshold_MouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            tbCpkThreshold.BorderBrush =
                new SolidColorBrush(Color.FromArgb(0xFF, 0xD4, 0xD4, 0xD4));

            valmoWin.SNumInput.init(1.67, 0.67, _spcObject.Discription, _spcObject.CpkWarningThreshold.ToString(), "", 1, null, ReSetCpkThreshold);
        }

        private void tbCpkThreshold_MouseLeave(object sender, MouseEventArgs e)
        {
            tbCpkThreshold.BorderBrush =
                new SolidColorBrush(Color.FromArgb(0xFF, 0xD4, 0xD4, 0xD4));
        }

        private void ReSetAlarmDelay(double newValue)
        {
            _spcObject.AlarmDelay = (int)newValue;
            tbAlarmDelay.Content = _spcObject.AlarmDelay.ToString();

            valmoWin.ds.SPCSave();
        }

        private void tbAlarmDelay_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tbAlarmDelay.BorderBrush =
                new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0xB4, 0xE1));
        }

        private void tbAlarmDelay_MouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            tbAlarmDelay.BorderBrush =
                new SolidColorBrush(Color.FromArgb(0xFF, 0xD4, 0xD4, 0xD4));

            valmoWin.SNumInput.init(3, 0, _spcObject.Discription, _spcObject.AlarmDelay.ToString(), "", 1, null, ReSetAlarmDelay);
        }

        private void tbAlarmDelay_MouseLeave(object sender, MouseEventArgs e)
        {
            tbAlarmDelay.BorderBrush =
                new SolidColorBrush(Color.FromArgb(0xFF, 0xD4, 0xD4, 0xD4));
        }

        private void ReSetSampleSize(double newValue)
        {
            _spcObject.SampleSize = (int)newValue;
            tbSampleSize.Content = _spcObject.SampleSize.ToString();
            lbSampleSize.Content = _spcObject.SampleSize.ToString();

            valmoWin.ds.SPCSave();
        }

        private void tbSampleSize_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tbSampleSize.BorderBrush =
                new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0xB4, 0xE1));
        }

        private void tbSampleSize_MouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            tbSampleSize.BorderBrush =
                new SolidColorBrush(Color.FromArgb(0xFF, 0xD4, 0xD4, 0xD4));

            valmoWin.SNumInput.init(25, 2, _spcObject.Discription, _spcObject.SampleSize.ToString(), "", 1, null, ReSetSampleSize);
        }

        private void tbSampleSize_MouseLeave(object sender, MouseEventArgs e)
        {
            tbSampleSize.BorderBrush =
                new SolidColorBrush(Color.FromArgb(0xFF, 0xD4, 0xD4, 0xD4));
        }

        private void ReSetStatisticsSampleCount(double newValue)
        {
            _spcObject.StatisticsSampleCount = (int)newValue;
            tbStatisticsSampleCount.Content = _spcObject.StatisticsSampleCount.ToString();

            valmoWin.ds.SPCSave();
        }

        private void tbStatisticsSampleCount_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tbStatisticsSampleCount.BorderBrush =
                new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0xB4, 0xE1));
        }

        private void tbStatisticsSampleCount_MouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            tbStatisticsSampleCount.BorderBrush =
                new SolidColorBrush(Color.FromArgb(0xFF, 0xD4, 0xD4, 0xD4));

            valmoWin.SNumInput.init(99, 10, _spcObject.Discription, _spcObject.StatisticsSampleCount.ToString(), "", 1, null, ReSetStatisticsSampleCount);
        }

        private void tbStatisticsSampleCount_MouseLeave(object sender, MouseEventArgs e)
        {
            tbStatisticsSampleCount.BorderBrush =
                new SolidColorBrush(Color.FromArgb(0xFF, 0xD4, 0xD4, 0xD4));
        }

        private void ReSetUSL(double newValue)
        {
            _spcObject.USL = newValue;
            tbUSL.Content = _spcObject.USL.ToString();
            lbUSL.Content = _spcObject.USL.ToString();

            valmoWin.ds.SPCSave();
        }

        private void tbUSL_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tbUSL.BorderBrush =
                new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0xB4, 0xE1));
        }

        private void tbUSL_MouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            tbUSL.BorderBrush =
                new SolidColorBrush(Color.FromArgb(0xFF, 0xD4, 0xD4, 0xD4));

            valmoWin.SNumInput.init(999, _spcObject.LSL, _spcObject.Discription, _spcObject.USL.ToString(), "", 1, null, ReSetUSL);
        }

        private void tbUSL_MouseLeave(object sender, MouseEventArgs e)
        {
            tbUSL.BorderBrush =
                new SolidColorBrush(Color.FromArgb(0xFF, 0xD4, 0xD4, 0xD4));
        }

        private void ReSetLSL(double newValue)
        {
            _spcObject.LSL = newValue;
            tbLSL.Content = _spcObject.LSL.ToString();
            lbLSL.Content = _spcObject.LSL.ToString();

            valmoWin.ds.SPCSave();
        }

        private void tbLSL_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tbLSL.BorderBrush =
                new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0xB4, 0xE1));
        }

        private void tbLSL_MouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            tbLSL.BorderBrush =
                new SolidColorBrush(Color.FromArgb(0xFF, 0xD4, 0xD4, 0xD4));

            valmoWin.SNumInput.init(_spcObject.USL, 0, _spcObject.Discription, _spcObject.LSL.ToString(), "", 1, null, ReSetLSL);
        }

        private void tbLSL_MouseLeave(object sender, MouseEventArgs e)
        {
            tbLSL.BorderBrush =
                new SolidColorBrush(Color.FromArgb(0xFF, 0xD4, 0xD4, 0xD4));
        }

        private void ReSetSamplePeriod(double newValue)
        {
            _spcObject.SamplePeriod = (int)newValue;
            tbSamplePeriod.Content = _spcObject.SamplePeriod.ToString();

            valmoWin.ds.SPCSave();
        }

        private void tbSamplePeriod_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tbSamplePeriod.BorderBrush =
                new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0xB4, 0xE1));
        }

        private void tbSamplePeriod_MouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            tbSamplePeriod.BorderBrush =
                new SolidColorBrush(Color.FromArgb(0xFF, 0xD4, 0xD4, 0xD4));

            valmoWin.SNumInput.init(40, _spcObject.SampleSize, _spcObject.Discription, _spcObject.SamplePeriod.ToString(), "", 1, null, ReSetSamplePeriod);
        }

        private void tbSamplePeriod_MouseLeave(object sender, MouseEventArgs e)
        {
            tbSamplePeriod.BorderBrush =
                new SolidColorBrush(Color.FromArgb(0xFF, 0xD4, 0xD4, 0xD4));
        }
    }
}
