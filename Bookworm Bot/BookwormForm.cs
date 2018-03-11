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
using Bookworm.Scan;
using Bookworm.Act;
using Bookworm.Output;
using Bookworm.Recognize;

namespace Bookworm
{
    public partial class BookwormForm : Form
    {
        public static BookwormForm FormInstance;
        public HookOrderType HookOrder = HookOrderType.Null;
        Random rgen = new Random();
        public Hotkeys Hotkeys;
        public Bot Bot = new Bot();

        public StatusOutput StatusOutput;
        public bool DoAnalyzeMode = true;
        DateTime lastMouseManipulation = DateTime.Now;

        public BookwormForm()
        {
            InitializeComponent();
            BookwormForm.FormInstance = this;
            Hotkeys = new Hotkeys(Bot, this); 
            StatusOutput = new StatusOutput(this);
            Hotkeys.LoadHotkeys();
            bLoadAndParseDictionary_Click(this, EventArgs.Empty);
            Bot.Database = LoadDatabase();
            GarbageCollector.StartCollectingInBackground();

            // LoadTilePositions();
        }

        private Database LoadDatabase()
        {
            try
            {
                return Databases.Load();
            } 
            catch (Exception exception)
            {
                LogException(exception);
                return new Database(true);
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
            // Capture
            Bot.Scan.PerformCapture();
            // this.panelGridIsDark.Visible = (GridLuminosity < 900000);
            
            // Redraw pictures
            this.picturebox.Invalidate();
            this.picturebox.Update();

            // Update status
            this.StatusOutput.UpdateAllStatus();
            

            // Autonomous
                       
            // Composure display
            //  this.panelRegainingComposure.Visible = RegainingComposure;

            // Capture display
            /*
            if (GettingImagesForScreenpartDown != Screenpart.Null)
            {
                this.panelCapture.Visible = true;
                this.lblCaptureUp.Text = "Capturing up (" + ScreenPartPictures[GettingImagesForScreenpartUp].Count + "): " + System.Enum.GetName(typeof(Screenpart), GettingImagesForScreenpartUp);
                this.lblCaptureDown.Text = "Capturing dn (" + ScreenPartPictures[GettingImagesForScreenpartDown].Count + "): " + System.Enum.GetName(typeof(Screenpart), GettingImagesForScreenpartDown);
            }
            this.panelAutomode.Visible = Autonomous;
            */
            this.lblPaused.Visible = false;

        }
        Rectangle rectAttackButton = new Rectangle(1129, 645, 191, 191);
        List<Letter> LastRecognizedKeyboard = new List<Letter>();
        List<Letter> Keyboard = new List<Letter>();
        List<AnalyzedImage> AttackPossibleButtons = new List<AnalyzedImage>();
        List<AnalyzedImage> AttackImpossibleButtons = new List<AnalyzedImage>();

        private void RefreshTrainingPanel()
        {
            this.lblTotalLetters.Text = "Total letters: " + LetterDB.Count;
            IEnumerable<Letter> unrecognized = LetterDB.Where(
                    (Letter l) => { return !l.Known; });
            IEnumerable<Letter> recognized = LetterDB.Where(
                    (Letter l) => { return l.Known; });
            this.lblTotalUnrecognizedLetters.Text
                = "Unrecognized letters: " + unrecognized.Count();
            this.cbKnownLetters.Items.Clear();
            this.cbKnownLetters.Items.AddRange(recognized.ToArray());
        }

        public List<Letter> LetterDB = new List<Letter>();

        public int CUT_EDGE_ROWS = 15;
        public int PRECISION = 8;
        // Letters section:
        // Topleft 667/541
        // Size 350/356
        // Letter width: 87,88,88,87
        // Letter height: 89
       AnalyzedImage CurrentAttackButton;
    

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
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
                        Rectangle simpleLetter = new Rectangle(simplifiedKeyboard.X + x * simplifiedKeyboard.Width / 5, simplifiedKeyboard.Y + y * simplifiedKeyboard.Y / 3,
                             simplifiedKeyboard.Width / 5, simplifiedKeyboard.Height / 5);
                        snapshot.Keyboard.PaintSimplified(e, simpleLetter);
                        e.Graphics.DrawRectangle(Pens.White, simpleLetter);

                        Rectangle recognizedLetter = new Rectangle(recognizedKeyboard.X + x * recognizedKeyboard.Width / 5, recognizedKeyboard.Y + y * recognizedKeyboard.Y / 3,
                            recognizedKeyboard.Width / 5, recognizedKeyboard.Height / 5);
                        e.Graphics.DrawString("?", DefaultFont, Brushes.White, recognizedLetter.X + 2, recognizedLetter.Y + 2);
                        e.Graphics.DrawRectangle(Pens.White, recognizedLetter);
                    }
                }
                e.Graphics.DrawRectangle(Pens.White, simplifiedKeyboard);
                e.Graphics.DrawRectangle(Pens.White, recognizedKeyboard);
            }
            Font fontLetter = new Font(FontFamily.GenericSansSerif, 16);
            if (Keyboard.Count > 0)
            {
                int TILESIZE = 4;
                int LETTERSIZE = 40;
                int stx = 400;
                int sty = 350;
                for (int y = 0; y < 4; y++)
                    for (int x = 0; x < 4; x++)
                    {
                        int i = y * 4 + x;
                        for (int x2 = 0; x2 < Keyboard[i].ColorDataWidth; x2++)
                            for (int y2 = 0; y2 < Keyboard[i].ColorDataHeight; y2++)
                            {
                                e.Graphics.FillRectangle(new SolidBrush(Keyboard[i].ColorData[x2, y2]), new Rectangle(x * LETTERSIZE + x2 * TILESIZE + stx, y * LETTERSIZE + y2 * TILESIZE + sty, TILESIZE, TILESIZE));
                            }       
                    }
            }
            if (LastRecognizedKeyboard.Count > 0)
            {
                int LETTERSIZE = 40;
                int stx = 400;
                int sty = 350;
                for (int y = 0; y < 4; y++)
                    for (int x = 0; x < 4; x++)
                    {
                        int i = y * 4 + x;
                        e.Graphics.DrawString(LastRecognizedKeyboard[i].InnerLetter + (LastRecognizedKeyboard[i].Known ? "" : "?"), fontLetter, Brushes.Black, new PointF(x * LETTERSIZE + stx, y * LETTERSIZE + sty + 170));



                    }
             
            }
            /*
            if (BestWord != null)
            {
                e.Graphics.DrawString(BestWord.String, fontLetter, Brushes.Black, 420, 700);
                e.Graphics.DrawString("Power: " + BestWord.Power.ToString(), fontLetter, Brushes.Black, 420, 720);
            }*/
           
        }


        

        public int CurrentIndexOfUnrecognizedLetter = -1;
        private void bFindNextUnrecognizedLetter_Click(object sender, EventArgs e)
        {
            if (LetterDB.Count > 0)
            {
                CurrentIndexOfUnrecognizedLetter++; 
                if (CurrentIndexOfUnrecognizedLetter >= LetterDB.Count)
                {
                    CurrentIndexOfUnrecognizedLetter = 0;
                }
                int startingat = CurrentIndexOfUnrecognizedLetter;
                while (true)
                {
                    if (LetterDB[CurrentIndexOfUnrecognizedLetter].Known == false)
                    {
                        LoadLetterToTrainPanel(LetterDB[CurrentIndexOfUnrecognizedLetter]);
                        return;
                    }
                    // Iterate
                    CurrentIndexOfUnrecognizedLetter++;
                    if (CurrentIndexOfUnrecognizedLetter == LetterDB.Count)
                    {
                        CurrentIndexOfUnrecognizedLetter = 0;
                    }
                    if (CurrentIndexOfUnrecognizedLetter == startingat) break;
                }
                MessageBox.Show("No more unrecognized letters.");
            }
        }
  
        public Letter EditingLetter;
        private void LoadLetterToTrainPanel(Letter letter)
        {
            EditingLetter = letter;
            this.tbLetter.Text = letter.InnerLetter.ToString();
            this.chKnown.Checked = letter.Known;
            this.chArtifact.Checked = letter.IsArtifact;
            this.chSmashed.Checked = letter.IsSmashed;
            this.chStunned.Checked = letter.IsStunned;
            this.chLocked.Checked = letter.IsLocked;
            this.chPlagued.Checked = letter.IsPlagued;
            this.chEmpty.Checked = letter.IsEmpty;


            this.chAmethyst.Checked = EditingLetter.IsAmethyst;
            this.chCrystal.Checked = EditingLetter.IsCrystal;
            this.chDiamond.Checked = EditingLetter.IsDiamond;
            this.chEmerald.Checked = EditingLetter.IsEmerald;
            this.chGarnet.Checked = EditingLetter.IsGarnet;
            this.chRuby.Checked = EditingLetter.IsRuby;
            this.chSapphire.Checked = EditingLetter.IsSapphire;

            this.pictureFullBitmap.Invalidate();
            this.pictureImgData.Invalidate();
            if (letter.ClosestFriend != null)
            {
                this.lblComputerSuggestionConfidenceIndex.Text = letter.ConfidenceIndex.ToString();
                this.lblComputerSuggestionLetter.Text = letter.ClosestFriend.ToString();
            }
            else
            {
                this.lblComputerSuggestionLetter.Text = "(no suggestion)";
                this.lblComputerSuggestionConfidenceIndex.Text = "0";
            }

        }

        private void bChooseKnownLetter_Click(object sender, EventArgs e)
        {
            if (cbKnownLetters.Items.Count > 0 && cbKnownLetters.SelectedIndex >= 0)
            {
                LoadLetterToTrainPanel((Letter)cbKnownLetters.Items[cbKnownLetters.SelectedIndex]);
            }
        }
        private void pictureFullBitmap_Paint(object sender, PaintEventArgs e)
        {
            if (EditingLetter != null)
            {
                e.Graphics.DrawImage(EditingLetter.FullBitmap, new Point(0,0));
            }
        }
        private void pictureImgData_Paint(object sender, PaintEventArgs e)
        {
            if (EditingLetter != null)
            {
                int TILESIZE = 4;
                int stx = 0;
                int sty = 0;
                        for (int x2 = 0; x2 < EditingLetter.ColorDataWidth; x2++)
                            for (int y2 = 0; y2 < EditingLetter.ColorDataHeight; y2++)
                            {
                                e.Graphics.FillRectangle(new SolidBrush(EditingLetter.ColorData[x2, y2]), new Rectangle(x2 * TILESIZE + stx,y2 * TILESIZE + sty, TILESIZE, TILESIZE));
                            }
                    
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
        private void bAllAreUnknown_Click(object sender, EventArgs e)
        {
            foreach (Letter l in LetterDB)
                l.Known = false;
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
        private void bDelayedAutostart_Click(object sender, EventArgs e)
        {/*
            StartAutomodeDelayedOn = true;
            startAutomodeIn = DateTime.Now.AddSeconds(10);*/
        }

        private void bRemoveFriend_Click(object sender, EventArgs e)
        {/*
            if (EditingLetter != null && EditingLetter.ClosestFriend != null)
                if (LetterDB.Contains(EditingLetter.ClosestFriend))
                {
                    LetterDB.Remove(EditingLetter.ClosestFriend);
                    EditingLetter.ClosestFriend = null;
                }*/
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }
    }
  }

