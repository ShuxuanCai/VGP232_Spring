using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Week_3
{
    class Program
    {
        static void Main(string[] args)
        {
            //string binaryFileName = "mySkill.dat";
            ////SaveSkill(binaryFileName);
            ////LoadSkill(binaryFileName);

            //string xmlFileName = "mySkill.xml";
            //Skill mySkill = new Skill() { Name = "Thunder Strike", Cost = 5, Modifier = 1 };

            //FileStream fs = new FileStream(xmlFileName, FileMode.Create);
            //XmlSerializer xmlWriter = new XmlSerializer(typeof(Skill));
            //xmlWriter.Serialize(fs, mySkill);
            //Console.WriteLine("xml file serialize.");

            //.bin/ .xml/ .json
            string saveedFile = "skillbook.xml";

            SkillCollection sc = new SkillCollection()
            {
                new Skill(){Name="Fireball", Cost = 5, Modifier = 5, Damage = 100 },
                new Skill(){Name="IceBolt", Cost = 8, Modifier = 3, Damage = 46 },
                new Skill(){Name="Thunder Strike", Cost = 3, Modifier = 5, Damage = 35 },
                new Skill(){Name="Magic Arrow", Cost = 4, Modifier = 10, Damage = 64 },
                new Skill(){Name="Rock Throw", Cost = 2, Modifier = 8, Damage = 10 },
                new Skill(){Name="Sleep", Cost = 1, Modifier = 1, Damage = 1 }
            };

            sc.Save(saveedFile);

            SkillCollection sc_1 = new SkillCollection();
            sc_1.Load(saveedFile);

            foreach(var skill in sc_1)
            {
                Console.WriteLine(skill);
            }

            Console.WriteLine("Done!");
        }

        private static void LoadSkill(string fileName)
        {
            //1 - Open a file: Read
            FileStream fs = new FileStream(fileName, FileMode.Open);
            //2 - Create a BinaryFormatter
            BinaryFormatter bf = new BinaryFormatter();
            //3 - Create a new object
            //4 - Deserialize
            Skill loadedSkill = (Skill)bf.Deserialize(fs);
            Console.WriteLine("Binary file loaded!");
            Console.WriteLine(loadedSkill);
        }

        private static void SaveSkill(string fileName, Skill mySkill)
        {
            mySkill = new Skill() { Name = "Thunder Strike", Cost = 5, Modifier = 1 };

            //1 - Opening a file
            FileStream fs = new FileStream(fileName, FileMode.Create);
            //2 - Create a BinaryFormatter
            BinaryFormatter bf = new BinaryFormatter();
            //3 - Serialize your file
            bf.Serialize(fs, mySkill);

            fs.Close();
            Console.WriteLine("Binary file saved!");
        }
    }
}
