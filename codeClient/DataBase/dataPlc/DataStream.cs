using System;

namespace nsDataMgr
{
    // class is used to create arbitrary formatted datastream
    public class DataStream
    {
        protected byte[] data;

        // constructor
        public DataStream(UInt32 len)
        {
            this.data = new byte[len];
        }

        /// <summary>
        /// indexer
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public byte this[uint index]
        {
            get
            {
                if (this.data != null)
                {
                    if (index < this.data.Length)
                        return this.data[index];
                }
                return 0;
            }

            set
            {
                if (this.data != null)
                {
                    if (index < this.data.Length)
                        this.data[index] = value;
                }
            }
        }
        
        /// <summary>
        /// write unsigned 8-bit value into stream 
        /// </summary>
        /// <param name="val"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public bool Write08(byte val, uint offset)
        {
            //if(this.data != null)
            //{
            //    if((offset + 1) <= this.data.Length)
            //    {
            //        unsafe
            //        {
            //            fixed (byte* ptr = &this.data[offset])
            //                *ptr = val;
            //        }
            //        return true;
            //    }
            //}
            return false;
        }

        /// <summary>
        /// write unsigned 16-bit value into stream 
        /// </summary>
        /// <param name="val"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public bool Write16(ushort val, uint offset)
        {
            //if(this.data != null)
            //{
            //    if((offset + 2) <= this.data.Length)
            //    {
            //        unsafe
            //        {
            //            fixed (byte* ptr = &this.data[offset])
            //                *(ushort*)ptr = val;
            //        }
            //        return true;
            //    }
            //}
            return false;
        }

        /// <summary>
        /// write unsigned 32-bit value into stream 
        /// </summary>
        /// <param name="val"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public bool Write32(uint val, uint offset)
        {
            //if (this.data != null)
            //{
            //    if ((offset + 4) <= this.data.Length)
            //    {
            //        unsafe
            //        {
            //            fixed (byte* ptr = &this.data[offset])
            //                *(uint*)ptr = val;
            //        }
            //        return true;
            //    }
            //}
            return false;
        }

        /// <summary>
        /// write signed 8-bit value into stream 
        /// </summary>
        /// <param name="val"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public bool Write08Signed(sbyte val, uint offset)
        {
            //if (this.data != null)
            //{
            //    if ((offset + 1) <= this.data.Length)
            //    {
            //        unsafe
            //        {
            //            fixed (byte* ptr = &this.data[offset])
            //                *(sbyte*)ptr = val;
            //        }
            //        return true;
            //    }
            //}
            return false;
        }

        /// <summary>
        /// write signed 16-bit value into stream 
        /// </summary>
        /// <param name="val"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public bool Write16Signed(short val, uint offset)
        {
            //if (this.data != null)
            //{
            //    if ((offset + 2) <= this.data.Length)
            //    {
            //        unsafe
            //        {
            //            fixed (byte* ptr = &this.data[offset])
            //                *(short*)ptr = val;
            //        }
            //        return true;
            //    }
            //}
            return false;
        }

        /// <summary>
        /// write signed 32-bit value into stream 
        /// </summary>
        /// <param name="val"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public bool Write32Signed(int val, uint offset)
        {
            //if (this.data != null)
            //{
            //    if ((offset + 4) <= this.data.Length)
            //    {
            //        unsafe
            //        {
            //            fixed (byte* ptr = &this.data[offset])
            //                *(int*)ptr = val;
            //        }
            //        return true;
            //    }
            //}
            return false;
        }
        
        /// <summary>
        /// write 32-bit floating point value into stream
        /// </summary>
        /// <param name="val"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public bool WriteReal(float val, uint offset)
        {
            //if (this.data != null)
            //{
            //    if ((offset + 4) <= this.data.Length)
            //    {
            //        unsafe
            //        {
            //            fixed (byte* ptr = &this.data[offset])
            //                *(float*)ptr = val;
            //        }
            //        return true;
            //    }
            //}
            return false;
        }

