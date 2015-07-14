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
    /// Interaction logic for heatingStateLEDCtrl2.xaml
    /// </summary>
    public partial class heatingStateLEDCtrl2 : UserControl
    {
        objUnit curObj;
        public heatingStateLEDCtrl2()
        {
            InitializeComponent();
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
        private void handleState(objUnit obj)
        {
            imgHeatingOn.Opacity = obj.value;
        }
    }
}
