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
    public partial class ViewIOPage : UserControl
    {
        private Label[] IO_Input = new Label[16];
        private Label[] IO_Output = new Label[16];

        public ViewIOPage()
        {
            InitializeComponent();

            valmoWin.dv.SysPr[25].addHandle(showModule0_State);
            valmoWin.dv.SysPr[28].addHandle(showModule1_State);
            valmoWin.dv.SysPr[30].addHandle(showModule2_State);
            valmoWin.dv.SysPr[33].addHandle(showModule3_State);
            valmoWin.dv.SysPr[39].addHandle(showModule4_State);
            valmoWin.dv.SysPr[45].addHandle(showModule5_State);
            valmoWin.dv.SysPr[49].addHandle(showModule6_State);
            valmoWin.dv.SysPr[52].addHandle(showModule7_State);

            IO_Input[0] = lb_In_01;
            IO_Input[1] = lb_In_02;
            IO_Input[2] = lb_In_03;
            IO_Input[3] = lb_In_04;
            IO_Input[4] = lb_In_05;
            IO_Input[5] = lb_In_06;
            IO_Input[6] = lb_In_07;
            IO_Input[7] = lb_In_08;
            IO_Input[8] = lb_In_09;
            IO_Input[9] = lb_In_10;
            IO_Input[10] = lb_In_11;
            IO_Input[11] = lb_In_12;
            IO_Input[12] = lb_In_13;
            IO_Input[13] = lb_In_14;
            IO_Input[14] = lb_In_15;
            IO_Input[15] = lb_In_16;

            IO_Output[0] = lb_Out_01;
            IO_Output[1] = lb_Out_02;
            IO_Output[2] = lb_Out_03;
            IO_Output[3] = lb_Out_04;
            IO_Output[4] = lb_Out_05;
            IO_Output[5] = lb_Out_06;
            IO_Output[6] = lb_Out_07;
            IO_Output[7] = lb_Out_08;
            IO_Output[8] = lb_Out_09;
            IO_Output[9] = lb_Out_10;
            IO_Output[10] = lb_Out_11;
            IO_Output[11] = lb_Out_12;
            IO_Output[12] = lb_Out_13;
            IO_Output[13] = lb_Out_14;
            IO_Output[14] = lb_Out_15;
            IO_Output[15] = lb_Out_16;

            valmoWin.dv.IOFPr[301].addHandle(setInput);
            valmoWin.dv.IOFPr[302].addHandle(setInput);
            valmoWin.dv.IOFPr[303].addHandle(setInput);
            valmoWin.dv.IOFPr[304].addHandle(setInput);
            valmoWin.dv.IOFPr[305].addHandle(setInput);
            valmoWin.dv.IOFPr[306].addHandle(setInput);
            valmoWin.dv.IOFPr[307].addHandle(setInput);
            valmoWin.dv.IOFPr[308].addHandle(setInput);
            valmoWin.dv.IOFPr[309].addHandle(setInput);
            valmoWin.dv.IOFPr[310].addHandle(setInput);
            valmoWin.dv.IOFPr[311].addHandle(setInput);
            valmoWin.dv.IOFPr[312].addHandle(setInput);
            valmoWin.dv.IOFPr[313].addHandle(setInput);
            valmoWin.dv.IOFPr[314].addHandle(setInput);
            valmoWin.dv.IOFPr[315].addHandle(setInput);
            valmoWin.dv.IOFPr[316].addHandle(setInput);
            valmoWin.dv.IOFPr[317].addHandle(setInput);
            valmoWin.dv.IOFPr[318].addHandle(setInput);
            valmoWin.dv.IOFPr[319].addHandle(setInput);
            valmoWin.dv.IOFPr[320].addHandle(setInput);
            valmoWin.dv.IOFPr[321].addHandle(setInput);
            valmoWin.dv.IOFPr[322].addHandle(setInput);
            valmoWin.dv.IOFPr[323].addHandle(setInput);

            valmoWin.dv.IOFPr[335].addHandle(setOutput);
            valmoWin.dv.IOFPr[336].addHandle(setOutput);
            valmoWin.dv.IOFPr[337].addHandle(setOutput);
            valmoWin.dv.IOFPr[338].addHandle(setOutput);
            valmoWin.dv.IOFPr[339].addHandle(setOutput);
            valmoWin.dv.IOFPr[340].addHandle(setOutput);
            valmoWin.dv.IOFPr[341].addHandle(setOutput);
            valmoWin.dv.IOFPr[342].addHandle(setOutput);
            valmoWin.dv.IOFPr[343].addHandle(setOutput);
            valmoWin.dv.IOFPr[344].addHandle(setOutput);
            valmoWin.dv.IOFPr[345].addHandle(setOutput);
            valmoWin.dv.IOFPr[346].addHandle(setOutput);
            valmoWin.dv.IOFPr[347].addHandle(setOutput);
            valmoWin.dv.IOFPr[348].addHandle(setOutput);
            valmoWin.dv.IOFPr[349].addHandle(setOutput);
            valmoWin.dv.IOFPr[350].addHandle(setOutput);
            valmoWin.dv.IOFPr[351].addHandle(setOutput);
            valmoWin.dv.IOFPr[352].addHandle(setOutput);
            valmoWin.dv.IOFPr[353].addHandle(setOutput);
            valmoWin.dv.IOFPr[354].addHandle(setOutput);
            valmoWin.dv.IOFPr[355].addHandle(setOutput);
            valmoWin.dv.IOFPr[356].addHandle(setOutput);
            valmoWin.dv.IOFPr[357].addHandle(setOutput);
            valmoWin.dv.IOFPr[358].addHandle(setOutput);
            valmoWin.dv.IOFPr[359].addHandle(setOutput);
            valmoWin.dv.IOFPr[360].addHandle(setOutput);
            valmoWin.dv.IOFPr[361].addHandle(setOutput);
            valmoWin.dv.IOFPr[362].addHandle(setOutput);
            valmoWin.dv.IOFPr[363].addHandle(setOutput);
            valmoWin.dv.IOFPr[364].addHandle(setOutput);
            valmoWin.dv.IOFPr[365].addHandle(setOutput);
            valmoWin.dv.IOFPr[366].addHandle(setOutput);

            reorder();
        }

        private void setInput(objUnit obj)
        {
            if (obj.value >= 1 && obj.value <= 16)
            {
                object description = TryFindResource("TP_" + obj.serialNum);

                if (description != null)
                {
                    IO_Input[(int)obj.value - 1].SetResourceReference(Label.ContentProperty, "TP_" + obj.serialNum);
                }
                else
                {
                    IO_Input[(int)obj.value - 1].Content = obj.serialNum + "unDefined";
                }

                if (obj.valueOld > 0 && obj.valueOld < 17)
                {
                    IO_Input[(int)obj.valueOld - 1].Content = null;
                }
            }
        }

        private void setOutput(objUnit obj)
        {
            if (obj.value >= 1 && obj.value <= 16)
            {
                object description = TryFindResource("TP_" + obj.serialNum);

                if (description != null)
                {
                    IO_Output[(int)obj.value - 1].SetResourceReference(Label.ContentProperty, (string)description);
                }
                else
                {
                    IO_Output[(int)obj.value - 1].SetResourceReference(Label.ContentProperty, obj.serialNum + "unDefined");
                }

                if (obj.valueOld > 0 && obj.valueOld < 17)
                {
                    IO_Output[(int)obj.valueOld - 1].Content = null;
                }
            }
        }

        private void showModule0_State(objUnit obj)
        {
            if (obj.value != 0)
            {
                cvsModule0Errors.Visibility = Visibility.Visible;

                spModule0Errors.Children.Clear();

                for (int i = 0; i < 16; i++)
                {
                    if (((obj.value >> i) & 0x01) == 1)
                    {
                        string str = TryFindResource("ModuleState_" + i).ToString();

                        if (str != null)
                        {
                            Label lb = new Label();
                            lb.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0x1A, 0x00));
                            lb.FontSize = 14;
                            lb.Content = str;
                            spModule0Errors.Children.Add(lb);
                        }
                    }
                }
            }
        }
        private void spModule0Errors_MouseEnter(object sender, MouseEventArgs e)
        {
            if (spModule0Errors.Children.Count > 0)
            {
                spModule0Errors.Height = spModule0Errors.Children.Count * 30;
                spModule0Errors.Background = new SolidColorBrush(Color.FromArgb(0xff, 0xea, 0xea, 0xea));
            }
        }
        private void spModule0Errors_MouseLeave(object sender, MouseEventArgs e)
        {
            spModule0Errors.Background = Brushes.Transparent;
            spModule0Errors.Height = 30;
        }

        private void showModule1_State(objUnit obj)
        {
            if (obj.value != 0)
            {
                cvsModule1Errors.Visibility = Visibility.Visible;

                spModule1Errors.Children.Clear();

                for (int i = 0; i < 16; i++)
                {
                    if (((obj.value >> i) & 0x01) == 1)
                    {
                        string str = TryFindResource("ModuleState_" + i).ToString();

                        if (str != null)
                        {
                            Label lb = new Label();
                            lb.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0x1A, 0x00));
                            lb.FontSize = 14;
                            lb.Content = str;
                            spModule1Errors.Children.Add(lb);
                        }
                    }
                }
            }
        }
        private void spModule1Errors_MouseEnter(object sender, MouseEventArgs e)
        {
            if (spModule1Errors.Children.Count > 0)
            {
                spModule1Errors.Height = spModule1Errors.Children.Count * 30;
                spModule1Errors.Background = new SolidColorBrush(Color.FromArgb(0xff, 0xea, 0xea, 0xea));
            }
        }
        private void spModule1Errors_MouseLeave(object sender, MouseEventArgs e)
        {
            spModule1Errors.Background = Brushes.Transparent;
            spModule1Errors.Height = 30;
        }

        private void showModule2_State(objUnit obj)
        {
            if (obj.value != 0)
            {
                cvsModule2Errors.Visibility = Visibility.Visible;

                spModule2Errors.Children.Clear();

                for (int i = 0; i < 16; i++)
                {
                    if (((obj.value >> i) & 0x01) == 1)
                    {
                        string str = TryFindResource("ModuleState_" + i).ToString();

                        if (str != null)
                        {
                            Label lb = new Label();
                            lb.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0x1A, 0x00));
                            lb.FontSize = 14;
                            lb.Content = str;
                            spModule2Errors.Children.Add(lb);
                        }
                    }
                }
            }
        }
        private void spModule2Errors_MouseEnter(object sender, MouseEventArgs e)
        {
            if (spModule2Errors.Children.Count > 0)
            {
                spModule2Errors.Height = spModule2Errors.Children.Count * 30;
                spModule2Errors.Background = new SolidColorBrush(Color.FromArgb(0xff, 0xea, 0xea, 0xea));
            }
        }
        private void spModule2Errors_MouseLeave(object sender, MouseEventArgs e)
        {
            spModule2Errors.Background = Brushes.Transparent;
            spModule2Errors.Height = 30;
        }

        private void showModule3_State(objUnit obj)
        {
            if (obj.value != 0)
            {
                cvsModule3Errors.Visibility = Visibility.Visible;

                spModule3Errors.Children.Clear();

                for (int i = 0; i < 16; i++)
                {
                    if (((obj.value >> i) & 0x01) == 1)
                    {
                        string str = TryFindResource("ModuleState_" + i).ToString();

                        if (str != null)
                        {
                            Label lb = new Label();
                            lb.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0x1A, 0x00));
                            lb.FontSize = 14;
                            lb.Content = str;
                            spModule3Errors.Children.Add(lb);
                        }
                    }
                }
            }
        }
        private void spModule3Errors_MouseEnter(object sender, MouseEventArgs e)
        {
            if (spModule3Errors.Children.Count > 0)
            {
                spModule3Errors.Height = spModule3Errors.Children.Count * 30;
                spModule3Errors.Background = new SolidColorBrush(Color.FromArgb(0xff, 0xea, 0xea, 0xea));
            }
        }
        private void spModule3Errors_MouseLeave(object sender, MouseEventArgs e)
        {
            spModule3Errors.Background = Brushes.Transparent;
            spModule3Errors.Height = 30;
        }

        private void showModule4_State(objUnit obj)
        {
            if (obj.value != 0)
            {
                cvsModule4Errors.Visibility = Visibility.Visible;

                spModule4Errors.Children.Clear();

                for (int i = 0; i < 16; i++)
                {
                    if (((obj.value >> i) & 0x01) == 1)
                    {
                        string str = TryFindResource("ModuleState_" + i).ToString();

                        if (str != null)
                        {
                            Label lb = new Label();
                            lb.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0x1A, 0x00));
                            lb.FontSize = 14;
                            lb.Content = str;
                            spModule4Errors.Children.Add(lb);
                        }
                    }
                }
            }
        }
        private void spModule4Errors_MouseEnter(object sender, MouseEventArgs e)
        {
            if (spModule4Errors.Children.Count > 0)
            {
                spModule4Errors.Height = spModule4Errors.Children.Count * 30;
                spModule4Errors.Background = new SolidColorBrush(Color.FromArgb(0xff, 0xea, 0xea, 0xea));
            }
        }
        private void spModule4Errors_MouseLeave(object sender, MouseEventArgs e)
        {
            spModule4Errors.Background = Brushes.Transparent;
            spModule4Errors.Height = 30;
        }

        private void showModule5_State(objUnit obj)
        {
            if (obj.value != 0)
            {
                cvsModule5Errors.Visibility = Visibility.Visible;

                spModule5Errors.Children.Clear();

                for (int i = 0; i < 16; i++)
                {
                    if (((obj.value >> i) & 0x01) == 1)
                    {
                        string str = TryFindResource("ModuleState_" + i).ToString();

                        if (str != null)
                        {
                            Label lb = new Label();
                            lb.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0x1A, 0x00));
                            lb.FontSize = 14;
                            lb.Content = str;
                            spModule5Errors.Children.Add(lb);
                        }
                    }
                }
            }
        }
        private void spModule5Errors_MouseEnter(object sender, MouseEventArgs e)
        {
            if (spModule5Errors.Children.Count > 0)
            {
                spModule5Errors.Height = spModule5Errors.Children.Count * 30;
                spModule5Errors.Background = new SolidColorBrush(Color.FromArgb(0xff, 0xea, 0xea, 0xea));
            }
        }
        private void spModule5Errors_MouseLeave(object sender, MouseEventArgs e)
        {
            spModule5Errors.Background = Brushes.Transparent;
            spModule5Errors.Height = 30;
        }

        private void showModule6_State(objUnit obj)
        {
            if (obj.value != 0)
            {
                cvsModule6Errors.Visibility = Visibility.Visible;

                spModule6Errors.Children.Clear();

                for (int i = 0; i < 16; i++)
                {
                    if (((obj.value >> i) & 0x01) == 1)
                    {
                        string str = TryFindResource("ModuleState_" + i).ToString();

                        if (str != null)
                        {
                            Label lb = new Label();
                            lb.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0x1A, 0x00));
                            lb.FontSize = 14;
                            lb.Content = str;
                            spModule6Errors.Children.Add(lb);
                        }
                    }
                }
            }
        }
        private void spModule6Errors_MouseEnter(object sender, MouseEventArgs e)
        {
            if (spModule6Errors.Children.Count > 0)
            {
                spModule6Errors.Height = spModule6Errors.Children.Count * 30;
                spModule6Errors.Background = new SolidColorBrush(Color.FromArgb(0xff, 0xea, 0xea, 0xea));
            }
        }
        private void spModule6Errors_MouseLeave(object sender, MouseEventArgs e)
        {
            spModule6Errors.Background = Brushes.Transparent;
            spModule6Errors.Height = 30;
        }

        private void showModule7_State(objUnit obj)
        {
            if (obj.value != 0)
            {
                cvsModule7Errors.Visibility = Visibility.Visible;

                spModule7Errors.Children.Clear();

                for (int i = 0; i < 16; i++)
                {
                    if (((obj.value >> i) & 0x01) == 1)
                    {
                        string str = TryFindResource("ModuleState_" + i).ToString();

                        if (str != null)
                        {
                            Label lb = new Label();
                            lb.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0x1A, 0x00));
                            lb.FontSize = 14;
                            lb.Content = str;
                            spModule7Errors.Children.Add(lb);
                        }
                    }
                }
            }
        }
        private void spModule7Errors_MouseEnter(object sender, MouseEventArgs e)
        {
            if (spModule7Errors.Children.Count > 0)
            {
                spModule7Errors.Height = spModule7Errors.Children.Count * 30;
                spModule7Errors.Background = new SolidColorBrush(Color.FromArgb(0xff, 0xea, 0xea, 0xea));
            }
        }
        private void spModule7Errors_MouseLeave(object sender, MouseEventArgs e)
        {
            spModule7Errors.Background = Brushes.Transparent;
            spModule7Errors.Height = 30;
        }

        private Point _lastMousePosition;
        private Point _mouseDownPosition;
        private bool _bIsMouseMove = false;
        private bool _bIsMouseDown = false;

        private void cvsMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _bIsMouseMove = false;
            _bIsMouseDown = false;
        }

        private void reorder()
        {
            cvsModules0.Width = (_bIsModule0Visiable == true) ? 400 : 82;
            cvsModules1.Width = (_bIsModule1Visiable == true) ? 400 : 82;
            cvsModules2.Width = (_bIsModule2Visiable == true) ? 400 : 82;
            cvsModules3.Width = (_bIsModule3Visiable == true) ? 400 : 82;
            cvsModules4.Width = (_bIsModule4Visiable == true) ? 400 : 82;
            cvsModules5.Width = (_bIsModule5Visiable == true) ? 400 : 82;
            cvsModules6.Width = (_bIsModule6Visiable == true) ? 400 : 82;
            cvsModules7.Width = (_bIsModule7Visiable == true) ? 400 : 82;

            sPanelGraph.Width = cvsModules0.Width + cvsModules1.Width + cvsModules2.Width
                + cvsModules3.Width + cvsModules4.Width + cvsModules5.Width + cvsModules6.Width + cvsModules7.Width + 60;
        }

        private bool _bIsModule0Visiable = false;
        private void cvsModule0Errors_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!_bIsMouseMove)
            {
                if (_bIsModule0Visiable == true)
                {
                    _bIsModule0Visiable = false;
                    ybar0.Visibility = Visibility.Hidden;
                }
                else
                {
                    _bIsModule0Visiable = true;
                    ybar0.Visibility = Visibility.Visible; 
                }
            }
            reorder();
        }

        private bool _bIsModule1Visiable = false;
        private void cvsModule1Errors_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!_bIsMouseMove)
            {
                if (_bIsModule1Visiable == true)
                {
                    _bIsModule1Visiable = false;
                    ybar1.Visibility = Visibility.Hidden;
                }
                else
                {
                    _bIsModule1Visiable = true;
                    ybar1.Visibility = Visibility.Visible; 
                }
            }
            reorder();
        }

        private bool _bIsModule2Visiable = false;
        private void cvsModule2Errors_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!_bIsMouseMove)
            {
                if (_bIsModule2Visiable == true)
                {
                    _bIsModule2Visiable = false;
                    ybar2.Visibility = Visibility.Hidden;
                }
                else
                {
                    _bIsModule2Visiable = true;
                    ybar2.Visibility = Visibility.Visible; 
                }
            }
            reorder();
        }

        private bool _bIsModule3Visiable = false;
        private void cvsModule3Errors_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!_bIsMouseMove)
            {
                if (_bIsModule3Visiable == true)
                {
                    _bIsModule3Visiable = false;
                    ybar3.Visibility = Visibility.Hidden;
                }
                else
                {
                    _bIsModule3Visiable = true;
                    ybar3.Visibility = Visibility.Visible; 
                }
            }
            reorder();
        }

        private bool _bIsModule4Visiable = false;
        private void cvsModule4Errors_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!_bIsMouseMove)
            {
                if (_bIsModule4Visiable == true)
                {
                    _bIsModule4Visiable = false;
                    ybar4.Visibility = Visibility.Hidden;
                }
                else
                {
                    _bIsModule4Visiable = true;
                    ybar4.Visibility = Visibility.Visible; 
                }
            }
            reorder();
        }

        private bool _bIsModule5Visiable = false;
        private void cvsModule5Errors_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!_bIsMouseMove)
            {
                if (_bIsModule5Visiable == true)
                {
                    _bIsModule5Visiable = false;
                    ybar5.Visibility = Visibility.Hidden;
                }
                else
                {
                    _bIsModule5Visiable = true;
                    ybar5.Visibility = Visibility.Visible; 
                }
            }
            reorder();
        }

        private bool _bIsModule6Visiable = false;
        private void cvsModule6Errors_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!_bIsMouseMove)
            {
                if (_bIsModule6Visiable == true)
                {
                    _bIsModule6Visiable = false;
                    ybar6.Visibility = Visibility.Hidden;
                }
                else
                {
                    _bIsModule6Visiable = true;
                    ybar6.Visibility = Visibility.Visible; 
                }
            }
            reorder();
        }

        private bool _bIsModule7Visiable = false;
        private void cvsModule7Errors_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!_bIsMouseMove)
            {
                if (_bIsModule7Visiable == true)
                {
                    _bIsModule7Visiable = false;
                    ybar7.Visibility = Visibility.Hidden;
                }
                else
                {
                    _bIsModule7Visiable = true;
                    ybar7.Visibility = Visibility.Visible;
                }
            }
            reorder();
        }

        private void cvsGraph_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _bIsMouseDown = true;
            _lastMousePosition = _mouseDownPosition = e.GetPosition(cvsMain);
        }

        private void cvsGraph_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _bIsMouseMove = false;
            _bIsMouseDown = false;
        }

        private void cvsGraph_MouseMove(object sender, MouseEventArgs e)
        {
            if (_bIsMouseDown == true)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point curMousePos = e.GetPosition(cvsMain);
                    if ((Math.Abs(curMousePos.Y - _mouseDownPosition.Y) > 3) || (Math.Abs(curMousePos.X - _mouseDownPosition.X) > 3))
                    {
                        _bIsMouseMove = true;
                    }

                    double dOld = Canvas.GetLeft(sPanelGraph);
                    double dNew = curMousePos.X - _lastMousePosition.X + dOld;

                    if (dNew <= -(sPanelGraph.Width - 1048))
                        dNew = -(sPanelGraph.Width - 1048);
                    if (dNew > 0)
                        dNew = 0;
                    Canvas.SetLeft(sPanelGraph, dNew);
                    _lastMousePosition = curMousePos;
                }
            }
        }

        private void cvsGraph_MouseLeave(object sender, MouseEventArgs e)
        {
            _bIsMouseMove = false;
            _bIsMouseDown = false;
        }

        private bool bIsTop = true;

        private void Button_MouseUp(object sender, MouseButtonEventArgs e)
        {
            bIsTop = !bIsTop;

            if (bIsTop == true)
            {
                imgDown.Visibility = Visibility.Visible;
                imgUp.Visibility = Visibility.Hidden;

                Canvas.SetTop(sPanelMain, 0);
            }
            else
            {
                imgDown.Visibility = Visibility.Hidden;
                imgUp.Visibility = Visibility.Visible;

                Canvas.SetTop(sPanelMain, valmoWin.MainPanelHeight - 1775);
            }
        }
    }
}
