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
using ManagedWinapi;

namespace BookwormBot
{
    public partial class BookwormForm : Form
    {
        List<Hotkey> Hotkeys = new List<Hotkey>();
        public void LoadHotkeys()
        {
            AddHotkey(Keys.Q, switchAutonomity, ctrl: true);
            AddHotkey(Keys.W, scanGrid, ctrl: true);
            AddHotkey(Keys.E, scanGridAndPutAllInDB, ctrl: true);
       //     AddHotkey(Keys.R, learnAllInThisGrid, ctrl: true);
            AddHotkey(Keys.T, toggleHeavyStuff, ctrl: true);
            AddHotkey(Keys.N, findBestWord, ctrl: true);
            AddHotkey(Keys.M, findNextBestWord, ctrl: true);
            AddHotkey(Keys.Up, captureScreenPartUp, ctrl: true);
            AddHotkey(Keys.Down, captureScreenPartDown, ctrl: true );
            AddHotkey(Keys.Back, clearThisCaptureScreen, ctrl: true, shift: true);
            AddHotkey(Keys.A, setCapturingAttack, ctrl: true, shift: true);
            AddHotkey(Keys.S, setCapturingScramble, ctrl: true, shift: true);
            AddHotkey(Keys.L, setCapturingLifePotion, ctrl: true, shift: true);
            AddHotkey(Keys.P, setCapturingPowerPotion, ctrl: true, shift: true);
            AddHotkey(Keys.U, setCapturingPurifyPotion, ctrl: true, shift: true);
            AddHotkey(Keys.NumPad9, clearAllHearts, ctrl: true);

            AddHotkey(Keys.NumPad1, captureHeart0, ctrl: true);
            AddHotkey(Keys.NumPad2, captureHeart1of4, ctrl: true);
            AddHotkey(Keys.NumPad3, captureHeart2of4, ctrl: true);
            AddHotkey(Keys.NumPad4, captureHeart3of4, ctrl: true);
            AddHotkey(Keys.NumPad5, captureHeartFull, ctrl: true);

            AddHotkey(Keys.NumPad7, captureHeartBack, ctrl: true);
            AddHotkey(Keys.NumPad8, captureHeartNext, ctrl: true);

            AddHotkey(Keys.A, setBonusAnimals, ctrl: true, alt: true);
            AddHotkey(Keys.S, setBonusFruit, ctrl: true, alt: true);
            AddHotkey(Keys.D, setBonusBone, ctrl: true, alt: true);
            AddHotkey(Keys.F, setBonusColors, ctrl: true, alt: true);
            AddHotkey(Keys.G, setBonusMetals, ctrl: true, alt: true);
            AddHotkey(Keys.H, setBonusFire, ctrl: true, alt: true);
            AddHotkey(Keys.J, setBonusBigcats, ctrl: true, alt: true);
            AddHotkey(Keys.K, clearAllBonusCategories, ctrl: true, alt: true);

            AddHotkey(Keys.X, removeWordFromList, ctrl: true, shift: true);
        }
        public void captureHeartBack(object sender, EventArgs e)
        {
            capturingHeartX--;
        }
        public void captureHeartNext(object sender, EventArgs e)
        {
            capturingHeartX++;
        }
        public void removeWordFromList(object sender, EventArgs e)
        {
            if (BestWord != null)
            {
                if (Vocabulary.Words.Contains(BestWord))
                {
                    Vocabulary.Words.Remove(BestWord);
                    RemovalVocabulary.Add(BestWord.String);
                }
            }
            SerializeRemovalVocabulary();
        }
        public void setBonusAnimals(object sender, EventArgs e)
        {
            if (!CurrentBonusCategories.Contains(BonusCategory.Animal))
                CurrentBonusCategories.Add(BonusCategory.Animal);
            LoadBonusWords();
            Vocabulary.ResortByBonusCategory(CurrentBonusCategories);
        }
        public void setBonusFruit(object sender, EventArgs e)
        {
            if (!CurrentBonusCategories.Contains(BonusCategory.FruitAndVegetable))
                CurrentBonusCategories.Add(BonusCategory.FruitAndVegetable);
            LoadBonusWords();
            Vocabulary.ResortByBonusCategory(CurrentBonusCategories);
        }
        public void setBonusBone(object sender, EventArgs e)
        {
            if (!CurrentBonusCategories.Contains(BonusCategory.Bone))
                CurrentBonusCategories.Add(BonusCategory.Bone);
            LoadBonusWords();
            Vocabulary.ResortByBonusCategory(CurrentBonusCategories);
        }
        public void setBonusColors(object sender, EventArgs e)
        {
            if (!CurrentBonusCategories.Contains(BonusCategory.Color))
                CurrentBonusCategories.Add(BonusCategory.Color);
            LoadBonusWords();
            Vocabulary.ResortByBonusCategory(CurrentBonusCategories);
        }
        public void setBonusMetals(object sender, EventArgs e)
        {
            if (!CurrentBonusCategories.Contains(BonusCategory.Metal))
                CurrentBonusCategories.Add(BonusCategory.Metal);
            LoadBonusWords();
            Vocabulary.ResortByBonusCategory(CurrentBonusCategories);
        }
        public void setBonusFire(object sender, EventArgs e)
        {
            if (!CurrentBonusCategories.Contains(BonusCategory.Fire))
                CurrentBonusCategories.Add(BonusCategory.Fire);
            LoadBonusWords();
            Vocabulary.ResortByBonusCategory(CurrentBonusCategories);
        }   
        public void setBonusBigcats(object sender, EventArgs e)
        {
            if (!CurrentBonusCategories.Contains(BonusCategory.BigCat))
                CurrentBonusCategories.Add(BonusCategory.BigCat);
            LoadBonusWords();
            Vocabulary.ResortByBonusCategory(CurrentBonusCategories);
        }


