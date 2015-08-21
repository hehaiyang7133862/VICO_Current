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
using System.Windows.Threading;
using System.IO;
using nsDataMgr;

namespace nsVicoClient.ctrls
{
    public partial class productMessageList : UserControl
    {
        private prdMsgUnit[] lstUnit;
        private ProductParam[] lstPrdData;

        public static nullEvent refushH;

        public void r()
        {
            cvsBack.Height = valmoWin.MainPanelHeight - 175 - 106 - 150 - 20;
            spBack.Height = cvsBack.Height + 106 + 150;
        }

        public productMessageList()
        {
            InitializeComponent();

            refushH += r;
            r();

            lstUnit = new prdMsgUnit[50];

            lstUnit[0] = uint0;
            lstUnit[1] = uint1;
            lstUnit[2] = uint2;
            lstUnit[3] = uint3;
            lstUnit[4] = uint4;
            lstUnit[5] = uint5;
            lstUnit[6] = uint6;
            lstUnit[7] = uint7;
            lstUnit[8] = uint8;
            lstUnit[9] = uint9;
            lstUnit[10] = uint10;
            lstUnit[11] = uint11;
            lstUnit[12] = uint12;
            lstUnit[13] = uint13;
            lstUnit[14] = uint14;
            lstUnit[15] = uint15;
            lstUnit[16] = uint16;
            lstUnit[17] = uint17;
            lstUnit[18] = uint18;
            lstUnit[19] = uint19;
            lstUnit[20] = uint20;
            lstUnit[21] = uint21;
            lstUnit[22] = uint22;
            lstUnit[23] = uint23;
            lstUnit[24] = uint24;
            lstUnit[25] = uint25;
            lstUnit[26] = uint26;
            lstUnit[27] = uint27;
            lstUnit[28] = uint28;
            lstUnit[29] = uint29;
            lstUnit[30] = uint30;
            lstUnit[31] = uint31;
            lstUnit[32] = uint32;
            lstUnit[33] = uint33;
            lstUnit[34] = uint34;
            lstUnit[35] = uint35;
            lstUnit[36] = uint36;
            lstUnit[37] = uint37;
            lstUnit[38] = uint38;
            lstUnit[39] = uint39;
            lstUnit[40] = uint40;
            lstUnit[41] = uint41;
            lstUnit[42] = uint42;
            lstUnit[43] = uint43;
            lstUnit[44] = uint44;
            lstUnit[45] = uint45;
            lstUnit[46] = uint46;
            lstUnit[47] = uint47;
            lstUnit[48] = uint48;
            lstUnit[49] = uint49;

            valmoWin.dv.PrdPr[22].add();
            valmoWin.dv.PrdPr[26].add();
            valmoWin.dv.PrdPr[33].add();
            valmoWin.dv.PrdPr[40].add();
            valmoWin.dv.PrdPr[61].add();
            valmoWin.dv.PrdPr[47].add();
            valmoWin.dv.PrdPr[54].add();
            valmoWin.dv.PrdPr[68].add();
            valmoWin.dv.PrdPr[75].add();
            valmoWin.dv.PrdPr[82].add();
            valmoWin.dv.PrdPr[89].add();
            valmoWin.dv.PrdPr[105].add();
            valmoWin.dv.PrdPr[106].add();

            valmoWin.dv.PrdPr[215].addHandle(refush);

        }

