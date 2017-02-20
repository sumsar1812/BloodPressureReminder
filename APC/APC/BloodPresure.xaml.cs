using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
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
using IEEE11073Parser;

namespace APC
{
    /// <summary>
    /// Interaction logic for BloodPresure.xaml
    /// </summary>
    public partial class BloodPresure : Window
    {
        public SpeechRecognizer SP;
        private static List<BloodMesurment> BloodPresures = new List<BloodMesurment>();
        public BloodPresure()
        {
            InitializeComponent();

            string[] settings = new string[] { "192.168.1.123", "9005" };
            //Start the IEEE11083 driver as a facade controller
            var facade = new IEEE11073ParserFacade();
            facade.NewIEEE11073Measurement += NewMeasurementEvent;
            facade.StartIEEE11073Parser(settings);
        }

        void test(object sender, SpeechRecognizedEventArgs e)
        {
            //command -> e.Result.Text;
        }

        static void NewMeasurementEvent(object sender, NewIEEE11073MeasurementArgs e)
        {
           
            Console.WriteLine("Data: " + e.data.DeviceMeasurementDateTime + " " + e.data.Type + " " + e.data.Value + " " + e.data.Unit);
            BloodPresures.Add(new BloodMesurment()
            {
                DataType = e.data.Type,
                DataValue = e.data.Value,
                Timestamp = e.data.DeviceMeasurementDateTime,
                Unit = e.data.Unit
            });
        }
    }

    public class BloodMesurment
    {
        public DateTime Timestamp { get; set; }
        public ParseResultType DataType { get; set; }
        public double DataValue { get; set; }
        public String Unit { get; set; }
    }

}
