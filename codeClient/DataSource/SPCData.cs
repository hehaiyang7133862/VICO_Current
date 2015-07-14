using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nsDataMgr;
using System.IO;
using System.Xml;
using System.Windows;
using System.Threading;

namespace nsVicoClient
{
    public class SPCData
    {
        public List<SPCVariable> lstSPCVariable = new List<SPCVariable>();

        public int _cycleNumber = 0;
        public int CycleNumber
        {
            set
            {
                _cycleNumber = value;

                if (CycleNumberChanged != null)
                {
                    CycleNumberChanged();
                }
            }
            get
            {
                return _cycleNumber;
            }
        }

        /// <summary>
        /// 周期变化事件
        /// </summary>
        public nullEvent CycleNumberChanged;

        public SPCData()
        {
            loadFromXML();

            CycleNumber = 0;
        }

        private int DataCorrectCount = 0;

        public void ReadNewData()
        {
            if (DataCorrectCount < 3)
            {
                DataCorrectCount++;

                return;
            }

            foreach (SPCVariable su in lstSPCVariable)
            {
                su.readNewValue();
            }

            CycleNumber++;
        }

        private string SavePath()
        {
            string curPath = @"D:\Valmo\SPC\";
            if (Directory.Exists(curPath) == false)
            {
                Directory.CreateDirectory(curPath);
            }

            return curPath + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
        }

        private void loadFromXML()
        {
            string configPath = @"D:\Valmo\SPC\";

            if (Directory.Exists(configPath) == false)
            {
                Directory.CreateDirectory(configPath);
            }

            XmlDocument xmlDoc = new XmlDocument();

            if (!File.Exists(configPath + "SPCConfig.xml"))
            {
                CreateXMl(configPath + "SPCConfig.xml");
                xmlDoc.Load(configPath + "SPCConfig.xml");
            }
            else
            {
                try
                {
                    xmlDoc.Load(configPath + "SPCConfig.xml");
                }
                catch
                {
                    File.Delete(configPath + "SPCConfig.xml");
                    CreateXMl(configPath + "SPCConfig.xml");
                    xmlDoc.Load(configPath + "SPCConfig.xml");
                }
            }

            try
            {
                XmlNode root = xmlDoc.SelectSingleNode("SPCConfig");
                XmlNodeList xnl = (root as XmlElement).SelectNodes("Variable");
                foreach (XmlNode xd in xnl)
                {
                    XmlElement xe_Variable = (XmlElement)xd;

                    SPCVariable su = new SPCVariable();

                    string serialNum = string.Empty;
                    serialNum = (xe_Variable.SelectSingleNode("SerialNum") as XmlElement).InnerText;
                    objUnit obj = valmoWin.dv.getObj(serialNum);

                    if (obj == null)
                    {
                        MessageBox.Show("对象" + serialNum + "不存在！");
                        continue;
                    }
                    su.CurObj = obj;

                    double Usl = 0;
                    try
                    {
                        Usl = Convert.ToDouble((xe_Variable.SelectSingleNode("USL") as XmlElement).InnerText);
                    }
                    catch (System.FormatException e)
                    {
                        MessageBox.Show(e.ToString());
                    }
                    su.USL = Usl;

                    double Lsl = 0;
                    try
                    {
                        Lsl = Convert.ToDouble((xe_Variable.SelectSingleNode("LSL") as XmlElement).InnerText);
                    }
                    catch (System.FormatException e)
                    {
                        MessageBox.Show(e.ToString());
                    }
                    su.LSL = Lsl;

                    int alarmDelay = 0;
                    try
                    {
                        alarmDelay = Convert.ToInt32((xe_Variable.SelectSingleNode("AlarmDelay") as XmlElement).InnerText);
                    }
                    catch (System.FormatException e)
                    {
                        MessageBox.Show(e.ToString());
                    }
                    su.AlarmDelay = alarmDelay;

                    double cpkWarningThreshold = 0;
                    try
                    {
                        cpkWarningThreshold = Convert.ToDouble((xe_Variable.SelectSingleNode("CpkWarningThreshold") as XmlElement).InnerText);
                    }
                    catch (System.FormatException e)
                    {
                        MessageBox.Show(e.ToString());
                    }
                    su.CpkWarningThreshold = cpkWarningThreshold;

                    int sampleSize = 0;
                    try
                    {
                        sampleSize = Convert.ToInt32((xe_Variable.SelectSingleNode("SampleSize") as XmlElement).InnerText);
                    }
                    catch (System.FormatException e)
                    {
                        MessageBox.Show(e.ToString());
                    }
                    su.SampleSize = sampleSize;

                    int samplePeriod = 0;
                    try
                    {
                        samplePeriod = Convert.ToInt32((xe_Variable.SelectSingleNode("SamplePeriod") as XmlElement).InnerText);
                    }
                    catch (System.FormatException e)
                    {
                        MessageBox.Show(e.ToString());
                    }
                    su.SamplePeriod = samplePeriod;

                    bool bSwitch = false;
                    try
                    {
                        bSwitch = Convert.ToBoolean((xe_Variable.SelectSingleNode("Switch") as XmlElement).InnerText);
                    }
                    catch (System.FormatException e)
                    {
                        MessageBox.Show(e.ToString());
                    }
                    su.Switch = bSwitch;

                    if (su.Switch == true)
                    {
                        lstSPCVariable.Add(su);
                    }
                     
                }
            }
            catch
            {
                MessageBox.Show("SPC配置文件格式有误，请检查！");
            }
        }

