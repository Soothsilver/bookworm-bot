﻿using System;
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
using Bookworm.Scan;
using Bookworm.Act;
using Bookworm.Output;
using Bookworm.Recognize;
using System.Threading.Tasks;

namespace Bookworm
{
    public partial class BookwormForm : Form
    {
        public static BookwormForm FormInstance;
        public HookOrderType HookOrder = HookOrderType.Null;
        Random rgen = new Random();
        public Hotkeys Hotkeys;
        public Bot Bot = new Bot();
        public LockModeState LockMode = new LockModeState();

        public StatusOutput StatusOutput;
        public bool DoAnalyzeMode = true;
        DateTime lastMouseManipulation = DateTime.Now;

        public BookwormForm()
        {
            InitializeComponent();
            LockMode.Active = true;
            BookwormForm.FormInstance = this;
            Hotkeys = new Hotkeys(Bot, this); 
            StatusOutput = new StatusOutput(this);
            Hotkeys.LoadHotkeys();
            bLoadAndParseDictionary_Click(this, EventArgs.Empty);
            this.cbDatabase.SelectedIndex = 1;
            Bot.Database.SwitchDatabase(this.cbDatabase.SelectedItem.ToString());
            Bot.Vocabulary.LoadGrimmDictionaryAsync();
            GarbageCollector.StartCollectingInBackground();
            foreach(Screenpart screenpart in (Screenpart[])Enum.GetValues(typeof(Screenpart)))
            {
                this.lbScreenparts.Items.Add(screenpart);
            }
            Task.Run((Action)CaptureAndBot);
            // LoadTilePositions();
        }

        private void CaptureAndBot()
        {
            while (true)
            {
                // Capture
                Bot.Scan.PerformCapture();
                // Autonomous
                this.Bot.Autonomous.Tick();
            }
        }

        private void LogException(Exception exception)
        {
            this.panelException.Visible = true;
            this.tbLog.AppendText(exception.ToString());
            this.tbException.Text = exception.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            // Redraw pictures
            this.picturebox.Invalidate();
            this.picturebox.Update();

            // Update status
            this.StatusOutput.UpdateAllStatus();
            
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                e.Graphics.FillRectangle(Brushes.DarkViolet, e.ClipRectangle);
                if (Bot.Scan.LastSnapshot != null)
                {
                    Snapshot snapshot = Bot.Scan.LastSnapshot;
                    e.Graphics.DrawImage(snapshot.FullBitmap, new Rectangle(10, 10, 640, 360));
                    e.Graphics.DrawRectangle(Pens.White, new Rectangle(10, 10, 640, 360));

                    int keyboardWidth = snapshot.KeyboardBitmap.Width;
                    int keyboardHeight = snapshot.KeyboardBitmap.Height;
                    int keyboardDisplayWidth = keyboardWidth * 1 / 3;
                    int keyboardDisplayHeight = keyboardHeight * 1 / 3;
                    // Actual keyboard                
                    e.Graphics.DrawImage(snapshot.KeyboardBitmap, new Rectangle(10, 380, keyboardDisplayWidth, keyboardDisplayHeight));
                    e.Graphics.DrawRectangle(Pens.White, new Rectangle(10, 380, keyboardDisplayWidth, keyboardDisplayHeight));

                    // Simplified keyboard
                    Rectangle simplifiedKeyboard = new Rectangle(keyboardDisplayWidth + 20, 380, keyboardDisplayWidth, keyboardDisplayHeight);
                    Rectangle recognizedKeyboard = new Rectangle(10, 380 + keyboardDisplayHeight + 10, keyboardDisplayWidth, keyboardDisplayHeight);

                    // Guesses
                    for (int x = 0; x < 5; x++)
                    {
                        for (int y = 0; y < 3; y++)
                        {
                            Rectangle simpleLetter = new Rectangle(simplifiedKeyboard.X + x * simplifiedKeyboard.Width / 5, simplifiedKeyboard.Y + y * simplifiedKeyboard.Height / 3,
                                 simplifiedKeyboard.Width / 5, simplifiedKeyboard.Height / 3);
                            snapshot.Keyboard.PaintSimplified(e, simpleLetter, y * 5 + x);
                            e.Graphics.DrawRectangle(Pens.White, simpleLetter);

                            if (Bot.Recognizator.LastRecognitionResults != null)
                            {
                                Rectangle recognizedLetter = new Rectangle(recognizedKeyboard.X + x * recognizedKeyboard.Width / 5, recognizedKeyboard.Y + y * recognizedKeyboard.Height / 3,
                                    recognizedKeyboard.Width / 5, recognizedKeyboard.Height / 3);
                                e.Graphics.DrawString(Bot.Recognizator.LastRecognitionResults.Keyboard[y*5+x].DisplayTwoLine, DefaultFont, Brushes.White, recognizedLetter.X + 5, recognizedLetter.Y + 5);
                                e.Graphics.DrawRectangle(Pens.White, recognizedLetter);
                                if (LockMode.Active && LockMode.X == x && LockMode.Y == y)
                                {
                                    e.Graphics.DrawRectangle(new Pen(Brushes.Yellow, 3), recognizedLetter);
                                    e.Graphics.DrawRectangle(new Pen(Brushes.Yellow, 3), simpleLetter);
                                }
                            }
                        }
                    }
                    e.Graphics.DrawRectangle(Pens.White, simplifiedKeyboard);
                    e.Graphics.DrawRectangle(Pens.White, recognizedKeyboard);
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }           
        }


        

