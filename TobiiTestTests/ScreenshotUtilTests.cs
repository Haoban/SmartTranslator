using NUnit.Framework;
using TobiiTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace TobiiTest.Tests
{
    [TestFixture()]
    public class ScreenshotUtilTests
    {
        [Test()]
        public void TakeScreenTest()
        {
            ScreenshotUtil.TakeScreen(0, 0, Tuple.Create(10,10));
            Assert.Fail();
        }
    }
}