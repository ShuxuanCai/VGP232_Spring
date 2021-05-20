using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WeaponLib
{
    public class WeaponCollection
        : List<Weapon>
        , IPeristence
        , IXmlSerializable
        , IJsonSerializable
        , ICsvSerializable
    {
        public int GetHighestBaseAttack()
        {
            int highestBaseAttack = 0;
            foreach (var i in this)
            {
                if (i.BaseAttack > highestBaseAttack)
                {
                    highestBaseAttack = i.BaseAttack;
                }
            }
            return highestBaseAttack;
        }

        public int GetLowestBaseAttack()
        {
            int lowestBaseAttack = int.MaxValue;
            foreach (var i in this)
            {
                if (i.BaseAttack < lowestBaseAttack)
                {
                    lowestBaseAttack = i.BaseAttack;
                }
            }
            return lowestBaseAttack;
        }

        public List<Weapon> GetAllWeaponsOfType(WeaponType type)
        {
            List<Weapon> weapon = new List<Weapon>();
            foreach (var i in this)
            {
                if (i.Type == type)
                {
                    weapon.Add(i);
                }
            }
            return weapon;
        }

        public List<Weapon> GetAllWeaponsOfRarity(int stars)
        {
            List<Weapon> weapon = new List<Weapon>();
            foreach (var i in this)
            {
                if (i.Rarity == stars)
                {
                    weapon.Add(i);
                }
            }
            return weapon;
        }

        public void SortBy(string columnName)
        {
            switch (columnName.ToLower())
            {
                case "name":
                    this.Sort(Weapon.CompareByName);
                    break;
                case "type":
                    this.Sort(Weapon.CompareByType);
                    break;
                case "image":
                    this.Sort(Weapon.CompareByImage);
                    break;
                case "rarity":
                    this.Sort(Weapon.CompareByRarity);
                    break;
                case "baseattack":
                    this.Sort(Weapon.CompareByBaseAttack);
                    break;
                case "secondarystat":
                    this.Sort(Weapon.CompareBySecondaryStat);
                    break;
                case "passive":
                    this.Sort(Weapon.CompareByPassive);
                    break;
                default:
                    Console.WriteLine("Wrong column name!");
                    break;
            }
        }

        //ERROR: -2. You are always adding in your load. You should first clear your list and then add.
        public bool Load(string filename)
        {
            this.Clear();
            string extension = Path.GetExtension(filename);

            try
            {
                if (extension == ".xml")
                {
                    return LoadXML(filename);
                }
                else if (extension == ".json")
                {
                    return LoadJSON(filename);
                }
                else if (extension == ".csv")
                {
                    return LoadCSV(filename);
                }
            }

            catch
            {
                Console.WriteLine("This extension type is not supported.");
                return false;
            }

            return false;
        }

        public bool Save(string filename)
        {
            string extension = Path.GetExtension(filename);

            try
            {
                if (extension == ".xml")
                {
                    return SaveAsXML(filename);
                }
                else if (extension == ".json")
                {
                    return SaveAsJSON(filename);
                }
                else if (extension == ".csv")
                {
                    return SaveAsCSV(filename);
                }
            }

            catch
            {
                Console.WriteLine("This extension type is not supported.");
                return false;
            }

            return false;
        }

        public bool LoadXML(string path)
        {
            XmlSerializer xml = new XmlSerializer(typeof(WeaponCollection));

            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    this.Clear();
                    this.AddRange((WeaponCollection)xml.Deserialize(reader));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        public bool SaveAsXML(string path)
        {
            XmlSerializer xml = new XmlSerializer(typeof(WeaponCollection));

            try
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    xml.Serialize(writer, this);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        public bool LoadJSON(string path)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    this.Clear();
                    this.AddRange(JsonSerializer.Deserialize<WeaponCollection>(reader.ReadToEnd()));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        public bool SaveAsJSON(string path)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.Write(JsonSerializer.Serialize<WeaponCollection>(this));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        public bool LoadCSV(string path)
        {
            this.Clear();

            using (StreamReader reader = new StreamReader(path))
            {
                string header = reader.ReadLine();

                while (reader.Peek() > 0)
                {
                    string line = reader.ReadLine();
                    if (Weapon.TryParse(line, out Weapon weapon))
                    {
                        this.Add(weapon);
                    }
                }
            }

            return true;

        }

        public bool SaveAsCSV(string path)
        {
            FileStream fs;

            if (File.Exists(path))
            {
                fs = File.Open(path, FileMode.Append);
            }
            else
            {
                fs = File.Open(path, FileMode.Create);
            }

            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.WriteLine("Name,Type,Image,Rarity,BaseAttack,SecondaryStat,Passive");

                foreach (var j in this)
                {
                    writer.WriteLine(j);
                }

                Console.WriteLine("The file has been saved.");
            }

            return true;
        }
    }
}
