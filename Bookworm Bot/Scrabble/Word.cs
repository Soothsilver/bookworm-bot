using System;
using System.Collections.Generic;
using System.Linq;

namespace Bookworm.Scrabble
{
    public class Word
    {
        public string Text { get; }
        public int Power { get; }

        public Word(string text)
        {
            this.Text = text;
            this.Power = DeterminePower(text);
        }

        private int DeterminePower(string text)
        {
            int power = 0;
            foreach(char c in text)
            {
                power += GetPowerOfChar(c);
            }
            if (text[text.Length - 1] == 'S')
            {
                power += 4;
            }
            return power;
        }

        static char[] GoldChars = new char[] { 'Z', 'Q', 'K', 'X', 'J'};
        static char[] SilverChars = new char[] { 'Y', 'F', 'P', 'V', 'M', 'W', 'B', 'H', 'C' };
        private int GetPowerOfChar(char c)
        {
            if (GoldChars.Contains(c)) return 3;
            if (SilverChars.Contains(c)) return 2;
            return 1;
        }

        internal bool CreatableFrom(List<char> usableLetters)
        {
            List<char> copy = new List<char>();
            foreach(char c in usableLetters)
            {
                copy.Add(c);
            }
            foreach(char c in this.Text)
            {
                if (!copy.Remove(c))
                {
                    return false;
                }
            }
            return true;
        }
    }
}