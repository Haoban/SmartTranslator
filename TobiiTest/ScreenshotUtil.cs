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
    public class ScreenshotUtil
    {
        // TODO: pass size of screenshot as parameter
        public static Bitmap TakeScreen(double x, double y, Tuple<int, int> ssize)
        {
            int wid = ssize.Item1;
            int hei = ssize.Item2;

            //Create a new bitmap.
            var bmpScreenshot = new Bitmap(wid,
                                           hei,
                                           PixelFormat.Format32bppArgb);

            // Create a graphics object from the bitmap.
            var gfxScreenshot = Graphics.FromImage(bmpScreenshot);

            int ix = (int)Math.Round(x); int iy = (int)Math.Round(y);

            int screenw = Screen.PrimaryScreen.Bounds.Size.Width;
            int screenh = Screen.PrimaryScreen.Bounds.Size.Height;

            // Validation of top left corner
            if (ix - wid / 2 < 0)
                ix = 0;
            else if (ix + wid / 2 > screenw)
                ix = screenw - wid;
            else
                ix = ix - wid / 2;

            if (iy - hei / 2 < 0)
                iy = 0;
            else if (iy + hei / 2 > screenh)
                iy = screenh - hei;
            else
                iy = iy - hei / 2;

            gfxScreenshot.CopyFromScreen(ix,
                                        iy,
                                        0,
                                        0,
                                        new Size(wid,hei),
                                        CopyPixelOperation.SourceCopy);

            return bmpScreenshot;
        }
    }
}
