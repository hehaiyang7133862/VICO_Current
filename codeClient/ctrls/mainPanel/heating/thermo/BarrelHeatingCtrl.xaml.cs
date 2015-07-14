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
    /// Interaction logic for BarrelHeatingCtrl.xaml
    /// </summary>
    public partial class BarrelHeatingCtrl : UserControl
    {
        public BarrelHeatingCtrl()
        {
            InitializeComponent();

            valmoWin.dv.TmpPr[66].addHandle(setHopper);
            valmoWin.dv.TmpPr[93].addHandle(setHeatingZone3);
            valmoWin.dv.TmpPr[100].addHandle(setHeatingZone4);
            valmoWin.dv.TmpPr[107].addHandle(setHeatingZone5);

            valmoWin.dv.TmpPr[11].addHandle(handleTmpPr_11);
            valmoWin.dv.TmpPr[10].addHandle(handleTmpPr_11);
            valmoWin.dv.TmpPr[13].addHandle(handleTmpPr_11);
            valmoWin.dv.TmpPr[14].addHandle(handleTmpPr_11);

            valmoWin.dv.TmpPr[18].addHandle(handleTmpPr_19);
            valmoWin.dv.TmpPr[19].addHandle(handleTmpPr_19);
            valmoWin.dv.TmpPr[21].addHandle(handleTmpPr_19);
            valmoWin.dv.TmpPr[22].addHandle(handleTmpPr_19);

            valmoWin.dv.TmpPr[26].addHandle(handleTmpPr_27);
            valmoWin.dv.TmpPr[27].addHandle(handleTmpPr_27);
            valmoWin.dv.TmpPr[29].addHandle(handleTmpPr_27);
            valmoWin.dv.TmpPr[30].addHandle(handleTmpPr_27);

            valmoWin.dv.TmpPr[34].addHandle(handleTmpPr_35);
            valmoWin.dv.TmpPr[35].addHandle(handleTmpPr_35);
            valmoWin.dv.TmpPr[37].addHandle(handleTmpPr_35);
            valmoWin.dv.TmpPr[38].addHandle(handleTmpPr_35);

            valmoWin.dv.TmpPr[42].addHandle(handleTmpPr_43);
            valmoWin.dv.TmpPr[43].addHandle(handleTmpPr_43);
            valmoWin.dv.TmpPr[44].addHandle(handleTmpPr_43);
            valmoWin.dv.TmpPr[45].addHandle(handleTmpPr_43);

            valmoWin.dv.TmpPr[50].addHandle(handleTmpPr_51);
            valmoWin.dv.TmpPr[51].addHandle(handleTmpPr_51);
            valmoWin.dv.TmpPr[53].addHandle(handleTmpPr_51);
            valmoWin.dv.TmpPr[54].addHandle(handleTmpPr_51);

        }
        private void handleTmpPr_66(objUnit obj)
        {
            if (obj.value == 0)
            {
                lbTmp064.Visibility = Visibility.Hidden;
                cvsSecValueEnd.Visibility = Visibility.Hidden;
                heatingStateLEDCtrl31.Visibility = Visibility.Hidden;
            }
            else
            {
                lbTmp064.Visibility = Visibility.Visible;
                cvsSecValueEnd.Visibility = Visibility.Visible;
                heatingStateLEDCtrl31.Visibility = Visibility.Visible;

                double i = Canvas.GetLeft(cvsSecValueEnd);
            }
           
        }
        private void handleTmpPr_11(objUnit obj)
        {
            if (valmoWin.dv.TmpPr[11].value >= valmoWin.dv.TmpPr[10].value - valmoWin.dv.TmpPr[14].value && obj.value < valmoWin.dv.TmpPr[10].value + valmoWin.dv.TmpPr[13].value)
            {
                processTmp011.fBackground = new SolidColorBrush(Color.FromRgb(78, 195, 0));
            }
            else
            {
                processTmp011.fBackground = new SolidColorBrush(Color.FromRgb(255,102,0));
            }
        }
        private void handleTmpPr_19(objUnit obj)
        {
            if (valmoWin.dv.TmpPr[19].value >= valmoWin.dv.TmpPr[18].value - valmoWin.dv.TmpPr[22].value && obj.value < valmoWin.dv.TmpPr[18].value + valmoWin.dv.TmpPr[21].value)
            {
                processTmp019.fBackground = new SolidColorBrush(Color.FromRgb(78, 195, 0));
            }
            else
            {
                processTmp019.fBackground = new SolidColorBrush(Color.FromRgb(255, 102, 0));
            }
        }
        private void handleTmpPr_27(objUnit obj)
        {
            if (valmoWin.dv.TmpPr[27].value >= valmoWin.dv.TmpPr[26].value - valmoWin.dv.TmpPr[30].value && obj.value < valmoWin.dv.TmpPr[26].value + valmoWin.dv.TmpPr[29].value)
            {
                processTmp027.fBackground = new SolidColorBrush(Color.FromRgb(78, 195, 0));
            }
            else
            {
                processTmp027.fBackground = new SolidColorBrush(Color.FromRgb(255, 102, 0));
            }
        }
        private void handleTmpPr_35(objUnit obj)
        {
            if (valmoWin.dv.TmpPr[35].value >= valmoWin.dv.TmpPr[34].value - valmoWin.dv.TmpPr[38].value && obj.value < valmoWin.dv.TmpPr[34].value + valmoWin.dv.TmpPr[37].value)
            {
                processTmp035.fBackground = new SolidColorBrush(Color.FromRgb(78, 195, 0));
            }
            else
            {
                processTmp035.fBackground = new SolidColorBrush(Color.FromRgb(255, 102, 0));
            }
        }
        private void handleTmpPr_43(objUnit obj)
        {
            if (valmoWin.dv.TmpPr[43].value >= valmoWin.dv.TmpPr[42].value - valmoWin.dv.TmpPr[46].value && obj.value < valmoWin.dv.TmpPr[42].value + valmoWin.dv.TmpPr[45].value)
            {
                processTmp043.fBackground = new SolidColorBrush(Color.FromRgb(78, 195, 0));
            }
            else
            {
                processTmp043.fBackground = new SolidColorBrush(Color.FromRgb(255, 102, 0));
            }
        }
        private void handleTmpPr_51(objUnit obj)
        {
            if (valmoWin.dv.TmpPr[51].value >= valmoWin.dv.TmpPr[50].value - valmoWin.dv.TmpPr[54].value && obj.value < valmoWin.dv.TmpPr[50].value + valmoWin.dv.TmpPr[53].value)
            {
                processTmp051.fBackground = new SolidColorBrush(Color.FromRgb(78, 195, 0));
            }
            else
            {
                processTmp051.fBackground = new SolidColorBrush(Color.FromRgb(255, 102, 0));
            }
        }

        private bool _bIsHopperOn = false;
        private void setHopper(objUnit obj)
        {
            _bIsHopperOn = (obj.value == 1) ? true : false;

            if (_bIsHopperOn == true)
            {
                lbTmp064.Visibility = Visibility.Visible;
                cvsSecValueEnd.Visibility = Visibility.Visible;
                heatingStateLEDCtrl31.Visibility = Visibility.Visible;
            }
            else
            {
                lbTmp064.Visibility = Visibility.Hidden;
                cvsSecValueEnd.Visibility = Visibility.Hidden;
                heatingStateLEDCtrl31.Visibility = Visibility.Hidden;
            }

            reLayou();
        }

        private bool _bIsHeatingZone3On = false;
        private void setHeatingZone3(objUnit obj)
        {
            _bIsHeatingZone3On = (obj.value == 1) ? true : false;

            if (_bIsHeatingZone3On == true)
            {
                cvsSec4.Visibility = Visibility.Visible;
                cvsSecValue4.Visibility = Visibility.Visible;
            }
            else
            {
                cvsSec4.Visibility = Visibility.Hidden;
                cvsSecValue4.Visibility = Visibility.Hidden;
            }

            reLayou();
        }

        private bool _bIsHeatingZone4On = false;
        private void setHeatingZone4(objUnit obj)
        {
            _bIsHeatingZone4On = (obj.value == 1) ? true : false;

            if (_bIsHeatingZone4On == true)
            {
                cvsSec5.Visibility = Visibility.Visible;
                cvsSecValue5.Visibility = Visibility.Visible;
            }
            else
            {
                cvsSec5.Visibility = Visibility.Hidden;
                cvsSecValue5.Visibility = Visibility.Hidden;
            }

            reLayou();
        }

        private bool _bIsHeatingZone5On = false;
        private void setHeatingZone5(objUnit obj)
        {
            _bIsHeatingZone5On = (obj.value == 1) ? true : false;

            if (_bIsHeatingZone5On == true)
            {
                cvsSec6.Visibility = Visibility.Visible;
                cvsSecValue6.Visibility = Visibility.Visible;
            }
            else
            {
                cvsSec6.Visibility = Visibility.Hidden;
                cvsSecValue6.Visibility = Visibility.Hidden;
            }

            reLayou();
        }

        private void reLayou()
        {
            double leftValueEnd = 539 + (_bIsHeatingZone3On == true ? 102 : 0) + (_bIsHeatingZone4On == true ? 102 : 0) + (_bIsHeatingZone5On == true ? 102 : 0);
            Canvas.SetLeft(cvsSecValueEnd, leftValueEnd);
            double leftPicEnd = 479 + (_bIsHeatingZone3On == true ? 102 : 0) + (_bIsHeatingZone4On == true ? 102 : 0) + (_bIsHeatingZone5On == true ? 102 : 0);
            Canvas.SetLeft(cvsSecEnd, leftPicEnd);
            int count = 3 + (_bIsHeatingZone3On == true ? 1 : 0) + (_bIsHeatingZone4On == true ? 1 : 0) + (_bIsHeatingZone5On == true ? 1 : 0);
            tmpSetBar1.setSecLenth(count);
            lnBasic.X2 = 297 + (_bIsHeatingZone3On == true ? 102 : 0) + (_bIsHeatingZone4On == true ? 102 : 0) + (_bIsHeatingZone5On == true ? 102 : 0);

            double leftHeatingZone4Pic = 479 + (_bIsHeatingZone3On == true ? 102 : 0);
            Canvas.SetLeft(cvsSec5, leftHeatingZone4Pic);
            double leftHeatingZone4Value = 506 + (_bIsHeatingZone3On == true ? 102 : 0);
            Canvas.SetLeft(cvsSecValue5, leftHeatingZone4Value);

            double leftHeatingZone5Pic = 479 + (_bIsHeatingZone3On == true ? 102 : 0) + (_bIsHeatingZone4On == true ? 102 : 0);
            Canvas.SetLeft(cvsSec6, leftHeatingZone5Pic);
            double leftHeatingZone5Value = 506 + (_bIsHeatingZone3On == true ? 102 : 0) + (_bIsHeatingZone4On == true ? 102 : 0);
            Canvas.SetLeft(cvsSecValue6, leftHeatingZone5Value);
        }
    }
}
