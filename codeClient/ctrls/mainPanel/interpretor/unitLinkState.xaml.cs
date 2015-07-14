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
    /// Interaction logic for unitLinkState.xaml
    /// </summary>
    public partial class unitLinkState : UserControl
    {
        public unitLinkState()
        {
            InitializeComponent();
            init();
            //valmoWin.dv.iprPr[36].addLb(lbSactName);
        }
        public void init()
        {
            valmoWin.dv.IprPr[28].addHandle(refreshLnStateFunc);
        }
        private void refreshLnStateFunc(objUnit obj)
        {
            if (interpretorPage.flagLoading)
                return;
            int tmpPr_28 = obj.value;

            int tmpStartUp = tmpPr_28 & 0x01;
            int tmpStartMld = (tmpPr_28 >> 1) & 0x01;
            int tmpStartDown = (tmpPr_28 >> 2) & 0x01;

            imgStartUp.Opacity = tmpPr_28 & 0x01;
            imgStartMld.Opacity = (tmpPr_28 >> 1) & 0x01;
            imgStartDown.Opacity = (tmpPr_28 >> 2) & 0x01;

            startUpLink.Opacity = tmpPr_28 & 0x01;
            startMldLink.Opacity = (tmpPr_28 >> 1) & 0x01;
            startDownLink.Opacity = (tmpPr_28 >> 2) & 0x01;

            int tmpUp = (tmpPr_28 >> 5) & 0x01;
            int tmpMld = (tmpPr_28 >> 6) & 0x01;
            int tmpDown = (tmpPr_28 >> 7) & 0x01;
            imgEndUp.Opacity = (tmpPr_28 >> 5) & 0x01;
            imgEndMld.Opacity = (tmpPr_28 >> 6) & 0x01;
            imgEndDown.Opacity = (tmpPr_28 >> 7) & 0x01;

            endUpLink.Opacity = (tmpPr_28 >> 5) & 0x01;
            endMldLink.Opacity = (tmpPr_28 >> 6) & 0x01;
            endDownLink.Opacity = (tmpPr_28 >> 7) & 0x01;


            //Console.WriteLine("flagStart:\t{0}\t{1}\t{2}", tmpStartUp, tmpStartMld, tmpStartDown);
            //Console.WriteLine("flagEnd:\t{0}\t{1}\t{2}\n", tmpUp, tmpMld, tmpDown);

            //int pos = valmoWin.dv.iprPr[17].valueNew;
            //int partNr = pos & 0xff;
            //int lineNr = (pos >> 8) & 0xff;
            //int rowNr = (pos >> 16) & 0xff;
            //lbPartNr.Content = partNr;
            //lbLnNr.Content = lineNr;
            //lbRowNr.Content = rowNr;
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
                valmoWin.dv.IprPr[28].setValue(tmpPr_28);
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
                valmoWin.dv.IprPr[28].setValue(tmpPr_28);
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