        private void CreateXMl(string Path)
        {
            XmlTextWriter xtw = new XmlTextWriter(Path, Encoding.UTF8);
            xtw.WriteStartDocument();
            xtw.WriteStartElement("SPCConfig");
            xtw.WriteEndElement();
            xtw.WriteEndDocument();
            xtw.Close();
        }

        public void SPCSave()
        {
            string loadPath = @"D:\Valmo\SPC\SPCConfig.xml";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(loadPath);

            try
            {
                XmlNode root = xmlDoc.SelectSingleNode("SPCConfig");
                XmlNodeList xnl = (root as XmlElement).SelectNodes("Variable");

                foreach (SPCVariable spcVariable in lstSPCVariable)
                {
                    bool bIsVariableExist = false;

                    foreach (XmlNode xd in xnl)
                    {
                        XmlNode xd_serialNum = (xd as XmlElement).SelectSingleNode("SerialNum");
                        if ((xd_serialNum as XmlElement).InnerText == spcVariable.CurObj.serialNum)
                        {
                            bIsVariableExist = true;

                            ((xd as XmlElement).SelectSingleNode("USL") as XmlElement).InnerText = spcVariable.USL.ToString();
                            ((xd as XmlElement).SelectSingleNode("LSL") as XmlElement).InnerText = spcVariable.LSL.ToString();
                            ((xd as XmlElement).SelectSingleNode("AlarmDelay") as XmlElement).InnerText = spcVariable.AlarmDelay.ToString();
                            ((xd as XmlElement).SelectSingleNode("CpkWarningThreshold") as XmlElement).InnerText = spcVariable.CpkWarningThreshold.ToString();
                            ((xd as XmlElement).SelectSingleNode("SampleSize") as XmlElement).InnerText = spcVariable.SampleSize.ToString();
                            ((xd as XmlElement).SelectSingleNode("SamplePeriod") as XmlElement).InnerText = spcVariable.SamplePeriod.ToString();
                            ((xd as XmlElement).SelectSingleNode("Switch") as XmlElement).InnerText = spcVariable.Switch.ToString();
                        }
                    }

                    if (bIsVariableExist == false)
                    {
                        XmlElement xe_Variable = xmlDoc.CreateElement("Variable");
                        root.AppendChild(xe_Variable);
                        XmlElement xe_SerialNum = xmlDoc.CreateElement("SerialNum");
                        xe_SerialNum.InnerText = spcVariable.CurObj.serialNum;
                        xe_Variable.AppendChild(xe_SerialNum);
                        XmlElement xe_USL = xmlDoc.CreateElement("USL");
                        xe_USL.InnerText = spcVariable.USL.ToString();
                        xe_Variable.AppendChild(xe_USL);
                        XmlElement xe_LSL = xmlDoc.CreateElement("LSL");
                        xe_LSL.InnerText = spcVariable.LSL.ToString();
                        xe_Variable.AppendChild(xe_LSL);
                        XmlElement xe_AlarmDelay = xmlDoc.CreateElement("AlarmDelay");
                        xe_AlarmDelay.InnerText = spcVariable.AlarmDelay.ToString();
                        xe_Variable.AppendChild(xe_AlarmDelay);
                        XmlElement xe_CpkWarningThreshold = xmlDoc.CreateElement("CpkWarningThreshold");
                        xe_CpkWarningThreshold.InnerText = spcVariable.CpkWarningThreshold.ToString();
                        xe_Variable.AppendChild(xe_CpkWarningThreshold);
                        XmlElement xe_SampleSize = xmlDoc.CreateElement("SampleSize");
                        xe_SampleSize.InnerText = spcVariable.SampleSize.ToString();
                        xe_Variable.AppendChild(xe_SampleSize);
                        XmlElement xe_SamplePeriod = xmlDoc.CreateElement("SamplePeriod");
                        xe_SamplePeriod.InnerText = spcVariable.SamplePeriod.ToString();
                        xe_Variable.AppendChild(xe_SamplePeriod);
                        XmlElement xe_Switch = xmlDoc.CreateElement("Switch");
                        xe_Switch.InnerText = spcVariable.Switch.ToString();
                        xe_Variable.AppendChild(xe_Switch);
                    }
                }

                xmlDoc.Save(loadPath);
            }
            catch
            {

            }
        }
    }

