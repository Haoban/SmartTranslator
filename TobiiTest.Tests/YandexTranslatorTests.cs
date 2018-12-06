using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TobiiTest;

namespace TobiiTest.Tests
{
    [TestFixture()]
    public class YandexTranslatorTests
    {
        [Test()]
        public void TranslateText()
        {
            var s = "Hello, world!";
            var ms = new YandexTranslator("English", "Finnish");
            var tl = ms.Translate(s);
            Assert.AreEqual("Hei, maailma!", tl);
            //Assert.Pass("Auto pass to reduce character consumption");
        }
    }
}
