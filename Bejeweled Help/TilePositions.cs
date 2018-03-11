using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BookwormBot
{
    public partial class BookwormForm : Form
    {
        public List<Rectangle> TilePositions = new List<Rectangle>();
        public void LoadTilePositions()
        {
            TilePositions.AddRange(new Rectangle[] {
                new Rectangle(667, 541, 88, 88),
                new Rectangle(754, 541, 88, 88),
                new Rectangle(842, 541, 88, 88),
                new Rectangle(929, 541, 88, 88),
                new Rectangle(667, 630, 88, 88),
                new Rectangle(754, 630, 88, 88),
                new Rectangle(842, 630, 88, 88),
                new Rectangle(929, 630, 88, 88),
                new Rectangle(667, 720, 88, 88),
                new Rectangle(754, 720, 88, 88),
                new Rectangle(842, 720, 88, 88),
                new Rectangle(929, 720, 88, 88),
                new Rectangle(667, 809, 88, 88),
                new Rectangle(754, 809, 88, 88),
                new Rectangle(842, 809, 88, 88),
                new Rectangle(929, 809, 88, 88)
            });
        }
    }
}
