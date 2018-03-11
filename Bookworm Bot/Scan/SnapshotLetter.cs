using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookworm.Scan
{
    public class SnapshotLetter
    {
        public Color[,] SimplifiedData { get; }
        public Bitmap Bitmap { get; }
        public bool IsDark { get; }

        public SnapshotLetter(Color[,] imgData, Bitmap thisLetterBitmap)
        {
            this.SimplifiedData = imgData;
            this.Bitmap = thisLetterBitmap;
            int darkPixels = 0;
            for (int x = 0; x < imgData.GetLength(0); x++)
            {
                for (int y = 0; y < imgData.GetLength(1); y++)
                {
                    if (IsDarkColor(imgData[x, y]))
                    {
                        darkPixels++;
                    }
                }
            }
            if (darkPixels > imgData.GetLength(0) * imgData.GetLength(1) * 0.7)
            {
                this.IsDark = true;
            }
        }
        
        private bool IsDarkColor(Color pixel)
        {
            return pixel.R < 50 && pixel.G < 50 && pixel.B < 50;
        }
    }
}