        public int CurrentIndexOfUnrecognizedLetter = -1;
  

        private void bChooseKnownLetter_Click(object sender, EventArgs e)
        {
            if (cbKnownLetters.Items.Count > 0 && cbKnownLetters.SelectedIndex >= 0)
            {
            }
        }

        private void bLoadLetterDB_Click(object sender, EventArgs e)
        {
            /*
            LetterDB.Clear();
            using (FileStream fs = new FileStream(LETTERDB_FILEPATH, FileMode.OpenOrCreate, FileAccess.Read))
            {
                BinaryFormatter bf = new BinaryFormatter();
                try
                {
                    LetterDB = (List<Letter>)bf.Deserialize(fs);
                    fs.Flush();
                }catch (Exception)
                {
                    LetterDB.Clear();
                }
            }
            LoadAttackButtons();
            LoadScreenparts();
            RefreshTrainingPanel();*/
        }

        private void LoadAttackButtons()
        {
            /*
            try
            {
                using (FileStream fs = new FileStream(ATTACKPOSSIBLE_FILEPATH, FileMode.OpenOrCreate, FileAccess.Read))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    AttackPossibleButtons = (List<AnalyzedImage>)bf.Deserialize(fs);
                    fs.Flush();
                }
            }
            catch
            {
                AttackPossibleButtons = new List<AnalyzedImage>();
            } 
            try
            {
                using (FileStream fs = new FileStream(ATTACKIMPOSSIBLE_FILEPATH, FileMode.OpenOrCreate, FileAccess.Read))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    AttackImpossibleButtons = (List<AnalyzedImage>)bf.Deserialize(fs);
                    fs.Flush();
                }
            }
            catch
            {
                AttackImpossibleButtons = new List<AnalyzedImage>();
            }*/
        }
        private void LoadScreenparts()
        {
            /*
            try
            {
                using (FileStream fs = new FileStream(SCREENPARTS_FILEPATH, FileMode.OpenOrCreate, FileAccess.Read))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    ScreenPartPictures = (Dictionary<Screenpart, List<AnalyzedImage>>)bf.Deserialize(fs);
                    fs.Flush();
                }
            }
            catch
            {
                ScreenPartPictures = new Dictionary<Screenpart, List<AnalyzedImage>>();
                foreach (Screenpart sp in Enum.GetValues(typeof(Screenpart)))
                {
                    if (sp != Screenpart.Null)
                    {
                        ScreenPartPictures.Add(sp, new List<AnalyzedImage>());
                    }
                }
            }
            */
        }
        private void SerializeRemovalVocabulary()
        {
         /*   
            using (FileStream stream = new FileStream("removalVocabulary.xml", FileMode.OpenOrCreate, FileAccess.Write))
            {
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(RemovalVocabulary.GetType());
                x.Serialize(stream, RemovalVocabulary);
            }*/
        }
        public void LoadRemovalVocabulary()
        {/*
            try
            {
                RemovalVocabulary = new List<string>();
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(RemovalVocabulary.GetType());
                TextReader textReader = new StreamReader("removalVocabulary.xml");
                RemovalVocabulary = (List<string>)x.Deserialize(textReader);
                textReader.Close();
            }
            catch
            {
                RemovalVocabulary = new List<string>();
            }*/
        }
        private void SerializeScreenparts()
        {
            /*
            using (System.IO.FileStream s = new System.IO.FileStream(SCREENPARTS_FILEPATH, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(s, ScreenPartPictures);
                s.Flush();
            }*/
        }
        private void bSaveLetter_Click(object sender, EventArgs e)
        {
            /*
            EditingLetter.Known = true;
            EditingLetter.InnerLetter = this.tbLetter.Text[0];
            EditingLetter.IsArtifact = this.chArtifact.Checked;
            EditingLetter.IsSmashed = this.chSmashed.Checked;
            EditingLetter.IsStunned = this.chStunned.Checked;
            EditingLetter.IsEmpty = this.chEmpty.Checked;
            EditingLetter.IsLocked = this.chLocked.Checked;
            EditingLetter.IsPlagued = this.chPlagued.Checked;

            EditingLetter.IsAmethyst = this.chAmethyst.Checked;
            EditingLetter.IsCrystal = this.chCrystal.Checked;
            EditingLetter.IsDiamond = this.chDiamond.Checked;
            EditingLetter.IsEmerald = this.chEmerald.Checked;
            EditingLetter.IsGarnet = this.chGarnet.Checked;
            EditingLetter.IsRuby = this.chRuby.Checked;
            EditingLetter.IsSapphire = this.chSapphire.Checked;

            RecognizeAllUnrecognizedInDB();
            RefreshTrainingPanel();
            LoadLetterToTrainPanel(EditingLetter);*/
        }
      
