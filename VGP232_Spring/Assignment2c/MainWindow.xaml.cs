using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeaponLib;

namespace Assignment2c
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public WeaponCollection mWeaponCollection { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            mWeaponCollection = new WeaponCollection();

            string[] types = Enum.GetNames(typeof(WeaponType));
            cbTypes.ItemsSource = types;
        }

        private void LoadClicked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "XML files |*.xml| JSON files |*.json| CSV files |*.csv";
            if (openFile.ShowDialog() == true)
            {
                if (!mWeaponCollection.Load(openFile.FileName))
                {
                    MessageBox.Show("Unable to load file.");
                }
                else
                {
                    lbWeapons.ItemsSource = mWeaponCollection;
                    lbWeapons.Items.Refresh();
                }
            }
        }

        private void AddClicked(object sender, RoutedEventArgs e)
        {
            EditWeaponWindow editWeapon = new EditWeaponWindow();
            editWeapon.Title = "Add Weapon";
            editWeapon.addSaveButton.Content = "Add";
            if(editWeapon.ShowDialog() == true)
            {
                mWeaponCollection.Add(editWeapon.TempWeapon);
                if(lbWeapons.ItemsSource == null)
                {
                    lbWeapons.ItemsSource = mWeaponCollection;
                }
                lbWeapons.Items.Refresh();
            }
        }

        private void EditClicked(object sender, RoutedEventArgs e)
        {
            if(lbWeapons.SelectedIndex == -1)
            {
                return;
            }

            EditWeaponWindow editWeaponWindow = new EditWeaponWindow();
            editWeaponWindow.Title = "Edit Weapon";
            editWeaponWindow.addSaveButton.Content = "Save";
            editWeaponWindow.TempWeapon = lbWeapons.SelectedItem as Weapon;

            if(editWeaponWindow.ShowDialog() == true)
            {
                mWeaponCollection[lbWeapons.SelectedIndex] = editWeaponWindow.TempWeapon;
                lbWeapons.Items.Refresh();
            }
        }

        private void SaveClicked(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "XML files |*.xml| JSON files |*.json| CSV files |*.csv";
            if (saveFile.ShowDialog() == true)
            {
                if (!mWeaponCollection.Save(saveFile.FileName))
                {
                    MessageBox.Show("Unable to save file.");
                }
            }
        }

        private void RemoveClicked(object sender, RoutedEventArgs e)
        {
            if (lbWeapons.SelectedIndex == -1)
            {
                return;
            }

            mWeaponCollection.RemoveAt(lbWeapons.SelectedIndex);
            lbWeapons.Items.Refresh();
        }

        private void SortByName(object sender, RoutedEventArgs e)
        {
            mWeaponCollection.SortBy("Name");
            lbWeapons.Items.Refresh();
        }

        private void SortByBaseAttack(object sender, RoutedEventArgs e)
        {
            mWeaponCollection.SortBy("BaseAttack");
            lbWeapons.Items.Refresh();
        }

        private void SortByRarity(object sender, RoutedEventArgs e)
        {
            mWeaponCollection.SortBy("Rarity");
            lbWeapons.Items.Refresh();
        }

        private void SortByPassive(object sender, RoutedEventArgs e)
        {
            mWeaponCollection.SortBy("Passive");
            lbWeapons.Items.Refresh();
        }

        private void SortBySecondaryStat(object sender, RoutedEventArgs e)
        {
            mWeaponCollection.SortBy("SecondaryStat");
            lbWeapons.Items.Refresh();
        }

        private void FilterTypeOnlySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cbTypes.SelectedIndex == -1)
            {
                return;
            }

            WeaponType type = (WeaponType)Enum.Parse(typeof(WeaponType), cbTypes.SelectedItem.ToString());
            lbWeapons.ItemsSource = mWeaponCollection.GetAllWeaponsOfType(type);
            lbWeapons.Items.Refresh();
        }

        private bool CustomFilter(object obj)
        {
            if (string.IsNullOrEmpty(tbFilterByName.Text))
            {
                return true;
            }
            else
            {
                return (obj.ToString().IndexOf(tbFilterByName.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }

        private void FilterNameTextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionView view = CollectionViewSource.GetDefaultView(lbWeapons.ItemsSource) as CollectionView;
            view.Filter = CustomFilter;
            CollectionViewSource.GetDefaultView(lbWeapons.ItemsSource).Refresh();
        }
    }
}
