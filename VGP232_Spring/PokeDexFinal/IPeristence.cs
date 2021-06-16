using System;
using System.Collections.Generic;
using System.Text;

namespace PokeDexFinal
{
    public interface IPeristence
    {
        bool Load(string filename);
        bool Save(string filename);
    }
}
