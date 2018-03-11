using Bookworm.Act;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

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
        public Scanning(Bot bot)
        {
            Bot = bot;
        }

        public void PerformCapture()
        {
            Rectangle keyboardRectangle = Positions.LetterQuest.Keyboard;

            Image fullCapture = capturer.CaptureScreen();
            Bitmap fullBitmap = new Bitmap(fullCapture);
            Bitmap keyboardBitmap = new Bitmap(keyboardRectangle.Width, keyboardRectangle.Height);
            Graphics g = Graphics.FromImage(keyboardBitmap);
            g.DrawImage(fullCapture, 0, 0, keyboardRectangle, GraphicsUnit.Pixel);
            g.Dispose();
            Bitmap attackBitmap = new Bitmap(191, 191);
            LastSnapshot = new Snapshot(fullBitmap, keyboardBitmap);
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
    }
}
