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
using System.Windows.Media.Animation;
using nsDataMgr;
using System.Diagnostics;  


namespace nsVicoClient.ctrls
{
    public partial class injectionNewPage : UserControl
    {
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
        private Point pCurMousePos;

        public injectionNewPage()
        {
            InitializeComponent();

            cvsSet.Height = 0;

            valmoWin.dv.InjPr[21].addHandle(handle_inj021);
            valmoWin.dv.InjPr[29].addHandle(handle_Inj029);
            valmoWin.dv.InjPr[36].addHandle(handleInjPr_36);
            valmoWin.dv.InjPr[37].addHandle(handleInjPr_037);
            valmoWin.dv.InjPr[38].addHandle(handleInjPr_038);
            valmoWin.dv.InjPr[39].addHandle(handleInjPr_039);
            valmoWin.dv.InjPr[40].addHandle(handleInjPr_040);
            valmoWin.dv.InjPr[41].addHandle(handleInjPr_041);
            valmoWin.dv.InjPr[42].addHandle(handleInjPr_042);
            valmoWin.dv.InjPr[43].addHandle(handleInjPr_043);
            valmoWin.dv.InjPr[44].addHandle(handleInjPr_044);
            valmoWin.dv.InjPr[46].addHandle(handleInjPr_046);
            valmoWin.dv.InjPr[48].addHandle(handleInjPr_48);
            valmoWin.dv.InjPr[49].addHandle(handle_Inj049);
            valmoWin.dv.InjPr[50].addHandle(handle_Inj050);
            valmoWin.dv.InjPr[51].addHandle(handle_Inj051);
            valmoWin.dv.InjPr[52].addHandle(handle_Inj052);
            valmoWin.dv.InjPr[53].addHandle(handle_Inj053);
            valmoWin.dv.InjPr[54].addHandle(handle_Inj054);
            valmoWin.dv.InjPr[56].addHandle(handle_Inj056);
            valmoWin.dv.InjPr[57].addHandle(handle_Inj057);
            valmoWin.dv.InjPr[58].addHandle(handle_Inj058);
            valmoWin.dv.InjPr[59].addHandle(handle_Inj059);
            valmoWin.dv.InjPr[60].addHandle(handle_Inj060);
            valmoWin.dv.InjPr[61].addHandle(handle_Inj061);
            valmoWin.dv.InjPr[62].addHandle(cvsInjParamHead_backgroundUpdate);
            valmoWin.dv.InjPr[67].addHandle(handle_Inj067);
            valmoWin.dv.InjPr[68].addHandle(handle_Inj068);
            valmoWin.dv.InjPr[69].addHandle(handle_Inj069);
            valmoWin.dv.InjPr[71].addHandle(handleInjPr_71);
            valmoWin.dv.InjPr[72].addHandle(handleInjPr_72);
            valmoWin.dv.InjPr[73].addHandle(handleInjPr_73);
            valmoWin.dv.InjPr[74].addHandle(handleInjPr_74);
            valmoWin.dv.InjPr[75].addHandle(handle_Inj075);
            valmoWin.dv.InjPr[76].addHandle(handle_Inj076);
            valmoWin.dv.InjPr[77].addHandle(handle_Inj077);
            valmoWin.dv.InjPr[78].addHandle(handle_Inj078);
            valmoWin.dv.InjPr[189].addHandle(handle_Inj189);
            valmoWin.dv.InjPr[199].addHandle(handle_Inj199);
            valmoWin.dv.InjPr[245].addHandle(handle_Inj245);
            valmoWin.dv.InjPr[246].addHandle(handle_Inj246);
            valmoWin.dv.InjPr[247].addHandle(handle_Inj247);
            valmoWin.dv.InjPr[248].addHandle(handle_Inj248);
            valmoWin.dv.PrdPr[251].addHandle(InjectionCurveSave);
            valmoWin.dv.PrdPr[252].addHandle(InjectionCurveRefush);
            valmoWin.dv.PrdPr[256].addHandle(HoldingCurveSave);
            valmoWin.dv.PrdPr[257].addHandle(HoldingCurveRefush);
            valmoWin.dv.InjPr[255].addHandle(handle_Inj255);
            valmoWin.dv.InjPr[256].addHandle(handle_Inj256);
            valmoWin.dv.InjPr[257].addHandle(handle_Inj257);
            valmoWin.dv.InjPr[258].addHandle(handle_Inj258);
            valmoWin.dv.InjPr[450].addHandle(handle_Inj450);
            valmoWin.dv.InjPr[451].addHandle(handle_Inj451);
            valmoWin.dv.InjPr[452].addHandle(handle_Inj452);
            valmoWin.dv.InjPr[453].addHandle(handle_Inj453);
            valmoWin.dv.InjPr[454].addHandle(handle_Inj454);
            valmoWin.dv.InjPr[455].addHandle(handle_Inj455);
            valmoWin.dv.InjPr[456].addHandle(handle_Inj456);
            valmoWin.dv.InjPr[457].addHandle(handle_Inj457);
            valmoWin.dv.InjPr[458].addHandle(handle_Inj458);
            valmoWin.dv.InjPr[459].addHandle(handle_Inj459);

            for (int i = 0; i < 8; i++)
            {
                Line l = new Line();
                l.Stroke = new SolidColorBrush(Color.FromArgb(0xFF, 0xE4, 0x00, 0xBB));
                l.StrokeThickness = 2;
                l.ClipToBounds = true;
                l.SnapsToDevicePixels = true;
                l.Visibility = Visibility.Hidden;

                cvsMap.Children.Add(l);
                lstLineHold.Add(l);
            }

            for (int i = 0; i < 9; i++)
            {
                Ring r = new Ring();
                r.Color = new SolidColorBrush(Color.FromArgb(0xFF, 0xE4, 0x00, 0xBB));
                r.Visibility = Visibility.Hidden;
                r.RenderTransform = new TranslateTransform(4, 4);

                cvsMap.Children.Add(r);
                lstRingHold.Add(r);
            }

            for (int i = 0; i < 21; i++)
            {
                Line l = new Line();
                l.Stroke = new SolidColorBrush(Color.FromArgb(0xFF, 0x33, 0x79, 0xE2));
                l.StrokeThickness = 2;
                l.ClipToBounds = true;
                l.SnapsToDevicePixels = true;
                l.Visibility = Visibility.Hidden;

                cvsMap.Children.Add(l);
                lstLInjSpd.Add(l);
            }

            for (int i = 0; i < 20; i++)
            {
                Line l = new Line();
                l.Stroke = new SolidColorBrush(Color.FromArgb(0xFF, 0xE4, 0x00, 0xBB));
                l.StrokeThickness = 2;
                l.ClipToBounds = true;
                l.SnapsToDevicePixels = true;
                l.Visibility = Visibility.Hidden;

                cvsMap.Children.Add(l);
                lstLInjPrs.Add(l);
            }

            for (int i = 0; i < 23; i++)
            {
                Ring r = new Ring();
                r.Color = new SolidColorBrush(Color.FromArgb(0xFF, 0x33, 0x79, 0xE2));
                r.Visibility = Visibility.Hidden;
                r.RenderTransform = new TranslateTransform(-5, 4);

                cvsMap.Children.Add(r);
                lstRInjSpd.Add(r);
            }
        }

        private int startInjLastValue = 0;
        private bool bIsSaveCurve = false;
        private void InjectionCurveSave(objUnit obj)
        {
            if (startInjLastValue == 0)
            {
                if (obj.value == 1)
                {
                    bIsSaveCurve = true;
                }
            }

            startInjLastValue = obj.value;
        }

        private int startHoldLastValue = 0;
        private bool bIsHoldCurveSave = false;
        private void HoldingCurveSave(objUnit obj)
        {
            if (startHoldLastValue == 0)
            {
                if (obj.value == 1)
                {
                    bIsHoldCurveSave = true;
                }
            }

            startHoldLastValue = obj.value;
        }

        private List<Point> historyData_Time_Speed = new List<Point>();
        private List<Point> historyData_Time_Pressure = new List<Point>();
        private List<Point> historyData_Time_Pos = new List<Point>();

        private void HoldingCurveRefush(objUnit obj)
        {
            int count = obj.value;

            if (AxisMaxSpeed > 0 & AxisMaxPos_Holding > 0 & AxisMaxPressure > 0)
            {
                int[] HoldingData = new int[count * 4];
                Lasal32.GetData(HoldingData, (uint)valmoWin.dv.PrdPr[258].valueNew, count * 16);

                List<Point> curveData_Time_Speed = new List<Point>();
                List<Point> curveData_Time_Pressure = new List<Point>();
                List<Point> curveData_Time_Pos = new List<Point>();

                for (int i = 0; i < count; i++)
                {
                    double time = objUnit.getDblValue(HoldingData[i * 4 + 3], UnitType.Tm_s) / AxisHoldingTime * 10000;
                    double speed = objUnit.getDblValue(Math.Abs(HoldingData[i * 4]), UnitType.Spd_mm) / AxisMaxSpeed * 10000;
                    double pressure = objUnit.getDblValue(HoldingData[i * 4 + 1], UnitType.Prs_Mpa) / AxisMaxPressure * 10000;
                    double pos = objUnit.getDblValue(HoldingData[i * 4 + 2], UnitType.Len_mm) / AxisMaxPos_Holding * 10000;

                    curveData_Time_Pos.Add(new Point(10000 - time, 10000 - pos));
                    curveData_Time_Pressure.Add(new Point(10000 - time, 10000 - pressure));
                    curveData_Time_Speed.Add(new Point(10000 - time, 10000 - speed));
                }

                if (!bIsHoldCurveSave)
                {
                    curve_Time_Speed_Current.refushCurve(curveData_Time_Speed);
                    curve_Time_Pressure_Current.refushCurve(curveData_Time_Pressure);
                    curve_Time_Pos_Current.refushCurve(curveData_Time_Pos);

                    historyData_Time_Speed.Clear();
                    historyData_Time_Pressure.Clear();
                    historyData_Time_Pos.Clear();

                    for (int i = 0; i < count; i++)
                    {
                        historyData_Time_Speed.Add(curveData_Time_Speed[i]);
                        historyData_Time_Pressure.Add(curveData_Time_Pressure[i]);
                        historyData_Time_Pos.Add(curveData_Time_Pos[i]);
                    }
                }
                else
                {
                    curve_Time_Speed.NewHistroyCurve(historyData_Time_Speed);
                    curve_Time_Pressure.NewHistroyCurve(historyData_Time_Pressure);
                    curve_Time_Pos.NewHistroyCurve(historyData_Time_Pos);

                    bIsHoldCurveSave = false;
                }
            }
        }

