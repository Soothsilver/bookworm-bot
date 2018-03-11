using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bookworm.Scan
{
    public class Snapshot
    {
        public SnapshotKeyboard Keyboard { get; } = new SnapshotKeyboard();

        public Bitmap FullBitmap { get; }
        public Bitmap KeyboardBitmap { get; }
        public DateTime Timestamp { get; }

        public Snapshot(Bitmap fullBitmap, Bitmap keyboardBitmap)
        {
            this.FullBitmap = fullBitmap;
            this.KeyboardBitmap = keyboardBitmap;
            this.Timestamp = DateTime.Now;
        }
    }
    public class SnapshotKeyboard
    {
        internal void PaintSimplified(PaintEventArgs e, Rectangle simpleLetter)
        {
        }
    }
}
