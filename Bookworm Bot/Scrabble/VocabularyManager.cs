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
                List<Word> bestWords = FindMostPowerfulMatchingWord(this.Vocabulary, recognitionResults.UsableLetters, 8);
                if (bestWords.Count > 0)
                {
                    LastScrabbleResult = new ScrabbleResult(bestWords[0], bestWords);
                }
                else
                {
                    LastScrabbleResult = new ScrabbleResult(null, new List<Word>());
                }
                wordformingInProgress = 0;
            });
        }

        private List<Word> FindMostPowerfulMatchingWord(Vocabulary vocabulary, List<char> usableLetters, int numWords)
        {
            List<Word> results = new List<Word>();
            foreach(Word word in vocabulary)
            {
                if (word.CreatableFrom(usableLetters))
                {
                    results.Add(word);
                    if (results.Count == numWords)
                    {
                        return results;
                    }
                }
            }
            return results;
        }
    }
}
