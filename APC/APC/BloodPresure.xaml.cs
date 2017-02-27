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
using System.Threading;

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
        private MediaPlayer mediaPlayer;
        private int SpeakTimeDelay = 6000;

        private static bool GotMesurement = false;
        string[] settings = new string[] { "192.168.0.100", "9005" };

        public BloodPresure()
        {
            InitializeComponent();
            InstructionImage.Source = new BitmapImage(new Uri(,));
            Status_State = STATE.START;
            synthesizer = new SpeechSynthesizer();
            synthesizer.SetOutputToDefaultAudioDevice();
            mediaPlayer = new MediaPlayer();
            mediaPlayer.Open(new Uri("song.mp3",UriKind.Relative));
            timer = new DispatcherTimer();

            TimerSeconds = 20;
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += OnUpdateTimerTick;
            timer.Start();
            mediaPlayer.Play();
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
                        mediaPlayer.Pause();
                        speak("Put on thing, and perform first messurement");
                        Thread.Sleep(SpeakTimeDelay);
                        mediaPlayer.Play();
                        prevStatus_State = Status_State;
                        Status_State = STATE.MEASSUREMENT1;
                        break;
                    }
                case STATE.MEASSUREMENT1:
                    {
                        TimerStatusBox.Content = "Messurment Time";
                        mediaPlayer.Pause();
                        speak("Put on thing, and perform second messurement");
                        Thread.Sleep(SpeakTimeDelay);
                        mediaPlayer.Play();
                        prevStatus_State = Status_State;
                        Status_State = STATE.MEASSUREMENT2;
                        break;
                    }
                case STATE.MEASSUREMENT2:
                    {
                        TimerStatusBox.Content = "Messurment Time";
                        mediaPlayer.Pause();
                        speak("Put on thing, and perform third messurement");
                        Thread.Sleep(SpeakTimeDelay);
                        mediaPlayer.Play();
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
            mediaPlayer.Play();
            var facade = new IEEE11073ParserFacade();
            facade.NewIEEE11073Measurement += NewMeasurementEvent;
            facade.StartIEEE11073Parser(new[] { "192.168.0.100", "9005" });

            //we wait for a valid messurement before we continue!
            while (!GotMesurement)
            {

            }

            prevStatus_State = Status_State;
            Status_State = STATE.FINISHED;
            facade.NewIEEE11073Measurement -= NewMeasurementEvent;
            GotMesurement = false;
            mediaPlayer.Pause();
            speak("Measurement done. Take the cuf off and relax");
            Thread.Sleep(SpeakTimeDelay);
            mediaPlayer.Play();

        }
        private void state_meassurement2_timerevent()
        {

            var facade = new IEEE11073ParserFacade();
            facade.NewIEEE11073Measurement += NewMeasurementEvent;
            facade.StartIEEE11073Parser(new[] { "192.168.0.100", "9005" });

            //we wait for a valid messurement before we continue!
            while (!GotMesurement)
            {

            }

            prevStatus_State = Status_State;
            Status_State = STATE.FINISHED;
            facade.NewIEEE11073Measurement -= NewMeasurementEvent;
            GotMesurement = false;
            mediaPlayer.Pause();
            speak("Measurement done. Take the cuf off and relax");
            Thread.Sleep(SpeakTimeDelay);
            mediaPlayer.Play();
        }
        private void state_meassurement3_timerevent()
        {
            var facade = new IEEE11073ParserFacade();
            facade.NewIEEE11073Measurement += NewMeasurementEvent;
            facade.StartIEEE11073Parser(new[] { "192.168.0.100", "9005" });

            //we wait for a valid messurement before we continue!
            while (!GotMesurement)
            {

            }

            prevStatus_State = Status_State;
            Status_State = STATE.FINISHED;
            facade.NewIEEE11073Measurement -= NewMeasurementEvent;
            GotMesurement = false;
            mediaPlayer.Stop();
            //we wait for a valid messurement before we continue!
            TimerStatusBox.Content = "COMPLETE";
            speak("All Measurements complete. Have a wonderful day");

            //TODO SAVE SOME SHIT HERE
            
            this.Close();

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
                DataType = e.data.Type.ToString(),
                DataValue = e.data.Value,
                Timestamp = e.data.DeviceMeasurementDateTime,
                Unit = e.data.Unit
                
            });

            GotMesurement = true;

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
        public String DataType { get; set; }
        public double DataValue { get; set; }
        public String Unit { get; set; }
    }


    public enum STATE{
        START, MEASSURING, MEASSUREMENT1, MEASSUREMENT2, MEASSUREMENT3,FINISHED
    }
}