        private List<Point> historyData_Pos_Speed = new List<Point>();
        private List<Point> historyData_Pos_Pressure = new List<Point>();
        private List<Point> historyData_Pos_Current = new List<Point>();

        private void InjectionCurveRefush(objUnit obj)
        {
            int count = obj.value;

            if (AxisMaxSpeed > 0 & AxisMaxPos_Injection > 0 & AxisMaxPressure > 0)
            {
                int[] InjectionData = new int[count * 4];
                Lasal32.GetData(InjectionData, (uint)valmoWin.dv.PrdPr[253].valueNew, count * 16);

                List<Point> curveData_Pos_Speed = new List<Point>();
                List<Point> curveData_Pos_Pressure = new List<Point>();
                List<Point> curveData_Pos_Current = new List<Point>();

                for (int i = 0; i < count; i++)
                {
                    double pos = objUnit.getDblValue(InjectionData[i * 4 + 3], UnitType.Len_mm) / AxisMaxPos_Injection * 10000;
                    double current = objUnit.getDblValue(InjectionData[i * 4 + 2], UnitType.Per) / 100 * 10000;
                    double speed = objUnit.getDblValue(Math.Abs(InjectionData[i * 4]), UnitType.Spd_mm) / AxisMaxSpeed * 10000;
                    double pressure = objUnit.getDblValue(InjectionData[i * 4 + 1], UnitType.Prs_Mpa) / AxisMaxPressure * 10000;

                    curveData_Pos_Speed.Add(new Point(pos, 10000 - speed));
                    curveData_Pos_Pressure.Add(new Point(pos, 10000 - pressure));
                    curveData_Pos_Current.Add(new Point(pos, 5000 - current / 2));
                }

                if (!bIsSaveCurve)
                {
                    curve_Pos_Speed_Current.refushCurve(curveData_Pos_Speed);
                    curve_Pos_Pressure_Current.refushCurve(curveData_Pos_Pressure);
                    curve_Pos_Current_Current.refushCurve(curveData_Pos_Current);

                    historyData_Pos_Speed.Clear();
                    historyData_Pos_Pressure.Clear();
                    historyData_Pos_Current.Clear();

                    for (int i = 0; i < count; i++)
                    {
                        historyData_Pos_Pressure.Add(curveData_Pos_Pressure[i]);
                        historyData_Pos_Current.Add(curveData_Pos_Current[i]);
                        historyData_Pos_Speed.Add(curveData_Pos_Speed[i]);
                    }
                }
                else
                {
                    curve_Pos_Speed.NewHistroyCurve(historyData_Pos_Speed);
                    curve_Pos_Pressure.NewHistroyCurve(historyData_Pos_Pressure);
                    curve_Pos_Current.NewHistroyCurve(historyData_Pos_Current);

                    bIsSaveCurve = false;
                }
            }
        }

        /// <summary>
        /// 切换点容积
        /// </summary>
        private void refushVPosV()
        {
            lbVP.Content = ((Injection_PosVP - Injection_PosVP) * Math.PI * Math.Pow(valmoWin.dv.InjPr[207].vDbl / 2, 2) / 1000).ToString("0.00");
        }
        /// <summary>
        /// VP设定
        /// </summary>
        private void updateVPCursor()
        {
            if (AxisMaxPos_Injection > 0)
            {
                Canvas.SetLeft(cvsVP, Injection_PosVP / AxisMaxPos_Injection * 400);
            }
        }

        /// <summary>
        /// 刷新注射部是否使用
        /// </summary>
        /// <param name="obj">Inj062</param>
        private void cvsInjParamHead_backgroundUpdate(objUnit obj)
        {
            cvsInjParamHead.Background = (obj.value == 0) ?
                 new SolidColorBrush(Color.FromArgb(0xFF, 0xE1, 0xF8, 0xF1)) :
                 new SolidColorBrush(Color.FromArgb(0xFF, 0xFC, 0xE6, 0xE6));
        }

        private void handle_inj021(objUnit obj)
        {
            if (AxisMaxPos_Injection > 0)
            {
                Canvas.SetLeft(cvsCursor, obj.vDbl / AxisMaxPos_Injection * 400);
                lbCurScrewPos.Content = obj.vDblStr;
            }
        }
        private void handle_Inj189(objUnit obj)
        {
            setPressureStaff(obj.vDbl);
        }
        private void handle_Inj199(objUnit obj)
        {
            setSpeedStaff(obj.vDbl);
        }

        #region 设定坐标系

        private double AxisMaxPressure = 0;
        private void setPressureStaff(double pMax)
        {
            AxisMaxPressure = ((int)(pMax * 1.1 / 100) + 1) * 100;

            lbPres1.Content = (AxisMaxPressure * 0.1).ToString("0.00");
            lbPres2.Content = (AxisMaxPressure * 0.2).ToString("0.00");
            lbPres3.Content = (AxisMaxPressure * 0.3).ToString("0.00");
            lbPres4.Content = (AxisMaxPressure * 0.4).ToString("0.00");
            lbPres5.Content = (AxisMaxPressure * 0.5).ToString("0.00");
            lbPres6.Content = (AxisMaxPressure * 0.6).ToString("0.00");
            lbPres7.Content = (AxisMaxPressure * 0.7).ToString("0.00");
            lbPres8.Content = (AxisMaxPressure * 0.8).ToString("0.00");
            lbPres9.Content = (AxisMaxPressure * 0.9).ToString("0.00");
            lbPres10.Content = (AxisMaxPressure * 1.0).ToString("0.00");

            refushHoldPoint();
            updateInjectionPrsPoints();

            curve_Pos_Pressure.ClearHistroyCurves();
            curve_Time_Pressure.ClearHistroyCurves();
        }

        private double AxisMaxSpeed = 0;
        private void setSpeedStaff(double vMax)
        {
            AxisMaxSpeed = ((int)(vMax * 1.1 / 100) + 1) * 100;

            lbSpeed1.Content = (AxisMaxSpeed * 0.1).ToString("0.00");
            lbSpeed2.Content = (AxisMaxSpeed * 0.2).ToString("0.00");
            lbSpeed3.Content = (AxisMaxSpeed * 0.3).ToString("0.00");
            lbSpeed4.Content = (AxisMaxSpeed * 0.4).ToString("0.00");
            lbSpeed5.Content = (AxisMaxSpeed * 0.5).ToString("0.00");
            lbSpeed6.Content = (AxisMaxSpeed * 0.6).ToString("0.00");
            lbSpeed7.Content = (AxisMaxSpeed * 0.7).ToString("0.00");
            lbSpeed8.Content = (AxisMaxSpeed * 0.8).ToString("0.00");
            lbSpeed9.Content = (AxisMaxSpeed * 0.9).ToString("0.00");
            lbSpeed10.Content = (AxisMaxSpeed * 1.0).ToString("0.00");

            updateInjectionSpdPoints();

            curve_Time_Speed.ClearHistroyCurves();
            curve_Pos_Speed.ClearHistroyCurves();
        }

        private double AxisMaxPos_Injection = 0;
        private void setInjectionPosStaff(double InjectionStartPos)
        {
            AxisMaxPos_Injection = ((int)(InjectionStartPos * 1.1 / 10) + 1) * 10;

            lbPosV3.Content = (AxisMaxPos_Injection * 1.0).ToString("0.00");
            lbPosV2.Content = (AxisMaxPos_Injection * 0.75).ToString("0.00");
            lbPosV1.Content = (AxisMaxPos_Injection * 0.5).ToString("0.00");
            lbPosV0.Content = (AxisMaxPos_Injection * 0.25).ToString("0.00");

            updateInjectionSpdPoints();
            updateInjectionPrsPoints();

            curve_Pos_Speed.ClearHistroyCurves();
            curve_Pos_Current.ClearHistroyCurves();
            curve_Pos_Pressure.ClearHistroyCurves();
        }

        private double AxisMaxPos_Holding = 0;
        private void setHoldingPosStaff(double vpPosition)
        {
            AxisMaxPos_Holding = ((int)(vpPosition * 1.1 / 5) + 1) * 5;

            lbPosP1.Content = (AxisMaxPos_Holding * 0.1).ToString("0.00");
            lbPosP2.Content = (AxisMaxPos_Holding * 0.2).ToString("0.00");
            lbPosP3.Content = (AxisMaxPos_Holding * 0.3).ToString("0.00");
            lbPosP4.Content = (AxisMaxPos_Holding * 0.4).ToString("0.00");
            lbPosP5.Content = (AxisMaxPos_Holding * 0.5).ToString("0.00");
            lbPosP6.Content = (AxisMaxPos_Holding * 0.6).ToString("0.00");
            lbPosP7.Content = (AxisMaxPos_Holding * 0.7).ToString("0.00");
            lbPosP8.Content = (AxisMaxPos_Holding * 0.8).ToString("0.00");
            lbPosP9.Content = (AxisMaxPos_Holding * 0.9).ToString("0.00");
            lbPosP10.Content = (AxisMaxPos_Holding).ToString("0.00");

            curve_Time_Pos.ClearHistroyCurves();
        }

        private double AxisHoldingTime = 0.0;
        private void setHoldingTimeStaff(double holdingTime)
        {
            int temp = (int)(holdingTime * 1.1 * 10000 / 5);
            string strTemp = temp.ToString();

            int c1 = Convert.ToInt32(strTemp.Substring(0, 1));

            int c2;
            if (strTemp.Length > 2)
            {
                c2 = Convert.ToInt32(strTemp.Substring(1, 1));
            }
            else
            {
                c2 = 0;
            }

            if (c1 != 1)
            {
                AxisHoldingTime = (c1 + 1) * Math.Pow(10, strTemp.Length - 1) * 5 / 10000;
            }
            else
            {
                AxisHoldingTime = (c1 * Math.Pow(10, strTemp.Length - 1) + c2 * Math.Pow(10, strTemp.Length - 2)) * 5 / 10000;
            }

            lbTime0.Content = (AxisHoldingTime * 0.2).ToString("0.00");
            lbTime1.Content = (AxisHoldingTime * 0.4).ToString("0.00");
            lbTime2.Content = (AxisHoldingTime * 0.6).ToString("0.00");
            lbTime3.Content = (AxisHoldingTime * 0.8).ToString("0.00");
            lbTime4.Content = (AxisHoldingTime * 1.0).ToString("0.00");

            refushHoldPoint();

            curve_Time_Pressure.ClearHistroyCurves();
            curve_Time_Speed.ClearHistroyCurves();
            curve_Time_Pos.ClearHistroyCurves();
        }

