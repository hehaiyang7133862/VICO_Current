using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nsDataMgr
{
    public class dvClass
    {
        objUnit[] items = new objUnit[2000];
        public objUnit this[int index]
        {
            get
            {
                if (index <= 0 || index >= items.Length)
                    return null;
                return items[index];
            }
            set
            {
                items[index] = value;
            }

        }
        public int length
        {
            get
            {
                return _maxLen;
            }
        }
        int _maxLen = 2000;
        public int maxLen
        {
            set
            {
                if (value > items.Length)
                    _maxLen = items.Length;
                else
                    _maxLen = value;
            }
        }
    }
}
