using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace nsVicoClient
{
    public class vm
    {
        static FileStream _fsLog = null;
        static FileStream fsLog
        {
            get
            {
                if (_fsLog == null)
                    _fsLog = new FileStream("log.vm", FileMode.OpenOrCreate);
               
                return _fsLog;
            }
            set
            {
                _fsLog = value;
            }
        }
        static StreamWriter _swLog = null;
        static StreamWriter swLog
        {
            get
            {
                if (_swLog == null)
                {
                    _swLog = new StreamWriter(fsLog);
                }
                return _swLog;
            }
            set
            {
                _swLog = value;
            }
        }
        //static FileStream fs2 = new FileStream("fsOut2.vm", FileMode.Create);
        //static StreamWriter sw2 = new StreamWriter(fs2);
        public static bool flagPerrorPrint = true;
        public static bool flagDebugPrint = true;
        /// <summary>
        /// return DateTime.Now.Ticks/10000;
        /// </summary>
        public static long tm
        {
            get
            {
                return DateTime.Now.Ticks/10000;
            }
        }
        /// <summary>
        ///             if (flagDebugPrint)
        ///        Console.WriteLine(str +"\t" + DateTime.Now.Ticks / 10000);
        ///    return DateTime.Now.Ticks / 10000;
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static long getTm(string str)
        {
            //if (flagDebugPrint)
            //    Console.WriteLine(str +"\t" + DateTime.Now.Ticks / 10000);
            return DateTime.Now.Ticks / 10000;
        }
        /// <summary>
        ///             long ticks = DateTime.Now.Ticks / 10000 % 10000;
        ///    if (flagDebugPrint)
        ///        Console.WriteLine(ticks);
        ///    return ticks;
        /// </summary>
        /// <returns></returns>
        public static long  getTm()
        {
            long ticks = DateTime.Now.Ticks / 10000 % 10000;
            //if (flagDebugPrint)
            //    Console.WriteLine(ticks);
            return ticks;
        }
        /// <summary>
        ///    if (flagDebugPrint)
        ///        Console.WriteLine(str + "\t" + DateTime.Now.Ticks / 100);
        ///    return DateTime.Now.Ticks / 100;
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static long getTmH(string str = "")
        {
            //if (flagDebugPrint)
            //    Console.WriteLine(str + "\t" + DateTime.Now.Ticks / 100);
            return DateTime.Now.Ticks / 10;
        }
        /// <summary>
        /// return DateTime.Now.Ticks / 10000 % 10000;
        /// </summary>
        /// <returns></returns>
        public static long getMilSecTm()
        {
            return DateTime.Now.Ticks / 10000 % 10000;
        }
        /// <summary>
        ///             if (flagDebugPrint)
        ///        Console.WriteLine((DateTime.Now.Ticks / 1000 % 10000 - lastTm).ToString());
        ///    lastTm = DateTime.Now.Ticks / 1000 % 10000;
        /// </summary>
        static  long lastTm = 0;
        public static void printMilSecTm()
        {
            if (flagDebugPrint)
                Console.WriteLine((DateTime.Now.Ticks / 1000 % 10000 - lastTm).ToString());
            lastTm = DateTime.Now.Ticks / 1000 % 10000;
        }
        /// <summary>
        ///    if (flagDebugPrint)
        ///       Console.WriteLine(str + (DateTime.Now.Ticks / 1000 % 10000 - lastTm).ToString());
        ///    lastTm = DateTime.Now.Ticks / 1000 % 10000;
        /// </summary>
        /// <param name="str"></param>
        public static void printMilSecTm(string str)
        {
            if (flagDebugPrint)
                Console.WriteLine(str + (DateTime.Now.Ticks / 1000 % 10000 - lastTm).ToString());
            lastTm = DateTime.Now.Ticks / 1000 % 10000;
        }
        /// <summary>
        ///     if (flagDebugPrint)
        ///        Console.WriteLine(DateTime.Now.Ticks / 100);
        /// </summary>
        public static void getTmH()
        {
            if (flagDebugPrint)
                Console.WriteLine(DateTime.Now.Ticks / 100);
        }
       /// <summary>
       ///      if (flagPerrorPrint)
       ///         Console.WriteLine("error : " + str);
       /// </summary>
       /// <param name="str"></param>
        public static void perror(string str)
        {
            if (flagPerrorPrint)
            {
                try
                {
                    Console.WriteLine("error : " + str);
                    fprintLn(str);
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine("[perror] " + ex.ToString());
                }

            }
            //System.Windows.MessageBox.Show(str);
        }
        /// <summary>
        ///    if (flagDebugPrint)
        ///        Console.WriteLine("debug : " + str);
        /// </summary>
        /// <param name="str"></param>
        public static void debug(string str)
        {
            if (flagDebugPrint)
                Console.WriteLine("debug : " + str);
        }
        /// <summary>
        ///     if (flagDebugPrint)
        ///        Console.WriteLine(str);
        /// </summary>
        /// <param name="str"></param>
        public static void printLn(string str)
        {
            if (flagDebugPrint)
                Console.WriteLine(str);
        }
        /// <summary>
        ///    if (flagDebugPrint)
        ///        Console.Write(str);
        /// </summary>
        /// <param name="str"></param>
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
        /// <summary>
        ///  输出 32位int值的 二进制表示方式
        /// </summary>
        /// <param name="value"></param>
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
            swLog.Write(str);
        }
        public static void fprintLn(string str)
        {
            swLog.WriteLine(str);
        }
        public static void fflush()
        {
            swLog.Flush();
            fsLog.Flush();
        }

        //public static void fprint2(string str)
        //{
        //    //sw2.Write(str);
        //}
        //public static void fprintLn2(string str)
        //{
        //    //sw2.WriteLine(str);
        //}
        //public static void fflush2()
        //{
        //    //sw2.Flush();
        //}

    }
}
