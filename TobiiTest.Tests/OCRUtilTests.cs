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
using System.Reflection;
using System.Net;

namespace TobiiTest.Tests
{
    [TestFixture]
    public class OCRUtilTests
    {
        [Test]
        public void TestMagnifyImage()
        {
            //Console.WriteLine(Directory.GetCurrentDirectory());
            //Image image = Image.FromFile(Resources.image);
            Image image = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "1.png"));
            Image result = OCRUtil.MagnifyImage(image, 5);
            Assert.AreEqual((image.Width)*5, result.Width);
        }

        [Test]
        public void TestRecognizeImage()
        {
            //var x = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase),"3.jpg");
            //Image image = Image.FromFile(x);
            Image image = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "3.jpg"));
            string result = OCRUtil.RecognizeImage((Bitmap)image);
            Assert.AreNotEqual("0",result.Length);
        }
    }
}
