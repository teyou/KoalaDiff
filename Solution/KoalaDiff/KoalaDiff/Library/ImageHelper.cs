using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Runtime.Caching;
using System.Drawing.Drawing2D;
using AForge.Imaging;

namespace KoalaDiff.Library
{
    public static class ImageHelper
    {
        /// <summary>
        /// Logger
        /// </summary>
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public static unsafe Bitmap HightlightDifferences(UnmanagedImage original, UnmanagedImage compare, int threshold, float alpha)
        {

            if (original.Height != compare.Height || original.Width != compare.Width)
            {
                throw new Exception("Image resolution (width * height) should be same");
            }

            int alphaInt = AlphaConverter(alpha);
            string cacheKey = string.Format("HD_{0}", threshold);

            Stopwatch sw = new Stopwatch();
            sw.Start();

            if (CacheHelper.Get(cacheKey) != null)
            {
                sw.Stop();

                _logger.Trace("HightlightDifferences2_Image from cachekey : [{0}] , takes: {1}", cacheKey, sw.Elapsed.ToString());
                return (CacheHelper.Get(cacheKey) as CompressBitmap).Decode(alphaInt);
            }

            //create an empty bitmap the same size as original
            var unNewBitmap = UnmanagedImage.Create(original.Width, original.Height, original.PixelFormat);
            sw.Stop();

            _logger.Trace("HightlightDifferences2_cloneData [{0}] takes {1}", cacheKey, sw.Elapsed.ToString());
            sw.Restart();

            //sw.Stop();
            //_logger.Trace("HightlightDifferences2_initData [{0}] takes {1}", cacheKey, sw.Elapsed.ToString());
            //sw.Restart();
            
            //set the number of bytes per pixel
            int pixelSize = System.Drawing.Image.GetPixelFormatSize(original.PixelFormat) / 8;

            Color highlightColor = Color.FromArgb(alphaInt, Color.Yellow);

            // for cache compression
            CompressBitmap cb = new CompressBitmap(original.Width, original.Height);
            
            //get the data from the original image
            byte* oRow = (byte*)original.ImageData.ToPointer();
            //get the data from the comparison image
            byte* cRow = (byte*)compare.ImageData.ToPointer();
            //get the data from the new image
            byte* nRow = (byte*)unNewBitmap.ImageData.ToPointer(); 
            
            int offset = unNewBitmap.Stride - unNewBitmap.Width * pixelSize;


            for (int y = 0; y < original.Height; y++)
            {
                List<int> highlightedRows = new List<int>();
                for (int x = 0; x < original.Width; x++, oRow += pixelSize, cRow += pixelSize, nRow += pixelSize)
                {
                    int oB = oRow[RGB.B];
                    int oG = oRow[RGB.G];
                    int oR = oRow[RGB.R];
                    
                    int cB = cRow[RGB.B];
                    int cG = cRow[RGB.G];
                    int cR = cRow[RGB.R];

                    //check if the pixel RGB value is similar (under threshold)                    
                    bool isWithinThreshold = ((oB - cB) * (oB - cB) + (oG - cG) * (oG - cG) + (oR - cR) * (oR - cR)) <= (threshold * threshold); //euclidean distance
                    ///TODO: check the algorithm                    
                    //bool isWithinThreshold = (Math.Abs(oR - cR) + Math.Abs(oG - cG) + Math.Abs(oB - cB)) < threshold; //citiblock distance
                    
                    if (!isWithinThreshold) //highlight process          
                    {
                        highlightedRows.Add(x);
                        //different pixel value
                        nRow[RGB.B] = highlightColor.B; //B
                        nRow[RGB.G] = highlightColor.G; //G
                        nRow[RGB.R] = highlightColor.R; //R
                        nRow[RGB.A] = highlightColor.A;
                    }
                }
                oRow += offset;
                cRow += offset;
                nRow += offset;

                cb.AddRowData(highlightedRows);
            }

            sw.Stop();

            _logger.Trace("HightlightDifferences2_[{0}] takes {1}", cacheKey, sw.Elapsed.ToString());

            CacheHelper.Add(cacheKey, cb);

            return unNewBitmap.ToManagedImage();
        }

        public static Bitmap MatrixBlend(Bitmap original, Bitmap compare, byte alpha)
        {
            return MatrixBlend(UnmanagedImage.FromManagedImage(original), compare, alpha);
        }

        public static Bitmap MatrixBlend(UnmanagedImage uOriginal, Bitmap compare, byte alpha)
        {
            //ref: http://ithoughthecamewithyou.com/post/Fastest-image-merge-(alpha-blend)-in-GDI2b.aspx

            string cacheKey = string.Format("MB_{0}", alpha);

            Stopwatch sw = new Stopwatch();
            sw.Start();

            // for the matrix the range is 0.0 - 1.0
            float alphaNorm = (float)alpha / 255.0F;

            // just change the alpha
            ColorMatrix matrix = new ColorMatrix(new float[][]{
                new float[] {1F, 0, 0, 0, 0},
                new float[] {0, 1F, 0, 0, 0},
                new float[] {0, 0, 1F, 0, 0},
                new float[] {0, 0, 0, alphaNorm, 0},
                new float[] {0, 0, 0, 0, 1F}});

            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(matrix);

            //create a blank bitmap the same size as original
            Bitmap newBitmap = uOriginal.Clone().ToManagedImage();
            sw.Stop();
            _logger.Trace("MatrixBlend_newBitmap [{0}] takes {1}", cacheKey, sw.Elapsed.ToString());

            sw.Restart();
            //get a graphics object from the new image            
            using (Graphics g = Graphics.FromImage(newBitmap))
            {
                g.CompositingMode = CompositingMode.SourceOver;
                g.CompositingQuality = CompositingQuality.HighSpeed;

                g.DrawImage(compare,
                    new Rectangle(0, 0, uOriginal.Width, uOriginal.Height),
                    0,
                    0,
                    compare.Width,
                    compare.Height,
                    GraphicsUnit.Pixel,
                    imageAttributes);
            }
            sw.Stop();
            _logger.Trace("MatrixBlend_drawImage [{0}] takes {1}", cacheKey, sw.Elapsed.ToString());

            return newBitmap;
        }

        private static int AlphaConverter(float alpha)
        {
            return Convert.ToInt32(255.0 * alpha);
        }
    }
}
