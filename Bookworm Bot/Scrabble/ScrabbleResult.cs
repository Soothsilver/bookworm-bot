using System;
using System.Collections.Generic;

namespace Bookworm.Scrabble
{
    public class ScrabbleResult
    {
        public Word BestWord { get; }
        public List<Word> GoodWords = new List<Word>();
        public DateTime Timestamp { get; }

        public ScrabbleResult(Word word, List<Word> goodWords)
        {
            BestWord = word;
            Timestamp = DateTime.Now;
            GoodWords = goodWords;
        }
    }
}