        #endregion

        #region 保压设定曲线

        private Point[] lstPointHold = new Point[9];
        private List<Ring> lstRingHold = new List<Ring>();
        private List<Line> lstLineHold = new List<Line>();

        private int holding_Segment = 1;
        private void handleInjPr_36(objUnit obj)
        {
            holding_Segment = obj.value;

            switch (holding_Segment)
            {
                case 1:
                    {
                        imgInjPr_36_4_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_36_3_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_36_2_1_injection.Visibility = Visibility.Hidden;

                        btnInj040.Visibility = Visibility.Hidden;
                        btnInj039.Visibility = Visibility.Hidden;
                        btnInj038.Visibility = Visibility.Hidden;
                        btnInj044.Visibility = Visibility.Hidden;
                        btnInj043.Visibility = Visibility.Hidden;
                        btnInj042.Visibility = Visibility.Hidden;
                    }
                    break;
                case 2:
                    {
                        imgInjPr_36_4_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_36_3_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_36_2_1_injection.Visibility = Visibility.Visible;

                        btnInj040.Visibility = Visibility.Hidden;
                        btnInj039.Visibility = Visibility.Hidden;
                        btnInj038.Visibility = Visibility.Visible;

                        btnInj044.Visibility = Visibility.Hidden;
                        btnInj043.Visibility = Visibility.Hidden;
                        btnInj042.Visibility = Visibility.Visible;
                    }
                    break;
                case 3:
                    {
                        imgInjPr_36_4_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_36_3_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_36_2_1_injection.Visibility = Visibility.Visible;

                        btnInj040.Visibility = Visibility.Hidden;
                        btnInj039.Visibility = Visibility.Visible;
                        btnInj038.Visibility = Visibility.Visible;

                        btnInj044.Visibility = Visibility.Hidden;
                        btnInj043.Visibility = Visibility.Visible;
                        btnInj042.Visibility = Visibility.Visible;
                    }
                    break;
                case 4:
                    {
                        imgInjPr_36_4_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_36_3_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_36_2_1_injection.Visibility = Visibility.Visible;

                        btnInj040.Visibility = Visibility.Visible;
                        btnInj039.Visibility = Visibility.Visible;
                        btnInj038.Visibility = Visibility.Visible;

                        btnInj044.Visibility = Visibility.Visible;
                        btnInj043.Visibility = Visibility.Visible;
                        btnInj042.Visibility = Visibility.Visible;
                    }
                    break;
            }

            refushHoldingTime();
        }

        private double holding_T1 = 0.0;
        private double holding_T2 = 0.0;
        private double holding_T3 = 0.0;
        private double holding_T4 = 0.0;
        private double holding_TSet = 0.0;

        private void handleInjPr_041(objUnit obj)
        {
            holding_T1 = obj.vDbl;

            refushHoldingTime();
        }
        private void handleInjPr_042(objUnit obj)
        {
            holding_T2 = obj.vDbl;

            refushHoldingTime();
        }
        private void handleInjPr_043(objUnit obj)
        {
            holding_T3 = obj.vDbl;

            refushHoldingTime();
        }
        private void handleInjPr_044(objUnit obj)
        {
            holding_T4 = obj.vDbl;

            refushHoldingTime();
        }
        private void refushHoldingTime()
        {
            switch (holding_Segment)
            {
                case 1:
                    holding_TSet = holding_T1;
                    break;
                case 2:
                    holding_TSet = holding_T1 + holding_T2;
                    break;
                case 3:
                    holding_TSet = holding_T1 + holding_T2 + holding_T3;
                    break;
                case 4:
                    holding_TSet = holding_T1 + holding_T2 + holding_T3 + holding_T4;
                    break;
            }

            setHoldingTimeStaff(holding_TSet);
        }

        private double holding_P1 = 0.0;
        private double holding_P2 = 0.0;
        private double holding_P3 = 0.0;
        private double holding_P4 = 0.0;

        private void handleInjPr_037(objUnit obj)
        {
            holding_P1 = obj.vDbl;

            refushHoldPoint();
        }
        private void handleInjPr_038(objUnit obj)
        {
            holding_P2 = obj.vDbl;

            refushHoldPoint();
        }
        private void handleInjPr_039(objUnit obj)
        {
            holding_P3 = obj.vDbl;

            refushHoldPoint();
        }
        private void handleInjPr_040(objUnit obj)
        {
            holding_P4 = obj.vDbl;

            refushHoldPoint();
        }

        private void refushHoldPoint()
        {
            if (AxisMaxPressure > 0 & AxisHoldingTime > 0)
            {
                lstPointHold[0].X = 0;
                lstPointHold[0].Y = Convert.ToInt32(holding_P1 / AxisMaxPressure * 300);
                lstPointHold[1].X = Convert.ToInt32(holding_T1 / AxisHoldingTime * 400);
                lstPointHold[1].Y = Convert.ToInt32(holding_P1 / AxisMaxPressure * 300);
                lstPointHold[2].X = Convert.ToInt32(holding_T1 / AxisHoldingTime * 400);
                lstPointHold[2].Y = Convert.ToInt32(holding_P2 / AxisMaxPressure * 300);
                lstPointHold[3].X = Convert.ToInt32((holding_T1 + holding_T2) / AxisHoldingTime * 400);
                lstPointHold[3].Y = Convert.ToInt32(holding_P2 / AxisMaxPressure * 300);
                lstPointHold[4].X = Convert.ToInt32((holding_T1 + holding_T2) / AxisHoldingTime * 400);
                lstPointHold[4].Y = Convert.ToInt32(holding_P3 / AxisMaxPressure * 300);
                lstPointHold[5].X = Convert.ToInt32((holding_T1 + holding_T2 + holding_T3) / AxisHoldingTime * 400);
                lstPointHold[5].Y = Convert.ToInt32(holding_P3 / AxisMaxPressure * 300);
                lstPointHold[6].X = Convert.ToInt32((holding_T1 + holding_T2 + holding_T3) / AxisHoldingTime * 400);
                lstPointHold[6].Y = Convert.ToInt32(holding_P4 / AxisMaxPressure * 300);
                lstPointHold[7].X = Convert.ToInt32((holding_T1 + holding_T2 + holding_T3 + holding_T4) / AxisHoldingTime * 400);
                lstPointHold[7].Y = Convert.ToInt32(holding_P4 / AxisMaxPressure * 300);
                lstPointHold[8].X = Convert.ToInt32((holding_T1 + holding_T2 + holding_T3 + holding_T4) / AxisHoldingTime * 400);
                lstPointHold[8].Y = 0;

                refushHoldingMap();
            }
        }
        private void refushHoldingMap()
        {
            int i = 0;
            for (; i < holding_Segment * 2; i++)
            {
                lstRingHold[i].Visibility = Visibility.Visible;

                Canvas.SetRight(lstRingHold[i], lstPointHold[i].X);
                Canvas.SetBottom(lstRingHold[i], lstPointHold[i].Y);
            }

            for (int j = i; j < 8; j++)
            {
                lstRingHold[j].Visibility = Visibility.Hidden;
            }

            lstRingHold[8].Visibility = Visibility.Visible;
            Canvas.SetRight(lstRingHold[8], lstPointHold[holding_Segment * 2 - 1].X);
            Canvas.SetBottom(lstRingHold[8], 0);

            int k = 0;
            for (; k < holding_Segment * 2 - 1; k++)
            {
                lstLineHold[k].Visibility = Visibility.Visible;
                if (lstPointHold[k + 1].X >= lstPointHold[k].X)
                {
                    Canvas.SetRight(lstLineHold[k], lstPointHold[k].X);
                }
                else
                {
                    Canvas.SetRight(lstLineHold[k], lstPointHold[k + 1].X);
                }
                lstLineHold[k].X2 = Math.Abs(lstPointHold[k + 1].X - lstPointHold[k].X);

                if (lstPointHold[k + 1].Y <= lstPointHold[k].Y)
                {
                    Canvas.SetBottom(lstLineHold[k], lstPointHold[k + 1].Y);
                }
                else
                {
                    Canvas.SetBottom(lstLineHold[k], lstPointHold[k].Y);
                }
                lstLineHold[k].Y2 = Math.Abs(lstPointHold[k + 1].Y - lstPointHold[k].Y);
            }

            for (int m = k; m < 7; m++)
            {
                lstLineHold[m].Visibility = Visibility.Hidden;
            }

            lstLineHold[7].Visibility = Visibility.Visible;
            Canvas.SetRight(lstLineHold[7], lstPointHold[holding_Segment * 2 - 1].X);
            Canvas.SetBottom(lstLineHold[7], 0);
            lstLineHold[7].X2 = 0;
            lstLineHold[7].Y2 = lstPointHold[holding_Segment * 2 - 1].Y;
        }

        #endregion

        #region 注射曲线设定

        private Point[] PointsPos_Spd = new Point[20];
        private Point[] PointsPos_Prs = new Point[20];

        private List<Ring> lstRInjSpd = new List<Ring>();
        private List<Line> lstLInjSpd = new List<Line>();

        private List<Ring> lstRInjPrs = new List<Ring>();
        private List<Line> lstLInjPrs = new List<Line>();

