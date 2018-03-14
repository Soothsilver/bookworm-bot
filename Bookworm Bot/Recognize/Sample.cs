using System;
using System.Drawing;
using Bookworm.Scan;

namespace Bookworm.Recognize
{
    [Serializable]
    public class Sample
    {
        public Bitmap Bitmap { get; protected set; }

        [NonSerialized]
        public bool Scanned = false;
        [NonSerialized]
        public Color[,] ColorData = null;
    }
    [Serializable]
    public class LetterSample : Sample
    {

        public SampleKind Kind { get; set; }
        public char Letter { get; set; }

        public LetterSample(SnapshotLetter letter)
        {
            this.Bitmap = letter.Bitmap;
            this.Kind = SampleKind.Unassigned;
        }
    }
    [Serializable]
    public class ScreenpartSample : Sample
    {
        public Screenpart Screenpart { get; set; }

        public ScreenpartSample(Bitmap bitmap, Screenpart screenpart)
        {
            Screenpart = screenpart;
            Bitmap = bitmap;
        }

    }
    [Serializable]
    public enum SampleKind
    {
        Unassigned,
        Known,
        BannedStone
    }
}