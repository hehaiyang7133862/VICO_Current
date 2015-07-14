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

namespace nsVicoClient.ctrls
{
    public partial class prdMsgUnit : UserControl
    {
        public bool bVisiable
        {
            set
            {
                if (value)
                {
                    cvsBackground.Height = 25;
                }
                else
                {
                    cvsBackground.Height = 0;
                }
            }
        }
        public string No
        {
            set
            {
                lbNo.Content = value;
            }
        }

        public prdMsgUnit()
        {
            InitializeComponent();
        }

        public void dataInitialize(double d1, double d2, double d3, double d4, double d5, double d6, double d7, double d8, double d9, double d10)
        {
            lbinj_HoldingTime.Content = valmoWin.dv.PrdPr[26].getStrValue(d1);
            lbcarriageTime.Content = valmoWin.dv.PrdPr[33].getStrValue(d2);
            lbcycleTime.Content = valmoWin.dv.PrdPr[40].getStrValue(d3);
            lbVPPositon.Content = valmoWin.dv.PrdPr[47].getStrValue(d4);
            lbVPPressure.Content = valmoWin.dv.PrdPr[54].getStrValue(d5);
            lbVPTime.Content = valmoWin.dv.PrdPr[61].getStrValue(d6);
            lbinjStartPosition.Content = valmoWin.dv.PrdPr[68].getStrValue(d7);
            lbminCushionPosition.Content = valmoWin.dv.PrdPr[75].getStrValue(d8);
            lbcushionCompletePositon.Content = valmoWin.dv.PrdPr[82].getStrValue(d9);
            lbinjPeakPressure.Content = valmoWin.dv.PrdPr[89].getStrValue(d10);
        }
    }
}