        /// <summary>
        /// read unsigned 8-bit value from stream
        /// </summary>
        /// <param name="val"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public bool Read08(out byte val, uint offset)
        {
            //if (this.data != null)
            //{
            //    if ((offset + 1) <= this.data.Length)
            //    {
            //        unsafe
            //        {
            //            fixed (byte* ptr = &this.data[offset])
            //                val = *(byte*)ptr;
            //        }
            //        return true;
            //    }
            //}
            val = 0;
            return false;
        }

        /// <summary>
        /// read unsigned 16-bit value from stream
        /// </summary>
        /// <param name="val"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public bool Read16(out ushort val, uint offset)
        {
            //if (this.data != null)
            //{
            //    if ((offset + 2) <= this.data.Length)
            //    {
            //        unsafe
            //        {
            //            fixed (byte* ptr = &this.data[offset])
            //                val = *(ushort*)ptr;
            //        }
            //        return true;
            //    }
            //}
            val = 0;
            return false;
        }

        /// <summary>
        /// read unsigned 32-bit value from stream
        /// </summary>
        /// <param name="val"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public bool Read32(out uint val, uint offset)
        {
            //if (this.data != null)
            //{
            //    if ((offset + 4) <= this.data.Length)
            //    {
            //        unsafe
            //        {
            //            fixed (byte* ptr = &this.data[offset])
            //                val = *(uint*)ptr;
            //        }
            //        return true;
            //    }
            //}
            val = 0;
            return false;
        }

        /// <summary>
        /// read signed 8-bit value from stream
        /// </summary>
        /// <param name="val"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public bool Read08Signed(out sbyte val, uint offset)
        {
            //if (this.data != null)
            //{
            //    if ((offset + 1) <= this.data.Length)
            //    {
            //        unsafe
            //        {
            //            fixed (byte* ptr = &this.data[offset])
            //                val = *(sbyte*)ptr;
            //        }
            //        return true;
            //    }
            //}
            val = 0;
            return false;
        }

        /// <summary>
        /// read signed 16-bit value from stream
        /// </summary>
        /// <param name="val"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public bool Read16Signed(out short val, uint offset)
        {
            //if (this.data != null)
            //{
            //    if ((offset + 2) <= this.data.Length)
            //    {
            //        unsafe
            //        {
            //            fixed (byte* ptr = &this.data[offset])
            //                val = *(short*)ptr;
            //        }
            //        return true;
            //    }
            //}
            val = 0;
            return false;
        }

        /// <summary>
        /// read signed 32-bit value from stream
        /// </summary>
        /// <param name="val"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public bool Read32Signed(out int val, uint offset)
        {
            //if (this.data != null)
            //{
            //    if ((offset + 4) <= this.data.Length)
            //    {
            //        unsafe
            //        {
            //            fixed (byte* ptr = &this.data[offset])
            //                val = *(int*)ptr;
            //        }
            //        return true;
            //    }
            //}
            val = 0;
            return false;
        }
        
        /// <summary>
        /// read 32-bit floating point value
        /// </summary>
        /// <param name="val"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public bool ReadReal(out float val, uint offset)
        {
            //if (this.data != null)
            //{
            //    if ((offset + 4) <= this.data.Length)
            //    {
            //        unsafe
            //        {
            //            fixed (byte* ptr = &this.data[offset])
            //                val = *(float*)ptr;
            //        }
            //        return true;
            //    }
            //}
            val = 0;
            return false;
        }

        /// <summary>
        /// get stream
        /// </summary>
        /// <returns></returns>
        public byte[] GetData()
        {
            return this.data;
        }

        /// <summary>
        /// copy data into stream
        /// </summary>
        /// <param name="rx"></param>
        public void SetData(byte[] rx)
        {
            if (rx != null)
            {
                int len = rx.Length;
                if (len > this.data.Length)
                    len = this.data.Length;

                for (int i = 0; i < len; i++)
                    this.data[i] = rx[i];
            }

        }

        /// <summary>
        /// get length of datastream
        /// </summary>
        /// <returns></returns>
        public uint GetLength()
        {
            return (uint)this.data.Length;
        }


    }

}
