using Bookworm.Act;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bookworm.Deprecated
{
    class StuffFromOldVersion
    {
        /*
         Bitmap captureBitmap = new Bitmap(LastCapture);
         List<AnalyzedImage> Positives = null;
         List<AnalyzedImage> Negatives = null;
         Rectangle ThisRectangle = Rectangle.Empty;
         for (int i = 0; i < 5; i++)
         {
             AnalyzedImage l = new AnalyzedImage();
             Color[,] imgData = GetColorDataFromBitmap(captureBitmap, ThisRectangle, 10, out l.ColorDataWidth, out l.ColorDataHeight);
             l.ColorData = imgData;
             CurrentAttackButton = l;
             int bestConfidenceWithPossible = Int32.MaxValue;
             int bestConfidenceWithImpossible = Int32.MaxValue;
             foreach (AnalyzedImage ai in Positives)
             {
                 int simin = GetSimilarityIndexRGB(l, ai);
                 if (simin < bestConfidenceWithPossible) bestConfidenceWithPossible = simin;
             }
             foreach (AnalyzedImage ai in Negatives)
             {
                 int simin = GetSimilarityIndexRGB(l, ai);
                 if (simin < bestConfidenceWithImpossible) bestConfidenceWithImpossible = simin;
             }
             bool possibility = bestConfidenceWithPossible < bestConfidenceWithImpossible;
             switch (i)
             {
                 case 0: AttackIsPossible = possibility; break;
                 case 1: ScrambleIsPossible = possibility; break;
                 case 2: LifePotionIsPossible = possibility; break;
                 case 3: PowerupPotionIsPossible = possibility; break;
                 case 4: PurifyPotionIsPossible = possibility; break;
             }
         }
         */
    
    /*
    private void ParseAttackButton(Bitmap attackBitmap)
    {
        AnalyzedImage l = new AnalyzedImage();
        Color[,] imgData = GetColorDataFromBitmap(attackBitmap, new Rectangle(0, 0, attackBitmap.Width, attackBitmap.Height),
                   10, out l.ColorDataWidth, out l.ColorDataHeight);
        l.ColorData = imgData;
        CurrentAttackButton = l;
        int bestConfidenceWithPossible = Int32.MaxValue;
        int bestConfidenceWithImpossible = Int32.MaxValue;
        foreach (AnalyzedImage ai in AttackPossibleButtons)
        {
            int simin = GetSimilarityIndexRGB(l, ai);
            if (simin < bestConfidenceWithPossible) bestConfidenceWithPossible = simin;
        }
        foreach (AnalyzedImage ai in AttackImpossibleButtons)
        {
            int simin = GetSimilarityIndexRGB(l, ai);
            if (simin < bestConfidenceWithImpossible) bestConfidenceWithImpossible = simin;
        }
        AttackIsPossible = bestConfidenceWithPossible < bestConfidenceWithImpossible;
        this.lblPossibleAttack.Text
            = AttackIsPossible ? "Attack is possible." : "Attack is NOT possible.";
        this.lblPossibleAttack.ForeColor = AttackIsPossible ? Color.Green : Color.Red;
    }
    */
    /*
    public float HP_HEALTH_POTION_LIMIT = 6;
    public int POOR_WORD_POWER_LIMIT = 20;
    public int STRONG_WORD_POWER_LIMIT = 32;
    public bool SPEEDHACK = false;
    public int AttackTriedXTimes = 0;

    private AutoPhase _aiPhase = AutoPhase.GoSearchWord;
    public AutoPhase AIPhase
    {
        get { return _aiPhase; }
        set
        {
            PhaseOrder.Insert(0, value);
            _aiPhase = value;
        }
    }
    public DateTime NextPhaseBeginsAtSoonest = DateTime.Now;
    public List<Letter> AvailableLetters = new List<Letter>();
    public List<AutoPhase> PhaseOrder = new List<AutoPhase>();
    public int IndexOfFirstLetter;
    public int CurrentlyAddingLetter;
    public int WordNotFoundXTimes = 0;
    public bool RegainingComposure = false;
    private bool PowerUpGiven;
    public int PoorWordGivenXTimes = 0;
    public void ExecuteAutonomous()
    {

        try
        {
            if (DateTime.Now > NextPhaseBeginsAtSoonest)
            {
                RegainingComposure = false;
                if (AIPhase == AutoPhase.GoSearchWord)
                {
                    // Click on dark stuff
                    if (GridLuminosity < 900000)
                    {
                        LeftClick(840, 525);
                        return;
                    }
                    if (CurrentLifeTotal <= HP_HEALTH_POTION_LIMIT && LifePotionIsPossible)
                    {
                        AIPhase = AutoPhase.GoClickHealthPotion;
                        return;
                    }
                    LastRecognizedKeyboard = Keyboard;
                    RecognizeLetters(LastRecognizedKeyboard, false, false);
                    foreach (Letter l in LastRecognizedKeyboard)
                    {
                        if (l.IsStunned)
                        {
                            if (PurifyPotionIsPossible)
                            {
                                LeftClickMiddleOfRectangle(rectPurifyPotion);
                                AIPhase = AutoPhase.GoSearchWord;
                                NextPhaseBeginsAtSoonest = DateTime.Now.AddSeconds(2);

                            }
                            else
                            {
                                Rectangle rectGrid = new Rectangle(667, 541, 350, 356);
                                LeftClick(rectGrid.X + rectGrid.Width / 2, rectGrid.Y + rectGrid.Height / 2);
                                AIPhase = AutoPhase.GoSearchWord;
                                NextPhaseBeginsAtSoonest = DateTime.Now.AddSeconds(2);
                            }
                            return;
                        }
                    }

                    FormBestWord();
                    IndexOfFirstLetter = -1;
                    AvailableLetters.Clear();
                    foreach (Letter l in LastRecognizedKeyboard)
                    {
                        AvailableLetters.Add(l);
                    }
                    AIPhase = AutoPhase.GoClickFirstLetter;
                    if (BestWord == null)
                    {
                        AIPhase = AutoPhase.GoSearchWord;
                        WordNotFoundXTimes++;
                        RightClickMiddle();
                        NextPhaseBeginsAtSoonest = DateTime.Now.AddSeconds(1);
                        if (WordNotFoundXTimes >= 3)
                        {
                            if (ScrambleIsPossible)
                            {
                                LeftClickMiddleOfRectangle(rectScramble);
                                AIPhase = AutoPhase.GoSearchWord;
                                WordNotFoundXTimes = 0;
                                NextPhaseBeginsAtSoonest = DateTime.Now.AddSeconds(2);
                            }
                        }
                    }
                }
                else if (AIPhase == AutoPhase.GoClickFirstLetter)
                {
                    // Click on dark stuff
                    if (GridLuminosity < 900000)
                    {
                        LeftClick(840, 525);
                        return;
                    }
                    Word w = BestWord;
                    char cFirst = w.String[0];
                    int index = AvailableLetters.FindIndex(new Predicate<Letter>((l) => { return l.InnerLetter == cFirst && l.Known; }));
                    IndexOfFirstLetter = index;
                    CurrentlyAddingLetter = 0;
                    AvailableLetters.RemoveAt(index);
                    LeftClickOnLetter(index);
                    AIPhase = AutoPhase.GoClickRestOfWord;
                    NextPhaseBeginsAtSoonest = DateTime.Now.AddSeconds(0.4f);
                }
                else if (AIPhase == AutoPhase.GoScramble)
                {
                    LeftClickMiddleOfRectangle(rectScramble);
                    AIPhase = AutoPhase.GoClickFirstLetter;
                    PoorWordGivenXTimes = 0;
                    NextPhaseBeginsAtSoonest = DateTime.Now.AddSeconds(2);
                }
                else if (AIPhase == AutoPhase.GoClickHealthPotion)
                {
                    LeftClickMiddleOfRectangle(rectLifePotion);
                    AIPhase = AutoPhase.GoSearchWord;
                    NextPhaseBeginsAtSoonest = DateTime.Now.AddSeconds(0.4f);
                }
                else if (AIPhase == AutoPhase.GoClickPowerUp)
                {
                    LeftClickMiddleOfRectangle(rectPowerupPotion);
                    PowerUpGiven = true;
                    AIPhase = AutoPhase.GoClickRestOfWord;
                    NextPhaseBeginsAtSoonest = DateTime.Now.AddSeconds(0.4f);
                }
                else if (AIPhase == AutoPhase.GoClickRestOfWord)
                {
                    Word w = BestWord;
                    if (CurrentlyAddingLetter == 0)
                    {
                        // Click on dark stuff
                        if (GridLuminosity < 900000)
                        {
                            RightClickMiddle();
                            NextPhaseBeginsAtSoonest = DateTime.Now.AddSeconds(0.4);
                            AIPhase = AutoPhase.GoSearchWord;
                            return;
                        }

                        LastRecognizedKeyboard = Keyboard;
                        RecognizeLetters(Keyboard, false, false);



                        if (!LastRecognizedKeyboard[IndexOfFirstLetter].IsEmpty && !LastRecognizedKeyboard[IndexOfFirstLetter].ClosestFriend.IsEmpty)
                        {
                            RightClickMiddle();
                            NextPhaseBeginsAtSoonest = DateTime.Now.AddSeconds(0.4);

                            AIPhase = AutoPhase.GoSearchWord;
                        }
                        bool recognizedOne = false;
                        foreach (Letter l in LastRecognizedKeyboard)
                        {
                            if (l.Known) { recognizedOne = true; break; }
                        }
                        if (!recognizedOne)
                        {
                            AIPhase = AutoPhase.GoSearchWord;
                            RightClickMiddle();
                            NextPhaseBeginsAtSoonest = DateTime.Now.AddSeconds(0.4);

                        }
                    }
                    while (AIPhase == AutoPhase.GoClickRestOfWord)
                    {

                        CurrentlyAddingLetter++;
                        if (CurrentlyAddingLetter == 1 && !PowerUpGiven && PowerupPotionIsPossible && w.Power >= STRONG_WORD_POWER_LIMIT)
                        {
                            AIPhase = AutoPhase.GoClickPowerUp;
                            CurrentlyAddingLetter--;
                            return;
                        }
                        if (CurrentlyAddingLetter == 1)
                        {
                            foreach (Letter l in AvailableLetters)
                            {
                                if (l.ConfidenceIndex > CONFIDENCE_THRESHOLD)
                                {
                                    LetterDB.Add(l);
                                }
                            }
                        }
                        int index = AvailableLetters.FindIndex(new Predicate<Letter>((l) => { return l.InnerLetter == w.String[CurrentlyAddingLetter] && l.Known; }));

                        LeftClickOnLetter(AvailableLetters[index].PositionInGrid);
                        AvailableLetters.RemoveAt(index);
                        AIPhase = AutoPhase.GoClickRestOfWord;
                        if (CurrentlyAddingLetter == w.String.Length - 1)
                        {
                            NextPhaseBeginsAtSoonest = DateTime.Now.AddSeconds(0.4f);
                            AIPhase = AutoPhase.GoPressEnter;
                            AttackTriedXTimes = 0;
                        }
                        if (!SPEEDHACK) break;
                    }
                }
                else if (AIPhase == AutoPhase.GoPressEnter)
                {
                    if (AttackIsPossible)
                    {
                        LeftClick(rectAttackButton.X + rectAttackButton.Width / 2, rectAttackButton.Y + rectAttackButton.Height / 2);
                        AIPhase = AutoPhase.GoSearchWord;
                        NextPhaseBeginsAtSoonest = DateTime.Now.AddSeconds(2);
                        WordNotFoundXTimes = 0;
                        if (BestWord.Power <= POOR_WORD_POWER_LIMIT)
                        {
                            PoorWordGivenXTimes++;
                            if (PoorWordGivenXTimes >= 3) AIPhase = AutoPhase.GoScramble;
                        }
                        else PoorWordGivenXTimes = 0;
                        PowerUpGiven = false;
                    }
                    else
                    {
                        AttackTriedXTimes++;
                        if (AttackTriedXTimes >= 3)
                        {
                            RegainComposure();
                            return;
                        }
                        NextPhaseBeginsAtSoonest = DateTime.Now.AddSeconds(2);
                    }
                }
            }
        }
        catch (Exception e)
        {
            RegainComposure();
            this.tbException.Text = e.ToString() + Environment.NewLine + Environment.NewLine + "Call stack: " + Environment.NewLine + e.StackTrace;
            this.panelException.Visible = true;
        }
    }

    public void RegainComposure()
    {
        RightClickMiddle();
        AIPhase = AutoPhase.GoSearchWord;
        BestWord = null;
        IndexOfFirstLetter = 0;
        CurrentlyAddingLetter = 0;
        AttackTriedXTimes = 0;
        WordNotFoundXTimes = 0;
        NextPhaseBeginsAtSoonest = DateTime.Now.AddSeconds(6);
        RegainingComposure = true;
    }

    private void RightClickMiddle()
    {
        RightClick(1680 / 2, 1050 / 2);
    }

    private void LeftClickMiddleOfRectangle(Rectangle rect)
    {
        LeftClick(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
    }

    private void LeftClickOnLetter(int index)
    {
        Rectangle rect = TilePositions[index];
        LeftClick(rect.X + 44, rect.Y + 44);
    }*/
}
}
