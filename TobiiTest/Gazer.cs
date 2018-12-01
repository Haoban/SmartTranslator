using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Tobii.Interaction;

namespace TobiiTest
{
    class Gazer
    {
        public bool State
        {
            get;
            private set;
        }

        private Tuple<double, double> coords;
        private FixationDataStream fixationDataStream = ((App)Application.Current).Host.Streams.CreateFixationDataStream();

        public Gazer()
        {
            State = false;
            fixationDataStream.IsEnabled = false;
            fixationDataStream.Begin((x, y, timestamp) =>
            {
                coords = Tuple.Create(x, y);
            });
        }

        public void Start()
        {
            if (State)
            {
                throw new InvalidOperationException("Trying to start Gazer, when it's started.");
            }
            else
            {
                State = true;
                fixationDataStream.IsEnabled = true;
                //var gazePointDataStream = ((App)Application.Current).Host.Streams.CreateGazePointDataStream();
                //var gazePointDataStream = ((App)Application.Current).Host.Streams.CreateGazePointDataStream(Tobii.Interaction.Framework.GazePointDataMode.Unfiltered);

                //fixationDataStream.Begin((x, y, timestamp) => Dispatcher.BeginInvoke(new Action(() => {
                /*
                fixationDataStream.Begin((x, y, timestamp) =>
                {
                    this.x = x;
                    this.y = y;
                });
                */
                /*
               fixationDataStream.Data((x, y, timestamp) => Dispatcher.BeginInvoke(new Action(() =>
                   lbl2.Content = String.Format("During fixation at X: {0} Y: {1}", x, y)
               )));
               fixationDataStream.End((x, y, timestamp) => Dispatcher.BeginInvoke(new Action(() =>
                   lbl3.Content = String.Format("End fixation at X: {0} Y: {1}", x, y)
               )));
               */
            }
        }

        public Tuple<double, double> Stop()
        {
            if (!State)
            {
                throw new InvalidOperationException("Trying to stop Gazer, when it's stopped.");
            }
            else
            {
                State = false;
                fixationDataStream.IsEnabled = false;
                return coords;
            }
        }

    }
}
