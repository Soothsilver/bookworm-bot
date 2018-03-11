using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Bookworm
{
    [Serializable]
    public class AnalyzedImage
    {
        public Color[,] ColorData;
        public int ColorDataWidth;
        public int ColorDataHeight;
    }
}