        private int InjectionSegment = 3;
        private void handleInjPr_48(objUnit obj)
        {
            InjectionSegment = obj.value;

            switch (InjectionSegment)
            {
                case 3:
                    {
                        imgInjPr_48_3_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_48_4_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_48_5_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_48_6_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_48_7_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_48_8_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_48_9_1_injection.Visibility = Visibility.Hidden;

                        InjectionPos4.Visibility = Visibility.Hidden;
                        InjectionPos5.Visibility = Visibility.Hidden;
                        InjectionPos6.Visibility = Visibility.Hidden;
                        InjectionPos7.Visibility = Visibility.Hidden;
                        InjectionPos8.Visibility = Visibility.Hidden;
                        InjectionPos9.Visibility = Visibility.Hidden;
                        InjectionPos10.Visibility = Visibility.Hidden;

                        InjectionSpd4.Visibility = Visibility.Hidden;
                        InjectionSpd5.Visibility = Visibility.Hidden;
                        InjectionSpd6.Visibility = Visibility.Hidden;
                        InjectionSpd7.Visibility = Visibility.Hidden;
                        InjectionSpd8.Visibility = Visibility.Hidden;
                        InjectionSpd9.Visibility = Visibility.Hidden;
                        InjectionSpd10.Visibility = Visibility.Hidden;

                        InjectionPrs4.Visibility = Visibility.Hidden;
                        InjectionPrs5.Visibility = Visibility.Hidden;
                        InjectionPrs6.Visibility = Visibility.Hidden;
                        InjectionPrs7.Visibility = Visibility.Hidden;
                        InjectionPrs8.Visibility = Visibility.Hidden;
                        InjectionPrs9.Visibility = Visibility.Hidden;
                        InjectionPrs10.Visibility = Visibility.Hidden;
                    }
                    break;
                case 4:
                    {
                        imgInjPr_48_3_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_48_4_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_48_5_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_48_6_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_48_7_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_48_8_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_48_9_1_injection.Visibility = Visibility.Hidden;

                        InjectionPos4.Visibility = Visibility.Visible;
                        InjectionPos5.Visibility = Visibility.Hidden;
                        InjectionPos6.Visibility = Visibility.Hidden;
                        InjectionPos7.Visibility = Visibility.Hidden;
                        InjectionPos8.Visibility = Visibility.Hidden;
                        InjectionPos9.Visibility = Visibility.Hidden;
                        InjectionPos10.Visibility = Visibility.Hidden;

                        InjectionSpd4.Visibility = Visibility.Visible;
                        InjectionSpd5.Visibility = Visibility.Hidden;
                        InjectionSpd6.Visibility = Visibility.Hidden;
                        InjectionSpd7.Visibility = Visibility.Hidden;
                        InjectionSpd8.Visibility = Visibility.Hidden;
                        InjectionSpd9.Visibility = Visibility.Hidden;
                        InjectionSpd10.Visibility = Visibility.Hidden;

                        InjectionPrs4.Visibility = Visibility.Visible;
                        InjectionPrs5.Visibility = Visibility.Hidden;
                        InjectionPrs6.Visibility = Visibility.Hidden;
                        InjectionPrs7.Visibility = Visibility.Hidden;
                        InjectionPrs8.Visibility = Visibility.Hidden;
                        InjectionPrs9.Visibility = Visibility.Hidden;
                        InjectionPrs10.Visibility = Visibility.Hidden;
                    }
                    break;
                case 5:
                    {
                        imgInjPr_48_3_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_48_4_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_48_5_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_48_6_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_48_7_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_48_8_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_48_9_1_injection.Visibility = Visibility.Hidden;

                        InjectionPos4.Visibility = Visibility.Visible;
                        InjectionPos5.Visibility = Visibility.Visible;
                        InjectionPos6.Visibility = Visibility.Hidden;
                        InjectionPos7.Visibility = Visibility.Hidden;
                        InjectionPos8.Visibility = Visibility.Hidden;
                        InjectionPos9.Visibility = Visibility.Hidden;
                        InjectionPos10.Visibility = Visibility.Hidden;

                        InjectionSpd4.Visibility = Visibility.Visible;
                        InjectionSpd5.Visibility = Visibility.Visible;
                        InjectionSpd6.Visibility = Visibility.Hidden;
                        InjectionSpd7.Visibility = Visibility.Hidden;
                        InjectionSpd8.Visibility = Visibility.Hidden;
                        InjectionSpd9.Visibility = Visibility.Hidden;
                        InjectionSpd10.Visibility = Visibility.Hidden;

                        InjectionPrs4.Visibility = Visibility.Visible;
                        InjectionPrs5.Visibility = Visibility.Visible;
                        InjectionPrs6.Visibility = Visibility.Hidden;
                        InjectionPrs7.Visibility = Visibility.Hidden;
                        InjectionPrs8.Visibility = Visibility.Hidden;
                        InjectionPrs9.Visibility = Visibility.Hidden;
                        InjectionPrs10.Visibility = Visibility.Hidden;
                    }
                    break;
                case 6:
                    {
                        imgInjPr_48_3_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_48_4_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_48_5_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_48_6_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_48_7_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_48_8_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_48_9_1_injection.Visibility = Visibility.Hidden;

                        InjectionPos4.Visibility = Visibility.Visible;
                        InjectionPos5.Visibility = Visibility.Visible;
                        InjectionPos6.Visibility = Visibility.Visible;
                        InjectionPos7.Visibility = Visibility.Hidden;
                        InjectionPos8.Visibility = Visibility.Hidden;
                        InjectionPos9.Visibility = Visibility.Hidden;
                        InjectionPos10.Visibility = Visibility.Hidden;

                        InjectionSpd4.Visibility = Visibility.Visible;
                        InjectionSpd5.Visibility = Visibility.Visible;
                        InjectionSpd6.Visibility = Visibility.Visible;
                        InjectionSpd7.Visibility = Visibility.Hidden;
                        InjectionSpd8.Visibility = Visibility.Hidden;
                        InjectionSpd9.Visibility = Visibility.Hidden;
                        InjectionSpd10.Visibility = Visibility.Hidden;

                        InjectionPrs4.Visibility = Visibility.Visible;
                        InjectionPrs5.Visibility = Visibility.Visible;
                        InjectionPrs6.Visibility = Visibility.Visible;
                        InjectionPrs7.Visibility = Visibility.Hidden;
                        InjectionPrs8.Visibility = Visibility.Hidden;
                        InjectionPrs9.Visibility = Visibility.Hidden;
                        InjectionPrs10.Visibility = Visibility.Hidden;
                    }
                    break;
                case 7:
                    {
                        imgInjPr_48_3_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_48_4_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_48_5_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_48_6_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_48_7_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_48_8_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_48_9_1_injection.Visibility = Visibility.Hidden;

                        InjectionPos4.Visibility = Visibility.Visible;
                        InjectionPos5.Visibility = Visibility.Visible;
                        InjectionPos6.Visibility = Visibility.Visible;
                        InjectionPos7.Visibility = Visibility.Visible;
                        InjectionPos8.Visibility = Visibility.Hidden;
                        InjectionPos9.Visibility = Visibility.Hidden;
                        InjectionPos10.Visibility = Visibility.Hidden;

                        InjectionSpd4.Visibility = Visibility.Visible;
                        InjectionSpd5.Visibility = Visibility.Visible;
                        InjectionSpd6.Visibility = Visibility.Visible;
                        InjectionSpd7.Visibility = Visibility.Visible;
                        InjectionSpd8.Visibility = Visibility.Hidden;
                        InjectionSpd9.Visibility = Visibility.Hidden;
                        InjectionSpd10.Visibility = Visibility.Hidden;

                        InjectionPrs4.Visibility = Visibility.Visible;
                        InjectionPrs5.Visibility = Visibility.Visible;
                        InjectionPrs6.Visibility = Visibility.Visible;
                        InjectionPrs7.Visibility = Visibility.Visible;
                        InjectionPrs8.Visibility = Visibility.Hidden;
                        InjectionPrs9.Visibility = Visibility.Hidden;
                        InjectionPrs10.Visibility = Visibility.Hidden;
                    }
                    break;
                case 8:
                    {
                        imgInjPr_48_3_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_48_4_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_48_5_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_48_6_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_48_7_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_48_8_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_48_9_1_injection.Visibility = Visibility.Hidden;

                        InjectionPos4.Visibility = Visibility.Visible;
                        InjectionPos5.Visibility = Visibility.Visible;
                        InjectionPos6.Visibility = Visibility.Visible;
                        InjectionPos7.Visibility = Visibility.Visible;
                        InjectionPos8.Visibility = Visibility.Visible;
                        InjectionPos9.Visibility = Visibility.Hidden;
                        InjectionPos10.Visibility = Visibility.Hidden;

                        InjectionSpd4.Visibility = Visibility.Visible;
                        InjectionSpd5.Visibility = Visibility.Visible;
                        InjectionSpd6.Visibility = Visibility.Visible;
                        InjectionSpd7.Visibility = Visibility.Visible;
                        InjectionSpd8.Visibility = Visibility.Visible;
                        InjectionSpd9.Visibility = Visibility.Hidden;
                        InjectionSpd10.Visibility = Visibility.Hidden;

                        InjectionPrs4.Visibility = Visibility.Visible;
                        InjectionPrs5.Visibility = Visibility.Visible;
                        InjectionPrs6.Visibility = Visibility.Visible;
                        InjectionPrs7.Visibility = Visibility.Visible;
                        InjectionPrs8.Visibility = Visibility.Visible;
                        InjectionPrs9.Visibility = Visibility.Hidden;
                        InjectionPrs10.Visibility = Visibility.Hidden;
                    }
                    break;
                case 9:
                    {
                        imgInjPr_48_5_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_48_4_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_48_3_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_48_6_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_48_7_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_48_8_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_48_9_1_injection.Visibility = Visibility.Hidden;

                        InjectionPos4.Visibility = Visibility.Visible;
                        InjectionPos5.Visibility = Visibility.Visible;
                        InjectionPos6.Visibility = Visibility.Visible;
                        InjectionPos7.Visibility = Visibility.Visible;
                        InjectionPos8.Visibility = Visibility.Visible;
                        InjectionPos9.Visibility = Visibility.Visible;
                        InjectionPos10.Visibility = Visibility.Hidden;

                        InjectionSpd4.Visibility = Visibility.Visible;
                        InjectionSpd5.Visibility = Visibility.Visible;
                        InjectionSpd6.Visibility = Visibility.Visible;
                        InjectionSpd7.Visibility = Visibility.Visible;
                        InjectionSpd8.Visibility = Visibility.Visible;
                        InjectionSpd9.Visibility = Visibility.Visible;
                        InjectionSpd10.Visibility = Visibility.Hidden;

                        InjectionPrs4.Visibility = Visibility.Visible;
                        InjectionPrs5.Visibility = Visibility.Visible;
                        InjectionPrs6.Visibility = Visibility.Visible;
                        InjectionPrs7.Visibility = Visibility.Visible;
                        InjectionPrs8.Visibility = Visibility.Visible;
                        InjectionPrs9.Visibility = Visibility.Visible;
                        InjectionPrs10.Visibility = Visibility.Hidden;
                    }
                    break;
                case 10:
                    {
                        imgInjPr_48_5_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_48_4_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_48_3_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_48_6_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_48_7_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_48_8_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_48_9_1_injection.Visibility = Visibility.Visible;

                        InjectionPos4.Visibility = Visibility.Visible;
                        InjectionPos5.Visibility = Visibility.Visible;
                        InjectionPos6.Visibility = Visibility.Visible;
                        InjectionPos7.Visibility = Visibility.Visible;
                        InjectionPos8.Visibility = Visibility.Visible;
                        InjectionPos9.Visibility = Visibility.Visible;
                        InjectionPos10.Visibility = Visibility.Visible;

                        InjectionSpd4.Visibility = Visibility.Visible;
                        InjectionSpd5.Visibility = Visibility.Visible;
                        InjectionSpd6.Visibility = Visibility.Visible;
                        InjectionSpd7.Visibility = Visibility.Visible;
                        InjectionSpd8.Visibility = Visibility.Visible;
                        InjectionSpd9.Visibility = Visibility.Visible;
                        InjectionSpd10.Visibility = Visibility.Visible;

                        InjectionPrs4.Visibility = Visibility.Visible;
                        InjectionPrs5.Visibility = Visibility.Visible;
                        InjectionPrs6.Visibility = Visibility.Visible;
                        InjectionPrs7.Visibility = Visibility.Visible;
                        InjectionPrs8.Visibility = Visibility.Visible;
                        InjectionPrs9.Visibility = Visibility.Visible;
                        InjectionPrs10.Visibility = Visibility.Visible;
                    }
                    break;
            }

            updateInjectionSpdPoints();
            updateInjectionPrsPoints();
        }

