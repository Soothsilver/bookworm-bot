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
                }
                LastRecognitionResults = recognitionResults;
                RecognizingInProgress = 0;
            });            
        }

        public RecognizedLetter Recognize(SnapshotLetter snapshotLetter, DatabaseManager databaseManager)
        {
            if (snapshotLetter.IsDark)
            {
                return new RecognizedLetter(RecognitionLetterKind.Dark, "Dark", snapshotLetter);
            }
            List<LetterSample> letters = new List<LetterSample>();
            lock (databaseManager)
            {
                foreach (var sample in databaseManager.Database.OfType<LetterSample>())
                {
                    if (!sample.Scanned)
                    {
                        sample.ColorData = bot.Scan.SimplifyLetter(sample.Bitmap, new Rectangle(0, 0, sample.Bitmap.Width, sample.Bitmap.Height));
                        sample.Scanned = true;
                    }                  
                    letters.Add(sample);                    
                }

                if (letters.Count > 0)
                {
                    LetterSample closestMatch = letters.MinBy(sample => GetDistance(snapshotLetter.SimplifiedData, sample.ColorData));
                    int distance = GetDistance(snapshotLetter.SimplifiedData, closestMatch.ColorData);
                    if (distance <= THRESHOLD_OF_DISTANCE_FOR_MATCH)
                    {
                        if (closestMatch.Kind == SampleKind.Known)
                        {
                            return new RecognizedLetter(RecognitionLetterKind.Letter, closestMatch.Letter.ToString(), snapshotLetter);
                        }
                        else
                        {
                            return new RecognizedLetter(RecognitionLetterKind.RecognizedButAssigned, "RbA" + closestMatch.Letter.ToString(), snapshotLetter);
                        }
                    }
                    return new RecognizedLetter(RecognitionLetterKind.UnknownLetter, "?" + closestMatch.Letter.ToString() + "-" + distance, snapshotLetter);
                }
                return new RecognizedLetter(RecognitionLetterKind.UnknownLetter, "??", snapshotLetter);
            }
        }

        private int GetDistance(Color[,] simplifiedData, Color[,] colorData)
        {
            int w = simplifiedData.GetLength(0);
            int h = simplifiedData.GetLength(1);
            int distance = 0;
            for (int x =0; x< w; x++)
            {
                for (int y =0; y< h;y++)
                {
                    if (simplifiedData[x,y].R != colorData[x,y].R)
                    {
                        distance++;
                    }
                }
            }
            return distance;
        }

        public const int THRESHOLD_OF_DISTANCE_FOR_MATCH = 10;
    }
}
