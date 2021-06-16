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

namespace PokeDexFinalWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ibImage.ImageSource =  new BitmapImage(new Uri("PokeDex.jpg", UriKind.Relative));
        }

        private void CheckPressed(object sender, RoutedEventArgs e)
        {
            LoadAndChooseWindow loadAndChooseWindow = new LoadAndChooseWindow();
            if(loadAndChooseWindow.ShowDialog() == true)
            {
            }
        }

        private void GeneratePressed(object sender, RoutedEventArgs e)
        {
            GenerateWindow generateWindow = new GenerateWindow();
            if (generateWindow.ShowDialog() == true)
            {
            }
        }
    }
}
