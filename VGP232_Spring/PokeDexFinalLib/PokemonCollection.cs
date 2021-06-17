using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

namespace PokeDexFinalLib
{
    public class PokemonCollection
        : List<PokemonInfo>
        , IPeristence
        , IXmlSerializable
        , IJsonSerializable
        , ICsvSerializable
    {
        public List<PokemonInfo> pokemons { get; set; }

        public List<PokemonInfo> GetAllPokemonsOfType(PokemonType type)
        {
            List<PokemonInfo> pokemons = new List<PokemonInfo>();
            foreach (var i in this)
            {
                if (i.TypeI == type || i.TypeII == type)
                {
                    pokemons.Add(i);
                }
            }
            return pokemons;
        }

        public List<PokemonInfo> GetAllPokemonsOfType2(PokemonType type1, PokemonType type2)
        {
            List<PokemonInfo> pokemons = new List<PokemonInfo>();
            foreach (var i in this)
            {
                if ((i.TypeI == type1 && i.TypeII == type2) || (i.TypeI == type2 && i.TypeII == type1))
                {
                    pokemons.Add(i);
                }
            }
            return pokemons;
        }

        public int GetAllPokemonsOfTotal()
        {
            int total = 0;
            foreach (var i in this)
            {
                total += i.Total;
            }
            return total;
        }

        public void SortBy(string columnName)
        {
            switch (columnName.ToLower())
            {
                case "nat":
                    this.Sort(PokemonInfo.CompareByNat);
                    break;
                case "name":
                    this.Sort(PokemonInfo.CompareByName);
                    break;
                case "hp":
                    this.Sort(PokemonInfo.CompareByHP);
                    break;
                case "atk":
                    this.Sort(PokemonInfo.CompareByAtk);
                    break;
                case "def":
                    this.Sort(PokemonInfo.CompareByDef);
                    break;
                case "spa":
                    this.Sort(PokemonInfo.CompareBySpA);
                    break;
                case "spd":
                    this.Sort(PokemonInfo.CompareBySpD);
                    break;
                case "spe":
                    this.Sort(PokemonInfo.CompareBySpe);
                    break;
                case "total":
                    this.Sort(PokemonInfo.CompareByTotal);
                    break;
                case "typei":
                    this.Sort(PokemonInfo.CompareByTypeI);
                    break;
                case "typeii":
                    this.Sort(PokemonInfo.CompareByTypeII);
                    break;
                default:
                    Console.WriteLine("Wrong column name!");
                    break;
            }
        }

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
            XmlSerializer xml = new XmlSerializer(typeof(PokemonCollection));

            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    this.Clear();
                    this.AddRange((PokemonCollection)xml.Deserialize(reader));
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
            XmlSerializer xml = new XmlSerializer(typeof(PokemonCollection));

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
                    this.AddRange(JsonSerializer.Deserialize<PokemonCollection>(reader.ReadToEnd()));
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
                    writer.Write(JsonSerializer.Serialize<PokemonCollection>(this));
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
                    if (PokemonInfo.TryParse(line, out PokemonInfo weapon))
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
                writer.WriteLine("Nat,Name,HP,Atk,Def,SpA,SpD,Spe,Total,TypeI,TypeII");

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
