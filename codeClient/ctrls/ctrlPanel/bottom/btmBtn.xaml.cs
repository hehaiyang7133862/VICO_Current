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
    /// btmBtn.xaml 的交互逻辑
    /// </summary>
    public partial class btmBtn : UserControl
    {
        private objUnit curObj;
        public btmBtn()
        {
            InitializeComponent();

        }

        public string objName
        {
            set
            {
                curObj = valmoWin.dv.getObj(value);
            }
            get
            {
                if (curObj == null)
                {
                    return "###";
                }
                else
                {
                    return curObj.serialNum;
                }
            }
        }

        btmBtnType _btnType = btmBtnType.k_14_BarrelHeatingActive;
        public btmBtnType type
        {
            get
            {
                return _btnType;
            }
            set
            {
                int num = (int)value;
                BitmapImage imgBg = (BitmapImage)App.Current.TryFindResource("imgKeyBg_" + num);
                if (imgBg != null)
                    btnBg.Source = imgBg;
                BitmapImage imgActive = (BitmapImage)App.Current.TryFindResource("imgKeyActive_" + num);
                if (imgActive != null)
                    BtnActive.Source = imgActive;
                curObj = valmoWin.dv.KeyPr[num];
                if(curObj != null)
                    curObj.addHandle(btnStateFunc, plcLstSpd.mapType);
            }
        }
        public void btnStateFunc(objUnit obj)
        {

            switch (obj.value)
            {
                case 1:
                    {
                        BtnActive.Visibility = Visibility.Visible;
                        btnBg.Visibility = Visibility.Hidden;
                    }
                    break;
                case 0:
                    {
                        BtnActive.Visibility = Visibility.Hidden;
                        btnBg.Visibility = Visibility.Visible;
                    }
                    break;
            }
        }


        bool isMousedown = false;
        private void focusCtrl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isMousedown = true;
            btnBg.Visibility = Visibility.Hidden;
            btnDown.Visibility = Visibility.Visible;
            if (curObj != null)
            {
                curObj.valueNew = 1;
            }
        }

        private void focusCtrl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isMousedown)
            {
                isMousedown = false;
                btnBg.Visibility = BtnActive.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
                btnDown.Visibility = Visibility.Hidden;
                if (curObj != null)
                {
                    curObj.valueNew = 0;
                }
            }
            
        }

        private void focusCtrl_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isMousedown)
            {
                isMousedown = false;
                btnBg.Visibility = BtnActive.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
                btnDown.Visibility = Visibility.Hidden;
                if (curObj != null)
                {
                    curObj.valueNew = 0;
                }
            }
            
        }
    }
    public enum btmBtnType
    {
        k_8_MainLub = 8,
        k_14_BarrelHeatingActive = 14,
        k_52_MoldHeating = 52,
        k_53_SecLub = 53,

        k_59_Expump = 59,
        k_60_watervalve = 60,
        k_N1 = 100,
        k_N2 = 101,
        k_N3 = 102
    }


}
