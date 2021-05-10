using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2b
{
    [TestFixture]
    public class UnitTests
    {
        private WeaponCollection weaponCollection;
        private WeaponCollection empty;
        private string inputPath;
        private string outputPath;

        private string outputPathXML;
        private string outputPathJSON;
        private string outputPathCSV;
        private string outputPathXMLEmpty;
        private string outputPathJSONEmpty;
        private string outputPathCSVEmpty;

        const string INPUT_FILE = "data2.csv";
        const string OUTPUT_FILE = "output.csv";

        const string OUTPUT_FILE_XML = "weapons.xml";
        const string OUTPUT_FILE_JSON = "weapons.json";
        const string OUTPUT_FILE_CSV = "weapons.csv";
        const string OUTPUT_FILE_XML_EMPTY = "empty.xml";
        const string OUTPUT_FILE_JSON_EMPTY = "empty.json";
        const string OUTPUT_FILE_CSV_EMPTY = "empty.csv";

        // A helper function to get the directory of where the actual path is.
        private string CombineToAppPath(string filename)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);
        }

        [SetUp]
        public void SetUp()
        {
            inputPath = CombineToAppPath(INPUT_FILE);
            outputPath = CombineToAppPath(OUTPUT_FILE);
            outputPathXML = CombineToAppPath(OUTPUT_FILE_XML);
            outputPathJSON = CombineToAppPath(OUTPUT_FILE_JSON);
            outputPathCSV = CombineToAppPath(OUTPUT_FILE_CSV);
            outputPathXMLEmpty = CombineToAppPath(OUTPUT_FILE_XML_EMPTY);
            outputPathJSONEmpty = CombineToAppPath(OUTPUT_FILE_JSON_EMPTY);
            outputPathCSVEmpty = CombineToAppPath(OUTPUT_FILE_CSV_EMPTY);
            weaponCollection = new WeaponCollection();
            empty = new WeaponCollection();
            weaponCollection.Load(inputPath);
        }

        [TearDown]
        public void CleanUp()
        {
            // We remove the output file after we are done.
            if (File.Exists(outputPath))
            {
                File.Delete(outputPath);
            }
            if (File.Exists(outputPathXML))
            {
                File.Delete(outputPathXML);
            }
            if (File.Exists(outputPathJSON))
            {
                File.Delete(outputPathJSON);
            }
            if (File.Exists(outputPathCSV))
            {
                File.Delete(outputPathCSV);
            }
            if (File.Exists(outputPathXMLEmpty))
            {
                File.Delete(outputPathXMLEmpty);
            }
            if (File.Exists(outputPathJSONEmpty))
            {
                File.Delete(outputPathJSONEmpty);
            }
            if (File.Exists(outputPathCSVEmpty))
            {
                File.Delete(outputPathCSVEmpty);
            }
        }

        // WeaponCollection Unit Tests
        [Test]
        public void WeaponCollection_GetHighestBaseAttack_HighestValue()
        {
            // Expected Value: 48
            // TODO: call WeaponCollection.GetHighestBaseAttack() and confirm that it matches the expected value using asserts.

            int highestBaseAttack = weaponCollection.GetHighestBaseAttack();
            Assert.AreEqual(highestBaseAttack, 48);
        }

        [Test]
        public void WeaponCollection_GetLowestBaseAttack_LowestValue()
        {
            // Expected Value: 23
            // TODO: call WeaponCollection.GetLowestBaseAttack() and confirm that it matches the expected value using asserts.

            int lowestBaseAttack = weaponCollection.GetLowestBaseAttack();
            Assert.AreEqual(lowestBaseAttack, 23);
        }

        [TestCase(WeaponType.Sword, 21)]
        public void WeaponCollection_GetAllWeaponsOfType_ListOfWeapons(WeaponType type, int expectedValue)
        {
            // TODO: call WeaponCollection.GetAllWeaponsOfType(type) and confirm that the weapons list returns Count matches the expected value using asserts.

            int count = weaponCollection.GetAllWeaponsOfType(type).Count();
            Assert.AreEqual(count, expectedValue);
        }

        [TestCase(5, 10)]
        public void WeaponCollection_GetAllWeaponsOfRarity_ListOfWeapons(int stars, int expectedValue)
        {
            // TODO: call WeaponCollection.GetAllWeaponsOfRarity(stars) and confirm that the weapons list returns Count matches the expected value using asserts.

            int count = weaponCollection.GetAllWeaponsOfRarity(stars).Count();
            Assert.AreEqual(count, expectedValue);
        }

        [Test]
        public void WeaponCollection_LoadThatExistAndValid_True()
        {
            // TODO: load returns true, expect WeaponCollection with count of 95 .

            Assert.IsTrue(weaponCollection.Load(inputPath));
            Assert.AreEqual(weaponCollection.Count(), 95);
        }

        [Test]
        public void WeaponCollection_LoadThatDoesNotExist_FalseAndEmpty()
        {
            // TODO: load returns false, expect an empty WeaponCollection

            Assert.IsFalse(weaponCollection.Load("WrongNameFile"));
            Assert.IsTrue(weaponCollection.Count == 0);
            
        }

        [Test]
        public void WeaponCollection_SaveWithValuesCanLoad_TrueAndNotEmpty()
        {
            // TODO: save returns true, load returns true, and WeaponCollection is not empty.

            Assert.IsTrue(weaponCollection.Save(outputPath));
            Assert.IsTrue(weaponCollection.Load(outputPath));
            Assert.IsTrue(weaponCollection.Count != 0);
        }

        [Test]
        public void WeaponCollection_SaveEmpty_TrueAndEmpty()
        {
            // After saving an empty WeaponCollection, load the file and expect WeaponCollection to be empty.

            weaponCollection.Clear();
            Assert.IsTrue(weaponCollection.Save(outputPath));
            Assert.IsTrue(weaponCollection.Load(outputPath));
            Assert.IsTrue(weaponCollection.Count == 0);
        }

        // Weapon Unit Tests
        [Test]
        public void Weapon_TryParseValidLine_TruePropertiesSet()
        {
            // TODO: create a Weapon with the stats above set properly
            Weapon expected = null;
            // TODO: uncomment this once you added the Type1 and Type2
            expected = new Weapon()
            {
                Name = "Skyward Blade",
                Type = WeaponType.Sword,
                Image = "https://vignette.wikia.nocookie.net/gensin-impact/images/0/03/Weapon_Skyward_Blade.png",
                Rarity = 5,
                BaseAttack = 46,
                SecondaryStat = "Energy Recharge",
                Passive = "Sky-Piercing Fang"
            };

            string line = "Skyward Blade,Sword,https://vignette.wikia.nocookie.net/gensin-impact/images/0/03/Weapon_Skyward_Blade.png,5,46,Energy Recharge,Sky-Piercing Fang";
            Weapon actual = null;

            // TODO: uncomment this once you have TryParse implemented.
            Assert.IsTrue(Weapon.TryParse(line, out actual));
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Type, actual.Type);
            Assert.AreEqual(expected.BaseAttack, actual.BaseAttack);
            // TODO: check for the rest of the properties, Image,Rarity,SecondaryStat,Passive
            Assert.AreEqual(expected.Image, actual.Image);
            Assert.AreEqual(expected.Rarity, actual.Rarity);
            Assert.AreEqual(expected.SecondaryStat, actual.SecondaryStat);
            Assert.AreEqual(expected.Passive, actual.Passive);
        }

        [Test]
        public void Weapon_TryParseInvalidLine_FalseNull()
        {
            // TODO: use "1,Bulbasaur,A,B,C,65,65", Weapon.TryParse returns false, and Weapon is null.

            string line = "1,Bulbasaur,A,B,C,65,65";
            Weapon actual = null;
            Assert.IsFalse(Weapon.TryParse(line, out actual));
        }

        // Test LoadJson Valid (Loading the data2.csv is in SetUp)
        [Test]
        public void WeaponCollection_Load_Save_Load_ValidJson()
        {
            Assert.IsTrue(weaponCollection.Save(outputPathJSON));
            Assert.IsTrue(weaponCollection.Load(outputPathJSON));
            Assert.AreEqual(weaponCollection.Count, 95);
        }

        [Test]
        public void WeaponCollection_Load_SaveAsJSON_Load_ValidJson()
        {
            Assert.IsTrue(weaponCollection.SaveAsJSON(outputPathJSON));
            Assert.IsTrue(weaponCollection.Load(outputPathJSON));
            Assert.AreEqual(weaponCollection.Count, 95);
        }

        [Test]
        public void WeaponCollection_Load_SaveAsJSON_LoadJSON_ValidJson()
        {
            Assert.IsTrue(weaponCollection.SaveAsJSON(outputPathJSON));
            Assert.IsTrue(weaponCollection.LoadJSON(outputPathJSON));
            Assert.AreEqual(weaponCollection.Count, 95);
        }

        [Test]
        public void WeaponCollection_Load_Save_LoadJSON_ValidJson()
        {
            Assert.IsTrue(weaponCollection.Save(outputPathJSON));
            Assert.IsTrue(weaponCollection.LoadJSON(outputPathJSON));
            Assert.AreEqual(weaponCollection.Count, 95);
        }

        // Test LoadCsv Valid
        [Test]
        public void WeaponCollection_Load_Save_Load_ValidCsv()
        {
            Assert.IsTrue(weaponCollection.Save(outputPathCSV));
            Assert.IsTrue(weaponCollection.Load(outputPathCSV));
            Assert.AreEqual(weaponCollection.Count, 95);
        }

        [Test]
        public void WeaponCollection_Load_SaveAsCSV_LoadCSV_ValidCsv()
        {
            Assert.IsTrue(weaponCollection.SaveAsCSV(outputPathCSV));
            Assert.IsTrue(weaponCollection.LoadCSV(outputPathCSV));
            Assert.AreEqual(weaponCollection.Count, 95);
        }

        // Test LoadXML Valid
        [Test]
        public void WeaponCollection_Load_Save_Load_ValidXml()
        {
            Assert.IsTrue(weaponCollection.Save(outputPathXML));
            Assert.IsTrue(weaponCollection.Load(outputPathXML));
            Assert.AreEqual(weaponCollection.Count, 95);
        }

        [Test]
        public void WeaponCollection_Load_SaveAsXML_LoadXML_ValidXml()
        {
            Assert.IsTrue(weaponCollection.SaveAsXML(outputPathXML));
            Assert.IsTrue(weaponCollection.LoadXML(outputPathXML));
            Assert.AreEqual(weaponCollection.Count, 95);
        }

        // Test SaveAsJSON Empty (Does not Load the data2.csv in SetUp)
        [Test]
        public void WeaponCollection_SaveEmpty_Load_ValidJson()
        {
            Assert.IsTrue(empty.SaveAsJSON(outputPathJSONEmpty));
            Assert.IsTrue(empty.Load(outputPathJSONEmpty));
            Assert.AreEqual(empty.Count, 0);
        }

        // Test SaveAsCSV Empty
        [Test]
        public void WeaponCollection_SaveEmpty_Load_ValidCsv()
        {
            Assert.IsTrue(empty.SaveAsCSV(outputPathCSVEmpty));
            Assert.IsTrue(empty.Load(outputPathCSVEmpty));
            Assert.AreEqual(empty.Count, 0);
        }

        // Test SaveAsXML Empty
        [Test]
        public void WeaponCollection_SaveEmpty_Load_ValidXml()
        {
            Assert.IsTrue(empty.SaveAsXML(outputPathXMLEmpty));
            Assert.IsTrue(empty.Load(outputPathXMLEmpty));
            Assert.AreEqual(empty.Count, 0);
        }

        // Test Load InvalidFormat
        [Test]
        public void WeaponCollection_Load_SaveJSON_LoadXML_InvalidXml()
        {
            Assert.IsTrue(weaponCollection.SaveAsJSON(outputPathJSON));
            Assert.IsFalse(weaponCollection.LoadXML(outputPathJSON));
            Assert.AreEqual(weaponCollection.Count, 0);
        }

        [Test]
        public void WeaponCollection_Load_SaveXML_LoadJSON_InvalidJson()
        {
            Assert.IsTrue(weaponCollection.SaveAsXML(outputPathXML));
            Assert.IsFalse(weaponCollection.LoadJSON(outputPathXML));
            Assert.AreEqual(weaponCollection.Count, 0);
        }

        [Test]
        public void WeaponCollection_ValidCsv_LoadXML_InvalidXml()
        {
            Assert.IsFalse(weaponCollection.LoadXML(inputPath));
            Assert.AreEqual(weaponCollection.Count, 0);
        }

        [Test]
        public void WeaponCollection_ValidCsv_LoadJSON_InvalidJson()
        {
            Assert.IsFalse(weaponCollection.LoadJSON(inputPath));
            Assert.AreEqual(weaponCollection.Count, 0);
        }
    }
}