        private double Injection_Spd1 = 0;
        private double Injection_Spd2 = 0;
        private double Injection_Spd3 = 0;
        private double Injection_Spd4 = 0;
        private double Injection_Spd5 = 0;
        private double Injection_Spd6 = 0;
        private double Injection_Spd7 = 0;
        private double Injection_Spd8 = 0;
        private double Injection_Spd9 = 0;
        private double Injection_Spd10 = 0;
        private double Injection_SpdVP = 0;

        private void handle_Inj056(objUnit obj)
        {
            Injection_Spd1 = obj.vDbl;

            updateInjectionSpdPoints();
        }
        private void handle_Inj057(objUnit obj)
        {
            Injection_Spd2 = obj.vDbl;

            updateInjectionSpdPoints();
        }
        private void handle_Inj058(objUnit obj)
        {
            Injection_Spd3 = obj.vDbl;

            updateInjectionSpdPoints();
        }
        private void handle_Inj059(objUnit obj)
        {
            Injection_Spd4 = obj.vDbl;

            updateInjectionSpdPoints();
        }
        private void handle_Inj060(objUnit obj)
        {
            Injection_Spd5 = obj.vDbl;

            updateInjectionSpdPoints();
        }
        private void handle_Inj255(objUnit obj)
        {
            Injection_Spd6 = obj.vDbl;

            updateInjectionSpdPoints();
        }
        private void handle_Inj256(objUnit obj)
        {
            Injection_Spd7 = obj.vDbl;

            updateInjectionSpdPoints();
        }
        private void handle_Inj257(objUnit obj)
        {
            Injection_Spd8 = obj.vDbl;

            updateInjectionSpdPoints();
        }
        private void handle_Inj258(objUnit obj)
        {
            Injection_Spd9 = obj.vDbl;

            updateInjectionSpdPoints();
        }
        private void handle_Inj061(objUnit obj)
        {
            Injection_Spd10 = obj.vDbl;

            updateInjectionSpdPoints();
        }
        private void handle_Inj069(objUnit obj)
        {
            Injection_SpdVP = obj.vDbl;

            updateInjectionSpdPoints();
        }

        private double Injection_Pos1 = 0;
        private double Injection_Pos2 = 0;
        private double Injection_Pos3 = 0;
        private double Injection_Pos4 = 0;
        private double Injection_Pos5 = 0;
        private double Injection_Pos6 = 0;
        private double Injection_Pos7 = 0;
        private double Injection_Pos8 = 0;
        private double Injection_Pos9 = 0;
        private double Injection_Pos10 = 0;
        private double Injection_PosVP = 0;

        private void handle_Inj049(objUnit obj)
        {
            setInjectionPosStaff(obj.vDbl);

            Injection_Pos1 = obj.vDbl;

            updateInjectionSpdPoints();
            updateInjectionPrsPoints();

            refushVPosV();
        }
        private void handle_Inj050(objUnit obj)
        {
            Injection_Pos2 = obj.vDbl;

            updateInjectionSpdPoints();
            updateInjectionPrsPoints();
        }
        private void handle_Inj051(objUnit obj)
        {
            Injection_Pos3 = obj.vDbl;

            updateInjectionSpdPoints();
            updateInjectionPrsPoints();
        }
        private void handle_Inj052(objUnit obj)
        {
            Injection_Pos4 = obj.vDbl;

            updateInjectionSpdPoints();
            updateInjectionPrsPoints();
        }
        private void handle_Inj053(objUnit obj)
        {
            Injection_Pos5 = obj.vDbl;

            updateInjectionSpdPoints();
            updateInjectionPrsPoints();
        }
        private void handle_Inj054(objUnit obj)
        {
            Injection_Pos6 = obj.vDbl;

            updateInjectionSpdPoints();
            updateInjectionPrsPoints();
        }
        private void handle_Inj245(objUnit obj)
        {
            Injection_Pos7 = obj.vDbl;

            updateInjectionSpdPoints();
            updateInjectionPrsPoints();
        }
        private void handle_Inj246(objUnit obj)
        {
            Injection_Pos8 = obj.vDbl;

            updateInjectionSpdPoints();
            updateInjectionPrsPoints();
        }
        private void handle_Inj247(objUnit obj)
        {
            Injection_Pos9 = obj.vDbl;

            updateInjectionSpdPoints();
            updateInjectionPrsPoints();
        }
        private void handle_Inj248(objUnit obj)
        {
            Injection_Pos10 = obj.vDbl;

            updateInjectionSpdPoints();
            updateInjectionPrsPoints();
        }
        private void handle_Inj068(objUnit obj)
        {
            Injection_PosVP = obj.vDbl;

            setHoldingPosStaff(Injection_PosVP);

            updateInjectionSpdPoints();

            refushVPosV();
        }

        private double Injection_Prs1 = 0;
        private double Injection_Prs2 = 0;
        private double Injection_Prs3 = 0;
        private double Injection_Prs4 = 0;
        private double Injection_Prs5 = 0;
        private double Injection_Prs6 = 0;
        private double Injection_Prs7 = 0;
        private double Injection_Prs8 = 0;
        private double Injection_Prs9 = 0;
        private double Injection_Prs10 = 0;

        private void handle_Inj450(objUnit obj)
        {
            Injection_Prs1 = obj.vDbl;

            updateInjectionPrsPoints();
        }
        private void handle_Inj451(objUnit obj)
        {
            Injection_Prs2 = obj.vDbl;

            updateInjectionPrsPoints();
        }
        private void handle_Inj452(objUnit obj)
        {
            Injection_Prs3 = obj.vDbl;

            updateInjectionPrsPoints();
        }
        private void handle_Inj453(objUnit obj)
        {
            Injection_Prs4 = obj.vDbl;

            updateInjectionPrsPoints();
        }
        private void handle_Inj454(objUnit obj)
        {
            Injection_Prs5 = obj.vDbl;

            updateInjectionPrsPoints();
        }
        private void handle_Inj455(objUnit obj)
        {
            Injection_Prs6 = obj.vDbl;

            updateInjectionPrsPoints();
        }
        private void handle_Inj456(objUnit obj)
        {
            Injection_Prs7 = obj.vDbl;

            updateInjectionPrsPoints();
        }
        private void handle_Inj457(objUnit obj)
        {
            Injection_Prs8 = obj.vDbl;

            updateInjectionPrsPoints();
        }
        private void handle_Inj458(objUnit obj)
        {
            Injection_Prs9 = obj.vDbl;

            updateInjectionPrsPoints();
        }
        private void handle_Inj459(objUnit obj)
        {
            Injection_Prs10 = obj.vDbl;

            updateInjectionPrsPoints();
        }

