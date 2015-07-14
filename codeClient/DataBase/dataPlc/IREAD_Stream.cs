using System;

namespace nsDataMgr
{
    public class IREAD_Stream : DataStream
    {
        // constructor
        public IREAD_Stream(uint lasalid)
            : base(256)
        {
            Init(lasalid);
        }

        /// <summary>
        /// initialize read-stream
        /// </summary>
        /// <param name="lasalid"></param>
        public void Init(uint lasalid)
        {
            //Write16(9, 0);
            //Write08((byte)Lasal32.CpCommand.I_READ, 2);
            //Write08((byte)Lasal32.CpPrefix.P_VARIA, 3); 
            //Write32(lasalid, 4);
            //Write08((byte)Lasal32.CpPrefix.P_EOL, 8);

            base.data[0] = 9;
            base.data[1] = 0;
            base.data[2] = (byte)Lasal32.CpCommand.I_READ;
            base.data[3] = (byte)Lasal32.CpPrefix.P_VARIA;

            //unsafe
            //{
            //    fixed (byte* p08 = &base.data[4])
            //        *(uint*)p08 = lasalid;
            //}

            base.data[8] = (byte)Lasal32.CpPrefix.P_EOL;

            //unsafe
            //{
            //    fixed (byte* p08 = &base.data[0])
            //        *(ushort*)p08 = (ushort)9;
            //    fixed (byte* p08 = &base.data[2])
            //        *p08 = (byte)Lasal32.CpCommand.I_READ;
            //    fixed (byte* p08 = &base.data[3])
            //        *p08 = (byte)Lasal32.CpPrefix.P_VARIA;
            //    fixed (byte* p08 = &base.data[4])
            //        *(uint*)p08 = lasalid;
            //    fixed (byte* p08 = &base.data[8])
            //        *p08 = (byte)Lasal32.CpPrefix.P_EOL;
            //}
        }
    }
}
