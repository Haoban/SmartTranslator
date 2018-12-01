using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace TobiiTest
{
    class ScreenshotUtil
    {
        // TODO: pass size of screenshot as parameter
        public static Bitmap TakeScreen(double x, double y)
        {
            //Create a new bitmap.
            var bmpScreenshot = new Bitmap(225,
                                           225,
                                           PixelFormat.Format32bppArgb);

            // Create a graphics object from the bitmap.
            var gfxScreenshot = Graphics.FromImage(bmpScreenshot);

            int roundx = (int)Math.Round(x); int roundy = (int)Math.Round(y);

            gfxScreenshot.CopyFromScreen(roundx - 112,
                                        roundy - 112,
                                        0,
                                        0,
                                        new Size(225,255),
                                        CopyPixelOperation.SourceCopy);

            return bmpScreenshot;
        }
    }
}
