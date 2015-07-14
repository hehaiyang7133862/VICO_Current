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
    /// Interaction logic for tmpProcessBarCtrl.xaml
    /// </summary>
    public partial class tmpProcessBarCtrl : UserControl
    {
        objUnit curObj;
        public tmpProcessBarCtrl()
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
                    curObj.addHandle(switchHandle);
                }
            }
        }
        private void switchHandle(objUnit obj)
        {
            double height = 0;
            if(curObj.value > 0)
                height = curObj.value;
            double maxH = 400 * objUnit.rate[UnitType.Temp_C];
            if (cvsMain.Height * height / maxH > cvsMain.Height)
                lbBg.Height = cvsMain.Height;
            else if (cvsMain.Height * height / maxH < 0)
            {
                lbBg.Height = 0;
            }
            else
            {
                lbBg.Height = cvsMain.Height * height / maxH;
            }
            Canvas.SetTop(lbBg, cvsMain.Height - lbBg.Height);
        }
        public double w
        {
            set
            {
                if (value < 0)
                    value = 0;
                cvsMain.Width = value;
                lbBg.Width = value;
            }
            get
            {
                return lbBg.Width ;
            }
        }
        public double h
        {
            set
            {
                if (value < 0)
                    value = 0;
                cvsMain.Height = value;
                Canvas.SetTop(lbBg, value - lbBg.Height);
            }
            get
            {
                return lbBg.Height;
            }
        }
        public Brush fBackground
        {
            get
            {
                return lbBg.Background;
            }
            set
            {
                lbBg.Background = value;
            }
        }
    }
}
