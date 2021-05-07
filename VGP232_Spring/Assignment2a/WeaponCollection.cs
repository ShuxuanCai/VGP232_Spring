using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2a
{
    public class WeaponCollection : List<Weapon>, IPeristence
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
            foreach(var i in this)
            {
                if(i.Type == type)
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
            if (string.IsNullOrEmpty(filename))
            {
                Console.WriteLine("No input file specified.");
                return false;
            }

            else if (!File.Exists(filename))
            {
                Console.WriteLine("The file specified does not exist.");
                return false;
            }

            else
            {
                using (StreamReader reader = new StreamReader(filename))
                {
                    string header = reader.ReadLine();

                    while (reader.Peek() > 0)
                    {
                        string line = reader.ReadLine();
                        if(Weapon.TryParse(line, out Weapon weapon))
                        {
                            this.Add(weapon);
                        }
                    }
                }

                return true;
            }
        }

        public bool Save(string filename)
        {
            if (!string.IsNullOrEmpty(filename))
            {
                FileStream fs;

                if (File.Exists(filename))
                {
                    fs = File.Open(filename, FileMode.Append);
                }
                else
                {
                    fs = File.Open(filename, FileMode.Create);
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

            else
            {
                return false;
            }
        }
    }
}
