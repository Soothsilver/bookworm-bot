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

        public Snapshot(Bitmap fullBitmap, Bitmap keyboardBitmap, SnapshotKeyboard keyboard)
        {
            this.FullBitmap = fullBitmap;
            this.KeyboardBitmap = keyboardBitmap;
            this.Keyboard = keyboard;
            this.Timestamp = DateTime.Now;
            int darks = this.Keyboard.Count(letter => letter.IsDark);
            this.Keyboard.IsDark = darks >= this.Keyboard.Count * 0.6;
        }
    }
    public class SnapshotKeyboard : List<SnapshotLetter>
    {
        public bool IsDark { get; internal set; }
        internal void PaintSimplified(PaintEventArgs e, Rectangle whereTo, int letterIndex)
        {
            SnapshotLetter letter = this[letterIndex];
            int width = letter.SimplifiedData.GetLength(0);
            int height = letter.SimplifiedData.GetLength(1);
            int pointSizeW = whereTo.Width / width;
            int pointSizeH = whereTo.Height / height;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y< height; y++)
                {
                    Rectangle rectPoint = new Rectangle(whereTo.X + x * pointSizeW, whereTo.Y + y * pointSizeH, pointSizeW, pointSizeH);
                    e.Graphics.FillRectangle(new SolidBrush(letter.SimplifiedData[x, y]), rectPoint);
                }
            }
        }
    }
}
