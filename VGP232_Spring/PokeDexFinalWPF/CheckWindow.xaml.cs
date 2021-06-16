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
    /// Interaction logic for CheckWindow.xaml
    /// </summary>
    public partial class CheckWindow : Window
    {
        public Spritesheet mySpritesheet { get; set; }

        private PokemonInfo tempPokemon;

        public PokemonInfo TempPokemon
        {
            get { return tempPokemon; }
            set
            {
                tempPokemon = value;
                DataContext = TempPokemon;
            }
        }

        public CheckWindow(PokemonInfo pokemonInfo)
        {
            InitializeComponent();
            TempPokemon = pokemonInfo;
            mySpritesheet = new Spritesheet();
            mySpritesheet.InputPaths = new List<string>();
            DataContext = mySpritesheet;
            lbImages.ItemsSource = mySpritesheet.InputPaths;
            string filepath = System.AppDomain.CurrentDomain.BaseDirectory + "Textures/" + TempPokemon.Nat + ".png";
            mySpritesheet.InputPaths.Add(filepath);
            lbImages.Items.Refresh();
        }

        private void OpenPressed(object sender, RoutedEventArgs e)
        {
            PokemonInfomation pokemonInfomation = new PokemonInfomation(TempPokemon);
            if (pokemonInfomation.ShowDialog() == true)
            {

            }
        }
    }
}
