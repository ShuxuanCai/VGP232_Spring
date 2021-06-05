using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for EncryptionToolWindow.xaml
    /// </summary>
    public partial class EncryptionToolWindow : Window
    {
        Crypto crypto;

        public EncryptionToolWindow()
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

        private void EncryptPressed(object sender, RoutedEventArgs e)
        {
            string plainText = tbPlainText.Text;
            plainText = plainText.Replace(' ', '+');
            int padding = plainText.Length % 4;
            if (padding != 0)
            {
                plainText += new string('+', 4 - padding);
            }

            byte[] data = Convert.FromBase64String(plainText);
            byte[] cypherData = crypto.Encrypt(data);
            string cypherText = Convert.ToBase64String(cypherData);
            tbSaveToBinaryFile.Text = cypherText;
        }

        private void DecryptPressed(object sender, RoutedEventArgs e)
        {
            string cypherText = tbCypherText.Text;
            byte[] cypherData = Convert.FromBase64String(cypherText);
            byte[] plainData = crypto.Decrypt(cypherData);
            string plainText = Convert.ToBase64String(plainData);
            plainText = plainText.Replace('+', ' ');
            tbSaveToTextFile.Text = plainText;
        }

        private void SaveToBinaryPressed(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Binary File (.bin)| *.bin";
            if (saveFile.ShowDialog() == true)
            {
                string filename = saveFile.FileName;
                string text = tbSaveToBinaryFile.Text;
                byte[] saveToBinary = Convert.FromBase64String(text);
                File.WriteAllBytes(filename, saveToBinary);
            }
        }

        private void SaveToTextPressed(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Text File (.txt)|*.txt";
            if (saveFile.ShowDialog() == true)
            {
                string filename = saveFile.FileName;
                File.WriteAllText(filename, tbSaveToTextFile.Text);
            }
        }

        private void LoadTextPressed(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Text File (.txt)|*.txt";
            if (openFile.ShowDialog() == true)
            {
                string filename = openFile.FileName;
                string text = File.ReadAllText(filename);
                tbPlainText.Text = text;
            }
        }

        private void LoadCipherPressed(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Binary File (.bin)| *.bin";
            if (openFile.ShowDialog() == true)
            {
                string filename = openFile.FileName;
                byte[] text = File.ReadAllBytes(filename);
                string loadBinary = Convert.ToBase64String(text);
                tbCypherText.Text = loadBinary;
            }
        }
    }
}
