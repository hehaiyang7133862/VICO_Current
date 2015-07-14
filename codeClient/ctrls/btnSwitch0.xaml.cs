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
    /// Interaction logic for btnSwitch0.xaml
    /// </summary>
    public partial class btnSwitch0 : UserControl
    {
        objUnit curObj;
        public btnSwitch0()
        {
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

                imgOpen.Visibility = (obj.value == 1) ^ (!_flagOpposite) ? Visibility.Visible : Visibility.Hidden;
                imgOff.Visibility = (obj.value == 0) ^ (!_flagOpposite) ? Visibility.Visible : Visibility.Hidden;

        }
        public bool stateOn
        {
            get
            {
                    return imgOpen.Visibility == Visibility.Visible;
            }
            set
            {
                if (value)
                {
                    imgOpen.Visibility = Visibility.Visible;
                }
                else
                {
                    imgOpen.Visibility = Visibility.Hidden;
                }
            }
        }

        private void cvsMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (curObj != null)
            {
                if (!valmoWin.dv.checkAccesslevel(curObj.accessLevel))
                    return;
                 curObj.setValue((curObj.valueNew == 0) ? 1 : 0);
            }
        }
    }
}
