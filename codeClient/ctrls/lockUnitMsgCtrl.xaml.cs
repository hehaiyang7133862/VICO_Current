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
    /// lockUnitMsgCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class lockUnitMsgCtrl : UserControl
    {
        objUnit curObj;
        public lockUnitMsgCtrl()
        {
            InitializeComponent();
        }
        public string objName
        {
            set
            {
                valueCtrl.objName = value;
                if (curObj.unitType == UnitType.DgtType || curObj.unitType == UnitType.Tm_minRD)
                    lbUnit.Content = "";
                else
                    lbUnit.Content = "[" + curObj.unit + "]";
                curObj.addHandle(handleUnit);
                object strDis = App.Current.TryFindResource(curObj.serialNum);
                if (strDis != null)
                    lbDis.Content = strDis.ToString();
            }
        }
        private void handleUnit(objUnit obj)
        {
            if (curObj.unitType == UnitType.DgtType || curObj.unitType == UnitType.Tm_minRD)
                lbUnit.Content = "";
            else
                lbUnit.Content = "[" + curObj.unit + "]";
        }
        public void lanRefresh()
        {
            lbDis.Content = valmoWin.dv.getCurDis(curObj.serialNum);
        }
        public static DependencyProperty disProperty = DependencyProperty.Register(
     "dis",                                                    // Property name
     typeof(Object),                                           // Property type
     typeof(lockUnitMsgCtrl),                                      // Type of the dependency property provider
     new PropertyMetadata("",                                // 默认的值
     new PropertyChangedCallback(OnUriChanged)             // Callback invoked on property value has changes
     )
 );

        private static void OnUriChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            lockUnitMsgCtrl ctrl = d as lockUnitMsgCtrl;
            ctrl.lbDis.Content = e.NewValue;
            //ctrl._ctrlDis = e.NewValue.ToString();

        }
        public Object dis
        {
            get;
            set;
        }
    }
}
