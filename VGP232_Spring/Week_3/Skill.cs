using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Week_3
{
    [Serializable]
    //[XmlRoot("Skills")]
    public class Skill
    {
        //[XmlAttribute("Name of Character")]
        public string Name { get; set; }
        //[XmlElement("cost")]
        public int Cost { get; set; }
        public int Modifier { get; set; }
        public int Damage { get; set; }

        public override string ToString()
        {
            return string.Format("Name: {0}     Cost: {1}     Modifier: {2}   Damege: {3}", Name, Cost, Modifier, Damage);
        }
    }
}
