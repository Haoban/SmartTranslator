using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace TobiiTest.Tests
{
    [TestFixture]
    public class OCRUtilTests
    {
        [Test]
        public void TestMagnifyImage()
        {
            Image image = Image.FromFile("C:/Users/wangb/source/repos/SmartTranslator/SmartTranslator/TobiiTest.Tests/1.png");
            Image result = OCRUtil.MagnifyImage(image, 5);
            Assert.AreEqual((image.Width)*5, result.Width);
        }

        [Test]
        public void TestRecognizeImage()
        {
            Image image = Image.FromFile("C:/Users/wangb/source/repos/SmartTranslator/SmartTranslator/TobiiTest.Tests/3.jpg");
            string result = OCRUtil.RecognizeImage((Bitmap)image);
            Assert.AreNotEqual("0",result.Length);
        }
    }
}
