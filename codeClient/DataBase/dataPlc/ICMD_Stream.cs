using System;

namespace nsDataMgr
{
    public class ICMD_Stream : DataStream
    {
        // constructor
        public ICMD_Stream(uint lasalid, ushort functionnumber)
            : base(256)
        {
            Init(lasalid, functionnumber);
        }

        /// <summary>
        /// reinitialize stream 
        /// </summary>
        /// <param name="lasalid"></param>
        /// <param name="functionnumber"></param>
        public void Init(uint lasalid, ushort functionnumber)
        {
            Write16(10, 0); // length
            Write08((byte)Lasal32.CpCommand.I_CMD, 2); // I_CMD
            Write32(lasalid, 3); // LsalId
            Write16(functionnumber, 7); // FunctionNo
            Write08((byte)Lasal32.CpPrefix.P_EOL, 9); // EOL
        }

        /// <summary>
        /// add unsigned 32-bit constant value to stream 
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public bool AddConstant(uint para)
        {
            ushort pos;
            if (Read16(out pos, 0) == true)
            {
                if (pos > 0)
                {
                    pos -= 1;
                    if (pos > 9)
                        Write08((byte)Lasal32.CpPrefix.P_COMMA, pos++); // comma
                    Write08((byte)Lasal32.CpPrefix.P_IMMED, pos++); // P_IMMED
                    Write32(para, pos); pos += 4; // value
                    Write08((byte)Lasal32.CpPrefix.P_EOL, pos++); // EOL
                    return Write16(pos, 0); // correct length
                }
            }
            return false;
        }

        /// <summary>
        /// add signed 32-bit constant value to stream 
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public bool AddConstantSigned(int para)
        {
            ushort pos;
            if (Read16(out pos, 0) == true)
            {
                if (pos > 0)
                {
                    pos -= 1;
                    if (pos > 9)
                        Write08((byte)Lasal32.CpPrefix.P_COMMA, pos++); // comma
                    Write08((byte)Lasal32.CpPrefix.P_IMMED, pos++); // P_IMMED
                    Write32Signed(para, pos); pos += 4; // value
                    Write08((byte)Lasal32.CpPrefix.P_EOL, pos++); // EOL
                    return Write16(pos, 0); // correct length
                }
            }
            return false;
        }
        
        /// <summary>
        /// get lasalid out of stream
        /// </summary>
        /// <returns></returns>
        public uint GetLasalID()
        {
            uint retcode;
            if(Read32(out retcode, 3) == true)
                return retcode;
            return 0;
        }
    }

}
