using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.IO;

namespace nsDataMgr
{
    /// <summary>
    /// void intEvent(int value)
    /// </summary>
    /// <param name="value"></param>
    public delegate void intEvent(int value);
    /// <summary>
    /// bool bool_intEvent(int value);
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public delegate int int_intEvent(int value);
    /// <summary>
    /// void nullEvent();
    /// </summary>
    public delegate void nullEvent();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void handleEvent(object sender, EventArgs e);
    /// <summary>
    /// public bool boolEvent();
    /// </summary>
    /// <returns></returns>
    public delegate bool boolEvent();
    /// <summary>
    /// public  void _boolEvent(bool flag);
    /// </summary>
    /// <param name="flag"></param>
    public delegate void _boolEvent(bool flag);
    /// <summary>
    /// public Double DblDblEvent(double value);
    /// </summary>
    /// <returns></returns>
    public delegate Double DblDblEvent(double value);
    /// <summary>
    /// public Double DblEvent(ref int num);
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public delegate Double DblRefIntEvent(ref int num);
    public delegate Double DblEvent();
    /// <summary>
    /// void strEvent(string str);
    /// </summary>
    /// <param name="str"></param>
    public delegate void strEvent(string str);
    /// <summary>
    /// void lstStrEvent(List<string> lstStr);
    /// </summary>
    /// <param name="lstStr"></param>
    public delegate void lstStrEvent(List<string> lstStr);
    /// <summary>
    /// void fileLstEvent(List<FileInfo> fileLst);
    /// </summary>
    /// <param name="fileLst"></param>
    public delegate void fileLstEvent(List<FileInfo> fileLst);
    public delegate void VoidDblEvent(double value);
    /// <summary>
    /// bool confirmEvent();
    /// </summary>
    /// <returns></returns>
    public delegate bool confirmEvent();
    public delegate void export2Excel(DateTime dts, DateTime dte);
    public delegate bool setValueEvent(int objAddr, int dValue);
    public delegate void setPbarValue(double a, double b);
    public delegate bool getValueEvent(int objAddr, ref int dValue);

    public delegate void callBackDebugEvent(Label lb, int value);
    public delegate void callBackObjEvent(objUnit obj);
    public delegate void callBackValueGrpEvent(List<intEvent> handleGrp, int value);
    public delegate void callbackObjLstEvent(List<Label> lbGrp, List<callBackObjEvent> handleGrp, objUnit obj);
    public delegate void callBackLstEvent(uint dwId, int value);
    public delegate objUnit getObjEvent(string ser);

    public class vm
    {
        static FileStream fs = new FileStream("fsOut.vm", FileMode.OpenOrCreate);
        static StreamWriter sw = new StreamWriter(fs);
        public static bool flagPerrorPrint = true;
        public static bool flagDebugPrint = true;

        public static long tm
        {
            get
            {
                return DateTime.Now.Ticks / 10000;
            }
        }
        public static long getTm(string str)
        {
            if (flagDebugPrint)
                Console.WriteLine(str + "\t" + DateTime.Now.Ticks / 10000);
            return DateTime.Now.Ticks / 10000;
        }
        public static long getTm()
        {
            long ticks = DateTime.Now.Ticks / 10000 % 10000;
            if (flagDebugPrint)
                Console.WriteLine(ticks);
            return ticks;
        }
        public static long getTmH(string str)
        {
            if (flagDebugPrint)
                Console.WriteLine(str + "\t" + DateTime.Now.Ticks / 100);
            return DateTime.Now.Ticks / 100;
        }
        public static long getMilSecTm()
        {
            return DateTime.Now.Ticks / 10000 % 10000;
        }
        static long lastTm = 0;
        public static void printMilSecTm()
        {
            if (flagDebugPrint)
                Console.WriteLine((DateTime.Now.Ticks / 1000 % 10000 - lastTm).ToString());
            lastTm = DateTime.Now.Ticks / 1000 % 10000;
        }
        public static void printMilSecTm(string str)
        {

            if (flagDebugPrint)
                Console.WriteLine(str + (DateTime.Now.Ticks / 1000 % 10000 - lastTm).ToString());
            lastTm = DateTime.Now.Ticks / 1000 % 10000;
        }
        public static void getTmH()
        {
            if (flagDebugPrint)
                Console.WriteLine(DateTime.Now.Ticks / 100);
        }
        public static void perror(string str)
        {
            if (flagPerrorPrint)
                Console.WriteLine("error : " + str);
        }
        public static void debug(string str)
        {
            if (flagDebugPrint)
                Console.WriteLine("debug : " + str);
        }
        public static void printLn(string str)
        {
            if (flagDebugPrint)
                Console.WriteLine(str);
        }
        public static void print(string str)
        {
            if (flagDebugPrint)
                Console.Write(str);
        }
        public static void ptTmp(string str)
        {
            if (flagDebugPrint)
                Console.WriteLine(str + "\t");
        }
        public static void iprPrint(string str)
        {
            if (flagDebugPrint)
                Console.WriteLine(str);
        }
        public static void iprPrint(string format, params object[] arg)
        {
            if (flagDebugPrint)
                Console.WriteLine(format, arg);
        }
        public static void printBinary(int value)
        {
            if (flagDebugPrint)
                Console.Write("{0}:{1:X8}\t", value, value);
            for (int i = 31; i >= 0; i--)
            {
                if (flagDebugPrint)
                    Console.Write(((value >> i) & 0x01));
                if (i % 8 == 0)
                    if (flagDebugPrint)
                        Console.Write("\t");

            }
            if (flagDebugPrint)
                Console.WriteLine();
        }
        public static void testInitTmStart(string str)
        {
            if (flagDebugPrint)
                Console.WriteLine("===>\t" + str + "\t start <===\t" + DateTime.Now.Ticks);
        }
        public static void testInitTmEnd(string str)
        {
            if (flagDebugPrint)
                Console.WriteLine("===>\t" + str + "\t end <===\t" + DateTime.Now.Ticks);
        }
        public static void fprint(string str)
        {
            sw.Write(str);
        }
        public static void fprintLn(string str)
        {
            sw.WriteLine(str);
        }
        public static void fflush()
        {
            sw.Flush();
        }
    }