        private void bAgreeWithComputer_Click(object sender, EventArgs e)
        {/*
            if (LetterDB.Contains(EditingLetter))
            {
                LetterDB.Remove(EditingLetter);
                this.bFindNextUnrecognizedLetter_Click(sender, e);
            }
            SerializeLetterDB();*/
        }

        private void bForgetThis_Click(object sender, EventArgs e)
        {
            /*
            if (LetterDB.Contains(EditingLetter))
            {
                LetterDB.Remove(EditingLetter);
                RefreshTrainingPanel();
                this.bFindNextUnrecognizedLetter_Click(sender, e);
            }
            SerializeLetterDB();*/
        }
        private void bRecognizeAll_Click(object sender, EventArgs e)
        {/*
            List<Letter> allLetters = new List<Letter>();
            foreach (Letter a in LetterDB)
            {
                allLetters.Add(a);
            }
            RecognizeLetters(allLetters, true);*/
        }
      
        private void bRemoveZeroConfidence_Click(object sender, EventArgs e)
        {/*
            for (int i = LetterDB.Count - 1; i >= 0; i--)
            {
                if ((LetterDB[i].ConfidenceIndex < CONFIDENCE_THRESHOLD || LetterDB[i].ConfidenceIndex > 30000) && LetterDB[i].Known == false)
                {
                    LetterDB.RemoveAt(i);
                }
            }
            SerializeLetterDB();*/
        }
   




     

        private void bLoadAndParseDictionary_Click(object sender, EventArgs e)
        {/*
            LoadAndParseDictionary(this.cbDictionary.Text);*/
        }

        DateTime startAutomodeIn = DateTime.Now;
        public bool StartAutomodeDelayedOn = false;

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void bRefreshDatabaseView_Click(object sender, EventArgs e)
        {
            RefreshDatabaseListView();
        }

