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
using System.IO;
using nsDataMgr;
using System.Collections.Specialized;
using System.Xml;
using System.Windows.Threading;

namespace nsVicoClient.ctrls
{
    public partial class lineChart : UserControl
    {
        private int _range = 3;
        public int Range
        {
            set
            {
                if (value > 3)
                {
                    _range = 3;
                }
                else if (value < 1)
                {
                    _range = 1;
                }
                else
                {
                    _range = value;
                }

                setRange(_range);
            }
            get
            {
                return _range;
            }
        }
        private SPCVariable[] spclst;

        public lineChart()
        {
            InitializeComponent();

            try
            {
                spclst = new SPCVariable[valmoWin.ds.lstSPCVariable.Count];
                for (int i = 0; i < spclst.Length; i++)
                {
                    spclst[i] = valmoWin.ds.lstSPCVariable[i];
                }
            }
            catch
            {
                Console.WriteLine("数据分析_趋势图_SPC初始化失败");
            }

            SPCVariableInit();
        }

        private void SPCVariableInit()
        {
            sPanel.Children.Clear();

            for (int i = 0; i < spclst.Length; i++)
            {
                chartUnit unit = new chartUnit(_range);
                unit.SPCVariable = spclst[i];
                unit.Nr = i;
                sPanel.Children.Add(unit);
                unit.GotoTop += gotoTopFunc;
            }

            sPanel.Height = spclst.Length * 92;
        }

        public void gotoTopFunc(int nr)
        {
            SPCVariable temp = new SPCVariable();

            temp = spclst[0];
            spclst[0] = spclst[nr];
            spclst[nr] = temp;

            SPCVariableInit();
        }

        private void setRange(int value)
        {
            switch (value)
            {
                case 3:
                    {
                        lbTwenty.Content = 0;
                        lbOne.Content = -700;
                        lbTwo.Content = -500;
                        lbThr.Content = -400;
                        lbFor.Content = -300;
                        lbFiv.Content = -200;
                        lbSix.Content = -100;
                        lbSev.Content = 0;
                        break;
                    }
                case 2:
                    {
                        lbTwenty.Content = -350;
                        lbOne.Content = -300;
                        lbTwo.Content = -250;
                        lbThr.Content = -200;
                        lbFor.Content = -150;
                        lbFiv.Content = -100;
                        lbSix.Content = -50;
                        lbSev.Content = 0;
                        break;
                    }
                case 1:
                    {
                        lbTwenty.Content = -140;
                        lbOne.Content = -120;
                        lbTwo.Content = -100;
                        lbThr.Content = -80;
                        lbFor.Content = -60;
                        lbFiv.Content = -40;
                        lbSix.Content = -20;
                        lbSev.Content = 0;
                        break;
                    }
            }

            foreach (object obj in sPanel.Children)
            {
                if (obj.GetType() == typeof(chartUnit))
                {
                    (obj as chartUnit).CurveRange = value;
                }
            }
        }

        private void btnAdd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            btnAdd.Background =
                new SolidColorBrush(Color.FromArgb(0xFF, 0xEA, 0xEA, 0xEA));
        }
        private void btnAdd_MouseUp(object sender, MouseButtonEventArgs e)
        {
            btnAdd.Background = Brushes.White;

            Range++;
        }
        private void btnAdd_MouseLeave(object sender, MouseEventArgs e)
        {
            btnAdd.Background = Brushes.White;
        }

        private void btnSub_MouseDown(object sender, MouseButtonEventArgs e)
        {
            btnSub.Background =
                new SolidColorBrush(Color.FromArgb(0xFF, 0xEA, 0xEA, 0xEA));
        }

        private void btnSub_MouseUp(object sender, MouseButtonEventArgs e)
        {
            btnSub.Background = Brushes.White;

            Range--;
        }

        private void btnSub_MouseLeave(object sender, MouseEventArgs e)
        {
            btnSub.Background = Brushes.White;
        }

        private Point _lastMousePosition;
        private Point _mouseDownPosition;
        private bool _bIsMouseMove;
        private bool _bIsMouseDown = false;

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _bIsMouseDown = true;
            _lastMousePosition = _mouseDownPosition = e.GetPosition(cvsMain);
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _bIsMouseMove = false;
            _bIsMouseDown = false;
        }

        private void Canvas_MouseLeave(object sender, MouseEventArgs e)
        {
            _bIsMouseMove = false;
            _bIsMouseDown = false;
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_bIsMouseDown == true)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point curMousePos = e.GetPosition(cvsMain);
                    if (Math.Abs(curMousePos.Y - _mouseDownPosition.Y) > 5)
                        _bIsMouseDown = true;

                    double dOld = Canvas.GetTop(sPanel);
                    double dNew = curMousePos.Y - _lastMousePosition.Y + dOld;

                    if (dNew <= -(sPanel.Height - (valmoWin.MainPanelHeight - 310)) - 20)
                        dNew = -(sPanel.Height - (valmoWin.MainPanelHeight - 310)) - 20;
                    if (dNew > -0)
                        dNew = -0;
                    Canvas.SetTop(sPanel, dNew);
                    _lastMousePosition = curMousePos;
                }
            }
        }
    }
}
