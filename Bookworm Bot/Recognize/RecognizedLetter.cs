using Bookworm.Scan;

namespace Bookworm.Recognize
{
    public class RecognizedLetter
    {
        public RecognitionLetterKind Kind { get; }
        public SnapshotLetter SnapshotLetter { get; }
        public string Letter { get; } = "";
        public string DisplayTwoLine { get; } = "";
        public bool IsRecognized
        {
            get
            {
                return Kind == RecognitionLetterKind.Letter || Kind == RecognitionLetterKind.RecognizedButUnassigned || Kind == RecognitionLetterKind.BannedLetter;
            }
        }

        public RecognizedLetter(RecognitionLetterKind kind, string letter, string desc, SnapshotLetter snapshotLetter)
        {
            this.SnapshotLetter = snapshotLetter;
            this.Kind = kind;
            this.DisplayTwoLine = letter + "\n" + desc;
            this.Letter = letter;
        }


        public override string ToString()
        {
            return Letter;
        }
    }
    public enum RecognitionLetterKind
    {
        Unspecified,
        UnknownLetter,
        Letter,
        RecognizedButUnassigned,
        Dark,
        BannedLetter
    }
}