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
    /// Carriage_Main.xaml 的交互逻辑
    /// </summary>
    public partial class Carriage_Main : UserControl
    {   
        /// <summary>
        /// 螺杆位置
        /// </summary>
        private double ActInjExternPosition = 0;
        /// <summary>
        /// 螺杆直径
        /// </summary>
        private double ScrewDiameter = 0;
        /// <summary>
        /// 最大注射行程
        /// </summary>
        private double MaxDisplacement = 1;
        /// <summary>
        /// 残料位置
        /// </summary>
        private double CompletePos = 0;
        /// <summary>
        /// 前松退位置
        /// </summary>
        private double PRPos = 0;
        /// <summary>
        /// 后松退位置
        /// </summary>
        private double SBPos = 0;
        private Point[] lstPointSpeed = new Point[6];
        private List<Ring> lstRingSpeed = new List<Ring>();
        private List<Line> lstLineSpeed = new List<Line>();
        private Point[] lstPointPressure = new Point[6];
        private List<Ring> lstRingPressure = new List<Ring>();
        private List<Line> lstLinePressure = new List<Line>();

        public Carriage_Main()
        {
            InitializeComponent();

            init();

            valmoWin.lstStartUpInit.Add(startUpInit);
            valmoWin.dv.InjPr[280].addHandle(updateInjUnitState);
        }

        private void updateInjUnitState(objUnit obj)
        {
            BarInj290.Visibility = (obj.value == 1) ? Visibility.Visible : Visibility.Hidden;
        }

        /// <summary>
        /// 曲线数据地址初始化
        /// </summary>
        private void startUpInit()
        {
            MaxDisplacement = valmoWin.dv.InjPr[190].vDbl;
        }

        public void init()
        {
            for (int i = 0; i < 5; i++)
            {
                Line lSpeed = new Line();
                lSpeed.Stroke = new SolidColorBrush(Color.FromArgb(0xFF, 0x33, 0x79, 0xE1));
                lSpeed.StrokeThickness = 1.5;
                lSpeed.ClipToBounds = true;
                lSpeed.Visibility = Visibility.Hidden;
                cvsMap.Children.Add(lSpeed);
                lstLineSpeed.Add(lSpeed);

                Line lPressure = new Line();
                lPressure.Stroke = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0x00, 0x95));
                lPressure.StrokeThickness = 1.5;
                lPressure.ClipToBounds = true;
                lPressure.Visibility = Visibility.Hidden;
                cvsMap.Children.Add(lPressure);
                lstLinePressure.Add(lPressure);
            }
            for (int i = 0; i < 6; i++)
            {
                Ring rSpeed = new Ring();
                rSpeed.Color = new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0x78, 0xE1));
                rSpeed.Visibility = Visibility.Hidden;
                cvsMap.Children.Add(rSpeed);
                lstRingSpeed.Add(rSpeed);

                Ring rPressure = new Ring();
                rPressure.Color = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0x00, 0x95));
                rPressure.Visibility = Visibility.Hidden;
                cvsMap.Children.Add(rPressure);
                lstRingPressure.Add(rPressure);
            }
            lstPointSpeed[5].X = 0;
            lstPointPressure[5].X = 0;

            valmoWin.dv.InjPr[81].addHandle(handleInjPr_81);
            valmoWin.dv.InjPr[82].addHandle(handleInjPr_82);
            valmoWin.dv.InjPr[83].addHandle(handleInjPr_83);
            valmoWin.dv.InjPr[278].add();

            valmoWin.dv.InjPr[207].addHandle(handle_Inj207);
            valmoWin.dv.InjPr[154].addHandle(handle_Inj154);
            valmoWin.dv.InjPr[190].addHandle(handle_Inj190);
            valmoWin.dv.InjPr[146].addHandle(handle_Inj146);
            valmoWin.dv.InjPr[96].addHandle(handle_Inj096);
            valmoWin.dv.InjPr[97].addHandle(handle_Inj097);
            valmoWin.dv.InjPr[98].addHandle(handle_Inj098);
            valmoWin.dv.InjPr[90].addHandle(handle_Inj090);
            valmoWin.dv.InjPr[91].addHandle(handle_Inj091);
            valmoWin.dv.InjPr[92].addHandle(handle_Inj092);
            valmoWin.dv.InjPr[87].addHandle(handle_Inj087);
            valmoWin.dv.InjPr[88].addHandle(handle_Inj088);
            valmoWin.dv.InjPr[89].addHandle(handle_Inj089);
            valmoWin.dv.InjPr[31].addHandle(handle_Inj031);
            valmoWin.dv.InjPr[21].addHandle(handle_Inj021);
            valmoWin.dv.InjPr[85].addHandle(handle_Inj085);
            valmoWin.dv.InjPr[151].addHandle(handle_Inj151);
            valmoWin.dv.InjPr[99].addHandle(handle_Inj099);
        }
        private void handle_Inj151(objUnit obj)
        {
            lb151.Content = obj.vDblStr;
        }

        private void handle_Inj031(objUnit obj)
        {
            //残料位置
            CompletePos = obj.vDbl > 0 ? obj.vDbl : 0;
            updateCompletePos();
        }
        private void handle_Inj085(objUnit obj)
        {
            //前松退位置
            PRPos = obj.vDbl > 0 ? obj.vDbl : 0;
            updatePRPos();
        }
        private void handle_Inj021(objUnit obj)
        {
            //螺杆位置
            ActInjExternPosition = obj.vDbl > 0 ? obj.vDbl : 0;

            Canvas.SetLeft(cvsCursor, ActInjExternPosition / MaxDisplacement * 830);
            Canvas.SetLeft(lbCurScrewPos, ActInjExternPosition / MaxDisplacement * 830 > 765 ? -65 : 0);
            lbCurScrewPos.Content = ActInjExternPosition.ToString("0.00");

            refush();
        }
        private void handle_Inj099(objUnit obj)
        {
            //后松退位置
            SBPos = obj.vDbl > 0 ? obj.vDbl : 0;
            updateSBPos();
        }

        private void refush()
        {
            updateCompletePos();
            updatePRPos();
            updateActInjExtPos();
            updateSBPos();
        }
        private void updateCompletePos()
        {
            if (CompletePos < ActInjExternPosition)
            {
                cvsCushionCompletePos.Width = CompletePos / MaxDisplacement * 830;
            }
            else
            {
                cvsCushionCompletePos.Width = ActInjExternPosition / MaxDisplacement * 830;
            }
        }
        private void updatePRPos()
        {
            if (PRPos < ActInjExternPosition)
            {
                cvsPR.Width = PRPos / MaxDisplacement * 830;
                lbPR.Content = (PRPos * Math.PI * Math.Pow(ScrewDiameter / 2, 2) / 1000).ToString("0.00");
            }
            else
            {
                cvsPR.Width = ActInjExternPosition / MaxDisplacement * 830;
                lbPR.Content = (ActInjExternPosition * Math.PI * Math.Pow(ScrewDiameter / 2, 2) / 1000).ToString("0.00");
            }
        }
        private void updateActInjExtPos()
        {
            if (sBP_S3 < ActInjExternPosition)
            {
                cvsIncExternPos.Width = sBP_S3 / MaxDisplacement * 830;
                lbIncExternPos.Content = (sBP_S3 * Math.PI * Math.Pow(ScrewDiameter / 2, 2) / 1000).ToString("0.00");
            }
            else
            {
                cvsIncExternPos.Width = ActInjExternPosition / MaxDisplacement * 830;
                lbIncExternPos.Content = (ActInjExternPosition * Math.PI * Math.Pow(ScrewDiameter / 2, 2) / 1000).ToString("0.00");
            }
        }
        private void updateSBPos()
        {
            cvsSB.Width = ActInjExternPosition / MaxDisplacement * 830;
            lbSB.Content = (SBPos * Math.PI * Math.Pow(ScrewDiameter / 2, 2) / 1000).ToString("0.00");
        }

        private void handle_Inj207(objUnit obj)
        {
            ScrewDiameter = obj.vDbl > 0 ? obj.vDbl : 0;
            refush();
        }
        /// <summary>
        /// handle_Inj096
        /// </summary>
        /// <param name="obj">储料第一段背压</param>
        private void handle_Inj096(objUnit obj)
        {
            lstPointPressure[4].Y = obj.vDbl;
            lstPointPressure[5].Y = obj.vDbl;
            refushPressure();
        }
        /// <summary>
        /// handle_Inj097
        /// </summary>
        /// <param name="obj">储料第二段背压</param>
        private void handle_Inj097(objUnit obj)
        {
            lstPointPressure[2].Y = obj.vDbl;
            lstPointPressure[3].Y = obj.vDbl;
            refushPressure();
        }
        /// <summary>
        /// handle_Inj098
        /// </summary>
        /// <param name="obj">储料第三段背压</param>
        private void handle_Inj098(objUnit obj)
        {
            lstPointPressure[1].Y = obj.vDbl;
            lstPointPressure[0].Y = obj.vDbl;
            refushPressure();
        }
        /// <summary>
        /// handle_Inj090
        /// </summary>
        /// <param name="obj">储料第一段速度</param>
        private void handle_Inj090(objUnit obj)
        {
            lstPointSpeed[4].Y = obj.vDbl;
            lstPointSpeed[5].Y = obj.vDbl;
            refushSpeed();
        }
        /// <summary>
        /// handle_Inj091
        /// </summary>
        /// <param name="obj">储料第二段速度</param>
        private void handle_Inj091(objUnit obj)
        {
            lstPointSpeed[2].Y = obj.vDbl;
            lstPointSpeed[3].Y = obj.vDbl;
            refushSpeed();
        }
        /// <summary>
        /// handle_Inj092
        /// </summary>
        /// <param name="obj">储料第三段速度</param>
        private void handle_Inj092(objUnit obj)
        {
            lstPointSpeed[0].Y = obj.vDbl;
            lstPointSpeed[1].Y = obj.vDbl;
            refushSpeed();
        }
        /// <summary>
        /// handle_Inj087
        /// </summary>
        /// <param name="obj">储料第一段位置</param>
        private void handle_Inj087(objUnit obj)
        {
            lstPointPressure[3].X = obj.vDbl;
            lstPointPressure[4].X = obj.vDbl;
            refushPressure();

            lstPointSpeed[3].X = obj.vDbl;
            lstPointSpeed[4].X = obj.vDbl;
            refushSpeed();
        }
        /// <summary>
        /// handle_Inj088
        /// </summary>
        /// <param name="obj">储料第二段位置</param>
        private void handle_Inj088(objUnit obj)
        {
            lstPointPressure[1].X = obj.vDbl;
            lstPointPressure[2].X = obj.vDbl;
            refushPressure();

            lstPointSpeed[1].X = obj.vDbl;
            lstPointSpeed[2].X = obj.vDbl;
            refushSpeed();
        }
        private double sBP_S3 = 0;
        /// <summary>
        /// handle_Inj089
        /// </summary>
        /// <param name="obj">储料第三段位置</param>
        private void handle_Inj089(objUnit obj)
        {
            lstPointPressure[0].X = obj.vDbl;
            refushPressure();

            lstPointSpeed[0].X = obj.vDbl;
            refushSpeed();

            sBP_S3 = obj.vDbl > 0 ? obj.vDbl : 0;

            updateActInjExtPos();
            updateSBPos();
        }

        private double MaxSpeed = 0.01;
        private double MaxPressure = 30;
        /// <summary>
        /// handle_Inj146
        /// </summary>
        /// <param name="obj">储料轴最大转速</param>
        private void handle_Inj146(objUnit obj)
        {
            MaxSpeed = obj.vDbl > 0 ? obj.vDbl : 0.01;
            setCarriageSpeedStaff(MaxSpeed);

            refushSpeed();
        }

        /// <summary>
        /// handle_Inj190
        /// </summary>
        /// <param name="obj">最大注射行程</param>
        private void handle_Inj190(objUnit obj)
        {
            MaxDisplacement = obj.vDbl > 0 ? obj.vDbl : 1;
            setCarriageDisplacementStaff(MaxDisplacement);

            refush();
            refushSpeed();
            refushPressure();
        }
        /// <summary>
        /// 最大注射容积
        /// </summary>
        private double MaxVolume = 1;
        /// <summary>
        /// handel_Inj154
        /// </summary>
        /// <param name="obj">最大注射容积</param>
        private void handle_Inj154(objUnit obj)
        {
            MaxVolume = obj.vDbl;
            setCarriageVolumeStaff(MaxVolume);
        }

        /// <summary>
        /// 设置储料转速标尺
        /// </summary>
        /// <param name="MaxS">储料轴最大转速</param>
        private void setCarriageSpeedStaff(double MaxS)
        {
            lbV1.Content = (MaxS * 0.1).ToString("0.00");
            lbV2.Content = (MaxS * 0.2).ToString("0.00");
            lbV3.Content = (MaxS * 0.3).ToString("0.00");
            lbV4.Content = (MaxS * 0.4).ToString("0.00");
            lbV5.Content = (MaxS * 0.5).ToString("0.00");
            lbV6.Content = (MaxS * 0.6).ToString("0.00");
            lbV7.Content = (MaxS * 0.7).ToString("0.00");
            lbV8.Content = (MaxS * 0.8).ToString("0.00");
            lbV9.Content = (MaxS * 0.9).ToString("0.00");
            lbV10.Content = MaxS.ToString("0.00");
        }
        /// <summary>
        /// 设置储料标尺
        /// </summary>
        /// <param name="MaxD">储料最大行程</param>
        private void setCarriageDisplacementStaff(double MaxD)
        {
            lbM1.Content = (MaxD * 0.1).ToString("0.00");
            lbM2.Content = (MaxD * 0.2).ToString("0.00");
            lbM3.Content = (MaxD * 0.3).ToString("0.00");
            lbM4.Content = (MaxD * 0.4).ToString("0.00");
            lbM5.Content = (MaxD * 0.5).ToString("0.00");
            lbM6.Content = (MaxD * 0.6).ToString("0.00");
            lbM7.Content = (MaxD * 0.7).ToString("0.00");
            lbM8.Content = (MaxD * 0.8).ToString("0.00");
            lbM9.Content = (MaxD * 0.9).ToString("0.00");
            lbM10.Content = MaxD.ToString("0.00");
        }
        /// <summary>
        /// 设置容积标尺
        /// </summary>
        /// <param name="MaxV">最大注射容积</param>
        private void setCarriageVolumeStaff(double MaxV)
        {
            lbCarriage1.Content = (MaxV * 0.1).ToString("0");
            lbCarriage2.Content = (MaxV * 0.2).ToString("0");
            lbCarriage3.Content = (MaxV * 0.3).ToString("0");
            lbCarriage4.Content = (MaxV * 0.4).ToString("0");
            lbCarriage5.Content = (MaxV * 0.5).ToString("0");
            lbCarriage6.Content = (MaxV * 0.6).ToString("0");
            lbCarriage7.Content = (MaxV * 0.7).ToString("0");
            lbCarriage8.Content = (MaxV * 0.8).ToString("0");
            lbCarriage9.Content = (MaxV * 0.9).ToString("0");
            lbCarriage10.Content = MaxV.ToString("0");
        }

        private void refushSpeed()
        {
            int count = valmoWin.dv.InjPr[82].value;

            switch (count)
            {
                case 1:
                    setPointSpeed(lstRingSpeed[0], lstPointSpeed[0].X, lstPointSpeed[0].Y);
                    setPointSpeed(lstRingSpeed[1], lstPointSpeed[5].X, lstPointSpeed[1].Y);
                    setLineSpeed(lstLineSpeed[0], lstPointSpeed[0].X, lstPointSpeed[0].Y, lstPointSpeed[5].X, lstPointSpeed[1].Y);

                    lstRingSpeed[2].Visibility = Visibility.Hidden;
                    lstRingSpeed[3].Visibility = Visibility.Hidden;
                    lstLineSpeed[1].Visibility = Visibility.Hidden;
                    lstLineSpeed[2].Visibility = Visibility.Hidden;

                    lstRingSpeed[4].Visibility = Visibility.Hidden;
                    lstRingSpeed[5].Visibility = Visibility.Hidden;
                    lstLineSpeed[3].Visibility = Visibility.Hidden;
                    lstLineSpeed[4].Visibility = Visibility.Hidden;
                    break;
                case 2:
                    setPointSpeed(lstRingSpeed[0], lstPointSpeed[0].X, lstPointSpeed[0].Y);
                    setPointSpeed(lstRingSpeed[1], lstPointSpeed[3].X, lstPointSpeed[1].Y);
                    setLineSpeed(lstLineSpeed[0], lstPointSpeed[0].X, lstPointSpeed[0].Y, lstPointSpeed[3].X, lstPointSpeed[1].Y);

                    lstRingSpeed[2].Visibility = Visibility.Hidden;
                    lstRingSpeed[3].Visibility = Visibility.Hidden;
                    lstLineSpeed[1].Visibility = Visibility.Hidden;
                    lstLineSpeed[2].Visibility = Visibility.Hidden;

                    setPointSpeed(lstRingSpeed[4], lstPointSpeed[4].X, lstPointSpeed[4].Y);
                    setPointSpeed(lstRingSpeed[5], lstPointSpeed[5].X, lstPointSpeed[5].Y);
                    setLineSpeed(lstLineSpeed[3], lstPointSpeed[3].X, lstPointSpeed[1].Y, lstPointSpeed[4].X, lstPointSpeed[4].Y);
                    setLineSpeed(lstLineSpeed[4], lstPointSpeed[4].X, lstPointSpeed[4].Y, lstPointSpeed[5].X, lstPointSpeed[5].Y);
                    break;
                case 3:
                    setPointSpeed(lstRingSpeed[0], lstPointSpeed[0].X, lstPointSpeed[0].Y);
                    setPointSpeed(lstRingSpeed[1], lstPointSpeed[1].X, lstPointSpeed[1].Y);
                    setLineSpeed(lstLineSpeed[0], lstPointSpeed[0].X, lstPointSpeed[0].Y, lstPointSpeed[1].X, lstPointSpeed[1].Y);

                    setPointSpeed(lstRingSpeed[2], lstPointSpeed[2].X, lstPointSpeed[2].Y);
                    setPointSpeed(lstRingSpeed[3], lstPointSpeed[3].X, lstPointSpeed[3].Y);
                    setLineSpeed(lstLineSpeed[1], lstPointSpeed[1].X, lstPointSpeed[1].Y, lstPointSpeed[2].X, lstPointSpeed[2].Y);
                    setLineSpeed(lstLineSpeed[2], lstPointSpeed[2].X, lstPointSpeed[2].Y, lstPointSpeed[3].X, lstPointSpeed[3].Y);

                    setPointSpeed(lstRingSpeed[4], lstPointSpeed[4].X, lstPointSpeed[4].Y);
                    setPointSpeed(lstRingSpeed[5], lstPointSpeed[5].X, lstPointSpeed[5].Y);
                    setLineSpeed(lstLineSpeed[3], lstPointSpeed[3].X, lstPointSpeed[3].Y, lstPointSpeed[4].X, lstPointSpeed[4].Y);
                    setLineSpeed(lstLineSpeed[4], lstPointSpeed[4].X, lstPointSpeed[4].Y, lstPointSpeed[5].X, lstPointSpeed[5].Y);
                    break;
                default:
                    break;
            }
        }

        private void setLineSpeed(Line ln, double x1, double y1, double x2, double y2)
        {
            ln.X1 = (int)(x1 / MaxDisplacement * 830);
            ln.Y1 = (int)(250 - y1 / MaxSpeed * 250);
            ln.X2 = (int)(x2 / MaxDisplacement * 830);
            ln.Y2 = (int)(250 - y2 / MaxSpeed * 250);
            ln.Visibility = Visibility.Visible;
        }
        private void setPointSpeed(Ring r, double x, double y)
        {
            Canvas.SetLeft(r, (int)(x / MaxDisplacement * 830 - 4));
            Canvas.SetTop(r, (int)(246 - y / MaxSpeed * 250));
            r.Visibility = Visibility.Visible;
        }

        private void setLinePressure(Line ln, double x1, double y1, double x2, double y2)
        {
            ln.X1 = (int)(x1 / MaxDisplacement * 830);
            ln.Y1 = (int)(250 - y1 / MaxPressure * 250);
            ln.X2 = (int)(x2 / MaxDisplacement * 830);
            ln.Y2 = (int)(250 - y2 / MaxPressure * 250);
            ln.Visibility = Visibility.Visible;
        }
        private void setPointPressure(Ring r, double x, double y)
        {
            Canvas.SetLeft(r, (int)(x / MaxDisplacement * 830 - 5));
            Canvas.SetTop(r, (int)(246 - y / MaxPressure * 250));
            r.Visibility = Visibility.Visible;
        }

        private void refushPressure()
        {
            int count = valmoWin.dv.InjPr[82].value;

            switch (count)
            {
                case 1:
                    setPointPressure(lstRingPressure[0], lstPointPressure[0].X, lstPointPressure[0].Y);
                    setPointPressure(lstRingPressure[1], lstPointPressure[5].X, lstPointPressure[1].Y);
                    setLinePressure(lstLinePressure[0], lstPointPressure[0].X, lstPointPressure[0].Y, lstPointPressure[5].X, lstPointPressure[1].Y);

                    lstRingPressure[2].Visibility = Visibility.Hidden;
                    lstRingPressure[3].Visibility = Visibility.Hidden;
                    lstLinePressure[1].Visibility = Visibility.Hidden;
                    lstLinePressure[2].Visibility = Visibility.Hidden;

                    lstRingPressure[4].Visibility = Visibility.Hidden;
                    lstRingPressure[5].Visibility = Visibility.Hidden;
                    lstLinePressure[3].Visibility = Visibility.Hidden;
                    lstLinePressure[4].Visibility = Visibility.Hidden;
                    break;
                case 2:
                    setPointPressure(lstRingPressure[0], lstPointPressure[0].X, lstPointPressure[0].Y);
                    setPointPressure(lstRingPressure[1], lstPointPressure[3].X, lstPointPressure[1].Y);
                    setLinePressure(lstLinePressure[0], lstPointPressure[0].X, lstPointPressure[0].Y, lstPointPressure[3].X, lstPointPressure[1].Y);

                    lstRingPressure[2].Visibility = Visibility.Hidden;
                    lstRingPressure[3].Visibility = Visibility.Hidden;
                    lstLinePressure[1].Visibility = Visibility.Hidden;
                    lstLinePressure[2].Visibility = Visibility.Hidden;

                    setPointPressure(lstRingPressure[4], lstPointPressure[4].X, lstPointPressure[4].Y);
                    setPointPressure(lstRingPressure[5], lstPointPressure[5].X, lstPointSpeed[5].Y);
                    setLinePressure(lstLinePressure[3], lstPointPressure[3].X, lstPointPressure[1].Y, lstPointPressure[4].X, lstPointPressure[4].Y);
                    setLinePressure(lstLinePressure[4], lstPointPressure[4].X, lstPointPressure[4].Y, lstPointPressure[5].X, lstPointPressure[5].Y);
                    break;
                case 3:
                    setPointPressure(lstRingPressure[0], lstPointPressure[0].X, lstPointPressure[0].Y);
                    setPointPressure(lstRingPressure[1], lstPointPressure[1].X, lstPointPressure[1].Y);
                    setLinePressure(lstLinePressure[0], lstPointPressure[0].X, lstPointPressure[0].Y, lstPointPressure[1].X, lstPointPressure[1].Y);

                    setPointPressure(lstRingPressure[2], lstPointPressure[2].X, lstPointPressure[2].Y);
                    setPointPressure(lstRingPressure[3], lstPointPressure[3].X, lstPointPressure[3].Y);
                    setLinePressure(lstLinePressure[1], lstPointPressure[1].X, lstPointPressure[1].Y, lstPointPressure[2].X, lstPointPressure[2].Y);
                    setLinePressure(lstLinePressure[2], lstPointPressure[2].X, lstPointPressure[2].Y, lstPointPressure[3].X, lstPointPressure[3].Y);

                    setPointPressure(lstRingPressure[4], lstPointPressure[4].X, lstPointPressure[4].Y);
                    setPointPressure(lstRingPressure[5], lstPointPressure[5].X, lstPointPressure[5].Y);
                    setLinePressure(lstLinePressure[3], lstPointPressure[3].X, lstPointPressure[3].Y, lstPointPressure[4].X, lstPointPressure[4].Y);
                    setLinePressure(lstLinePressure[4], lstPointPressure[4].X, lstPointPressure[4].Y, lstPointPressure[5].X, lstPointPressure[5].Y);
                    break;
                default:
                    break;
            }
        }

        private void handleInjPr_81(objUnit obj)
        {
            switch (obj.value)
            {
                case 0:
                    {
                        bdInjPr_81.Visibility = Visibility.Hidden;
                        btnInj085.Visibility = Visibility.Hidden;
                        btnInj086.Visibility = Visibility.Hidden;
                    }
                    break;
                case 1:
                    {
                        bdInjPr_81.Visibility = Visibility.Visible;
                        btnInj085.Visibility = Visibility.Visible;
                        btnInj086.Visibility = Visibility.Visible;

                    }
                    break;
            }
        }
        private void handleInjPr_82(objUnit obj)
        {
            refushSpeed();
            refushPressure();
            switch (obj.value)
            {
                case 1:
                    {
                        imgInjPr_82_1_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_82_2_1_injection.Visibility = Visibility.Hidden;

                        btnInj087.Visibility = Visibility.Hidden;
                        btnInj088.Visibility = Visibility.Hidden;

                        btnInj090.Visibility = Visibility.Hidden;
                        btnInj091.Visibility = Visibility.Hidden;

                        btnInj093.Visibility = Visibility.Hidden;
                        btnInj094.Visibility = Visibility.Hidden;

                        btnInj096.Visibility = Visibility.Hidden;
                        btnInj097.Visibility = Visibility.Hidden;

                    }
                    break;
                case 2:
                    {
                        imgInjPr_82_1_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_82_2_1_injection.Visibility = Visibility.Hidden;

                        btnInj087.Visibility = Visibility.Visible;
                        btnInj088.Visibility = Visibility.Hidden;

                        btnInj090.Visibility = Visibility.Visible;
                        btnInj091.Visibility = Visibility.Hidden;

                        btnInj093.Visibility = Visibility.Visible;
                        btnInj094.Visibility = Visibility.Hidden;

                        btnInj096.Visibility = Visibility.Visible;
                        btnInj097.Visibility = Visibility.Hidden;
                    }
                    break;
                case 3:
                    {
                        imgInjPr_82_1_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_82_2_1_injection.Visibility = Visibility.Visible;

                        btnInj087.Visibility = Visibility.Visible;
                        btnInj088.Visibility = Visibility.Visible;

                        btnInj090.Visibility = Visibility.Visible;
                        btnInj091.Visibility = Visibility.Visible;

                        btnInj093.Visibility = Visibility.Visible;
                        btnInj094.Visibility = Visibility.Visible;

                        btnInj096.Visibility = Visibility.Visible;
                        btnInj097.Visibility = Visibility.Visible;
                    }
                    break;
            }
            //refreshMapCushion();
        }
        private void handleInjPr_83(objUnit obj)
        {
            switch (obj.value)
            {
                case 0:
                    {
                        imgInjPr_83_1.Visibility = Visibility.Hidden;
                        btnInj099.Visibility = Visibility.Hidden;
                        btnInj100.Visibility = Visibility.Hidden;
                    }
                    break;
                case 1:
                    {
                        imgInjPr_83_1.Visibility = Visibility.Visible;
                        btnInj099.Visibility = Visibility.Visible;
                        btnInj100.Visibility = Visibility.Visible;
                    }
                    break;
            }
        }

        private void lbInjPr_81_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.InjPr[81].accessLevel) || _bIsMouseMove)
                return;
            if (NewMessageBox.Show() == true)
            {
                valmoWin.dv.InjPr[81].setValue((valmoWin.dv.InjPr[81].value == 0) ? 1 : 0);
            }
        }
        private void lbInjPr_82_2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.InjPr[82].accessLevel) || _bIsMouseMove)
                return;

            if (NewMessageBox.Show() == true)
            {
                valmoWin.dv.InjPr[82].setValue(valmoWin.dv.InjPr[82].value > 2 ? 2 : 3);
            }
        }

        private void lbInjPr_82_1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.InjPr[82].accessLevel) || _bIsMouseMove)
                return;

            if (NewMessageBox.Show() == true)
            {
                valmoWin.dv.InjPr[82].setValue(valmoWin.dv.InjPr[82].value > 1 ? 1 : 2);
            }
        }

        private void lbInjPr_83_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.InjPr[83].accessLevel) || _bIsMouseMove)
                return;
            if (NewMessageBox.Show() == true)
            {
                valmoWin.dv.InjPr[83].setValue(valmoWin.dv.InjPr[83].value == 1 ? 0 : 1);
            }
        }

        private void lbInjPr_110_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.InjPr[110].accessLevel) || _bIsMouseMove)
                return;
            if (valmoWin.dv.InjPr[110].value != 0)
            {
                valmoWin.dv.InjPr[110].setValue(0);
            }

        }

        private void lbInjPr_110_1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.InjPr[110].accessLevel) || _bIsMouseMove)
                return;
            if (valmoWin.dv.InjPr[110].value != 1)
            {
                valmoWin.dv.InjPr[110].setValue(1);
            }

        }

        private void lbInjPr_110_2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.InjPr[110].accessLevel) || _bIsMouseMove)
                return;
            if (valmoWin.dv.InjPr[110].value != 2)
            {
                valmoWin.dv.InjPr[110].setValue(2);
            }

        }

        /// <summary>
        /// 鼠标是否按下
        /// </summary>
        private bool _bIsMouseDown = false;
        /// <summary>
        /// 鼠标是否移动
        /// </summary>
        private bool _bIsMouseMove = false;
        /// <summary>
        /// 鼠标按下位置
        /// </summary>
        Point pMouseDown;
        private Point pMouseDown0;

        private void cvsMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _bIsMouseDown = true;
            pMouseDown0 = e.GetPosition(cvsMain);
            pMouseDown = e.GetPosition(cvsMain);
        }

        private void cvsMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _bIsMouseMove = false;
            _bIsMouseDown = false;
        }

        private void cvsMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (_bIsMouseDown == true)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point curMousePos = e.GetPosition(cvsMain);
                    if (Math.Abs(curMousePos.Y - pMouseDown0.Y) > 5)
                        _bIsMouseMove = true;

                    double dOld = Canvas.GetTop(sPanelMain);
                    double dNew = curMousePos.Y - pMouseDown.Y + dOld;

                    if (dNew <= -(sPanelMain.Height - (valmoWin.MainPanelHeight - 195)) - 20)
                        dNew = -(sPanelMain.Height - (valmoWin.MainPanelHeight - 195)) - 20;
                    if (dNew > 0)
                        dNew = 0;
                    Canvas.SetTop(sPanelMain, dNew);
                    pMouseDown = curMousePos;
                }
            }
        }

        private void cvsMain_MouseLeave(object sender, MouseEventArgs e)
        {
            _bIsMouseMove = false;
            _bIsMouseDown = false;
        }

        /// <summary>
        /// 重新布局
        /// </summary>
        private void reorder()
        {
            cvsTime.Height = _bIsTimeVisiable ? 200 : 50;
            cvsInjUnit.Height = _bIsInjUnitVisiable ? 200 : 50;

            sPanelMain.Height = cvsTime.Height + cvsInjUnit.Height + 770;
        }

        private bool _bIsTimeVisiable = true;
        private void cvsTimeHead_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!_bIsMouseMove)
            {
                if (_bIsTimeVisiable == true)
                {
                    _bIsTimeVisiable = false;
                    imgZipedTime.Visibility = Visibility.Visible;
                    imgUnzipedTime.Visibility = Visibility.Hidden;
                }
                else
                {
                    _bIsTimeVisiable = true;
                    imgZipedTime.Visibility = Visibility.Hidden;
                    imgUnzipedTime.Visibility = Visibility.Visible;
                }
            }
            reorder();
        }

        private bool _bIsInjUnitVisiable = true;
    }
}
