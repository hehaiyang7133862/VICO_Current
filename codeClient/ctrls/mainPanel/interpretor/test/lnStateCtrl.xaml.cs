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
    /// Interaction logic for lnStateCtrl.xaml
    /// </summary>
    public partial class lnStateCtrl : UserControl
    {
        public lnStateCtrl()
        {
            InitializeComponent();
            valmoWin.dv.IprPr[28].addHandle(refreshLnStateFunc);
            valmoWin.dv.IprPr[36].addLb(lbSactName);
            valmoWin.dv.IprPr[17].add();
        }

        private void refreshLnStateFunc(objUnit obj)
        {
            int tmpPr_28 = obj.value;

            int tmpStartUp = tmpPr_28 & 0x01;
            int tmpStartMld = (tmpPr_28 >> 1) & 0x01;
            int tmpStartDown = (tmpPr_28 >> 2) & 0x01;

            startUp.state = tmpStartUp == 1 ? true : false;
            startMld.state = tmpStartMld == 1 ? true : false;
            startDown.state = tmpStartDown == 1 ? true : false;

            int tmpUp = (tmpPr_28 >> 5) & 0x01;
            int tmpMld = (tmpPr_28 >> 6) & 0x01;
            int tmpDown = (tmpPr_28 >> 7) & 0x01;
             endUp.state = tmpUp == 1 ? true : false;
            endMld.state = tmpMld == 1 ? true : false;
            endDown.state = tmpDown == 1 ? true : false;

            //Console.WriteLine("flagStart:\t{0}\t{1}\t{2}", tmpStartUp, tmpStartMld, tmpStartDown);
            //Console.WriteLine("flagEnd:\t{0}\t{1}\t{2}\n", tmpUp, tmpMld, tmpDown);

            int pos = valmoWin.dv.IprPr[17].value;
            int partNr = pos & 0xff;
            int lineNr = (pos >> 8) & 0xff;
            int rowNr = (pos >> 16) & 0xff;
            lbPartNr.Content = partNr;
            lbLnNr.Content = lineNr;
            lbRowNr.Content = rowNr;
        }

        private void startUp_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int tmpPr_28 = valmoWin.dv.IprPr[28].valueNew;
            vm.printBinary(tmpPr_28);
            if ((tmpPr_28 & 0x01) == 0x01)
            {
                tmpPr_28 = tmpPr_28 & (~0x01);
                valmoWin.dv.IprPr[28].setValue(tmpPr_28);
                vm.printBinary(tmpPr_28);
            }
            else
            {
                tmpPr_28 = tmpPr_28 | 0x01;
                valmoWin.dv.IprPr[28].setValue(tmpPr_28) ;
                vm.printBinary(tmpPr_28);
            }
        }

        private void endUp_MouseDown(object sender, MouseButtonEventArgs e)
        {
           int tmpPr_28 = valmoWin.dv.IprPr[28].valueNew;
            vm.printBinary(tmpPr_28);
            if (((tmpPr_28 >> 5) & 0x01) == 0x01)
            {
                tmpPr_28 = tmpPr_28 & (~(0x01 << 5));
                valmoWin.dv.IprPr[28].setValue(tmpPr_28) ;
                vm.printBinary(tmpPr_28);
            }
            else
            {
                tmpPr_28 = tmpPr_28 | (0x01 << 5);
                valmoWin.dv.IprPr[28].setValue(tmpPr_28);
                vm.printBinary(tmpPr_28);
            }
        }

        private void startMld_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int tmpPr_28 = valmoWin.dv.IprPr[28].valueNew;
            vm.printBinary(tmpPr_28);
            if (((tmpPr_28 >> 1) & 0x01) == 0x01)
            {
                tmpPr_28 = tmpPr_28 & (~(0x01 << 1));
                valmoWin.dv.IprPr[28].setValue(tmpPr_28);
                vm.printBinary(tmpPr_28);
            }
            else
            {
                tmpPr_28 = tmpPr_28 | (0x01 << 1);
                valmoWin.dv.IprPr[28].setValue(tmpPr_28);
                vm.printBinary(tmpPr_28);
            }
        }

        private void startDown_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int tmpPr_28 = valmoWin.dv.IprPr[28].valueNew;
            vm.printBinary(tmpPr_28);
            if (((tmpPr_28 >> 2) & 0x01) == 0x01)
            {
                tmpPr_28 = tmpPr_28 & (~(0x01 << 2));
                valmoWin.dv.IprPr[28].setValue(tmpPr_28);
                vm.printBinary(tmpPr_28);
            }
            else
            {
                tmpPr_28 = tmpPr_28 | (0x01 << 2);
                valmoWin.dv.IprPr[28].setValue(tmpPr_28);
                vm.printBinary(tmpPr_28);
            }

        }

        private void endMld_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int tmpPr_28 = valmoWin.dv.IprPr[28].valueNew;
            vm.printBinary(tmpPr_28);
            if (((tmpPr_28 >> 6) & 0x01) == 0x01)
            {
                tmpPr_28 = tmpPr_28 & (~(0x01 << 6));
                valmoWin.dv.IprPr[28].setValue(tmpPr_28);
                vm.printBinary(tmpPr_28);
            }
            else
            {
                tmpPr_28 = tmpPr_28 | (0x01 << 6);
                valmoWin.dv.IprPr[28].setValue(tmpPr_28);
                vm.printBinary(tmpPr_28);
            }
        }

        private void endDown_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int tmpPr_28 = valmoWin.dv.IprPr[28].valueNew;
            vm.printBinary(tmpPr_28);
            if (((tmpPr_28 >> 7) & 0x01) == 0x01)
            {
                tmpPr_28 = tmpPr_28 & (~(0x01 << 7));
                valmoWin.dv.IprPr[28].setValue(tmpPr_28);
                vm.printBinary(tmpPr_28);
            }
            else
            {
                tmpPr_28 = tmpPr_28 | (0x01 << 7);
                valmoWin.dv.IprPr[28].setValue(tmpPr_28);
                vm.printBinary(tmpPr_28);
            }
        }
    }
}