        public void clearAllBonusCategories(object sender, EventArgs e)
        {
            CurrentBonusCategories.Clear();

            LoadBonusWords();
            Vocabulary.SortByPower();
            Vocabulary.ClearToStart();
        }
        public void toggleHeavyStuff(object sender, EventArgs e)
        {
            DoHeavyStuff = !DoHeavyStuff;
        }
        public void captureHeart0(object sender, EventArgs e)
        {
            CaptureHeart(Screenpart.HeartNone);
        }
        public void captureHeart1of4(object sender, EventArgs e)
        {
            CaptureHeart(Screenpart.HeartOneQuarter);
        }
        public void captureHeart2of4(object sender, EventArgs e)
        {
            CaptureHeart(Screenpart.HeartHalf);
        }
        public void captureHeart3of4(object sender, EventArgs e)
        {
            CaptureHeart(Screenpart.HeartThreeQuarters);
        }
        public void captureHeartFull(object sender, EventArgs e)
        {
            CaptureHeart(Screenpart.HeartFull);
        }
        public void clearAllHearts(object sender, EventArgs e)
        {
            ScreenPartPictures[Screenpart.HeartNone].Clear();
            ScreenPartPictures[Screenpart.HeartOneQuarter].Clear();
            ScreenPartPictures[Screenpart.HeartHalf].Clear();
            ScreenPartPictures[Screenpart.HeartThreeQuarters].Clear();
            ScreenPartPictures[Screenpart.HeartFull].Clear();
            SerializeScreenparts();
        }
        public void clearThisCaptureScreen(object sender, EventArgs e)
        {
            ScreenPartPictures[GettingImagesForScreenpartDown].Clear();
            ScreenPartPictures[GettingImagesForScreenpartUp].Clear();
            SerializeScreenparts();
        }
        int capturingHeartX = 3;
        private void CaptureHeart(Screenpart screenpart)
        {
            Rectangle rectHeart3 = new Rectangle(233, 54, 40, 32);
            Rectangle rectHeartX = new Rectangle(233 - 40 * 2 + 40 * (capturingHeartX - 1), 54, 40, 32);
            AnalyzedImage aimage = new AnalyzedImage();
            Bitmap LastCaptureBitmap = new Bitmap(LastCapture);
            aimage.ColorData = GetColorDataFromBitmap(LastCaptureBitmap, rectHeartX,
                       10, out aimage.ColorDataWidth, out aimage.ColorDataHeight);
            ScreenPartPictures[screenpart].Add(aimage);
            SerializeScreenparts();
        }
        public void setCapturingAttack(object sender, EventArgs e)
        {
            GettingImagesForScreenpartUp = Screenpart.AttackAvailable;
            GettingImagesForScreenpartDown = Screenpart.AttackNone;
        }
        public void setCapturingScramble(object sender, EventArgs e)
        {
            GettingImagesForScreenpartUp = Screenpart.ScrambleAvailable;
            GettingImagesForScreenpartDown = Screenpart.ScrambleNone;
        }
        public void setCapturingLifePotion(object sender, EventArgs e)
        {
            GettingImagesForScreenpartUp = Screenpart.HealthPotionAvailable;
            GettingImagesForScreenpartDown = Screenpart.HealthPotionNone;
        }
        public void setCapturingPowerPotion(object sender, EventArgs e)
        {
            GettingImagesForScreenpartUp = Screenpart.PowerupPotionAvailable;
            GettingImagesForScreenpartDown = Screenpart.PowerupPotionNone;
        }
        public void setCapturingPurifyPotion(object sender, EventArgs e)
        {
            GettingImagesForScreenpartUp = Screenpart.PurifyPotionAvailable;
            GettingImagesForScreenpartDown = Screenpart.PurifyPotionNone;
        }
        public void captureScreenPartUp(object sender, EventArgs e)
        {
            CaptureScreenpart(GettingImagesForScreenpartUp);
        }

