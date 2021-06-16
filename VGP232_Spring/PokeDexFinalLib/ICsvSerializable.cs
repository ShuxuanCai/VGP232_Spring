using System;
using System.Collections.Generic;
using System.Text;

namespace PokeDexFinalLib
{
    public interface ICsvSerializable
    {
        bool LoadCSV(string path);
        bool SaveAsCSV(string path);
    }
}
