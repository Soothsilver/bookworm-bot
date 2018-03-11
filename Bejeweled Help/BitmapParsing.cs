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
        public Dictionary<Screenpart, List<AnalyzedImage>> ScreenPartPictures = new Dictionary<Screenpart, List<AnalyzedImage>>();
        private void PerformCapture()
        {

            ScreenCapture cp = new ScreenCapture();
            LastCapture = cp.CaptureScreen();
            fullBitmap = new Bitmap(LastCapture);
            keyboardBitmap = new Bitmap(525, 331);
            Graphics g = Graphics.FromImage(keyboardBitmap);
            g.DrawImage(LastCapture, 0, 0, new Rectangle(577, 637, 525, 331), GraphicsUnit.Pixel);
            g.Dispose();
            Bitmap attackBitmap = new Bitmap(191, 191);
            Graphics g2 = Graphics.FromImage(attackBitmap);
            g2.DrawImage(LastCapture, 0, 0, rectAttackButton, GraphicsUnit.Pixel);
            g2.Dispose();
            if (DoAnalyzeMode)
            {
                Keyboard = ParseKeyboardBitmap(fullBitmap);
                AnalyzeScreenparts();
                AnalyzeLife();
                this.lblHP.Text = CurrentLifeTotal.ToString() + (CurrentLifeTotal == 10 ? "+" : "") + " HP";
                this.lblPossibilities.Text =
                    "Attack: " + (AttackIsPossible ? "YES" : "NO") + Environment.NewLine;// +
                 //   "Scramble: " + (ScrambleIsPossible ? "YES" : "NO") + Environment.NewLine +
                 //   "Life: " + (LifePotionIsPossible ? "YES" : "NO") + Environment.NewLine +
                 //   "Power-up: " + (PowerupPotionIsPossible ? "YES" : "NO") + Environment.NewLine +
                 //   "Purify: " + (PurifyPotionIsPossible ? "YES" : "NO") + Environment.NewLine;
                // ParseAttackButton(attackBitmap);
                // Grid Luminosity
                AnalyzedImage ai = new AnalyzedImage();
                ai.ColorData = GetColorDataFromBitmap(keyboardBitmap, new Rectangle(0,0,keyboardBitmap.Width,keyboardBitmap.Height),
                    PRECISION, out ai.ColorDataWidth, out ai.ColorDataHeight);
                GridLuminosity = GetLuminosityIndex(ai);
               this.lblGridLuminosity.Text = GridLuminosity.ToString();
            }
        }
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
                    Rectangle rectThisLetter = new Rectangle(rectFullLetter.X + CUT_EDGE_ROWS, rectFullLetter.Y + CUT_EDGE_ROWS, rectFullLetter.Width - CUT_EDGE_ROWS * 2, rectFullLetter.Height - CUT_EDGE_ROWS * 2);
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
        private void AnalyzeLife()
        {
            Bitmap fullscreen = new Bitmap(LastCapture);
            string confidences = "";
            float totalHP = 0;
            for (int i = 0; i < 10;i++)
            {
                Rectangle rectHeart = new Rectangle(153 + 40 * i, 54, 40, 32);
                AnalyzedImage heartai = new AnalyzedImage();
                heartai.ColorData = GetColorDataFromBitmap(fullscreen, rectHeart, 10, out heartai.ColorDataWidth, out heartai.ColorDataHeight);
              
                int confidence = Int32.MaxValue;
                float thisHeartTotal = 0;
                foreach (AnalyzedImage ai in ScreenPartPictures[Screenpart.HeartFull])
                {
                    int similarity = GetSimilarityIndex(ai, heartai);
                    if (similarity < confidence) { confidence = similarity; thisHeartTotal = 1; }
                } 
                foreach (AnalyzedImage ai in ScreenPartPictures[Screenpart.HeartThreeQuarters])
                {
                    int similarity = GetSimilarityIndex(ai, heartai);
                    if (similarity < confidence) { confidence = similarity; thisHeartTotal = 0.75f; }
                } 
                foreach (AnalyzedImage ai in ScreenPartPictures[Screenpart.HeartHalf])
                {
                    int similarity = GetSimilarityIndex(ai, heartai);
                    if (similarity < confidence) { confidence = similarity; thisHeartTotal = 0.5f; }
                } 
                foreach (AnalyzedImage ai in ScreenPartPictures[Screenpart.HeartOneQuarter])
                {
                    int similarity = GetSimilarityIndex(ai, heartai);
                    if (similarity < confidence) { confidence = similarity; thisHeartTotal = 0.25f; }
                } 
                foreach (AnalyzedImage ai in ScreenPartPictures[Screenpart.HeartNone])
                {
                    int similarity = GetSimilarityIndex(ai, heartai);
                    if (similarity < confidence) { confidence = similarity; thisHeartTotal = 0; }
                }
                confidences += confidence + " :  ";
                totalHP += thisHeartTotal;
             //   if (thisHeartTotal != 1) break;
            }
            CurrentLifeTotal = totalHP;
            this.Text = confidences;
        }
        private void AnalyzeScreenparts()
        {
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
    public class ScreenCapture
    {
        /// <summary>
        /// Creates an Image object containing a screen shot of the entire desktop
        /// </summary>
        /// <returns></returns>
        public Image CaptureScreen()
        {
            return CaptureWindow(User32.GetDesktopWindow());
        }
        /// <summary>
        /// Creates an Image object containing a screen shot of a specific window
        /// </summary>
        /// <param name="handle">The handle to the window. (In windows forms, this is obtained by the Handle property)</param>
        /// <returns></returns>
        public Image CaptureWindow(IntPtr handle)
        {
            // get te hDC of the target window
            IntPtr hdcSrc = User32.GetWindowDC(handle);
            // get the size
            User32.RECT windowRect = new User32.RECT();
            User32.GetWindowRect(handle, ref windowRect);
            int width = windowRect.right - windowRect.left;
            int height = windowRect.bottom - windowRect.top;
            // create a device context we can copy to
            IntPtr hdcDest = GDI32.CreateCompatibleDC(hdcSrc);
            // create a bitmap we can copy it to,
            // using GetDeviceCaps to get the width/height
            IntPtr hBitmap = GDI32.CreateCompatibleBitmap(hdcSrc, width, height);
            // select the bitmap object
            IntPtr hOld = GDI32.SelectObject(hdcDest, hBitmap);
            // bitblt over
            GDI32.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, 0, 0, GDI32.SRCCOPY);
            // restore selection
            GDI32.SelectObject(hdcDest, hOld);
            // clean up
            GDI32.DeleteDC(hdcDest);
            User32.ReleaseDC(handle, hdcSrc);
            // get a .NET image object for it
            Image img = Image.FromHbitmap(hBitmap);
            // free up the Bitmap object
            GDI32.DeleteObject(hBitmap);
            return img;
        }
        /// <summary>
        /// Captures a screen shot of a specific window, and saves it to a file
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="filename"></param>
        /// <param name="format"></param>
        public void CaptureWindowToFile(IntPtr handle, string filename, ImageFormat format)
        {
            Image img = CaptureWindow(handle);
            img.Save(filename, format);
        }
        /// <summary>
        /// Captures a screen shot of the entire desktop, and saves it to a file
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="format"></param>
        public void CaptureScreenToFile(string filename, ImageFormat format)
        {
            Image img = CaptureScreen();
            img.Save(filename, format);
        }

        /// <summary>
        /// Helper class containing Gdi32 API functions
        /// </summary>
        public class GDI32
        {

            public const int SRCCOPY = 0x00CC0020; // BitBlt dwRop parameter
            [DllImport("gdi32.dll")]
            public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest,
                int nWidth, int nHeight, IntPtr hObjectSource,
                int nXSrc, int nYSrc, int dwRop);
            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth,
                int nHeight);
            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleDC(IntPtr hDC);
            [DllImport("gdi32.dll")]
            public static extern bool DeleteDC(IntPtr hDC);
            [DllImport("gdi32.dll")]
            public static extern bool DeleteObject(IntPtr hObject);
            [DllImport("gdi32.dll")]
            public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
        }

        /// <summary>
        /// Helper class containing User32 API functions
        /// </summary>
        public class User32
        {
            [StructLayout(LayoutKind.Sequential)]
            public struct RECT
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
            }
            [DllImport("user32.dll")]
            public static extern IntPtr GetDesktopWindow();
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowDC(IntPtr hWnd);
            [DllImport("user32.dll")]
            public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);
        }
    }
    public enum Screenpart
    {
        Null,
        HealthPotionAvailable,
        HealthPotionNone,
        PowerupPotionAvailable,
        PowerupPotionNone,
        PurifyPotionAvailable,
        PurifyPotionNone,
        ScrambleAvailable,
        ScrambleNone,
        AttackAvailable,
        AttackNone,
        HeartFull,
        HeartThreeQuarters,
        HeartHalf,
        HeartOneQuarter,
        HeartNone
    } 
}
