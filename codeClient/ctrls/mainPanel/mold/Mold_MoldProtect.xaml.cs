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
using System.Globalization;
using nsDataMgr;

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// Mold_MoldProtect.xaml 的交互逻辑
    /// </summary>
    public partial class Mold_MoldProtect : UserControl
    {
        private bool _bIsMouseMove = false;
        /// <summary>
        /// 模保开始位置
        /// </summary>
        private double PosMoldProtectStart = 2;
        /// <summary>
        /// 模保结束位置
        /// </summary>
        private double PosMoldProtectEnd = 0;
        /// <summary>
        /// 模保第二段位置
        /// </summary>
        private double PosMoldProtectStep2 = 1;
        /// <summary>
        /// 模保第一段扭力偏差限幅
        /// </summary>
        private double CurrentOffsetLimit_Step1;
        /// <summary>
        /// 模保第二段扭力偏差限幅
        /// </summary>
        private double CurrentOffsetLimit_Step2;
        /// <summary>
        /// 模保第一段速度偏差限幅
        /// </summary>
        private double SpeedOffsetLimit_Step1;
        /// <summary>
        /// 模保第二段速度偏差限幅
        /// </summary>
        private double SpeedOffsetLimit_Step2;
        /// <summary>
        /// 模保样本位置速度曲线数据地址
        /// </summary>
        private uint blockAddr_Position_Speed_Pro = 0;
        /// <summary>
        /// 模保样本电流吨位曲线数据地址
        /// </summary>
        private uint blockAddr_Current_Tonnage_Pro = 0;
        /// <summary>
        /// 模保偏差位置速度曲线数据地址
        /// </summary>
        private uint blockAddr_Position_Speed_Offset = 0;
        /// <summary>
        /// 模保偏差电流吨位曲线数据地址
        /// </summary>
        private uint blockAddr_Current_Tonnage_Offset = 0;
        /// <summary>
        /// 模保吨位
        /// </summary>
        private List<int> lst_Tonnage_MoldClamp = new List<int>();
        /// <summary>
        /// 模保时间
        /// </summary>
        private List<int> lst_Time_MoldClamp = new List<int>();
        /// <summary>
        /// 样本位置
        /// </summary>
        private List<int> lst_Position_Pro = new List<int>();
        /// <summary>
        /// 样本时间
        /// </summary>
        private List<int> lst_Speed_Pro = new List<int>();
        /// <summary>
        /// 样本电流
        /// </summary>
        private List<int> lst_Current_Pro = new List<int>();
        /// <summary>
        /// 样本吨位
        /// </summary>
        private List<int> lst_Tonnage_Pro = new List<int>();
        /// <summary>
        /// 偏差位置
        /// </summary>
        private List<int> lst_Position_Offset = new List<int>();
        /// <summary>
        /// 偏差速度
        /// </summary>
        private List<int> lst_Speed_Offset = new List<int>();
        /// <summary>
        /// 偏差电流
        /// </summary>
        private List<int> lst_Current_Offset = new List<int>();
        /// <summary>
        /// 偏差吨位
        /// </summary>
        private List<int> lst_Tonnage_Offset = new List<int>();

        public Mold_MoldProtect()
        {
            InitializeComponent();

            valmoWin.dv.MldPr[531].addHandle(MoldProtectState);

            valmoWin.dv.PrdPr[261].addHandle(MoldProtectStart);

            valmoWin.dv.MldPr[553].addHandle(setMoldProtectStart);
            valmoWin.dv.MldPr[554].addHandle(setMoldProtectStep2);
            valmoWin.dv.MldPr[41].addHandle(setMoldProtectEnd);

            valmoWin.dv.MldPr[541].addHandle(setCurrentOffsetlimit_Step1);
            valmoWin.dv.MldPr[542].addHandle(setCurrentOffsetlimit_Step2);
            valmoWin.dv.MldPr[546].addHandle(setSpeedOffsetlimit_Step1);
            valmoWin.dv.MldPr[547].addHandle(setSpeedOffsetlimit_Step2);
            valmoWin.dv.MldPr[551].addHandle(setPositionOffsetLimit);
            valmoWin.dv.MldPr[232].addHandle(moldProtect_Max_Speed);
            valmoWin.lstStartUpInit.Add(startUpInit);
        }

        /// <summary>
        /// 曲线数据地址初始化
        /// </summary>
        private void startUpInit()
        {
            blockAddr_Position_Speed_Pro = (uint)valmoWin.dv.PrdPr[1006].valueNew;
            blockAddr_Current_Tonnage_Pro = (uint)valmoWin.dv.PrdPr[1057].valueNew;
            blockAddr_Position_Speed_Offset = (uint)valmoWin.dv.PrdPr[1109].valueNew;
            blockAddr_Current_Tonnage_Offset = (uint)valmoWin.dv.PrdPr[1159].valueNew;
        }

        /// <summary>
        /// 设置移模速度曲线，速度轴坐标
        /// </summary>
        /// <param name="obj">Mld232</param>
        private void moldProtect_Max_Speed(objUnit obj)
        {
            double Max_MoldProtect_Speed = obj.vDbl > 0 ? obj.vDbl : 0;

        }

        /// <summary>
        /// 设置模保起始位置
        /// </summary>
        /// <param name="obj">Mld553</param>
        private void setMoldProtectStart(objUnit obj)
        {
            PosMoldProtectStart = obj.vDbl;

            refush_CurrentOffset_limitLineX(PosMoldProtectStart, PosMoldProtectEnd, PosMoldProtectStep2);
            refush_SpeedOffset_limitLineX(PosMoldProtectStart, PosMoldProtectEnd, PosMoldProtectStep2);
            refush_PositionPro_Staff();
        }
        /// <summary>
        /// 设置模保第二段位置
        /// </summary>
        /// <param name="obj">Mld554</param>
        private void setMoldProtectStep2(objUnit obj)
        {
            PosMoldProtectStep2 = obj.vDbl;

            refush_CurrentOffset_limitLineX(PosMoldProtectStart, PosMoldProtectEnd, PosMoldProtectStep2);
            refush_SpeedOffset_limitLineX(PosMoldProtectStart, PosMoldProtectEnd, PosMoldProtectStep2);
        }
        /// <summary>
        /// 设置模保结束位置
        /// </summary>
        /// <param name="obj">Mld041</param>
        private void setMoldProtectEnd(objUnit obj)
        {
            PosMoldProtectEnd = obj.vDbl;

            refush_CurrentOffset_limitLineX(PosMoldProtectStart, PosMoldProtectEnd, PosMoldProtectStep2);
            refush_SpeedOffset_limitLineX(PosMoldProtectStart, PosMoldProtectEnd, PosMoldProtectStep2);
            refush_PositionPro_Staff();
        }
        /// <summary>
        /// 设置模保第一段扭力偏差限幅
        /// </summary>
        /// <param name="obj">Mld541</param>
        private void setCurrentOffsetlimit_Step1(objUnit obj)
        {
            CurrentOffsetLimit_Step1 = obj.vDbl;

            refush_CurrentOffset_limmitLineY(CurrentOffsetLimit_Step1, CurrentOffsetLimit_Step2);
        }
        /// <summary>
        /// 设置模保第二段扭力偏差限幅
        /// </summary>
        /// <param name="obj">Mld542</param>
        private void setCurrentOffsetlimit_Step2(objUnit obj)
        {
            CurrentOffsetLimit_Step2 = obj.vDbl;

            refush_CurrentOffset_limmitLineY(CurrentOffsetLimit_Step1, CurrentOffsetLimit_Step2);
        }
        /// <summary>
        /// 设置模保第一段速度偏差限幅
        /// </summary>
        /// <param name="obj">Mld546</param>
        private void setSpeedOffsetlimit_Step1(objUnit obj)
        {
            SpeedOffsetLimit_Step1 = obj.vDbl;

            refush_SpeedOffset_limmitLineY(SpeedOffsetLimit_Step1, SpeedOffsetLimit_Step2);
        }
        /// <summary>
        /// 设置模保第二段速度偏差限幅
        /// </summary>
        /// <param name="obj">Mld547</param>
        private void setSpeedOffsetlimit_Step2(objUnit obj)
        {
            SpeedOffsetLimit_Step2 = obj.vDbl;

            refush_SpeedOffset_limmitLineY(SpeedOffsetLimit_Step1, SpeedOffsetLimit_Step2);
        }
        /// <summary>
        /// 设置模保位置偏差限幅
        /// </summary>
        /// <param name="obj">Mld551</param>
        private void setPositionOffsetLimit(objUnit obj)
        {
            PositionOffsetLimit = obj.vDbl > 0 ? obj.vDbl : 0;

            PositionUpLimit.Content = "+" + PositionOffsetLimit;
            PositionDownLimit.Content = "-" + PositionOffsetLimit;
        }

        private void refush_CurrentOffset_limmitLineY(double limit1, double limit2)
        {
            double forcelimit = limit1 > limit2 ? limit1 : limit2;

            currentUpLimit.Content = "+" + forcelimit;
            currentDownLimit.Content = "-" + forcelimit;

            if (forcelimit > 0)
            {
                Canvas.SetTop(pForceUp1, -CurrentOffsetLimit_Step1 / forcelimit * 82 - 5);
                Canvas.SetTop(pForceUp2, -CurrentOffsetLimit_Step1 / forcelimit * 82 - 5);
                Canvas.SetTop(pForceUp3, -CurrentOffsetLimit_Step2 / forcelimit * 82 - 5);
                Canvas.SetTop(pForceUp4, -CurrentOffsetLimit_Step2 / forcelimit * 82 - 5);
                Canvas.SetTop(pForceDown1, CurrentOffsetLimit_Step1 / forcelimit * 82 - 4);
                Canvas.SetTop(pForceDown2, CurrentOffsetLimit_Step1 / forcelimit * 82 - 4);
                Canvas.SetTop(pForceDown3, CurrentOffsetLimit_Step2 / forcelimit * 82 - 4);
                Canvas.SetTop(pForceDown4, CurrentOffsetLimit_Step2 / forcelimit * 82 - 4);

                lForceUp1.Y1 = -CurrentOffsetLimit_Step1 / forcelimit * 82;
                lForceUp1.Y2 = -CurrentOffsetLimit_Step1 / forcelimit * 82;
                lForceUp2.Y1 = -CurrentOffsetLimit_Step1 / forcelimit * 82;
                lForceUp2.Y2 = -CurrentOffsetLimit_Step2 / forcelimit * 82;
                lForceUp3.Y1 = -CurrentOffsetLimit_Step2 / forcelimit * 82;
                lForceUp3.Y2 = -CurrentOffsetLimit_Step2 / forcelimit * 82;
                lForceDown1.Y1 = CurrentOffsetLimit_Step1 / forcelimit * 82;
                lForceDown1.Y2 = CurrentOffsetLimit_Step1 / forcelimit * 82;
                lForceDown2.Y1 = CurrentOffsetLimit_Step1 / forcelimit * 82;
                lForceDown2.Y2 = CurrentOffsetLimit_Step2 / forcelimit * 82;
                lForceDown3.Y1 = CurrentOffsetLimit_Step2 / forcelimit * 82;
                lForceDown3.Y2 = CurrentOffsetLimit_Step2 / forcelimit * 82;
            }
        }

        private void refush_SpeedOffset_limmitLineY(double limit1, double limit2)
        {
            double Speedlimit = limit1 > limit2 ? limit1 : limit2;

            SpeedUpLimit.Content = "+" + Speedlimit;
            SpeedDownLimit.Content = "-" + Speedlimit;

            if (Speedlimit > 0)
            {
                Canvas.SetTop(pSpeedUp1, -SpeedOffsetLimit_Step1 / Speedlimit * 82 - 5);
                Canvas.SetTop(pSpeedUp2, -SpeedOffsetLimit_Step1 / Speedlimit * 82 - 5);
                Canvas.SetTop(pSpeedUp3, -SpeedOffsetLimit_Step2 / Speedlimit * 82 - 5);
                Canvas.SetTop(pSpeedUp4, -SpeedOffsetLimit_Step2 / Speedlimit * 82 - 5);
                Canvas.SetTop(pSpeedDown1, SpeedOffsetLimit_Step1 / Speedlimit * 82 - 4);
                Canvas.SetTop(pSpeedDown2, SpeedOffsetLimit_Step1 / Speedlimit * 82 - 4);
                Canvas.SetTop(pSpeedDown3, SpeedOffsetLimit_Step2 / Speedlimit * 82 - 4);
                Canvas.SetTop(pSpeedDown4, SpeedOffsetLimit_Step2 / Speedlimit * 82 - 4);

                lSpeedUp1.Y1 = -SpeedOffsetLimit_Step1 / Speedlimit * 82;
                lSpeedUp1.Y2 = -SpeedOffsetLimit_Step1 / Speedlimit * 82;
                lSpeedUp2.Y1 = -SpeedOffsetLimit_Step1 / Speedlimit * 82;
                lSpeedUp2.Y2 = -SpeedOffsetLimit_Step2 / Speedlimit * 82;
                lSpeedUp3.Y1 = -SpeedOffsetLimit_Step2 / Speedlimit * 82;
                lSpeedUp3.Y2 = -SpeedOffsetLimit_Step2 / Speedlimit * 82;
                lSpeedDown1.Y1 = SpeedOffsetLimit_Step1 / Speedlimit * 82;
                lSpeedDown1.Y2 = SpeedOffsetLimit_Step1 / Speedlimit * 82;
                lSpeedDown2.Y1 = SpeedOffsetLimit_Step1 / Speedlimit * 82;
                lSpeedDown2.Y2 = SpeedOffsetLimit_Step2 / Speedlimit * 82;
                lSpeedDown3.Y1 = SpeedOffsetLimit_Step2 / Speedlimit * 82;
                lSpeedDown3.Y2 = SpeedOffsetLimit_Step2 / Speedlimit * 82;
            }
        }
        /// <summary>
        /// 刷新样本位置
        /// </summary>
        private void refush_PositionPro_Staff()
        {
            refush_CurvePro(PosMoldProtectStart, PosMoldProtectEnd);
            refush_CurveCurrentOffset(PosMoldProtectStart, PosMoldProtectEnd);
            refush_CurveSpeedOffset(PosMoldProtectStart, PosMoldProtectEnd);
            refush_CurvePositionOffset(PosMoldProtectStart, PosMoldProtectEnd);
        }
        private void refush_CurrentOffset_limitLineX(double PosStart, double PosEnd, double PosStep2)
        {
            double temp = Math.Abs(PosStart - PosEnd);

            if (temp > 0)
            {
                Canvas.SetLeft(pForceUp1, -4);
                Canvas.SetLeft(pForceUp2, (PosStart - PosStep2) / temp * 299 - 4);
                Canvas.SetLeft(pForceUp3, (PosStart - PosStep2) / temp * 299 - 4);
                Canvas.SetLeft(pForceUp4, 295);
                Canvas.SetLeft(pForceDown1, -4);
                Canvas.SetLeft(pForceDown2, (PosStart - PosStep2) / temp * 299 - 4);
                Canvas.SetLeft(pForceDown3, (PosStart - PosStep2) / temp * 299 - 4);
                Canvas.SetLeft(pForceDown4, 295);

                lForceUp1.X1 = 0;
                lForceUp1.X2 = (PosStart - PosStep2) / temp * 299;
                lForceUp2.X1 = (PosStart - PosStep2) / temp * 299;
                lForceUp2.X2 = (PosStart - PosStep2) / temp * 299;
                lForceUp3.X1 = (PosStart - PosStep2) / temp * 299;
                lForceUp3.X2 = 299;

                lForceDown1.X1 = 0;
                lForceDown1.X2 = (PosStart - PosStep2) / temp * 299;
                lForceDown2.X1 = (PosStart - PosStep2) / temp * 299;
                lForceDown2.X2 = (PosStart - PosStep2) / temp * 299;
                lForceDown3.X1 = (PosStart - PosStep2) / temp * 299;
                lForceDown3.X2 = 299;

                Canvas.SetLeft(pSpeedUp1, -4);
                Canvas.SetLeft(pSpeedUp2, (PosStart - PosStep2) / temp * 299 - 4);
                Canvas.SetLeft(pSpeedUp3, (PosStart - PosStep2) / temp * 299 - 4);
                Canvas.SetLeft(pSpeedUp4, 299);
                Canvas.SetLeft(pSpeedDown1, -4);
                Canvas.SetLeft(pSpeedDown2, (PosStart - PosStep2) / temp * 299 - 4);
                Canvas.SetLeft(pSpeedDown3, (PosStart - PosStep2) / temp * 299 - 4);
                Canvas.SetLeft(pSpeedDown4, 299);

                lSpeedUp1.X1 = 0;
                lSpeedUp1.X2 = (PosStart - PosStep2) / temp * 299;
                lSpeedUp2.X1 = (PosStart - PosStep2) / temp * 299;
                lSpeedUp2.X2 = (PosStart - PosStep2) / temp * 299;
                lSpeedUp3.X1 = (PosStart - PosStep2) / temp * 299;
                lSpeedUp3.X2 = 299;

                lSpeedDown1.X1 = 0;
                lSpeedDown1.X2 = (PosStart - PosStep2) / temp * 299;
                lSpeedDown2.X1 = (PosStart - PosStep2) / temp * 299;
                lSpeedDown2.X2 = (PosStart - PosStep2) / temp * 299;
                lSpeedDown3.X1 = (PosStart - PosStep2) / temp * 299;
                lSpeedDown3.X2 = 299;
            }
        }
        private void refush_SpeedOffset_limitLineX(double PosStart, double PosEnd, double PosStep2)
        {
            double temp = Math.Abs(PosStart - PosEnd);

        }
        private void refush_PositionOffset_limitLineX(double PosStart, double PosEnd)
        {
            double temp = PosEnd - PosStart;
        }
        /// <summary>
        /// 刷新样本曲线位置坐标
        /// </summary>
        /// <param name="PosStart">样本位置起点</param>
        /// <param name="PosEnd">样本位置终点</param>
        private void refush_CurvePro(double PosStart, double PosEnd)
        {
            double temp = PosEnd - PosStart;

        }
        /// <summary>
        /// 刷新电流偏移曲线位置坐标
        /// </summary>
        /// <param name="PosStart">样本位置起点</param>
        /// <param name="PosEnd">样本位置终点</param>
        private void refush_CurveCurrentOffset(double PosStart, double PosEnd)
        {
            double temp = PosEnd - PosStart;

        }
        /// <summary>
        /// 刷新速度偏移曲线位置坐标
        /// </summary>
        /// <param name="PosStart">样本位置起点</param>
        /// <param name="PosEnd">样本位置终点</param>
        private void refush_CurveSpeedOffset(double PosStart, double PosEnd)
        {
            double temp = PosEnd - PosStart;

        }
        /// <summary>
        /// 刷新位置偏移曲线位置坐标
        /// </summary>
        /// <param name="PosStart">样本位置起点</param>
        /// <param name="PosEnd">样本位置终点</param>
        private void refush_CurvePositionOffset(double PosStart, double PosEnd)
        {
            double temp = PosEnd - PosStart;
        }

        private double PositionOffsetLimit;
        /// <summary>
        /// 模保曲线开始
        /// </summary>
        /// <param name="obj">Prd974</param>
        private void MoldProtectStart(objUnit obj)
        {
            if (valmoWin.dv.MldPr[531].value == 0)
            {
                return;
            }

            int count = obj.value;

            int maxCurrent = valmoWin.dv.MldPr[541].value > valmoWin.dv.MldPr[542].value ? valmoWin.dv.MldPr[541].value : valmoWin.dv.MldPr[542].value;
            int maxSpeed = valmoWin.dv.MldPr[546].value > valmoWin.dv.MldPr[547].value ? valmoWin.dv.MldPr[546].value : valmoWin.dv.MldPr[547].value;
            int maxPos = valmoWin.dv.MldPr[551].value;

            int[] MoldProtectData = new int[count * 4];
            Lasal32.GetData(MoldProtectData, (uint)valmoWin.dv.PrdPr[262].valueNew, count * 16);

            Point[] curveData_Current = new Point[count];
            Point[] curveData_Speed = new Point[count];
            Point[] curveData_Pos = new Point[count];

            for (int i = 0; i < count; i++)
            {
                double current = MoldProtectData[i * 4] * 1.0 / maxCurrent * 5000;
                double speed = MoldProtectData[i * 4 + 1] * 1.0 / maxSpeed * 5000;
                double pos = MoldProtectData[i * 4 + 2] * 1.0 / maxPos * 5000;

                curveData_Current[i] = new Point(i * 10000 / count, 5000 + current);
                curveData_Speed[i] = new Point(i * 10000 / count, 5000 + speed);
                curveData_Pos[i] = new Point(i * 10000 / count, 5000 + pos);

            }

            curveSpeedOffset.refushCurve(curveData_Speed);
            curveCurrentOffset.refushCurve(curveData_Current);
            curvePositionOffset.refushCurve(curveData_Pos);

        }

        private int getHigh_16(int raw)
        {
            int temp = (short)((raw >> 16) & 0xffff);

            if (temp > 10000)
            {
                temp = 10000;
            }

            if (temp < -10000)
            {
                temp = -10000;
            }

            return temp;
        }
        private int getLow_16(int raw)
        {
            int temp = (short)(raw & 0xffff);

            if (temp > 10000)
            {
                temp = 10000;
            }

            if (temp < -10000)
            {
                temp = -10000;
            }

            return temp;
        }
        /// <summary>
        /// 解析数据
        /// </summary>
        /// <param name="rawdata">原始数据</param>
        /// <param name="count">数量</param>
        /// <param name="data1">目标数据1</param>
        /// <param name="data2">目标数据2</param>
        private void DataAnalysis(int[] rawdata, int count, List<int> data1, List<int> data2, int staff1, int staff2)
        {
            int temp;
            data1.Clear();
            data2.Clear();

            for (int i = 0; i < count && i < 30; i++)
            {
                temp = rawdata[i];
                data1.Add(getHigh_16(temp) * 1000 / staff1);
                data2.Add(getLow_16(temp) * 1000 / staff2);
            }
        }
        /// <summary>
        /// 返回曲线的点集
        /// </summary>
        /// <param name="data1">X</param>
        /// <param name="data2">Y</param>
        /// <returns>点集</returns>
        private Point[] getPointlst(List<int> x, List<int> y)
        {
            Point[] Ps;
            int count = 0;

            if (x.Count == y.Count)
            {
                count = x.Count;
                Ps = new Point[count];

                for (int i = 0; i < count; i++)
                {
                    Ps[i] = new Point(x[i], y[i]);
                }
            }
            else
            {
                Ps = new Point[0];
            }

            return Ps;
        }

        /// <summary>
        /// 模具保护状态
        /// </summary>
        /// <param name="obj">Mld531</param>
        public void MoldProtectState(objUnit obj)
        {
            if (obj.value == 0 || obj.value == 1)
                tbl_MoldProtectState.SelectedIndex = obj.value;
        }

        private void ReSample(object sender, MouseButtonEventArgs e)
        {
            if (!valmoWin.dv.checkAccesslevel(valmoWin.dv.MldPr[534].accessLevel) || _bIsMouseMove)
                return;
            valmoWin.dv.MldPr[534].valueNew = 0;
        }
    }
}