    public class SPCVariable
    {
        private static double A2 = 0;
        private static double D4 = 0;
        private static double D3 = 0;
        private static double d2 = 0;

        private double cycleNum = 0;
        public bool Switch = false;
        private objUnit _obj;
        /// <summary>
        /// 设置或获取对象
        /// </summary>
        public objUnit CurObj
        {
            set
            {
                _obj = value;

                this.Discription = valmoWin.dv.getCurDis(_obj.serialNum);
                this.Unit = _obj.unit;
            }
            get
            {
                return _obj;
            }
        }
        private string _discription;
        /// <summary>
        /// 设置或获取参数名称
        /// </summary>
        public string Discription
        {
            set
            {
                _discription = value;
            }
            get
            {
                return _discription;
            }
        }
        private string _unit;
        /// <summary>
        /// 设置或获取参数单位
        /// </summary>
        public string Unit
        {
            set
            {
                _unit = value;
            }
            get
            {
                return _unit;
            }
        }

        private int _alarmDealy;
        /// <summary>
        /// 设置或获取报警延迟
        /// </summary>
        public int AlarmDelay
        {
            set
            {
                _alarmDealy = value;
            }
            get
            {
                return _alarmDealy;
            }
        }
        private int _sampleSize;
        /// <summary>
        /// 设置或获取样本大小
        /// </summary>
        public int SampleSize
        {
            set
            {
                if ((value < 2) || (value > 25))
                {
                    MessageBox.Show("样本大小设置不正确");
                    return;
                }
                else
                {
                    _sampleSize = value;

                    switch (_sampleSize)
                    {
                        case 2:
                            {
                                A2 = 1.88;
                                D4 = 3.27;
                                D3 = 0;
                                d2 = 1.13;
                            }
                            break;
                        case 3:
                            {
                                A2 = 1.02;
                                D4 = 2.57;
                                D3 = 0;
                                d2 = 1.69;
                            }
                            break;
                        case 4:
                            {
                                A2 = 0.73;
                                D4 = 2.28;
                                D3 = 0;
                                d2 = 2.06;
                            }
                            break;
                        case 5:
                            {
                                A2 = 0.58;
                                D4 = 2.11;
                                D3 = 0;
                                d2 = 2.33;
                            }
                            break;
                        case 6:
                            {
                                A2 = 0.48;
                                D4 = 2.00;
                                D3 = 0;
                                d2 = 2.53;
                            }
                            break;
                        case 7:
                            {
                                A2 = 0.42;
                                D4 = 1.92;
                                D3 = 0.08;
                                d2 = 2.7;
                            }
                            break;
                        case 8:
                            {
                                A2 = 0.37;
                                D4 = 1.86;
                                D3 = 0.14;
                                d2 = 2.85;
                            }
                            break;
                        case 9:
                            {
                                A2 = 0.34;
                                D4 = 1.82;
                                D3 = 0.18;
                                d2 = 2.97;
                            }
                            break;
                        case 10:
                            {
                                A2 = 0.31;
                                D4 = 1.78;
                                D3 = 0.22;
                                d2 = 3.08;
                            }
                            break;
                        case 11:
                            {
                                A2 = 0.285;
                                D4 = 1.174;
                                D3 = 0.26;
                                d2 = 3.17;
                            }
                            break;
                        case 12:
                            {
                                A2 = 0.266;
                                D4 = 1.72;
                                D3 = 0.28;
                                d2 = 3.26;
                            }
                            break;
                        case 13:
                            {
                                A2 = 0.25;
                                D4 = 1.70;
                                D3 = 0.31;
                                d2 = 3.34;
                            }
                            break;
                        case 14:
                            {
                                A2 = 0.235;
                                D4 = 1.67;
                                D3 = 0.33;
                                d2 = 3.40;
                            }
                            break;
                        case 15:
                            {
                                A2 = 0.22;
                                D4 = 1.65;
                                D3 = 0.35;
                                d2 = 3.47;
                            }
                            break;
                        case 16:
                            {
                                A2 = 0.21;
                                D4 = 1.64;
                                D3 = 0.36;
                                d2 = 3.53;
                            }
                            break;
                        case 17:
                            {
                                A2 = 0.20;
                                D4 = 1.62;
                                D3 = 0.38;
                                d2 = 3.59;
                            }
                            break;
                        case 18:
                            {
                                A2 = 0.19;
                                D4 = 1.61;
                                D3 = 0.39;
                                d2 = 3.64;
                            }
                            break;
                        case 19:
                            {
                                A2 = 0.19;
                                D4 = 1.60;
                                D3 = 0.40;
                                d2 = 3.69;
                            }
                            break;
                        case 20:
                            {
                                A2 = 0.18;
                                D4 = 1.585;
                                D3 = 0.415;
                                d2 = 3.735;
                            }
                            break;
                        case 21:
                            {
                                A2 = 0.17;
                                D4 = 1.575;
                                D3 = 0.425;
                                d2 = 3.78;
                            }
                            break;
                        case 22:
                            {
                                A2 = 0.17;
                                D4 = 1.57;
                                D3 = 0.43;
                                d2 = 3.82;
                            }
                            break;
                        case 23:
                            {
                                A2 = 0.16;
                                D4 = 1.56;
                                D3 = 0.44;
                                d2 = 3.86;
                            }
                            break;
                        case 24:
                            {
                                A2 = 0.16;
                                D4 = 1.55;
                                D3 = 0.45;
                                d2 = 3.90;
                            }
                            break;
                        case 25:
                            {
                                A2 = 0.15;
                                D4 = 1.54;
                                D3 = 0.46;
                                d2 = 3.93;
                            }
                            break;
                        default:
                            {
                                A2 = 0;
                                D4 = 0;
                                D3 = 0;
                                d2 = 0;
                            }
                            break;
                    }
                }
            }
            get
            {
                return _sampleSize;
            }
        }
        private int _samplePeriod;
        /// <summary>
        /// 设置或获取取样间隔
        /// </summary>
        public int SamplePeriod
        {
            set
            {
                _samplePeriod = value;
            }
            get
            {
                return _samplePeriod;
            }
        }
        private double _cpkWarningThreshold;
        /// <summary>
        /// 设置或获取Cpk报警阀值
        /// </summary>
        public double CpkWarningThreshold
        {
            set
            {
                _cpkWarningThreshold = value;

               
            }
            get
            {
                return _cpkWarningThreshold;
            }
        }
        private double _Lsl;
        /// <summary>
        /// 设置或获取产品规格下限
        /// </summary>
        public double LSL
        {
            set
            {
                _Lsl = value;


                if (LSLUpdate != null)
                {
                    LSLUpdate();
                }
            }
            get
            {
                return _Lsl;
            }
        }
        private double _Usl;
        /// <summary>
        /// 设置或获取产品规格上限
        /// </summary>
        public double USL
        {
            set
            {
                _Usl = value;

                if (USLUpdate != null)
                {
                    USLUpdate();
                }
            }
            get
            {
                return _Usl;
            }
        }
        /// <summary>
        /// CPK连续超限数
        /// </summary>
        private int CpkOutControlCount = 0;
        private int _SPCState = 0;
        /// <summary>
        /// 设置或获取SPC状态
        /// </summary>
        public int SPCState
        {
            set
            {
                if ((value >= 0) && (value <= 2))
                    _SPCState = value;

                if (SPCStateChanged != null)
                {
                    SPCStateChanged();
                }
            }
            get
            {
                return _SPCState;
            }
        }
        private int _statisticsSampleCount = 40;
        /// <summary>
        /// 设置或获取图表统计的样本数
        /// </summary>
        public int StatisticsSampleCount
        {
            set
            {
                _statisticsSampleCount = value;
            }
            get
            {
                return _statisticsSampleCount;
            }
        }
        public double MaxValue(int count)
        {
            double Max = 0;
            if (historyRawData.Count == 0)
            {
                return Max;
            }
            int startIndex = 0;
            if (count > historyRawData.Count)
            {
                startIndex = 0;
            }
            else
            {
                startIndex = historyRawData.Count - count;
            }

            Max = historyRawData[startIndex];
            for (int i = startIndex; i < historyRawData.Count; i++)
            {
                if (historyRawData[i] > Max)
                {
                    Max = historyRawData[i];
                }
            }

            return Max;
        }
        public double MinValue(int count)
        {
            double Min = 0;
            if (historyRawData.Count == 0)
            {
                return Min;
            }

            int startIndex = 0;
            if (count > historyRawData.Count)
            {
                startIndex = 0;
            }
            else
            {
                startIndex = historyRawData.Count - count;
            }

            Min = historyRawData[startIndex];
            for (int i = startIndex; i < historyRawData.Count; i++)
            {
                if (historyRawData[i] < Min)
                {
                    Min = historyRawData[i];
                }
            }

            return Min;
        }
        public double Average(int count)
        {
            double Average = 0;
            if (historyRawData.Count == 0)
            {
                return Average;
            }

            int startIndex = 0;
            if (count > historyRawData.Count)
            {
                startIndex = 0;
            }
            else
            {
                startIndex = historyRawData.Count - count;
            }

            double tmp = 0;
            for (int i = startIndex; i < historyRawData.Count; i++)
            {
                tmp += historyRawData[i];
            }
            Average = tmp / (historyRawData.Count - startIndex);
            return Average;
        }
        private double _curValue;
        /// <summary>
        /// 获取或设置参数当前值
        /// </summary>
        public double CurValue
        {
            set
            {
                cycleNum++;
                _curValue = value;

                historyRawData.Add(_curValue);
                if (historyRawData.Count > 1000)
                    historyRawData.RemoveAt(0);

                if (SamplePeriod > 0)
                {
                    if (((cycleNum % SamplePeriod) == 0) && (cycleNum / SamplePeriod) > 1)
                    {
                        if (Switch == true)
                        {
                            ReSample();
                        }
                    }
                }

                if (CurValueChanged != null)
                {
                    CurValueChanged();
                }
            }
            get
            {
                return _curValue;
            }
        }
        private SPCSample _curSample;
        /// <summary>
        /// 设置或获取当前样本呢
        /// </summary>
        public SPCSample CurSample
        {
            set
            {
                _curSample = value;

                if (_curSample.Average == 0)
                    return;

                historySample.Add(_curSample);
                while (historySample.Count > _statisticsSampleCount)
                {
                    historySample.RemoveAt(0);
                }

                EndTime = _curSample.SamplingTime;
                StartTime = historySample[0].SamplingTime;

                if (_curSample.Cpk < CpkWarningThreshold)
                {
                    CpkOutControlCount++;

                    if (CpkOutControlCount > AlarmDelay)
                    {
                        if (SPCState != 2)
                        {
                            SPCState = 2;
                        }
                    }
                }
                else
                {
                    SPCState = 1;
                    CpkOutControlCount = 0;
                }
            }
            get
            {
                return _curSample;
            }
        }
        private double _Average2;
        /// <summary>
        /// 设置或获取参数平均值的平均值
        /// </summary>
        public double Average2
        {
            set
            {
                _Average2 = value;
            }
            get
            {
                return _Average2;
            }
        }
        private double _AverageRange;
        public double AverageRange
        {
            set
            {
                _AverageRange = value;
            }
            get
            {
                return _AverageRange;
            }
        }
        private double _uclx;
        /// <summary>
        /// 设置或获取UCLx
        /// </summary>
        public double UCLx
        {
            set
            {
                _uclx = value;
            }
            get
            {
                return _uclx;
            }
        }
        private double _lclx;
        /// <summary>
        /// 设置获取LCLx
        /// </summary>
        public double LCLx
        {
            set
            {
                _lclx = value;
            }
            get
            {
                return _lclx;
            }
        }
        private double _uclr;
        public double Uclr
        {
            set
            {
                _uclr = value;
            }
            get
            {
                return _uclr;
            }
        }
        private double _lclr;
        public double Lclr
        {
            set
            {
                _lclr = value;
            }
            get
            {
                return _lclr;
            }
        }
        private DateTime _startTime = DateTime.Now;
        /// <summary>
        /// 设置或获取最早样本取样时间
        /// </summary>
        public DateTime StartTime
        {
            set
            {
                _startTime = value;
            }
            get
            {
                return _startTime;
            }
        }
        private DateTime _endTime = DateTime.Now;
        /// <summary>
        /// 设置或获取最晚样本取样时间
        /// </summary>
        public DateTime EndTime
        {
            set
            {
                _endTime = value;
            }
            get
            {
                return _endTime;
            }
        }

