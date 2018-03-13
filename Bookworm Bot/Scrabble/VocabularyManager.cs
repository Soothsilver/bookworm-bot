using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bookworm.Recognize;

namespace Bookworm.Scrabble
{
    public class VocabularyManager
    {
        public const string PATH_TO_DICTIONARY = "Scrabble\\GrimmDictionary.txt";
        private static char[] HashArray = new char[] { '#' };
        public Vocabulary Vocabulary { get; } = new Vocabulary();
        public bool VocabularyLoaded { get; private set; } = false;
        private int wordformingInProgress = 0;
        public ScrabbleResult LastScrabbleResult = null;

        public void LoadGrimmDictionaryAsync()
        {
            Task.Run(() =>
            {
                LoadGrimmDictionary(PATH_TO_DICTIONARY);
                VocabularyLoaded = true;
            });
        }
        private void LoadGrimmDictionary(string path)
        {
            foreach (string line in System.IO.File.ReadLines(path))
            {
                string[] parts = line.Split(HashArray, 2);
                Vocabulary.Add(new Word(parts[0]));
            }
            Vocabulary.Sort((w1, w2) => -w1.Power.CompareTo(w2.Power));
        }

        internal void StartWordforming(RecognitionResults recognitionResults)
        {
            if (!VocabularyLoaded)
            {
                return;
            }
            if (Interlocked.CompareExchange(ref wordformingInProgress, 1, 0) == 1)
            {
                return;
            }
            Task.Run(() =>
            {
                Word bestWord = FindMostPowerfulMatchingWord(this.Vocabulary, recognitionResults.UsableLetters);
                LastScrabbleResult = new ScrabbleResult(bestWord);
                wordformingInProgress = 0;
            });
        }

        private Word FindMostPowerfulMatchingWord(Vocabulary vocabulary, List<char> usableLetters)
        {
            foreach(Word word in vocabulary)
            {
                if (word.CreatableFrom(usableLetters))
                {
                    return word;
                }
            }
            return null;
        }
    }
}
