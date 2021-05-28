using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Week_6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Crypto crypto;

        public MainWindow()
        {
            InitializeComponent();
            crypto = new Crypto();
            crypto.Initialize(CryptoAlgorithm.AES);
        }

        private void EncryptPressed(object sender, RoutedEventArgs e)
        {
            string plainText = tbPlainText.Text;
            plainText = plainText.Replace(' ', '+');
            int padding = plainText.Length % 4;
            if(padding != 0)
            {
                plainText += new string('+', 4 - padding);
            }

            byte[] data = Convert.FromBase64String(plainText);
            byte[] cypherData = crypto.Encrypt(data);
            string cypherText = Convert.ToBase64String(cypherData);
            tbCypherText.Text = cypherText;
        }

        private void DecryptPressed(object sender, RoutedEventArgs e)
        {
            string cypherText = tbCypherText.Text;
            byte[] cypherData = Convert.FromBase64String(cypherText);
            byte[] plainData = crypto.Decrypt(cypherData);
            string plainText = Convert.ToBase64String(plainData);
            plainText = plainText.Replace('+', ' ');
            tbDecypherText.Text = plainText;
        }

        private void SavePressed(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            if(saveFile.ShowDialog() == true)
            {
                File.WriteAllText(saveFile.FileName, tbDecypherText.Text);
            }
        }
    }
}