    public enum typeName
    {
        noactive = 0,
        undefined = 1,
        moldclose = 2,
        moldopen = 3,
        moldopened = 4,
        carriageFor = 5,
        carriageBack = 6,
        nozzleOpen = 7,
        injection = 8,
        holding = 9,
        presRelease = 10,
        cooling = 11,
        preSuckback = 12,
        plasticaz = 13,
        suckback = 14,
        nozzleClose = 15,
        ejectorFor = 16,
        ejectorShake = 17,
        ejectorBack = 18,
        coreAin = 19,
        coreAout = 20,
        coreBin = 21,
        coreBout = 22,
        coreCin = 23,
        coreCout = 24,
        coreDin = 25,
        coreDout = 26,
        coreEin = 27,
        coreEout = 28,
        coreFin = 29,
        coreFout = 30,
        airBlow1 = 31,
        airBlow2 = 32,
        airBlow3 = 33,
        airBlow4 = 34,
        airBlow5 = 35,
        airBlow6 = 36,
        airBlow7 = 37,
        airBlow8 = 38,
        airBlow9 = 39,
        airBlow10 = 40,
        airBlow11 = 41,
        airBlow12 = 42,
        valveGate1 = 43,
        valveGate2 = 44,
        valveGate3 = 45,
        valveGate4 = 46,
        takeout = 47,
        tableIn = 48,
        tableOut = 49
    };

    public enum barType
    {
        /// <summary>
        /// 无动作
        /// </summary>
        noactive = 0,
        /// <summary>
        /// 未定义
        /// </summary>
        undefined = 1,
        /// <summary>
        /// 合模
        /// </summary>
        moldclose = 2,
        /// <summary>
        /// 开模
        /// </summary>
        moldopen = 3,
        /// <summary>
        /// 模具开启时间
        /// </summary>
        moldopened = 4,
        /// <summary>
        /// 射台进
        /// </summary>
        carriageFor = 5,
        /// <summary>
        /// 射台退
        /// </summary>
        carriageBack = 6,
        /// <summary>
        /// 喷嘴开
        /// </summary>
        nozzleOpen = 7,
        /// <summary>
        /// 注射
        /// </summary>
        injection = 8,
        /// <summary>
        /// 保压
        /// </summary>
        holding = 9,
        /// <summary>
        /// 泄压
        /// </summary>
        pressRelease = 10,
        /// <summary>
        /// 冷却
        /// </summary>
        cooling = 11,
        /// <summary>
        /// 前松退
        /// </summary>
        preSuckback = 12,
        /// <summary>
        /// 储料
        /// </summary>
        plasticaz = 13,
        /// <summary>
        /// 后松退
        /// </summary>
        suckback = 14,
        /// <summary>
        /// 喷嘴关
        /// </summary>
        nozzleClose = 15,
        /// <summary>
        /// 顶针进
        /// </summary>
        ejectorFor = 16,
        /// <summary>
        /// 顶针震动
        /// </summary>
        ejectorShake = 17,
        /// <summary>
        /// 顶针退
        /// </summary>
        ejectorBack = 18,
        /// <summary>
        /// 中子进
        /// </summary>
        coreIn = 19,
        /// <summary>
        /// 中子退
        /// </summary>
        coreOut = 20,
        /// <summary>
        /// 吹气
        /// </summary>
        airBlow = 21,
        /// <summary>
        /// 气阀
        /// </summary>
        valveGate = 22,
        /// <summary>
        /// 取出
        /// </summary>
        takeout = 23,
        /// <summary>
        /// 转盘正转
        /// </summary>
        tableIn = 24,
        /// <summary>
        /// 转盘反转
        /// </summary>
        tableOut = 25,
    }

