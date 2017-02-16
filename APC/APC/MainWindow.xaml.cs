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
        private DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.SystemIdle);
        public MainWindow()
        {
            InitializeComponent();
            string path = @"C:\Users\Public\BloodpreasureUser.txt";
            if (!File.Exists(path))
            {
                new WelcomeToTheSystem().Show();
                Hide();
            }
            else
            {
                loadUsers(path);
                label1.Content = DateTime.Now.ToString("hh:mm:ss tt");
                timer.Tick += new EventHandler(OnUpdateTimerTick);
                timer.Interval = TimeSpan.FromMilliseconds(1000);
                timer.Start();
            }

            

        }
        private void OnUpdateTimerTick(object sender, EventArgs e)
        {
            label1.Content = DateTime.Now.ToString("hh:mm:ss tt");
        }

        private void loadUsers(string path)
        {
            string text = System.IO.File.ReadAllText(path);

        }
    }
}
