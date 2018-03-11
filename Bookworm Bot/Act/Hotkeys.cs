using ManagedWinapi;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bookworm.Act
{
    public class Hotkeys
    {
        List<Hotkey> ListOfHotkeys = new List<Hotkey>();
        Bot Bot;
        BookwormForm Form;
        
        public Hotkeys(Bot bot, BookwormForm form)
        {
            Form = form;
            Bot = bot;
        }

        public void LoadHotkeys()
        {
            AddHotkey(Keys.Left, switchTabLeft, alt: true);
            AddHotkey(Keys.Right, switchTabRight, alt: true);
            AddHotkey(Keys.R, saveUnknownsIntoDatabase, alt: true);
            AddHotkey(Keys.R, saveAllIntoDatabase, alt: true, shift: true);
            /*
            AddHotkey(Keys.Q, switchAutonomity, ctrl: true);
            AddHotkey(Keys.W, scanGrid, ctrl: true);
            AddHotkey(Keys.Down, captureScreenPartDown, ctrl: true);*/
        }

        private void saveAllIntoDatabase()
        {
            if (Bot.Scan.LastSnapshot != null)
            {
                Bot.Database.SaveAllSnapshotLettersIntoDatabase(Bot.Scan.LastSnapshot);
            }
        }

        private void saveUnknownsIntoDatabase()
        {
            if (Bot.Recognizator.LastRecognitionResults != null)
            {
                Bot.Database.SaveUnknownSnapshotLettersIntoDatabase(Bot.Recognizator.LastRecognitionResults);
            }
        }

        void switchTabLeft()
        {
            if (Form.tabControl.SelectedIndex > 0)
            {
                Form.tabControl.SelectedIndex--;
            }
        }
        void switchTabRight()
        {
            if (Form.tabControl.SelectedIndex < Form.tabControl.TabPages.Count - 1)
            {
                Form.tabControl.SelectedIndex++;
            }
        }
        public void switchAutonomity(object sender, EventArgs e)
        {
            Bot.Autonomous.IsAutonomous = !Bot.Autonomous.IsAutonomous;
        }

      //  private void CaptureScreenpart(Screenpart screenpart)
      //  {
            /*
            Rectangle scanWhat = new Rectangle(0, 0, 10, 10);
            switch (screenpart)
            {
                case Screenpart.AttackAvailable: scanWhat = rectAttack; break;
                case Screenpart.AttackNone: scanWhat = rectAttack; break;
                case Screenpart.HealthPotionAvailable: scanWhat = rectLifePotion; break;
                case Screenpart.HealthPotionNone: scanWhat = rectLifePotion; break;
                case Screenpart.PowerupPotionAvailable: scanWhat = rectPowerupPotion; break;
                case Screenpart.PowerupPotionNone: scanWhat = rectPowerupPotion; break;
                case Screenpart.PurifyPotionAvailable: scanWhat = rectPurifyPotion; break;
                case Screenpart.PurifyPotionNone: scanWhat = rectPurifyPotion; break;
                case Screenpart.ScrambleAvailable: scanWhat = rectScramble; break;
                case Screenpart.ScrambleNone: scanWhat = rectScramble; break;

            }
            AnalyzedImage aimage = new AnalyzedImage();
            Bitmap LastCaptureBitmap = new Bitmap(LastCapture);
            aimage.ColorData = GetColorDataFromBitmap(LastCaptureBitmap, scanWhat,
                       10, out aimage.ColorDataWidth, out aimage.ColorDataHeight);
            ScreenPartPictures[screenpart].Add(aimage);
            SerializeScreenparts();*/
  //      }
        public void captureScreenPartDown(object sender, EventArgs e)
        {
            // CaptureScreenpart(GettingImagesForScreenpartDown);
        }

       
        public void scanGrid(object sender, EventArgs e)
        {
            /*
            LastRecognizedKeyboard = FormInstance.Keyboard;
            RecognizeLetters(FormInstance.Keyboard, false);
            */
        }
        public void scanGridAndPutAllInDB(object sender, EventArgs e)
        {
            /*
            LastRecognizedKeyboard = FormInstance.Keyboard;
            RecognizeLetters(FormInstance.Keyboard, true);
            */
        }

        public void AddHotkey(
            Keys code,
            Action hotkeyAction,
            bool ctrl = false, 
            bool shift = false,
            bool alt = false)
        {
            Hotkey h = new Hotkey();
            h.KeyCode = code;
            h.Ctrl = ctrl;
            h.Shift = shift;
            h.Alt = alt;
            h.HotkeyPressed += new EventHandler((obj, e)=>
            {
                hotkeyAction();
            });
            h.Enabled = true;
            ListOfHotkeys.Add(h);
        }
    }
}
