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
    /// cntBtn.xaml 的交互逻辑
    /// </summary>
    public partial class cntBtn : UserControl
    {
        public static string[] ctrlIdBasic = new string[] {"10", "11", "18", "19", "20", "35", "36", "12", "13", "21", "22", "54", "37", "38", "7", "6", "23", "24", "25", "39", "40" };
        objUnit objKey;
        public cntBtn()
        {
            InitializeComponent();

            valmoWin.lstLanRefresh.Add(languageChange);
        }

        private void languageChange()
        {
            Type = Type;
        }

        public static DependencyProperty disProperty = DependencyProperty.Register(
            "dis",                                                    // Property name
            typeof(Object),                                           // Property type
            typeof(cntBtn),                                      // Type of the dependency property provider
            new PropertyMetadata("",                                // 默认的值
            new PropertyChangedCallback(OnUriChanged)             // Callback invoked on property value has changes
            )
        );

        private static void OnUriChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            cntBtn ctrl = d as cntBtn;
            ctrl.lbFocus.Content = e.NewValue;
        }
        public string dis
        {
            set
            {
                lbFocus.Content = value;
            }
        }

        private ctnBtnType _type;
        public ctnBtnType Type
        {
            set
            {
                _type = value;
                object obj = valmoWin.dv.getObj("Key" + ((int)_type).ToString().PadLeft(3, '0'));
                if (obj == null)
                {
                    return;
                }
                objKey = (objUnit)obj;
                objKey.addHandle(btnStateFunc, plcLstSpd.mapType); 

                BitmapImage imgBg = (BitmapImage)App.Current.TryFindResource("imgKeyBg_" + (int)_type);
                if (imgBg != null)
                {
                    btnBg.Source = imgBg;
                }
                string lbDis = (string)App.Current.TryFindResource("btnKey_" + (int)_type);
                if (lbDis != null)
                {
                    dis = lbDis;
                }
            }
            get
            {
                return _type;
            }
        }
        public void btnStateFunc(objUnit obj)
        {
            switch (obj.value)
            {
                case 1:
                    {
                        lbFocus.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0xE1, 0xA9));
                        lbFocus.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xBD, 0xDE, 0xD7));
                    }
                    break;
                case 0:
                    {
                        lbFocus.BorderBrush = Brushes.Transparent;
                        lbFocus.Background = Brushes.Transparent;
                    }
                    break;
            }
        }
        bool isMousedown = false;
        private void cvsMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isMousedown = true;
            lbFocus.Background = Brushes.LightCoral;
            if (objKey != null)
                objKey.valueNew = 1;
        }

        private void cvsMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isMousedown)
            {
                isMousedown = false;
                lbFocus.Background = Brushes.Transparent;
                if (objKey != null)
                    objKey.valueNew = 0;
            }
        }

        private void cvsMain_MouseLeave(object sender, MouseEventArgs e)
        {
            cvsMain_MouseUp(null, null);
        }
    }

    public enum ctnBtnType
    {
        k_0_null = 0,
        k_6_UnitFor = 6,
        k_7_UnitBack = 7,
        k_10_KeyMoldOpen = 10,
        k_11_KeyMoldClose = 11,
        k_12_Ejectorback = 12,
        k_13_EjectorFor = 13,
        k_18_KeyInjection = 18,
        k_19_Charge = 19,
        k_20_Suckback = 20,
        k_21_CarriageFor = 21,
        k_22_Carriageback = 22,
        k_23_Air1 = 23,
        k_24_Air2 = 24,
        k_25_Air3 = 25,
        k_26_Air4 = 26,
        k_27_Air5 = 27,
        k_28_Air6 = 28,
        k_29_Air7 = 29,
        k_30_Air8 = 30,
        k_31_Air9 = 31,
        k_32_Air10 = 32,
        k_33_Air11 = 33,
        k_34_Air12 = 34,
        k_35_CoreAIN = 35,
        k_36_CoreAOUT = 36,
        k_37_CoreBIN = 37,
        k_38_CoreBOUT = 38,
        k_39_CoreCIN = 39,
        k_40_CoreCOUT = 40,
        k_41_CoreDIN = 41,
        k_42_CoreDOUT = 42,
        k_43_CoreEIN = 43,
        k_44_CoreEOUT = 44,
        k_45_CoreFIN = 45,
        k_46_CoreFOUT = 46,
        k_54_Nozzle = 54,
        k_61_TPos = 61,
        k_62_TNeg = 62
    }
}
