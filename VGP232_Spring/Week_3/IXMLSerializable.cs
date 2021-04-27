using System;
using System.Collections.Generic;
using System.Text;

namespace Week_3
{
    interface IXMLSerializable
    {
        bool SaveAsXML(string fileName);
        bool LoadXML(string fileName);
    }
}
