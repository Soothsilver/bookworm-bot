using System;

namespace Bookworm.Scrabble
{
    public class ScrabbleResult
    {
        public Word BestWord { get; }
        public DateTime Timestamp { get; }

        public ScrabbleResult(Word word)
        {
            BestWord = word;
            Timestamp = DateTime.Now;
        }
    }
}