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

namespace Assignment4
{
    /// <summary>
    /// Interaction logic for SelectionWindow.xaml
    /// </summary>
    public partial class SelectionWindow : Window
    {
        Crypto crypto;

        public SelectionWindow()
        {
            InitializeComponent();
            crypto = new Crypto();
        }

        private void NextPressed(object sender, RoutedEventArgs e)
        {
            KeysWindow keysWindow = new KeysWindow();
            if(RSAClicked.IsChecked == true)
            {
                crypto.Initialize(CryptoAlgorithm.RSA);
                if(keysWindow.ShowDialog() == true)
                {
                }
            }

            else if(AESClicked.IsChecked == true)
            {
                keysWindow.importPrivateKey.Visibility = Visibility.Hidden;
                keysWindow.importPublicKey.Visibility = Visibility.Hidden;
                keysWindow.exportPrivateKey.Visibility = Visibility.Hidden;
                keysWindow.exportPublicKey.Visibility = Visibility.Hidden;
                crypto.Initialize(CryptoAlgorithm.AES);
                if (keysWindow.ShowDialog() == true)
                {
                }
            }
        }
    }
}
