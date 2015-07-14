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
    /// Interaction logic for menuGrpCtrl.xaml
    /// </summary>
    public partial class menuGrpCtrl : UserControl
    {
        public menuGrpCtrl()
        {
            InitializeComponent();

            valmoWin.dv.IprPr[8].addHandle(handleIpr_8);
            valmoWin.dv.IprPr[18].addHandle(handleIpr18);
        }
        private void handleIpr18(objUnit obj)
        {
            int value = obj.value;
            if((value >> 26 & 0x01) == 1 ||
                (value >> 27 & 0x01) == 1 ||
                (value >> 28 & 0x01) == 1 ||
                (value >> 29 & 0x01) == 1 ||
                (value >> 30 & 0x01) == 1 ||
                (value >> 31 & 0x01) == 1 
               )
            {
                menuCtrl10.Visibility = Visibility.Visible;
            }
            else
            {
                menuCtrl10.Visibility = Visibility.Hidden;
            }

            if ((value >> 14 & 0x01) == 1 ||
                (value >> 15 & 0x01) == 1 ||
                (value >> 16 & 0x01) == 1 ||
                (value >> 17 & 0x01) == 1 ||
                (value >> 18 & 0x01) == 1 ||
                (value >> 19 & 0x01) == 1 ||
                (value >> 20 & 0x01) == 1 ||
                (value >> 21 & 0x01) == 1 ||
                (value >> 22 & 0x01) == 1 ||
                (value >> 23 & 0x01) == 1 ||
                (value >> 24 & 0x01) == 1 ||
                (value >> 25 & 0x01) == 1
            )
            {
                menuCtrl9.Visibility = Visibility.Visible;
            }
            else
            {
                menuCtrl9.Visibility = Visibility.Hidden;
            }

        }
        private void handleIpr_8(objUnit obj)
        {
            init();
        }
        public void init()
        {
            iprCtrl.curUnit.get_sl_setup();

            //判断中子是否可用
            if (iprCtrl.curUnit.flagCoreAVisible ||
                iprCtrl.curUnit.flagCoreBVisible ||
                iprCtrl.curUnit.flagCoreCVisible ||
                iprCtrl.curUnit.flagCoreDVisible ||
                iprCtrl.curUnit.flagCoreEVisible ||
                iprCtrl.curUnit.flagCoreFVisible
                )
            {
                menuCtrl10.Visibility = Visibility.Visible;
            }
            else
            {
                menuCtrl10.Visibility = Visibility.Hidden;
            }

            //判断吹气是否有用
            if(iprCtrl.curUnit.flagAir1 ||
                iprCtrl.curUnit.flagAir2 ||
                iprCtrl.curUnit.flagAir3 ||
                iprCtrl.curUnit.flagAir4 ||
                iprCtrl.curUnit.flagAir5 ||
                iprCtrl.curUnit.flagAir6 ||
                iprCtrl.curUnit.flagAir7 ||
                iprCtrl.curUnit.flagAir8 ||
                iprCtrl.curUnit.flagAir9 ||
                iprCtrl.curUnit.flagAir10 ||
                iprCtrl.curUnit.flagAir11 ||
                iprCtrl.curUnit.flagAir12
            )
            {
                menuCtrl9.Visibility = Visibility.Visible;
            }
            else
            {
                menuCtrl9.Visibility = Visibility.Hidden;
            }

            //判断安全门是否可用
            if (iprCtrl.curUnit.flagSaftyDoor)
            {
                menuCtrl3.Visibility = Visibility.Visible;
                menuCtrl4.Visibility = Visibility.Visible;
            }
            else
            {
                menuCtrl3.Visibility = Visibility.Hidden;
                menuCtrl4.Visibility = Visibility.Hidden;
            }

            //判断转盘是否可用
            if (iprCtrl.curUnit.flagRotateTable)
            {
                menuCtrl11.Visibility = Visibility.Visible;
            }
            else
            {
                menuCtrl11.Visibility = Visibility.Hidden;
            }

            //判断喷嘴是否可用
            if(iprCtrl.curUnit.flagNozzle)
            {
                menuCtrl13.Visibility = Visibility.Visible;
            }
            else
            {
                menuCtrl13.Visibility = Visibility.Hidden;
            }
            //判断阀针是否可用
            if (iprCtrl.curUnit.flagValveGate1 ||
                iprCtrl.curUnit.flagValveGate2 ||
                iprCtrl.curUnit.flagValveGate3 ||
                iprCtrl.curUnit.flagValveGate4
                )
            {
                menuCtrl14.Visibility = Visibility.Visible;
            }
            else
            {
                menuCtrl14.Visibility = Visibility.Hidden;
            }
        }
        public void refreshLan()
        {
            menuCtrl1.refreshLan();
            menuCtrl2.refreshLan();
            menuCtrl3.refreshLan();
            menuCtrl4.refreshLan();
            menuCtrl5.refreshLan();
            menuCtrl6.refreshLan();
            menuCtrl7.refreshLan();
            menuCtrl8.refreshLan();
            menuCtrl9.refreshLan();
            menuCtrl10.refreshLan();
            menuCtrl11.refreshLan();
            menuCtrl12.refreshLan();
            menuCtrl13.refreshLan();
            menuCtrl14.refreshLan();
            //menuCtrl16.refreshLan();
            menuCtrl17.refreshLan();
            menuCtrl18.refreshLan();
            menuCtrl19.refreshLan();
            menuCtrl20.refreshLan();
            menuCtrl21.refreshLan();
            menuCtrl22.refreshLan();
            menuCtrl23.refreshLan();
            menuCtrl24.refreshLan();
            //menuCtrl29.refreshLan();
            //menuCtrl30.refreshLan();
            //menuCtrl31.refreshLan();
        }
    }
}
