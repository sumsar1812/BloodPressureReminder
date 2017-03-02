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
using System.Windows.Threading;

namespace APC
{
    /// <summary>
    /// Interaction logic for Results.xaml
    /// </summary>
    public partial class Results : Window
    {
        private DispatcherTimer timer = new DispatcherTimer();
        public Results()
        {
            InitializeComponent();

            SYS.Content = GlobalVarOfThemAll.SYS;
            DIA.Content = GlobalVarOfThemAll.DIA;

            timer.Interval = TimeSpan.FromMilliseconds(300000);
            timer.Tick += OnUpdateTimerTick;
            timer.Start();

        }

        private void OnUpdateTimerTick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
