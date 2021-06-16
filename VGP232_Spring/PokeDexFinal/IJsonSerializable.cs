using System;
using System.Collections.Generic;
using System.Text;

namespace PokeDexFinal
{
    public interface IJsonSerializable
    {
        bool LoadJSON(string path);
        bool SaveAsJSON(string path);
    }
}
