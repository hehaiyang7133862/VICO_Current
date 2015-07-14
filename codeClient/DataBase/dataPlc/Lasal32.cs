using System;
using System.Runtime.InteropServices;

namespace nsDataMgr
{
    public class Lasal32
    {
        public const byte DONE = 0;
        public const byte BUSY = 1;
        public const byte GO_ON = 2;
        public const byte IPERROR = 3;
        public const byte QUIT = 4;
        public const uint ACCESS_DENIED = 0x80000010;

        public const byte LslBit_STRINGSERVER = 0x80;
        public const byte LslBit_SERVER = 0x40;
        public const byte LslBit_PARSE = 0x20;
        public const byte LslBit_BITNO = 0x1F;

        [StructLayout(LayoutKind.Sequential)]
        public struct SRLVarInfo
        {
            public uint dwAddr;	// Address of the Variable or Server
            public uint dwType;	// Have LSL_RL_TYPE_
            public uint dwSize;	// must be Zero (for future)
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PlcDateTime
        {
            public ushort dYear;
            public ushort dMonth;
            public ushort dDayOfWeek;
            public ushort dDay;
            public ushort tHour;
            public ushort tMinute;
            public ushort tSecond;
            public ushort tMilliseconds;
        }

        /// <summary>
        /// plc reflist (static, dynamic)
        /// </summary>
        public enum CpReflist : uint
        {
            RF_DYNAMIC = 0x00010000,
            RF_STATIC = 0x00010001,
        }
        /// <summary>
        /// plc textformat (ascii, unicode)
        /// </summary>
        public enum CpTextFormat : uint
        {
            TXT_ASCII = 0,
            TXT_UNICODE = 1,
        }
        /// <summary>
        /// plc command
        /// </summary>
        public enum CpCommand : byte
        {
            I_GET_OBJ = 0,  // name -> handle
            I_GET_OBJ_CLS = 1,  // name -> handle and class name
            I_READ_CLASS = 2,  // index -> class info
            I_WRITE = 3,  // write to server
            I_WRITE_TO_CLNT = 4,  // write to the server connected to a client
            I_READ_OBJECT = 5,  // class header, index -> object info
            I_READ_CHANNEL = 6,  // read the channel
            I_READ_METHOD = 7,  // class header, index -> method info
            I_READ = 8,  // handle -> read server channel
            I_STARTPROG = 9,  // program no, label no -> start parallel
            I_RUNPROG = 10,  // program no, label no -> start sequential
            I_CMD = 11,  // handle, cmd, paras -> execute method
            I_DELAY = 12,  // time -> wait till time elapsed
            I_NOP = 13,  // ==== not used
            I_LBL = 14,  // ==== not used
            I_GOTO = 15,  // goto label
            I_ENDPROG = 16,  // terminates the current ipr program
            I_CALL = 17,  // subroutine call
            I_RET = 18,  // return from subroutine
            I_FUNCTION = 19,  // ======= not used
            I_ENDFUNCTION = 20,  // ======= not used
            I_INC = 21,  // handle -> increments data image
            I_DEC = 22,  // handle -> decrements data image
            I_START_LOAD_PR = 23,  // prepare OPsystem for new program to load
            I_LOAD_PROG = 24,  // load new program code
            I_COMMENT = 25,  // line is not executed
            I_JMPIF = 26,  // jump if condition is true
            I_WAITFOR = 27,  // wait until condition is true
            I_SETFORTIME = 28,  // ======= not used
            I_GETPROG = 29,  // load program code from the PLC
            I_GETPROGSTATE = 30,  // program no -> state
            I_CHECK_FOR_LOAD = 31,  // check if new progs are to load
            I_GET_DESC_CRC = 32,  // CRC of all LASAL-descriptor lists in the PLC
            I_READ_CONNECT = 33,  // get the client connection
            I_GET_CLS = 34,  // get the class name from an object's address
            I_GET_OBJ_NAME = 35,  // get object name from class name and object addr
            I_READ_CLT = 36,  // call read method of connected server
            I_STOPPROG = 37,  // stops a interpreter program
            I_GET_PROGNR = 38,  // get programnumber of programname
            I_GET_ACT_OFFSET = 39,  // get the actual offset of the program
            I_GET_TRIGGER_COUNT = 40,  // get the count of the trigger
            I_GET_VERSION = 41,  // returns the loader version
            I_NEW_OBJ = 42,  // get the programname of the programnumber
            I_CONNECT = 43,
            I_SET_CYCLE = 44,
            I_DEL = 45,
            I_STOP_IPR_CHEK_FOR_LOAD = 46, //stop all interpreter and checkforload
            I_REGISTER = 47, // for login and to inform the PLC that the channel is still used
            I_RELEASE0 = 48, // release the communication channel at end of debug session
            I_TRY_SOFTLOAD_IPR0 = 49, // Try to load an program from the temporary memory in a soft mode
            I_END_SOFTLOAD_IPR = 50, // End the softload
            I_GET_CFL_CYCLE = 51, // Get the Cycle time of the CheckForLoad
            I_GET_CALLED_IPR = 52, // Liefert die Nummer des Interpreters der von diesem Ipr aufgerufen wurde
            I_GET_CALLED_FROM = 53, // Bringt die Nummer des Iprs von dem dieser Ipr aufgerufen wurde
            I_INIT = 54, // calls the init-method of a server or an object
            I_GET_CLS_BY_NAME = 55, // gets a class header pointer for a given class name
            I_GET_NXT_DERIVED = 56, // get next derived class of a base class
            I_LOCK = 57, // lock communication buffer
            I_UNLOCK = 58, // unlock communcation buffer
            I_LSLCMD = 59,
            I_CMD_DEBUGIP = 60, // gleich wie I_CMD, nur wird für die Serveradresse der this-Pointer (DebugIp Objekt) verwendet
            I_CREATE_PROG = 61, // ++pr:test
            I_LOAD_PROG2 = 62, // wie I_LOAD_PROG jedoch ohne den Fehler mit der falschen Länge bei der Checksummenberechnung
            I_START_IPR = 63, // ++pr:test
            I_GET_GLOBAL_ADDR = 64,
            I_GET_DATA = 65,
            I_GETPROGSTATE_ALL = 66, // program state
            I_SET_DATA = 67,
            I_GET_STACKINFO = 68,
            I_GET_OBJ_LIST = 69,
            I_VISU_RDY = 70,
            I_RD_SVRLIST = 71, // damit auf mehrere server mit einem aufruf gelesen werden kann
            I_WR_SVRLIST = 72, // damit auf mehrere server mit einem aufruf geschrieben werden kann            
            I_ILLEGAL = 73,
        }
        public static CpCommand Mode_0 = 0;
        /// <summary>
        /// plc prefix
        /// </summary>
        public enum CpPrefix : byte
        {
            P_IMMED = 0, // data source is a constant
            P_VARIA = 1, // data source is a channel
            P_SYS = 2, // data source are system variables
            P_POPEN = 3, // open parenthesis
            P_PCLOSE = 4, // close parenthesis
            P_ADD = 5, // addition
            P_SUB = 6, // subtraction
            P_COMMA = 7, //
            P_EOL = 8, // end of line
            P_EQ = 9, // comparisons
            P_NEQ = 10,
            P_GT = 11,
            P_GEQ = 12,
            P_LT = 13,
            P_LEQ = 14,
            P_NOT = 15, // binary negation
            P_AND = 16,
            P_OR = 17,
            P_XOR = 18,
            P_USER_STREAM = 19,
            P_MUL = 20,
            P_DIV = 21,
            P_ILLEGAL = 22,
        }
        /// <summary>
        /// plc state
        /// </summary>
        public enum CpState : byte
        {
            CS_RUN_RAM = 0,
            CS_RUN_ROM,
            CS_RUNTIME,
            CS_POINTER,
            CS_CHKSUM,
            CS_WATCHDOG,
            CS_ERROR,
            CS_PROM_DEFECT,
            CS_RESET,
            CS_WD_DEFECT,
            CS_STOP,            // 10
            CS_BUSY_PROG,
            CS_PGM_TOO_LONG,
            CS_PROG_END,
            CS_PROG_MEMO,
            CS_STOP_BRKPT,
            CS_CPU_STOP,
            CS_INTERROR,
            CS_SINGLESTEP,
            CS_READY,
            CS_LOAD,		    // 20
            CS_WRONG_MODULE,
            CS_MEMORY_FULL,
            CS_NOT_LINKED,
            CS_DIV_BY,
            CS_DIAS_ERROR,
            CS_WAIT,
            CS_OP_PROG,
            CS_OP_INSTALLED,
            CS_OP_TOO_LONG,
            CS_NO_OPSYS,        // 30
            CS_SEARCH_OPSYS,
            CS_NO_DEVICE,
            CS_UNUSED_CODE,
            CS_MEMORY_ERROR,
            CS_MAX_IO,
            CS_LOADERROR,
            CS_OSERROR,
            CS_APPMEMERROR,
            CS_OFFLINE,
            CS_APPL_LOAD,       // 40
            CS_APPL_SAVE,
            CS_NC42,
            CS_NC43,
            CS_VARAN_MANAGER_ERROR,
            CS_VARAN_ERROR,
            CS_APPL_LOAD_LOADER,
            CS_APPL_SAVE_ERROR,
            CS_NC48,
            CS_NC49,
            CS_ACCESS,          // 50
            CS_BND_EXCEED,
            CS_PRIV_INSTR,
            CS_FLOAT,

