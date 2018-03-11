using System;
using System.Collections.Generic;

namespace Bookworm.Recognize
{
    public class RecognitionResults
    {
        public DateTime Timestamp;
        public List<char> UsableLetters = new List<char>();
        public List<RecognizedLetter> Keyboard = new List<RecognizedLetter>();

        public RecognitionResults()
        {
            Timestamp = DateTime.Now;
        }
    }
}