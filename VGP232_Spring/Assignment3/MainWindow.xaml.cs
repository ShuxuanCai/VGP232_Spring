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
using System.Xml;
using TextureAtlasLib;

namespace Assignment3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Spritesheet mySpritesheet { get; set; }

        public MainWindow()
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
                foreach(string imagePath in openFile.FileNames)
                {
                    mySpritesheet.InputPaths.Add(imagePath);
                }
                lbImages.Items.Refresh();
            }
        }

        private void RemovePressed(object sender, RoutedEventArgs e)
        {
            if(lbImages.SelectedIndex == -1)
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
            mySpritesheet.OutputFile = "spriteSheet.png";

            try
            {
                mySpritesheet.Generate(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void NewPressed(object sender, RoutedEventArgs e)
        {
            if(MessageBoxResult.Yes == MessageBox.Show("Would you like to save first if there’s an existing project and then open the new one?", "SAVE or NOT", MessageBoxButton.YesNo))
            {
                SavePressed(this, null);
            }
            tbOutputDir.Text = null;
            tbOutputFile.Text = null;
            tbColumns.Text = null;
            cbMetaData.IsChecked = false;
            mySpritesheet.InputPaths.Clear();
            lbImages.Items.Refresh();
        }

        private void OpenPressed(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "XML files |*.xml";
            if (openFile.ShowDialog() == true)
            {
                if (!mySpritesheet.LoadXML(openFile.FileName))
                {
                    MessageBox.Show("Unable to load file.");
                }

                else
                {
                    tbColumns.Text = mySpritesheet.Columns.ToString();
                    tbOutputFile.Text = mySpritesheet.OutputFile;
                    tbOutputDir.Text = mySpritesheet.OutputDirectory;
                    cbMetaData.IsChecked = mySpritesheet.IncludeMetaData;
                    lbImages.ItemsSource = mySpritesheet.InputPaths;
                    lbImages.Items.Refresh();
                }
            }
        }

        private void SavePressed(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "XML files |*.xml";
            if (saveFile.ShowDialog() == true)
            {
                if (!mySpritesheet.SaveAsXML(saveFile.FileName))
                {
                    MessageBox.Show("Unable to save file.");
                }
            }
        }

        private void SaveAsPressed(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.InitialDirectory = tbOutputDir.Text;
            saveFile.Filter = "XML files |*.xml";
            if (saveFile.ShowDialog() == true)
            {
                string path = saveFile.FileName;
                if (!mySpritesheet.SaveAsXML(saveFile.FileName))
                {
                    if (!System.IO.Directory.Exists(path))
                    {
                        System.IO.Directory.CreateDirectory(path);
                    }
                    tbOutputDir.Text = path;
                }
            }
        }

        private void ExitPressed(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BrowsePressed(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFile.Filter = "PNG images |*.png";
            if (saveFile.ShowDialog() == true)
            {
                tbOutputFile.Text = System.IO.Path.GetFileName(saveFile.FileName);
                mySpritesheet.OutputFile = tbOutputFile.Text;
                tbOutputDir.Text = System.IO.Path.GetDirectoryName(saveFile.FileName);
                mySpritesheet.OutputDirectory = tbOutputDir.Text;
            }
        }
    }
}
