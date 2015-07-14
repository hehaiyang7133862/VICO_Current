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
    /// TopParamMonitor.xaml 的交互逻辑
    /// </summary>
    public partial class TopParamMonitor : UserControl
    {
        private objUnit _curobj;
        public objUnit CurObj
        {
            set
            {
                _curobj = value;

                lbUnit.Content = "[" + _curobj.unit + "]";

                _curobj.addHandle(refushValue);
            }
            get
            {
                return _curobj;
            }
        }

        private void refushValue(objUnit obj)
        {
            lbValue.Content = obj.vDblStr;
        }

        public TopParamMonitor()
        {
            InitializeComponent();
        }
    }
}
