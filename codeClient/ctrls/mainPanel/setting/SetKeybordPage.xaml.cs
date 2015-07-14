using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
using nsVicoClient.ctrls;

namespace nsVicoClient.ctrls
{
    public partial class SetKeybordPage : UserControl
    {
        SetKeybordUnit[,] lstKeybord = new SetKeybordUnit[3, 7];
        SetKeybordUnit[,] lstKeyPool = new SetKeybordUnit[5, 8];

        private int[] strLstKeybord = new int[49];

        private int[] focusPosition = { -1, 0, 0 };
        private int[] targetPosition = { -1, 0, 0 };

        public SetKeybordPage()
        {
            InitializeComponent();

            lstInitialize();

            if (Properties.Settings.Default.ctrlLst != null)
            {
                keybordInitialize();
            }
            else
            {
                keybordDefaultSetting();
            }
            keyPoolInitialize();
        }

        private void lstInitialize()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    SetKeybordUnit u = new SetKeybordUnit();
                    u.Margin = new Thickness(20, 20, 0, 0);
                    u.MouseDown += new MouseButtonEventHandler(key_MouseDown);
                    u.MouseUp += new MouseButtonEventHandler(key_MouseUp);
                    u.MouseEnter +=new MouseEventHandler(key_MouseEnter);
                    u.MouseLeave += new MouseEventHandler(key_MouseLeave);

