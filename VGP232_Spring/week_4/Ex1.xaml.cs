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

namespace week_4
{
    /// <summary>
    /// Interaction logic for Ex1.xaml
    /// </summary>
    public partial class Ex1 : Window
    {
        private int count = 0;
        public Ex1()
        {
            InitializeComponent();
        }

        private void IncrementCountPressed(object sender, RoutedEventArgs e)
        {
            ++count;
            tbCounter.Text = string.Format("Count: {0}", count);
        }

        private void SayHelloPressed(object sender, RoutedEventArgs e)
        {
            tbHelloMessage.Text = string.Format("Hello {0}", tBoxUserName.Text);
        }
    }
}
