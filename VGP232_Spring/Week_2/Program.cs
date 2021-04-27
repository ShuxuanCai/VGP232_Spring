using System;

namespace Week_2
{
    class Program
    {
        public enum DayOfWeek
        {
            Monday,
            Tuesday,
            Wednesday,
            Size
        }

        public enum Fruits
        {
            Banana,
            Apple,
            Watermalon,
            Size
        }

        static void RunningSum(int left, ref int total)
        {
            total += left;
        }

        static void OutSum(int left, int right, out int total)
        {
            total = left + right;
        }

        static int Sum(int[] numbers)
        {
            int total = 0;
            for(int i = 0; i < numbers.Length; ++i)
            {
                total += numbers[i];
            }
            return total;
        }
        
        static int SumParams(params int[] numbers)
        {
            int total = 0;
            for (int i = 0; i < numbers.Length; ++i)
            {
                total += numbers[i];
            }
            return total;
        }

        //Optionals
        static void PrintAName(string name = "Default", int age = 0) // after default can add optional paramters
        {
            Console.WriteLine(name);
        }

        //Name Parameters
        static void SetConfiguration(bool fullScreenEnables, ref int width, out int height)
        {
            height = 5;
            Console.WriteLine("Configuration set.");
        }

        static void Main(string[] args)
        {
            //TestMethod();

            CharacterCollection characters = new CharacterCollection();
            characters.Add(new Character("Jhon", 50, 30));
            characters.RemoveAt(0);

            int maxHP = characters.GetMaxHPFromCharacters();
        }

        private static void TestMethod()
        {
            Console.WriteLine("Hello World!");

            Fruits myFruit = Fruits.Apple;

            string myStringFruit = "Apple";

            try
            {
                Fruits myFruit2 = (Fruits)Enum.Parse(typeof(Fruits), myStringFruit);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot convert this value into a fruit");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Finishing...");
            }

            Fruits myFruit3;
            if (Enum.TryParse<Fruits>(myStringFruit, out myFruit3))
            {

            }
            else
            {

            }

            int total = 0;
            RunningSum(7, ref total);
            Console.WriteLine(total);

            int total2;
            OutSum(4, 2, out total2);
            Console.WriteLine(total2);

            int[] Numbers = { 1, 2, 3, 4, 5, 6, 7 };
            Sum(Numbers);

            SumParams(1, 2, 3, 4, 5, 6, 7);

            PrintAName();
            PrintAName("Chris");

            SetConfiguration(height: out total2, fullScreenEnables: false, width: ref total);
        }
    }
}
