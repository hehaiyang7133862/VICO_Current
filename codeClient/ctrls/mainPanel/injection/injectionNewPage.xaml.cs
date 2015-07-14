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
        /// 保压第一段时间
        /// </summary>
        private double holding_T1 = 0.0;
        /// <summary>
        /// 保压第二段时间
        /// </summary>
        private double holding_T2 = 0.0;
        /// <summary>
        /// 保压第三段时间
        /// </summary>
        private double holding_T3 = 0.0;
        /// <summary>
        /// 保压第四段时间
        /// </summary>
        private double holding_T4 = 0.0;
        /// <summary>
        /// 最大注射速度
        /// </summary>
        private double dMaxInjSpeed = 1;
        /// <summary>
        /// 最大保压压力
        /// </summary>
        private double dMaxHoldingPressure = 0.0;
        /// <summary>
        /// 注射最大行程
        /// </summary>
        private double dMaxInjStroke = 0;
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
        /// <summary>
        /// 注射分段曲线
        /// </summary>
        private List<Line> lstLineInj = new List<Line>();
        /// <summary>
        /// 注射分段节点
        /// </summary>
        private Point[] lstPointInj = new Point[21];
        /// <summary>
        /// 注射曲线节点
        /// </summary>
        private List<Ring> lstRingInj = new List<Ring>();
        /// <summary>
        /// 保压分段节点
        /// </summary>
        private Point[] lstPointHold = new Point[10];
        /// <summary>
        /// 保压分段节点
        /// </summary>
        private List<Ring> lstRingHold = new List<Ring>();
        /// <summary>
        /// 保压分段曲线
        /// </summary>
        private List<Line> lstLineHold = new List<Line>();

        public injectionNewPage()
        {
            InitializeComponent();
            init();

            cvsSet.Height = 0;
            valmoWin.lstStartUpInit.Add(startUpInit);

            valmoWin.dv.PrdPr[257].addHandle(HoldingCurveRefush);
            valmoWin.dv.PrdPr[256].addHandle(HoldingCurveSave);
            valmoWin.dv.PrdPr[252].addHandle(InjectionCurveRefush);
            valmoWin.dv.PrdPr[251].addHandle(InjectionCurveSave);

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
            int maxHoldSpeed = valmoWin.dv.InjPr[199].value;
            int maxHoldPressure = valmoWin.dv.InjPr[189].value;
            int maxHoldPos = valmoWin.dv.InjPr[49].value;

            if (holding_TSet == 0 || maxHoldPos == 0 || maxHoldPressure == 0 || maxHoldSpeed == 0)
            {
                return;
            }

            int count = obj.value;

            int[] HoldingData = new int[count * 4];
            Lasal32.GetData(HoldingData, (uint)valmoWin.dv.PrdPr[258].valueNew, count * 16);

            List<Point> curveData_Time_Speed = new List<Point>();
            List<Point> curveData_Time_Pressure = new List<Point>();
            List<Point> curveData_Time_Pos = new List<Point>();

            for (int i = 0; i < count; i++)
            {
                double time = HoldingData[i * 4 + 3] * 1.0 / holding_TSet / 1000 * 10000;
                double speed = Math.Abs(HoldingData[i * 4]) * 1.0 / maxHoldSpeed * 10000;
                double pressure = HoldingData[i * 4 + 1] * 1.0 / maxHoldPressure * 10000;
                double pos = HoldingData[i * 4 + 2] * 1.0 / maxHoldPos * 10000;

                curveData_Time_Pos.Add(new Point(10000 - time, 10000 - pos));
                curveData_Time_Pressure.Add(new Point(10000 - time, 10000 - pressure));
                curveData_Time_Speed.Add(new Point(10000 - time, 10000 - speed));
            }

            if (!bIsHoldCurveSave)
            {
                curve_Time_Speed.UpdateCurve(curveData_Time_Speed);
                curve_Time_Pressure.UpdateCurve(curveData_Time_Pressure);
                curve_Time_Pos.UpdateCurve(curveData_Time_Pos);

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
                curve_Time_Speed.SaveCurve(historyData_Time_Speed);
                curve_Time_Pressure.SaveCurve(historyData_Time_Pressure);
                curve_Time_Pos.SaveCurve(historyData_Time_Pos);
                bIsHoldCurveSave = false;
            }
        }

        private List<Point> historyData_Pos_Speed = new List<Point>();
        private List<Point> historyData_Pos_Pressure = new List<Point>();
        private List<Point> historyData_Pos_Current = new List<Point>();

        private void InjectionCurveRefush(objUnit obj)
        {
            int maxInjectSpeed = valmoWin.dv.InjPr[199].value;
            int maxInjectPressure = valmoWin.dv.InjPr[189].value;
            int maxInjectPosition = valmoWin.dv.InjPr[049].value;

            if (maxInjectSpeed == 0 || maxInjectPosition == 0 || maxInjectPressure == 0)
            {
                return;
            }

            int count = obj.value;

            int[] InjectionData = new int[count * 4];
            Lasal32.GetData(InjectionData, (uint)valmoWin.dv.PrdPr[253].valueNew, count * 16);

            List<Point> curveData_Pos_Speed = new List<Point>();
            List<Point> curveData_Pos_Pressure = new List<Point>();
            List<Point> curveData_Pos_Current = new List<Point>();

            for (int i = 0; i < count; i++)
            {
                double pos = InjectionData[i * 4 + 3] * 1.0 / maxInjectPosition * 10000;
                double current = InjectionData[i * 4 + 2] * 1.0 / 1000 * 10000;
                double speed = Math.Abs(InjectionData[i * 4]) * 1.0 / maxInjectSpeed * 10000;
                double pressure = InjectionData[i * 4 + 1] * 1.0 / maxInjectPressure * 10000;

                curveData_Pos_Speed.Add(new Point(pos, 10000 - speed));
                curveData_Pos_Pressure.Add(new Point(pos, 10000 - pressure));
                curveData_Pos_Current.Add(new Point(pos, 5000 - current / 2));
            }

            if (!bIsSaveCurve)
            {
                curve_Pos_Speed.UpdateCurve(curveData_Pos_Speed);
                curve_Pos_Pressure.UpdateCurve(curveData_Pos_Pressure);
                curve_Pos_Current.UpdateCurve(curveData_Pos_Current);

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
                curve_Pos_Speed.SaveCurve(historyData_Pos_Speed);
                curve_Pos_Pressure.SaveCurve(historyData_Pos_Pressure);
                curve_Pos_Current.SaveCurve(historyData_Pos_Current);

                bIsSaveCurve = false;
            }
        }

        /// <summary>
        /// 曲线数据地址初始化
        /// </summary>
        private void startUpInit()
        {
            dMaxInjStroke = valmoWin.dv.InjPr[049].vDbl;
        }

        private void updatelbVP()
        {
            lbVP.Content = ((valmoWin.dv.InjPr[49].vDbl - valmoWin.dv.InjPr[68].vDbl) * Math.PI * Math.Pow(valmoWin.dv.InjPr[207].vDbl / 2, 2) / 1000).ToString("0.00");
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

        public void init()
        {
            lstPointInj[0].Y = 0;
            lstPointInj[20].X = 0;

            for (int i = 0; i < 20; i++)
            {
                Line l = new Line();
                l.Stroke = new SolidColorBrush(Color.FromArgb(0xFF, 0x33, 0x79, 0xE2));
                l.StrokeThickness = 2;
                l.ClipToBounds = true;
                l.SnapsToDevicePixels = true;
                l.Visibility = Visibility.Hidden;
                cvsMap.Children.Add(l);
                lstLineInj.Add(l);
            }
            for (int i = 0; i < 21; i++)
            {
                Ring r = new Ring();
                r.Color = new SolidColorBrush(Color.FromArgb(0xFF, 0x33, 0x79, 0xE2));
                r.Visibility = Visibility.Hidden;
                cvsMap.Children.Add(r);
                lstRingInj.Add(r);
            }

            for (int i = 0; i < 9; i++)
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
            for (int i = 0; i < 10; i++)
            {
                Ring r = new Ring();
                r.Color = new SolidColorBrush(Color.FromArgb(0xFF, 0xE4, 0x00, 0xBB));
                r.Visibility = Visibility.Hidden;

                cvsMap.Children.Add(r);
                lstRingHold.Add(r);
            }

            valmoWin.dv.InjPr[68].addHandle(handle_Inj068);
            valmoWin.dv.InjPr[36].addHandle(handleInjPr_36);
            valmoWin.dv.InjPr[72].addHandle(handleInjPr_72);
            valmoWin.dv.InjPr[71].addHandle(handleInjPr_71);
            valmoWin.dv.InjPr[73].addHandle(handleInjPr_73);
            valmoWin.dv.InjPr[74].addHandle(handleInjPr_74);

            valmoWin.dv.InjPr[189].addHandle(handle_Inj189);
            valmoWin.dv.InjPr[199].addHandle(handle_Inj199);
            valmoWin.dv.InjPr[41].addHandle(handleInjPr_041);
            valmoWin.dv.InjPr[42].addHandle(handleInjPr_042);
            valmoWin.dv.InjPr[43].addHandle(handleInjPr_043);
            valmoWin.dv.InjPr[44].addHandle(handleInjPr_044);

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

            valmoWin.dv.InjPr[245].addHandle(handle_Inj245);
            valmoWin.dv.InjPr[246].addHandle(handle_Inj246);
            valmoWin.dv.InjPr[247].addHandle(handle_Inj247);
            valmoWin.dv.InjPr[248].addHandle(handle_Inj248);

            valmoWin.dv.InjPr[255].addHandle(handle_Inj255);
            valmoWin.dv.InjPr[256].addHandle(handle_Inj256);
            valmoWin.dv.InjPr[257].addHandle(handle_Inj257);
            valmoWin.dv.InjPr[258].addHandle(handle_Inj258);
            valmoWin.dv.InjPr[37].addHandle(handleInjPr_037);
            valmoWin.dv.InjPr[38].addHandle(handleInjPr_038);
            valmoWin.dv.InjPr[39].addHandle(handleInjPr_039);
            valmoWin.dv.InjPr[40].addHandle(handleInjPr_040);

            valmoWin.dv.InjPr[48].addHandle(handleInjPr_48);
            valmoWin.dv.InjPr[21].addHandle(handle_inj021);
        }

        private void handle_inj021(objUnit obj)
        {
            double ActInjExternPosition = obj.vDbl > 0 ? obj.vDbl : 0;

            if (dMaxInjStroke > 0)
            {
                Canvas.SetLeft(cvsCursor, ActInjExternPosition / dMaxInjStroke * 363.6 + 557);
                lbCurScrewPos.Content = ActInjExternPosition.ToString("0.00");
            }
        }

        /// <summary>
        /// handle_Inj049
        /// </summary>
        /// <param name="obj">注射起始位置</param>
        private void handle_Inj049(objUnit obj)
        {
            updatelbVP();

            lstPointInj[0].X = obj.vDbl;
            lstPointInj[1].X = obj.vDbl;

            dMaxInjStroke = obj.vDbl;

            refushInj();

            ClearPosSpeed();
            ClearPosPressure();
            ClearPosCurrent();

            setHoldingDisplacementStaff(dMaxInjStroke * 1.1);

            if (obj.value > 0)
            {
                lbPosV3.Content = obj.getStrValue(dMaxInjStroke * 1.1);
                lbPosV2.Content = obj.getStrValue(dMaxInjStroke * 0.75 * 1.1);
                lbPosV1.Content = obj.getStrValue(dMaxInjStroke * 0.5 * 1.1);
                lbPosV0.Content = obj.getStrValue(dMaxInjStroke * 0.25 * 1.1);
            }
        }
        /// <summary>
        /// handle_Inj050
        /// </summary>
        /// <param name="obj">注射位置二</param>
        private void handle_Inj050(objUnit obj)
        {
            lstPointInj[2].X = obj.vDbl;
            lstPointInj[3].X = obj.vDbl;
            refushInj();
        }
        /// <summary>
        /// handle_Inj051
        /// </summary>
        /// <param name="obj">注射位置三</param>
        private void handle_Inj051(objUnit obj)
        {
            lstPointInj[4].X = obj.vDbl;
            lstPointInj[5].X = obj.vDbl;
            refushInj();
        }
        /// <summary>
        /// handle_Inj052
        /// </summary>
        /// <param name="obj">注射位置四</param>
        private void handle_Inj052(objUnit obj)
        {
            lstPointInj[7].X = obj.vDbl;
            lstPointInj[6].X = obj.vDbl;
            refushInj();
        }
        /// <summary>
        /// handle_Inj053
        /// </summary>
        /// <param name="obj">注射位置五</param>
        private void handle_Inj053(objUnit obj)
        {
            lstPointInj[9].X = obj.vDbl;
            lstPointInj[8].X = obj.vDbl;
            refushInj();
        }
        /// <summary>
        /// handle_Inj054
        /// </summary>
        /// <param name="obj">注射位置六</param>
        private void handle_Inj054(objUnit obj)
        {
            lstPointInj[11].X = obj.vDbl;
            lstPointInj[10].X = obj.vDbl;
            refushInj();
        }
        /// <summary>
        /// handle_Inj245
        /// </summary>
        /// <param name="obj">注射位置七</param>
        private void handle_Inj245(objUnit obj)
        {
            lstPointInj[13].X = obj.vDbl;
            lstPointInj[12].X = obj.vDbl;
            refushInj();
        }
        /// <summary>
        /// handle_Inj246
        /// </summary>
        /// <param name="obj">注射位置八</param>
        private void handle_Inj246(objUnit obj)
        {
            lstPointInj[15].X = obj.vDbl;
            lstPointInj[14].X = obj.vDbl;
            refushInj();
        }
        /// <summary>
        /// handle_Inj247
        /// </summary>
        /// <param name="obj">注射位置九</param>
        private void handle_Inj247(objUnit obj)
        {
            lstPointInj[17].X = obj.vDbl;
            lstPointInj[16].X = obj.vDbl;
            refushInj();
        }
        /// <summary>
        /// handle_Inj248
        /// </summary>
        /// <param name="obj">注射位置十</param>
        private void handle_Inj248(objUnit obj)
        {
            lstPointInj[19].X = obj.vDbl;
            lstPointInj[18].X = obj.vDbl;
            refushInj();
        }
        /// <summary>
        /// handle_Inj056
        /// </summary>
        /// <param name="obj">注射速度一</param>
        private void handle_Inj056(objUnit obj)
        {
            lstPointInj[2].Y = obj.vDbl;
            lstPointInj[1].Y = obj.vDbl;
            refushInj();
        }
        /// <summary>
        /// handle_Inj057
        /// </summary>
        /// <param name="obj">注射速度二</param>
        private void handle_Inj057(objUnit obj)
        {
            lstPointInj[4].Y = obj.vDbl;
            lstPointInj[3].Y = obj.vDbl;
            refushInj();
        }
        /// <summary>
        /// handle_Inj058
        /// </summary>
        /// <param name="obj">注射速度三</param>
        private void handle_Inj058(objUnit obj)
        {
            lstPointInj[6].Y = obj.vDbl;
            lstPointInj[5].Y = obj.vDbl;
            refushInj();
        }
        /// <summary>
        /// handle_Inj059
        /// </summary>
        /// <param name="obj">注射速度四</param>
        private void handle_Inj059(objUnit obj)
        {
            lstPointInj[8].Y = obj.vDbl;
            lstPointInj[7].Y = obj.vDbl;
            refushInj();
        }
        /// <summary>
        /// handle_Inj060
        /// </summary>
        /// <param name="obj">注射速度五</param>
        private void handle_Inj060(objUnit obj)
        {
            lstPointInj[10].Y = obj.vDbl;
            lstPointInj[9].Y = obj.vDbl;
            refushInj();
        }
        /// <summary>
        /// handle_Inj255
        /// </summary>
        /// <param name="obj">注射速度六</param>
        private void handle_Inj255(objUnit obj)
        {
            lstPointInj[12].Y = obj.vDbl;
            lstPointInj[11].Y = obj.vDbl;
            refushInj();
        }
        /// <summary>
        /// handle_Inj256
        /// </summary>
        /// <param name="obj">注射速度七</param>
        private void handle_Inj256(objUnit obj)
        {
            lstPointInj[14].Y = obj.vDbl;
            lstPointInj[13].Y = obj.vDbl;
            refushInj();
        }
        /// <summary>
        /// handle_Inj257
        /// </summary>
        /// <param name="obj">注射速度八</param>
        private void handle_Inj257(objUnit obj)
        {
            lstPointInj[16].Y = obj.vDbl;
            lstPointInj[15].Y = obj.vDbl;
            refushInj();
        }
        /// <summary>
        /// handle_Inj258
        /// </summary>
        /// <param name="obj">注射速度九</param>
        private void handle_Inj258(objUnit obj)
        {
            lstPointInj[18].Y = obj.vDbl;
            lstPointInj[17].Y = obj.vDbl;
            refushInj();
        }
        /// <summary>
        /// handle_Inj061
        /// </summary>
        /// <param name="obj">注射速度十</param>
        private void handle_Inj061(objUnit obj)
        {
            lstPointInj[20].Y = obj.vDbl;
            lstPointInj[19].Y = obj.vDbl;

            refushInj();
        }

        /// <summary>
        /// handle_Inj026
        /// </summary>
        /// <param name="obj">VP切换位置</param>
        private void handle_Inj068(objUnit obj)
        {
            updatelbVP();

            if (dMaxInjStroke > 0)
            {
                Canvas.SetLeft(cvsVP, obj.vDbl * 363 / dMaxInjStroke + 363);
            }
        }
        /// <summary>
        /// 回调函数_Inj199
        /// </summary>
        /// <param name="obj">最大注射速度</param>
        private void handle_Inj199(objUnit obj)
        {
            dMaxInjSpeed = obj.vDbl > 0 ? obj.vDbl : 1;
            setInjSpeedStaff(dMaxInjSpeed * 1.1);
            refushInj();
        }
        /// <summary>
        /// 回调函数_Inj188
        /// </summary>
        /// <param name="obj">最大保压压力</param>
        private void handle_Inj189(objUnit obj)
        {
            dMaxHoldingPressure = obj.vDbl;
            setHoldingPressureStaff(dMaxHoldingPressure * 1.1);

            refushHoldingMap();
        }
        /// <summary>
        /// handle_Inj037
        /// </summary>
        /// <param name="obj">保压第一段压力</param>
        private void handleInjPr_037(objUnit obj)
        {
            lstPointHold[0].Y = valmoWin.dv.InjPr[37].vDbl;
            lstPointHold[1].Y = valmoWin.dv.InjPr[37].vDbl;

            refushHoldingMap();
        }
        /// <summary>
        /// handle_Inj038
        /// </summary>
        /// <param name="obj">保压第二段压力</param>
        private void handleInjPr_038(objUnit obj)
        {
            lstPointHold[2].Y = valmoWin.dv.InjPr[38].vDbl;
            lstPointHold[3].Y = valmoWin.dv.InjPr[38].vDbl;

            refushHoldingMap();
        }
        /// <summary>
        /// handle_Inj039
        /// </summary>
        /// <param name="obj">保压第三段压力</param>
        private void handleInjPr_039(objUnit obj)
        {
            lstPointHold[4].Y = valmoWin.dv.InjPr[39].vDbl;
            lstPointHold[5].Y = valmoWin.dv.InjPr[39].vDbl;

            refushHoldingMap();
        }
        /// <summary>
        /// handle_Inj040
        /// </summary>
        /// <param name="obj">保压第四段压力</param>
        private void handleInjPr_040(objUnit obj)
        {
            lstPointHold[6].Y = valmoWin.dv.InjPr[40].vDbl;
            lstPointHold[7].Y = valmoWin.dv.InjPr[40].vDbl;

            refushHoldingMap();
        }
        /// <summary>
        /// 获取保压第一段时间
        /// </summary>
        /// <param name="obj">Inj041</param>
        private void handleInjPr_041(objUnit obj)
        {
            holding_T1 = obj.vDbl;

            setHoldingTimeStaff();
        }
        /// <summary>
        /// 获取保压第二段时间
        /// </summary>
        /// <param name="obj">Inj042</param>
        private void handleInjPr_042(objUnit obj)
        {
            holding_T2 = obj.vDbl;
            setHoldingTimeStaff();
        }
        /// <summary>
        /// 获取保压第三段时间
        /// </summary>
        /// <param name="obj">Inj043</param>
        private void handleInjPr_043(objUnit obj)
        {
            holding_T3 = obj.vDbl;
            setHoldingTimeStaff();
        }
        /// <summary>
        /// 获取保压第四段时间
        /// </summary>
        /// <param name="obj">Inj044</param>
        private void handleInjPr_044(objUnit obj)
        {
            holding_T4 = obj.vDbl;
            setHoldingTimeStaff();
        }

        /// <summary>
        /// 保压设定时间
        /// </summary>
        private double holding_TSet = 0.0;
        /// <summary>
        /// 设定保压时间标尺
        /// </summary>
        private void setHoldingTimeStaff()
        {
            refushHoldPoint();

            ClearTimePos();
            ClearTimePressure();
            ClearTimeSpeed();

            switch (valmoWin.dv.InjPr[36].value)
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

            lbTime0.Content = (holding_TSet * 0.2 * 1.1).ToString();
            lbTime1.Content = (holding_TSet * 0.4 * 1.1).ToString();
            lbTime2.Content = (holding_TSet * 0.6 * 1.1).ToString();
            lbTime3.Content = (holding_TSet * 0.8 * 1.1).ToString();
            lbTime4.Content = (holding_TSet * 1.1).ToString();

            refushHoldingMap();
        }

        private void refushHoldPoint()
        {
            lstPointHold[0].X = 0;
            lstPointHold[0].Y = valmoWin.dv.InjPr[37].vDbl;
            lstPointHold[1].X = holding_T1;
            lstPointHold[1].Y = valmoWin.dv.InjPr[37].vDbl;
            lstPointHold[2].X = holding_T1;
            lstPointHold[2].Y = valmoWin.dv.InjPr[38].vDbl;
            lstPointHold[3].X = holding_T1 + holding_T2;
            lstPointHold[3].Y = valmoWin.dv.InjPr[38].vDbl;
            lstPointHold[4].X = holding_T1 + holding_T2;
            lstPointHold[4].Y = valmoWin.dv.InjPr[39].vDbl;
            lstPointHold[5].X = holding_T1 + holding_T2 + holding_T3;
            lstPointHold[5].Y = valmoWin.dv.InjPr[39].vDbl;
            lstPointHold[6].X = holding_T1 + holding_T2 + holding_T3;
            lstPointHold[6].Y = valmoWin.dv.InjPr[40].vDbl;
            lstPointHold[7].X = holding_T1 + holding_T2 + holding_T3 + holding_T4;
            lstPointHold[7].Y = valmoWin.dv.InjPr[40].vDbl;
            lstPointHold[8].X = holding_T1 + holding_T2 + holding_T3 + holding_T4;
            lstPointHold[8].Y = 0;
        }

        private void refushHoldingMap()
        {
            int count = valmoWin.dv.InjPr[36].value * 2 + 1;

            App.log.Info("Count" + count + "\t holdT" + holding_TSet + "\t MaxHoldPressure" + dMaxHoldingPressure);
            if (holding_TSet <= 0 || dMaxHoldingPressure <= 0)
            {
                return;
            }
            int i;
            for (i = 0; i < count - 1; i++)
            {
                lstRingHold[i].Visibility = Visibility.Visible;
                Canvas.SetLeft(lstRingHold[i], 359 - lstPointHold[i].X * 363 / holding_TSet);
                Canvas.SetTop(lstRingHold[i], 269 - lstPointHold[i].Y * 273 / dMaxHoldingPressure);

                lstLineHold[i].Visibility = Visibility.Visible;
                if (lstPointHold[i + 1].X >= lstPointHold[i].X)
                {
                    Canvas.SetLeft(lstLineHold[i], 363 - lstPointHold[i + 1].X * 363 / holding_TSet);
                    lstLineHold[i].X2 = (lstPointHold[i + 1].X - lstPointHold[i].X) * 363 / holding_TSet;
                }
                else
                {
                    Canvas.SetLeft(lstLineHold[i], 363 - lstPointHold[i].X * 363 / holding_TSet);
                    lstLineHold[i].X2 = (lstPointHold[i].X - lstPointHold[i + 1].X) * 363 / holding_TSet;
                }

                if (i == count - 2)
                {
                    Canvas.SetTop(lstLineHold[i], 273 - lstPointHold[i].Y * 273 / dMaxHoldingPressure);
                    lstLineHold[i].Y2 = lstPointHold[i].Y * 273 / dMaxHoldingPressure;
                }
                else
                {
                    if (lstPointHold[i + 1].Y <= lstPointHold[i].Y)
                    {
                        Canvas.SetTop(lstLineHold[i], 273 - lstPointHold[i].Y * 273 / dMaxHoldingPressure);
                        lstLineHold[i].Y2 = (lstPointHold[i].Y - lstPointHold[i + 1].Y) * 273 / dMaxHoldingPressure;
                    }
                    else
                    {
                        Canvas.SetTop(lstLineHold[i], 273 - lstPointHold[i + 1].Y * 273 / dMaxHoldingPressure);
                        lstLineHold[i].Y2 = (lstPointHold[i + 1].Y - lstPointHold[i].Y) * 273 / dMaxHoldingPressure;
                    }
                }
            }

            for (int j = i; j < 9; j++)
            {
                lstLineHold[j].Visibility = Visibility.Hidden;
                lstRingHold[j + 1].Visibility = Visibility.Hidden;
            }
            lstRingHold[i].Visibility = Visibility.Visible;
            Canvas.SetLeft(lstRingHold[i], 359 - lstPointHold[i].X * 363 / holding_TSet);
            Canvas.SetTop(lstRingHold[i], 269 - lstPointHold[8].Y * 273 / dMaxHoldingPressure);
        }

        private void setLineInj(Line ln, double x1, double y1, double x2, double y2)
        {
            if (dMaxInjSpeed != 0 && dMaxInjStroke != 0)
            {
                ln.X1 = (int)(x1 / dMaxInjStroke * 363 + 363);
                ln.Y1 = (int)(273 - y1 / dMaxInjSpeed * 273);
                ln.X2 = (int)(x2 / dMaxInjStroke * 363 + 363);
                ln.Y2 = (int)(273 - y2 / dMaxInjSpeed * 273);
                ln.Visibility = Visibility.Visible;
            }
        }

        private void setPointInj(Ring r, double x, double y)
        {
            if (dMaxInjSpeed != 0 && dMaxInjStroke != 0)
            {
                Canvas.SetLeft(r, (int)(x / dMaxInjStroke * 363 + 359));
                Canvas.SetTop(r, (int)(269 - y / dMaxInjSpeed * 273));
                r.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// 设置注射速度标尺
        /// </summary>
        /// <param name="vMax">最大注射速度</param>
        private void setInjSpeedStaff(double vMax)
        {
            ClearTimeSpeed();
            ClearPosSpeed();

            double tempVmax = vMax;

            lbSpeed1.Content = (tempVmax * 0.1).ToString("0.00");
            lbSpeed2.Content = (tempVmax * 0.2).ToString("0.00");
            lbSpeed3.Content = (tempVmax * 0.3).ToString("0.00");
            lbSpeed4.Content = (tempVmax * 0.4).ToString("0.00");
            lbSpeed5.Content = (tempVmax * 0.5).ToString("0.00");
            lbSpeed6.Content = (tempVmax * 0.6).ToString("0.00");
            lbSpeed7.Content = (tempVmax * 0.7).ToString("0.00");
            lbSpeed8.Content = (tempVmax * 0.8).ToString("0.00");
            lbSpeed9.Content = (tempVmax * 0.9).ToString("0.00");
            lbSpeed10.Content = (tempVmax).ToString("0.00");
        }
        /// <summary>
        /// 设置保压压力标尺
        /// </summary>
        /// <param name="maxHoldingPressure">最大保压压力</param>
        private void setHoldingPressureStaff(double pMax)
        {
            ClearTimePressure();
            ClearPosPressure();

            double tempPMax = pMax;

            lbPres1.Content = (tempPMax * 0.1).ToString("0.00");
            lbPres2.Content = (tempPMax * 0.2).ToString("0.00");
            lbPres3.Content = (tempPMax * 0.3).ToString("0.00");
            lbPres4.Content = (tempPMax * 0.4).ToString("0.00");
            lbPres5.Content = (tempPMax * 0.5).ToString("0.00");
            lbPres6.Content = (tempPMax * 0.6).ToString("0.00");
            lbPres7.Content = (tempPMax * 0.7).ToString("0.00");
            lbPres8.Content = (tempPMax * 0.8).ToString("0.00");
            lbPres9.Content = (tempPMax * 0.9).ToString("0.00");
            lbPres10.Content = (tempPMax).ToString("0.00");
        }
        /// <summary>
        /// 设置保压位移标尺
        /// </summary>
        /// <param name="vp_Postion">VP切换位置</param>
        private void setHoldingDisplacementStaff(double vp_Postion)
        {
            ClearTimePos();

            lbPosP1.Content = (vp_Postion * 0.1).ToString("0.00");
            lbPosP2.Content = (vp_Postion * 0.2).ToString("0.00");
            lbPosP3.Content = (vp_Postion * 0.3).ToString("0.00");
            lbPosP4.Content = (vp_Postion * 0.4).ToString("0.00");
            lbPosP5.Content = (vp_Postion * 0.5).ToString("0.00");
            lbPosP6.Content = (vp_Postion * 0.6).ToString("0.00");
            lbPosP7.Content = (vp_Postion * 0.7).ToString("0.00");
            lbPosP8.Content = (vp_Postion * 0.8).ToString("0.00");
            lbPosP9.Content = (vp_Postion * 0.9).ToString("0.00");
            lbPosP10.Content = (vp_Postion).ToString("0.00");
        }

        private void handleInjPr_36(objUnit obj)
        {
            switch (obj.value)
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

            setHoldingTimeStaff();
        }

        /// <summary>
        /// handle_Inj048
        /// </summary>
        /// <param name="obj">注射段数</param>
        private void handleInjPr_48(objUnit obj)
        {
            refushInj();
            switch (obj.value)
            {
                case 3:
                    {
                        imgInjPr_48_5_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_48_4_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_48_3_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_48_6_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_48_7_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_48_8_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_48_9_1_injection.Visibility = Visibility.Hidden;

                        btnInj052.Visibility = Visibility.Hidden;
                        btnInj053.Visibility = Visibility.Hidden;
                        btnInj054.Visibility = Visibility.Hidden;

                        btnInj061.Visibility = Visibility.Hidden;
                        btnInj059.Visibility = Visibility.Hidden;
                        btnInj060.Visibility = Visibility.Hidden;

                        btnInj245.Visibility = Visibility.Hidden;
                        btnInj246.Visibility = Visibility.Hidden;
                        btnInj247.Visibility = Visibility.Hidden;
                        btnInj248.Visibility = Visibility.Hidden;

                        btnInj255.Visibility = Visibility.Hidden;
                        btnInj256.Visibility = Visibility.Hidden;
                        btnInj257.Visibility = Visibility.Hidden;
                        btnInj258.Visibility = Visibility.Hidden;
                    }
                    break;
                case 4:
                    {
                        imgInjPr_48_5_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_48_4_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_48_3_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_48_6_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_48_7_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_48_8_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_48_9_1_injection.Visibility = Visibility.Hidden;

                        btnInj052.Visibility = Visibility.Visible;
                        btnInj053.Visibility = Visibility.Hidden;
                        btnInj054.Visibility = Visibility.Hidden;

                        btnInj059.Visibility = Visibility.Visible;
                        btnInj061.Visibility = Visibility.Hidden;
                        btnInj060.Visibility = Visibility.Hidden;

                        btnInj245.Visibility = Visibility.Hidden;
                        btnInj246.Visibility = Visibility.Hidden;
                        btnInj247.Visibility = Visibility.Hidden;
                        btnInj248.Visibility = Visibility.Hidden;

                        btnInj255.Visibility = Visibility.Hidden;
                        btnInj256.Visibility = Visibility.Hidden;
                        btnInj257.Visibility = Visibility.Hidden;
                        btnInj258.Visibility = Visibility.Hidden;
                    }
                    break;
                case 5:
                    {
                        imgInjPr_48_5_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_48_4_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_48_3_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_48_6_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_48_7_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_48_8_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_48_9_1_injection.Visibility = Visibility.Hidden;

                        btnInj052.Visibility = Visibility.Visible;
                        btnInj053.Visibility = Visibility.Visible;
                        btnInj054.Visibility = Visibility.Hidden;

                        btnInj060.Visibility = Visibility.Visible;
                        btnInj059.Visibility = Visibility.Visible;
                        btnInj061.Visibility = Visibility.Hidden;

                        btnInj245.Visibility = Visibility.Hidden;
                        btnInj246.Visibility = Visibility.Hidden;
                        btnInj247.Visibility = Visibility.Hidden;
                        btnInj248.Visibility = Visibility.Hidden;

                        btnInj255.Visibility = Visibility.Hidden;
                        btnInj256.Visibility = Visibility.Hidden;
                        btnInj257.Visibility = Visibility.Hidden;
                        btnInj258.Visibility = Visibility.Hidden;
                    }
                    break;
                case 6:
                    {
                        imgInjPr_48_5_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_48_4_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_48_3_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_48_6_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_48_7_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_48_8_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_48_9_1_injection.Visibility = Visibility.Hidden;

                        btnInj052.Visibility = Visibility.Visible;
                        btnInj053.Visibility = Visibility.Visible;
                        btnInj054.Visibility = Visibility.Visible;

                        btnInj061.Visibility = Visibility.Visible;
                        btnInj059.Visibility = Visibility.Visible;
                        btnInj060.Visibility = Visibility.Visible;

                        btnInj245.Visibility = Visibility.Hidden;
                        btnInj246.Visibility = Visibility.Hidden;
                        btnInj247.Visibility = Visibility.Hidden;
                        btnInj248.Visibility = Visibility.Hidden;

                        btnInj255.Visibility = Visibility.Hidden;
                        btnInj256.Visibility = Visibility.Hidden;
                        btnInj257.Visibility = Visibility.Hidden;
                        btnInj258.Visibility = Visibility.Hidden;
                    }
                    break;
                case 7:
                    {
                        imgInjPr_48_5_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_48_4_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_48_3_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_48_6_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_48_7_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_48_8_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_48_9_1_injection.Visibility = Visibility.Hidden;

                        btnInj052.Visibility = Visibility.Visible;
                        btnInj053.Visibility = Visibility.Visible;
                        btnInj054.Visibility = Visibility.Visible;

                        btnInj061.Visibility = Visibility.Visible;
                        btnInj059.Visibility = Visibility.Visible;
                        btnInj060.Visibility = Visibility.Visible;

                        btnInj245.Visibility = Visibility.Hidden;
                        btnInj246.Visibility = Visibility.Visible;
                        btnInj247.Visibility = Visibility.Hidden;
                        btnInj248.Visibility = Visibility.Hidden;

                        btnInj255.Visibility = Visibility.Visible;
                        btnInj256.Visibility = Visibility.Hidden;
                        btnInj257.Visibility = Visibility.Hidden;
                        btnInj258.Visibility = Visibility.Hidden;
                    }
                    break;
                case 8:
                    {
                        imgInjPr_48_5_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_48_4_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_48_3_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_48_6_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_48_7_1_injection.Visibility = Visibility.Visible;
                        imgInjPr_48_8_1_injection.Visibility = Visibility.Hidden;
                        imgInjPr_48_9_1_injection.Visibility = Visibility.Hidden;

                        btnInj052.Visibility = Visibility.Visible;
                        btnInj053.Visibility = Visibility.Visible;
                        btnInj054.Visibility = Visibility.Visible;

                        btnInj061.Visibility = Visibility.Visible;
                        btnInj059.Visibility = Visibility.Visible;
                        btnInj060.Visibility = Visibility.Visible;

                        btnInj245.Visibility = Visibility.Visible;
                        btnInj246.Visibility = Visibility.Visible;
                        btnInj247.Visibility = Visibility.Hidden;
                        btnInj248.Visibility = Visibility.Hidden;

                        btnInj255.Visibility = Visibility.Visible;
                        btnInj256.Visibility = Visibility.Visible;
                        btnInj257.Visibility = Visibility.Hidden;
                        btnInj258.Visibility = Visibility.Hidden;
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

                        btnInj052.Visibility = Visibility.Visible;
                        btnInj053.Visibility = Visibility.Visible;
                        btnInj054.Visibility = Visibility.Visible;

                        btnInj061.Visibility = Visibility.Visible;
                        btnInj059.Visibility = Visibility.Visible;
                        btnInj060.Visibility = Visibility.Visible;

                        btnInj245.Visibility = Visibility.Visible;
                        btnInj246.Visibility = Visibility.Visible;
                        btnInj247.Visibility = Visibility.Visible;
                        btnInj248.Visibility = Visibility.Hidden;

                        btnInj255.Visibility = Visibility.Visible;
                        btnInj256.Visibility = Visibility.Visible;
                        btnInj257.Visibility = Visibility.Visible;
                        btnInj258.Visibility = Visibility.Hidden;
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

                        btnInj052.Visibility = Visibility.Visible;
                        btnInj053.Visibility = Visibility.Visible;
                        btnInj054.Visibility = Visibility.Visible;

                        btnInj061.Visibility = Visibility.Visible;
                        btnInj059.Visibility = Visibility.Visible;
                        btnInj060.Visibility = Visibility.Visible;

                        btnInj245.Visibility = Visibility.Visible;
                        btnInj246.Visibility = Visibility.Visible;
                        btnInj247.Visibility = Visibility.Visible;
                        btnInj248.Visibility = Visibility.Visible;

                        btnInj255.Visibility = Visibility.Visible;
                        btnInj256.Visibility = Visibility.Visible;
                        btnInj257.Visibility = Visibility.Visible;
                        btnInj258.Visibility = Visibility.Visible;
                    }
                    break;
            }
        }

        private void refushInj()
        {
            int count = valmoWin.dv.InjPr[48].value;
            switch (count)
            {
                case 3:
                    setPointInj(lstRingInj[0], lstPointInj[0].X, lstPointInj[0].Y);
                    setPointInj(lstRingInj[1], lstPointInj[1].X, lstPointInj[1].Y);
                    setLineInj(lstLineInj[0], lstPointInj[0].X, lstPointInj[0].Y, lstPointInj[1].X, lstPointInj[1].Y);

                    setPointInj(lstRingInj[2], lstPointInj[2].X, lstPointInj[2].Y);
                    setPointInj(lstRingInj[3], lstPointInj[3].X, lstPointInj[3].Y);
                    setLineInj(lstLineInj[1], lstPointInj[1].X, lstPointInj[1].Y, lstPointInj[2].X, lstPointInj[2].Y);
                    setLineInj(lstLineInj[2], lstPointInj[2].X, lstPointInj[2].Y, lstPointInj[3].X, lstPointInj[3].Y);

                    setPointInj(lstRingInj[4], lstPointInj[4].X, lstPointInj[4].Y);
                    setPointInj(lstRingInj[5], lstPointInj[5].X, lstPointInj[5].Y);
                    setLineInj(lstLineInj[3], lstPointInj[3].X, lstPointInj[3].Y, lstPointInj[4].X, lstPointInj[4].Y);
                    setLineInj(lstLineInj[4], lstPointInj[4].X, lstPointInj[4].Y, lstPointInj[5].X, lstPointInj[5].Y);

                    setPointInj(lstRingInj[6], lstPointInj[20].X, lstPointInj[6].Y);
                    lstRingInj[7].Visibility = Visibility.Hidden;
                    setLineInj(lstLineInj[5], lstPointInj[5].X, lstPointInj[5].Y, lstPointInj[20].X, lstPointInj[6].Y);
                    lstLineInj[6].Visibility = Visibility.Hidden;

                    lstRingInj[8].Visibility = Visibility.Hidden;
                    lstRingInj[9].Visibility = Visibility.Hidden;
                    lstLineInj[7].Visibility = Visibility.Hidden;
                    lstLineInj[8].Visibility = Visibility.Hidden;

                    lstRingInj[10].Visibility = Visibility.Hidden;
                    lstRingInj[11].Visibility = Visibility.Hidden;
                    lstLineInj[9].Visibility = Visibility.Hidden;
                    lstLineInj[10].Visibility = Visibility.Hidden;

                    lstRingInj[12].Visibility = Visibility.Hidden;
                    lstRingInj[13].Visibility = Visibility.Hidden;
                    lstLineInj[11].Visibility = Visibility.Hidden;
                    lstLineInj[12].Visibility = Visibility.Hidden;

                    lstRingInj[14].Visibility = Visibility.Hidden;
                    lstRingInj[15].Visibility = Visibility.Hidden;
                    lstLineInj[13].Visibility = Visibility.Hidden;
                    lstLineInj[14].Visibility = Visibility.Hidden;

                    lstRingInj[16].Visibility = Visibility.Hidden;
                    lstRingInj[17].Visibility = Visibility.Hidden;
                    lstLineInj[15].Visibility = Visibility.Hidden;
                    lstLineInj[16].Visibility = Visibility.Hidden;

                    lstRingInj[18].Visibility = Visibility.Hidden;
                    lstRingInj[19].Visibility = Visibility.Hidden;
                    lstLineInj[17].Visibility = Visibility.Hidden;
                    lstLineInj[18].Visibility = Visibility.Hidden;

                    lstRingInj[20].Visibility = Visibility.Hidden;
                    lstLineInj[19].Visibility = Visibility.Hidden;

                    break;
                case 4:
                    setPointInj(lstRingInj[0], lstPointInj[0].X, lstPointInj[0].Y);
                    setPointInj(lstRingInj[1], lstPointInj[1].X, lstPointInj[1].Y);
                    setLineInj(lstLineInj[0], lstPointInj[0].X, lstPointInj[0].Y, lstPointInj[1].X, lstPointInj[1].Y);

                    setPointInj(lstRingInj[2], lstPointInj[2].X, lstPointInj[2].Y);
                    setPointInj(lstRingInj[3], lstPointInj[3].X, lstPointInj[3].Y);
                    setLineInj(lstLineInj[1], lstPointInj[1].X, lstPointInj[1].Y, lstPointInj[2].X, lstPointInj[2].Y);
                    setLineInj(lstLineInj[2], lstPointInj[2].X, lstPointInj[2].Y, lstPointInj[3].X, lstPointInj[3].Y);

                    setPointInj(lstRingInj[4], lstPointInj[4].X, lstPointInj[4].Y);
                    setPointInj(lstRingInj[5], lstPointInj[5].X, lstPointInj[5].Y);
                    setLineInj(lstLineInj[3], lstPointInj[3].X, lstPointInj[3].Y, lstPointInj[4].X, lstPointInj[4].Y);
                    setLineInj(lstLineInj[4], lstPointInj[4].X, lstPointInj[4].Y, lstPointInj[5].X, lstPointInj[5].Y);

                    setPointInj(lstRingInj[6], lstPointInj[6].X, lstPointInj[6].Y);
                    setPointInj(lstRingInj[7], lstPointInj[7].X, lstPointInj[7].Y);
                    setLineInj(lstLineInj[5], lstPointInj[5].X, lstPointInj[5].Y, lstPointInj[6].X, lstPointInj[6].Y);
                    setLineInj(lstLineInj[6], lstPointInj[6].X, lstPointInj[6].Y, lstPointInj[7].X, lstPointInj[7].Y);

                    setPointInj(lstRingInj[8], lstPointInj[20].X, lstPointInj[8].Y);
                    lstRingInj[9].Visibility = Visibility.Hidden;
                    setLineInj(lstLineInj[7], lstPointInj[7].X, lstPointInj[7].Y, lstPointInj[20].X, lstPointInj[8].Y);
                    lstLineInj[8].Visibility = Visibility.Hidden;

                    lstRingInj[10].Visibility = Visibility.Hidden;
                    lstRingInj[11].Visibility = Visibility.Hidden;
                    lstLineInj[9].Visibility = Visibility.Hidden;
                    lstLineInj[10].Visibility = Visibility.Hidden;

                    lstRingInj[12].Visibility = Visibility.Hidden;
                    lstRingInj[13].Visibility = Visibility.Hidden;
                    lstLineInj[11].Visibility = Visibility.Hidden;
                    lstLineInj[12].Visibility = Visibility.Hidden;

                    lstRingInj[14].Visibility = Visibility.Hidden;
                    lstRingInj[15].Visibility = Visibility.Hidden;
                    lstLineInj[13].Visibility = Visibility.Hidden;
                    lstLineInj[14].Visibility = Visibility.Hidden;

                    lstRingInj[16].Visibility = Visibility.Hidden;
                    lstRingInj[17].Visibility = Visibility.Hidden;
                    lstLineInj[15].Visibility = Visibility.Hidden;
                    lstLineInj[16].Visibility = Visibility.Hidden;

                    lstRingInj[18].Visibility = Visibility.Hidden;
                    lstRingInj[19].Visibility = Visibility.Hidden;
                    lstLineInj[17].Visibility = Visibility.Hidden;
                    lstLineInj[18].Visibility = Visibility.Hidden;

                    lstRingInj[20].Visibility = Visibility.Hidden;
                    lstLineInj[19].Visibility = Visibility.Hidden;
                    break;
                case 5:
                    setPointInj(lstRingInj[0], lstPointInj[0].X, lstPointInj[0].Y);
                    setPointInj(lstRingInj[1], lstPointInj[1].X, lstPointInj[1].Y);
                    setLineInj(lstLineInj[0], lstPointInj[0].X, lstPointInj[0].Y, lstPointInj[1].X, lstPointInj[1].Y);

                    setPointInj(lstRingInj[2], lstPointInj[2].X, lstPointInj[2].Y);
                    setPointInj(lstRingInj[3], lstPointInj[3].X, lstPointInj[3].Y);
                    setLineInj(lstLineInj[1], lstPointInj[1].X, lstPointInj[1].Y, lstPointInj[2].X, lstPointInj[2].Y);
                    setLineInj(lstLineInj[2], lstPointInj[2].X, lstPointInj[2].Y, lstPointInj[3].X, lstPointInj[3].Y);

                    setPointInj(lstRingInj[4], lstPointInj[4].X, lstPointInj[4].Y);
                    setPointInj(lstRingInj[5], lstPointInj[5].X, lstPointInj[5].Y);
                    setLineInj(lstLineInj[3], lstPointInj[3].X, lstPointInj[3].Y, lstPointInj[4].X, lstPointInj[4].Y);
                    setLineInj(lstLineInj[4], lstPointInj[4].X, lstPointInj[4].Y, lstPointInj[5].X, lstPointInj[5].Y);

                    setPointInj(lstRingInj[6], lstPointInj[6].X, lstPointInj[6].Y);
                    setPointInj(lstRingInj[7], lstPointInj[7].X, lstPointInj[7].Y);
                    setLineInj(lstLineInj[5], lstPointInj[5].X, lstPointInj[5].Y, lstPointInj[6].X, lstPointInj[6].Y);
                    setLineInj(lstLineInj[6], lstPointInj[6].X, lstPointInj[6].Y, lstPointInj[7].X, lstPointInj[7].Y);

                    setPointInj(lstRingInj[8], lstPointInj[8].X, lstPointInj[8].Y);
                    setPointInj(lstRingInj[9], lstPointInj[9].X, lstPointInj[9].Y);
                    setLineInj(lstLineInj[7], lstPointInj[7].X, lstPointInj[7].Y, lstPointInj[8].X, lstPointInj[8].Y);
                    setLineInj(lstLineInj[8], lstPointInj[8].X, lstPointInj[8].Y, lstPointInj[9].X, lstPointInj[9].Y);

                    setPointInj(lstRingInj[10], lstPointInj[20].X, lstPointInj[10].Y);
                    lstRingInj[11].Visibility = Visibility.Hidden;
                    setLineInj(lstLineInj[9], lstPointInj[9].X, lstPointInj[9].Y, lstPointInj[20].X, lstPointInj[10].Y);
                    lstLineInj[10].Visibility = Visibility.Hidden;

                    lstRingInj[12].Visibility = Visibility.Hidden;
                    lstRingInj[13].Visibility = Visibility.Hidden;
                    lstLineInj[11].Visibility = Visibility.Hidden;
                    lstLineInj[12].Visibility = Visibility.Hidden;

                    lstRingInj[14].Visibility = Visibility.Hidden;
                    lstRingInj[15].Visibility = Visibility.Hidden;
                    lstLineInj[13].Visibility = Visibility.Hidden;
                    lstLineInj[14].Visibility = Visibility.Hidden;

                    lstRingInj[16].Visibility = Visibility.Hidden;
                    lstRingInj[17].Visibility = Visibility.Hidden;
                    lstLineInj[15].Visibility = Visibility.Hidden;
                    lstLineInj[16].Visibility = Visibility.Hidden;

                    lstRingInj[18].Visibility = Visibility.Hidden;
                    lstRingInj[19].Visibility = Visibility.Hidden;
                    lstLineInj[17].Visibility = Visibility.Hidden;
                    lstLineInj[18].Visibility = Visibility.Hidden;

                    lstRingInj[20].Visibility = Visibility.Hidden;
                    lstLineInj[19].Visibility = Visibility.Hidden;
                    break;
                case 6:
                    setPointInj(lstRingInj[0], lstPointInj[0].X, lstPointInj[0].Y);
                    setPointInj(lstRingInj[1], lstPointInj[1].X, lstPointInj[1].Y);
                    setLineInj(lstLineInj[0], lstPointInj[0].X, lstPointInj[0].Y, lstPointInj[1].X, lstPointInj[1].Y);

                    setPointInj(lstRingInj[2], lstPointInj[2].X, lstPointInj[2].Y);
                    setPointInj(lstRingInj[3], lstPointInj[3].X, lstPointInj[3].Y);
                    setLineInj(lstLineInj[1], lstPointInj[1].X, lstPointInj[1].Y, lstPointInj[2].X, lstPointInj[2].Y);
                    setLineInj(lstLineInj[2], lstPointInj[2].X, lstPointInj[2].Y, lstPointInj[3].X, lstPointInj[3].Y);

                    setPointInj(lstRingInj[4], lstPointInj[4].X, lstPointInj[4].Y);
                    setPointInj(lstRingInj[5], lstPointInj[5].X, lstPointInj[5].Y);
                    setLineInj(lstLineInj[3], lstPointInj[3].X, lstPointInj[3].Y, lstPointInj[4].X, lstPointInj[4].Y);
                    setLineInj(lstLineInj[4], lstPointInj[4].X, lstPointInj[4].Y, lstPointInj[5].X, lstPointInj[5].Y);

                    setPointInj(lstRingInj[6], lstPointInj[6].X, lstPointInj[6].Y);
                    setPointInj(lstRingInj[7], lstPointInj[7].X, lstPointInj[7].Y);
                    setLineInj(lstLineInj[5], lstPointInj[5].X, lstPointInj[5].Y, lstPointInj[6].X, lstPointInj[6].Y);
                    setLineInj(lstLineInj[6], lstPointInj[6].X, lstPointInj[6].Y, lstPointInj[7].X, lstPointInj[7].Y);

                    setPointInj(lstRingInj[8], lstPointInj[8].X, lstPointInj[8].Y);
                    setPointInj(lstRingInj[9], lstPointInj[9].X, lstPointInj[9].Y);
                    setLineInj(lstLineInj[7], lstPointInj[7].X, lstPointInj[7].Y, lstPointInj[8].X, lstPointInj[8].Y);
                    setLineInj(lstLineInj[8], lstPointInj[8].X, lstPointInj[8].Y, lstPointInj[9].X, lstPointInj[9].Y);

                    setPointInj(lstRingInj[10], lstPointInj[10].X, lstPointInj[10].Y);
                    setPointInj(lstRingInj[11], lstPointInj[11].X, lstPointInj[11].Y);
                    setLineInj(lstLineInj[9], lstPointInj[9].X, lstPointInj[9].Y, lstPointInj[10].X, lstPointInj[10].Y);
                    setLineInj(lstLineInj[10], lstPointInj[10].X, lstPointInj[10].Y, lstPointInj[11].X, lstPointInj[11].Y);

                    setPointInj(lstRingInj[12], lstPointInj[20].X, lstPointInj[12].Y);
                    lstRingInj[13].Visibility = Visibility.Hidden;
                    setLineInj(lstLineInj[11], lstPointInj[11].X, lstPointInj[11].Y, lstPointInj[20].X, lstPointInj[12].Y);
                    lstLineInj[12].Visibility = Visibility.Hidden;

                    lstRingInj[14].Visibility = Visibility.Hidden;
                    lstRingInj[15].Visibility = Visibility.Hidden;
                    lstLineInj[13].Visibility = Visibility.Hidden;
                    lstLineInj[14].Visibility = Visibility.Hidden;

                    lstRingInj[16].Visibility = Visibility.Hidden;
                    lstRingInj[17].Visibility = Visibility.Hidden;
                    lstLineInj[15].Visibility = Visibility.Hidden;
                    lstLineInj[16].Visibility = Visibility.Hidden;

                    lstRingInj[18].Visibility = Visibility.Hidden;
                    lstRingInj[19].Visibility = Visibility.Hidden;
                    lstLineInj[17].Visibility = Visibility.Hidden;
                    lstLineInj[18].Visibility = Visibility.Hidden;

                    lstRingInj[20].Visibility = Visibility.Hidden;
                    lstLineInj[19].Visibility = Visibility.Hidden;
                    break;
                case 7:
                    setPointInj(lstRingInj[0], lstPointInj[0].X, lstPointInj[0].Y);
                    setPointInj(lstRingInj[1], lstPointInj[1].X, lstPointInj[1].Y);
                    setLineInj(lstLineInj[0], lstPointInj[0].X, lstPointInj[0].Y, lstPointInj[1].X, lstPointInj[1].Y);

                    setPointInj(lstRingInj[2], lstPointInj[2].X, lstPointInj[2].Y);
                    setPointInj(lstRingInj[3], lstPointInj[3].X, lstPointInj[3].Y);
                    setLineInj(lstLineInj[1], lstPointInj[1].X, lstPointInj[1].Y, lstPointInj[2].X, lstPointInj[2].Y);
                    setLineInj(lstLineInj[2], lstPointInj[2].X, lstPointInj[2].Y, lstPointInj[3].X, lstPointInj[3].Y);

                    setPointInj(lstRingInj[4], lstPointInj[4].X, lstPointInj[4].Y);
                    setPointInj(lstRingInj[5], lstPointInj[5].X, lstPointInj[5].Y);
                    setLineInj(lstLineInj[3], lstPointInj[3].X, lstPointInj[3].Y, lstPointInj[4].X, lstPointInj[4].Y);
                    setLineInj(lstLineInj[4], lstPointInj[4].X, lstPointInj[4].Y, lstPointInj[5].X, lstPointInj[5].Y);

                    setPointInj(lstRingInj[6], lstPointInj[6].X, lstPointInj[6].Y);
                    setPointInj(lstRingInj[7], lstPointInj[7].X, lstPointInj[7].Y);
                    setLineInj(lstLineInj[5], lstPointInj[5].X, lstPointInj[5].Y, lstPointInj[6].X, lstPointInj[6].Y);
                    setLineInj(lstLineInj[6], lstPointInj[6].X, lstPointInj[6].Y, lstPointInj[7].X, lstPointInj[7].Y);

                    setPointInj(lstRingInj[8], lstPointInj[8].X, lstPointInj[8].Y);
                    setPointInj(lstRingInj[9], lstPointInj[9].X, lstPointInj[9].Y);
                    setLineInj(lstLineInj[7], lstPointInj[7].X, lstPointInj[7].Y, lstPointInj[8].X, lstPointInj[8].Y);
                    setLineInj(lstLineInj[8], lstPointInj[8].X, lstPointInj[8].Y, lstPointInj[9].X, lstPointInj[9].Y);

                    setPointInj(lstRingInj[10], lstPointInj[10].X, lstPointInj[10].Y);
                    setPointInj(lstRingInj[11], lstPointInj[11].X, lstPointInj[11].Y);
                    setLineInj(lstLineInj[9], lstPointInj[9].X, lstPointInj[9].Y, lstPointInj[10].X, lstPointInj[10].Y);
                    setLineInj(lstLineInj[10], lstPointInj[10].X, lstPointInj[10].Y, lstPointInj[11].X, lstPointInj[11].Y);

                    setPointInj(lstRingInj[12], lstPointInj[12].X, lstPointInj[12].Y);
                    setPointInj(lstRingInj[13], lstPointInj[13].X, lstPointInj[13].Y);
                    setLineInj(lstLineInj[11], lstPointInj[11].X, lstPointInj[11].Y, lstPointInj[12].X, lstPointInj[12].Y);
                    setLineInj(lstLineInj[12], lstPointInj[12].X, lstPointInj[12].Y, lstPointInj[13].X, lstPointInj[13].Y);

                    setPointInj(lstRingInj[14], lstPointInj[20].X, lstPointInj[14].Y);
                    lstRingInj[15].Visibility = Visibility.Hidden;
                    setLineInj(lstLineInj[13], lstPointInj[13].X, lstPointInj[13].Y, lstPointInj[20].X, lstPointInj[14].Y);
                    lstLineInj[14].Visibility = Visibility.Hidden;

                    lstRingInj[16].Visibility = Visibility.Hidden;
                    lstRingInj[17].Visibility = Visibility.Hidden;
                    lstLineInj[15].Visibility = Visibility.Hidden;
                    lstLineInj[16].Visibility = Visibility.Hidden;

                    lstRingInj[18].Visibility = Visibility.Hidden;
                    lstRingInj[19].Visibility = Visibility.Hidden;
                    lstLineInj[17].Visibility = Visibility.Hidden;
                    lstLineInj[18].Visibility = Visibility.Hidden;

                    lstRingInj[20].Visibility = Visibility.Hidden;
                    lstLineInj[19].Visibility = Visibility.Hidden;
                    break;
                case 8:
                    setPointInj(lstRingInj[0], lstPointInj[0].X, lstPointInj[0].Y);
                    setPointInj(lstRingInj[1], lstPointInj[1].X, lstPointInj[1].Y);
                    setLineInj(lstLineInj[0], lstPointInj[0].X, lstPointInj[0].Y, lstPointInj[1].X, lstPointInj[1].Y);

                    setPointInj(lstRingInj[2], lstPointInj[2].X, lstPointInj[2].Y);
                    setPointInj(lstRingInj[3], lstPointInj[3].X, lstPointInj[3].Y);
                    setLineInj(lstLineInj[1], lstPointInj[1].X, lstPointInj[1].Y, lstPointInj[2].X, lstPointInj[2].Y);
                    setLineInj(lstLineInj[2], lstPointInj[2].X, lstPointInj[2].Y, lstPointInj[3].X, lstPointInj[3].Y);

                    setPointInj(lstRingInj[4], lstPointInj[4].X, lstPointInj[4].Y);
                    setPointInj(lstRingInj[5], lstPointInj[5].X, lstPointInj[5].Y);
                    setLineInj(lstLineInj[3], lstPointInj[3].X, lstPointInj[3].Y, lstPointInj[4].X, lstPointInj[4].Y);
                    setLineInj(lstLineInj[4], lstPointInj[4].X, lstPointInj[4].Y, lstPointInj[5].X, lstPointInj[5].Y);

                    setPointInj(lstRingInj[6], lstPointInj[6].X, lstPointInj[6].Y);
                    setPointInj(lstRingInj[7], lstPointInj[7].X, lstPointInj[7].Y);
                    setLineInj(lstLineInj[5], lstPointInj[5].X, lstPointInj[5].Y, lstPointInj[6].X, lstPointInj[6].Y);
                    setLineInj(lstLineInj[6], lstPointInj[6].X, lstPointInj[6].Y, lstPointInj[7].X, lstPointInj[7].Y);

                    setPointInj(lstRingInj[8], lstPointInj[8].X, lstPointInj[8].Y);
                    setPointInj(lstRingInj[9], lstPointInj[9].X, lstPointInj[9].Y);
                    setLineInj(lstLineInj[7], lstPointInj[7].X, lstPointInj[7].Y, lstPointInj[8].X, lstPointInj[8].Y);
                    setLineInj(lstLineInj[8], lstPointInj[8].X, lstPointInj[8].Y, lstPointInj[9].X, lstPointInj[9].Y);

                    setPointInj(lstRingInj[10], lstPointInj[10].X, lstPointInj[10].Y);
                    setPointInj(lstRingInj[11], lstPointInj[11].X, lstPointInj[11].Y);
                    setLineInj(lstLineInj[9], lstPointInj[9].X, lstPointInj[9].Y, lstPointInj[10].X, lstPointInj[10].Y);
                    setLineInj(lstLineInj[10], lstPointInj[10].X, lstPointInj[10].Y, lstPointInj[11].X, lstPointInj[11].Y);

                    setPointInj(lstRingInj[12], lstPointInj[12].X, lstPointInj[12].Y);
                    setPointInj(lstRingInj[13], lstPointInj[13].X, lstPointInj[13].Y);
                    setLineInj(lstLineInj[11], lstPointInj[11].X, lstPointInj[11].Y, lstPointInj[12].X, lstPointInj[12].Y);
                    setLineInj(lstLineInj[12], lstPointInj[12].X, lstPointInj[12].Y, lstPointInj[13].X, lstPointInj[13].Y);

                    setPointInj(lstRingInj[14], lstPointInj[14].X, lstPointInj[14].Y);
                    setPointInj(lstRingInj[15], lstPointInj[15].X, lstPointInj[15].Y);
                    setLineInj(lstLineInj[13], lstPointInj[13].X, lstPointInj[13].Y, lstPointInj[14].X, lstPointInj[14].Y);
                    setLineInj(lstLineInj[14], lstPointInj[14].X, lstPointInj[14].Y, lstPointInj[15].X, lstPointInj[15].Y);

                    setPointInj(lstRingInj[16], lstPointInj[20].X, lstPointInj[16].Y);
                    lstRingInj[17].Visibility = Visibility.Hidden;
                    setLineInj(lstLineInj[15], lstPointInj[15].X, lstPointInj[15].Y, lstPointInj[20].X, lstPointInj[16].Y);
                    lstLineInj[16].Visibility = Visibility.Hidden;

                    lstRingInj[18].Visibility = Visibility.Hidden;
                    lstRingInj[19].Visibility = Visibility.Hidden;
                    lstLineInj[17].Visibility = Visibility.Hidden;
                    lstLineInj[18].Visibility = Visibility.Hidden;

                    lstRingInj[20].Visibility = Visibility.Hidden;
                    lstLineInj[19].Visibility = Visibility.Hidden;
                    break;
                case 9:
                    setPointInj(lstRingInj[0], lstPointInj[0].X, lstPointInj[0].Y);
                    setPointInj(lstRingInj[1], lstPointInj[1].X, lstPointInj[1].Y);
                    setLineInj(lstLineInj[0], lstPointInj[0].X, lstPointInj[0].Y, lstPointInj[1].X, lstPointInj[1].Y);

                    setPointInj(lstRingInj[2], lstPointInj[2].X, lstPointInj[2].Y);
                    setPointInj(lstRingInj[3], lstPointInj[3].X, lstPointInj[3].Y);
                    setLineInj(lstLineInj[1], lstPointInj[1].X, lstPointInj[1].Y, lstPointInj[2].X, lstPointInj[2].Y);
                    setLineInj(lstLineInj[2], lstPointInj[2].X, lstPointInj[2].Y, lstPointInj[3].X, lstPointInj[3].Y);

                    setPointInj(lstRingInj[4], lstPointInj[4].X, lstPointInj[4].Y);
                    setPointInj(lstRingInj[5], lstPointInj[5].X, lstPointInj[5].Y);
                    setLineInj(lstLineInj[3], lstPointInj[3].X, lstPointInj[3].Y, lstPointInj[4].X, lstPointInj[4].Y);
                    setLineInj(lstLineInj[4], lstPointInj[4].X, lstPointInj[4].Y, lstPointInj[5].X, lstPointInj[5].Y);

                    setPointInj(lstRingInj[6], lstPointInj[6].X, lstPointInj[6].Y);
                    setPointInj(lstRingInj[7], lstPointInj[7].X, lstPointInj[7].Y);
                    setLineInj(lstLineInj[5], lstPointInj[5].X, lstPointInj[5].Y, lstPointInj[6].X, lstPointInj[6].Y);
                    setLineInj(lstLineInj[6], lstPointInj[6].X, lstPointInj[6].Y, lstPointInj[7].X, lstPointInj[7].Y);

                    setPointInj(lstRingInj[8], lstPointInj[8].X, lstPointInj[8].Y);
                    setPointInj(lstRingInj[9], lstPointInj[9].X, lstPointInj[9].Y);
                    setLineInj(lstLineInj[7], lstPointInj[7].X, lstPointInj[7].Y, lstPointInj[8].X, lstPointInj[8].Y);
                    setLineInj(lstLineInj[8], lstPointInj[8].X, lstPointInj[8].Y, lstPointInj[9].X, lstPointInj[9].Y);

                    setPointInj(lstRingInj[10], lstPointInj[10].X, lstPointInj[10].Y);
                    setPointInj(lstRingInj[11], lstPointInj[11].X, lstPointInj[11].Y);
                    setLineInj(lstLineInj[9], lstPointInj[9].X, lstPointInj[9].Y, lstPointInj[10].X, lstPointInj[10].Y);
                    setLineInj(lstLineInj[10], lstPointInj[10].X, lstPointInj[10].Y, lstPointInj[11].X, lstPointInj[11].Y);

                    setPointInj(lstRingInj[12], lstPointInj[12].X, lstPointInj[12].Y);
                    setPointInj(lstRingInj[13], lstPointInj[13].X, lstPointInj[13].Y);
                    setLineInj(lstLineInj[11], lstPointInj[11].X, lstPointInj[11].Y, lstPointInj[12].X, lstPointInj[12].Y);
                    setLineInj(lstLineInj[12], lstPointInj[12].X, lstPointInj[12].Y, lstPointInj[13].X, lstPointInj[13].Y);

                    setPointInj(lstRingInj[14], lstPointInj[14].X, lstPointInj[14].Y);
                    setPointInj(lstRingInj[15], lstPointInj[15].X, lstPointInj[15].Y);
                    setLineInj(lstLineInj[13], lstPointInj[13].X, lstPointInj[13].Y, lstPointInj[14].X, lstPointInj[14].Y);
                    setLineInj(lstLineInj[14], lstPointInj[14].X, lstPointInj[14].Y, lstPointInj[15].X, lstPointInj[15].Y);

                    setPointInj(lstRingInj[16], lstPointInj[16].X, lstPointInj[16].Y);
                    setPointInj(lstRingInj[17], lstPointInj[17].X, lstPointInj[17].Y);
                    setLineInj(lstLineInj[15], lstPointInj[15].X, lstPointInj[15].Y, lstPointInj[16].X, lstPointInj[16].Y);
                    setLineInj(lstLineInj[16], lstPointInj[16].X, lstPointInj[16].Y, lstPointInj[17].X, lstPointInj[17].Y);

                    setPointInj(lstRingInj[18], lstPointInj[20].X, lstPointInj[18].Y);
                    lstRingInj[19].Visibility = Visibility.Hidden;
                    setLineInj(lstLineInj[17], lstPointInj[17].X, lstPointInj[17].Y, lstPointInj[20].X, lstPointInj[18].Y);
                    lstLineInj[18].Visibility = Visibility.Hidden;

                    lstRingInj[20].Visibility = Visibility.Hidden;
                    lstLineInj[19].Visibility = Visibility.Hidden;
                    break;
                case 10:
                    setPointInj(lstRingInj[0], lstPointInj[0].X, lstPointInj[0].Y);
                    setPointInj(lstRingInj[1], lstPointInj[1].X, lstPointInj[1].Y);
                    setLineInj(lstLineInj[0], lstPointInj[0].X, lstPointInj[0].Y, lstPointInj[1].X, lstPointInj[1].Y);

                    setPointInj(lstRingInj[2], lstPointInj[2].X, lstPointInj[2].Y);
                    setPointInj(lstRingInj[3], lstPointInj[3].X, lstPointInj[3].Y);
                    setLineInj(lstLineInj[1], lstPointInj[1].X, lstPointInj[1].Y, lstPointInj[2].X, lstPointInj[2].Y);
                    setLineInj(lstLineInj[2], lstPointInj[2].X, lstPointInj[2].Y, lstPointInj[3].X, lstPointInj[3].Y);

                    setPointInj(lstRingInj[4], lstPointInj[4].X, lstPointInj[4].Y);
                    setPointInj(lstRingInj[5], lstPointInj[5].X, lstPointInj[5].Y);
                    setLineInj(lstLineInj[3], lstPointInj[3].X, lstPointInj[3].Y, lstPointInj[4].X, lstPointInj[4].Y);
                    setLineInj(lstLineInj[4], lstPointInj[4].X, lstPointInj[4].Y, lstPointInj[5].X, lstPointInj[5].Y);

                    setPointInj(lstRingInj[6], lstPointInj[6].X, lstPointInj[6].Y);
                    setPointInj(lstRingInj[7], lstPointInj[7].X, lstPointInj[7].Y);
                    setLineInj(lstLineInj[5], lstPointInj[5].X, lstPointInj[5].Y, lstPointInj[6].X, lstPointInj[6].Y);
                    setLineInj(lstLineInj[6], lstPointInj[6].X, lstPointInj[6].Y, lstPointInj[7].X, lstPointInj[7].Y);

                    setPointInj(lstRingInj[8], lstPointInj[8].X, lstPointInj[8].Y);
                    setPointInj(lstRingInj[9], lstPointInj[9].X, lstPointInj[9].Y);
                    setLineInj(lstLineInj[7], lstPointInj[7].X, lstPointInj[7].Y, lstPointInj[8].X, lstPointInj[8].Y);
                    setLineInj(lstLineInj[8], lstPointInj[8].X, lstPointInj[8].Y, lstPointInj[9].X, lstPointInj[9].Y);

                    setPointInj(lstRingInj[10], lstPointInj[10].X, lstPointInj[10].Y);
                    setPointInj(lstRingInj[11], lstPointInj[11].X, lstPointInj[11].Y);
                    setLineInj(lstLineInj[9], lstPointInj[9].X, lstPointInj[9].Y, lstPointInj[10].X, lstPointInj[10].Y);
                    setLineInj(lstLineInj[10], lstPointInj[10].X, lstPointInj[10].Y, lstPointInj[11].X, lstPointInj[11].Y);

                    setPointInj(lstRingInj[12], lstPointInj[12].X, lstPointInj[12].Y);
                    setPointInj(lstRingInj[13], lstPointInj[13].X, lstPointInj[13].Y);
                    setLineInj(lstLineInj[11], lstPointInj[11].X, lstPointInj[11].Y, lstPointInj[12].X, lstPointInj[12].Y);
                    setLineInj(lstLineInj[12], lstPointInj[12].X, lstPointInj[12].Y, lstPointInj[13].X, lstPointInj[13].Y);

                    setPointInj(lstRingInj[14], lstPointInj[14].X, lstPointInj[14].Y);
                    setPointInj(lstRingInj[15], lstPointInj[15].X, lstPointInj[15].Y);
                    setLineInj(lstLineInj[13], lstPointInj[13].X, lstPointInj[13].Y, lstPointInj[14].X, lstPointInj[14].Y);
                    setLineInj(lstLineInj[14], lstPointInj[14].X, lstPointInj[14].Y, lstPointInj[15].X, lstPointInj[15].Y);

                    setPointInj(lstRingInj[16], lstPointInj[16].X, lstPointInj[16].Y);
                    setPointInj(lstRingInj[17], lstPointInj[17].X, lstPointInj[17].Y);
                    setLineInj(lstLineInj[15], lstPointInj[15].X, lstPointInj[15].Y, lstPointInj[16].X, lstPointInj[16].Y);
                    setLineInj(lstLineInj[16], lstPointInj[16].X, lstPointInj[16].Y, lstPointInj[17].X, lstPointInj[17].Y);

                    setPointInj(lstRingInj[18], lstPointInj[18].X, lstPointInj[18].Y);
                    setPointInj(lstRingInj[19], lstPointInj[19].X, lstPointInj[19].Y);
                    setLineInj(lstLineInj[17], lstPointInj[17].X, lstPointInj[17].Y, lstPointInj[18].X, lstPointInj[18].Y);
                    setLineInj(lstLineInj[18], lstPointInj[18].X, lstPointInj[18].Y, lstPointInj[19].X, lstPointInj[19].Y);

                    setPointInj(lstRingInj[20], lstPointInj[20].X, lstPointInj[20].Y);
                    setLineInj(lstLineInj[19], lstPointInj[19].X, lstPointInj[19].Y, lstPointInj[20].X, lstPointInj[20].Y);
                    break;
                default:
                    break;
            }
        }

        private void handleInjPr_72(objUnit obj)
        {
            bPos.Visibility = obj.value == 1 ?
                Visibility.Visible : Visibility.Hidden;
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

            sPanelMain.Height = cvsInjParam.Height + cvsInjMonitor.Height + cvsInjActionSet.Height + 1000;
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
            curve_Time_Pos.HistoryCount = Convert.ToInt32(count);
            curve_Time_Pressure.HistoryCount = Convert.ToInt32(count);
            curve_Time_Speed.HistoryCount = Convert.ToInt32(count);
            curve_Pos_Speed.HistoryCount = Convert.ToInt32(count);
            curve_Pos_Pressure.HistoryCount = Convert.ToInt32(count);
            curve_Pos_Current.HistoryCount = Convert.ToInt32(count);

            HistoryCount.Content = count;

            HistoryCount.BorderBrush = new SolidColorBrush(Color.FromArgb(0xff, 0xd4, 0xd4, 0xd4));
        }

        private void ClearPosCurrent()
        {
            historyData_Pos_Current.Clear();
            curve_Pos_Current.Clear();
        }
        private void ClearPosPressure()
        {
            historyData_Pos_Pressure.Clear();
            curve_Pos_Pressure.Clear();
        }
        private void ClearPosSpeed()
        {
            historyData_Pos_Speed.Clear();
            curve_Pos_Speed.Clear();
        }
        private void ClearTimeSpeed()
        {
            historyData_Time_Speed.Clear();
            curve_Time_Speed.Clear();
        }
        private void ClearTimePressure()
        {
            historyData_Time_Pressure.Clear();
            curve_Time_Pressure.Clear();
        }
        private void ClearTimePos()
        {
            historyData_Time_Pos.Clear();
            curve_Time_Pos.Clear();
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
                ClearTimeSpeed();
                ClearTimePressure();
                ClearTimePos();
                ClearPosSpeed();
                ClearPosPressure();
                ClearPosCurrent();
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
    }
}
