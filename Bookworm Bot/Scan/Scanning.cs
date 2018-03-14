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
                Bitmap fourButtonsNext = ExtractBitmap(fullBitmap, Positions.LetterQuest.FourButtonsNextButton);
                Bitmap threeButtonsNext = ExtractBitmap(fullBitmap, Positions.LetterQuest.ThreeButtonsNextButton);
                Bitmap row = ExtractBitmap(fullBitmap, Positions.LetterQuest.TreasureChestRow);
                snapshot.SetScreenpart(Screenpart.FOUR_BUTTON_NEXT, fourButtonsNext, Bot.PresageAndRecognize.SimplifyOtherBitmap(fullBitmap, Positions.LetterQuest.FourButtonsNextButton));
                snapshot.SetScreenpart(Screenpart.THREE_BUTTON_NEXT, threeButtonsNext, Bot.PresageAndRecognize.SimplifyOtherBitmap(fullBitmap, Positions.LetterQuest.ThreeButtonsNextButton));
                snapshot.SetScreenpart(Screenpart.TREASURE_CHEST_TOP_ROW, row, Bot.PresageAndRecognize.SimplifyOtherBitmap(fullBitmap, Positions.LetterQuest.TreasureChestRow));
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

        private Bitmap ExtractBitmap(Bitmap fullBitmap, Rectangle partRectangle)
        {
            Bitmap partialBitmap = new Bitmap(partRectangle.Width, partRectangle.Height);
            Graphics g = Graphics.FromImage(partialBitmap);
            g.DrawImage(fullBitmap, 0, 0, partRectangle, GraphicsUnit.Pixel);
            g.Dispose();
            return partialBitmap;
        }

        private SnapshotKeyboard ParseKeyboardBitmap(Bitmap fullScreenBitmap)
        {
            SnapshotKeyboard list = new SnapshotKeyboard();
            for (int i = 0; i < Constants.LETTERQUEST_KEYBOARD_LETTER_COUNT; i++)
            {
                Rectangle letterPosition = Positions.LetterQuest.Letters[i];
                Color[,] imgData = Bot.PresageAndRecognize.SimplifyLetter(fullScreenBitmap, letterPosition);
                Bitmap thisLetterBitmap = new Bitmap(letterPosition.Width, letterPosition.Height);
                Graphics g = Graphics.FromImage(thisLetterBitmap);
                g.DrawImage(fullScreenBitmap,0, 0, letterPosition, GraphicsUnit.Pixel);
                g.Dispose();
                list.Add(new SnapshotLetter(imgData, thisLetterBitmap));
            }
            return list;
        }


    }

}