        private void RefreshDatabaseListView()
        {
            lock (Bot.Database.DatabaseLock)
            {
                var imagelist = new ImageList();
                this.listviewDatabase.Clear();
                int i = 1;
                foreach (Sample sample in Bot.Database.Database)
                {
                    imagelist.Images.Add(sample.Bitmap);
                    ListViewItem item = new ListViewItem("Letter " + i, i - 1);
                    if (sample is LetterSample)
                    {
                        LetterSample letterSample = sample as LetterSample;
                        if (letterSample.Kind == SampleKind.Known)
                        {
                            item.Text = letterSample.Letter.ToString();
                        }
                        if (letterSample.Kind == SampleKind.BannedStone)
                        {
                            item.Text = "Banned";
                        }
                    }
                    if (sample is ScreenpartSample)
                    {
                        ScreenpartSample screenpartSample = sample as ScreenpartSample;
                        item.Text = screenpartSample.Screenpart.ToString();
                    }
                    item.Tag = sample;
                    this.listviewDatabase.Items.Add(item);
                    i++;
                }
                this.listviewDatabase.LargeImageList = imagelist;
            }
        }

        private void tabControl_TabIndexChanged(object sender, EventArgs e)
        {
            lock (Bot.Database.DatabaseLock)
            {
                RefreshDatabaseListView();
            }
        }

        public Sample EditingLetter;

        private void listviewDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listviewDatabase.SelectedItems.Count == 0)
            {
                return;
            }
            ListViewItem selectedItem = this.listviewDatabase.SelectedItems[0];
            Sample letterSample = (Sample)selectedItem.Tag;
            this.pictureSingleLetter.Image = letterSample.Bitmap;
            this.EditingLetter = letterSample;
        }

        private void bDeleteSingleLetter_Click(object sender, EventArgs e)
        {
            if (this.EditingLetter != null)
            {
                Bot.Database.Remove(this.EditingLetter);
                for(int i =0; i < this.listviewDatabase.Items.Count; i++)
                {
                    ListViewItem item = this.listviewDatabase.Items[i];
                    if (item.Tag == this.EditingLetter)
                    {
                        this.listviewDatabase.Items.RemoveAt(i);
                        break;
                    }
                }
                LetterEditComplete();
            }
        }

        private void LetterEditComplete()
        {
            if (listviewDatabase.SelectedIndices.Count == 1)
            {
                int index = listviewDatabase.SelectedIndices[0];
                if (index >= 0 && index < listviewDatabase.Items.Count - 1)
                {
                    listviewDatabase.SelectedIndices.Clear();
                    listviewDatabase.SelectedIndices.Add(index + 1);
                    listviewDatabase.Items[index + 1].Selected = true;
                    listviewDatabase.Refresh();
                    this.tbSingleLetter.Select();
                    this.tbSingleLetter.SelectAll();
                }
            }
        }

        private void bSaveSingleLetter_Click(object sender, EventArgs e)
        {
            if (this.EditingLetter != null && this.EditingLetter is LetterSample)
            {
                if (this.tbSingleLetter.Text.Length == 1)
                {
                    (this.EditingLetter as LetterSample).Kind = SampleKind.Known;
                    (this.EditingLetter as LetterSample).Letter = this.tbSingleLetter.Text.ToUpper()[0];
                    Bot.Database.SaveDatabase();
                }
                LetterEditComplete();
            }
        }

        private void bClearDatabase_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to delete ALL the selected database items?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var collection = listviewDatabase.SelectedItems;
                foreach (var item in collection)
                {
                    Bot.Database.Remove(((LetterSample)((ListViewItem)item).Tag));
                }
                Bot.Database.SaveDatabase();
                RefreshDatabaseListView();
            }
        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void lblBestWord_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void lblWordformingStatus_Click(object sender, EventArgs e)
        {

        }

        private void bSwitchDatabase_Click(object sender, EventArgs e)
        {
            this.tabControl.SelectedIndex = 5;
        }

        private void bSwitchDatabasesNow_Click(object sender, EventArgs e)
        {
            string dbname = this.cbDatabase.SelectedItem.ToString();
            Bot.Database.SwitchDatabase(dbname);
        }

        private void bBanLetter_Click(object sender, EventArgs e)
        {
            if (this.EditingLetter != null && this.EditingLetter is LetterSample)
            {
               
                (this.EditingLetter as LetterSample).Kind = SampleKind.BannedStone;
                Bot.Database.SaveDatabase();
                
                LetterEditComplete();
            }
        }

        private void BookwormForm_Load(object sender, EventArgs e)
        {

        }
    }
  }

