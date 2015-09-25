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
                img.Height = 36;
                img.Width = 36;
                img.Stretch = Stretch.Fill;

                Image_MA_Lst1[i] = img;
            }

            Image_MA_Lst2 = new Image[32];
            for (int i = 0; i < 32; i++)
            {
                Image img = new Image();
                img.Source = (BitmapImage)App.Current.TryFindResource("MA" + (i + 32).ToString().PadLeft(2, '0'));
                img.Height = 36;
                img.Width = 36;
                img.Stretch = Stretch.Fill;

                Image_MA_Lst2[i] = img;
            }

            Image_CurrrentAction_Lst1 = new List<Image>();
            Image_CurrrentAction_Lst2 = new List<Image>();

            valmoWin.dv.SysPr[106].addHandle(MachionAction1);
            valmoWin.dv.SysPr[107].addHandle(MachionAction2);
        }


        private List<Image> Image_CurrrentAction_Lst1;
        private void MachionAction1(objUnit obj)
        {
            Image_CurrrentAction_Lst1.Clear();

            int temp = obj.value;

            for (int i = 0; i < 32; i++)
            {
                if (((temp >> i) & 0x01) == 1)
                {
                    Image_CurrrentAction_Lst1.Add(Image_MA_Lst1[i]);
                }
            }

            Refush();
        }

        private List<Image> Image_CurrrentAction_Lst2;
        private void MachionAction2(objUnit obj)
        {
            Image_CurrrentAction_Lst2.Clear();

            int temp = obj.value;

            for (int i = 0; i < 32; i++)
            {
                if (((temp >> i) & 0x01) == 1)
                {
                    Image_CurrrentAction_Lst2.Add(Image_MA_Lst2[i]);

                }
            }

            Refush();
        }

        private void Refush()
        {
            cvsMain.Children.Clear();
            int count = 0;

            foreach (Image img in Image_CurrrentAction_Lst1)
            {
                cvsMain.Children.Add(img);
                Canvas.SetLeft(img, 4 + count * 40);
                count++;
            }

            foreach (Image img in Image_CurrrentAction_Lst2)
            {
                cvsMain.Children.Add(img);
                Canvas.SetLeft(img, 4 + count * 40);
                count++;
            }
        }
    }
}