            CS_DIAS_RISCERROR = 60,  // Intell. Dias Master Unknown Instruct

            CS_OS_ERROR = 64,	// internal error, all appl stopped, general error
            CS_FILE_ERROR,					// error while operating with a file(save/load/..)
            CS_DEBUG_ASSERT_FAILED,			// debug assertion failed
            CS_RT_RUNTIME,
            CS_BG_RUNTIME,

            CS_LOADER_INIT = 100,
            CS_LOADER_RUNRAM,
            CS_LOADER_RUNROM,
            CS_LOADER_RUNTIME,
            CS_LOADER_READY,
            CS_LOADER_OK,				// 105
            CS_LOADER_UNKNOWN_CID,
            CS_LOADER_UNKNOWN_CONSTR,
            CS_LOADER_UNKNOWN_OBJECT,
            CS_LOADER_UNKNOWN_CHNL,
            CS_LOADER_WRONG_CONNECT,	// 110
            CS_LOADER_WRONG_ATTR,
            CS_LOADER_SYNTAX_ERROR,
            CS_LOADER_NO_FILE_OPEN,
            CS_LOADER_OUTOF_NEAR,
            CS_LOADER_OUTOF_FAR,		// 115
            CS_LOADER_INCOMPATIBLE,		// object with the same name already exists, but has a different class
            CS_LOADER_COMPATIBLE,		// object with the same name and class already exists, has to be updated