        public void DataInit()
        {
            lstPrdData = new ProductParam[10];

            for (int i = 0; i < 10; i++)
            {
                lstPrdData[i] = new ProductParam();
            }

            int count = valmoWin.dv.PrdPr[106].valueNew;
            int[] historyData = new int[count * 10];
            Lasal32.GetData(historyData, (uint)valmoWin.dv.PrdPr[105].valueNew, count * 40);

            for (int i = 0; i < count; i++)
            {
                lstPrdData[0].newValue = valmoWin.dv.PrdPr[187].getDblValue(historyData[i * 10]);
                lstPrdData[1].newValue = valmoWin.dv.PrdPr[33].getDblValue(historyData[i * 10 + 1]);
                lstPrdData[2].newValue = valmoWin.dv.PrdPr[40].getDblValue(historyData[i * 10 + 2]);
                lstPrdData[3].newValue = valmoWin.dv.PrdPr[47].getDblValue(historyData[i * 10 + 3]);
                lstPrdData[4].newValue = valmoWin.dv.PrdPr[54].getDblValue(historyData[i * 10 + 4]);
                lstPrdData[5].newValue = valmoWin.dv.PrdPr[61].getDblValue(historyData[i * 10 + 5]);
                lstPrdData[6].newValue = valmoWin.dv.PrdPr[68].getDblValue(historyData[i * 10 + 6]);
                lstPrdData[7].newValue = valmoWin.dv.PrdPr[75].getDblValue(historyData[i * 10 + 7]);
                lstPrdData[8].newValue = valmoWin.dv.PrdPr[82].getDblValue(historyData[i * 10 + 8]);
                lstPrdData[9].newValue = valmoWin.dv.PrdPr[89].getDblValue(historyData[i * 10 + 9]);
            }

            for (int i = 0; i < count; i++)
            {
                lstUnit[i].bVisiable = true;
            }

            if (count < 50)
            {
                for (int i = count; i < 50; i++)
                {
                    lstUnit[i].bVisiable = false;
                }
            }

            for (int i = 0; i < count; i++)
            {
                lstUnit[count - i - 1].dataInitialize(lstPrdData[0][i], lstPrdData[1][i], lstPrdData[2][i], lstPrdData[3][i], lstPrdData[4][i],
                    lstPrdData[5][i], lstPrdData[6][i], lstPrdData[7][i], lstPrdData[8][i], lstPrdData[9][i]);
            }

            vMax.dataInitialize(lstPrdData[0].max, lstPrdData[1].max, lstPrdData[2].max, lstPrdData[3].max, lstPrdData[4].max,
                lstPrdData[5].max, lstPrdData[6].max, lstPrdData[7].max, lstPrdData[8].max, lstPrdData[9].max);
            vMin.dataInitialize(lstPrdData[0].min, lstPrdData[1].min, lstPrdData[2].min, lstPrdData[3].min, lstPrdData[4].min,
                lstPrdData[5].min, lstPrdData[6].min, lstPrdData[7].min, lstPrdData[8].min, lstPrdData[9].min);
            vAvg.dataInitialize(lstPrdData[0].avg, lstPrdData[1].avg, lstPrdData[2].avg, lstPrdData[3].avg, lstPrdData[4].avg,
                lstPrdData[5].avg, lstPrdData[6].avg, lstPrdData[7].avg, lstPrdData[8].avg, lstPrdData[9].avg);
            vOff.dataInitialize(lstPrdData[0].offset, lstPrdData[1].offset, lstPrdData[2].offset, lstPrdData[3].offset, lstPrdData[4].offset,
                lstPrdData[5].offset, lstPrdData[6].offset, lstPrdData[7].offset, lstPrdData[8].offset, lstPrdData[9].offset);
        }

