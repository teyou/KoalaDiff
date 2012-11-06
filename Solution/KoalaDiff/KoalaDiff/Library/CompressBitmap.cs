using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoalaDiff.Library
{
    /// <summary>
    /// Compressed bitmap ( run-length encoding )
    /// </summary>
    public class CompressBitmap
    {
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public int Width { get; protected set; }
        public int Height { get; protected set; }
        private List<List<int>> rows;
        public CompressBitmap(int width, int height)
        {
            Width = width;
            Height = height;
            rows = new List<List<int>>(height);
        }

        public void AddRowData(List<int> columns)
        {
            rows.Add(columns);
        }

        public unsafe Bitmap Decode(int alpha)
        {
            _logger.Trace("Start Decode");
            Stopwatch sw = new Stopwatch();
            sw.Start();

            Bitmap newBitmap = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);

            BitmapData newData = newBitmap.LockBits(
                  new Rectangle(0, 0, Width, Height),
                  ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            //set the number of bytes per pixel
            int pixelSize = 4;
            Color highlightColor = Color.FromArgb(alpha, Color.Yellow);

            for (int y = 0; y < Height; y++)
            {
                //get the data from the new image
                byte* nRow = (byte*)newData.Scan0 + (y * newData.Stride);

                var highlightedPoint = rows[y];

                foreach (var col in highlightedPoint)
                {
                    nRow[col * pixelSize] = highlightColor.B; //B
                    nRow[col * pixelSize + 1] = highlightColor.G; //G
                    nRow[col * pixelSize + 2] = highlightColor.R; //R
                    nRow[col * pixelSize + 3] = highlightColor.A;
                }
            }

            //unlock the bitmaps
            newBitmap.UnlockBits(newData);

            sw.Stop();
            _logger.Trace("Finished Decode takes {0}", sw.Elapsed.ToString());
            return newBitmap;

        }
    }
}
