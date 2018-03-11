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
        public int CONFIDENCE_THRESHOLD = 2500;
        public int CONFUSING_LETTERS_CONFIDENCE_THRESHOLD = 2500;
        public void LearnAllUnknowns()
        {
            foreach (Letter l in LetterDB)
            {
                if (Keyboard.Contains(l))
                 l.Known = true;
            }
            SerializeLetterDB();
        }
        public void RecognizeLetters(List<Letter> checkingwhat, bool totalMistrust, bool addToLetterDB = true)
        {
            for (int li = checkingwhat.Count() - 1; li >= 0; li--)
            {
                Letter l = checkingwhat[li];
                int similarityWithClosest = Int32.MaxValue;
                Letter closestLetter = null;

                foreach (Letter l2 in LetterDB)
                {
                    if (!l2.Known) continue;
                    int similarity = GetSimilarityIndex(l, l2);
                    if (similarity < similarityWithClosest)
                    {
                        similarityWithClosest = similarity;
                        closestLetter = l2;
                    }
                }

                int confIndex = Int32.MaxValue;
                char letter = ' ';
                if (closestLetter != null)
                {
                    letter = closestLetter.InnerLetter;
                    confIndex = similarityWithClosest;
                    l.IsEmpty = closestLetter.IsEmpty;
                    l.IsSmashed = closestLetter.IsSmashed;
                    l.IsStunned = closestLetter.IsStunned;
                    l.IsLocked = closestLetter.IsLocked;
                    l.IsArtifact = closestLetter.IsArtifact;
                    l.IsPlagued = closestLetter.IsPlagued;

                    l.IsCrystal = closestLetter.IsCrystal;
                    l.IsDiamond = closestLetter.IsDiamond;
                    l.IsEmerald = closestLetter.IsEmerald;
                    l.IsGarnet = closestLetter.IsGarnet;
                    l.IsRuby = closestLetter.IsRuby;
                    l.IsSapphire = closestLetter.IsSapphire;
                    l.IsAmethyst = closestLetter.IsAmethyst;
                    
                }
                l.ConfidenceIndex = confIndex;
                l.InnerLetter = letter;
                l.ClosestFriend = closestLetter;
                if (totalMistrust)
                {
                    if (!LetterDB.Contains(l) && addToLetterDB)
                        LetterDB.Add(l);
                }
                else
                {
                    if (((l.InnerLetter == 'I' || l.InnerLetter == 'T') && l.ConfidenceIndex <= CONFUSING_LETTERS_CONFIDENCE_THRESHOLD)
                        || (l.InnerLetter != 'I' && l.InnerLetter != 'T' && l.ConfidenceIndex <= CONFIDENCE_THRESHOLD))
                    {
                        l.Known = true;
                    }
                    else
                    {

                        if (!LetterDB.Contains(l) && addToLetterDB)
                            LetterDB.Add(l);
                    }
                }
            }
            SerializeLetterDB();
            RefreshTrainingPanel();
        }/*
        private int GetSimilarityIndex(Letter l, Letter l2)
        {
            int similarity = 0;
            for (int i = 0; i < l.ColorDataWidth; i++)
                for (int j = 0; j < l.ColorDataHeight; j++)
                {
                    similarity += (int)Math.Abs(l.ColorData[i, j].GetBrightness() - l2.ColorData[i, j].GetBrightness()) * 1000;
                    similarity += (int)Math.Abs(l.ColorData[i, j].GetSaturation() - l2.ColorData[i, j].GetSaturation()) * 1000;
                    similarity += (int)(Math.Abs(l.ColorData[i, j].GetHue() - l2.ColorData[i, j].GetHue()) / 0.36f);
                }
            return similarity;
        }*/
        private void RecognizeAllUnrecognizedInDB(bool mistrust = true)
        {
            IEnumerable<Letter> enumera = LetterDB.Where((Letter l) => { return l.Known == false; });
            List<Letter> lNew = new List<Letter>(enumera);
            RecognizeLetters(lNew, mistrust);
        }
     
        private int GetSimilarityIndex(AnalyzedImage l, AnalyzedImage l2)
        {
            int similarity = 0;
            for (int i = 0; i < l.ColorDataWidth; i++)
                for (int j = 0; j < l.ColorDataHeight; j++)
                {
                    similarity += (int)(Math.Abs(l.ColorData[i, j].GetBrightness() - l2.ColorData[i, j].GetBrightness()) * 1000);
                    similarity += (int)(Math.Abs(l.ColorData[i, j].GetSaturation() - l2.ColorData[i, j].GetSaturation()) * 1000);
                    similarity += (int)(Math.Abs(l.ColorData[i, j].GetHue() - l2.ColorData[i, j].GetHue()) / 0.36f);
                }
            return similarity;
        }
        private int GetSimilarityIndexRGB(AnalyzedImage l, AnalyzedImage l2)
        {
            int similarity = 0;
            for (int i = 0; i < l.ColorDataWidth; i++)
                for (int j = 0; j < l.ColorDataHeight; j++)
                {
                    similarity += (int)(Math.Abs(l.ColorData[i, j].R - l2.ColorData[i, j].R) * 1000);
                    similarity += (int)(Math.Abs(l.ColorData[i, j].G - l2.ColorData[i, j].G) * 1000);
                    similarity += (int)(Math.Abs(l.ColorData[i, j].B - l2.ColorData[i, j].B) / 0.36f);
                }
            return similarity;
        }
        private int GetLuminosityIndex(AnalyzedImage l)
        {
            int luminosity = 0;
            for (int i = 0; i < l.ColorDataWidth; i++)
                for (int j = 0; j < l.ColorDataHeight; j++)
                {
                    luminosity += (int)(l.ColorData[i, j].GetBrightness() * 1000);
                }
            return luminosity;
        }
      
    
    }
}
