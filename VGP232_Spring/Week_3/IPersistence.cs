using System;
using System.Collections.Generic;
using System.Text;

namespace Week_3
{
    interface IPersistence
    {
        bool Save(string fileName);
        bool Load(string fileName);
    }
}