        /// <summary>
        /// 历史数据，保存1000组，FIFO
        /// </summary>
        public List<double> historyRawData = new List<double>();
        /// <summary>
        /// 历史样本数据，FIFO
        /// </summary>
        public List<SPCSample> historySample = new List<SPCSample>();

        public nullEvent CurValueChanged;
        public nullEvent SPCStateChanged;
        public nullEvent ReSampleHandle;
        public nullEvent LSLUpdate;
        public nullEvent USLUpdate;

        public SPCVariable()
        { }

        public string readNewValue()
        {
            CurValue = CurObj.vDblNew;

            return CurValue.ToString();
        }

        private void calculateUclx()
        {
            UCLx = _Average2 + AverageRange * A2;
        }
        private void calculateLclx()
        {
            LCLx = _Average2 - AverageRange * A2;
        }
        private void calculateUclr()
        {
            Uclr = D4 * _AverageRange;
        }
        private void calculateLclr()
        {
            Lclr = D3 * _AverageRange;
        }

        /// <summary>
        /// 获取样本数据
        /// </summary>
        /// <param name="size">样本大小</param>
        /// <param name="period">取样周期</param>
        /// <returns>样本</returns>
        private double[] ReturnSampleData(int size, int period)
        {
            double[] sampleData = new double[size];

            if (historyRawData.Count > period)
            {
                for (int i = 0; i < size; i++)
                {
                    sampleData[i] = historyRawData[historyRawData.Count - period + i];
                }
            }

            return sampleData;
        }