    public enum MachionState
    {
        /// <summary>
        /// 合模
        /// </summary>
        moldClose = 0,
        moldOpen = 1,
        takeout =2,
        cycleInterval = 3,
        moldAdjustFwd =4,
        moldAdjustBwd =5,
        ejectFwd =6,
        ejectBwd = 7,
        wait = 8,
        airBlow5 =9,
        airBlow6 =10,
        airBlow1 = 11,
        airBlow2 = 12,
        airBlow3 = 13,
        airBlow4 = 14,
        injUnitFwd = 15,
        injUnitBwd = 16,
        injection = 17,
        carriage = 18,
        suckback = 19,
        cooling = 20,
        airBlow7 = 21,
        airBlow8 = 22,
        moldAjust = 23,
        autoPurge = 24,
        coreAOut = 26,
        coreAIn = 27,
        coreBOut = 28,
        coreBIn = 29,
        coreCOut = 30,
        coreCIn = 31
    }
    public enum MachionState2
    {
        lubrication = 0,
        airBlow9 = 1,
        airBlow10 =2,
        airBlow11 =3,
        airBlow12 = 4,
        coreDOut =5,
        coreDIn = 6,
        coreFOut = 7,
        coreFIn = 8,
        lubrication2 = 9,
        moldOpened = 31
    }

    public enum motorState
    {
        Rdy = 0,
        ON = 1,
        Eab = 2,
        Fat = 3,
        VtE = 4,
        Qst = 5,
        SDs = 6,
        Wrn = 7,
        Sp1 = 8,
        Rmt = 9,
        TaR = 10,
        ItL = 11
    }

    public struct Anablocks
    {
        private uint _Active;
        /// <summary>
        /// 获取或设置动作在当前周期是否被激活
        /// </summary>
        public uint Active
        {
            set
            {
                if (value == 1)
                {
                    _Active = 1;
                }
                else
                {
                    _Active = 0;
                }
            }
            get
            {
                return _Active;
            }
        }
        private uint _Move;
        /// <summary>
        /// 获取或设置动作状态
        /// 0 停止
        /// 1 进行
        /// </summary>
        public uint Move
        {
            set
            {
                if (value == 1)
                {
                    _Move = 1;
                }
                else
                {
                    _Move = 0;
                }
            }
            get
            {
                return _Move;
            }
        }
        private typeName _Name;
        /// <summary>
        /// 获取或设置动作名称
        /// </summary>
        public typeName Name
        {
            set
            {
                _Name = value;
            }
            get
            {
                return _Name;
            }
        }
        private double _tStart;
        /// <summary>
        /// 获取或设置动作开始时间
        /// </summary>
        public double tStart
        {
            set
            {
                if (value > 0)
                {
                    _tStart = value;
                }
                else
                {
                    _tStart = 0;
                }
            }
            get
            {
                return _tStart;
            }
        }
        private double _tEnd;
        /// <summary>
        /// 获取或设置动作结束时间
        /// </summary>
        public double tEnd
        {
            set
            {
                if (value > 0)
                {
                    _tEnd = value;
                }
                else
                {
                    _tEnd = 0;
                }
            }
            get
            {
                return _tEnd;
            }
        }
        private uint _seqStart;
        /// <summary>
        /// 获取或设置动作开始模数
        /// </summary>
        public uint seqStart
        {
            set
            {
                if (value > 0)
                {
                    _seqStart = value;
                }
                else
                {
                    _seqStart = 0;
                }
            }
            get
            {
                return _seqStart;
            }
        }
        private uint _seqEnd;
        /// <summary>
        /// 获取或设置动作结束模数
        /// </summary>
        public uint seqEnd
        {
            set
            {
                if (value > 0)
                {
                    _seqEnd = value;
                }
                else
                {
                    _seqEnd = 0;
                }
            }
            get
            {
                return _seqEnd;
            }
        }
        private uint _SvrPtr;
        /// <summary>
        /// 获取或设置对应Svr地址
        /// </summary>
        public uint SvrPtr
        {
            set
            {
                _SvrPtr = value;
            }
            get
            {
                return _SvrPtr;
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="ActiveSet">当前周期是否被激活</param>
        /// <param name="MoveSet">动作状态</param>
        /// <param name="NameSet">动作名称</param>
        /// <param name="tStartSet">动作开始时间</param>
        /// <param name="tEndSet">动作结束时间</param>
        /// <param name="seqStartSet">动作开始周期</param>
        /// <param name="seqEndSet">动作结束周期</param>
        public void init(uint ActiveSet, uint MoveSet, uint NameSet, uint tStartSet, uint tEndSet, uint seqStartSet, uint seqEndSet)
        {
            _Active = ActiveSet;
            _Move = MoveSet;
            _Name = (typeName)NameSet;
            _tStart = tStartSet;
            _tEnd = tEndSet;
            _seqStart = seqStartSet;
            _seqEnd = seqEndSet;
        }

        public string tostring()
        {
            string str = string.Empty;

            str += _Active.ToString() + @"\t";
            str += _Move.ToString() + @"\t";
            str += _Name.ToString() + @"\t";
            str += _tStart.ToString() + @"\t";
            str += _tEnd.ToString() + @"\t";
            str += _seqStart.ToString() + @"\t";
            str += _seqEnd.ToString() + @"\t";

            return str;
        }
    }
}
