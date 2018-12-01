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
        public Preferences pref;
        Gazer gazer;
        Translator translator;
        Tuple<double, double> coords;

        string SourceLanguage
        {
            get;
            set;
        }
        string TargetLanguage
        {
            get;
            set;
        }

        public MainWindow()
        {
            InitializeComponent();

            pref = new Preferences();
            gazer = new Gazer();
            //translator = Translator.Create(pref.Get("translator"), SourceLanguage, TargetLanguage);

            UpdateSourceCB();
            UpdateTargCB();

            sourceLanguageCB.SelectionChanged += SourceLanguageCB_SelectionChanged;
            targetLanguageCB.SelectionChanged += TargetLanguageCB_SelectionChanged;

            _timer = new Timer(250); //Updates every quarter second.
            _timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            _timer.Enabled = true;
            //OCRUtil.RecognizeImage("n48.jpg");
            //OCRUtil.RecognizeImage("Screen718973825.639451.png");
        }

        // Fill source language combo box
        private void UpdateSourceCB()
        {
            sourceLanguageCB.Items.Clear();
            
            sourceLanguageCB.Items.Add(new ComboBoxItem()
            {
                Content = "Select Source Language",
                Visibility = Visibility.Collapsed
            });
            sourceLanguageCB.SelectedIndex = 0;
            
            foreach (string lang in OCRUtil.AvailableOCRLangs().Values)
            {
                sourceLanguageCB.Items.Add(lang);
            }
            targetLanguageCB.SelectedIndex = 0;
        }

        // Fill target language combo box
        private void UpdateTargCB()
        {
            targetLanguageCB.Items.Clear();
            
            targetLanguageCB.Items.Add(new ComboBoxItem()
            {
                Content = "Select Target Language",
                Visibility = Visibility.Collapsed
            });
            targetLanguageCB.SelectedIndex = 0;
            
            switch (pref.Get("translator"))
            {
                case "Google":
                    foreach (string lang in GoogleTranslator.Languages)
                    {
                        targetLanguageCB.Items.Add(lang);
                    }
                    break;
                case "Microsoft":
                    foreach (string lang in MicrosoftTranslator.Languages.Keys)
                    {
                        targetLanguageCB.Items.Add(lang);
                    }
                    break;
                default:
                    throw new ArgumentException("Unknown translator: " + pref.Get("translator"));
            }
            targetLanguageCB.SelectedIndex = 0;
        }
        
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            
        }

        private void Preferences_Click(object sender, RoutedEventArgs e)
        {
            var pr = new Preference(pref);
            Console.WriteLine(pr.prefs);
            pr.ShowDialog();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            new About().ShowDialog();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("pressed: " + e.Key.ToString());
            Console.WriteLine("pref: " + Enum.Parse(typeof(Key), pref.Get("key")));
            //Console.WriteLine(e.Key.ToString().Equals(Enum.Parse(typeof(Key), pref.Get("key")).ToString()));
            //Console.WriteLine(e.Key.ToString() == Enum.Parse(typeof(Key), pref.Get("key")));
            //Console.WriteLine("LeftCtrl".Equals("LeftCtrl"));
            if (e.Key.ToString().Equals(Enum.Parse(typeof(Key), pref.Get("key")).ToString()))
            {
                Console.WriteLine("Right key pressed.");
                Console.WriteLine("Checking languages...");
                Console.WriteLine("Checking gazer...");
                Console.WriteLine("State: " + gazer.State);
                if (!gazer.State)
                {
                    Console.WriteLine("Starting gazer...");
                    gazer.Start();
                }
                else
                {
                    Console.WriteLine("Stopping gazer...");
                    var coords = gazer.Stop();
                    var screen = ScreenshotUtil.TakeScreen(coords.Item1, coords.Item2);
                    var text = OCRUtil.RecognizeImage(screen);
                    srcTextTB.Text = text;
                    
                    translator = Translator.Create(pref.Get("translator"), SourceLanguage, TargetLanguage);
                    Console.WriteLine("current translator: " + translator);
                    var tl = translator.Translate(text);

                    // Debug
                    /*
                    var gt = (GoogleTranslator)translator;
                    if (gt.Error != null)
                    {
                        Console.WriteLine(gt.Error.Message);
                        Console.WriteLine(gt.Error.StackTrace);
                    }
                       */ 
                    Console.WriteLine(tl);
                    targTextTB.Text = tl;
                }
            }
        }

        private void SourceLanguageCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!targetLanguageCB.SelectedItem.ToString().Equals("Select Target Language"))
            {
                SourceLanguage = sourceLanguageCB.SelectedItem.ToString();
                //translator.SourceLanguage = sourceLanguageCB.SelectedItem.ToString();
            }
                
        }

        private void TargetLanguageCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!targetLanguageCB.SelectedItem.ToString().Equals("Select Target Language"))
            {
                //translator.TargetLanguage = (string)targetLanguageCB.SelectedItem.ToString();
                TargetLanguage = (string)targetLanguageCB.SelectedItem.ToString();
            }
        }
    }
}
