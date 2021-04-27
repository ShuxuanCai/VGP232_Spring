using System;
using System.Collections.Generic;
using System.Text;

namespace Week_3
{
    interface IJSONSerializable
    {
        bool SaveAsJSON(string fileName);
        bool LoadJSON(string fileName);
    }
}
