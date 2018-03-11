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

namespace BookwormBot
{
    public partial class BookwormForm : Form
    {
        public List<string> RemovalVocabulary = new List<string>();
        public Word BestWord;
        public int CurrentWord = 0;
        public List<BonusCategory> CurrentBonusCategories = new List<BonusCategory>();

        public void FormBestWord()
        {
            /*
            if (LastRecognizedKeyboard != null)
            {
                CurrentWord = 0;
                Vocabulary.ClearToStart();
                List<char> chars = new List<char>();
                foreach (Letter l in LastRecognizedKeyboard)
                {
                    if (l.Known && !l.IsLocked)
                    {
                        if (l.InnerLetter == ' ') { BestWord = null; return; }
                        chars.Add(l.InnerLetter);
                    }
                }
                Vocabulary.ScanForTheseLetters(chars);
                BestWord = Vocabulary.EarliestUnbroken();
            }*/
        }
        public void FormNextBestWord()
        {
            CurrentWord++;
            BestWord = Vocabulary.EarliestUnbroken(CurrentWord);
        }
        public Vocabulary Vocabulary;
        public void LoadDictionariesFinalTouches()
        {/*
            Vocabulary.SortByPower();
            Vocabulary.ClearToStart();
            LoadRemovalVocabulary();
            foreach (string s in RemovalVocabulary)
            {
                for (int vi = Vocabulary.Words.Count-1; vi >= 0; vi--)
                {
                    if (Vocabulary.Words[vi].String == s)
                    {
                        Vocabulary.Words.RemoveAt(vi);
                    }
                }
            }*/
        }
        public void LoadBonusWords()
        {
            int brokenwords = 0; int addedwords = 0;
            for (int i = Vocabulary.Words.Count - 1; i >= 0; i--)
            {
                bool isbonus = false;
                foreach (KeyValuePair<BonusCategory, bool> kvp in Vocabulary.Words[i].IsBonus)
                {
                    if (kvp.Value == true) { isbonus = true; break; }
                }
                if (isbonus) Vocabulary.Words.RemoveAt(i);
            }
            for (int i = 0; i < 7; i++)
            {
                string wordfile = "";
                BonusCategory bc = BonusCategory.Null;
                switch (i)
                {
                    case 0: wordfile = "animals.txt"; bc = BonusCategory.Animal; break;
                    case 1: wordfile = "fruitsandvegs.txt"; bc = BonusCategory.FruitAndVegetable; break;
                    case 2: wordfile = "bone.txt"; bc = BonusCategory.Bone; break;
                    case 3: wordfile = "color.txt"; bc = BonusCategory.Color; break;
                    case 4: wordfile = "metals.txt"; bc = BonusCategory.Metal; break;
                    case 5: wordfile = "fire.txt"; bc = BonusCategory.Fire; break;
                    case 6: wordfile = "bigcats.txt"; bc = BonusCategory.BigCat; break;
                }
                if (!CurrentBonusCategories.Contains(bc)) continue;
                string[] allwords = System.IO.File.ReadAllLines(".\\bonuswords\\" + wordfile);
                foreach (string s in allwords)
                {
                    if (s.Length < 3) { brokenwords++; continue; }
                    if (s.Length > 16) { brokenwords++; continue; }
                    bool isBroken = false;
                    foreach (Word w in Vocabulary.Words)
                    {
                        if (w.String == s)
                        {
                            isBroken = true;
                            w.IsBonus[bc] = true;
                            brokenwords++;
                            break;
                        }
                    }
                    if (isBroken) continue;
                    string word = s.ToUpper().Replace("QU", "Q");
                    Word w2 = new Word(word);
                    w2.IsBonus[bc] = true;
                    Vocabulary.Words.Add(w2);
                    addedwords++;
                }
            }
            LoadDictionariesFinalTouches();
        }
        public void LoadAndParseDictionary(string wordfile)
        {
            DateTime operationStart = DateTime.Now;
            string[] allwords = System.IO.File.ReadAllLines(".\\dictionary\\" + wordfile);
            Vocabulary = new Vocabulary();
            int brokenwords = 0; int addedwords = 0;
            foreach (string s in allwords)
            {
                if (s.Length < 3) { brokenwords++; continue; }
                if (s.Length > 16) { brokenwords++; continue; }
                bool isBroken = false;
                foreach (char c in s)
                {
                    if (!Char.IsLetter(c)) { brokenwords++; isBroken = true; break; }
                }
                if (isBroken) continue;
                string word = s.ToUpper().Replace("QU", "Q");
                Vocabulary.Words.Add(new Word(word));
                addedwords++;
            }
            LoadDictionariesFinalTouches();
         //   MessageBox.Show("Parsing took " + (DateTime.Now - operationStart).TotalMilliseconds + " ms. I ignored " + brokenwords + " words and have a " + addedwords + "-word vocabulary.");
           
            /*
            MessageBox.Show("Documents: " + Word.GetPowerOfWord("DOCUMENTS") + "; Outcome: " + Word.GetPowerOfWord("OUTCOME"));
            MessageBox.Show("Parsing took " + (DateTime.Now - operationStart).TotalMilliseconds + " ms. I ignored " + brokenwords + " words and have a " + addedwords + "-word vocabulary.");
            operationStart = DateTime.Now;
            Vocabulary.SortByPower();
            MessageBox.Show("Sorting took " + (DateTime.Now - operationStart).TotalMilliseconds + " ms. The first word is " + Vocabulary.Words[0].String + " with power " + Vocabulary.Words[0].Power + ".");
            operationStart = DateTime.Now;
            Vocabulary.ClearToStart();
            MessageBox.Show("Cleaning took: " + (DateTime.Now - operationStart).TotalMilliseconds + " ms.");
            operationStart = DateTime.Now;

            Vocabulary.ScanForTheseLetters(new List<char>(new char[] { 'A', 'U', 'T', 'O', 'N', 'O', 'M', 'O', 'U', 'S', 'B', 'C', 'D', 'E', 'F', 'G' }));
            MessageBox.Show("Scanning took " + (DateTime.Now - operationStart).TotalMilliseconds + " ms.");
            operationStart = DateTime.Now;
            Word w = Vocabulary.EarliestUnbroken();
            MessageBox.Show("Earliest unbroken took: " + (DateTime.Now - operationStart).TotalMilliseconds + " ms. The word is: " + w.String);
        */
        }
    }
    public class Vocabulary
    {
        public List<Word> Words = new List<Word>();
        public void ScanForTheseLetters(List<Letter> list)
        {
            ScanForTheseLetters(list.ConvertAll(new Converter<Letter,char>((l) => {
                return l.InnerLetter;
            })));
        }
        public void ScanForTheseLetters(List<char> chars)
        {
            foreach (char c in chars)
            {
                foreach (Word w in Words)
                {
                    w.TemporaryNumberOfChars[c]--;
                }
            }
            foreach (Word w in Words)
            {
                foreach (KeyValuePair<char, int> kvp in w.TemporaryNumberOfChars)
                {
                    if (kvp.Value > 0)
                    {
                        w.Legal = false;
                        break;
                    }
                }
            }
        }
        public void ResortByBonusCategory(List<BonusCategory> bonusCategories)
        {
            foreach (Word w in Words)
            {
                foreach (BonusCategory bc in bonusCategories)
                {
                    if (w.IsBonus[bc])
                        w.TemporaryPower += Word.BONUS_CATEGORY_POWER_BOOST;
                }
            } 
            Words.Sort(new Comparison<Word>((w, w2) =>
            {
                if (w.TemporaryPower > w2.TemporaryPower) return -1;
                if (w.TemporaryPower < w2.TemporaryPower) return 1;
                return 0;
            }
                 ));
        }
        public void SortByPower()
        {
            Words.Sort(new Comparison<Word>((w, w2) => 
                {

                    if (w.Power > w2.Power) return -1;
                    if (w.Power < w2.Power) return 1;
                    return 0;
                }
                ));
        }
        public void ClearToStart()
        {
            foreach (Word w in Words)
            {
                w.Legal = true;
                w.CloneToTemporary();
            }
        }
        public Word EarliestUnbroken(int skip = 0)
        {
            
            for (int i = 0; i < Words.Count; i++)
            {
                if (Words[i].Legal)
                {
                    if (skip == 0)
                        return Words[i];
                    else
                        skip--;
                }
            }
            return null;
        }
    }
    public class Word
    {
        public const int BONUS_CATEGORY_POWER_BOOST = 15;
        public int TemporaryPower;
        public int Power;
        public string String;
        public Dictionary<char, int> NumberOfChars = new Dictionary<char, int>();
        public Dictionary<char, int> TemporaryNumberOfChars = new Dictionary<char, int>();
        public bool Legal = true;
        public Dictionary<BonusCategory, bool> IsBonus = new Dictionary<BonusCategory, bool>();
        public Word(string s)
        {
            String = s;
            for (char c = 'A'; c <= 'Z'; c++)
            {
                NumberOfChars.Add(c, 0);
            }
            foreach (char c in String)
            {
                NumberOfChars[c]++;
            }
            DeterminePower();
            foreach (BonusCategory bc in Enum.GetValues(typeof(BonusCategory)))
            {
                IsBonus.Add(bc, false);
            }
        }
        private void DeterminePower()
        {
            Power = 0;
            foreach (KeyValuePair<char, int> kvp in NumberOfChars)
            {
                Power += kvp.Value * Word.Powers[kvp.Key];
            }
        }
        public void CloneToTemporary()
        {
            for (char c = 'A'; c <= 'Z'; c++)
            {
                TemporaryNumberOfChars[c] = NumberOfChars[c];
            }
            TemporaryPower = Power;
        }
        public static Dictionary<char, int> Powers = new Dictionary<char, int>();
        public static char[] POWER_4 = new char[] { 'A', 'D', 'E', 'G', 'I', 'L', 'N', 'O', 'R', 'S', 'T', 'U' };
        public static char[] POWER_5 = new char[] { 'B', 'C', 'F', 'H', 'M', 'P' };
        public static char[] POWER_6 = new char[] { 'V', 'W', 'Y' };
        public static char[] POWER_7 = new char[] { 'J', 'K' };
        public static char[] POWER_8 = new char[] { 'X', 'Z' };
        public static char[] POWER_11 = new char[] { 'Q' };
        public static int GetPowerOfWord(string word)
        {
            Word w = new Word(word);
            w.DeterminePower();
            return w.Power;
        }
        static Word()
        {
            foreach (char c in POWER_4)
                Powers.Add(c, 4);
            foreach (char c in POWER_5)
                Powers.Add(c, 5);
            foreach (char c in POWER_6)
                Powers.Add(c, 6);
            foreach (char c in POWER_7)
                Powers.Add(c, 7);
            foreach (char c in POWER_8)
                Powers.Add(c, 8);
            foreach (char c in POWER_11)
                Powers.Add(c, 11);
        }
    }
    public enum BonusCategory
    {
        Null,
        Animal,
        FruitAndVegetable,
        Bone,
        Color,
        Metal,
        Fire,
        BigCat
    }
}
