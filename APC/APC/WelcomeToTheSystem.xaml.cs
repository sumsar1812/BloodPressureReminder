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
using System.Windows.Shapes;

namespace APC
{
    /// <summary>
    /// Interaction logic for WelcomeToTheSystem.xaml
    /// </summary>
    public partial class WelcomeToTheSystem : Window
    {
        public WelcomeToTheSystem()
        {
            InitializeComponent();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(tbxName.Text))
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(GlobalVarOfThemAll.path);
                file.WriteLine(tbxName.Text);
                file.Close();
                MainWindow timeWindow = new MainWindow();
                App.Current.MainWindow = timeWindow;
                this.Close();
                timeWindow.Show();

            }
            else
            {
                
            }


        }
    }
}
