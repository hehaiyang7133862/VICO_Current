using System;

namespace nsDataMgr
{
    public class IRESULT_Stream : DataStream
    {
        // constructor
        public IRESULT_Stream()
            : base(280)
        {
            Init();
        }

        /// <summary>
        /// initialize stream 
        /// </summary>
        public void Init()
        {
            base.data[0] = 0xFF;
            base.data[1] = 0xFF;

//            Write16(0xFFFF, 0); // result
        }

        /// <summary>
        /// check if result is valid 
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            uint result;
            if (Read32(out result, 0) == true)
            {
                if (result == Lasal32.DONE)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// get length of result in byte 
        /// </summary>
        /// <returns></returns>
        public ushort GetResultLengthInByte()
        {
            if (IsValid() == true)
            {
                ushort retcode;
                if (Read16(out retcode, 4) == true)
                {
                    if(retcode < base.GetLength())
                        return retcode;
                }
            }
            return 0;
        }

        /// <summary>
        /// get unsigned 32-bit resultparameter by using index  
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="index">0, 1, 2, 3, ...</param>
        /// <returns></returns>
        public bool GetParameter(out uint parameter, uint index)
        {
            uint len = GetResultLengthInByte();
            if (index < ((len - 2) / 4))
            {
                return Read32(out parameter, 6 + index * 4);
            }

            parameter = 0;
            return (false);
        }

        /// <summary>
        /// get signed 32-bit resultparameter by using index  
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="index">0, 1, 2, 3, ...</param>
        /// <returns></returns>
        public bool GetParameterSigned(out int parameter, uint index)
        {
            uint len = GetResultLengthInByte();
            if (index < ((len - 2) / 4))
            {
                return Read32Signed(out parameter, 6 + index * 4);
            }

            parameter = 0;
            return (false);
        }
        
        /// <summary>
        /// get size of entire resultbuffer
        /// </summary>
        /// <returns></returns>
        public uint GetSize()
        {
            uint len = GetLength();
            if (len > 2)
                return 260; // 256;

            return 0;
        }

    }
}
