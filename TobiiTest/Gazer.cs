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
    public class Gazer
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
