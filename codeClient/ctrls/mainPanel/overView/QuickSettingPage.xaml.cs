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
    public partial class QuickSettingPage : UserControl
    {
        public QuickSettingPage()
        {
            InitializeComponent();

            valmoWin.dv.InjPr[72].addHandle(handleInjPr_72);
            valmoWin.dv.InjPr[71].addHandle(handleInjPr_71);

            valmoWin.dv.TmpPr[93].addHandle(setHeating4);
            valmoWin.dv.TmpPr[100].addHandle(setHeating5);
            valmoWin.dv.TmpPr[107].addHandle(setHeating6);
            valmoWin.dv.TmpPr[66].addHandle(setHeating7);
        }

        private void setHeating4(objUnit obj)
        {
            cvsHeating4.Visibility = (obj.value == 1) ? Visibility.Visible : Visibility.Hidden;
        }

        private void setHeating5(objUnit obj)
        {
            cvsHeating5.Visibility = (obj.value == 1) ? Visibility.Visible : Visibility.Hidden;
        }

        private void setHeating6(objUnit obj)
        {
            cvsHeating6.Visibility = (obj.value == 1) ? Visibility.Visible : Visibility.Hidden;
        }

        private void setHeating7(objUnit obj)
        {
            cvsHeating7.Visibility = (obj.value == 1) ? Visibility.Visible : Visibility.Hidden;
        }

        private void handleInjPr_72(objUnit obj)
        {
            lbPos.Background = obj.value == 1 ?
                new SolidColorBrush(Color.FromArgb(0xFF, 0xEA, 0xEA, 0xEA)) :
                Brushes.White;
            lbPos.BorderBrush = (obj.value == 1) ?
                new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0xB4, 0xE1)) :
                Brushes.Transparent;
        }
        private void handleInjPr_71(objUnit obj)
        {
            lbTm.Background = obj.value == 1 ?
                new SolidColorBrush(Color.FromArgb(0xFF, 0xEA, 0xEA, 0xEA)) :
                Brushes.White;
            lbTm.BorderBrush = (obj.value == 1) ?
                new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0xB4, 0xE1)) :
                Brushes.Transparent;
        }

        private void lbPos_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.InjPr[72].accessLevel) || mainPanelCtrl.bIsMouseMove)
                return;
            valmoWin.dv.InjPr[72].setValue((valmoWin.dv.InjPr[72].valueNew == 1) ? 0 : 1);
        }

        private void lbTm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.InjPr[71].accessLevel) || mainPanelCtrl.bIsMouseMove)
                return;
            valmoWin.dv.InjPr[71].setValue((valmoWin.dv.InjPr[71].valueNew == 1) ? 0 : 1);
        }
    }
}
