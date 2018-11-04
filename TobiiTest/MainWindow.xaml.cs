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

namespace TobiiTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly Timer _timer;

        public MainWindow()
        {
            InitializeComponent();
            _timer = new Timer(250); //Updates every quarter second.
            _timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            _timer.Enabled = true;
            //OCRUtil.RecognizeImage("n48.jpg");
            OCRUtil.RecognizeImage("Screen24229070,0274542.png");
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            //var gazePointDataStream = ((App)Application.Current).Host.Streams.CreateGazePointDataStream();
            //var gazePointDataStream = ((App)Application.Current).Host.Streams.CreateGazePointDataStream(Tobii.Interaction.Framework.GazePointDataMode.Unfiltered);
            /*
            var fixationDataStream = ((App)Application.Current).Host.Streams.CreateFixationDataStream();
            fixationDataStream.Begin((x, y, timestamp) => Dispatcher.BeginInvoke(new Action(() => {
                lbl1.Content = "Begin fixation at X: " + x + " Y: " + y;
                //var abc = ScreenshotUtil.Abc(x, y, timestamp);
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

    }
}
