using System;
using System.Collections.Generic;
using System.Text;

namespace Week_3
{
    interface IBinarySerializable
    {
        bool SaveAsBinary(string fileName);
        bool LoadBinary(string fileName);
    }
}
