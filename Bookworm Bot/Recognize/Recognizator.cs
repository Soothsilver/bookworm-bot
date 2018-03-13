using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bookworm.Scan;
using MoreLinq;

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
            if (snapshot.Keyboard.IsDark)
            {
                return;
            }
            if (Interlocked.CompareExchange(ref RecognizingInProgress, 1, 0) == 1)
            {
                return;
            }
            Task.Run(() =>
            {
                RecognitionResults recognitionResults = new RecognitionResults();
                for (int i = 0; i < snapshot.Keyboard.Count; i++)
                {
                    var rl = Recognize(snapshot.Keyboard[i], bot.Database);
                    recognitionResults.Keyboard.Add(rl);
                    if (rl.Kind == RecognitionLetterKind.Letter)
                    {
                        recognitionResults.UsableLetters.Add(rl.Letter.ToUpper()[0]);
                    }
                }
                LastRecognitionResults = recognitionResults;
                bot.Vocabulary.StartWordforming(recognitionResults);
                RecognizingInProgress = 0;
            });            
        }

        public RecognizedLetter Recognize(SnapshotLetter snapshotLetter, DatabaseManager databaseManager)
        {
            if (snapshotLetter.IsDark)
            {
                return new RecognizedLetter(RecognitionLetterKind.Dark, "Dark", "", snapshotLetter);
            }
            List<LetterSample> letters = new List<LetterSample>();
            lock (databaseManager.DatabaseLock)
            {
                foreach (var sample in databaseManager.Database.OfType<LetterSample>())
                {
                    if (!sample.Scanned)
                    {
                        sample.ColorData = bot.PresageAndRecognize.SimplifyLetter(sample.Bitmap, new Rectangle(0, 0, sample.Bitmap.Width, sample.Bitmap.Height));
                        sample.Scanned = true;
                    }                  
                    letters.Add(sample);                    
                }

                if (letters.Count > 0)
                {
                    LetterSample closestMatch = letters.MinBy(sample => bot.PresageAndRecognize.GetDistance(snapshotLetter.SimplifiedData, sample.ColorData));
                    int distance = bot.PresageAndRecognize.GetDistance(snapshotLetter.SimplifiedData, closestMatch.ColorData);
                    if (distance <= PresageAndRecognize.THRESHOLD_OF_DISTANCE_FOR_MATCH)
                    {
                        if (closestMatch.Kind == SampleKind.Known)
                        {
                            return new RecognizedLetter(RecognitionLetterKind.Letter, closestMatch.Letter.ToString(), distance.ToString(), snapshotLetter);
                        }
                        else if (closestMatch.Kind == SampleKind.BannedStone)
                        {
                            return new RecognizedLetter(RecognitionLetterKind.BannedLetter, "Banned", distance.ToString(), snapshotLetter);
                        }
                        else
                        {
                            return new RecognizedLetter(RecognitionLetterKind.RecognizedButUnassigned, "RbU" + closestMatch.Letter.ToString(), distance.ToString(), snapshotLetter);
                        }
                    }
                    return new RecognizedLetter(RecognitionLetterKind.UnknownLetter, "?" + closestMatch.Letter.ToString(), distance.ToString(), snapshotLetter);
                }
                return new RecognizedLetter(RecognitionLetterKind.UnknownLetter, "??", "no db", snapshotLetter);
            }
        }
    }
}
