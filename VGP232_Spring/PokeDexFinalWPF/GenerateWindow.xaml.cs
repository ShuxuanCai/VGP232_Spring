using Microsoft.Win32;
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
using TextureAtlasLib;

namespace PokeDexFinalWPF
{
    /// <summary>
    /// Interaction logic for GenerateWindow.xaml
    /// </summary>
    public partial class GenerateWindow : Window
    {
        public Spritesheet mySpritesheet { get; set; }

        public GenerateWindow()
        {
            InitializeComponent();
            mySpritesheet = new Spritesheet();
            mySpritesheet.InputPaths = new List<string>();
            DataContext = mySpritesheet;
            lbImages.ItemsSource = mySpritesheet.InputPaths;
        }

        private void AddPressed(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "PNG Images| *.png";
            openFile.Multiselect = true;
            if (openFile.ShowDialog() == true)
            {
                foreach (string imagePath in openFile.FileNames)
                {
                    mySpritesheet.InputPaths.Add(imagePath);
                }
                lbImages.Items.Refresh();
            }
        }

        private void RemovePressed(object sender, RoutedEventArgs e)
        {
            if (lbImages.SelectedIndex == -1)
            {
                return;
            }
            mySpritesheet.InputPaths.RemoveAt(lbImages.SelectedIndex);
            lbImages.Items.Refresh();
        }

        private void GeneratePressed(object sender, RoutedEventArgs e)
        {
            mySpritesheet.IncludeMetaData = false;
            mySpritesheet.OutputDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            mySpritesheet.OutputFile = "pokemons.png";

            try
            {
                mySpritesheet.Generate(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
