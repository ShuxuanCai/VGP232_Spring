using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

namespace Week_3
{
    [Serializable]
    public class SkillCollection 
        : List<Skill>
        , IPersistence
        , IBinarySerializable
        , IXMLSerializable
        , IJSONSerializable
    {
        public bool Save(string fileName)
        {
            string extension = Path.GetExtension(fileName);

            if(extension == ".xml")
            {
                return SaveAsXML(fileName);
            }
            else if(extension == ".bin")
            {
                return SaveAsBinary(fileName);
            }
            else if(extension == ".json")
            {
                return SaveAsJSON(fileName);
            }

            return false;
        }
        public bool Load(string fileName)
        {
            string extension = Path.GetExtension(fileName);

            if (extension == ".xml")
            {
                return LoadXML(fileName);
            }
            else if (extension == ".bin")
            {
                return LoadBinary(fileName);
            }
            else if (extension == ".json")
            {
                return LoadJSON(fileName);
            }

            return false;
        }

        public bool SaveAsBinary(string fileName)
        {
            //FileStream fs = new FileStream(fileName, FileMode.Create);

            //BinaryFormatter bf = new BinaryFormatter();

            //bf.Serialize(fs, this);

            //return true;

            //Same
            BinaryFormatter bf = new BinaryFormatter();
            
            try
            {
                using(StreamWriter writer = new StreamWriter(fileName))
                {
                    bf.Serialize(writer.BaseStream, this);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }
        public bool LoadBinary(string fileName)
        {
            BinaryFormatter bf = new BinaryFormatter();
            
            try
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    this.Clear();
                    this.AddRange((SkillCollection)bf.Deserialize(reader.BaseStream));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        public bool SaveAsXML(string fileName)
        {
            XmlSerializer xml = new XmlSerializer(typeof(SkillCollection));

            try
            {
                using(StreamWriter writer = new StreamWriter(fileName))
                {
                    xml.Serialize(writer, this);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }
        public bool LoadXML(string fileName)
        {
            XmlSerializer xml = new XmlSerializer(typeof(SkillCollection));
            try
            {
                using(StreamReader reader = new StreamReader(fileName))
                {
                    this.Clear();
                    this.AddRange((SkillCollection)xml.Deserialize(reader));
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        public bool SaveAsJSON(string fileName)
        {
            try
            {
                using(StreamWriter writer = new StreamWriter(fileName))
                {
                    //Good
                    writer.Write(JsonSerializer.Serialize<SkillCollection>(this));
                    //Or(Same)
                    //writer.Write(JsonSerializer.Serialize(this, typeof(SkillCollection)));
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }
        public bool LoadJSON(string fileName)
        {
            try
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    //Good way
                    this.AddRange(JsonSerializer.Deserialize<SkillCollection>(reader.ReadToEnd()));
                    //this.AddRange((SkillCollection)JsonSerializer.Deserialize(reader.ReadToEnd(), typeof(SkillCollection)));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }
    }
}
