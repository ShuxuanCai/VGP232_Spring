﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2a
{
    public enum WeaponType
    {
        Sword,
        Polearm, 
        Claymore, 
        Catalyst, 
        Bow, 
        None
    }

    public class Weapon
    {
        // Name,Type,Rarity,BaseAttack
        public string Name { get; set; }
        public WeaponType Type { get; set; }
        public string Image { get; set; }
        public int Rarity { get; set; }
        public int BaseAttack { get; set; }
        public string SecondaryStat { get; set; }
        public string Passive { get; set; }

        public static bool TryParse(string rawData, out Weapon weapon)
        {
            string[] values = rawData.Split(',');
            weapon = new Weapon();

            if (values.Length == 7)
            {
                try
                {
                    weapon.Name = values[0];
                    Enum.TryParse(values[1], out WeaponType type);
                    weapon.Type = type;
                    weapon.Image = values[2];
                    int.TryParse(values[3], out int rarity);
                    weapon.Rarity = rarity;
                    int.TryParse(values[4], out int baseAttack);
                    weapon.BaseAttack = baseAttack;
                    weapon.SecondaryStat = values[5];
                    weapon.Passive = values[6];

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            else
            {
                throw new Exception("Wrong number properties of data!");
            }
        }

        /// <summary>
        /// The Comparator function to check for name
        /// </summary>
        /// <param name="left">Left side Weapon</param>
        /// <param name="right">Right side Weapon</param>
        /// <returns> -1 (or any other negative value) for "less than", 0 for "equals", or 1 (or any other positive value) for "greater than"</returns>
        public static int CompareByName(Weapon left, Weapon right)
        {
            return left.Name.CompareTo(right.Name);
        }

        // TODO: add sort for each property:
        // CompareByType
        public static int CompareByType(Weapon left, Weapon right)
        {
            return left.Type.CompareTo(right.Type);
        }

        public static int CompareByImage(Weapon left, Weapon right)
        {
            return left.Image.CompareTo(right.Image);
        }

        // CompareByRarity
        public static int CompareByRarity(Weapon left, Weapon right)
        {
            return left.Rarity - right.Rarity;
        }

        // CompareByBaseAttack
        public static int CompareByBaseAttack(Weapon left, Weapon right)
        {
            return left.BaseAttack - right.BaseAttack;
        }

        public static int CompareBySecondaryStat(Weapon left, Weapon right)
        {
            return left.SecondaryStat.CompareTo(right.SecondaryStat);
        }

        public static int CompareByPassive(Weapon left, Weapon right)
        {
            return left.Passive.CompareTo(right.Passive);
        }

        /// <summary>
        /// The Weapon string with all the properties
        /// </summary>
        /// <returns>The Weapon formated string</returns>
        public override string ToString()
        {
            // TODO: construct a comma seperated value string
            // Name,Type,Rarity,BaseAttack
            return $"{Name},{Type},{Image},{Rarity},{BaseAttack},{SecondaryStat},{Passive}";
        }
    }
}
