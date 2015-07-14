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
    public partial class thermoUnit : UserControl
    {
        private double maxTmp = 0;
        private double upOffset = 0;
        private double lowOffset = 0;
        private int _nr = 0;
        public int Nr
        {
            set
            {
                _nr = value;
                lbSerNum.Content = _nr;
            }
            get
            {
                return _nr;
            }
        }
        public bool bIsHeating
        {
            get
            {
                return imgHeating.Opacity == 1;
            }
            set
            {
                imgHeating.Opacity = value ? 1 : 0;
            }
        }

        private bool _switch = false;
        public bool Switch
        {
            set
            {
                _switch = value;
            }
            get
            {
                return _switch;
            }
        }

        public thermoUnit()
        {
            InitializeComponent();
        }

        private objUnit objCurAndSetting;
        public string objCurAndSettingValue
        {
            set
            {
                 objCurAndSetting = valmoWin.dv.getObj(value);
                 if (objCurAndSetting != null)
                 {
                     objCurAndSetting.addHandle(handleCurAndSettingValue);
                     lbSerNum.Content = Int32.Parse(objCurAndSetting.serialNum.Substring(3, 3)) - 259;
                 }
            }
        }

        private void handleCurAndSettingValue(objUnit obj)
        {
            maxTmp = valmoWin.dv.TmpPr[178].vDbl;
            upOffset = valmoWin.dv.TmpPr[179].vDbl;
            lowOffset = valmoWin.dv.TmpPr[180].vDbl;

            if (maxTmp > 0)
            {
                curValue = valmoWin.dv.tempTypeObj.getDblValue((short)((obj.value >> 16) & 0xffff));
                settingValue = valmoWin.dv.tempTypeObj.getDblValue((short)(obj.value & 0xffff));

                if (_switch == true)
                {
                    if (curValue > (settingValue + upOffset))
                    {
                        cvsHead.Background = new SolidColorBrush(Color.FromArgb(0xff, 0xff, 0x50, 0x50));
                    }
                    else if (curValue < (settingValue - lowOffset))
                    {
                        cvsHead.Background = new SolidColorBrush(Color.FromArgb(0xff, 0x78, 0xdd, 0xff));
                    }
                    else
                    {
                        cvsHead.Background = new SolidColorBrush(Color.FromArgb(0xff, 0xff, 0xcc, 0x00));
                    }
                }
                else
                {
                    cvsHead.Background = new SolidColorBrush(Color.FromArgb(0xff, 0xbc, 0xbc, 0xbc));
                }
            }
            else
            {
 
            }
        }
        private double _curValue = 0;
        public double curValue
        {
            set
            {
                _curValue = value;

                lbCurrentValue.Content = _curValue.ToString("0.0");
                BarCurrentValue.Y1 = 100 - 100 * _curValue / maxTmp;
            }
            get
            {
                return _curValue;
            }
        }
        private double _settingValue = 0;
        public double settingValue
        {
            set
            {
                _settingValue = value;

                lbSettingValue.Content = _settingValue.ToString("0,0");
                Canvas.SetTop(cvsSetting, 100 - 100 * _settingValue / maxTmp);
            }
            get
            {
                return _settingValue;
            }
        }
        public bool focus
        {
            get
            {
                return bdBg.Opacity == 1;
            }
            set
            {
                bdBg.Opacity = value ? 1 : 0;
            }
        }

        private void cvsMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!focus)
            {
                heatingPage.showPanelHanle(Int32.Parse(lbSerNum.Content.ToString()), callbackFunc);
                focus = true;
            }
        }
        private void callbackFunc(int value)
        {
            focus = false;
        }
    }
}
