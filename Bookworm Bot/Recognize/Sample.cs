using System;
using System.Drawing;
using Bookworm.Scan;

namespace Bookworm.Recognize
{
    [Serializable]
    public class Sample
    {
        public Bitmap Bitmap { get; protected set; }
    }
    [Serializable]
    public class LetterSample : Sample
    {
        public bool Scanned { get; set; } = false;
        public Color[,] ColorData { get; set; } = null;

        public SampleKind Kind { get; set; }
        public char Letter { get; set; }

        public LetterSample(SnapshotLetter letter)
        {
            this.Bitmap = letter.Bitmap;
            this.Kind = SampleKind.Unassigned;
        }
    }
    [Serializable]
    public enum SampleKind
    {
        Unassigned,
        Known
    }
}