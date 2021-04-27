using System;
using System.Collections.Generic;
using System.Text;

namespace Week_2
{
    public class CharacterCollection : List<Character>
    {
        void SortBy(string propertyName)
        {
            if(propertyName == "HP")
            {
                this.Sort(CompareHP);
            }
        }

        private int CompareHP(Character x, Character y)
        {
            return x.HP - y.HP;
        }

        public int GetMaxHPFromCharacters()
        {
            int maxHP = 0;
            foreach(var character in this)
            {
                if(character.HP > maxHP)
                {
                    maxHP = character.HP;
                }
            }

            //Same with foreach
            //for(int i = 0; i < this.Count; ++i)
            //{
            //    if(this[i].HP > maxHP)
            //    {
            //        maxHP = this[i].HP;
            //    }
            //}

            return maxHP;
        }
    }
}
