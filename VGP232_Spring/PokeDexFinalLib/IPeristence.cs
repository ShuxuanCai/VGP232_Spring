using System;
using System.Collections.Generic;
using System.Text;

namespace PokeDexFinalLib
{
    public interface IPeristence
    {
        bool Load(string filename);
        bool Save(string filename);
    }
}
