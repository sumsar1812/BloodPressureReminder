using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace APC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            GlobalVarOfThemAll.path = @"C:\Users\Public\BloodpreasureUser.txt";
            string path = @"C:\Users\Public\BloodpreasureUser.txt";
            if (!File.Exists(path))
            {
                new WelcomeToTheSystem().Show();
                Hide();
            }


                
            
            //loadUsers(path);
            tid.Text = DateTime.Now.ToString("HH:mm");      
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += OnUpdateTimerTick;
            timer.Start();




    }
        private void OnUpdateTimerTick(object sender, EventArgs e)
        {
            DateTime TimeRN = DateTime.Now;
            tid.Text = TimeRN.ToString("HH:mm");
            dato.Text = TimeRN.ToString("dddd") + " d." + TimeRN.ToString("dd") + "/" + TimeRN.ToString("MM");
            CheckForEvents(TimeRN);
        }

        private void loadUsers(string path)
        {
            string text = System.IO.File.ReadAllText(path);

        }

        private void CheckForEvents(DateTime time)
        {
            string timeRN = time.ToString("HH:mm:ss");
            switch (timeRN)
            {
                case "06:00:00":
                    { 
                    MorningEvent();
                    break;
                    }
                case "18:00:00":
                    {
                        EveningEvent();
                        break;
                    }
                case "00:00:00":
                    {
                        EasterEgg.Visibility = Visibility.Visible;
                        break;
                    }
                case "00:01:00":
                {
                    EasterEgg.Visibility = Visibility.Hidden;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void MorningEvent()
        {
            if(true) //check here that pressure Sensor is activated. IF activated start Bloodpressure Window
            { 
            BloodPresure BPWindow = new BloodPresure();
            BPWindow.ShowDialog();
            }
        }
        private void EveningEvent()
        {
            if (true)
            { 
            BloodPresure BPWindow = new BloodPresure();
            BPWindow.ShowDialog();
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            MorningEvent();
        }
    }
}
