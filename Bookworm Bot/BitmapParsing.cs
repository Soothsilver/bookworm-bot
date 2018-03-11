using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Bookworm;
using Bookworm.Act;
using Bookworm.Scan;

namespace BookwormBot
{
    public partial class BookwormForm : Form
    {
        public bool AttackIsPossible { get; set; }
        public bool ScrambleIsPossible { get; set; }
        public bool LifePotionIsPossible { get; set; }
        public bool PowerupPotionIsPossible { get; set; }
        public bool PurifyPotionIsPossible { get; set; }
        public float CurrentLifeTotal { get; set; }

        public int GridLuminosity = 0;
        public const string SCREENPARTS_FILEPATH = "screenparts.dat";
        public Screenpart GettingImagesForScreenpartUp;
        public Screenpart GettingImagesForScreenpartDown;
        public Image LastCapture;
        public Bitmap fullBitmap;
        public Bitmap keyboardBitmap;
        public Dictionary<Screenpart, List<AnalyzedImage>> ScreenPartPictures = new Dictionary<Screenpart, List<AnalyzedImage>>();
     
        private List<Letter> ParseKeyboardBitmap(Bitmap fullscreen)
        {
            List<Letter> list = new List<Letter>();
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    int i = y * 4 + x;
                    Letter l = new Letter();
                    Rectangle rectFullLetter = TilePositions[i];
                    int CUT_EDGE_ROWS = 0;
                    Rectangle rectThisLetter = new Rectangle(rectFullLetter.X + CUT_EDGE_ROWS, rectFullLetter.Y + CUT_EDGE_ROWS, rectFullLetter.Width - CUT_EDGE_ROWS * 2, rectFullLetter.Height - CUT_EDGE_ROWS * 2);
                    int PRECISION = 0;
                    Color[,] imgData = GetColorDataFromBitmap(fullscreen, rectThisLetter,
                        PRECISION, out l.ColorDataWidth, out l.ColorDataHeight);
                    l.ColorData = imgData;
                    Bitmap thisLetterBitmap = new Bitmap(rectFullLetter.Width, rectFullLetter.Height);
                    Graphics g = Graphics.FromImage(thisLetterBitmap);
                    g.DrawImage(fullscreen, 0, 0, new Rectangle(rectFullLetter.X, rectFullLetter.Y, rectFullLetter.Width, rectFullLetter.Height), GraphicsUnit.Pixel);
                    g.Dispose();
                    l.FullBitmap = thisLetterBitmap;
                    l.PositionInGrid = i;
                    list.Add(l);
                }

            }
            return list;
        }
  
        private void AnalyzeScreenparts()
        {
            /*
            Bitmap captureBitmap = new Bitmap(LastCapture);
            List<AnalyzedImage> Positives = null;
            List<AnalyzedImage> Negatives = null;
            Rectangle ThisRectangle = Rectangle.Empty;
            for (int i = 0; i < 5; i++)
            {
                switch (i)
                {
                    case 0:
                        ThisRectangle = rectAttack;
                        Positives = ScreenPartPictures[Screenpart.AttackAvailable];
                        Negatives = ScreenPartPictures[Screenpart.AttackNone];
                        break;
                    case 1:
                        ThisRectangle = rectScramble;
                        Positives = ScreenPartPictures[Screenpart.ScrambleAvailable];
                        Negatives = ScreenPartPictures[Screenpart.ScrambleNone];
                        break;
                    case 2:
                        ThisRectangle = rectLifePotion;
                        Positives = ScreenPartPictures[Screenpart.HealthPotionAvailable];
                        Negatives = ScreenPartPictures[Screenpart.HealthPotionNone];
                        break;
                    case 3:
                        ThisRectangle = rectPowerupPotion;
                        Positives = ScreenPartPictures[Screenpart.PowerupPotionAvailable];
                        Negatives = ScreenPartPictures[Screenpart.PowerupPotionNone];
                        break;
                    case 4:
                        ThisRectangle = rectPurifyPotion;
                        Positives = ScreenPartPictures[Screenpart.PurifyPotionAvailable];
                        Negatives = ScreenPartPictures[Screenpart.PurifyPotionNone];
                        break;
                }
                AnalyzedImage l = new AnalyzedImage();
                Color[,] imgData = GetColorDataFromBitmap(captureBitmap, ThisRectangle, 10, out l.ColorDataWidth, out l.ColorDataHeight);
                l.ColorData = imgData;
                CurrentAttackButton = l;
                int bestConfidenceWithPossible = Int32.MaxValue;
                int bestConfidenceWithImpossible = Int32.MaxValue;
                foreach (AnalyzedImage ai in Positives)
                {
                    int simin = GetSimilarityIndexRGB(l, ai);
                    if (simin < bestConfidenceWithPossible) bestConfidenceWithPossible = simin;
                }
                foreach (AnalyzedImage ai in Negatives)
                {
                    int simin = GetSimilarityIndexRGB(l, ai);
                    if (simin < bestConfidenceWithImpossible) bestConfidenceWithImpossible = simin;
                }
                bool possibility = bestConfidenceWithPossible < bestConfidenceWithImpossible;
                switch (i)
                {
                    case 0: AttackIsPossible = possibility; break;
                    case 1: ScrambleIsPossible = possibility; break;
                    case 2: LifePotionIsPossible = possibility; break;
                    case 3: PowerupPotionIsPossible = possibility; break;
                    case 4: PurifyPotionIsPossible = possibility; break;
                }
            }
            */
        }
        /*
        private void ParseAttackButton(Bitmap attackBitmap)
        {
            AnalyzedImage l = new AnalyzedImage();
            Color[,] imgData = GetColorDataFromBitmap(attackBitmap, new Rectangle(0, 0, attackBitmap.Width, attackBitmap.Height),
                       10, out l.ColorDataWidth, out l.ColorDataHeight);
            l.ColorData = imgData;
            CurrentAttackButton = l;
            int bestConfidenceWithPossible = Int32.MaxValue;
            int bestConfidenceWithImpossible = Int32.MaxValue;
            foreach (AnalyzedImage ai in AttackPossibleButtons)
            {
                int simin = GetSimilarityIndexRGB(l, ai);
                if (simin < bestConfidenceWithPossible) bestConfidenceWithPossible = simin;
            }
            foreach (AnalyzedImage ai in AttackImpossibleButtons)
            {
                int simin = GetSimilarityIndexRGB(l, ai);
                if (simin < bestConfidenceWithImpossible) bestConfidenceWithImpossible = simin;
            }
            AttackIsPossible = bestConfidenceWithPossible < bestConfidenceWithImpossible;
            this.lblPossibleAttack.Text
                = AttackIsPossible ? "Attack is possible." : "Attack is NOT possible.";
            this.lblPossibleAttack.ForeColor = AttackIsPossible ? Color.Green : Color.Red;
        }
        */
        private Color[,] GetColorDataFromBitmap(Bitmap keyboardBitmap, Rectangle rectangle, int PRECISION, out int w, out int h)
        {
            int gwidth = (int)Math.Ceiling((float)rectangle.Width / (float)PRECISION);
            int gheight = (int)Math.Ceiling((float)rectangle.Height / (float)PRECISION);
            w = gwidth;
            h = gheight;
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
                    for (int i = 0; i < PRECISION; i++)
                    {
                        int thisx = i + x * PRECISION + rectangle.X;
                        if (thisx >= rectangle.Right) break;
                        for (int j = 0; j < PRECISION; j++)
                        {
                            // One pixel
                            int thisy = j + y * PRECISION + rectangle.Y;
                            if (thisy >= rectangle.Bottom) break;
                            Color c = keyboardBitmap.GetPixel(thisx, thisy);
                            r += c.R;
                            g += c.G;
                            b += c.B;
                            pixels++;
                        }
                    }
                    r /= pixels;
                    g /= pixels;
                    b /= pixels;
                    grid[x, y] = Color.FromArgb(r, g, b);
                }
            }

            return grid;
        }

    

  
    }
    class BitmapParsing
    {
    }
 
}