        bool isInit = false;
        public void refush(objUnit obj)
        {
            if (isInit == false)
            {
                DataInit();

                isInit = true;
            }

            double inj_HoldingTime = valmoWin.dv.PrdPr[187].vDbl;
            double carriageTime = valmoWin.dv.PrdPr[33].vDbl;
            double carriageTimeBase = valmoWin.dv.PrdPr[34].vDbl;
            double cycleTime = valmoWin.dv.PrdPr[40].vDbl;
            double cycleTimeBase = valmoWin.dv.PrdPr[41].vDbl;
            double VPPosition = valmoWin.dv.PrdPr[47].vDbl;
            double VPPositionBase = valmoWin.dv.PrdPr[48].vDbl;
            double VPPressure = valmoWin.dv.PrdPr[54].vDbl;
            double VPPressureBase = valmoWin.dv.PrdPr[55].vDbl;
            double VPTime = valmoWin.dv.PrdPr[61].vDbl;
            double VPTimeBase = valmoWin.dv.PrdPr[62].vDbl;
            double InjStartPosition = valmoWin.dv.PrdPr[68].vDbl;
            double InjStartPositionBase = valmoWin.dv.PrdPr[69].vDbl;
            double minCushionPosition = valmoWin.dv.PrdPr[75].vDbl;
            double minCushionPositionBase = valmoWin.dv.PrdPr[76].vDbl;
            double cushionCompletePosition = valmoWin.dv.PrdPr[82].vDbl;
            double cushionCompletePositionBase = valmoWin.dv.PrdPr[83].vDbl;
            double injPeakPressure = valmoWin.dv.PrdPr[89].vDbl;
            double injPeakPressureBase = valmoWin.dv.PrdPr[90].vDbl;

            lstPrdData[0].newValue = inj_HoldingTime;
            lstPrdData[1].newValue = carriageTime;
            lstPrdData[2].newValue = cycleTime;
            lstPrdData[3].newValue = VPPosition;
            lstPrdData[4].newValue = VPPressure;
            lstPrdData[5].newValue = VPTime;
            lstPrdData[6].newValue = InjStartPosition;
            lstPrdData[7].newValue = minCushionPosition;
            lstPrdData[8].newValue = cushionCompletePosition;
            lstPrdData[9].newValue = injPeakPressure;

            string carriageTimeOffset = valmoWin.dv.PrdPr[33].getStrValue(Math.Abs(carriageTime - carriageTimeBase));
            string cycleTimeOffset = valmoWin.dv.PrdPr[40].getStrValue(Math.Abs(cycleTime - cycleTimeBase));
            string VPPositionOffset = valmoWin.dv.PrdPr[47].getStrValue(Math.Abs(VPPosition - VPPositionBase));
            string VPPressureOffset = valmoWin.dv.PrdPr[54].getStrValue(Math.Abs(VPPressure - VPPressureBase));
            string VPTimeOffset = valmoWin.dv.PrdPr[61].getStrValue(Math.Abs(VPTime - VPTimeBase));
            string InjStartPositionOffset = valmoWin.dv.PrdPr[68].getStrValue(Math.Abs(InjStartPosition - InjStartPositionBase));
            string minCushionPositionOffset = valmoWin.dv.PrdPr[75].getStrValue(Math.Abs(minCushionPosition - minCushionPositionBase));
            string cushionCompletePositionOffset = valmoWin.dv.PrdPr[82].getStrValue(Math.Abs(cushionCompletePosition - cushionCompletePositionBase));
            string injPeakPressureOffset = valmoWin.dv.PrdPr[89].getStrValue(Math.Abs(injPeakPressure - injPeakPressureBase));

            int count = lstPrdData[0].Count;


            for (int i = 0; i < count; i++)
            {
                lstUnit[i].bVisiable = true;
            }

            if (count < 50)
            {
                for (int i = count; i < 50; i++)
                {
                    lstUnit[i].bVisiable = false;
                }
            }

            for (int i = 0; i < count; i++)
            {
                lstUnit[count - i - 1].dataInitialize(lstPrdData[0][i], lstPrdData[1][i], lstPrdData[2][i], lstPrdData[3][i], lstPrdData[4][i],
                    lstPrdData[5][i], lstPrdData[6][i], lstPrdData[7][i], lstPrdData[8][i], lstPrdData[9][i]);
            }

            vMax.dataInitialize(lstPrdData[0].max, lstPrdData[1].max, lstPrdData[2].max, lstPrdData[3].max, lstPrdData[4].max,
                lstPrdData[5].max, lstPrdData[6].max, lstPrdData[7].max, lstPrdData[8].max, lstPrdData[9].max);
            vMin.dataInitialize(lstPrdData[0].min, lstPrdData[1].min, lstPrdData[2].min, lstPrdData[3].min, lstPrdData[4].min,
                lstPrdData[5].min, lstPrdData[6].min, lstPrdData[7].min, lstPrdData[8].min, lstPrdData[9].min);
            vAvg.dataInitialize(lstPrdData[0].avg, lstPrdData[1].avg, lstPrdData[2].avg, lstPrdData[3].avg, lstPrdData[4].avg,
                lstPrdData[5].avg, lstPrdData[6].avg, lstPrdData[7].avg, lstPrdData[8].avg, lstPrdData[9].avg);
            vOff.dataInitialize(lstPrdData[0].offset, lstPrdData[1].offset, lstPrdData[2].offset, lstPrdData[3].offset, lstPrdData[4].offset,
                lstPrdData[5].offset, lstPrdData[6].offset, lstPrdData[7].offset, lstPrdData[8].offset, lstPrdData[9].offset);

            lbOffset2.Content = (carriageTime > carriageTimeBase) ? "+" + carriageTimeOffset : "-" + carriageTimeOffset;
            lbOffset3.Content = (cycleTime > cycleTimeBase) ? "+" + cycleTimeOffset : "-" + cycleTimeOffset;
            lbOffset4.Content = (VPPosition > VPPositionBase) ? "+" + VPPositionOffset : "-" + VPPositionOffset;
            lbOffset5.Content = (VPPressure > VPPressureBase) ? "+" + VPPressureOffset : "-" + VPPressureOffset;
            lbOffset6.Content = (VPTime > VPTimeBase) ? "+" + VPTimeOffset : "-" + VPTimeOffset;
            lbOffset7.Content = (InjStartPosition > InjStartPositionBase) ? "+" + InjStartPositionOffset : "-" + InjStartPositionOffset;
            lbOffset8.Content = (minCushionPosition > minCushionPositionBase) ? "+" + minCushionPositionOffset : "-" + minCushionPositionOffset;
            lbOffset9.Content = (cushionCompletePosition > cushionCompletePositionBase) ? "+" + cushionCompletePositionOffset : "-" + cushionCompletePositionOffset;
            lbOffset10.Content = (injPeakPressure > injPeakPressureBase) ? "+" + injPeakPressureOffset : "-" + injPeakPressureOffset;

            //if (lstPrdData[0].maxIndex == lstPrdData[0].minIndex)
            //{
            //    arrMax1.Visibility = Visibility.Hidden;
            //    arrMin1.Visibility = Visibility.Hidden;
            //}
            //else
            //{
            //    Canvas.SetTop(arrMax1, lstPrdData[0].maxIndex * 25 + 6);
            //    Canvas.SetTop(arrMin1, lstPrdData[0].minIndex * 25 + 6);
            //}

            //if (lstPrdData[1].maxIndex == lstPrdData[1].minIndex)
            //{
            //    arrMax2.Visibility = Visibility.Hidden;
            //    arrMin2.Visibility = Visibility.Hidden;
            //}
            //else
            //{
            //    Canvas.SetTop(arrMax2, lstPrdData[1].maxIndex * 25 + 6);
            //    Canvas.SetTop(arrMin2, lstPrdData[1].minIndex * 25 + 6);
            //}

            //if (lstPrdData[2].maxIndex == lstPrdData[2].minIndex)
            //{
            //    arrMax3.Visibility = Visibility.Hidden;
            //    arrMin3.Visibility = Visibility.Hidden;
            //}
            //else
            //{
            //    Canvas.SetTop(arrMax3, lstPrdData[2].maxIndex * 25 + 6);
            //    Canvas.SetTop(arrMin3, lstPrdData[2].minIndex * 25 + 6);
            //}

            //if (lstPrdData[3].maxIndex == lstPrdData[3].minIndex)
            //{
            //    arrMax4.Visibility = Visibility.Hidden;
            //    arrMin4.Visibility = Visibility.Hidden;
            //}
            //else
            //{
            //    Canvas.SetTop(arrMax4, lstPrdData[3].maxIndex * 25 + 6);
            //    Canvas.SetTop(arrMin4, lstPrdData[3].minIndex * 25 + 6);
            //}

            //if (lstPrdData[4].maxIndex == lstPrdData[4].minIndex)
            //{
            //    arrMax5.Visibility = Visibility.Hidden;
            //    arrMin5.Visibility = Visibility.Hidden;
            //}
            //else
            //{
            //    Canvas.SetTop(arrMax5, lstPrdData[4].maxIndex * 25 + 6);
            //    Canvas.SetTop(arrMin5, lstPrdData[4].minIndex * 25 + 6);
            //}

            //if (lstPrdData[5].maxIndex == lstPrdData[5].minIndex)
            //{
            //    arrMax6.Visibility = Visibility.Hidden;
            //    arrMin6.Visibility = Visibility.Hidden;
            //}
            //else
            //{
            //    Canvas.SetTop(arrMax6, lstPrdData[5].maxIndex * 25 + 6);
            //    Canvas.SetTop(arrMin6, lstPrdData[5].minIndex * 25 + 6);
            //}

            //if (lstPrdData[6].maxIndex == lstPrdData[6].minIndex)
            //{
            //    arrMax7.Visibility = Visibility.Hidden;
            //    arrMin7.Visibility = Visibility.Hidden;
            //}
            //else
            //{
            //    Canvas.SetTop(arrMax7, lstPrdData[6].maxIndex * 25 + 6);
            //    Canvas.SetTop(arrMin7, lstPrdData[6].minIndex * 25 + 6);
            //}

            //if (lstPrdData[7].maxIndex == lstPrdData[7].minIndex)
            //{
            //    arrMax8.Visibility = Visibility.Hidden;
            //    arrMin8.Visibility = Visibility.Hidden;
            //}
            //else
            //{
            //    Canvas.SetTop(arrMax8, lstPrdData[7].maxIndex * 25 + 6);
            //    Canvas.SetTop(arrMin8, lstPrdData[7].minIndex * 25 + 6);
            //}

            //if (lstPrdData[8].maxIndex == lstPrdData[8].minIndex)
            //{
            //    arrMax9.Visibility = Visibility.Hidden;
            //    arrMin9.Visibility = Visibility.Hidden;
            //}
            //else
            //{
            //    Canvas.SetTop(arrMax9, lstPrdData[8].maxIndex * 25 + 6);
            //    Canvas.SetTop(arrMin9, lstPrdData[8].minIndex * 25 + 6);
            //}

            //if (lstPrdData[9].maxIndex == lstPrdData[9].minIndex)
            //{
            //    arrMax10.Visibility = Visibility.Hidden;
            //    arrMin10.Visibility = Visibility.Hidden;
            //}
            //else
            //{
            //    Canvas.SetTop(arrMax10, lstPrdData[9].maxIndex * 25 + 6);
            //    Canvas.SetTop(arrMin10, lstPrdData[9].minIndex * 25 + 6);
            //}
        }

