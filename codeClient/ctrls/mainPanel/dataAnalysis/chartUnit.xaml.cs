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
using System.Windows.Threading;
using nsDataMgr;

namespace nsVicoClient.ctrls
{
    public partial class chartUnit : UserControl
    {
        public intEvent GotoTop;

        private int _Nr = 0;
        public int Nr
        {
            set
            {
                _Nr = value;
                lbNo.Content = _Nr.ToString();
            }
            get
            {
                return _Nr;
            }
        }
        private double _basicValue;
        public double BasicValue
        {
            set 
            {
                _basicValue = value;
            }
        }
        private double _offset;
        public double Offset
        {
            set
            {
                if (value > 0)
                {
                    _offset = value;
                }
            }
        }
        private SPCVariable _spcVariable;
        public SPCVariable SPCVariable
        {
            set
            {
                _spcVariable = value;

                lbCurName.SetResourceReference(Label.ContentProperty, "TP_" + _spcVariable.CurObj.serialNum);
                lbCurUnit.Content = _spcVariable.Unit;
                _spcVariable.CurValueChanged += readNewValue;

                AutoOffset();
            }
        }
        private int _curveRange = 3;
        public int CurveRange
        {
            set
            {
                _curveRange = value;

                refushCurve();
            }
            get
            {
                return _curveRange;
            }
        }

        DispatcherTimer dt = new DispatcherTimer();

        public chartUnit(int rg)
        {
            InitializeComponent();

            valmoWin.BackstageClockTick += clock;
            _curveRange = rg;
            dt.Interval = new TimeSpan(0, 0, 0, 0, 500);
            dt.Tick += new EventHandler(dt_Tick);
        }

        void dt_Tick(object sender, EventArgs e)
        {
            if (addOrSub == false)
            {
                Offset = _offset + 0.5;
            }
            else
            {
                Offset = _offset - 0.5;
            }

            refushUi();
        }

        private void AutoOffset()
        {
            int count = 700;
            switch (_curveRange)
            {
                case 3:
                    {
                        count = 700;
                        break;
                    }
                case 2:
                    {
                        count = 350;
                        break;
                    }
                case 1:
                    {
                        count = 140;
                        break;
                    }
            }

            double max = _spcVariable.MaxValue(count);
            double min = _spcVariable.MinValue(count);
            double average = _spcVariable.Average(count);

            _basicValue = average;
            _offset = (Math.Abs(max - average) > Math.Abs(min - average)) ?
                Math.Abs(max - average) :
                Math.Abs(min - average);
            if (_offset < 0.01)
            {
                _offset = 0.01;
            }

            refushCurve();
            refushUi();
        }

        private void refushUi()
        {
            lbValueBasic.Content = _basicValue.ToString("0.00");
            lbOffset.Content = _offset.ToString("0.00");
            lbMax.Content = (_basicValue + _offset).ToString("0.00");
            lbMin.Content = (_basicValue - _offset).ToString("0.00");
        }

        private void readNewValue()
        {
            lbCurValue.Content = _spcVariable.CurValue.ToString("0.00");

            refushCurve();
        }

        private void refushCurve()
        {
            int count = 700;
            switch (_curveRange)
            {
                case 3:
                    {
                        count = 700;
                        break;
                    }
                case 2:
                    {
                        count = 350;
                        break;
                    }
                case 1:
                    {
                        count = 140;
                        break;
                    }
            }

            int starIndex = 0;
            if (_spcVariable.historyRawData.Count < count)
            {
                starIndex = 0;
            }
            else
            {
                starIndex = _spcVariable.historyRawData.Count - count;
            }

            Point[] curveData = new Point[count];
            int j = 0;
            for (int i = starIndex; i < _spcVariable.historyRawData.Count; i++)
            {
                curveData[j] = new Point(10000 - j * 10000 / count, (_spcVariable.historyRawData[i] - _basicValue + _offset) / _offset / 2 * 10000);
                j++;
            }

            if (_spcVariable.historyRawData.Count >= count)
            {
                Canvas.SetLeft(pCurrent, 700);
            }
            else
            {
                Canvas.SetLeft(pCurrent, _spcVariable.historyRawData.Count * 700 / count);

                for (int i = _spcVariable.historyRawData.Count; i < count; i++)
                {
                    curveData[i] = new Point(10000 - _spcVariable.historyRawData.Count * 10000 / count, (_spcVariable.CurValue - _basicValue + _offset) / _offset / 2 * 10000);
                }
            }
            Canvas.SetTop(pCurrent, 70 - (_spcVariable.CurValue - _basicValue + _offset) / _offset / 2 * 70);

            TrendCurve.ClearCurve();
            TrendCurve.refushCurve(curveData);
        }

