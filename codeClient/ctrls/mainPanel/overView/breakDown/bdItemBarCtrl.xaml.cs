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
    public partial class bdItemBarCtrl : UserControl
    {
        private objUnit tmObj = valmoWin.dv.tmTypeObj;

        public Visibility visiable
        {
            set
            {
                if (value == Visibility.Visible)
                {
                    cvsBackground.Height = 58;
                }
                else
                {
                    cvsBackground.Height = 0;
                }
            }
        }
        private typeName _type;
        /// <summary>
        /// 获取或设置动作条类型
        /// </summary>
        public typeName type
        {
            set
            {
                _type = value;

                object sourceImg = TryFindResource("picIpr" + Convert.ToInt32(_type).ToString().PadLeft(3, '0'));
                if (sourceImg != null)
                {
                    ActionImg.Source = (BitmapImage)sourceImg;
                }

                object strName = TryFindResource("strIpr" + Convert.ToInt32(_type).ToString().PadLeft(3, '0'));
                if (strName != null)
                {
                    ActionName.Content = strName.ToString();
                }
            }
            get
            {
                return _type;
            }
        }

        public bdItemBarCtrl()
        {
            InitializeComponent();

            _type = typeName.noactive;
            cvsBackground.Height = 0;
        }

        public void setCurAction(Anablocks curAction)
        {
            double cycleTime = valmoWin.dv.PrdPr[269].vDbl;

            if (curAction.seqEnd == curAction.seqStart)
            {
                if (curAction.tEnd < curAction.tStart)
                {
                    curAction.tEnd = curAction.tStart;
                }

                if (curAction.tStart > cycleTime)
                {
                    curAction.tStart = curAction.tEnd = cycleTime;
                }

                if (curAction.tEnd > cycleTime)
                {
                    curAction.tEnd = cycleTime;
                }

                double actionTime = curAction.tEnd - curAction.tStart;

                if (curAction.Move == 1)
                {
                    cvsMain.Background = new SolidColorBrush(Color.FromRgb(0xCB, 0xE5, 0xEA));
                }
                else
                {
                    cvsMain.Background = new SolidColorBrush(Color.FromRgb(220, 220, 220));

                    if (cycleTime > 0)
                    {
                        lbPer.Content = (100 * actionTime / cycleTime).ToString("0.0");
                    }
                }

                lbTimeBeginCur.Content = curAction.tStart.ToString("0.00");
                lbTimeContinueCur.Content = actionTime.ToString("0.00");
                lbTimeEndCur.Content = curAction.tEnd.ToString("0.00");

                lbCur.Content = actionTime.ToString("0.00");
                if (curAction.tStart * bdItemPage.sRate > 500)
                {
                    Canvas.SetRight(lbCur, 600 - curAction.tStart * bdItemPage.sRate);
                }
                else
                {
                    Canvas.SetLeft(lbCur, curAction.tStart * bdItemPage.sRate);
                }

                Canvas.SetLeft(prgCur, curAction.tStart * bdItemPage.sRate);
                prgCur.X2 = actionTime * bdItemPage.sRate;

                prgCur2.X2 = 0;
            }
            else if (curAction.seqEnd > curAction.seqStart)
            {
                if (curAction.tEnd > cycleTime)
                {
                    curAction.tEnd = cycleTime;
                }
                if (curAction.tStart > cycleTime)
                {
                    curAction.tStart = cycleTime;
                }

                double actionTime = cycleTime - curAction.tStart + curAction.tEnd;

                if (curAction.Move == 1)
                {
                    cvsMain.Background = new SolidColorBrush(Color.FromRgb(0xCB, 0xE5, 0xEA));
                }
                else
                {
                    cvsMain.Background = new SolidColorBrush(Color.FromRgb(220, 220, 220));


                    if (cycleTime > 0)
                    {
                        if (actionTime > cycleTime)
                        {
                            actionTime = cycleTime;
                        }
                        lbPer.Content = (100 * actionTime / cycleTime).ToString("0.0");
                    }
                }

                lbTimeBeginCur.Content = curAction.tStart.ToString("0.00");
                lbTimeContinueCur.Content = actionTime.ToString("0.00");
                lbTimeEndCur.Content = curAction.tEnd.ToString("0.00");

                lbCur.Content = actionTime.ToString("0.00");
                if (curAction.tStart * bdItemPage.sRate > 500)
                {
                    Canvas.SetRight(lbCur, 600 - curAction.tStart * bdItemPage.sRate);
                }
                else
                {
                    Canvas.SetLeft(lbCur, curAction.tStart * bdItemPage.sRate);
                }

                Canvas.SetLeft(prgCur, curAction.tStart * bdItemPage.sRate);
                prgCur.X2 = 600 - curAction.tStart * bdItemPage.sRate;

                prgCur2.X2 = curAction.seqEnd * bdItemPage.sRate;
            }
            else
            {
                return;
            }
        }

        public void setBaseAction(Anablocks curAction)
        {
            double cycleTime = valmoWin.dv.PrdPr[269].vDbl;

            if (curAction.seqEnd == curAction.seqStart)
            {
                if (curAction.tEnd < curAction.tStart)
                {
                    curAction.tEnd = curAction.tStart;
                }

                if (curAction.tStart > cycleTime)
                {
                    curAction.tStart = curAction.tEnd = cycleTime;
                }

                double actionTime = curAction.tEnd - curAction.tStart;

                lbTimeBeginBase.Content = curAction.tStart.ToString("0.00");
                lbTimeContinueBase.Content = actionTime.ToString("0.00");
                lbTimeEndBase.Content = curAction.tEnd.ToString("0.00");

                lbBase.Content = actionTime.ToString("0.00");
                
                if (curAction.tStart * bdItemPage.sRate > 500)
                {
                    Canvas.SetRight(lbBase, Convert.ToDouble(600 - curAction.tStart * bdItemPage.sRate));
                }
                else
                {
                    Canvas.SetLeft(lbBase, curAction.tStart * bdItemPage.sRate);
                }

                Canvas.SetLeft(prgBase, curAction.tStart * bdItemPage.sRate);
                prgBase.X2 = actionTime * bdItemPage.sRate;

                prgBase2.X2 = 0;
            }
            else if (curAction.seqEnd > curAction.seqStart)
            {
                if (curAction.tEnd > cycleTime)
                {
                    curAction.tEnd = cycleTime;
                }
                if (curAction.tStart > cycleTime)
                {
                    curAction.tStart = cycleTime;
                }

                double actionTime = cycleTime - curAction.tStart + curAction.tEnd;

                lbTimeBeginBase.Content = curAction.tStart.ToString("0.00");
                lbTimeContinueBase.Content = actionTime.ToString("0.00");
                lbTimeEndBase.Content = curAction.tEnd.ToString("0.00");

                lbBase.Content = actionTime.ToString("0.00");
                if (curAction.tStart * bdItemPage.sRate > 500)
                {
                    App.log.Info("SetRight \t" + (600 - curAction.tStart * bdItemPage.sRate));
                    Canvas.SetRight(lbBase, 600 - curAction.tStart * bdItemPage.sRate);
                }
                else
                {
                    Canvas.SetLeft(lbBase, curAction.tStart * bdItemPage.sRate);
                }

                Canvas.SetLeft(prgBase, curAction.tStart * bdItemPage.sRate);
                prgBase.X2 = 600 - curAction.tStart * bdItemPage.sRate;

                prgBase2.X2 = curAction.seqEnd * bdItemPage.sRate;
            }
            else
            {
                return;
            }
        }

    }
}