                    lstKeybord[i, j] = u;
                    GridKeybord.Children.Add(u);
                    Grid.SetRow(u, i);
                    Grid.SetColumn(u, j);
                }
            }

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    SetKeybordUnit u = new SetKeybordUnit();
                    u.Margin = new Thickness(20, 20, 0, 0);
                    u.ReadOnly = true;
                    u.MouseDown += new MouseButtonEventHandler(key_MouseDown);

                    lstKeyPool[i, j] = u;
                    GridKeyPool.Children.Add(u);
                    Grid.SetRow(u, i);
                    Grid.SetColumn(u, j);
                }
            }
        }

        private void keybordInitialize()
        {
            StringCollection keybordlist = Properties.Settings.Default.ctrlLst;

            int j = 0;
            for (int i = 0; i < keybordlist.Count && j < 21; i++)
            {
                int t = Convert.ToInt32(keybordlist[i]);

                if (ctnBtnType.IsDefined(typeof(ctnBtnType), t))
                {
                    strLstKeybord[j] = t;
                    lstKeybord[j / 7, j % 7].Type = (ctnBtnType)t;
                    j++;
                }
            }
            for (int i = keybordlist.Count; i < 21; i++)
            {
                strLstKeybord[i] = 0;
                lstKeybord[i / 7, i % 7].Type = ctnBtnType.k_0_null;
            }
        }

        private void keybordDefaultSetting()
        {
            StringCollection keybord = new StringCollection();

            string[] klst = cntBtn.ctrlIdBasic;

            for (int i = 0; i < klst.Length; i++)
            {
                keybord.Add(klst[i]);
            }

            Properties.Settings.Default.ctrlLst = keybord;
            Properties.Settings.Default.Save();

            keybordInitialize();
        }

        private void keyPoolInitialize()
        {
            int j = 0;
            for (int i = 1; i < 63; i++)
            {
                if (ctnBtnType.IsDefined(typeof(ctnBtnType), i))
                {
                    if (j < 40)
                    {
                        lstKeyPool[j / 8, j % 8].Type = (ctnBtnType)i;
                        j++;
                    }
                }
            }
        }

        private bool _bIsMouseDown = false;
        private Point _MouseDownPoint;
        private Point _LastMousePoint;

        private void key_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender.GetType() == typeof(SetKeybordUnit))
            {
                SetKeybordUnit su = (SetKeybordUnit)sender;

                if (su.Type != ctnBtnType.k_0_null)
                {
                    _bIsMouseDown = true;
                    _MouseDownPoint = _LastMousePoint = e.GetPosition(cvsMain);

                    if (su.ReadOnly == true)
                    {
                        focusPosition[0] = 1;
                    }
                    else
                    {
                        focusPosition[0] = 0;
                    }
                    focusPosition[1] = Grid.GetRow(su);
                    focusPosition[2] = Grid.GetColumn(su);

                    BitmapImage Image = (BitmapImage)App.Current.TryFindResource("imgKeyBg_" + (int)su.Type);
                    if (Image != null)
                    {
                        ImageMoving.Source = Image;
                    }
                    Canvas.SetLeft(cvsMoving, _MouseDownPoint.X - 70);
                    Canvas.SetTop(cvsMoving, _MouseDownPoint.Y - 70);
                    cvsMoving.Visibility = Visibility.Visible;
                }
                else
                {
                    focusPosition[0] = -1;

                    int Row = Grid.GetRow(su);
                    int Column = Grid.GetColumn(su);
                    strLstKeybord[Row * 7 + Column] = 0;
                    cvsMoving.Visibility = Visibility.Hidden;
                }
            }
        }

        private void key_MouseMove(object sender, MouseEventArgs e)
        {
            if (_bIsMouseDown == true)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point _CurrentMosePoint = e.GetPosition(cvsMain);

                    double topOld = Canvas.GetTop(cvsMoving);
                    double topNew = topOld + _CurrentMosePoint.Y - _LastMousePoint.Y;
                    Canvas.SetTop(cvsMoving, topNew);
                    double leftOld = Canvas.GetLeft(cvsMoving);
                    double leftNew = leftOld + _CurrentMosePoint.X - _LastMousePoint.X;
                    Canvas.SetLeft(cvsMoving, leftNew);

                    _LastMousePoint = _CurrentMosePoint;
                }
            }
        }

        private void key_MouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            if (_bIsMouseDown == true)
            {
                _bIsMouseDown = false;

                if (focusPosition[0] == 0)
                {
                    ctnBtnType temp;
                    int Srow = focusPosition[1];
                    int Scolumn = focusPosition[2];
                    int Trow = targetPosition[1];
                    int Tcolumen = targetPosition[2];

                    temp = lstKeybord[Trow, Tcolumen].Type;
                    lstKeybord[Trow, Tcolumen].Type = lstKeybord[Srow, Scolumn].Type;
                    lstKeybord[Srow, Scolumn].Type = temp;
                    int it = strLstKeybord[Trow * 7 + Tcolumen];
                    strLstKeybord[Trow * 7 + Tcolumen] = strLstKeybord[Srow * 7 + Scolumn];
                    strLstKeybord[Srow * 7 + Scolumn] = it;
                }
                else if (focusPosition[0] == 1)
                {
                    int Srow = focusPosition[1];
                    int Scolumn = focusPosition[2];
                    int Trow = targetPosition[1];
                    int Tcolumen = targetPosition[2];

                    int Nr = Array.IndexOf(strLstKeybord, Convert.ToInt32(lstKeyPool[Srow, Scolumn].Type));

                    if (Nr == -1)
                    {
                        lstKeybord[Trow, Tcolumen].Type = lstKeyPool[Srow, Scolumn].Type;
                        strLstKeybord[Trow * 7 + Tcolumen] = Convert.ToInt32(lstKeyPool[Srow, Scolumn].Type);
                    }
                    else
                    {
                        lstKeybord[Nr / 7, Nr % 7].Warning();
                    }
                }

                focusPosition[0] = -1;
                cvsMoving.Visibility = Visibility.Hidden;
            }
        }

        private void key_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender.GetType() == typeof(SetKeybordUnit))
            {
                SetKeybordUnit su = (SetKeybordUnit)sender;
                su.Selected();
                targetPosition[0] = 0;
                targetPosition[1] = Grid.GetRow(su);
                targetPosition[2] = Grid.GetColumn(su);
            }
        }

        private void key_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender.GetType() == typeof(SetKeybordUnit))
            {
                (sender as SetKeybordUnit).UnSelected();
                targetPosition[0] = -1;
            }
        }

        private void cvsMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _bIsMouseDown = false;
            focusPosition[0] = -1;

            cvsMoving.Visibility = Visibility.Hidden;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            keybordDefaultSetting();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            StringCollection keybord = new StringCollection();

            foreach (int i in strLstKeybord)
            {
                if (ctnBtnType.IsDefined(typeof(ctnBtnType), i))
                {
                    keybord.Add(i.ToString());
                }
            }

            Properties.Settings.Default.ctrlLst = keybord;
            Properties.Settings.Default.Save();

            keysPanel.refreshCtrlsHandle();
        }
    }
}
