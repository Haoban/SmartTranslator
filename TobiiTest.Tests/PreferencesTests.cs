using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TobiiTest.Tests
{
    [TestFixture]
    public class PreferencesTests
    {
        [Test]
        public void TestUpdate()
        {
            Preferences pref = new Preferences();
            pref.Update("key", Key.RightCtrl.ToString());
            string key = pref.Get("key");
            Assert.AreEqual("RightCtrl", key);
        }
    }
}