        Rectangle rectAttack = new Rectangle(683, 958, 317, 71);
        Rectangle rectScramble = new Rectangle(282, 1000, 266, 37);
        Rectangle rectLifePotion = new Rectangle(226, 532, 66, 90);
        Rectangle rectPowerupPotion = new Rectangle(357, 532, 66, 90);
        Rectangle rectPurifyPotion = new Rectangle(490, 532, 66, 90);

        private void CaptureScreenpart(Screenpart screenpart)
        {
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
            SerializeScreenparts();
        }
        public void captureScreenPartDown(object sender, EventArgs e)
        {
            CaptureScreenpart(GettingImagesForScreenpartDown);
        }

        public void findBestWord(object sender, EventArgs e)
        {
            FormBestWord();
        }
        public void findNextBestWord(object sender, EventArgs e)
        {
            FormNextBestWord();
        }
        public void learnAllInThisGrid(object sender, EventArgs e)
        {
            foreach (Letter l in LastRecognizedKeyboard)
            {
                if (!l.Known) l.Known = true;
            }
            SerializeLetterDB();
        }
        public void switchAutonomity(object sender, EventArgs e)
        {
            Autonomous = !Autonomous;
        }
        public void scanGrid(object sender, EventArgs e)
        {
            LastRecognizedKeyboard = FormInstance.Keyboard;
            RecognizeLetters(FormInstance.Keyboard, false);
        }
        public void scanGridAndPutAllInDB(object sender, EventArgs e)
        {
            LastRecognizedKeyboard = FormInstance.Keyboard;
            RecognizeLetters(FormInstance.Keyboard, true);
        }

        public void AddHotkey(Keys code, Action<object, EventArgs> evHandler, bool ctrl = false, bool shift = false, bool alt = false)
        {
            Hotkey h = new Hotkey();
            h.KeyCode = code;
            h.Ctrl = ctrl;
            h.Shift = shift;
            h.Alt = alt;
            h.HotkeyPressed += new EventHandler(evHandler);
            h.Enabled = true;
            Hotkeys.Add(h);
        }

    }
}