        public void ReSample()
        {
            double[] sampleData = ReturnSampleData(_sampleSize, _samplePeriod);

            CurSample = new SPCSample(sampleData, _Usl, _Lsl);

            double tmp =0;
            for(int i = 0;i<historySample.Count;i++)
            {
                tmp +=  historySample[i].Average;
            }
            Average2 = tmp /historySample.Count;

            double tmpR = 0;
            for (int i = 0; i < historySample.Count; i++)
            {
                tmpR += historySample[i].Range;
            }
            AverageRange = tmpR / historySample.Count;

            calculateUclx();
            calculateLclx();
            calculateUclr();
            calculateLclr();

            if (ReSampleHandle != null)
            {
                ReSampleHandle();
            }
        }
    }

    public class SPCSample
    {
        public double Average = 0;
        public double Range = 0;
        public double Cp = 0;
        public double Ca = 0;
        public double STDEV;
        public double Cpk;
        public DateTime SamplingTime = DateTime.Now;

        public SPCSample(double[] rawData, double USL, double LSL)
        {
            Average = CalculateAverage(rawData);
            Range = CalculateRange(rawData);
            STDEV = CalculateSTDEV(rawData, Average);
            Cp = CalculateCp(STDEV, USL, LSL);
            Ca = CalculateCa(Average, USL, LSL);
            Cpk = CalculateCpk(Cp, Ca);
            SamplingTime = DateTime.Now;
        }

