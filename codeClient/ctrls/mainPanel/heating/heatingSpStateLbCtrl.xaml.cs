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
    /// Interaction logic for heatingSpStateLbCtrl.xaml
    /// </summary>
    public partial class heatingSpStateLbCtrl : UserControl
    {
        objUnit curObj;
        public heatingSpStateLbCtrl()
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
                    curObj.addHandle(stateHandle);
                }
            }
        }
        private void stateHandle(objUnit obj)
        {
            lbMain.Content = curObj.vDblStr + curObj.unit;
        }
        public string dis
        {
            get
            {
                return lbMain.Content.ToString();
            }
            set
            {
                lbMain.Content = value;
            }
        }
    }
}
