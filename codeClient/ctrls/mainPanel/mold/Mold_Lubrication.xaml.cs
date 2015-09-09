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
    /// Mold_Lubrication.xaml 的交互逻辑
    /// </summary>
    public partial class Mold_Lubrication : UserControl
    {
        public Mold_Lubrication()
        {
            InitializeComponent();

            valmoWin.dv.MldPr[098].addHandle(upadatprg1);
            valmoWin.dv.MldPr[053].addHandle(upadatprg1);
            valmoWin.dv.MldPr[099].addHandle(upadatprg2);
            valmoWin.dv.MldPr[058].addHandle(upadatprg2);
            valmoWin.dv.MldPr[094].addHandle(upadatprg3);
            valmoWin.dv.MldPr[050].addHandle(upadatprg3);
            valmoWin.dv.MldPr[097].addHandle(upadatprg4);
            valmoWin.dv.MldPr[057].addHandle(upadatprg4);

            valmoWin.dv.MldPr[101].addHandle(RefushLubType);
        }

        private void RefushLubType(objUnit obj)
        {
            if (obj.value == 0)
            {
                lbLubType.SetResourceReference(Label.ContentProperty, "CB_LubPumpMode_0");
            }
            else
            {
                lbLubType.SetResourceReference(Label.ContentProperty, "CB_LubPumpMode_1");
            }
        }

        private void upadatprg1(objUnit obj)
        {
            if(valmoWin.dv.MldPr[053].vDbl>0)
            {
                prg1.Value = (int)(100 * valmoWin.dv.MldPr[098].vDbl / valmoWin.dv.MldPr[053].vDbl);
            }
        }
        private void upadatprg2(objUnit obj)
        {
            if (valmoWin.dv.MldPr[058].vDbl > 0)
            {
                prg2.Value = (int)(100 * valmoWin.dv.MldPr[099].vDbl / valmoWin.dv.MldPr[058].vDbl);
            }
        }
        private void upadatprg3(objUnit obj)
        {
            if (valmoWin.dv.MldPr[050].vDbl > 0)
            {
                prg3.Value = (int)(100 * valmoWin.dv.MldPr[094].vDbl / valmoWin.dv.MldPr[050].vDbl);
            }
        }
        private void upadatprg4(objUnit obj)
        {
            if (valmoWin.dv.MldPr[057].vDbl > 0)
            {
                prg4.Value = (int)(100 * valmoWin.dv.MldPr[097].vDbl / valmoWin.dv.MldPr[057].vDbl);
            }
        }

        private void MBmouseMove(object sender, MouseEventArgs e)
        {
        }
        private void BSMouseMove(object sender, MouseEventArgs e)
        {
        }
    }
}
