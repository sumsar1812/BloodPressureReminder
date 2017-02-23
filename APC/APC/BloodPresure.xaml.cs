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
using System.Windows.Threading;
using System.Speech.Synthesis;

namespace APC
{
    /// <summary>
    /// Interaction logic for BloodPresure.xaml
    /// </summary>
    public partial class BloodPresure : Window
    {
        public SpeechRecognizer SP;
        private static List<BloodMesurment> BloodPresures = new List<BloodMesurment>();
        private DispatcherTimer timer;
        private int TimerSeconds;
        public SpeechSynthesizer synthesizer;
        private STATE Status_State;
        private STATE prevStatus_State;

        public BloodPresure()
        {
            InitializeComponent();
            Status_State = STATE.START;
            synthesizer = new SpeechSynthesizer();
            synthesizer.SetOutputToDefaultAudioDevice();
            timer = new DispatcherTimer();
            string[] settings = new string[] { "192.168.1.123", "9005" };
            //Start the IEEE11083 driver as a facade controller
            var facade = new IEEE11073ParserFacade();
            facade.NewIEEE11073Measurement += NewMeasurementEvent;
            TimerSeconds = 20;
            facade.StartIEEE11073Parser(settings);
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += OnUpdateTimerTick;
            timer.Start();
        }

        private void OnUpdateTimerTick(object sender, EventArgs e)
        {
            switch (Status_State)
            {


                case STATE.START:
                    {
                    state_start_timerevent();
                    break;
                    }
                case STATE.MEASSURING:
                    {
                        state_meassuring_timerevent();
                        break;
                    }
                case STATE.MEASSUREMENT1:
                    {
                        state_meassurement1_timerevent();
                        break;
                    }
                case STATE.MEASSUREMENT2:
                    {
                        state_meassurement2_timerevent();
                        break;
                    }
                case STATE.MEASSUREMENT3:
                    {
                        state_meassurement3_timerevent();
                        break;
                    }
                case STATE.FINISHED:
                    {
                        state_finished_timerevent();
                        break;
                    }
            }
        }

        private void state_start_timerevent()
        {
            if(TimerSeconds <= 0)
            {
                prevStatus_State = Status_State;
                Status_State = STATE.MEASSURING;
                return;
            }
            TimerSeconds -= 1;
            TimeSpan t = TimeSpan.FromSeconds(TimerSeconds);
            TimerStatusBox.Content = t.ToString(@"mm\:ss");
        }
        private void state_meassuring_timerevent()
        {
            TimerSeconds = 10;
            switch(prevStatus_State)
            {
                case STATE.START:
                    {
                        TimerStatusBox.Content = "Messurment Time";
                        speak("Put on thing, and perform first messurement");
                        prevStatus_State = Status_State;
                        Status_State = STATE.MEASSUREMENT1;
                        break;
                    }
                case STATE.MEASSUREMENT1:
                    {
                        TimerStatusBox.Content = "Messurment Time";
                        speak("Put on thing, and perform second messurement");
                        prevStatus_State = Status_State;
                        Status_State = STATE.MEASSUREMENT2;
                        break;
                    }
                case STATE.MEASSUREMENT2:
                    {
                        TimerStatusBox.Content = "Messurment Time";
                        speak("Put on thing, and perform third messurement");
                        prevStatus_State = Status_State;
                        Status_State = STATE.MEASSUREMENT3;
                        break;
                    }
                case STATE.MEASSUREMENT3:
                    {
                        


                        this.Close();
                        break;
                    }
            }

        }
        private void state_meassurement1_timerevent()
        {
            //we wait for a valid messurement before we continue!
            if(true)
            {
                prevStatus_State = Status_State;
                Status_State = STATE.FINISHED;
            }

        }
        private void state_meassurement2_timerevent()
        {
            //we wait for a valid messurement before we continue!
            if (true)
            {
                prevStatus_State = Status_State;
                Status_State = STATE.FINISHED;
            }

        }
        private void state_meassurement3_timerevent()
        {
            //we wait for a valid messurement before we continue!
            TimerStatusBox.Content = "GOOD WORK, YOU HAVE DONE GOOD WORK SON";
            if (false)
            {
               // WE ARE NOW COMPLETLY DONE! SAVE DATA TO FILE OR DATABASE AND CLOSE THIS WINDOW
            }
        }
        private void state_finished_timerevent()
        {

            if (TimerSeconds <= 0)
            {
                
                Status_State = STATE.MEASSURING;
                return;
            }
            TimerSeconds -= 1;
            TimeSpan t = TimeSpan.FromSeconds(TimerSeconds);
            TimerStatusBox.Content = t.ToString(@"mm\:ss");
             
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

        private void speak(string Text)
        {
            synthesizer.SpeakAsync(Text);
            Speech.Text = Text;

        }
    }

    public class BloodMesurment
    {
        public DateTime Timestamp { get; set; }
        public ParseResultType DataType { get; set; }
        public double DataValue { get; set; }
        public String Unit { get; set; }
    }


    public enum STATE{
        START, MEASSURING, MEASSUREMENT1, MEASSUREMENT2, MEASSUREMENT3,FINISHED
    }

    

}
