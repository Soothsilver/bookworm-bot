using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BonusWordDecompiler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.File.WriteAllText(saveFileDialog1.FileName, this.tbWordlist.Text);
            }
        }
        byte[] inputfile;
        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                inputfile = System.IO.File.ReadAllBytes(openFileDialog1.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int count = inputfile.Length;
            string currentword = "";
            StringBuilder wordlist = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                char c = (char)inputfile[i];
                if (Char.IsLetter(c)) currentword += c;
                if (c == 0)
                {
                    if (currentword.Length > 2) wordlist.AppendLine(currentword);
                    currentword = "";
                }
            }
            this.tbWordlist.Text = wordlist.ToString();
        }
    }
}
