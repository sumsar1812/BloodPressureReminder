using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
using WpfAnimatedGif;

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
        private int SpeakTimeDelay = 5000;

        private static bool GotMesurement = false;
        string[] settings = new string[] { "192.168.0.100", "9005" };

        public BloodPresure()
        {
            InitializeComponent();
            Status_State = STATE.START;
            synthesizer = new SpeechSynthesizer();
            synthesizer.SetOutputToDefaultAudioDevice();
            mediaPlayer = new MediaPlayer();
            mediaPlayer.Open(new Uri("song.mp3",UriKind.Relative));
            timer = new DispatcherTimer();

            TimerSeconds = 300;
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

        /// <summary>
        /// Only one of them may be true or the world ends.... Love..
        /// </summary>
        /// <param name="on"></param>
        /// <param name="off"></param>
        /// <param name="water"></param>
        public void ChangePrettyImage(bool on, bool off, bool water)
        {
            if (on)
            {
                //pretty
                InstructionImageWater.Visibility = Visibility.Hidden;
                InstructionCuffOn.Visibility = Visibility.Visible;
                InstructionCuffOff.Visibility = Visibility.Hidden;
                // end pretty
            } else if (off)
            {
                //pretty
                InstructionImageWater.Visibility = Visibility.Hidden;
                InstructionCuffOn.Visibility = Visibility.Hidden;
                InstructionCuffOff.Visibility = Visibility.Visible;
                // end pretty  
            } else if (water)
            {
                //pretty
                InstructionImageWater.Visibility = Visibility.Visible;
                InstructionCuffOn.Visibility = Visibility.Hidden;
                InstructionCuffOff.Visibility = Visibility.Hidden;
                // end pretty
            }

        }

        private void state_meassuring_timerevent()
        {
            TimerSeconds = 60;
            switch(prevStatus_State)
            {
                case STATE.START:
                    {
                        TimerStatusBox.Content = "measurement Time";
                        mediaPlayer.Pause();

                        ChangePrettyImage(true,false,false);

                        speak("Put on cuff, and perform first measurement");
                        Thread.Sleep(SpeakTimeDelay);
                        mediaPlayer.Play();
                        prevStatus_State = Status_State;
                        Status_State = STATE.MEASSUREMENT1;
                        break;
                    }
                case STATE.MEASSUREMENT1:
                    {
                        TimerStatusBox.Content = "measurement Time";
                        mediaPlayer.Pause();

                        ChangePrettyImage(true, false, false);


                        speak("Put on thing, and perform second measurement");
                        Thread.Sleep(SpeakTimeDelay);
                        mediaPlayer.Play();
                        prevStatus_State = Status_State;
                        Status_State = STATE.MEASSUREMENT2;
                        break;
                    }
                case STATE.MEASSUREMENT2:
                    {
                        TimerStatusBox.Content = "measurement Time";
                        mediaPlayer.Pause();

                        ChangePrettyImage(true, false, false);

                        speak("Put on thing, and perform third measurement");
                        Thread.Sleep(SpeakTimeDelay);
                        mediaPlayer.Play();
                        prevStatus_State = Status_State;
                        Status_State = STATE.MEASSUREMENT3;
                        break;
                    }
                case STATE.MEASSUREMENT3:
                    {                       
                        Close();
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

            ChangePrettyImage(false, true, false);
            Thread.Sleep(5000);
            

            prevStatus_State = Status_State;
            Status_State = STATE.FINISHED;
            facade.NewIEEE11073Measurement -= NewMeasurementEvent;
            GotMesurement = false;
            mediaPlayer.Pause();
            speak("measurement done. Take the cuf off and relax");
            Thread.Sleep(SpeakTimeDelay);
            ChangePrettyImage(false, false, true);
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

            ChangePrettyImage(false, true, false);
            Thread.Sleep(5000);
            

            prevStatus_State = Status_State;
            Status_State = STATE.FINISHED;
            facade.NewIEEE11073Measurement -= NewMeasurementEvent;
            GotMesurement = false;
            mediaPlayer.Pause();
            speak("measurement done. Take the cuf off and relax");
            Thread.Sleep(SpeakTimeDelay);
            ChangePrettyImage(false, false, true);
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

            ChangePrettyImage(false, true, false);
            Thread.Sleep(5000);
            ChangePrettyImage(false, false, true);

            prevStatus_State = Status_State;
            Status_State = STATE.FINISHED;
            facade.NewIEEE11073Measurement -= NewMeasurementEvent;
            GotMesurement = false;
            mediaPlayer.Stop();
            //we wait for a valid messurement before we continue!
            TimerStatusBox.Content = "COMPLETE";
            speak("All Measurements complete. Have a wonderful day");

            GlobalVarOfThemAll.DIA = (int) ((BloodPresures[1].DiastolicValue+ BloodPresures[2].DiastolicValue)/2);
            GlobalVarOfThemAll.SYS = (int)((BloodPresures[1].SystolicValue + BloodPresures[2].SystolicValue) / 2);
            GlobalVarOfThemAll.MAF = (int)((BloodPresures[1].MAFValue + BloodPresures[2].MAFValue) / 2);

            Results ResultWindow = new Results();
            ResultWindow.ShowDialog();
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

            if (e.data.Type == ParseResultType.BloodPressure)
            {
                var a = (BloodPressureParseResult)e.data;

                Debug.WriteLine(a.DiastolicValue);
                Debug.WriteLine(a.MAFValue);
                Debug.WriteLine(a.SystolicValue);

                BloodMesurment bm = new BloodMesurment();
                bm.DiastolicValue = a.DiastolicValue;
                bm.MAFValue = a.MAFValue;
                bm.SystolicValue = a.SystolicValue;
                bm.DataType = e.data.Type.ToString();
                bm.Timestamp = e.data.DeviceMeasurementDateTime;

                BloodPresures.Add(bm);
                GotMesurement = true;
            }
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
        public double DiastolicValue { get; set; }
        public double MAFValue { get; set; }
        public double SystolicValue { get; set; }

    }


    public enum STATE{
        START, MEASSURING, MEASSUREMENT1, MEASSUREMENT2, MEASSUREMENT3,FINISHED
    }
}