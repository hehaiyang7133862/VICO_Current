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
    public partial class SetKeybordUnit : UserControl
    {
        private DispatcherTimer Timer;
        private BitmapImage _btnImage = null;
        public BitmapImage BtnImage
        {
            get
            {
                return _btnImage;
            }
            set
            {
                _btnImage = value;

                KeyImage.Source = _btnImage;
            }
        }
        private ctnBtnType _type = ctnBtnType.k_0_null;
        public ctnBtnType Type
        {
            set
            {
                _type = value;

                if (_type != ctnBtnType.k_0_null)
                {
                    BitmapImage img = (BitmapImage)App.Current.TryFindResource("imgKeyBg_" + (int)_type);
                    if (img != null)
                    {
                        BtnImage = img;
                    }
                    string dis = (string)App.Current.TryFindResource("btnKey_" + (int)_type);
                    if (dis != null)
                    {
                        KeyName.SetResourceReference(Label.ContentProperty, "btnKey_" + (int)_type);
                    }
                    else
                    {
                        KeyName.Content = "btnKey_" + (int)_type + "_undefined";
                    }

                    if ((BtnImage != null) || (dis != null))
                    {
                        cvsDetial.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        cvsDetial.Visibility = Visibility.Hidden;
                    }
                }
                else
                {
                    BtnImage = null;
                    KeyName.Content = null;

                    cvsDetial.Visibility = Visibility.Hidden;
                }

            }
            get
            {
                return _type;
            }
        }
        private bool _bIsReadOnly = false;
        public bool ReadOnly
        {
            set
            {
                _bIsReadOnly = value;

                btnDelete.Visibility = (_bIsReadOnly == true) ? Visibility.Hidden : Visibility.Visible;
            }
            get
            {
                return _bIsReadOnly;
            }
        }

        public SetKeybordUnit()
        {
            InitializeComponent();

            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 3);
            Timer.Tick += new EventHandler(Normal);
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Type = ctnBtnType.k_0_null;
        }

        private void Normal(object sender, EventArgs e)
        {
            KeyBorder.Background = Brushes.Transparent;
            
            Timer.Stop();
        }

        public void Warning()
        {
            KeyBorder.Background = new SolidColorBrush(Color.FromArgb(0xff, 0xff, 0x1a, 0x00));

            Timer.Start();
        }

        public void Selected()
        {
            KeyBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xff, 0x3c, 0xe1, 0x00));
        }

        public void UnSelected()
        {
            KeyBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xff, 0xd4, 0xd4, 0xd4));
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            UnSelected();
        }

    }
}
