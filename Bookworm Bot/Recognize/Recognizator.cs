using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bookworm.Scan;

namespace Bookworm.Recognize
{
    public class Recognizator
    {
        private Bot bot;
        private int RecognizingInProgress = 0;
        public RecognitionResults LastRecognitionResults;

        public Recognizator(Bot bot)
        {
            this.bot = bot;
        }

        internal void StartRecognizing(Snapshot snapshot)
        {
            if (Interlocked.CompareExchange(ref RecognizingInProgress, 1, 0) == 1)
            {
                return;
            }
            Task.Run(() =>
            {
                RecognitionResults recognitionResults = new RecognitionResults();
                for (int i = 0; i < snapshot.Keyboard.Count; i++)
                {
                    var rl = new RecognizedLetter(RecognitionLetterKind.UnknownLetter, snapshot.Keyboard[i]);
                    recognitionResults.Keyboard.Add(rl);
                }
                LastRecognitionResults = recognitionResults;
                RecognizingInProgress = 0;
            });            
        }
    }
}
