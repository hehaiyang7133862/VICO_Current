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
    /// Interaction logic for airSetUnitCtrl.xaml
    /// </summary>
    public partial class airSetUnitCtrl : UserControl
    {
        private string _Nr;
        public string Nr
        {
            set
            {
                _Nr = value;
                lb_Nr.Content = _Nr;
            }
        }

        public airSetUnitCtrl()
        {
            InitializeComponent();
        }

        private objUnit curActObj;
        public string objAct
        {
            set
            {
                curActObj = valmoWin.dv.getObj(value);
                if (curActObj != null)
                {
                    curActObj.addHandle(actFunc);
                    btnSwitch21.setObj(value);
                }
            }
        }
        public void actFunc(objUnit obj)
        {
            cvsPanel.Visibility = obj.value == 1 ? Visibility.Visible : Visibility.Hidden;
        }

        objUnit curChkObj;//声明值为零时，全都不显示
        public string objChk
        {
            set
            {
                curChkObj = valmoWin.dv.getObj(value);
                if (curChkObj != null)
                {
                    curChkObj.addHandle(chkFunc);
                }
            }

        }
        private void chkFunc(objUnit obj)
        {
            this.Visibility = obj.value == 1 ? Visibility.Visible : Visibility.Hidden;

        }
        public string objMenu
        {
            set
            {
                MenuCtrl.objname = value;
            }
        }

        public string objTm
        {
            set
            {
                tmCtrl.objName = value;
            }
        }
        public string objTmSet
        {
            set
            {
                tmSetCtrl.objName = value;
            }
        }
        public string objKeeping
        {
            set
            {
                keepingCtrl.objName = value;
            }
        }
        public string objKeepingSet
        {
            set
            {
                keepingSetCtrl.objName = value;
            }
        }
    }
}