            CP_LC2_STARTUP = 0xD0, // Reserviert für LC2 bis 0xDF
            CP_LC2_INIT_FOR_CHANGED,

            CS_LINKING = 0xE0,	// 224
            CS_LINKERROR,
            CS_LINKDONE,

            CS_OP_BURN = 0xE6,	// 230
            CS_OP_BURN_FAIL,
            CS_OP_INSTALL,				// i.e. booting a .rtb-file via dll

            CS_USVWAIT = 0xF0,	// 240
            CS_REBOOT,
            CS_LSLSAVE,
            CS_LSLLOAD,

            CS_CONTINUE = 0xFC,	// 252
            CS_PRE_RUN,
            CS_PRE_RESET,

            CS_CONNECTIONBREAK = 0xFF,	// 255
        }
        /// <summary>
        /// symboltable datatype
        /// </summary>
        public enum LslType : byte
        {
            T_NONE = 0,
            T_BOOL = 1,
            T_SINT = 2,
            T_INT = 3,
            T_DINT = 4,
            T_USINT = 6,
            T_UINT = 7,
            T_UDINT = 8,
            T_ULINT = 9,
            T_REAL = 10,
            T_LREAL = 11,
            T_CHAR = 20,
            T_HSINT = 21,
            T_HINT = 22,
            T_HDINT = 23,
            T_BSINT = 24,
            T_BINT = 25,
            T_BDINT = 26,
            T_ENUMERATED = 27,
            T_ARRAY = 28,
            T_STRUCTURE = 29,
            T_POINTER = 30,
            T_BIT = 32,
            T_CLASS = 33,
            T_CHARARRAY = 41,
        }

        /// <summary>
        /// create handle to ensure access to connection functionality
        /// </summary>
        /// <returns></returns>
        [DllImport("lasal32.dll")]
        public static extern int OcbOpen();

        [DllImport("lasal32.dll")]
        public static extern bool SetCommand(byte nCommand);

        /// <summary>
        /// destroy already created handle
        /// </summary>
        /// <param name="ocbNum"></param>
        [DllImport("lasal32.dll")]
        public static extern void OcbClose(int ocbNum);

        /// <summary>
        /// open connection-port to plc
        /// </summary>
        /// <param name="ocbNum"></param>
        /// <param name="szComm"></param>
        /// <param name="uBaudRate"></param>
        /// <param name="uPcStation"></param>
        /// <param name="uSpsStation"></param>
        /// <param name="uAutoInit"></param>
        /// <returns></returns>
        [DllImport("lasal32.dll")]
        public static extern bool OnlineH(int ocbNum, string szComm, byte uBaudRate, byte uPcStation, byte uSpsStation, byte uAutoInit);

        /// <summary>
        /// close communication-port to plc
        /// </summary>
        /// <param name="ocbNum"></param>
        [DllImport("lasal32.dll")]
        public static extern void OfflineH(int ocbNum);

        /// <summary>
        /// check communication-port to plc
        /// </summary>
        /// <param name="ocbNum"></param>
        /// <returns></returns>
        [DllImport("lasal32.dll")]
        public static extern bool IsOnlineH(int ocbNum);

        /// <summary>
        /// send commandstream to plc and get answer
        /// </summary>
        /// <param name="ocbNum"></param>
        /// <param name="paras"></param>
        /// <param name="result"></param>
        /// <param name="resultSize"></param>
        /// <returns></returns>
        [DllImport("lasal32.dll")]
        public static extern bool ExecIprH(int ocbNum, byte[] paras, byte[] result, uint resultSize);

        /// <summary>
        /// get CpStatus of plc
        /// </summary>
        /// <param name="ocbNum"></param>
        /// <param name="pCpState"></param>
        /// <returns></returns>
        [DllImport("lasal32.dll")]
        public static extern bool GetCpuStatusH(int ocbNum, out CpState pCpState);

        /// <summary>
        /// get date and time from plc
        /// </summary>
        /// <param name="ocbNum"></param>
        /// <param name="pSysTime"></param>
        /// <returns></returns>
        [DllImport("lasal32.dll")]
        public static extern bool LslReadDateTimeH(int ocbNum, out PlcDateTime pSysTime);

        /// <summary>
        /// set date and time in plc
        /// </summary>
        /// <param name="ocbNum"></param>
        /// <param name="pSysTime"></param>
        /// <returns></returns>
        [DllImport("lasal32.dll")]
        public static extern bool LslSetDateTimeH(int ocbNum, ref PlcDateTime pSysTime);

        /// <summary>
        /// get address of global variable
        /// </summary>
        /// <param name="ocbNum"></param>
        /// <param name="varname"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        [DllImport("lasal32.dll")]
        
        public static extern bool LslGetAdressVarH(int ocbNum, [MarshalAs(UnmanagedType.LPStr)]string varname, out uint address);