        private void updateInjectionSpdPoints()
        {
            if (AxisMaxPos_Injection > 0 & AxisMaxSpeed > 0)
            {
                PointsPos_Spd[0].X = Convert.ToInt32(Injection_Pos1 / AxisMaxPos_Injection * 400);
                PointsPos_Spd[0].Y = 0;
                PointsPos_Spd[1].X = Convert.ToInt32(Injection_Pos1 / AxisMaxPos_Injection * 400);
                PointsPos_Spd[1].Y = Convert.ToInt32(Injection_Spd1 / AxisMaxSpeed * 300);
                PointsPos_Spd[2].X = Convert.ToInt32(Injection_Pos2 / AxisMaxPos_Injection * 400);
                PointsPos_Spd[2].Y = Convert.ToInt32(Injection_Spd1 / AxisMaxSpeed * 300);
                PointsPos_Spd[3].X = Convert.ToInt32(Injection_Pos2 / AxisMaxPos_Injection * 400);
                PointsPos_Spd[3].Y = Convert.ToInt32(Injection_Spd2 / AxisMaxSpeed * 300);
                PointsPos_Spd[4].X = Convert.ToInt32(Injection_Pos3 / AxisMaxPos_Injection * 400);
                PointsPos_Spd[4].Y = Convert.ToInt32(Injection_Spd2 / AxisMaxSpeed * 300);
                PointsPos_Spd[5].X = Convert.ToInt32(Injection_Pos3 / AxisMaxPos_Injection * 400);
                PointsPos_Spd[5].Y = Convert.ToInt32(Injection_Spd3 / AxisMaxSpeed * 300);
                PointsPos_Spd[6].X = Convert.ToInt32(Injection_Pos4 / AxisMaxPos_Injection * 400);
                PointsPos_Spd[6].Y = Convert.ToInt32(Injection_Spd3 / AxisMaxSpeed * 300);
                PointsPos_Spd[7].X = Convert.ToInt32(Injection_Pos4 / AxisMaxPos_Injection * 400);
                PointsPos_Spd[7].Y = Convert.ToInt32(Injection_Spd4 / AxisMaxSpeed * 300);
                PointsPos_Spd[8].X = Convert.ToInt32(Injection_Pos5 / AxisMaxPos_Injection * 400);
                PointsPos_Spd[8].Y = Convert.ToInt32(Injection_Spd4 / AxisMaxSpeed * 300);
                PointsPos_Spd[9].X = Convert.ToInt32(Injection_Pos5 / AxisMaxPos_Injection * 400);
                PointsPos_Spd[9].Y = Convert.ToInt32(Injection_Spd5 / AxisMaxSpeed * 300);
                PointsPos_Spd[10].X = Convert.ToInt32(Injection_Pos6 / AxisMaxPos_Injection * 400);
                PointsPos_Spd[10].Y = Convert.ToInt32(Injection_Spd5 / AxisMaxSpeed * 300);
                PointsPos_Spd[11].X = Convert.ToInt32(Injection_Pos6 / AxisMaxPos_Injection * 400);
                PointsPos_Spd[11].Y = Convert.ToInt32(Injection_Spd6 / AxisMaxSpeed * 300);
                PointsPos_Spd[12].X = Convert.ToInt32(Injection_Pos7 / AxisMaxPos_Injection * 400);
                PointsPos_Spd[12].Y = Convert.ToInt32(Injection_Spd6 / AxisMaxSpeed * 300);
                PointsPos_Spd[13].X = Convert.ToInt32(Injection_Pos7 / AxisMaxPos_Injection * 400);
                PointsPos_Spd[13].Y = Convert.ToInt32(Injection_Spd7 / AxisMaxSpeed * 300);
                PointsPos_Spd[14].X = Convert.ToInt32(Injection_Pos8 / AxisMaxPos_Injection * 400);
                PointsPos_Spd[14].Y = Convert.ToInt32(Injection_Spd7 / AxisMaxSpeed * 300);
                PointsPos_Spd[15].X = Convert.ToInt32(Injection_Pos8 / AxisMaxPos_Injection * 400);
                PointsPos_Spd[15].Y = Convert.ToInt32(Injection_Spd8 / AxisMaxSpeed * 300);
                PointsPos_Spd[16].X = Convert.ToInt32(Injection_Pos9 / AxisMaxPos_Injection * 400);
                PointsPos_Spd[16].Y = Convert.ToInt32(Injection_Spd8 / AxisMaxSpeed * 300);
                PointsPos_Spd[17].X = Convert.ToInt32(Injection_Pos9 / AxisMaxPos_Injection * 400);
                PointsPos_Spd[17].Y = Convert.ToInt32(Injection_Spd9 / AxisMaxSpeed * 300);
                PointsPos_Spd[18].X = Convert.ToInt32(Injection_Pos10 / AxisMaxPos_Injection * 400);
                PointsPos_Spd[18].Y = Convert.ToInt32(Injection_Spd9 / AxisMaxSpeed * 300);
                PointsPos_Spd[19].X = Convert.ToInt32(Injection_Pos10 / AxisMaxPos_Injection * 400);
                PointsPos_Spd[19].Y = Convert.ToInt32(Injection_Spd10 / AxisMaxSpeed * 300);

                updateInjectionSpdSetMap();
            }
        }
        private void updateInjectionSpdSetMap()
        {
            //速度曲线
            int i = 0;
            for (; i < InjectionSegment * 2; i++)
            {
                lstRInjSpd[i].Visibility = Visibility.Visible;
                Canvas.SetLeft(lstRInjSpd[i], PointsPos_Spd[i].X);
                Canvas.SetBottom(lstRInjSpd[i], PointsPos_Spd[i].Y);
            }

            for (int n = i; n < 20; n++)
            {
                lstRInjSpd[n].Visibility = Visibility.Hidden;
            }

            if (IsVPPosOn == true)
            {
                lstRInjSpd[20].Visibility = Visibility.Visible;
                Canvas.SetLeft(lstRInjSpd[20], Injection_PosVP / AxisMaxPos_Injection * 400);
                Canvas.SetBottom(lstRInjSpd[20], PointsPos_Spd[InjectionSegment * 2 - 1].Y);

                lstRInjSpd[21].Visibility = Visibility.Visible;
                Canvas.SetLeft(lstRInjSpd[21], Injection_PosVP / AxisMaxPos_Injection * 400);
                Canvas.SetBottom(lstRInjSpd[21], Injection_SpdVP / AxisMaxSpeed * 400);

                lstRInjSpd[22].Visibility = Visibility.Visible;
                Canvas.SetLeft(lstRInjSpd[22], 0);
                Canvas.SetBottom(lstRInjSpd[22], Injection_SpdVP / AxisMaxSpeed * 400);
            }
            else
            {
                lstRInjSpd[20].Visibility = Visibility.Visible;
                Canvas.SetLeft(lstRInjSpd[20], 0);
                Canvas.SetBottom(lstRInjSpd[20], PointsPos_Spd[InjectionSegment * 2 - 1].Y);

                lstRInjSpd[21].Visibility = Visibility.Hidden;
                lstRInjSpd[22].Visibility = Visibility.Hidden;
            }

            int j = 0;
            for (; j < InjectionSegment * 2 - 1; j++)
            {
                lstLInjSpd[j].Visibility = Visibility.Visible;
                if (PointsPos_Spd[j + 1].X >= PointsPos_Spd[j].X)
                {
                    Canvas.SetLeft(lstLInjSpd[j], PointsPos_Spd[j].X);
                }
                else
                {
                    Canvas.SetLeft(lstLInjSpd[j], PointsPos_Spd[j + 1].X);
                }
                lstLInjSpd[j].X2 = Math.Abs(PointsPos_Spd[j + 1].X - PointsPos_Spd[j].X);

                if (PointsPos_Spd[j + 1].Y <= PointsPos_Spd[j].Y)
                {
                    Canvas.SetBottom(lstLInjSpd[j], PointsPos_Spd[j + 1].Y);
                }
                else
                {
                    Canvas.SetBottom(lstLInjSpd[j], PointsPos_Spd[j].Y);
                }
                lstLInjSpd[j].Y2 = Math.Abs(PointsPos_Spd[j + 1].Y - PointsPos_Spd[j].Y);
            }

            for (int n = j; n < 19; n++)
            {
                lstLInjSpd[n].Visibility = Visibility.Hidden;
            }

            if (IsVPPosOn == true)
            {
                lstLInjSpd[19].Visibility = Visibility.Visible;
                Canvas.SetLeft(lstLInjSpd[19], Injection_PosVP / AxisMaxPos_Injection * 400);
                Canvas.SetBottom(lstLInjSpd[19], PointsPos_Spd[InjectionSegment * 2 - 1].Y);
                lstLInjSpd[19].X2 = Math.Abs(PointsPos_Spd[InjectionSegment * 2 - 1].X - Injection_PosVP / AxisMaxPos_Injection * 400);
                lstLInjSpd[19].Y2 = 0;

                lstLInjSpd[20].Visibility = Visibility.Visible;
                Canvas.SetLeft(lstLInjSpd[20], 0);
                Canvas.SetBottom(lstLInjSpd[20], Injection_SpdVP / AxisMaxSpeed * 400);
                lstLInjSpd[20].X2 = Injection_PosVP / AxisMaxPos_Injection * 400;
                lstLInjSpd[20].Y2 = 0;

                updateVPCursor();
            }
            else
            {
                lstLInjSpd[19].Visibility = Visibility.Visible;
                Canvas.SetLeft(lstLInjSpd[19], 0);
                Canvas.SetBottom(lstLInjSpd[19], PointsPos_Spd[InjectionSegment * 2 - 1].Y);
                lstLInjSpd[19].X2 = Math.Abs(PointsPos_Spd[InjectionSegment * 2 - 1].X);
                lstLInjSpd[19].Y2 = 0;

                lstLInjSpd[20].Visibility = Visibility.Hidden;
            }
        }

