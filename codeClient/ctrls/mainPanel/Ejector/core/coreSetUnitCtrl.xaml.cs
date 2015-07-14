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
    /// Interaction logic for coreSetUnitCtrl.xaml
    /// </summary>
    public partial class coreSetUnitCtrl : UserControl
    {

        public static DependencyProperty disProperty = DependencyProperty.Register(
            "dis",                                                    // Property name
            typeof(Object),                                           // Property type
            typeof(coreSetUnitCtrl),                                      // Type of the dependency property provider
            new PropertyMetadata("",                                // 默认的值
            new PropertyChangedCallback(OnUriChanged)                // Callback invoked on property value has changes
            )
        );

        private static void OnUriChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as coreSetUnitCtrl).lbSecName.Content = e.NewValue;
        }
        public string dis
        {
            set
            {
                lbSecName.Content = value;
            }
        }

        public coreSetUnitCtrl()
        {
            InitializeComponent();
        }
        objUnit _objVisible;
        public string objVisible
        {
            get
            {
                if (_objVisible != null)
                    return _objVisible.serialNum;
                else
                    return null;
            }
            set
            {
                _objVisible = valmoWin.dv.getObj(value);
                if (_objVisible != null)
                    _objVisible.addHandle(visibleHandle);
            }
        }
        public void visibleHandle(objUnit obj)
        {
            this.Visibility = obj.value == 1 ? Visibility.Visible : Visibility.Hidden;
        }
        public string objOutput1
        {
            set
            {
                state1.objName = value;
            }
        }
        public string objOutput2
        {
            set
            {
                state3.objName = value;
            }
        }
        public objUnit stateObj;
        public string objState
        {
            set
            {
                stateObj = valmoWin.dv.getObj(value);
                stateObj.addHandle(handleStateValue);
            }
        }
        public void handleStateValue(objUnit obj)
        {
            int value = obj.value;
            if (value == 3 || value == 5)
            {
                state4.state = true;
            }
            else
            {
                state4.state = false;
            }

            if (value == 4 || value == 6)
            {
                state2.state = true;
            }
            else 
            {
                state2.state = false;
            }
        }
        public string objSwitch1
        {
            set
            {
                switch1.setObj(value);
            }
        }
        public string objSwitch2
        {
            set
            {
                switch2.setObj(value);
            }
        }
        public string objSwitch3
        {
            set
            {
                btnSwitch1.objName = value;
            }
        }
        public string objSwitch4
        {
            set
            {
                btnSwitch2.objName = value;
            }
        }
        public string objSwitch5
        {
            set
            {
                btnSwitch3.objName = value;
            }
        }
        public string objPos1
        {
            set
            {

                btnSetCtrl1.objName = value;

            }
        }
        public string objPos2
        {
            set
            {
                btnSetCtrl4.objName = value;
            }
        }
        public string objTm1
        {
            set
            {
                btnSetCtrl2.objName = value;
            }
        }
        public string objTm2
        {
            set
            {
                btnSetCtrl3.objName = value;
            }
        }
        public string objTm3
        {
            set
            {
                btnSetCtrl5.objName = value;
            }
        }
        public string objTm4
        {
            set
            {
                btnSetCtrl6.objName = value;
            }
        }
        public string objTmRo1
        {
            set
            {
                lbCtrl1.objName = value;
            }
        }
        public string objTmRo2
        {
            set
            {
                lbCtrl2.objName = value;
            }
        }
        public string objTmRo3
        {
            set
            {
                lbCtrl3.objName = value;
            }
        }
        public string objTmRo4
        {
            set
            {
                lbCtrl4.objName = value;
            }
        }
        objUnit curObjMode;
        public string objMode
        {
            set
            {
                curObjMode = valmoWin.dv.getObj(value);
                if (curObjMode != null)
                {
                    curObjMode.addHandle(modeFunc);
                }
            }
        }
        private void modeFunc(objUnit obj)
        {
            img0.Opacity = 0;
            img2.Opacity = 0;
            img1.Opacity = 0;
            switch(obj.value)
            {
                case 0:
                    {
                        img0.Opacity = 1;
                    }
                    break;
                case 1:
                    {
                        img1.Opacity = 1;
                    }
                    break;
                case 2:
                    { }
                    break;
            }
        }

        private void lb0_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (curObjMode != null)
            {
                curObjMode.valueNew = 0;
            }
        }

        private void lb1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (curObjMode != null)
            {
                curObjMode.valueNew = 1;
            }
        }

        private void lb2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (curObjMode != null)
            {
                curObjMode.valueNew = 2;
            }
        }

    }
}
