using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nsDataMgr
{
    public class I_WR_SVRLIST_Stream : DataStream
    {
        // constructor
        public I_WR_SVRLIST_Stream(uint[] lasalid, uint[] value, uint no)
            : base(256)
        {
            Init(lasalid, value, no);
        }

        /// <summary>
        /// initialize I_WR_SVRLIST stream
        /// </summary>
        /// <param name="lasalid"></param>
        /// <param name="no"></param>
        public void Init(uint[] lasalid, uint[] value, uint no)
        {
            //unsafe
            //{
            //    fixed (byte* p08 = &base.data[0])
            //        *(ushort*)p08 = (ushort)(8 + (no * 8));
            //    fixed (byte* p08 = &base.data[2])
            //        *p08 = (byte)Lasal32.CpCommand.I_WR_SVRLIST;
            //    fixed (byte* p08 = &base.data[3])
            //        *(uint*)p08 = no;

            //    for (uint i = 0; i < no; i++)
            //    {
            //        Write32(lasalid[i], 7 + (i * 8));
            //        Write32(value[i], 7 + (i * 8) + 4);
            //    }

            //    fixed (byte* p08 = &base.data[7 + (no * 8)])
            //        *p08 = (byte)Lasal32.CpPrefix.P_EOL;
            //}
        }

    }
}
