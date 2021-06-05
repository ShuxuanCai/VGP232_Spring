using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
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
    /// Interaction logic for KeysWindow.xaml
    /// </summary>
    public partial class KeysWindow : Window
    {
        Crypto crypto;

        public KeysWindow()
        {
            InitializeComponent();
            SelectionWindow selectionWindow = new SelectionWindow();
            crypto = new Crypto();
            if (selectionWindow.RSAClicked.IsPressed)
            {
                crypto.Initialize(CryptoAlgorithm.RSA);
            }
            else
            {
                crypto.Initialize(CryptoAlgorithm.AES);
            }
        }

        private void NextPressed(object sender, RoutedEventArgs e)
        {
            EncryptionToolWindow encryptionToolWindow = new EncryptionToolWindow();
            if(encryptionToolWindow.ShowDialog() == true)
            {

            }
        }

        private void K1Pressed(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "XML File| *.xml";
            string path = openFile.FileName;
            if(openFile.ShowDialog() == true)
            {
                crypto.LoadK1(path);
            }
        }

        private void K2Pressed(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "XML File| *.xml";
            string path = openFile.FileName;
            if (openFile.ShowDialog() == true)
            {
                crypto.LoadK2(path);
            }
        }

        private void K3Pressed(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "XML File| *.xml";
            string path = saveFile.FileName;
            if (saveFile.ShowDialog() == true)
            {
                crypto.SaveK1(path);
            }
        }

        private void K4Pressed(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "XML File| *.xml";
            string path = saveFile.FileName;
            if (saveFile.ShowDialog() == true)
            {
                crypto.SaveK2(path);
            }
        }
    }
}