        private bool isMouseDown = false;
        private Point lastMousePos;
        private void cvsBack_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            if (lstPrdData[0].Count * 25 < cvsBack.Height)
            {
                return;
            }

            isMouseDown = true;
            lastMousePos = e.GetPosition(cvsMain);
        }

        private void cvsBack_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown == true)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point curMousePos = e.GetPosition(cvsMain);

                    double oldTop = Canvas.GetTop(sPanel);
                    double newTop = curMousePos.Y - lastMousePos.Y + oldTop;

                    if (newTop <= -(lstPrdData[0].Count * 25 - cvsBack.Height))
                        newTop = -(lstPrdData[0].Count * 25 - cvsBack.Height);
                    if (newTop > 0)
                        newTop = 0;
                    Canvas.SetTop(sPanel, newTop);
                    Canvas.SetTop(cvsCurrent, newTop);
                    lastMousePos = curMousePos;
                }
            }
        }

        private void cvsBack_MouseLeave(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }

        private void cvsBack_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = false;
        }
    }

    public class ProductParam
    {
        private List<double> historyValue;

        public int Count
        {
            get
            {
                return historyValue.Count;
            }
        }
        public double newValue
        {
            set
            {
                historyValue.Add(value);

                if (historyValue.Count > 50)
                {
                    historyValue.RemoveAt(0);
                }

                reCalculate();
            }
        }

        public double max;
        public int maxIndex;
        public double min;
        public int minIndex;
        public double avg;
        public double offset;

        public ProductParam()
        {
            historyValue = new List<double>();

            max = 0;
            maxIndex = 0;
            min = 0;
            minIndex = 0;
            avg = 0;
            offset = 0;
        }

        public double this[int index]
        {
            get
            {
                if (index >= historyValue.Count)
                {
                    return 0;
                }
                else
                {
                    return historyValue[index];
                }
            }
        }

        public void reCalculate()
        {
            if (historyValue.Count > 0)
            {
                min = max = historyValue[0];

                double temp = 0;
                foreach (double d in historyValue)
                {
                    if (d > max)
                    {
                        max = d;
                    }

                    if (d < min)
                    {
                        min = d;
                    }

                    temp += d;
                }

                avg = temp / historyValue.Count;
                offset = max - min;

                maxIndex = historyValue.IndexOf(max);
                minIndex = historyValue.IndexOf(min);
            }
        }
    }
}