        /// <summary>
        /// send data to plc
        /// </summary>
        /// <param name="ocbNum"></param>
        /// <param name="tx"></param>
        /// <param name="address"></param>
        /// <param name="bytesize"></param>
        /// <returns></returns>
        [DllImport("lasal32.dll")]
        public static extern bool SetDataH(int ocbNum, byte[] tx, uint address, short bytesize);

        /// <summary>
        /// get data from plc
        /// </summary>
        /// <param name="ocbNum"></param>
        /// <param name="rx"></param>
        /// <param name="address"></param>
        /// <param name="bytesize"></param>
        /// <returns></returns>
        [DllImport("lasal32.dll")]
        public static extern bool GetDataH(int ocbNum, byte[] rx, uint address, short bytesize);

        //[UnmanagedFunctionPointerAttribute(CallingConvention.Cdecl)]
        //public delegate uint CB_FUNCTYPE(uint step);

        //[DllImport("lasal32.dll")]
        //public static extern bool FileWriteH(int ocbNum, string dpne, byte[] pData, uint length, CB_FUNCTYPE pcallback, uint attrib, uint offs);

        //[DllImport("lasal32.dll")]
        //public static extern bool FileLoadH(int ocbNum, byte[] pData, string dpne, uint datalen, CB_FUNCTYPE pcallback, uint offs);

        //[DllImport("lasal32.dll")]
        //public static extern bool FileInfoH(int ocbNum, byte[] RXbuf, string dpne, uint buflen);

        private static uint GetLasalIdSub(int ocbNum, byte[] tx)
        {
            //byte[] rx = new byte[260];

            //if (ExecIprH(ocbNum, tx, rx, 256) == true)
            //{
            //    unsafe
            //    {
            //        fixed (byte* prx = rx)
            //        {
            //            if (*(uint*)prx == DONE)
            //            {
            //                fixed (byte* pcm = &rx[14]) // --> to channelmode
            //                {
            //                    if (*(uint*)pcm < 3) // just server is accessible
            //                    {
            //                        fixed (byte* pry = &rx[6]) // --> to lasalid
            //                            return *(uint*)pry;
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}

            return 0;
        }

        /// <summary>
        /// get lasalid from plc by using byte array (ASCII-0-string)
        /// </summary>
        /// <param name="ocbNum"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static uint GetLasalIdH(int ocbNum, byte[] name)
        {
            byte[] tx = new byte[256];

            uint i = 0;
            uint j = 3;
            byte chr;
            do
            {
                chr = name[i++];
                tx[j++] = chr;
            }
            while (chr != 0);

            tx[j] = (byte)CpPrefix.P_EOL;
            tx[0] = (byte)j;
            tx[1] = 0;
            tx[2] = (byte)CpCommand.I_GET_OBJ;

            return GetLasalIdSub(ocbNum, tx);
        }

        /// <summary>
        /// get lasalid from plc by using string
        /// </summary>
        /// <param name="ocbNum"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static uint GetLasalIdH(int ocbNum, string name)
        {
            byte[] tx = new byte[256];

            uint j = 3;
            foreach (char chr in name)
                tx[j++] = (byte)chr;

            tx[j++] = 0;
            tx[j] = (byte)CpPrefix.P_EOL;
            tx[0] = (byte)j;
            tx[1] = 0;
            tx[2] = (byte)CpCommand.I_GET_OBJ;

            return GetLasalIdSub(ocbNum, tx);
        }

        /// <summary>
        /// get crc of plc to check if project, particularly lasalids has changed 
        /// </summary>
        /// <param name="ocbNum"></param>
        /// <returns></returns>
        public static uint GetDescCrc(int ocbNum)
        {
            byte[] tx = new byte[32];
            byte[] rx = new byte[260];

            tx[0] = 3;
            tx[1] = 0;
            tx[2] = (byte)CpCommand.I_GET_DESC_CRC;
            tx[3] = (byte)CpPrefix.P_EOL;

            //if (ExecIprH(ocbNum, tx, rx, 256) == true)
            //{
            //    unsafe
            //    {
            //        fixed (byte* prx = rx)
            //        {
            //            if (*(uint*)prx == DONE)
            //            {
            //                fixed (byte* pry = &rx[6])
            //                    return *(uint*)pry;
            //            }
            //        }
            //    }
            //}

            return 0;
        }

        /// <summary>
        /// get loaderversion
        /// </summary>
        /// <param name="ocbNum"></param>
        /// <returns></returns>
        public static uint GetLoaderVersion(int ocbNum)
        {
            byte[] tx = new byte[32];
            byte[] rx = new byte[260];

            tx[0] = 3;
            tx[1] = 0;
            tx[2] = (byte)CpCommand.I_GET_VERSION;
            tx[3] = (byte)CpPrefix.P_EOL;

            //if (ExecIprH(ocbNum, tx, rx, 256) == true)
            //{
            //    unsafe
            //    {
            //        fixed (byte* prx = rx)
            //        {
            //            if (*(uint*)prx == DONE)
            //            {
            //                fixed (byte* pry = &rx[6])
            //                    return *(uint*)pry;
            //            }
            //        }
            //    }
            //}

            return 0;
        }

