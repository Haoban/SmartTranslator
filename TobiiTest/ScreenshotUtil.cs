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
        public static Bitmap TakeScreen(double x, double y)
        {
            //Create a new bitmap.
            var bmpScreenshot = new Bitmap(225,
                                           225,
                                           PixelFormat.Format32bppArgb);

            // Create a graphics object from the bitmap.
            var gfxScreenshot = Graphics.FromImage(bmpScreenshot);

            // Take the screenshot from the upper left corner to the right bottom corner.
            /*
            gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                        Screen.PrimaryScreen.Bounds.Y,
                                        0,
                                        0,
                                        Screen.PrimaryScreen.Bounds.Size,
                                        CopyPixelOperation.SourceCopy);

            */
            int roundx = (int)Math.Round(x); int roundy = (int)Math.Round(y);

            gfxScreenshot.CopyFromScreen(roundx - 112,
                                        roundy - 112,
                                        0,
                                        0,
                                        new Size(225,255),
                                        CopyPixelOperation.SourceCopy);
            // Save the screenshot to the specified path that the user has chosen.
            //bmpScreenshot.Save("Screen" + timestamp.ToString() + ".png", ImageFormat.Png);
            return bmpScreenshot;
        }
    }
}
