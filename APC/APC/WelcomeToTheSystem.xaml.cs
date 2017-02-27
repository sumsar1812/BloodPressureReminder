using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
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
        public SpeechSynthesizer synthesizer;
        public WelcomeToTheSystem()
        {
            InitializeComponent();
            synthesizer = new SpeechSynthesizer();
            synthesizer.SetOutputToDefaultAudioDevice();


            synthesizer.SpeakAsync("Hello. Welcome to the Bloodpressure System. Please enter your name and birthdate.");
            Speech.Text = "Hello. Welcome to the Bloodpressure System. Please enter your name and birthdate.";

        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(tbxName.Text) && DateP.SelectedDate != null)
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
                if (string.IsNullOrWhiteSpace(tbxName.Text) && DateP.SelectedDate == null)
                { 
                    synthesizer.SpeakAsync("Please enter your Name and birthdate.");
                    Speech.Text = "Please enter your name and birthdate.";
                }
                else if (string.IsNullOrWhiteSpace(tbxName.Text))
                {
                    synthesizer.SpeakAsync("Please enter your Name");
                    Speech.Text = "Please enter your name";
                }
                else if (DateP.SelectedDate == null)
                {
                    synthesizer.SpeakAsync("Please enter your birthdate.");
                    Speech.Text = "Please enter your birthdate.";
                }
                else
                {
                    synthesizer.SpeakAsync("Call 1-900-RASMUS for support");
                }

            }


        }



    }
}