        /// <summary>
        /// check if application in plc is running, return true when program in plc is alright
        /// </summary>
        /// <param name="ocbNum"></param>
        /// <returns></returns>
        public static bool IsPlcRun(int ocbNum)
        {
            CpState cpstate;
            if (GetCpuStatusH(ocbNum, out cpstate) == true)
            {
                switch (cpstate)
                {
                    case CpState.CS_RUN_RAM:
                    case CpState.CS_RUN_ROM:
                    case CpState.CS_STOP_BRKPT:
                    case CpState.CS_SINGLESTEP: return true;
                }
            }

            return false;
        }

        /// <summary>
        /// read data from dataserver
        /// </summary>
        /// <param name="ocbNum"></param>
        /// <param name="value"></param>
        /// <param name="lasalid"></param>
        /// <returns></returns>
        public static bool CallReadMethodOfServerH(int ocbNum, out uint value, uint lasalid)
        {
            value = 0;
            if (lasalid != 0)
            {
                IREAD_Stream tx = new IREAD_Stream(lasalid);
                IRESULT_Stream rx = new IRESULT_Stream();

                if (ExecIprH(ocbNum, tx.GetData(), rx.GetData(), rx.GetSize()) == true)
                {
                    if (rx.IsValid() == true)
                    {
                        rx.Read32(out value, 6);
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// write data to dataserver
        /// </summary>
        /// <param name="ocbNum"></param>
        /// <param name="value"></param>
        /// <param name="lasalid"></param>
        /// <param name="access_denied"></param>
        /// <returns></returns>
        public static bool CallWriteMethodOfServerH(int ocbNum, uint value, uint lasalid, out bool access_denied)
        {
            access_denied = false;
            if (lasalid != 0)
            {
                IWRITE_Stream tx = new IWRITE_Stream(lasalid, value);
                IRESULT_Stream rx = new IRESULT_Stream();

                if (ExecIprH(ocbNum, tx.GetData(), rx.GetData(), rx.GetSize()) == true)
                {
                    uint result;
                    if (rx.IsValid() == true)
                    {
                        if (rx.Read32(out result, 6) == true)
                        {
                            if (result == ACCESS_DENIED)
                                access_denied = true;
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// call read-method of more than one server
        /// </summary>
        /// <param name="ocbNum"></param>
        /// <param name="value">array of values</param>
        /// <param name="lasalid">array of lasalid</param>
        /// <param name="no">number of lasalids</param>
        /// <returns></returns>
        public static bool CallReadMethodOfServerListH(int ocbNum, out uint[] value, uint[] lasalid, uint no)
        {
            if ((lasalid != null) && (no > 0) && (no <= 60))
            {
                I_RD_SVRLIST_Stream tx = new I_RD_SVRLIST_Stream(lasalid, no);
                IRESULT_Stream rx = new IRESULT_Stream();
                if (ExecIprH(ocbNum, tx.GetData(), rx.GetData(), rx.GetSize()) == true)
                {
                    uint tmp;
                    if (rx.Read32(out tmp, 6) == true)
                    {
                        if (tmp == no)
                        {
                            value = new uint[no];
                            for (uint i = 0; i < no; i++)
                                rx.Read32(out value[i], 10 + i * 4);
                            return true;
                        }
                    }
                }
            }

            value = null;
            return false;
        }

        /// <summary>
        /// call write-method of more than one server
        /// </summary>
        /// <param name="ocbNum"></param>
        /// <param name="value">array of values</param>
        /// <param name="lasalid">array of lasalid</param>
        /// <param name="no">number of lasalids</param>
        /// <returns></returns>
        public static bool CallWriteMethodOfServerListH(int ocbNum, uint[] lasalid, uint[] value, uint no)
        {
            if ((lasalid != null) && (value != null) && (no > 0) && (no <= 30))
            {
                I_WR_SVRLIST_Stream tx = new I_WR_SVRLIST_Stream(lasalid, value, no);
                IRESULT_Stream rx = new IRESULT_Stream();
                if (ExecIprH(ocbNum, tx.GetData(), rx.GetData(), rx.GetSize()) == true)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// send I_VISU_RDY to plc und josef
        /// </summary>
        /// <param name="ocbNum"></param>
        /// <returns></returns>
        public static bool SendVisuReadyH(int ocbNum)
        {
            byte[] tx = new byte[32];
            byte[] rx = new byte[260];

            tx[0] = 3;
            tx[1] = 0;
            tx[2] = (byte)CpCommand.I_VISU_RDY;
            tx[3] = (byte)CpPrefix.P_EOL;

            if (ExecIprH(ocbNum, tx, rx, 256) == true)
                return true;

            return false;
        }

        [DllImport("lasal32.dll")]
        private static extern bool LslWriteToSvrStrH(int ocbNum, uint lasalid, byte[] pdata, int iLen);

        [DllImport("lasal32.dll")]
        private static extern bool LslWriteToSvrStrH(int ocbNum, uint lasalid, ushort[] pdata, int iLen);

        /// <summary>
        /// write string to stringserver
        /// </summary>
        /// <param name="ocbNum"></param>
        /// <param name="lasalid"></param>
        /// <param name="source"></param>
        /// <param name="TextFormat"></param>
        /// <returns></returns>
        public static bool WriteStringH(int ocbNum, uint lasalid, string source, CpTextFormat TextFormat)
        {
            if (source != null)
            {
                if (TextFormat == CpTextFormat.TXT_ASCII)
                {
                    byte[] tx = new byte[source.Length + 1];
                    int len = 0;
                    foreach (char chr in source)
                    {
                        tx[len++] = (byte)chr;
                        if (chr == 0)
                            break;
                    }
                    tx[len] = 0;

                    return LslWriteToSvrStrH(ocbNum, lasalid, tx, len);
                }
                else
                {
                    ushort[] tx = new ushort[source.Length + 2];
                    tx[0] = 0x200; // unicode id
                    int len = 1;
                    foreach (char chr in source)
                    {
                        tx[len++] = (ushort)chr;
                        if (chr == 0)
                            break;
                    }
                    tx[len] = 0;
                    return LslWriteToSvrStrH(ocbNum, lasalid, tx, len * 2);
                }
            }
            return false;
        }

        [DllImport("lasal32.dll")]
        private static extern bool LslReadFromSvrStrH(int ocbNum, uint lasalid, byte[] pdata, uint dwLen, ref uint pStringLen);

        /// <summary>
        /// read string from stringserver
        /// </summary>
        /// <param name="ocbNum"></param>
        /// <param name="destination"></param>
        /// <param name="lasalid"></param>
        /// <returns></returns>
        public static bool ReadStringH(int ocbNum, out string destination, uint lasalid)
        {
            byte[] rx = new byte[1026];
            destination = null;

            //if (LslReadFromSvrStrH(ocbNum, lasalid, rx, 1024, ref len) == true)
            //{
            //    if (len >= 2)
            //    {
            //        if ((rx[0] == 0) && (rx[1] == 2))
            //        {
            //            // unicode string
            //            unsafe
            //            {
            //                fixed (byte* ph = &rx[2])
            //                {
            //                    destination = new string((char*)ph);
            //                }
            //            }

            //            return true;
            //        }
            //    }

            //    // asciicode string
            //    unsafe
            //    {
            //        fixed (byte* ph = &rx[0])
            //        {
            //            destination = new string((sbyte*)ph);
            //        }
            //    }

                ////get length of string
                //for (uint i = 0; i < len; i++)
                //{
                //    if (rx[i] == 0)
                //    {
                //        len = i;
                //        break;
                //    }
                //}

                //char[] tmp = new char[len];
                //while (len > 0)
                //{
                //    len--;
                //    tmp[len] = (char)rx[len];
                //}
                //destination = new string(tmp);
                return true;
            //}

            //return false;
        }

        [DllImport("lasal32.dll")]
        public static extern bool LoadPrj(string nProjectName);

        [DllImport("lasal32.dll")]
        public static extern bool LinkModules();
        [DllImport("lasal32.dll")]
        public static extern bool LslSave();

        [DllImport("lasal32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern bool LoadModul(byte[] lst, int len, CB_FUNCTYPE callback);

        // delegate for refreshlistcallbackmethod
        [UnmanagedFunctionPointerAttribute(CallingConvention.Cdecl)]
        public delegate void CB_RLADD_FUNCTYPE(uint pCallbackData, uint dwAddr, uint dwVarID, int nData);

        [DllImport("lasal32.dll")]
        public static extern int LslRefreshListCreate();

        // create refreshlist
        [DllImport("lasal32.dll")]
        public static extern int LslRefreshListCreateExt([MarshalAs(UnmanagedType.LPStr)]string szComm, byte uBaudRate, byte uPcStation, byte uSpsStation, [MarshalAs(UnmanagedType.LPStr)]string lpcName, CB_RLADD_FUNCTYPE pCallback, uint dwCallbackData, uint dwTimeoutMS);

        [DllImport("lasal32.dll")]
        public static extern bool LslRefreshListSetCallback(int iRlb, CB_RLADD_FUNCTYPE pCallback, uint dwCallbackData);

        // add entry to refreshlist
        [DllImport("lasal32.dll")]
        public static extern bool LslRefreshListAdd(int iRlb, ref SRLVarInfo vari, uint dwVarID, uint dwRefreshTime, CpReflist reflist);

        [DllImport("lasal32.dll")]
        public static extern bool LslRefreshListOnline(int iRlb, [MarshalAs(UnmanagedType.LPStr)]string szComm, byte uBaudRate, byte uPcStation, byte uSpsStation);

        [DllImport("lasal32.dll")]
        public static extern bool LstRefreshListCreate();

        [DllImport("lasal32.dll")]
        public static extern bool LslRefreshListOffline(int iRlb);

        [DllImport("lasal32.dll")]
        public static extern bool LslRefreshListIsOnline(int iRlb);

        // start refreshlist
        [DllImport("lasal32.dll")]
        public static extern bool LslRefreshListStart(int iRlb, CpReflist reflist);

        // clear all entries in refreshlist
        [DllImport("lasal32.dll")]
        public static extern bool LslRefreshListClear(int iRLB, CpReflist reflist);

        // get info about object, server, etc... from plc
        [DllImport("lasal32.dll")]
        public static extern bool LslRefreshListGetVarInfo(int iRlb, Byte[] Servername, ref SRLVarInfo vari);
        [DllImport("lasal32.dll")]
        public static extern bool LslRefreshListGetVarInfo(int iRlb, [MarshalAs(UnmanagedType.LPStr)]string Servername, ref SRLVarInfo vari);

        // make info
        [DllImport("lasal32.dll")]
        public static extern bool LslRefreshListMakeSRLVarInfo(ref SRLVarInfo vari, uint dwAddr, uint dwSize, byte LslBit_Info, LslType Type);

        // read data from plc
        [DllImport("lasal32.dll")]
        public static extern bool LslRefreshListGetData(int iRlb, ref SRLVarInfo vari, byte[] rx, int bytesize);

        // write data to plc
        [DllImport("lasal32.dll")]
        public static extern bool LslRefreshListSetData(int iRLB, ref SRLVarInfo vari, byte[] tx, int bytesize);

        // destroy refreshlist
        [DllImport("lasal32.dll")]
        public static extern int LslRefreshListDestroy(int iRLB);

        // delegate for LoadProjectSingleObjCallbackmethod
        [UnmanagedFunctionPointerAttribute(CallingConvention.Cdecl)]
        public delegate uint CB_FUNCTYPE(uint count);

        // send whole project to plc
        [DllImport("lasal32.dll")]
        public static extern bool LslLoadProjectSingleObjFileH(int ocbNum, [MarshalAs(UnmanagedType.LPStr)]string lpProjectName, byte[] pData, CB_FUNCTYPE callback);

        /// <summary>
        /// send command to plc
        /// </summary>
        /// <param name="ocbNum"></param>
        /// <param name="uCommand"></param>
        /// <returns></returns>
        [DllImport("lasal32.dll")]
        public static extern bool SetCommandH(int ocbNum, byte uCommand);

        // link modules in plc
        [DllImport("lasal32.dll")]
        public static extern bool LinkModulesH(int ocbNum);

        /// <summary>
        /// save project on plc
        /// </summary>
        /// <param name="ocbNum"></param>
        /// <returns></returns>
        [DllImport("lasal32.dll")]
        public static extern bool LslSaveH(int ocbNum);


        public enum FileUpdate : uint
        {
            UpdateStart = 0,
            UpdateCancel = 1,
            UpdateEnd = 2,
        }

        /// <summary>
        /// FileUpdateInfo
        /// </summary>
        /// <param name="ocbNum"></param>
        /// <param name="info"></param>
        [DllImport("lasal32.dll")]
        public static extern void FileUpdateInfoH(int ocbNum, FileUpdate info);

        /// <summary>
        /// send file to plc
        /// </summary>
        /// <param name="ocbNum"></param>
        /// <param name="dpne"></param>
        /// <param name="src"></param>
        /// <param name="len"></param>
        /// <param name="pCallback">retcode <> 0 = break</param>
        /// <param name="attrib"></param>
        /// <returns></returns>
        [DllImport("lasal32.dll")]
        public static extern bool FileSaveH(int ocbNum, [MarshalAs(UnmanagedType.LPStr)]string dpne, byte[] src, uint len, CB_FUNCTYPE pCallback, uint attrib);

        /// <summary>
        /// load referenced file from plc
        /// </summary>
        /// <param name="ocbNum"></param>
        /// <param name="dst"></param>
        /// <param name="dpne"></param>
        /// <param name="dstmaxlen"></param>
        /// <param name="pCallback"></param>
        /// <param name="offs"></param>
        /// <returns></returns>
        [DllImport("lasal32.dll")]
        public static extern bool FileLoadH(int ocbNum, byte[] dst, [MarshalAs(UnmanagedType.LPStr)]string dpne, uint dstmaxlen, CB_FUNCTYPE pCallback, uint offs);
        //BOOL LASAL32_EXPORTS FileLoadH(int ocbNum, char* RXbuf, char* dest, unsigned long datalen, CB_FUNCTYPE* callback, long offs);

        [DllImport("lasal32.dll")]
        private static extern bool FileInfoH(int ocbNum, byte[] dst, [MarshalAs(UnmanagedType.LPStr)]string dpne, uint buflen);
        //BOOL LASAL32_EXPORTS FileInfoH(int ocbNum, char* RXbuf, char* dest, unsigned long buflen);

        /// <summary>
        /// get fileinformation of referenced file from plc
        /// </summary>
        /// <param name="ocbNum"></param>
        /// <param name="dpne"></param>
        /// <param name="length"></param>
        /// <param name="date"></param>
        /// <param name="time"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public static bool FileInfoH(int ocbNum, string dpne, out uint length, out uint date, out uint time, out uint attribute)
        {
            length = 0;
            date = 0;
            time = 0;
            attribute = 0;

            //byte[] rx = new byte[100];
            //if (Lasal32.FileInfoH(ocbNum, rx, dpne, 20) == true)
            //{
            //    attribute = rx[11];
            //    uint tim;
            //    uint dat;

            //    //unsafe
            //    //{
            //    //    fixed (byte* ph = &rx[16])
            //    //    {
            //    //        length = *(uint*)ph;
            //    //    }
            //    //    fixed (byte* pt = &rx[14])
            //    //    {
            //    //        dat = *(ushort*)pt;
            //    //    }
            //    //    fixed (byte* pd = &rx[12])
            //    //    {
            //    //        tim = *(ushort*)pd;
            //    //    }
            //    //}

            //    // JJJJJJJJJJJJJJJJ MMMMTTTTTTTTWWWW
            //    uint day = (dat & 0x001F);
            //    uint mon = (dat & 0x01E0) >> 5;
            //    uint yea = 1980 + ((dat & 0xFE00) >> 9);
            //    date = day | (mon << 4) | (yea << 16);

            //    // HHHHHHHHMMMMMMMM SSSSSSSSxxxxxxxx                
            //    uint sec = (tim & 0x001F);
            //    uint min = (tim >> 5) & 0x003F;
            //    uint hou = (tim >> 11) & 0x001F;
            //    time = (sec << 8) | (min << 16) | (hou << 24);

            //    return true;
            //}
            return false;
        }

        /// <summary>
        /// perform ping
        /// </summary>
        /// <param name="ipaddress"></param>
        /// <param name="timeout_ms"></param>
        /// <param name="pinfo"></param>
        /// <returns></returns>
        [DllImport("lasal32.dll")]
        public static extern int LslPing([MarshalAs(UnmanagedType.LPStr)]string ipaddress, uint timeout_ms, byte[] pinfo);

        [DllImport("lasal32.dll")]
        public static extern bool LslGetDataListSpecialH(int ocbNum, uint dwCount, uint[] pAddr, uint[] pLen, uint[] pResult);

        // get 'nCount' byte from address 'addr0'
        [DllImport("lasal32.dll")]
        public static extern bool GetDataH(int ocbNum, out uint pBuffer, uint addr0, ushort nCount);

        [DllImport("lasal32.dll")]
        public static extern bool ServiceProviderH(int ocbNum, [MarshalAs(UnmanagedType.LPStr)]string stream);

        [DllImport("lasal32.dll")]
        public static extern bool Online(string szPort, byte uBaudrate, byte uPcStation, byte uSpsStation, byte uAutoInit);

        [DllImport("lasal32.dll")]
        public static extern void Offline();

        [DllImport("lasal32.dll")]
        public static extern bool LslWriteToSvr(int objAddr, int dValue);

        [DllImport("lasal32.dll")]
        public static extern bool LslGetObject(string objName, ref int objAddr, ref CpCommand pMode, string pszClsName);

        [DllImport("lasal32.dll")]
        public static extern bool LslGetObjCls(ref int objAddr, string ClassName);

        [DllImport("lasal32.dll")]
        public static extern bool LslReadFromSvr(int objAddr, ref int dValue);

        [DllImport("lasal32.dll")]
        public static extern bool IsOnline();

        [DllImport("lasal32.dll")]
        public static extern int GetLastError();

        [DllImport("lasal32.dll")]
        public static extern bool SetData(int[] pData, uint addr, UInt16 count);

        [DllImport("lasal32.dll")]
        public static extern bool GetData(int[] pData, uint addr, int length);

        [DllImport("lasal32.dll")]
        public static extern bool GetData(int[] pData, uint addr, UInt16 nCount);

        [DllImport("lasal32.dll")]
        public static extern bool GetData(uint[] pData, uint addr, int length);

        [DllImport("lasal32.dll")]
        public static extern bool LslExecNewInstr(int SrvAddress, short Cmd, int[] Paras, int ParaCnt, byte[] Result, int ResultCnt, int useLenField);

        public static string getString(string str1)
        {
            int add = 0;

            Lasal32.CpCommand mode = 0;

            Lasal32.LslGetObject(str1, ref add, ref mode, "");
            string str = Lasal32.LslReadString(add);

            return str;
        }

        public static string LslReadString(int objAddr)
        {
            int[] adwParas = new int[2];
            byte[] pResult = new byte[4010];
            int nStrLen;

            if (LslExecNewInstr(objAddr, 1, adwParas, 0, pResult, 4000, 1))
            {
                nStrLen = pResult[2];
                nStrLen += pResult[3] * 256;
                return System.Text.Encoding.Default.GetString(pResult, 6, nStrLen);
            }
            else
            {
                return "Error";
            }
        }
        //public static int LslWriteString(int objAddr, string NewText)
        //{

        //    int[] adwParas = new int[75];
        //    byte[] pResult = new byte[4010];
        //    int[] baNewText = new int[100];
        //    byte[] newASCIItext = new byte[238];
        //    byte[] result = new byte[256];
        //    int newStringlen;

        //    int ii;
        //    int DWORDcount = 0;
        //    newStringlen = NewText.Length;
        //    if (newStringlen > 238)
        //    {
        //        newStringlen = 238;
        //    }
        //    for (int i = 0; i < newStringlen; i++)
        //    {
        //        newASCIItext[i] = (Byte)NewText[i];
        //    }
        //    DWORDcount = newStringlen / 4;
        //    if ((DWORDcount * 4) < newStringlen)
        //    {
        //        DWORDcount = DWORDcount + 1;
        //    }
        //    DWORDcount = DWORDcount + 1;
        //    baNewText[0] = newStringlen;
        //    ii = 1;
        //    for (int i = 0; i < newStringlen; i += 4)
        //    {
        //        baNewText[ii] = newASCIItext[i + 3];
        //        baNewText[ii] *= 256;
        //        baNewText[ii] += newASCIItext[i + 2];
        //        baNewText[ii] *= 256;
        //        baNewText[ii] += newASCIItext[i + 1];
        //        baNewText[ii] *= 256;
        //        baNewText[ii] += newASCIItext[i];
        //        ii = ii + 1;
        //    }
        //    if (LslExecNewInstr(objAddr, (int)2, baNewText, (uint)DWORDcount, pResult, (uint)0, (uint)1))
        //    {
        //        return (int)DWORDcount;

        //    }
        //    else
        //    {
        //        return -1;
        //    }
        //}
    }
}