        private double CalculateAverage(double[] inputData)
        {
            double tmp = 0;
            int i = 0;

            foreach (double d in inputData)
            {
                tmp += d;
                i++;
            }

            return tmp / i;
        }
        private double CalculateRange(double[] inputdata)
        {
            double max, min;

            max = min = inputdata[0];

            for (int i = 1; i < inputdata.Length; i++)
            {
                if (inputdata[i] > max)
                    max = inputdata[i];
                else if (inputdata[i] < min)
                    min = inputdata[i];
            }

            return max - min;
        }
        private double CalculateSTDEV(double[] inputData, double average)
        {
            double tmp = 0;
            int n = 0;
            foreach (double d in inputData)
            {
                tmp += Math.Pow(d - average, 2);
                n++;
            }

            return Math.Sqrt(tmp / (n - 1));
        }
        private double CalculateCp(double STDEV,double USL,double LSL)
        {
            if (STDEV > 0)
                return (USL - LSL) / 6 / STDEV;
            else
                return 0;
        }
        private double CalculateCa(double in_average, double USL, double LSL)
        {
            return (in_average - (USL + LSL) / 2) / ((USL - LSL) / 2);
        }
        private double CalculateCpk(double cp, double ca)
        {
            return cp * (1 - Math.Abs(ca));
        }
    }

    public class RawData
    {
 
    }
}
