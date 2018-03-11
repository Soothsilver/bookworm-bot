using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Bookworm
{
    [Serializable]
    public class Letter : AnalyzedImage
    {
        public override string ToString()
        {
            return InnerLetter.ToString();
        }
        public char InnerLetter = '?';
        public bool Known = false;
        public int ConfidenceIndex;
        public Letter ClosestFriend;
        public bool IsArtifact = false;
        //public Color[,] ColorData;
        public Bitmap FullBitmap;
        public bool IsSmashed;
        public bool IsEmpty;
        public bool IsStunned;
        public bool IsLocked;
        public bool IsPlagued;
        // Gems
        public bool IsAmethyst;
        public bool IsEmerald;
        public bool IsSapphire;
        public bool IsGarnet;
        public bool IsRuby;
        public bool IsCrystal;
        public bool IsDiamond;

        public int PositionInGrid;
    }
}
