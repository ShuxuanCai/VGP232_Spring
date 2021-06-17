using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using PokeDexFinalLib;

namespace PokeDexFinalWPF
{
    /// <summary>
    /// Interaction logic for LoadAndChooseWindow.xaml
    /// </summary>
    public partial class LoadAndChooseWindow : Window
    {
        public PokemonCollection pokemonCollection { get; set; }
        public PokemonCollection pokemonCollection2 { get; set; }

        public LoadAndChooseWindow()
        {
            InitializeComponent();
            pokemonCollection = new PokemonCollection();
            pokemonCollection2 = new PokemonCollection();

            string[] types = Enum.GetNames(typeof(PokemonType));
            cbTypes1.ItemsSource = types;
            cbTypes2.ItemsSource = types;
        }

        private void LoadClicked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "CSV files |*.csv| XML files |*.xml| JSON files |*.json";
            if (openFile.ShowDialog() == true)
            {
                if (!pokemonCollection.Load(openFile.FileName))
                {
                    MessageBox.Show("Unable to load file.");
                }
                else
                {
                    lbPokemons.ItemsSource = pokemonCollection;
                    pokemonCollection.pokemons = lbPokemons.ItemsSource as List<PokemonInfo>;
                    lbPokemons.Items.Refresh();
                }
            }
        }

        private void AddClicked(object sender, RoutedEventArgs e)
        {
            if (lbPokemons.SelectedIndex == -1)
            {
                return;
            }

            if (pokemonCollection2.Count == 6)
            {
                MessageBox.Show("It is already 6 Pokemons!");
                return;
            }

            pokemonCollection2.Add(lbPokemons.SelectedItem as PokemonInfo);

            int total = pokemonCollection2.GetAllPokemonsOfTotal();
            tbTotal.Text = total.ToString();

            lbYourPokemons.ItemsSource = pokemonCollection2;
            lbYourPokemons.Items.Refresh();
        }

        private void CheckClicked(object sender, RoutedEventArgs e)
        {
            if (lbPokemons.SelectedIndex == -1)
            {
                return;
            }

            CheckWindow checkWindow = new CheckWindow(lbPokemons.SelectedItem as PokemonInfo);
            if (checkWindow.ShowDialog() == true)
            {

            }
        }

        private void SaveClicked(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "CSV files |*.csv| XML files |*.xml| JSON files |*.json";
            if (saveFile.ShowDialog() == true)
            {
                if (!pokemonCollection.Save(saveFile.FileName))
                {
                    MessageBox.Show("Unable to save file.");
                }
            }
        }

        private void ResetClicked(object sender, RoutedEventArgs e)
        {
            number.IsChecked = false;
            nat.IsChecked = false;
            hp.IsChecked = false;
            atk.IsChecked = false;
            def.IsChecked = false;
            spa.IsChecked = false;
            spd.IsChecked = false;
            spe.IsChecked = false;
            total.IsChecked = false;
            cbTypes1.SelectedIndex = 0;
            cbTypes2.SelectedIndex = 0;
            tbFilterByName.Text = String.Empty;
            tbTotal.Text = String.Empty;
            pokemonCollection.Clear();
            lbPokemons.Items.Refresh();
            pokemonCollection2.Clear();
            lbYourPokemons.Items.Refresh();
        }


        private void RemoveClicked(object sender, RoutedEventArgs e)
        {
            if (lbPokemons.SelectedIndex == -1)
            {
                return;
            }

            pokemonCollection.RemoveAt(lbPokemons.SelectedIndex);
            lbPokemons.Items.Refresh();
        }

        private void RemoveClicked2(object sender, RoutedEventArgs e)
        {
            if (lbYourPokemons.SelectedIndex == -1)
            {
                return;
            }

            pokemonCollection2.RemoveAt(lbYourPokemons.SelectedIndex);
            lbYourPokemons.Items.Refresh();

            int total = pokemonCollection2.GetAllPokemonsOfTotal();
            tbTotal.Text = total.ToString();
        }

        private void SortByNumber(object sender, RoutedEventArgs e)
        {
            pokemonCollection.SortBy("Nat");
            lbPokemons.Items.Refresh();
        }

        private void SortByName(object sender, RoutedEventArgs e)
        {
            pokemonCollection.SortBy("Name");
            lbPokemons.Items.Refresh();
        }

        private void SortByHP(object sender, RoutedEventArgs e)
        {
            pokemonCollection.SortBy("HP");
            lbPokemons.Items.Refresh();
        }

        private void SortByAtk(object sender, RoutedEventArgs e)
        {
            pokemonCollection.SortBy("Atk");
            lbPokemons.Items.Refresh();
        }

        private void SortByDef(object sender, RoutedEventArgs e)
        {
            pokemonCollection.SortBy("Def");
            lbPokemons.Items.Refresh();
        }

        private void SortBySpA(object sender, RoutedEventArgs e)
        {
            pokemonCollection.SortBy("SpA");
            lbPokemons.Items.Refresh();
        }

        private void SortBySpD(object sender, RoutedEventArgs e)
        {
            pokemonCollection.SortBy("SpD");
            lbPokemons.Items.Refresh();
        }

        private void SortBySpe(object sender, RoutedEventArgs e)
        {
            pokemonCollection.SortBy("Spe");
            lbPokemons.Items.Refresh();
        }

        private void SortByTotal(object sender, RoutedEventArgs e)
        {
            pokemonCollection.SortBy("Total");
            lbPokemons.Items.Refresh();
        }

        private void ShowOnlyType1(object sender, SelectionChangedEventArgs e)
        {
            if (cbTypes1.SelectedIndex == -1)
            {
                return;
            }

            PokemonType type = (PokemonType)Enum.Parse(typeof(PokemonType), cbTypes1.SelectedItem.ToString());
            lbPokemons.ItemsSource = pokemonCollection.GetAllPokemonsOfType(type);
            lbPokemons.Items.Refresh();
        }

        private void ShowOnlyType2(object sender, SelectionChangedEventArgs e)
        {
            if (cbTypes2.SelectedIndex == -1)
            {
                return;
            }

            PokemonType type = (PokemonType)Enum.Parse(typeof(PokemonType), cbTypes1.SelectedItem.ToString());
            PokemonType type2 = (PokemonType)Enum.Parse(typeof(PokemonType), cbTypes2.SelectedItem.ToString());
            lbPokemons.ItemsSource = pokemonCollection.GetAllPokemonsOfType2(type, type2);
            lbPokemons.Items.Refresh();
        }

        private void ShowAllClicked(object sender, RoutedEventArgs e)
        {
            lbPokemons.ItemsSource = pokemonCollection;
            pokemonCollection.pokemons = lbPokemons.ItemsSource as List<PokemonInfo>;
            lbPokemons.Items.Refresh();
        }

        private void FilterNameTextChanged(object sender, TextChangedEventArgs e)
        {
            string name = (sender as TextBox).Text;
            pokemonCollection.pokemons = pokemonCollection.pokemons.FindAll(x => x.Name.StartsWith(name, StringComparison.OrdinalIgnoreCase));
            lbPokemons.ItemsSource = pokemonCollection.pokemons;
            lbPokemons.Items.Refresh();
        }
    }
}
