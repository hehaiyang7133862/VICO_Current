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

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// Interaction logic for btnCtrl.xaml
    /// </summary>
    public partial class btnCtrl : UserControl
    {
        //SolidColorBrush downBrushe = new SolidColorBrush(Color.FromRgb(194,194,194));
        //SolidColorBrush upBrushe = new SolidColorBrush(Color.FromRgb(185,217,114));
        SolidColorBrush readOnlyBrushe = new SolidColorBrush(Color.FromRgb(200, 200, 200));

        ImageBrush downImg = new ImageBrush();
        ImageBrush upImg = new ImageBrush();
        public MouseButtonEventHandler upHandle
        {
            get;
            set;
        }
        public btnCtrl()
        {
            InitializeComponent();
            //cvsMain.Background = upBrushe;
            downImg.ImageSource = (BitmapImage)App.Current.TryFindResource("BtnDownBg");
            upImg.ImageSource = (BitmapImage)App.Current.TryFindResource("BtnConfirmBg");
            lbDis.Background = upImg;
            type = btnType.typeConfirm;
        }

        btnType _type = btnType.typeConfirm;
        public btnType type
        {
            get
            {
                return _type;
            }
            set
            {
                switch (value)
                {
                    case btnType.typeConfirm:
                        upImg.ImageSource = (BitmapImage)App.Current.TryFindResource("BtnConfirmBg");
                        break;
                    case btnType.typeSave:
                        upImg.ImageSource = (BitmapImage)App.Current.TryFindResource("BtnSaveBg");
                        break;
                    case btnType.typeDel:
                        upImg.ImageSource = (BitmapImage)App.Current.TryFindResource("BtnDelBg");
                        break;
                }
                _type = value;
                lbDis.Background = upImg;
            }
        }

        public double w
        {
            get
            {
                return cvsMain.Width;
            }
            set
            {
                if (value > 0)
                {
                    cvsMain.Width = value;
                    lbDis.Width = value - 2;
                }
                else
                {
                    cvsMain.Width = 80;
                    lbDis.Width = 72;
                }
            }
        }

        private void cvsMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lbDis.Background = downImg;
        }

        private void cvsMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (lbDis.Background == downImg)
            {
                if (upHandle != null)
                    upHandle(sender ,e);
                lbDis.Background = upImg;
            }
        }

        private void cvsMain_MouseLeave(object sender, MouseEventArgs e)
        {
            if (lbDis.Background == downImg)
            {
                lbDis.Background = upImg;
            }
        }
        public static DependencyProperty disProperty = DependencyProperty.Register(
            "dis",                                                    // Property name
            typeof(Object),                                           // Property type
            typeof(btnCtrl),                                      // Type of the dependency property provider
            new PropertyMetadata("",                                // 默认的值
            new PropertyChangedCallback(OnUriChanged)             // Callback invoked on property value has changes
            )
        );

        private static void OnUriChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as btnCtrl).lbDis.Content = e.NewValue;
        }

        public object dis
        {
            set
            {
                lbDis.Content = value;
            }
            get
            {
                return lbDis.Content;
            }
        }


        public bool readOnly
        {
            get
            {
                return cvsMain.Background == readOnlyBrushe;
            }
            set
            {
                //cvsMain.Background = value ? readOnlyBrushe : upBrushe;
            }
        }
    }

    public enum btnType : byte
    {
        typeConfirm,
        typeSave,
        typeDel
    }
}
