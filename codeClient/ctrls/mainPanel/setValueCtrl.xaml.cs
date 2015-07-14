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
    /// setValueCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class setValueCtrl : UserControl
    {
        objUnit curObj;
        SolidColorBrush freezeColor =  new SolidColorBrush(Color.FromRgb(212, 212, 212));
        public setValueCtrl()
        {
            InitializeComponent();
        }

        public string objName
        {
            set
            {
                lbCtrlValue.objName = value;
                this.curObj = lbCtrlValue.obj;
                if (curObj != null)
                {
                    handleRefrsheUnit(curObj);
                    curObj.addHandle(handleRefrsheUnit);
                }
            }
        }

        private void handleRefrsheUnit(objUnit obj)
        {
            if (obj.unit == "")
            {
                lbUnit.Content = "";
            }
            else
            {
                lbUnit.Content = "[" + obj.unit + "]";
            }
        }
        public string unit
        {
            set
            {
                if (value == "")
                {
                    lbUnit.Content = "";
                }
                else
                {
                    lbUnit.Content = "[" + value + "]";
                }
            }
        }

        public bool readOnly
        {
            set
            {
                if (value)
                {
                    bdBox.BorderThickness = new Thickness(0);
                    bdBox.Background = Brushes.Transparent;
                }
            }
            get
            {
                if (bdBox.BorderThickness.Left == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool flagFreeze
        {
            set
            {
                bdBox.Background = freezeColor;
            }
            get
            {
                return bdBox.Background == freezeColor;
            }
        }
        public Point pos
        {
            get;
            set;
        }
        private void bdBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (curObj != null && !readOnly && !flagFreeze)
            {
                if (!valmoWin.dv.checkAccesslevel(curObj.accessLevel))
                    return;
                //imgBg.Opacity = 1;
                Thickness margin = new Thickness(pos.X, pos.Y, 0, 0);
                //if (_ctrlDis == null)
                //{
                //    valmoWin.SNumKeyPanel.init(curObj, disposeFunc, margin);
                //}
                //else
                bdBox.BorderThickness = new Thickness(2);
                bdBox.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 195, 147));
                    valmoWin.SNumKeyPanel.init(curObj,  disposeFunc);
            }
            else
            {
                vm.perror("[btnSetDown] obj is null.");
            }
        }
        public void disposeFunc()
        {
            bdBox.BorderThickness = new Thickness(1);
            bdBox.BorderBrush = Brushes.Silver;
            bdBox.Background = Brushes.White;
            //imgBg.Opacity = 0;
        }

    }
}
