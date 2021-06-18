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
using TextureAtlasLib;

namespace PokeDexFinalWPF
{
    /// <summary>
    /// Interaction logic for LoadAndChooseWindow.xaml
    /// </summary>
    public partial class LoadAndChooseWindow : Window
    {
        public PokemonCollection pokemonCollection { get; set; }
        public PokemonCollection pokemonCollection2 { get; set; }
        public Spritesheet mySpritesheet1 { get; set; }
        public Spritesheet mySpritesheet2 { get; set; }
        public Spritesheet mySpritesheet3 { get; set; }
        public Spritesheet mySpritesheet4 { get; set; }
        public Spritesheet mySpritesheet5 { get; set; }
        public Spritesheet mySpritesheet6 { get; set; }

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

            if (lbImages1.Items.Count == 1 && lbImages2.Items.Count == 1 && lbImages3.Items.Count == 1 && lbImages4.Items.Count == 1 && lbImages5.Items.Count == 1 && lbImages6.Items.Count == 1)
            {
                MessageBox.Show("There are already 6 Pokemons!");
                return;
            }

            pokemonCollection2.Add(lbPokemons.SelectedItem as PokemonInfo);

            if (lbImages1.Items.Count == 0)
            {
                mySpritesheet1 = new Spritesheet();
                mySpritesheet1.InputPaths = new List<string>();
                DataContext = mySpritesheet1;
                lbImages1.ItemsSource = mySpritesheet1.InputPaths;
                PokemonInfo p = lbPokemons.SelectedItem as PokemonInfo;
                string filepath = System.AppDomain.CurrentDomain.BaseDirectory + "Textures/" + p.Nat + ".png";
                mySpritesheet1.InputPaths.Add(filepath);
                lbImages1.Items.Refresh();
            }

            else if (lbImages2.Items.Count == 0)
            {
                mySpritesheet2 = new Spritesheet();
                mySpritesheet2.InputPaths = new List<string>();
                DataContext = mySpritesheet2;
                lbImages2.ItemsSource = mySpritesheet2.InputPaths;
                PokemonInfo p = lbPokemons.SelectedItem as PokemonInfo;
                string filepath = System.AppDomain.CurrentDomain.BaseDirectory + "Textures/" + p.Nat + ".png";
                mySpritesheet2.InputPaths.Add(filepath);
                lbImages2.Items.Refresh();
            }

            else if (lbImages3.Items.Count == 0)
            {
                mySpritesheet3 = new Spritesheet();
                mySpritesheet3.InputPaths = new List<string>();
                DataContext = mySpritesheet3;
                lbImages3.ItemsSource = mySpritesheet3.InputPaths;
                PokemonInfo p = lbPokemons.SelectedItem as PokemonInfo;
                string filepath = System.AppDomain.CurrentDomain.BaseDirectory + "Textures/" + p.Nat + ".png";
                mySpritesheet3.InputPaths.Add(filepath);
                lbImages3.Items.Refresh();
            }

            else if (lbImages4.Items.Count == 0)
            {
                mySpritesheet4 = new Spritesheet();
                mySpritesheet4.InputPaths = new List<string>();
                DataContext = mySpritesheet4;
                lbImages4.ItemsSource = mySpritesheet4.InputPaths;
                PokemonInfo p = lbPokemons.SelectedItem as PokemonInfo;
                string filepath = System.AppDomain.CurrentDomain.BaseDirectory + "Textures/" + p.Nat + ".png";
                mySpritesheet4.InputPaths.Add(filepath);
                lbImages4.Items.Refresh();
            }

            else if (lbImages5.Items.Count == 0)
            {
                mySpritesheet5 = new Spritesheet();
                mySpritesheet5.InputPaths = new List<string>();
                DataContext = mySpritesheet5;
                lbImages5.ItemsSource = mySpritesheet5.InputPaths;
                PokemonInfo p = lbPokemons.SelectedItem as PokemonInfo;
                string filepath = System.AppDomain.CurrentDomain.BaseDirectory + "Textures/" + p.Nat + ".png";
                mySpritesheet5.InputPaths.Add(filepath);
                lbImages5.Items.Refresh();
            }

            else if (lbImages6.Items.Count == 0)
            {
                mySpritesheet6 = new Spritesheet();
                mySpritesheet6.InputPaths = new List<string>();
                DataContext = mySpritesheet6;
                lbImages6.ItemsSource = mySpritesheet6.InputPaths;
                PokemonInfo p = lbPokemons.SelectedItem as PokemonInfo;
                string filepath = System.AppDomain.CurrentDomain.BaseDirectory + "Textures/" + p.Nat + ".png";
                mySpritesheet6.InputPaths.Add(filepath);
                lbImages6.Items.Refresh();
            }
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

        private void SaveClicked2(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "CSV files |*.csv| XML files |*.xml| JSON files |*.json";
            if (saveFile.ShowDialog() == true)
            {
                if (!pokemonCollection2.Save(saveFile.FileName))
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
            pokemonCollection.Clear();
            lbPokemons.Items.Refresh();
            if (lbImages1.Items.Count != 0)
            {
                mySpritesheet1.InputPaths.Clear();
                lbImages1.Items.Refresh();
            }
            if (lbImages2.Items.Count != 0)
            {
                mySpritesheet2.InputPaths.Clear();
                lbImages2.Items.Refresh();
            }
            if (lbImages3.Items.Count != 0)
            {
                mySpritesheet3.InputPaths.Clear();
                lbImages3.Items.Refresh();
            }
            if (lbImages4.Items.Count != 0)
            {
                mySpritesheet4.InputPaths.Clear();
                lbImages4.Items.Refresh();
            }
            if (lbImages5.Items.Count != 0)
            {
                mySpritesheet5.InputPaths.Clear();
                lbImages5.Items.Refresh();
            }
            if (lbImages6.Items.Count != 0)
            {
                mySpritesheet6.InputPaths.Clear();
                lbImages6.Items.Refresh();
            }
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
            if (lbImages1.SelectedIndex != -1)
            {
                mySpritesheet1.InputPaths.RemoveAt(lbImages1.SelectedIndex);
                pokemonCollection2.RemoveAt(lbImages1.SelectedIndex);
                lbImages1.Items.Refresh();
            }
            else if (lbImages2.SelectedIndex != -1)
            {
                mySpritesheet2.InputPaths.RemoveAt(lbImages2.SelectedIndex);
                pokemonCollection2.RemoveAt(lbImages2.SelectedIndex);
                lbImages2.Items.Refresh();
            }
            else if (lbImages3.SelectedIndex != -1)
            {
                mySpritesheet3.InputPaths.RemoveAt(lbImages3.SelectedIndex);
                pokemonCollection2.RemoveAt(lbImages3.SelectedIndex);
                lbImages3.Items.Refresh();
            }
            else if (lbImages4.SelectedIndex != -1)
            {
                mySpritesheet4.InputPaths.RemoveAt(lbImages4.SelectedIndex);
                pokemonCollection2.RemoveAt(lbImages4.SelectedIndex);
                lbImages4.Items.Refresh();
            }
            else if (lbImages5.SelectedIndex != -1)
            {
                mySpritesheet5.InputPaths.RemoveAt(lbImages5.SelectedIndex);
                pokemonCollection2.RemoveAt(lbImages5.SelectedIndex);
                lbImages5.Items.Refresh();
            }
            else if (lbImages6.SelectedIndex != -1)
            {
                mySpritesheet6.InputPaths.RemoveAt(lbImages6.SelectedIndex);
                pokemonCollection2.RemoveAt(lbImages6.SelectedIndex);
                lbImages6.Items.Refresh();
            }
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
