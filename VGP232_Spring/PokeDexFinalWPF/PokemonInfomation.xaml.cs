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
using PokeDexFinalLib;

namespace PokeDexFinalWPF
{
    /// <summary>
    /// Interaction logic for PokemonInfomation.xaml
    /// </summary>
    public partial class PokemonInfomation : Window
    {
        public PokemonInfomation(PokemonInfo pokemonInfo)
        {
            InitializeComponent();
            tbNumber.Text = pokemonInfo.Nat.ToString();
            tbName.Text = pokemonInfo.Name;
            tbHP.Text = pokemonInfo.HP.ToString();
            tbAtk.Text = pokemonInfo.Atk.ToString();
            tbDef.Text = pokemonInfo.Def.ToString();
            tbSpA.Text = pokemonInfo.SpA.ToString();
            tbSpD.Text = pokemonInfo.SpD.ToString();
            tbSpe.Text = pokemonInfo.Spe.ToString();
            tbTotal.Text = pokemonInfo.Total.ToString();
        }
    }
}
