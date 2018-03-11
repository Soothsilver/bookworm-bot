using Bookworm.Act;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookworm.Scan
{
    public class Scanning
    {
        public DateTime LastScanTimestamp
        {
            get
            {
                if (LastSnapshot == null) return DateTime.MinValue;
                else
                {
                    return LastSnapshot.Timestamp;
                }
            }
        }
        public Snapshot LastSnapshot = null;
        public Bot Bot;
        public ScreenCapturer capturer = new ScreenCapturer();
        public volatile bool SnapshotInProgress = false;
        public Scanning(Bot bot)
        {
            Bot = bot;
        }

        public void PerformCapture()
        {
            if (SnapshotInProgress)
            {
                return;
            }
            SnapshotInProgress = true;
            Task.Run(() =>
            {
                Rectangle keyboardRectangle = Positions.LetterQuest.Keyboard;
                Image fullCapture = capturer.CaptureScreen();
                Bitmap fullBitmap = new Bitmap(fullCapture);
                Bitmap keyboardBitmap = new Bitmap(keyboardRectangle.Width, keyboardRectangle.Height);
                Graphics g = Graphics.FromImage(keyboardBitmap);
                g.DrawImage(fullCapture, 0, 0, keyboardRectangle, GraphicsUnit.Pixel);
                g.Dispose();
                SnapshotKeyboard keyboard = ParseKeyboardBitmap(fullBitmap);
                Snapshot snapshot = new Snapshot(fullBitmap, keyboardBitmap, keyboard);
                Bot.Recognizator.StartRecognizing(snapshot);
                LastSnapshot = snapshot;
                SnapshotInProgress = false;
            });
           
            /*
            Graphics g2 = Graphics.FromImage(attackBitmap);
            g2.DrawImage(fullCapture, 0, 0, ButtonPositions.Bookworm.rectAttack, GraphicsUnit.Pixel);
            g2.Dispose();*/
            /*
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
               this.lblGridLuminosity.Text = GridLuminosity.ToString();*/

        }

        private SnapshotKeyboard ParseKeyboardBitmap(Bitmap fullScreenBitmap)
        {
            SnapshotKeyboard list = new SnapshotKeyboard();
            for (int i = 0; i < Constants.LETTERQUEST_KEYBOARD_LETTER_COUNT; i++)
            {
                Rectangle letterPosition = Positions.LetterQuest.Letters[i];
                Rectangle importantSection = Positions.LetterQuest.ImportantLetterSection;
                Rectangle importantLetterPart = new Rectangle(letterPosition.X + importantSection.X, letterPosition.Y + importantSection.Y, importantSection.Width, importantSection.Height);
                Color[,] imgData = GetColorDataFromBitmap(fullScreenBitmap, importantLetterPart, 10, 7);
                Bitmap thisLetterBitmap = new Bitmap(letterPosition.Width, letterPosition.Height);
                Graphics g = Graphics.FromImage(thisLetterBitmap);
                g.DrawImage(fullScreenBitmap, letterPosition);
                g.Dispose();
                list.Add(new SnapshotLetter(imgData, thisLetterBitmap));
            }
            return list;
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
                    if (r < 150 && g < 150 && b < 150)
                    {
                        r = g = b = 0;
                    }
                    else
                    {
                        r = g = b = 255;
                    }
                    grid[x, y] = Color.FromArgb(r, g, b);
                }
            }

            return grid;
        }

    }

}
