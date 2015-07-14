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
    public partial class IOBtnCtrl : UserControl
    {
        objUnit curForceObj;
        public IOBtnCtrl()
        {
            InitializeComponent();
        }
        public int ObjName
        {
            set
            {
                if (valmoWin.dv.IOFPr[value] != null)
                {
                    curForceObj = valmoWin.dv.IOFPr[value];
                }
                if (curForceObj == null)
                {
                    img_0.Visibility = Visibility.Hidden;
                    img_1.Visibility = Visibility.Hidden;
                    img_20.Visibility = Visibility.Hidden;
                    img_21.Visibility = Visibility.Hidden;
                    imgForce.Visibility = Visibility.Hidden;
                }
                else
                {
                    curForceObj.addHandle(refreshLst);
                }
            }
        }
        public void setObj(int value)
        {
                if (valmoWin.dv.IOFPr[value] != null)
                {
                    curForceObj = valmoWin.dv.IOFPr[value];
                }
            if (curForceObj == null)
            {
                img_0.Visibility = Visibility.Hidden;
                img_1.Visibility = Visibility.Hidden;
                img_20.Visibility = Visibility.Hidden;
                img_21.Visibility = Visibility.Hidden;
                imgForce.Visibility = Visibility.Hidden;
            }
            else
            {
                curForceObj.addHandle(refreshLst);
            }
        }
        void refreshLst(objUnit obj)
        {
            switch (obj.value)
            {
                case 0:
                    imgForce.Visibility = Visibility.Visible;
                    img_0.Visibility = Visibility.Hidden;
                    img_20.Visibility = Visibility.Visible;
                    img_1.Visibility = Visibility.Visible;
                    img_21.Visibility = Visibility.Hidden;
                    break;
                case 1:
                    imgForce.Visibility = Visibility.Visible;
                    img_0.Visibility = Visibility.Visible;
                    img_20.Visibility = Visibility.Hidden;
                    img_1.Visibility = Visibility.Hidden;
                    img_21.Visibility = Visibility.Visible;
                    break;
                case 2:
                    imgForce.Visibility = Visibility.Hidden;
                    img_0.Visibility = Visibility.Visible;
                    img_20.Visibility = Visibility.Hidden;
                    img_1.Visibility = Visibility.Visible;
                    img_21.Visibility = Visibility.Hidden;
                    break;
            }
        }
        private void img_21_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            if (!valmoWin.dv.checkAccesslevel(curForceObj.accessLevel))
                return;
            curForceObj.setValue(2);
        }

        private void img_20_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(curForceObj.accessLevel))
                return;
            curForceObj.setValue(2);
        }

        private void img_0_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(curForceObj.accessLevel))
                return;
            curForceObj.setValue(0);
        }

        private void img_1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(curForceObj.accessLevel))
                return;
            curForceObj.setValue(1);
        }
    }
}
