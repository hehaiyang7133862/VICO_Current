using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nsDataMgr
{
    public class I_RD_SVRLIST_Stream : DataStream
    {
        // constructor
        public I_RD_SVRLIST_Stream(uint[] lasalid, uint no)
            : base(256)
        {
            Init(lasalid, no);
        }

        /// <summary>
        /// initialize I_RD_SVRLIST stream
        /// </summary>
        /// <param name="lasalid"></param>
        /// <param name="no"></param>
        public void Init(uint[] lasalid, uint no)
        {
            //unsafe
            //{
            //    fixed (byte* p08 = &base.data[0])
            //        *(ushort*)p08 = (ushort)(8 + (no * 4));
            //    fixed (byte* p08 = &base.data[2])
            //        *p08 = (byte)Lasal32.CpCommand.I_RD_SVRLIST;
            //    fixed (byte* p08 = &base.data[3])
            //        *(uint*)p08 = no;
            //    for (uint i = 0; i < no; i++)
            //    {
            //        fixed (byte* p08 = &base.data[7 + (i * 4)])
            //            *(uint*)p08 = lasalid[i];
            //    }

            //    fixed (byte* p08 = &base.data[7 + (no * 4)])
            //        *p08 = (byte)Lasal32.CpPrefix.P_EOL;
            //}
        }
    }
}
