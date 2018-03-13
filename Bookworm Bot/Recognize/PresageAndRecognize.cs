using Bookworm.Act;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookworm.Recognize
{
    public class PresageAndRecognize
    {

        public Color[,] SimplifyLetter(Bitmap bitmap, Rectangle whereIsLetterInBitmap)
        {
            Rectangle importantSection = Positions.LetterQuest.ImportantLetterSection;
            Rectangle importantLetterPart = new Rectangle(whereIsLetterInBitmap.X + importantSection.X, whereIsLetterInBitmap.Y + importantSection.Y, importantSection.Width, importantSection.Height);
            Color[,] imgData = GetColorDataFromBitmap(bitmap, importantLetterPart, 5, 5);
            return imgData;
        }

        /// <summary>
        /// Gets the color data for a part of a bitmap. Data from adjacent pixels is collapsed into a single element. The WIDTH COLLAPSE parameter determines how many horizontal
        /// pixels are made into one. The HEIGHT COLLAPSE does the same for vertical. RECTANGLE is the part of of the bitmap from which data is got.
        /// </summary>
        private Color[,] GetColorDataFromBitmap(Bitmap bitmap, Rectangle rectangle, int widthCollapse, int heightCollapse)
        {
            int gwidth = rectangle.Width / widthCollapse;
            int gheight = rectangle.Height / heightCollapse;
            Color[,] grid = new Color[gwidth, gheight];

            for (int x = 0; x < gwidth; x++)
            {
                for (int y = 0; y < gheight; y++)
                {
                    // One ROI
                    int r = 0;
                    int g = 0;
                    int b = 0;
                    int pixels = 0;
                    for (int i = 0; i < widthCollapse; i++)
                    {
                        int thisx = i + x * widthCollapse + rectangle.X;
                        if (thisx >= rectangle.Right) break;
                        for (int j = 0; j < heightCollapse; j++)
                        {
                            // One pixel
                            int thisy = j + y * heightCollapse + rectangle.Y;
                            if (thisy >= rectangle.Bottom) break;
                            Color c = bitmap.GetPixel(thisx, thisy);
                            r += c.R;
                            g += c.G;
                            b += c.B;
                            pixels++;
                        }
                    }
                    r /= pixels;
                    g /= pixels;
                    b /= pixels;
                    if (r < 120 && g < 120 && b < 120)
                    {
                        r = g = b = 0;
                    }
                    else
                    {
                        r += (255 - r) * 4 / 5;
                        g += (255 - g) * 4 / 5;
                        b += (255 - b) * 4 / 5;
                    }
                    grid[x, y] = Color.FromArgb(r, g, b);
                }
            }

            return grid;
        }
        public int GetDistance(Color[,] simplifiedData, Color[,] colorData)
        {
            int w = simplifiedData.GetLength(0);
            int h = simplifiedData.GetLength(1);
            int distance = 0;
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    distance += Math.Abs((int)simplifiedData[x, y].R - (int)colorData[x, y].R) +
                        Math.Abs((int)simplifiedData[x, y].G - (int)colorData[x, y].G) +
                        Math.Abs((int)simplifiedData[x, y].B - (int)colorData[x, y].B);
                }
            }
            return distance;
        }

        public const int THRESHOLD_OF_DISTANCE_FOR_MATCH = 2000;
    }
}