        private void updateInjectionPrsPoints()
        {
            if (AxisMaxPos_Injection > 0 & AxisMaxPressure > 0)
            {
                PointsPos_Prs[0].X = Convert.ToInt32(Injection_Pos1 / AxisMaxPos_Injection * 400);
                PointsPos_Prs[0].Y = 0;
                PointsPos_Prs[1].X = Convert.ToInt32(Injection_Pos1 / AxisMaxPos_Injection * 400);
                PointsPos_Prs[1].Y = Convert.ToInt32(Injection_Prs1 / AxisMaxPressure * 300);
                PointsPos_Prs[2].X = Convert.ToInt32(Injection_Pos2 / AxisMaxPos_Injection * 400);
                PointsPos_Prs[2].Y = Convert.ToInt32(Injection_Prs1 / AxisMaxPressure * 300);
                PointsPos_Prs[3].X = Convert.ToInt32(Injection_Pos2 / AxisMaxPos_Injection * 400);
                PointsPos_Prs[3].Y = Convert.ToInt32(Injection_Prs2 / AxisMaxPressure * 300);
                PointsPos_Prs[4].X = Convert.ToInt32(Injection_Pos3 / AxisMaxPos_Injection * 400);
                PointsPos_Prs[4].Y = Convert.ToInt32(Injection_Prs2 / AxisMaxPressure * 300);
                PointsPos_Prs[5].X = Convert.ToInt32(Injection_Pos3 / AxisMaxPos_Injection * 400);
                PointsPos_Prs[5].Y = Convert.ToInt32(Injection_Prs3 / AxisMaxPressure * 300);
                PointsPos_Prs[6].X = Convert.ToInt32(Injection_Pos4 / AxisMaxPos_Injection * 400);
                PointsPos_Prs[6].Y = Convert.ToInt32(Injection_Prs3 / AxisMaxPressure * 300);
                PointsPos_Prs[7].X = Convert.ToInt32(Injection_Pos4 / AxisMaxPos_Injection * 400);
                PointsPos_Prs[7].Y = Convert.ToInt32(Injection_Prs4 / AxisMaxPressure * 300);
                PointsPos_Prs[8].X = Convert.ToInt32(Injection_Pos5 / AxisMaxPos_Injection * 400);
                PointsPos_Prs[8].Y = Convert.ToInt32(Injection_Prs4 / AxisMaxPressure * 300);
                PointsPos_Prs[9].X = Convert.ToInt32(Injection_Pos5 / AxisMaxPos_Injection * 400);
                PointsPos_Prs[9].Y = Convert.ToInt32(Injection_Prs5 / AxisMaxPressure * 300);
                PointsPos_Prs[10].X = Convert.ToInt32(Injection_Pos6 / AxisMaxPos_Injection * 400);
                PointsPos_Prs[10].Y = Convert.ToInt32(Injection_Prs5 / AxisMaxPressure * 300);
                PointsPos_Prs[11].X = Convert.ToInt32(Injection_Pos6 / AxisMaxPos_Injection * 400);
                PointsPos_Prs[11].Y = Convert.ToInt32(Injection_Prs6 / AxisMaxPressure * 300);
                PointsPos_Prs[12].X = Convert.ToInt32(Injection_Pos7 / AxisMaxPos_Injection * 400);
                PointsPos_Prs[12].Y = Convert.ToInt32(Injection_Prs6 / AxisMaxPressure * 300);
                PointsPos_Prs[13].X = Convert.ToInt32(Injection_Pos7 / AxisMaxPos_Injection * 400);
                PointsPos_Prs[13].Y = Convert.ToInt32(Injection_Prs7 / AxisMaxPressure * 300);
                PointsPos_Prs[14].X = Convert.ToInt32(Injection_Pos8 / AxisMaxPos_Injection * 400);
                PointsPos_Prs[14].Y = Convert.ToInt32(Injection_Prs7 / AxisMaxPressure * 300);
                PointsPos_Prs[15].X = Convert.ToInt32(Injection_Pos8 / AxisMaxPos_Injection * 400);
                PointsPos_Prs[15].Y = Convert.ToInt32(Injection_Prs8 / AxisMaxPressure * 300);
                PointsPos_Prs[16].X = Convert.ToInt32(Injection_Pos9 / AxisMaxPos_Injection * 400);
                PointsPos_Prs[16].Y = Convert.ToInt32(Injection_Prs8 / AxisMaxPressure * 300);
                PointsPos_Prs[17].X = Convert.ToInt32(Injection_Pos9 / AxisMaxPos_Injection * 400);
                PointsPos_Prs[17].Y = Convert.ToInt32(Injection_Prs9 / AxisMaxPressure * 300);
                PointsPos_Prs[18].X = Convert.ToInt32(Injection_Pos10 / AxisMaxPos_Injection * 400);
                PointsPos_Prs[18].Y = Convert.ToInt32(Injection_Prs9 / AxisMaxPressure * 300);
                PointsPos_Prs[19].X = Convert.ToInt32(Injection_Pos10 / AxisMaxPos_Injection * 400);
                PointsPos_Prs[19].Y = Convert.ToInt32(Injection_Prs10 / AxisMaxPressure * 300);

                updateInjectionPrsSetMap();
            }
        }
        private void updateInjectionPrsSetMap()
        {
            //int i = 0;
            //for (; i < InjectionSegment * 2; i++)
            //{
            //    lstRInjSpd[i].Visibility = Visibility.Visible;
            //    Canvas.SetLeft(lstRInjSpd[i], PointsPos_Spd[i].X);
            //    Canvas.SetBottom(lstRInjSpd[i], PointsPos_Spd[i].Y);
            //}

            //for (int n = i; n < 20; n++)
            //{
            //    lstRInjSpd[n].Visibility = Visibility.Hidden;
            //}

            //if (IsVPPosOn == true)
            //{
            //    lstRInjSpd[20].Visibility = Visibility.Visible;
            //    Canvas.SetLeft(lstRInjSpd[20], Injection_PosVP / AxisMaxPos_Injection * 400);
            //    Canvas.SetBottom(lstRInjSpd[20], PointsPos_Spd[InjectionSegment * 2 - 1].Y);

            //    lstRInjSpd[21].Visibility = Visibility.Visible;
            //    Canvas.SetLeft(lstRInjSpd[21], Injection_PosVP / AxisMaxPos_Injection * 400);
            //    Canvas.SetBottom(lstRInjSpd[21], Injection_SpdVP / AxisMaxSpeed * 400);

            //    lstRInjSpd[22].Visibility = Visibility.Visible;
            //    Canvas.SetLeft(lstRInjSpd[22], 0);
            //    Canvas.SetBottom(lstRInjSpd[22], Injection_SpdVP / AxisMaxSpeed * 400);
            //}
            //else
            //{
            //    lstRInjSpd[20].Visibility = Visibility.Visible;
            //    Canvas.SetLeft(lstRInjSpd[20], 0);
            //    Canvas.SetBottom(lstRInjSpd[20], PointsPos_Spd[InjectionSegment * 2 - 1].Y);

            //    lstRInjSpd[21].Visibility = Visibility.Hidden;
            //    lstRInjSpd[22].Visibility = Visibility.Hidden;
            //}

            if (btnMS.Visibility == Visibility.Visible)
            {
                int j = 0;
                for (; j < InjectionSegment * 2 - 1; j++)
                {
                    lstLInjPrs[j].Visibility = Visibility.Visible;
                    if (PointsPos_Prs[j + 1].X >= PointsPos_Prs[j].X)
                    {
                        Canvas.SetLeft(lstLInjPrs[j], PointsPos_Prs[j].X);
                    }
                    else
                    {
                        Canvas.SetLeft(lstLInjPrs[j], PointsPos_Prs[j + 1].X);
                    }
                    lstLInjPrs[j].X2 = Math.Abs(PointsPos_Prs[j + 1].X - PointsPos_Prs[j].X);

                    if (PointsPos_Prs[j + 1].Y <= PointsPos_Prs[j].Y)
                    {
                        Canvas.SetBottom(lstLInjPrs[j], PointsPos_Prs[j + 1].Y);
                    }
                    else
                    {
                        Canvas.SetBottom(lstLInjPrs[j], PointsPos_Prs[j].Y);
                    }
                    lstLInjPrs[j].Y2 = Math.Abs(PointsPos_Prs[j + 1].Y - PointsPos_Prs[j].Y);
                }

                for (int n = j; n < 19; n++)
                {
                    lstLInjPrs[n].Visibility = Visibility.Hidden;
                }

                lstLInjPrs[19].Visibility = Visibility.Visible;
                Canvas.SetLeft(lstLInjPrs[19], 0);
                Canvas.SetBottom(lstLInjPrs[19], PointsPos_Prs[InjectionSegment * 2 - 1].Y);
                lstLInjPrs[19].X2 = Math.Abs(PointsPos_Prs[InjectionSegment * 2 - 1].X);
                lstLInjPrs[19].Y2 = 0;
            }
            else
            {
                for (int i = 0; i < 20; i++)
                {
                    lstLInjPrs[i].Visibility = Visibility.Hidden; ;
                }

                lstLInjPrs[0].Visibility = Visibility.Visible;
                Canvas.SetLeft(lstLInjPrs[0], 0);
                Canvas.SetBottom(lstLInjPrs[0], Convert.ToInt32(valmoWin.dv.InjPr[46].vDbl / AxisMaxPressure * 300));
                lstLInjPrs[0].X2 = 400;
                lstLInjPrs[0].Y2 = 0;
            }
        }

        #endregion

        private bool IsVPPosOn = false;

        private void handle_Inj029(objUnit obj)
        {
            double temp = obj.vDbl;

            if (VPTime > 0)
            {
                lVPTime.X2 = temp / VPTime * 400;
            }
        }

        double VPTime = 0;
        private void handle_Inj067(objUnit obj)
        {
             VPTime = obj.vDbl;

             lbVPTimeS2.Content = (VPTime * 0.25).ToString("0.00");
             lbVPTimeS3.Content = (VPTime * 0.5).ToString("0.00");
             lbVPTimeS4.Content = (VPTime * 0.75).ToString("0.00");
             lbVPTimeS5.Content = (VPTime * 1.0).ToString("0.00");
        }

        double CoolTime = 0;
        private void handle_Inj075(objUnit obj)
        {
            double temp = obj.vDbl;

            if (CoolTime > 0)
            {
                lCool.X2 = temp / CoolTime * 400;
            }
        }
        private void handle_Inj076(objUnit obj)
        {
            CoolTime = obj.vDbl;

            lbCoolS2.Content = (CoolTime * 0.25).ToString("0.00");
            lbCoolS3.Content = (CoolTime * 0.5).ToString("0.00");
            lbCoolS4.Content = (CoolTime * 0.75).ToString("0.00");
            lbCoolS5.Content = (CoolTime * 1.0).ToString("0.00");
        }

        double InjectionDelay = 0;
        private void handle_Inj077(objUnit obj)
        {
            double temp = obj.vDbl;

            if (InjectionDelay > 0)
            {
                lInjectionDelay.X2 = temp / InjectionDelay * 400;
            }
        }
        private void handle_Inj078(objUnit obj)
        {
            InjectionDelay = obj.vDbl;

            lbInjectionDelayS2.Content = (InjectionDelay * 0.25).ToString("0.00");
            lbInjectionDelayS3.Content = (InjectionDelay * 0.50).ToString("0.00");
            lbInjectionDelayS4.Content = (InjectionDelay * 0.75).ToString("0.00");
            lbInjectionDelayS5.Content = (InjectionDelay * 1.00).ToString("0.00");
        }

        private void handleInjPr_72(objUnit obj)
        {
            if (obj.value == 1)
            {
                IsVPPosOn = true;

                bPos.Visibility = Visibility.Visible;
                cvsVP.Visibility = Visibility.Visible;
            }
            else
            {
                IsVPPosOn = false;

                bPos.Visibility = Visibility.Hidden;
                cvsVP.Visibility = Visibility.Hidden;
            }

            updateInjectionSpdPoints();
        }
        private void handleInjPr_71(objUnit obj)
        {
            bTime.Visibility = obj.value == 1 ?
                Visibility.Visible : Visibility.Hidden;
        }
        private void handleInjPr_73(objUnit obj)
        {
            bSpeed.Visibility = obj.value == 1 ?
                Visibility.Visible : Visibility.Hidden;
        }
        private void handleInjPr_74(objUnit obj)
        {
            bPressure.Visibility = obj.value == 1 ?
                Visibility.Visible : Visibility.Hidden;
        }

