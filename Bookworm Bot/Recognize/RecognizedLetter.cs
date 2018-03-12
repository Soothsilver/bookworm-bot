using Bookworm.Scan;

namespace Bookworm.Recognize
{
    public class RecognizedLetter
    {
        public RecognitionLetterKind Kind { get; }
        public SnapshotLetter SnapshotLetter { get; }
        public string Letter { get; } = "";

        public RecognizedLetter(RecognitionLetterKind kind, string letter, SnapshotLetter snapshotLetter)
        {
            this.SnapshotLetter = snapshotLetter;
            this.Kind = kind;
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
        RecognizedButAssigned,
        Dark
    }
}