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
    public partial class thermoPage : UserControl
    {
        public thermoPage()
        {
            InitializeComponent();

            valmoWin.dv.TmpPr[190].addHandle(Zone1_IsHeating);
            valmoWin.dv.TmpPr[191].addHandle(Zone2_IsHeating);
            valmoWin.dv.TmpPr[192].addHandle(Zone3_IsHeating);

            valmoWin.dv.TmpPr[237].addHandle(Zone1_Switch);
            valmoWin.dv.TmpPr[238].addHandle(Zone2_Switch);
            valmoWin.dv.TmpPr[239].addHandle(Zone3_Switch);
        }

        private void Zone1_IsHeating(objUnit obj)
        {
            int value = obj.value;
            thermoUnit1.bIsHeating = (value & 0x01) == 1;
            thermoUnit2.bIsHeating = ((value >> 1) & 0x01) == 1;
            thermoUnit3.bIsHeating = ((value >> 2) & 0x01) == 1;
            thermoUnit4.bIsHeating = ((value >> 3) & 0x01) == 1;
            thermoUnit5.bIsHeating = ((value >> 4) & 0x01) == 1;
            thermoUnit6.bIsHeating = ((value >> 5) & 0x01) == 1;
            thermoUnit7.bIsHeating = ((value >> 6) & 0x01) == 1;
            thermoUnit8.bIsHeating = ((value >> 7) & 0x01) == 1;
            thermoUnit9.bIsHeating = ((value >> 8) & 0x01) == 1;
            thermoUnit10.bIsHeating = ((value >> 9) & 0x01) == 1;
            thermoUnit11.bIsHeating = ((value >> 10) & 0x01) == 1;
            thermoUnit12.bIsHeating = ((value >> 11) & 0x01) == 1;
            thermoUnit13.bIsHeating = ((value >> 12) & 0x01) == 1;
            thermoUnit14.bIsHeating = ((value >> 13) & 0x01) == 1;
            thermoUnit15.bIsHeating = ((value >> 14) & 0x01) == 1;
            thermoUnit16.bIsHeating = ((value >> 15) & 0x01) == 1;
            thermoUnit17.bIsHeating = ((value >> 16) & 0x01) == 1;
            thermoUnit18.bIsHeating = ((value >> 17) & 0x01) == 1;
            thermoUnit19.bIsHeating = ((value >> 18) & 0x01) == 1;
            thermoUnit20.bIsHeating = ((value >> 19) & 0x01) == 1;
            thermoUnit21.bIsHeating = ((value >> 20) & 0x01) == 1;
            thermoUnit22.bIsHeating = ((value >> 21) & 0x01) == 1;
            thermoUnit23.bIsHeating = ((value >> 22) & 0x01) == 1;
            thermoUnit24.bIsHeating = ((value >> 23) & 0x01) == 1;
            thermoUnit25.bIsHeating = ((value >> 24) & 0x01) == 1;
            thermoUnit26.bIsHeating = ((value >> 25) & 0x01) == 1;
            thermoUnit27.bIsHeating = ((value >> 26) & 0x01) == 1;
            thermoUnit28.bIsHeating = ((value >> 27) & 0x01) == 1;
            thermoUnit29.bIsHeating = ((value >> 28) & 0x01) == 1;
            thermoUnit30.bIsHeating = ((value >> 29) & 0x01) == 1;
            thermoUnit31.bIsHeating = ((value >> 30) & 0x01) == 1;
            thermoUnit32.bIsHeating = ((value >> 31) & 0x01) == 1;
        }
        private void Zone1_Switch(objUnit obj)
        {
            int value = obj.value;

            thermoUnit1.Switch = (value & 0x01) == 1;
            thermoUnit2.Switch = ((value >> 1) & 0x01) == 1;
            thermoUnit3.Switch = ((value >> 2) & 0x01) == 1;
            thermoUnit4.Switch = ((value >> 3) & 0x01) == 1;
            thermoUnit5.Switch = ((value >> 4) & 0x01) == 1;
            thermoUnit6.Switch = ((value >> 5) & 0x01) == 1;
            thermoUnit7.Switch = ((value >> 6) & 0x01) == 1;
            thermoUnit8.Switch = ((value >> 7) & 0x01) == 1;
            thermoUnit9.Switch = ((value >> 8) & 0x01) == 1;
            thermoUnit10.Switch = ((value >> 9) & 0x01) == 1;
            thermoUnit11.Switch = ((value >> 10) & 0x01) == 1;
            thermoUnit12.Switch = ((value >> 11) & 0x01) == 1;
            thermoUnit13.Switch = ((value >> 12) & 0x01) == 1;
            thermoUnit14.Switch = ((value >> 13) & 0x01) == 1;
            thermoUnit15.Switch = ((value >> 14) & 0x01) == 1;
            thermoUnit16.Switch = ((value >> 15) & 0x01) == 1;
            thermoUnit17.Switch = ((value >> 16) & 0x01) == 1;
            thermoUnit18.Switch = ((value >> 17) & 0x01) == 1;
            thermoUnit19.Switch = ((value >> 18) & 0x01) == 1;
            thermoUnit20.Switch = ((value >> 19) & 0x01) == 1;
            thermoUnit21.Switch = ((value >> 20) & 0x01) == 1;
            thermoUnit22.Switch = ((value >> 21) & 0x01) == 1;
            thermoUnit23.Switch = ((value >> 22) & 0x01) == 1;
            thermoUnit24.Switch = ((value >> 23) & 0x01) == 1;
            thermoUnit25.Switch = ((value >> 24) & 0x01) == 1;
            thermoUnit26.Switch = ((value >> 25) & 0x01) == 1;
            thermoUnit27.Switch = ((value >> 26) & 0x01) == 1;
            thermoUnit28.Switch = ((value >> 27) & 0x01) == 1;
            thermoUnit29.Switch = ((value >> 28) & 0x01) == 1;
            thermoUnit30.Switch = ((value >> 29) & 0x01) == 1;
            thermoUnit31.Switch = ((value >> 30) & 0x01) == 1;
            thermoUnit32.Switch = ((value >> 31) & 0x01) == 1;
        }

        private void Zone2_IsHeating(objUnit obj)
        {
            int value = obj.value;
            thermoUnit33.bIsHeating = (value & 0x01) == 1;
            thermoUnit34.bIsHeating = ((value >> 1) & 0x01) == 1;
            thermoUnit35.bIsHeating = ((value >> 2) & 0x01) == 1;
            thermoUnit36.bIsHeating = ((value >> 3) & 0x01) == 1;
            thermoUnit37.bIsHeating = ((value >> 4) & 0x01) == 1;
            thermoUnit38.bIsHeating = ((value >> 5) & 0x01) == 1;
            thermoUnit39.bIsHeating = ((value >> 6) & 0x01) == 1;
            thermoUnit40.bIsHeating = ((value >> 7) & 0x01) == 1;
            thermoUnit41.bIsHeating = ((value >> 8) & 0x01) == 1;
            thermoUnit42.bIsHeating = ((value >> 9) & 0x01) == 1;
            thermoUnit43.bIsHeating = ((value >> 10) & 0x01) == 1;
            thermoUnit44.bIsHeating = ((value >> 11) & 0x01) == 1;
            thermoUnit45.bIsHeating = ((value >> 12) & 0x01) == 1;
            thermoUnit46.bIsHeating = ((value >> 13) & 0x01) == 1;
            thermoUnit47.bIsHeating = ((value >> 14) & 0x01) == 1;
            thermoUnit48.bIsHeating = ((value >> 15) & 0x01) == 1;
            thermoUnit49.bIsHeating = ((value >> 16) & 0x01) == 1;
            thermoUnit50.bIsHeating = ((value >> 17) & 0x01) == 1;
            thermoUnit51.bIsHeating = ((value >> 18) & 0x01) == 1;
            thermoUnit52.bIsHeating = ((value >> 19) & 0x01) == 1;
            thermoUnit53.bIsHeating = ((value >> 20) & 0x01) == 1;
            thermoUnit54.bIsHeating = ((value >> 21) & 0x01) == 1;
            thermoUnit55.bIsHeating = ((value >> 22) & 0x01) == 1;
            thermoUnit56.bIsHeating = ((value >> 23) & 0x01) == 1;
            thermoUnit57.bIsHeating = ((value >> 24) & 0x01) == 1;
            thermoUnit58.bIsHeating = ((value >> 25) & 0x01) == 1;
            thermoUnit59.bIsHeating = ((value >> 26) & 0x01) == 1;
            thermoUnit60.bIsHeating = ((value >> 27) & 0x01) == 1;
            thermoUnit61.bIsHeating = ((value >> 28) & 0x01) == 1;
            thermoUnit62.bIsHeating = ((value >> 29) & 0x01) == 1;
            thermoUnit63.bIsHeating = ((value >> 30) & 0x01) == 1;
            thermoUnit64.bIsHeating = ((value >> 31) & 0x01) == 1;
        }
        private void Zone2_Switch(objUnit obj)
        {
            int value = obj.value;

            thermoUnit33.Switch = (value & 0x01) == 1;
            thermoUnit34.Switch = ((value >> 1) & 0x01) == 1;
            thermoUnit35.Switch = ((value >> 2) & 0x01) == 1;
            thermoUnit36.Switch = ((value >> 3) & 0x01) == 1;
            thermoUnit37.Switch = ((value >> 4) & 0x01) == 1;
            thermoUnit38.Switch = ((value >> 5) & 0x01) == 1;
            thermoUnit39.Switch = ((value >> 6) & 0x01) == 1;
            thermoUnit40.Switch = ((value >> 7) & 0x01) == 1;
            thermoUnit41.Switch = ((value >> 8) & 0x01) == 1;
            thermoUnit42.Switch = ((value >> 9) & 0x01) == 1;
            thermoUnit43.Switch = ((value >> 10) & 0x01) == 1;
            thermoUnit44.Switch = ((value >> 11) & 0x01) == 1;
            thermoUnit45.Switch = ((value >> 12) & 0x01) == 1;
            thermoUnit46.Switch = ((value >> 13) & 0x01) == 1;
            thermoUnit47.Switch = ((value >> 14) & 0x01) == 1;
            thermoUnit48.Switch = ((value >> 15) & 0x01) == 1;
            thermoUnit49.Switch = ((value >> 16) & 0x01) == 1;
            thermoUnit50.Switch = ((value >> 17) & 0x01) == 1;
            thermoUnit51.Switch = ((value >> 18) & 0x01) == 1;
            thermoUnit52.Switch = ((value >> 19) & 0x01) == 1;
            thermoUnit53.Switch = ((value >> 20) & 0x01) == 1;
            thermoUnit54.Switch = ((value >> 21) & 0x01) == 1;
            thermoUnit55.Switch = ((value >> 22) & 0x01) == 1;
            thermoUnit56.Switch = ((value >> 23) & 0x01) == 1;
            thermoUnit57.Switch = ((value >> 24) & 0x01) == 1;
            thermoUnit58.Switch = ((value >> 25) & 0x01) == 1;
            thermoUnit59.Switch = ((value >> 26) & 0x01) == 1;
            thermoUnit60.Switch = ((value >> 27) & 0x01) == 1;
            thermoUnit61.Switch = ((value >> 28) & 0x01) == 1;
            thermoUnit62.Switch = ((value >> 29) & 0x01) == 1;
            thermoUnit63.Switch = ((value >> 30) & 0x01) == 1;
            thermoUnit64.Switch = ((value >> 31) & 0x01) == 1;
        }

        private void Zone3_IsHeating(objUnit obj)
        {
            int value = obj.value;
            thermoUnit65.bIsHeating = (value & 0x01) == 1;
            thermoUnit66.bIsHeating = ((value >> 1) & 0x01) == 1;
            thermoUnit67.bIsHeating = ((value >> 2) & 0x01) == 1;
            thermoUnit68.bIsHeating = ((value >> 3) & 0x01) == 1;
            thermoUnit69.bIsHeating = ((value >> 4) & 0x01) == 1;
            thermoUnit70.bIsHeating = ((value >> 5) & 0x01) == 1;
            thermoUnit71.bIsHeating = ((value >> 6) & 0x01) == 1;
            thermoUnit72.bIsHeating = ((value >> 7) & 0x01) == 1;
            thermoUnit73.bIsHeating = ((value >> 8) & 0x01) == 1;
            thermoUnit74.bIsHeating = ((value >> 9) & 0x01) == 1;
            thermoUnit75.bIsHeating = ((value >> 10) & 0x01) == 1;
            thermoUnit76.bIsHeating = ((value >> 11) & 0x01) == 1;
            thermoUnit77.bIsHeating = ((value >> 12) & 0x01) == 1;
            thermoUnit78.bIsHeating = ((value >> 13) & 0x01) == 1;
            thermoUnit79.bIsHeating = ((value >> 14) & 0x01) == 1;
            thermoUnit80.bIsHeating = ((value >> 15) & 0x01) == 1;
            thermoUnit81.bIsHeating = ((value >> 16) & 0x01) == 1;
            thermoUnit82.bIsHeating = ((value >> 17) & 0x01) == 1;
            thermoUnit83.bIsHeating = ((value >> 18) & 0x01) == 1;
            thermoUnit84.bIsHeating = ((value >> 19) & 0x01) == 1;
            thermoUnit85.bIsHeating = ((value >> 20) & 0x01) == 1;
            thermoUnit86.bIsHeating = ((value >> 21) & 0x01) == 1;
            thermoUnit87.bIsHeating = ((value >> 22) & 0x01) == 1;
            thermoUnit88.bIsHeating = ((value >> 23) & 0x01) == 1;
            thermoUnit89.bIsHeating = ((value >> 24) & 0x01) == 1;
            thermoUnit90.bIsHeating = ((value >> 25) & 0x01) == 1;
            thermoUnit91.bIsHeating = ((value >> 26) & 0x01) == 1;
            thermoUnit92.bIsHeating = ((value >> 27) & 0x01) == 1;
            thermoUnit93.bIsHeating = ((value >> 28) & 0x01) == 1;
            thermoUnit94.bIsHeating = ((value >> 29) & 0x01) == 1;
            thermoUnit95.bIsHeating = ((value >> 30) & 0x01) == 1;
            thermoUnit96.bIsHeating = ((value >> 31) & 0x01) == 1;
        }
        private void Zone3_Switch(objUnit obj)
        {
            int value = obj.value;
            thermoUnit65.Switch = (value & 0x01) == 1;
            thermoUnit66.Switch = ((value >> 1) & 0x01) == 1;
            thermoUnit67.Switch = ((value >> 2) & 0x01) == 1;
            thermoUnit68.Switch = ((value >> 3) & 0x01) == 1;
            thermoUnit69.Switch = ((value >> 4) & 0x01) == 1;
            thermoUnit70.Switch = ((value >> 5) & 0x01) == 1;
            thermoUnit71.Switch = ((value >> 6) & 0x01) == 1;
            thermoUnit72.Switch = ((value >> 7) & 0x01) == 1;
            thermoUnit73.Switch = ((value >> 8) & 0x01) == 1;
            thermoUnit74.Switch = ((value >> 9) & 0x01) == 1;
            thermoUnit75.Switch = ((value >> 10) & 0x01) == 1;
            thermoUnit76.Switch = ((value >> 11) & 0x01) == 1;
            thermoUnit77.Switch = ((value >> 12) & 0x01) == 1;
            thermoUnit78.Switch = ((value >> 13) & 0x01) == 1;
            thermoUnit79.Switch = ((value >> 14) & 0x01) == 1;
            thermoUnit80.Switch = ((value >> 15) & 0x01) == 1;
            thermoUnit81.Switch = ((value >> 16) & 0x01) == 1;
            thermoUnit82.Switch = ((value >> 17) & 0x01) == 1;
            thermoUnit83.Switch = ((value >> 18) & 0x01) == 1;
            thermoUnit84.Switch = ((value >> 19) & 0x01) == 1;
            thermoUnit85.Switch = ((value >> 20) & 0x01) == 1;
            thermoUnit86.Switch = ((value >> 21) & 0x01) == 1;
            thermoUnit87.Switch = ((value >> 22) & 0x01) == 1;
            thermoUnit88.Switch = ((value >> 23) & 0x01) == 1;
            thermoUnit89.Switch = ((value >> 24) & 0x01) == 1;
            thermoUnit90.Switch = ((value >> 25) & 0x01) == 1;
            thermoUnit91.Switch = ((value >> 26) & 0x01) == 1;
            thermoUnit92.Switch = ((value >> 27) & 0x01) == 1;
            thermoUnit93.Switch = ((value >> 28) & 0x01) == 1;
            thermoUnit94.Switch = ((value >> 29) & 0x01) == 1;
            thermoUnit95.Switch = ((value >> 30) & 0x01) == 1;
            thermoUnit96.Switch = ((value >> 31) & 0x01) == 1;
        }

        private bool bIsMouseDown = false;
        private Point pMouseDownPos;
        private Point pMouseLastPos;

        private void cvsMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            bIsMouseDown = true;
            pMouseDownPos = pMouseLastPos = e.GetPosition(cvsMain);
        }

        private void cvsMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            bIsMouseDown = false;
        }

        private void cvsMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (bIsMouseDown == true)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point curMousePos = e.GetPosition(cvsMain);

                    double dOld = Canvas.GetTop(sPanelMain);
                    double dNew = curMousePos.Y - pMouseLastPos.Y + dOld;

                    if (dNew <= -(sPanelMain.Height - (valmoWin.MainPanelHeight - 195)) - 20)
                        dNew = -(sPanelMain.Height - (valmoWin.MainPanelHeight - 195)) - 20;
                    if (dNew > 0)
                        dNew = 0;
                    Canvas.SetTop(sPanelMain, dNew);
                    pMouseLastPos = curMousePos;
                }
            }
        }

        private void cvsMain_MouseLeave(object sender, MouseEventArgs e)
        {
            bIsMouseDown = false;
        }
    }
}
