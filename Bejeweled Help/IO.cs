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
        public string LETTERDB_FILEPATH = "letterdb.dat";
        public string ATTACKPOSSIBLE_FILEPATH = "attackPossible.dat";
        public string ATTACKIMPOSSIBLE_FILEPATH = "attackImpossible.dat";
        private void SerializeLetterDB()
        {
            using (System.IO.FileStream s = new System.IO.FileStream("letterdb.dat", System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(s, LetterDB);
                s.Flush();
            }
        }

    }
}
