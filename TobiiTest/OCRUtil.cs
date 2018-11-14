using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;



namespace TobiiTest
{
    class OCRUtil
    {
        // Magnifies image by factor
        public static Image MagnifyImage(Image image, int factor)
        {
            return ResizeImage(image, image.Width * factor, image.Height * factor);
        }

        // From: https://stackoverflow.com/questions/1922040/how-to-resize-an-image-c-sharp
        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public static void RecognizeImage(string path)
        {
            var bm = Bitmap.FromFile(path);
            var abc = MagnifyImage(Bitmap.FromFile(path), 5); // 4 or 5 should be enough... customizable?
            RecognizeImage((Bitmap)abc);
        }

        // From: https://github.com/charlesw/tesseract-samples/blob/master/src/Tesseract.ConsoleDemo/Program.cs
        public static string RecognizeImage(Bitmap abc)
        {
            //var testImagePath = "./phototest.tif";
            /*
            if (args.Length > 0)
            {
                testImagePath = args[0];
            }
            */
            
            try
            {
                string text;
                using (var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default))
                {
                    //using (var img = Pix.LoadFromFile(testImagePath))
                    //engine.SetVariable("textord_min_linesize", 3);
                    using (var img = PixConverter.ToPix(abc))
                    {
                        using (var page = engine.Process(img))
                        {
                            text = page.GetText();
                            Console.WriteLine("Mean confidence: {0}", page.GetMeanConfidence());

                            Console.WriteLine("Text (GetText): \r\n{0}", text);

                            //Call the translator
                            Translator t = new Translator();
                            string targetlanguage;
                            string sourcelanguage;

                            targetlanguage = "Chinese";
                            sourcelanguage = "English";
                            string translation = t.Translate(text, sourcelanguage, targetlanguage);
                            Console.WriteLine("Text (TranslatedText): \r\n{0}",translation);
                            //end

                            Console.WriteLine("Text (iterator):");
                    
                            using (var iter = page.GetIterator())
                            {
                                iter.Begin();

                                do
                                {
                                    do
                                    {
                                        do
                                        {
                                            do
                                            {
                                                if (iter.IsAtBeginningOf(PageIteratorLevel.Block))
                                                {
                                                    Console.WriteLine("<BLOCK>");
                                                }

                                                Console.Write(iter.GetText(PageIteratorLevel.Word));
                                                Console.Write(" ");

                                                if (iter.IsAtFinalOf(PageIteratorLevel.TextLine, PageIteratorLevel.Word))
                                                {
                                                    Console.WriteLine();
                                                }
                                            } while (iter.Next(PageIteratorLevel.TextLine, PageIteratorLevel.Word));

                                            if (iter.IsAtFinalOf(PageIteratorLevel.Para, PageIteratorLevel.TextLine))
                                            {
                                                Console.WriteLine();
                                            }
                                        } while (iter.Next(PageIteratorLevel.Para, PageIteratorLevel.TextLine));
                                    } while (iter.Next(PageIteratorLevel.Block, PageIteratorLevel.Para));
                                } while (iter.Next(PageIteratorLevel.Block));
                            }                                                 
                        }
                    }
                    
                }
                return text;
            }
     
           

            catch (Exception e)
            {
                Trace.TraceError(e.ToString());
                Console.WriteLine("Unexpected Error: " + e.Message);
                Console.WriteLine("Details: ");
                Console.WriteLine(e.ToString());
                return "";
            }

        }
    }
}