        private void lbInjPr_36_2_injection_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.InjPr[36].accessLevel) || _bIsMouseMove)
                return;
            valmoWin.dv.InjPr[36].setValue((valmoWin.dv.InjPr[36].value > 1) ? 1 : 2);

        }
        private void lbInjPr_36_3_injection_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.InjPr[36].accessLevel) || _bIsMouseMove)
                return;
            valmoWin.dv.InjPr[36].setValue((valmoWin.dv.InjPr[36].valueNew > 2) ? 2 : 3);

        }
        private void lbInjPr_36_4_injection_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.InjPr[36].accessLevel) || _bIsMouseMove)
                return;
            valmoWin.dv.InjPr[36].setValue((valmoWin.dv.InjPr[36].valueNew > 3) ? 3 : 4);

        }
        private void lbInjPr_48_3_injection_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.InjPr[48].accessLevel) || _bIsMouseMove)
                return;
            valmoWin.dv.InjPr[48].setValue((valmoWin.dv.InjPr[48].valueNew > 3) ? 3 : 4);
        }
        private void lbInjPr_48_4_injection_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.InjPr[48].accessLevel) || _bIsMouseMove)
                return;
            valmoWin.dv.InjPr[48].setValue((valmoWin.dv.InjPr[48].valueNew > 4) ? 4 : 5);
        }
        private void lbInjPr_48_5_injection_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.InjPr[48].accessLevel) || _bIsMouseMove)
                return;
            valmoWin.dv.InjPr[48].setValue((valmoWin.dv.InjPr[48].valueNew > 5) ? 5 : 6);
        }
        private void lbInjPr_48_6_injection_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.InjPr[48].accessLevel) || _bIsMouseMove)
                return;
            valmoWin.dv.InjPr[48].setValue((valmoWin.dv.InjPr[48].valueNew > 6) ? 6 : 7);
        }
        private void lbInjPr_48_7_injection_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.InjPr[48].accessLevel) || _bIsMouseMove)
                return;
            valmoWin.dv.InjPr[48].setValue((valmoWin.dv.InjPr[48].valueNew > 7) ? 7 : 8);
        }
        private void lbInjPr_48_8_injection_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.InjPr[48].accessLevel) || _bIsMouseMove)
                return;
            valmoWin.dv.InjPr[48].setValue((valmoWin.dv.InjPr[48].valueNew > 8) ? 8 : 9);
        }
        private void lbInjPr_48_9_injection_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.InjPr[48].accessLevel) || _bIsMouseMove)
                return;
            valmoWin.dv.InjPr[48].setValue((valmoWin.dv.InjPr[48].valueNew > 9) ? 9 : 10);
        }

        private void lbPos_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.InjPr[72].accessLevel) || _bIsMouseMove)
                return;
            valmoWin.dv.InjPr[72].setValue((valmoWin.dv.InjPr[72].valueNew == 1) ? 0 : 1);
        }
        private void lbTm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.InjPr[71].accessLevel) || _bIsMouseMove)
                return;
            valmoWin.dv.InjPr[71].setValue((valmoWin.dv.InjPr[71].valueNew == 1) ? 0 : 1);
        }
        private void lbSpd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.InjPr[73].accessLevel) || _bIsMouseMove)
                return;
            valmoWin.dv.InjPr[73].setValue((valmoWin.dv.InjPr[73].valueNew == 1) ? 0 : 1);
        }
        private void lbPrs_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.InjPr[74].accessLevel) || _bIsMouseMove)
                return;
            valmoWin.dv.InjPr[74].setValue((valmoWin.dv.InjPr[74].valueNew == 1) ? 0 : 1);
        }

        private Point mouseDownPos;

        private void cvsmain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _bIsMouseDown = true;
            mouseDownPos = e.GetPosition(cvsMain);
            pCurMousePos = e.GetPosition(cvsMain);
        }

        private void cvsmain_MouseMove(object sender, MouseEventArgs e)
        {
            if (_bIsMouseDown == true)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point curMousePos = e.GetPosition(cvsMain);
                    if (Math.Abs(curMousePos.Y - mouseDownPos.Y) > 5)
                        _bIsMouseMove = true;

                    double dOld = Canvas.GetTop(sPanelMain);
                    double dNew = curMousePos.Y - pCurMousePos.Y + dOld;

                    if (dNew <= -(sPanelMain.Height - (valmoWin.MainPanelHeight - 73) + 20))
                        dNew = -(sPanelMain.Height - (valmoWin.MainPanelHeight - 73) + 20);
                    if (dNew > 0)
                        dNew = 0;
                    Canvas.SetTop(sPanelMain, dNew);
                    pCurMousePos = curMousePos;
                }
            }
        }

        private void cvsmain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _bIsMouseMove = false;
            _bIsMouseDown = false;
        }

        private void cvsmain_MouseLeave(object sender, MouseEventArgs e)
        {
            _bIsMouseDown = false;
        }

        /// <summary>
        /// 重新布局
        /// </summary>
        private void reorder()
        {
            cvsInjParam.Height = _bIsInjParamVisiable ? 150 : 50;
            cvsInjMonitor.Height = _bIsInjMonitorVisiable ? 500 : 50;
            cvsInjActionSet.Height = _bIsInjActionSetVisiable ? 160 : 50;

            sPanelMain.Height = cvsInjParam.Height + cvsInjMonitor.Height + cvsInjActionSet.Height + 1080;
        }

        private bool _bIsInjParamVisiable = true;
        private void cvsInjParamHead_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!_bIsMouseMove)
            {
                if (_bIsInjParamVisiable == true)
                {
                    _bIsInjParamVisiable = false;
                    imgZipedInjParam.Visibility = Visibility.Visible;
                    imgUnzipedInjParam.Visibility = Visibility.Hidden;
                }
                else
                {
                    _bIsInjParamVisiable = true;
                    imgZipedInjParam.Visibility = Visibility.Hidden;
                    imgUnzipedInjParam.Visibility = Visibility.Visible;
                }
            }
            reorder();
        }
        private bool _bIsInjMonitorVisiable = true;
        private void cvsInjMonitorHead_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!_bIsMouseMove)
            {
                if (_bIsInjMonitorVisiable == true)
                {
                    _bIsInjMonitorVisiable = false;
                    imgZipedInjMonitor.Visibility = Visibility.Visible;
                    imgUnzipedInjMonitor.Visibility = Visibility.Hidden;
                }
                else
                {
                    _bIsInjMonitorVisiable = true;
                    imgZipedInjMonitor.Visibility = Visibility.Hidden;
                    imgUnzipedInjMonitor.Visibility = Visibility.Visible;
                }
            }
            reorder();
        }

        private bool _bIsInjActionSetVisiable = true;
        private void cvsInjActionSetHead_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!_bIsMouseMove)
            {
                if (_bIsInjActionSetVisiable == true)
                {
                    _bIsInjActionSetVisiable = false;
                    imgZipedInjActionSet.Visibility = Visibility.Visible;
                    imgUnzipedInjActionSet.Visibility = Visibility.Hidden;
                }
                else
                {
                    _bIsInjActionSetVisiable = true;
                    imgZipedInjActionSet.Visibility = Visibility.Hidden;
                    imgUnzipedInjActionSet.Visibility = Visibility.Visible;
                }
            }
            reorder();
        }

        private void HistoryCount_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!_bIsMouseMove)
            {
                HistoryCount.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0xB4, 0xE1));

                valmoWin.SNumInput.init(20, 1, "HistoryCount", HistoryCount.Content.ToString(), "", 1, CancelHandle, setHistoryCount);
            }
        }

        private void setHistoryCount(double count)
        {
            CurveCanves.MaxHistoryVisualsCount = Convert.ToInt32(count);

            HistoryCount.Content = count;
            HistoryCount.BorderBrush = new SolidColorBrush(Color.FromArgb(0xff, 0xd4, 0xd4, 0xd4));
        }

        private void CancelHandle()
        {
            HistoryCount.BorderBrush = new SolidColorBrush(Color.FromArgb(0xff, 0xd4, 0xd4, 0xd4));
        }

        private void btnClear_MouseDown(object sender, MouseButtonEventArgs e)
        {
            btnClear.Background = Brushes.Gray;
        }

        private void btnClear_MouseUp(object sender, MouseButtonEventArgs e)
        {
            btnClear.Background = Brushes.Transparent;

            if (!_bIsMouseMove)
            {
                curve_Pos_Speed.ClearHistroyCurves();
                curve_Pos_Current.ClearHistroyCurves();
                curve_Pos_Pressure.ClearHistroyCurves();
                curve_Time_Speed.ClearHistroyCurves();
                curve_Time_Pos.ClearHistroyCurves();
                curve_Time_Pressure.ClearHistroyCurves();
            }
        }

        private void btnClear_MouseLeave(object sender, MouseEventArgs e)
        {
            btnClear.Background = Brushes.Transparent;
        }

        private bool _bIsSetVisiable = false;
        private void btnSet_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!_bIsMouseMove)
            {
                if (_bIsSetVisiable == false)
                {
                    _bIsSetVisiable = true;
                    btnSet.BorderBrush = new SolidColorBrush(Color.FromArgb(0xff, 0x00, 0xb4, 0xe1));

                    DoubleAnimation heightAnimation = new DoubleAnimation();
                    heightAnimation.From = 0;
                    heightAnimation.To = 50;
                    heightAnimation.Duration = TimeSpan.FromMilliseconds(300);
                    cvsSet.BeginAnimation(Canvas.HeightProperty, heightAnimation);
                }
                else
                {
                    _bIsSetVisiable = false;
                    btnSet.BorderBrush = Brushes.Transparent;

                    DoubleAnimation heightAnimation = new DoubleAnimation();
                    heightAnimation.From = 50;
                    heightAnimation.To = 0;
                    heightAnimation.Duration = TimeSpan.FromMilliseconds(300);
                    cvsSet.BeginAnimation(Canvas.HeightProperty, heightAnimation);
                }
            }
        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (btnMS.Visibility == Visibility.Visible)
            {
                btnMS.Visibility = Visibility.Hidden;
                cvsPrs.Visibility = Visibility.Visible;

                valmoWin.dv.InjPr[461].valueNew = 0;
                updateInjectionPrsSetMap();
            }
            else
            {
                btnMS.Visibility = Visibility.Visible;
                cvsPrs.Visibility = Visibility.Hidden;

                valmoWin.dv.InjPr[461].valueNew = 1;
                updateInjectionPrsSetMap();
            }
        }

        private void handleInjPr_046(objUnit obj)
        {
            updateInjectionPrsSetMap();
        }
    }
}
