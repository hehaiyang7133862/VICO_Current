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
    /// Interaction logic for btnSwitch2.xaml
    /// </summary>
    public partial class btnSwitch2 : UserControl
    {
        objUnit curObj;

        public btnSwitch2()
        {
            bitNr = -1;
            InitializeComponent();
        }
        bool _flagOpposite = false;
        public bool flagOpposite
        {
            set
            {
                _flagOpposite = value;
            }
        }
        public string objName
        {
            set
            {
                curObj = valmoWin.dv.getObj(value);
                if (curObj != null)
                {
                    curObj.addHandle(handleState);
                }
            }

        }
        public void setObj(string value)
        {
            curObj = valmoWin.dv.getObj(value);
            if (curObj != null)
            {
                curObj.addHandle(handleState);
            }
        }
        private void handleState(objUnit obj)
        {
            if (bitNr > 0)
            {
                //imgOpen.Visibility = (((obj.value >> bitNr) & 0x01) == 0) ^ (!_flagOpposite) ? Visibility.Visible : Visibility.Hidden;
                //imgOff.Visibility = (((obj.value >> bitNr) & 0x01) == 1) ^ (!_flagOpposite) ? Visibility.Visible : Visibility.Hidden;
            }
            else
            {
                imgOpen.Visibility = (obj.value == 0) ^ (!_flagOpposite) ? Visibility.Visible : Visibility.Hidden;
                imgOff.Visibility = (obj.value == 1) ^ (!_flagOpposite) ? Visibility.Visible : Visibility.Hidden;
            }
        }
        public bool stateOn
        {
            get
            {
                if (imgOpen.Visibility == Visibility.Visible)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (value)
                {
                    imgOpen.Visibility = Visibility.Visible;
                    imgOff.Visibility = Visibility.Hidden;
                }
                else
                {
                    imgOpen.Visibility = Visibility.Hidden;
                    imgOff.Visibility = Visibility.Visible;
                }
            }
        }

        public int bitNr
        {
            get;
            set;
        }

        private void cvsMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (curObj != null)
            {
                if (!valmoWin.dv.checkAccesslevel(curObj.accessLevel))
                    return;
                if(bitNr < 0)
                    curObj.setValue((curObj.valueNew == 0) ? 1 : 0);
                else if(bitNr >= 0 && bitNr < 32)
                {
                    vm.printLn("[" + curObj.serialNum + "]." + bitNr + "\t" + ((curObj.value >> bitNr) & 0x01));
                    vm.printLn("new " + ((((curObj.value >> bitNr) & 0x01) == 0) ? (curObj.value | (1 << bitNr)) : (curObj.value & (~(1 << bitNr)))));
                    curObj.valueNew = (((curObj.value >> bitNr) & 0x01) == 0) ? (curObj.value | (1 << bitNr)) : (curObj.value & (~(1 << bitNr)));
                    
                }
            }
        }
    }
}