        private void clock()
        {
            pCurrent.Visibility = (pCurrent.Visibility == Visibility.Visible) ? Visibility.Hidden : Visibility.Visible;
        }

        private void btnAdaptive_MouseDown(object sender, MouseButtonEventArgs e)
        {
            btnAdaptive.Background = new SolidColorBrush(Color.FromArgb(0xff, 0xd4, 0xd4, 0xd4));
        }
        private void btnAdaptive_MouseLeave(object sender, MouseEventArgs e)
        {
            btnAdaptive.Background = Brushes.Transparent;
        }
        private void btnAdaptive_MouseUp(object sender, MouseButtonEventArgs e)
        {
            AutoOffset();
            btnAdaptive.Background = Brushes.Transparent;
        }

        private bool _bIsMouseDown = false;
        /// <summary>
        /// false Add
        /// true Sub
        /// </summary>
        private bool addOrSub = false;
        private DateTime dtMouseDown;
        private void imgValueAdd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _bIsMouseDown = true;
            addOrSub = false;
            dt.Start();
            dtMouseDown = DateTime.Now;
            imgValueAdd.Background = new SolidColorBrush(Color.FromArgb(0xff, 0xd4, 0xd4, 0xd4));
        }

        private void imgValueAdd_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_bIsMouseDown)
            {
                if ((DateTime.Now - dtMouseDown) < TimeSpan.FromMilliseconds(500))
                {
                    if (addOrSub == false)
                    {
                        Offset = _offset + 0.1;
                    }
                    else
                    {
                        Offset = _offset - 0.1;
                    }

                    refushUi();
                }

                imgValueAdd.Background = Brushes.Transparent;
                _bIsMouseDown = false;

                dt.Stop();
                refushCurve();
            }
        }
        private void imgValueAdd_MouseLeave(object sender, MouseEventArgs e)
        {
            imgValueAdd.Background = Brushes.Transparent;
            _bIsMouseDown = false;

            dt.Stop();
            refushCurve();
        }

        private void imgValueSub_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _bIsMouseDown = true;
            addOrSub = true;
            dt.Start();
            dtMouseDown = DateTime.Now;
            imgValueSub.Background = new SolidColorBrush(Color.FromArgb(0xff, 0xd4, 0xd4, 0xd4));
        }
        private void imgValueSub_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_bIsMouseDown)
            {
                if ((DateTime.Now - dtMouseDown) < TimeSpan.FromMilliseconds(500))
                {
                    if (addOrSub == false)
                    {
                        Offset = _offset + 0.1;
                    }
                    else
                    {
                        Offset = _offset - 0.1;
                    }

                    refushUi();
                }

                imgValueSub.Background = Brushes.Transparent;
                _bIsMouseDown = false;

                dt.Stop();
                refushCurve();
            }
        }
        private void imgValueSub_MouseLeave(object sender, MouseEventArgs e)
        {
            imgValueSub.Background = Brushes.Transparent;
            _bIsMouseDown = false;

            dt.Stop();
            refushCurve();
        }

        private void lbOffset_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lbOffset.BorderBrush =
                new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0xB4, 0xE1));
        }
        private void lbOffset_MouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            lbOffset.BorderBrush =
                new SolidColorBrush(Color.FromArgb(0xFF, 0xD4, 0xD4, 0xD4));

            valmoWin.SNumInput.init(999, 0, _spcVariable.Discription + " USL", _offset.ToString("0.00"), "", 1, null, ReSetOffset);
        }
        private void ReSetOffset(double newValue)
        {
            _offset = newValue;

            refushUi();
            refushCurve();
        }

        private void btnGotoTop_MouseDown(object sender, MouseButtonEventArgs e)
        {
            btnGotoTop.Background = new SolidColorBrush(Color.FromArgb(0xff, 0xd4, 0xd4, 0xd4));
        }

        private void btnGotoTop_MouseUp(object sender, MouseButtonEventArgs e)
        {
            btnGotoTop.Background = Brushes.Transparent;

            if (GotoTop != null)
            {
                GotoTop(Nr);
            }
        }

        private void btnGotoTop_MouseLeave(object sender, MouseEventArgs e)
        {
            btnGotoTop.Background = Brushes.Transparent;
        }
    }
}
