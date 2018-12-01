using System;
using System.Collections.Generic;
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
using Tobii.Interaction;
using System.Windows.Forms.Integration;
using System.Runtime.InteropServices;

namespace TobiiTest
{
    
    public partial class MainWindow : Window
    {

        private readonly Timer _timer;
        Preferences pref = new Preferences();

        public MainWindow()
        {
            InitializeComponent();
            // Fill source language combo box
            foreach (string lang in OCRUtil.AvailableOCRLangs().Values)
            {
                sourceLanguageCB.Items.Add(lang);
            }
            // Fill target language combo box
            targetLanguageCB.Items.Clear();
            targetLanguageCB.Items.Add(new ComboBoxItem()
            {
                Content = "Select Target Language",
                Visibility = Visibility.Collapsed
            });
            targetLanguageCB.SelectedIndex = 0;
            switch (pref.Get("translator"))
            {
                case "google":
                    foreach (string lang in GoogleTranslator.Languages)
                    {
                        targetLanguageCB.Items.Add(lang);
                    }
                    break;
                case "microsoft":
                    foreach (string lang in MicrosoftTranslator.Languages.Keys)
                    {
                        targetLanguageCB.Items.Add(lang);
                    }
                    break;
            }
            foreach (string lang in OCRUtil.AvailableOCRLangs().Values)
            {
                sourceLanguageCB.Items.Add(lang);
            }

            _timer = new Timer(250); //Updates every quarter second.
            _timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            _timer.Enabled = true;
            //OCRUtil.RecognizeImage("n48.jpg");
            //OCRUtil.RecognizeImage("Screen718973825.639451.png");
        }
        
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            //var gazePointDataStream = ((App)Application.Current).Host.Streams.CreateGazePointDataStream();
            //var gazePointDataStream = ((App)Application.Current).Host.Streams.CreateGazePointDataStream(Tobii.Interaction.Framework.GazePointDataMode.Unfiltered);
          
            //var fixationDataStream = ((App)Application.Current).Host.Streams.CreateFixationDataStream();
            /*
           fixationDataStream.Begin((x, y, timestamp) => Dispatcher.BeginInvoke(new Action(() => {
               lbl1.Content = "Begin fixation at X: " + x + " Y: " + y;
               //var abc = ScreenshotUtil.TakeScreen(x, y, timestamp);
               //OCRUtil.RecognizeImage(abc);
           })));
           fixationDataStream.Data((x, y, timestamp) => Dispatcher.BeginInvoke(new Action(() =>
               lbl2.Content = String.Format("During fixation at X: {0} Y: {1}", x, y)
           )));
           fixationDataStream.End((x, y, timestamp) => Dispatcher.BeginInvoke(new Action(() =>
               lbl3.Content = String.Format("End fixation at X: {0} Y: {1}", x, y)
           )));
           */
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Preferences_Click(object sender, RoutedEventArgs e)
        {
            new Preference().ShowDialog();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            new About().ShowDialog();
        }
    }
}
