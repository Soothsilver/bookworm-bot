using Bookworm.Scan;

namespace Bookworm.Recognize
{
    public class RecognizedLetter
    {
        public RecognitionLetterKind Kind { get; }
        public SnapshotLetter SnapshotLetter { get; }
        public string Letter { get; } = "";

        public RecognizedLetter(RecognitionLetterKind kind, SnapshotLetter snapshotLetter)
        {
            this.SnapshotLetter = snapshotLetter;
            this.Kind = kind;
        }

        public override string ToString()
        {
            if (Kind == RecognitionLetterKind.UnknownLetter)
            {
                return "??";
            }
            else
            {
                return Letter;
            }
        }
    }
    public enum RecognitionLetterKind
    {
        Unspecified,
        UnknownLetter,
        Letter
    }
}