using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WeaponLib;

namespace Assignment2c
{
    /// <summary>
    /// Interaction logic for EditWeaponWindow.xaml
    /// </summary>
    public partial class EditWeaponWindow : Window
    {
        private Weapon tempWeapon;

        public Weapon TempWeapon
        {
            get { return tempWeapon; }
            set
            {
                tempWeapon = value;
                DataContext = TempWeapon;
            }
        }

        public EditWeaponWindow()
        {
            InitializeComponent();

            string[] types = Enum.GetNames(typeof(WeaponType));
            cbTypes.ItemsSource = types;

            int[] rarities = { 1, 2, 3, 4, 5 };
            cbRarities.ItemsSource = rarities;

            TempWeapon = new Weapon();
        }

        private void CancelClicked(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void SubmitClicked(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void Generate(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            tempWeapon.BaseAttack = random.Next(20, 51);
            Array types = Enum.GetValues(typeof(WeaponType));
            tempWeapon.Type = (WeaponType)types.GetValue(random.Next(types.Length));
            tempWeapon.Rarity = random.Next(1, 6);

            DialogResult = true;
            Close();
        }

        private void ImageLoaded(object sender, RoutedEventArgs e)
        {
            // This is wrong, it said tempWeapon is null.
            //if (ImageUrl != null)
            //{
            //    BitmapImage bitmap = new BitmapImage();
            //    bitmap.BeginInit();
            //    bitmap.UriSource = new Uri(tempWeapon.Image);
            //    bitmap.EndInit();
            //    addImage.Source = bitmap;
            //}
        }
    }
}
