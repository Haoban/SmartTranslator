using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Tobii.Interaction;


namespace TobiiTest.Tests
{
    [TestFixture]
    public class GazerTests
    {
        [Test]
        public void TestStop()
        {
            // TODO: Add your test code here
            Gazer gazer = new Gazer();
            gazer.Start();
            Tuple<double, double> coods = gazer.Stop();
            Assert.IsNotNull(coods);
        }
    }
}
