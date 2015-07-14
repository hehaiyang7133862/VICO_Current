using System;

namespace nsDataMgr
{
    public class IWRITE_Stream : DataStream
    {
        // constructor
        public IWRITE_Stream(uint lasalid, UInt32 value)
            : base(256)
        {
            Init(lasalid, value);
        }

        /// <summary>
        /// initialize write-stream
        /// </summary>
        /// <param name="lasalid"></param>
        /// <param name="value"></param>
        public void Init(uint lasalid, uint value)
        {
            Write16(13, 0);
            Write08((byte)Lasal32.CpCommand.I_WRITE, 2);
            Write32(lasalid, 3);
            Write08((byte)Lasal32.CpPrefix.P_IMMED, 7);
            Write32(value, 8);
            Write08((byte)Lasal32.CpPrefix.P_EOL, 12);
        }
    }
}
