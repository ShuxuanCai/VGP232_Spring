﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PokeDexFinal
{
    public enum PokemonType
    {
        None,
        Fire,
        Water,
        Grass,
        Poison,
        Flying,
        Dragon,
        Bug,
        Normal,
        Electric,
        Ground,
        Fairy,
        Fighting,
        Psychic,
        Rock,
        Steel,
        Ice,
        Ghost,
        Dark
    }

    public class PokemonInfo
    {
        public string Nat { get; set; }
        public string Name { get; set; }
        public int HP { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int SpA { get; set; }
        public int SpD { get; set; }
        public int Spe { get; set; }
        public int Total { get; set; }
        public PokemonType TypeI { get; set; }
        public PokemonType TypeII { get; set; }


        public static bool TryParse(string rawData, out PokemonInfo pokemonInfo)
        {
            string[] values = rawData.Split(',');
            pokemonInfo = new PokemonInfo();

            try
            {
                if (values.Length == 10 || values.Length == 11)
                {
                    pokemonInfo.Nat = values[0];
                    pokemonInfo.Name = values[1];
                    int.TryParse(values[2], out int hp);
                    pokemonInfo.HP = hp;
                    int.TryParse(values[3], out int atk);
                    pokemonInfo.Atk = atk;
                    int.TryParse(values[4], out int def);
                    pokemonInfo.Def = def;
                    int.TryParse(values[5], out int spa);
                    pokemonInfo.SpA = spa;
                    int.TryParse(values[6], out int spd);
                    pokemonInfo.SpD = spd;
                    int.TryParse(values[7], out int spe);
                    pokemonInfo.Spe = spe;
                    int.TryParse(values[8], out int total);
                    pokemonInfo.Total = total;
                    Enum.TryParse(values[9], out PokemonType type1);
                    pokemonInfo.TypeI = type1;
                    Enum.TryParse(values[10], out PokemonType type2);
                    pokemonInfo.TypeII = type2;

                    return true;
                }
                else
                {
                    throw new Exception("Wrong number properties of data!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static int CompareByNat(PokemonInfo left, PokemonInfo right)
        {
            return left.Nat.CompareTo(right.Nat);
        }

        public static int CompareByName(PokemonInfo left, PokemonInfo right)
        {
            return left.Name.CompareTo(right.Name);
        }

        public static int CompareByHP(PokemonInfo left, PokemonInfo right)
        {
            return left.HP.CompareTo(right.HP);
        }

        public static int CompareByAtk(PokemonInfo left, PokemonInfo right)
        {
            return left.Atk.CompareTo(right.Atk);
        }

        public static int CompareByDef(PokemonInfo left, PokemonInfo right)
        {
            return left.Def.CompareTo(right.Def);
        }

        public static int CompareBySpA(PokemonInfo left, PokemonInfo right)
        {
            return left.SpA.CompareTo(right.SpA);
        }

        public static int CompareBySpD(PokemonInfo left, PokemonInfo right)
        {
            return left.SpD.CompareTo(right.SpD);
        }

        public static int CompareBySpe(PokemonInfo left, PokemonInfo right)
        {
            return left.Spe.CompareTo(right.Spe);
        }

        public static int CompareByTotal(PokemonInfo left, PokemonInfo right)
        {
            return left.Total.CompareTo(right.Total);
        }

        public static int CompareByTypeI(PokemonInfo left, PokemonInfo right)
        {
            return left.TypeI.CompareTo(right.TypeI);
        }

        public static int CompareByTypeII(PokemonInfo left, PokemonInfo right)
        {
            return left.TypeII.CompareTo(right.TypeII);
        }

        public override string ToString()
        {
            //return $"{Nat},{Name},{HP},{Atk},{Def},{SpA},{SpD},{Spe},{Total},{TypeI},{TypeII}";
            return string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}", Nat, Name, HP, Atk, Def,
            SpA, SpD, Spe, Total, TypeI.ToString(), TypeII == PokemonType.None ? "" : TypeII.ToString());
        }
    }
}
