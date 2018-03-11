namespace BookwormBot
{
    partial class BookwormForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BookwormForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panelTraining = new System.Windows.Forms.Panel();
            this.chPlagued = new System.Windows.Forms.CheckBox();
            this.bRemoveFriend = new System.Windows.Forms.Button();
            this.chLocked = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chRuby = new System.Windows.Forms.CheckBox();
            this.chDiamond = new System.Windows.Forms.CheckBox();
            this.chCrystal = new System.Windows.Forms.CheckBox();
            this.chGarnet = new System.Windows.Forms.CheckBox();
            this.chSapphire = new System.Windows.Forms.CheckBox();
            this.chEmerald = new System.Windows.Forms.CheckBox();
            this.chAmethyst = new System.Windows.Forms.CheckBox();
            this.chStunned = new System.Windows.Forms.CheckBox();
            this.chSmashed = new System.Windows.Forms.CheckBox();
            this.chEmpty = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.chArtifact = new System.Windows.Forms.CheckBox();
            this.bAgreeWithComputer = new System.Windows.Forms.Button();
            this.lblComputerSuggestionConfidenceIndex = new System.Windows.Forms.Label();
            this.lblWithCertainty = new System.Windows.Forms.Label();
            this.lblComputerSuggestionLetter = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.bLoadLetterDB = new System.Windows.Forms.Button();
            this.bForgetThis = new System.Windows.Forms.Button();
            this.chKnown = new System.Windows.Forms.CheckBox();
            this.bSaveLetter = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbLetter = new System.Windows.Forms.TextBox();
            this.pictureImgData = new System.Windows.Forms.PictureBox();
            this.pictureFullBitmap = new System.Windows.Forms.PictureBox();
            this.bFindNextUnrecognizedLetter = new System.Windows.Forms.Button();
            this.cbKnownLetters = new System.Windows.Forms.ComboBox();
            this.bChooseKnownLetter = new System.Windows.Forms.Button();
            this.lblTotalUnrecognizedLetters = new System.Windows.Forms.Label();
            this.lblTotalLetters = new System.Windows.Forms.Label();
            this.lblPossibilities = new System.Windows.Forms.Label();
            this.bLoadAndParseDictionary = new System.Windows.Forms.Button();
            this.cbDictionary = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblGridLuminosity = new System.Windows.Forms.Label();
            this.panelAutomode = new System.Windows.Forms.Panel();
            this.lblPaused = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panelGridIsDark = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.panelException = new System.Windows.Forms.Panel();
            this.tbException = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panelCapture = new System.Windows.Forms.Panel();
            this.lblCaptureDown = new System.Windows.Forms.Label();
            this.lblCaptureUp = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblHP = new System.Windows.Forms.Label();
            this.panelRegainingComposure = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblCurrentBonus = new System.Windows.Forms.Label();
            this.lblNowCapturing = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelTraining.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureImgData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureFullBitmap)).BeginInit();
            this.panelAutomode.SuspendLayout();
            this.panelGridIsDark.SuspendLayout();
            this.panelException.SuspendLayout();
            this.panelCapture.SuspendLayout();
            this.panelRegainingComposure.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1312, 741);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panelTraining
            // 
            this.panelTraining.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTraining.Controls.Add(this.chPlagued);
            this.panelTraining.Controls.Add(this.bRemoveFriend);
            this.panelTraining.Controls.Add(this.chLocked);
            this.panelTraining.Controls.Add(this.groupBox1);
            this.panelTraining.Controls.Add(this.chStunned);
            this.panelTraining.Controls.Add(this.chSmashed);
            this.panelTraining.Controls.Add(this.chEmpty);
            this.panelTraining.Controls.Add(this.button3);
            this.panelTraining.Controls.Add(this.button2);
            this.panelTraining.Controls.Add(this.button1);
            this.panelTraining.Controls.Add(this.chArtifact);
            this.panelTraining.Controls.Add(this.bAgreeWithComputer);
            this.panelTraining.Controls.Add(this.lblComputerSuggestionConfidenceIndex);
            this.panelTraining.Controls.Add(this.lblWithCertainty);
            this.panelTraining.Controls.Add(this.lblComputerSuggestionLetter);
            this.panelTraining.Controls.Add(this.label2);
            this.panelTraining.Controls.Add(this.bLoadLetterDB);
            this.panelTraining.Controls.Add(this.bForgetThis);
            this.panelTraining.Controls.Add(this.chKnown);
            this.panelTraining.Controls.Add(this.bSaveLetter);
            this.panelTraining.Controls.Add(this.label1);
            this.panelTraining.Controls.Add(this.tbLetter);
            this.panelTraining.Controls.Add(this.pictureImgData);
            this.panelTraining.Controls.Add(this.pictureFullBitmap);
            this.panelTraining.Controls.Add(this.bFindNextUnrecognizedLetter);
            this.panelTraining.Controls.Add(this.cbKnownLetters);
            this.panelTraining.Controls.Add(this.bChooseKnownLetter);
            this.panelTraining.Controls.Add(this.lblTotalUnrecognizedLetters);
            this.panelTraining.Controls.Add(this.lblTotalLetters);
            this.panelTraining.Location = new System.Drawing.Point(672, 25);
            this.panelTraining.Name = "panelTraining";
            this.panelTraining.Size = new System.Drawing.Size(638, 329);
            this.panelTraining.TabIndex = 3;
            // 
            // chPlagued
            // 
            this.chPlagued.AutoSize = true;
            this.chPlagued.Location = new System.Drawing.Point(283, 86);
            this.chPlagued.Name = "chPlagued";
            this.chPlagued.Size = new System.Drawing.Size(65, 17);
            this.chPlagued.TabIndex = 29;
            this.chPlagued.Text = "Plagued";
            this.chPlagued.UseVisualStyleBackColor = true;
            // 
            // bRemoveFriend
            // 
            this.bRemoveFriend.Location = new System.Drawing.Point(383, 283);
            this.bRemoveFriend.Name = "bRemoveFriend";
            this.bRemoveFriend.Size = new System.Drawing.Size(99, 26);
            this.bRemoveFriend.TabIndex = 28;
            this.bRemoveFriend.Text = "Remove friend";
            this.bRemoveFriend.UseVisualStyleBackColor = true;
            this.bRemoveFriend.Click += new System.EventHandler(this.bRemoveFriend_Click);
            // 
            // chLocked
            // 
            this.chLocked.AutoSize = true;
            this.chLocked.Location = new System.Drawing.Point(283, 105);
            this.chLocked.Name = "chLocked";
            this.chLocked.Size = new System.Drawing.Size(62, 17);
            this.chLocked.TabIndex = 27;
            this.chLocked.Text = "Locked";
            this.chLocked.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chRuby);
            this.groupBox1.Controls.Add(this.chDiamond);
            this.groupBox1.Controls.Add(this.chCrystal);
            this.groupBox1.Controls.Add(this.chGarnet);
            this.groupBox1.Controls.Add(this.chSapphire);
            this.groupBox1.Controls.Add(this.chEmerald);
            this.groupBox1.Controls.Add(this.chAmethyst);
            this.groupBox1.Location = new System.Drawing.Point(359, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(144, 154);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Gem ";
            // 
            // chRuby
            // 
            this.chRuby.AutoSize = true;
            this.chRuby.Location = new System.Drawing.Point(6, 90);
            this.chRuby.Name = "chRuby";
            this.chRuby.Size = new System.Drawing.Size(51, 17);
            this.chRuby.TabIndex = 33;
            this.chRuby.Text = "Ruby";
            this.chRuby.UseVisualStyleBackColor = true;
            // 
            // chDiamond
            // 
            this.chDiamond.AutoSize = true;
            this.chDiamond.Location = new System.Drawing.Point(6, 128);
            this.chDiamond.Name = "chDiamond";
            this.chDiamond.Size = new System.Drawing.Size(68, 17);
            this.chDiamond.TabIndex = 32;
            this.chDiamond.Text = "Diamond";
            this.chDiamond.UseVisualStyleBackColor = true;
            // 
            // chCrystal
            // 
            this.chCrystal.AutoSize = true;
            this.chCrystal.Location = new System.Drawing.Point(6, 109);
            this.chCrystal.Name = "chCrystal";
            this.chCrystal.Size = new System.Drawing.Size(57, 17);
            this.chCrystal.TabIndex = 31;
            this.chCrystal.Text = "Crystal";
            this.chCrystal.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chCrystal.UseVisualStyleBackColor = true;
            // 
            // chGarnet
            // 
            this.chGarnet.AutoSize = true;
            this.chGarnet.Location = new System.Drawing.Point(6, 72);
            this.chGarnet.Name = "chGarnet";
            this.chGarnet.Size = new System.Drawing.Size(58, 17);
            this.chGarnet.TabIndex = 30;
            this.chGarnet.Text = "Garnet";
            this.chGarnet.UseVisualStyleBackColor = true;
            // 
            // chSapphire
            // 
            this.chSapphire.AutoSize = true;
            this.chSapphire.Location = new System.Drawing.Point(6, 54);
            this.chSapphire.Name = "chSapphire";
            this.chSapphire.Size = new System.Drawing.Size(68, 17);
            this.chSapphire.TabIndex = 29;
            this.chSapphire.Text = "Sapphire";
            this.chSapphire.UseVisualStyleBackColor = true;
            // 
            // chEmerald
            // 
            this.chEmerald.AutoSize = true;
            this.chEmerald.Location = new System.Drawing.Point(6, 37);
            this.chEmerald.Name = "chEmerald";
            this.chEmerald.Size = new System.Drawing.Size(64, 17);
            this.chEmerald.TabIndex = 28;
            this.chEmerald.Text = "Emerald";
            this.chEmerald.UseVisualStyleBackColor = true;
            // 
            // chAmethyst
            // 
            this.chAmethyst.AutoSize = true;
            this.chAmethyst.Location = new System.Drawing.Point(6, 19);
            this.chAmethyst.Name = "chAmethyst";
            this.chAmethyst.Size = new System.Drawing.Size(69, 17);
            this.chAmethyst.TabIndex = 27;
            this.chAmethyst.Text = "Amethyst";
            this.chAmethyst.UseVisualStyleBackColor = true;
            // 
            // chStunned
            // 
            this.chStunned.AutoSize = true;
            this.chStunned.Location = new System.Drawing.Point(283, 180);
            this.chStunned.Name = "chStunned";
            this.chStunned.Size = new System.Drawing.Size(66, 17);
            this.chStunned.TabIndex = 25;
            this.chStunned.Text = "Stunned";
            this.chStunned.UseVisualStyleBackColor = true;
            // 
            // chSmashed
            // 
            this.chSmashed.AutoSize = true;
            this.chSmashed.Location = new System.Drawing.Point(283, 162);
            this.chSmashed.Name = "chSmashed";
            this.chSmashed.Size = new System.Drawing.Size(70, 17);
            this.chSmashed.TabIndex = 24;
            this.chSmashed.Text = "Smashed";
            this.chSmashed.UseVisualStyleBackColor = true;
            // 
            // chEmpty
            // 
            this.chEmpty.AutoSize = true;
            this.chEmpty.Location = new System.Drawing.Point(283, 144);
            this.chEmpty.Name = "chEmpty";
            this.chEmpty.Size = new System.Drawing.Size(55, 17);
            this.chEmpty.TabIndex = 23;
            this.chEmpty.Text = "Empty";
            this.chEmpty.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(423, 8);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(119, 41);
            this.button3.TabIndex = 22;
            this.button3.Text = "Delete all non-known zero-confidences";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.bRemoveZeroConfidence_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(504, 241);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(119, 50);
            this.button2.TabIndex = 21;
            this.button2.Text = "Set all letters to unknown";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.bAllAreUnknown_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(504, 174);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 50);
            this.button1.TabIndex = 20;
            this.button1.Text = "Retest all";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.bRecognizeAll_Click);
            // 
            // chArtifact
            // 
            this.chArtifact.AutoSize = true;
            this.chArtifact.Location = new System.Drawing.Point(283, 126);
            this.chArtifact.Name = "chArtifact";
            this.chArtifact.Size = new System.Drawing.Size(59, 17);
            this.chArtifact.TabIndex = 19;
            this.chArtifact.Text = "Artifact";
            this.chArtifact.UseVisualStyleBackColor = true;
            // 
            // bAgreeWithComputer
            // 
            this.bAgreeWithComputer.Location = new System.Drawing.Point(297, 283);
            this.bAgreeWithComputer.Name = "bAgreeWithComputer";
            this.bAgreeWithComputer.Size = new System.Drawing.Size(80, 26);
            this.bAgreeWithComputer.TabIndex = 18;
            this.bAgreeWithComputer.Text = "I agree";
            this.bAgreeWithComputer.UseVisualStyleBackColor = true;
            this.bAgreeWithComputer.Click += new System.EventHandler(this.bAgreeWithComputer_Click);
            // 
            // lblComputerSuggestionConfidenceIndex
            // 
            this.lblComputerSuggestionConfidenceIndex.AutoSize = true;
            this.lblComputerSuggestionConfidenceIndex.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComputerSuggestionConfidenceIndex.Location = new System.Drawing.Point(410, 257);
            this.lblComputerSuggestionConfidenceIndex.Name = "lblComputerSuggestionConfidenceIndex";
            this.lblComputerSuggestionConfidenceIndex.Size = new System.Drawing.Size(51, 20);
            this.lblComputerSuggestionConfidenceIndex.TabIndex = 17;
            this.lblComputerSuggestionConfidenceIndex.Text = "label3";
            // 
            // lblWithCertainty
            // 
            this.lblWithCertainty.AutoSize = true;
            this.lblWithCertainty.Location = new System.Drawing.Point(294, 260);
            this.lblWithCertainty.Name = "lblWithCertainty";
            this.lblWithCertainty.Size = new System.Drawing.Size(113, 13);
            this.lblWithCertainty.TabIndex = 16;
            this.lblWithCertainty.Text = "with confidence index:";
            // 
            // lblComputerSuggestionLetter
            // 
            this.lblComputerSuggestionLetter.AutoSize = true;
            this.lblComputerSuggestionLetter.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComputerSuggestionLetter.Location = new System.Drawing.Point(305, 225);
            this.lblComputerSuggestionLetter.Name = "lblComputerSuggestionLetter";
            this.lblComputerSuggestionLetter.Size = new System.Drawing.Size(70, 25);
            this.lblComputerSuggestionLetter.TabIndex = 15;
            this.lblComputerSuggestionLetter.Text = "label3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(294, 208);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "The computer believes this is:";
            // 
            // bLoadLetterDB
            // 
            this.bLoadLetterDB.Location = new System.Drawing.Point(548, 8);
            this.bLoadLetterDB.Name = "bLoadLetterDB";
            this.bLoadLetterDB.Size = new System.Drawing.Size(75, 124);
            this.bLoadLetterDB.TabIndex = 13;
            this.bLoadLetterDB.Text = "Load DB";
            this.bLoadLetterDB.UseVisualStyleBackColor = true;
            this.bLoadLetterDB.Click += new System.EventHandler(this.bLoadLetterDB_Click);
            // 
            // bForgetThis
            // 
            this.bForgetThis.Location = new System.Drawing.Point(137, 272);
            this.bForgetThis.Name = "bForgetThis";
            this.bForgetThis.Size = new System.Drawing.Size(140, 37);
            this.bForgetThis.TabIndex = 12;
            this.bForgetThis.Text = "Forget this letter";
            this.bForgetThis.UseVisualStyleBackColor = true;
            this.bForgetThis.Click += new System.EventHandler(this.bForgetThis_Click);
            // 
            // chKnown
            // 
            this.chKnown.AutoSize = true;
            this.chKnown.Enabled = false;
            this.chKnown.Location = new System.Drawing.Point(137, 192);
            this.chKnown.Name = "chKnown";
            this.chKnown.Size = new System.Drawing.Size(83, 17);
            this.chKnown.TabIndex = 11;
            this.chKnown.Text = "Recognized";
            this.chKnown.UseVisualStyleBackColor = true;
            // 
            // bSaveLetter
            // 
            this.bSaveLetter.Location = new System.Drawing.Point(137, 215);
            this.bSaveLetter.Name = "bSaveLetter";
            this.bSaveLetter.Size = new System.Drawing.Size(140, 51);
            this.bSaveLetter.TabIndex = 10;
            this.bSaveLetter.Text = "Know this letter";
            this.bSaveLetter.UseVisualStyleBackColor = true;
            this.bSaveLetter.Click += new System.EventHandler(this.bSaveLetter_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(134, 155);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Letter:";
            // 
            // tbLetter
            // 
            this.tbLetter.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbLetter.Location = new System.Drawing.Point(177, 148);
            this.tbLetter.Name = "tbLetter";
            this.tbLetter.Size = new System.Drawing.Size(100, 31);
            this.tbLetter.TabIndex = 8;
            // 
            // pictureImgData
            // 
            this.pictureImgData.Location = new System.Drawing.Point(3, 215);
            this.pictureImgData.Name = "pictureImgData";
            this.pictureImgData.Size = new System.Drawing.Size(121, 94);
            this.pictureImgData.TabIndex = 6;
            this.pictureImgData.TabStop = false;
            this.pictureImgData.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureImgData_Paint);
            // 
            // pictureFullBitmap
            // 
            this.pictureFullBitmap.Location = new System.Drawing.Point(3, 115);
            this.pictureFullBitmap.Name = "pictureFullBitmap";
            this.pictureFullBitmap.Size = new System.Drawing.Size(121, 94);
            this.pictureFullBitmap.TabIndex = 5;
            this.pictureFullBitmap.TabStop = false;
            this.pictureFullBitmap.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureFullBitmap_Paint);
            // 
            // bFindNextUnrecognizedLetter
            // 
            this.bFindNextUnrecognizedLetter.Location = new System.Drawing.Point(130, 59);
            this.bFindNextUnrecognizedLetter.Name = "bFindNextUnrecognizedLetter";
            this.bFindNextUnrecognizedLetter.Size = new System.Drawing.Size(119, 50);
            this.bFindNextUnrecognizedLetter.TabIndex = 4;
            this.bFindNextUnrecognizedLetter.Text = "Find next unrecognized letter";
            this.bFindNextUnrecognizedLetter.UseVisualStyleBackColor = true;
            this.bFindNextUnrecognizedLetter.Click += new System.EventHandler(this.bFindNextUnrecognizedLetter_Click);
            // 
            // cbKnownLetters
            // 
            this.cbKnownLetters.FormattingEnabled = true;
            this.cbKnownLetters.Location = new System.Drawing.Point(3, 59);
            this.cbKnownLetters.Name = "cbKnownLetters";
            this.cbKnownLetters.Size = new System.Drawing.Size(121, 21);
            this.cbKnownLetters.TabIndex = 3;
            // 
            // bChooseKnownLetter
            // 
            this.bChooseKnownLetter.Location = new System.Drawing.Point(3, 86);
            this.bChooseKnownLetter.Name = "bChooseKnownLetter";
            this.bChooseKnownLetter.Size = new System.Drawing.Size(121, 23);
            this.bChooseKnownLetter.TabIndex = 2;
            this.bChooseKnownLetter.Text = "Edit known letter";
            this.bChooseKnownLetter.UseVisualStyleBackColor = true;
            this.bChooseKnownLetter.Click += new System.EventHandler(this.bChooseKnownLetter_Click);
            // 
            // lblTotalUnrecognizedLetters
            // 
            this.lblTotalUnrecognizedLetters.AutoSize = true;
            this.lblTotalUnrecognizedLetters.Location = new System.Drawing.Point(4, 36);
            this.lblTotalUnrecognizedLetters.Name = "lblTotalUnrecognizedLetters";
            this.lblTotalUnrecognizedLetters.Size = new System.Drawing.Size(35, 13);
            this.lblTotalUnrecognizedLetters.TabIndex = 1;
            this.lblTotalUnrecognizedLetters.Text = "label1";
            // 
            // lblTotalLetters
            // 
            this.lblTotalLetters.AutoSize = true;
            this.lblTotalLetters.Location = new System.Drawing.Point(4, 13);
            this.lblTotalLetters.Name = "lblTotalLetters";
            this.lblTotalLetters.Size = new System.Drawing.Size(35, 13);
            this.lblTotalLetters.TabIndex = 0;
            this.lblTotalLetters.Text = "label1";
            // 
            // lblPossibilities
            // 
            this.lblPossibilities.AutoSize = true;
            this.lblPossibilities.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPossibilities.Location = new System.Drawing.Point(1083, 533);
            this.lblPossibilities.Name = "lblPossibilities";
            this.lblPossibilities.Size = new System.Drawing.Size(51, 20);
            this.lblPossibilities.TabIndex = 6;
            this.lblPossibilities.Text = "label3";
            // 
            // bLoadAndParseDictionary
            // 
            this.bLoadAndParseDictionary.Location = new System.Drawing.Point(1072, 392);
            this.bLoadAndParseDictionary.Name = "bLoadAndParseDictionary";
            this.bLoadAndParseDictionary.Size = new System.Drawing.Size(238, 60);
            this.bLoadAndParseDictionary.TabIndex = 7;
            this.bLoadAndParseDictionary.Text = "Load and parse dictionary";
            this.bLoadAndParseDictionary.UseVisualStyleBackColor = true;
            this.bLoadAndParseDictionary.Click += new System.EventHandler(this.bLoadAndParseDictionary_Click);
            // 
            // cbDictionary
            // 
            this.cbDictionary.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbDictionary.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbDictionary.FormattingEnabled = true;
            this.cbDictionary.Items.AddRange(new object[] {
            "english-words.10",
            "english-words.20",
            "english-words.35",
            "english-words.40",
            "english-words.50",
            "english-words.55",
            "english-words.60",
            "english-words.70",
            "english-words.80",
            "english-words.95"});
            this.cbDictionary.Location = new System.Drawing.Point(1072, 365);
            this.cbDictionary.Name = "cbDictionary";
            this.cbDictionary.Size = new System.Drawing.Size(238, 21);
            this.cbDictionary.TabIndex = 9;
            this.cbDictionary.Text = "english-words.10";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(679, 365);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(390, 208);
            this.label3.TabIndex = 10;
            this.label3.Text = resources.GetString("label3.Text");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1069, 696);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Grid luminosity:";
            // 
            // lblGridLuminosity
            // 
            this.lblGridLuminosity.AutoSize = true;
            this.lblGridLuminosity.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGridLuminosity.Location = new System.Drawing.Point(1152, 694);
            this.lblGridLuminosity.Name = "lblGridLuminosity";
            this.lblGridLuminosity.Size = new System.Drawing.Size(45, 16);
            this.lblGridLuminosity.TabIndex = 12;
            this.lblGridLuminosity.Text = "label5";
            // 
            // panelAutomode
            // 
            this.panelAutomode.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panelAutomode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelAutomode.Controls.Add(this.lblPaused);
            this.panelAutomode.Controls.Add(this.label5);
            this.panelAutomode.Location = new System.Drawing.Point(655, 725);
            this.panelAutomode.Name = "panelAutomode";
            this.panelAutomode.Size = new System.Drawing.Size(581, 88);
            this.panelAutomode.TabIndex = 13;
            // 
            // lblPaused
            // 
            this.lblPaused.AutoSize = true;
            this.lblPaused.Font = new System.Drawing.Font("Georgia", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaused.Location = new System.Drawing.Point(365, 20);
            this.lblPaused.Name = "lblPaused";
            this.lblPaused.Size = new System.Drawing.Size(175, 38);
            this.lblPaused.TabIndex = 1;
            this.lblPaused.Text = "but Paused";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Georgia", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(258, 38);
            this.label5.TabIndex = 0;
            this.label5.Text = "Automode Active";
            // 
            // panelGridIsDark
            // 
            this.panelGridIsDark.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panelGridIsDark.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelGridIsDark.Controls.Add(this.label7);
            this.panelGridIsDark.Location = new System.Drawing.Point(655, 823);
            this.panelGridIsDark.Name = "panelGridIsDark";
            this.panelGridIsDark.Size = new System.Drawing.Size(581, 88);
            this.panelGridIsDark.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Georgia", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(12, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(192, 38);
            this.label7.TabIndex = 0;
            this.label7.Text = "Grid is dark.";
            // 
            // panelException
            // 
            this.panelException.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panelException.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelException.Controls.Add(this.tbException);
            this.panelException.Controls.Add(this.label8);
            this.panelException.Location = new System.Drawing.Point(55, 725);
            this.panelException.Name = "panelException";
            this.panelException.Size = new System.Drawing.Size(581, 186);
            this.panelException.TabIndex = 14;
            this.panelException.Visible = false;
            // 
            // tbException
            // 
            this.tbException.Location = new System.Drawing.Point(24, 44);
            this.tbException.Multiline = true;
            this.tbException.Name = "tbException";
            this.tbException.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbException.Size = new System.Drawing.Size(532, 124);
            this.tbException.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Georgia", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(278, 38);
            this.label8.TabIndex = 0;
            this.label8.Text = "Exception occured";
            // 
            // panelCapture
            // 
            this.panelCapture.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panelCapture.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelCapture.Controls.Add(this.lblCaptureDown);
            this.panelCapture.Controls.Add(this.lblCaptureUp);
            this.panelCapture.Location = new System.Drawing.Point(655, 606);
            this.panelCapture.Name = "panelCapture";
            this.panelCapture.Size = new System.Drawing.Size(408, 103);
            this.panelCapture.TabIndex = 15;
            this.panelCapture.Visible = false;
            // 
            // lblCaptureDown
            // 
            this.lblCaptureDown.AutoSize = true;
            this.lblCaptureDown.Font = new System.Drawing.Font("Georgia", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCaptureDown.Location = new System.Drawing.Point(10, 45);
            this.lblCaptureDown.Name = "lblCaptureDown";
            this.lblCaptureDown.Size = new System.Drawing.Size(112, 25);
            this.lblCaptureDown.TabIndex = 1;
            this.lblCaptureDown.Text = "Capturing:";
            // 
            // lblCaptureUp
            // 
            this.lblCaptureUp.AutoSize = true;
            this.lblCaptureUp.Font = new System.Drawing.Font("Georgia", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCaptureUp.Location = new System.Drawing.Point(8, 12);
            this.lblCaptureUp.Name = "lblCaptureUp";
            this.lblCaptureUp.Size = new System.Drawing.Size(112, 25);
            this.lblCaptureUp.TabIndex = 0;
            this.lblCaptureUp.Text = "Capturing:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(550, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 16);
            this.label6.TabIndex = 16;
            this.label6.Text = "Life:";
            // 
            // lblHP
            // 
            this.lblHP.AutoSize = true;
            this.lblHP.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblHP.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHP.Location = new System.Drawing.Point(531, 52);
            this.lblHP.Name = "lblHP";
            this.lblHP.Size = new System.Drawing.Size(101, 42);
            this.lblHP.TabIndex = 17;
            this.lblHP.Text = "3 HP";
            // 
            // panelRegainingComposure
            // 
            this.panelRegainingComposure.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panelRegainingComposure.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelRegainingComposure.Controls.Add(this.label9);
            this.panelRegainingComposure.Location = new System.Drawing.Point(55, 533);
            this.panelRegainingComposure.Name = "panelRegainingComposure";
            this.panelRegainingComposure.Size = new System.Drawing.Size(581, 186);
            this.panelRegainingComposure.TabIndex = 18;
            this.panelRegainingComposure.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Georgia", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(136, 74);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(331, 38);
            this.label9.TabIndex = 0;
            this.label9.Text = "Regaining composure.";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(9, 802);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(196, 144);
            this.label10.TabIndex = 20;
            this.label10.Text = "[Ctrl+Alt+\r\nA] Bonus Animals\r\nS] Bonus Fruits and Vegetables\r\nD] Bonus Bone\r\nF] B" +
    "onus Color\r\nG] Bonus Metals\r\nH] Bonus Fire\r\nJ] Bonus Big Cats\r\nK] Remove all bon" +
    "uses";
            this.label10.Visible = false;
            // 
            // lblCurrentBonus
            // 
            this.lblCurrentBonus.AutoSize = true;
            this.lblCurrentBonus.Location = new System.Drawing.Point(270, 933);
            this.lblCurrentBonus.Name = "lblCurrentBonus";
            this.lblCurrentBonus.Size = new System.Drawing.Size(97, 13);
            this.lblCurrentBonus.TabIndex = 21;
            this.lblCurrentBonus.Text = "No bonus enabled.";
            // 
            // lblNowCapturing
            // 
            this.lblNowCapturing.AutoSize = true;
            this.lblNowCapturing.Location = new System.Drawing.Point(512, 17);
            this.lblNowCapturing.Name = "lblNowCapturing";
            this.lblNowCapturing.Size = new System.Drawing.Size(115, 13);
            this.lblNowCapturing.TabIndex = 22;
            this.lblNowCapturing.Text = "Now capturing heart: 3";
            // 
            // BookwormForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1336, 964);
            this.Controls.Add(this.lblNowCapturing);
            this.Controls.Add(this.lblCurrentBonus);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.panelRegainingComposure);
            this.Controls.Add(this.lblHP);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panelCapture);
            this.Controls.Add(this.panelException);
            this.Controls.Add(this.panelGridIsDark);
            this.Controls.Add(this.panelAutomode);
            this.Controls.Add(this.lblGridLuminosity);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbDictionary);
            this.Controls.Add(this.bLoadAndParseDictionary);
            this.Controls.Add(this.lblPossibilities);
            this.Controls.Add(this.panelTraining);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BookwormForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bookworm Bot";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelTraining.ResumeLayout(false);
            this.panelTraining.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureImgData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureFullBitmap)).EndInit();
            this.panelAutomode.ResumeLayout(false);
            this.panelAutomode.PerformLayout();
            this.panelGridIsDark.ResumeLayout(false);
            this.panelGridIsDark.PerformLayout();
            this.panelException.ResumeLayout(false);
            this.panelException.PerformLayout();
            this.panelCapture.ResumeLayout(false);
            this.panelCapture.PerformLayout();
            this.panelRegainingComposure.ResumeLayout(false);
            this.panelRegainingComposure.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panelTraining;
        private System.Windows.Forms.Button bLoadLetterDB;
        private System.Windows.Forms.Button bForgetThis;
        private System.Windows.Forms.CheckBox chKnown;
        private System.Windows.Forms.Button bSaveLetter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbLetter;
        private System.Windows.Forms.PictureBox pictureImgData;
        private System.Windows.Forms.PictureBox pictureFullBitmap;
        private System.Windows.Forms.Button bFindNextUnrecognizedLetter;
        private System.Windows.Forms.ComboBox cbKnownLetters;
        private System.Windows.Forms.Button bChooseKnownLetter;
        private System.Windows.Forms.Label lblTotalUnrecognizedLetters;
        private System.Windows.Forms.Label lblTotalLetters;
        private System.Windows.Forms.CheckBox chArtifact;
        private System.Windows.Forms.Button bAgreeWithComputer;
        private System.Windows.Forms.Label lblComputerSuggestionConfidenceIndex;
        private System.Windows.Forms.Label lblWithCertainty;
        private System.Windows.Forms.Label lblComputerSuggestionLetter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label lblPossibilities;
        private System.Windows.Forms.Button bLoadAndParseDictionary;
        private System.Windows.Forms.ComboBox cbDictionary;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblGridLuminosity;
        private System.Windows.Forms.Panel panelAutomode;
        private System.Windows.Forms.Label lblPaused;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panelGridIsDark;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chSmashed;
        private System.Windows.Forms.CheckBox chEmpty;
        private System.Windows.Forms.Panel panelException;
        private System.Windows.Forms.TextBox tbException;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chStunned;
        private System.Windows.Forms.Panel panelCapture;
        private System.Windows.Forms.Label lblCaptureDown;
        private System.Windows.Forms.Label lblCaptureUp;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblHP;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chAmethyst;
        private System.Windows.Forms.CheckBox chDiamond;
        private System.Windows.Forms.CheckBox chCrystal;
        private System.Windows.Forms.CheckBox chGarnet;
        private System.Windows.Forms.CheckBox chSapphire;
        private System.Windows.Forms.CheckBox chEmerald;
        private System.Windows.Forms.CheckBox chRuby;
        private System.Windows.Forms.CheckBox chLocked;
        private System.Windows.Forms.Panel panelRegainingComposure;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button bRemoveFriend;
        private System.Windows.Forms.CheckBox chPlagued;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblCurrentBonus;
        private System.Windows.Forms.Label lblNowCapturing;
    }
}

