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
    /// MachionActionCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class MachionActionCtrl : UserControl
    {
        private bool isReversal = false;
        public bool IsReversal
        {
            set
            {
                isReversal = value;
            }
        }

        public Image[] Image_MA_Lst1;
        public Image[] Image_MA_Lst2;

        public MachionActionCtrl()
        {
            InitializeComponent();

            Image_MA_Lst1 = new Image[32];
            for (int i = 0; i < 32; i++)
            {
                Image img = new Image();
                img.Source = (BitmapImage)App.Current.TryFindResource("MA" + i.ToString().PadLeft(2, '0'));
                Image_MA_Lst1[i] = img;
            }

            Image_MA_Lst2 = new Image[32];
            for (int i = 0; i < 32; i++)
            {
                Image img = new Image();
                img.Source = (BitmapImage)App.Current.TryFindResource("MA" + (i + 32).ToString().PadLeft(2, '0'));
                Image_MA_Lst2[i] = img;
            }

            valmoWin.dv.SysPr[106].addHandle(MachionAction1);
            valmoWin.dv.SysPr[107].addHandle(MachionAction2);
        }

        private int count = 0;

        private bool state1 = false;
        private void MachionAction1(objUnit obj)
        {
            if ((state1 == true) && (state2 == true))
            {
                cvsMain.Children.Clear();
                count = 0;
                state1 = false;
                state2 = false;
            }

            if (state1 == false)
            {
                int temp = obj.value;

                for (int i = 0; i < 32; i++)
                {
                    if (((temp >> i) & 0x01) == 1)
                    {
                        cvsMain.Children.Add(Image_MA_Lst1[i]);

                        if (isReversal == false)
                        {
                            Canvas.SetLeft(Image_MA_Lst1[i], count * 40 + 4);
                        }
                        else
                        {
                            Canvas.SetRight(Image_MA_Lst1[i], count * 40 + 4);
                        }
                    }
                }

                state1 = true;
            }
        }

        private bool state2 = false;
        private void MachionAction2(objUnit obj)
        {
            if ((state1 == true) && (state2 == true))
            {
                cvsMain.Children.Clear();
                count = 0;
                state1 = false;
                state2 = false;
            }

            if (state2 == false)
            {
                int temp = obj.value;

                for (int i = 0; i < 32; i++)
                {
                    if (((temp >> i) & 0x01) == 1)
                    {
                        if (((temp >> i) & 0x01) == 1)
                        {
                            cvsMain.Children.Add(Image_MA_Lst2[i]);

                            if (isReversal == false)
                            {
                                Canvas.SetLeft(Image_MA_Lst2[i], count * 40 + 4);
                            }
                            else
                            {
                                Canvas.SetRight(Image_MA_Lst2[i], count * 40 + 4);
                            }
                        }
                    }
                }

                state2 = true;
            }
        }


    }
}
