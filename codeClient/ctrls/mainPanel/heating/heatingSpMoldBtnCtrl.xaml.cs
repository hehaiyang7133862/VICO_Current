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
    /// Interaction logic for heatingSpMoldBtnCtrl.xaml
    /// </summary>
    public partial class heatingSpMoldBtnCtrl : UserControl
    {
        objUnit curObj;
        public heatingSpMoldBtnCtrl()
        {
            InitializeComponent();
        }
        public double h
        {
            set
            {
                if (value > 0)
                {
                    lbMain.Height = value;
                    lbMain1.Height = value;
                    lbMain2.Height = value;
                    lbMain3.Height = value;
                }
            }
            get
            {
                return lbMain.Height;
            }
        }
        public double w
        {
            set
            {
                lbMain.Width = value;
                lbMain1.Width = value;
                lbMain2.Width = value;
                lbMain3.Width = value;
                lbBg0.Width = value;
                lbBg1.Width = value;
                lbBg2.Width = value;
                lbBg3.Width = value;
                cvsMain.Width = value;
                tbMain.Width = value;
                cvsItem0.Width = value;
                cvsItem1.Width = value;
                cvsItem2.Width = value;
                cvsItem3.Width = value;
            }
            get
            {
                return lbMain.Width;
            }
        }
        public FontFamily fFamily
        {
            set
            {
                lbMain.FontFamily = value;
                lbMain1.FontFamily = value;
                lbMain2.FontFamily = value;
                lbMain3.FontFamily = value;
            }
            get
            {
                return lbMain.FontFamily;
            }
        }
        public double fSize
        {
            get
            {
                return lbMain.FontSize;
            }
            set
            {
                if (value > 0)
                {
                    lbMain.FontSize = value;
                    lbMain1.FontSize = value;
                    lbMain2.FontSize = value;
                    lbMain3.FontSize = value;
                }
            }
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
        public objUnit obj
        {
            get
            {
                return curObj;
            }
        }

        private void lbMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (curObj != null)
            {
                if (!valmoWin.dv.checkAccesslevel(curObj.accessLevel))
                    return;
                if (curObj.valueNew == 0)
                {
                    curObj.setValue(1);
                }
                else if (curObj.value == 1)
                {
                    if (curObj.serialNum == "Tmp060")
                        curObj.setValue(0);
                    else
                        curObj.setValue(2);
                }
                else
                {
                    if(curObj.serialNum != "Tmp060")
                        curObj.setValue(0);
                }
            }
        }
        private void stateHandle(objUnit obj)//0:关闭 1:自动、开 2:比例
        {
            switch (obj.value)
            {
                case 0:
                    tbMain.SelectedIndex = 0;
                    break;
                case 1:
                    {
                        if (curObj.serialNum == "Tmp060")
                            tbMain.SelectedIndex = 3;
                        else
                            tbMain.SelectedIndex = 2;
                        break;
                    }
                case 2:
                    {
                        if (curObj.serialNum != "Tmp060")
                            tbMain.SelectedIndex = 1;
                    }
                    break;
            }
        }
    }
}
