using Bookworm.Recognize;
using Bookworm.Scrabble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bookworm.Act
{
    public class Autonomous
    {
        private readonly Bot bot;
        private AutonomousState _state;
        public AutonomousState State
        {
            get
            {
                return _state;
            }
            set
            {
                LastStateChange = DateTime.Now;
                LastBackspace = DateTime.Now;
                _state = value;
            }
        }
        public AutonomousState AfterPauseState;
        public int MaximumUnrecognizedLettersToContinuePlaying = 11;
        public TimeSpan TimeUntilAction = TimeSpan.Zero;

        public void WakeUp()
        {
            TimeUntilAction = TimeSpan.FromMilliseconds(500);
            State = AutonomousState.WakingUp;
            AfterPauseState = AutonomousState.ReadingLetters;
        }

        public Autonomous(Bot bot)
        {
            this.bot = bot;
        }
        public bool IsAutonomous { get; set; } = false;
        public string Status { get; private set; } = "Not activated";
        public List<char> ConstructingWord;
        private DateTime LastStateChange = DateTime.Now;
        private DateTime LastBackspace = DateTime.Now;
        private DateTime LastTick = DateTime.Now;
        private static double DELAY_AFTER_ATTACKING = 50;
        private static double DELAY_BEFORE_NEXT_LETTER = 50;
        private static double DELAY_BEFORE_ATTACKING = 200;
        private static double DELAY_BEFORE_SPELLING = 50;
        private static double DELAY_AFTER_TREASURE_ROW_COMPLETE = 1000;

        public void Tick()
        {
            TimeSpan elapsedTime = DateTime.Now - LastTick;
            LastTick = DateTime.Now;
            if (!IsAutonomous)
            {
                Status = "Turned off";
                return;
            }
            switch (State)
            {
                case AutonomousState.WakingUp:
                    TimeUntilAction -= elapsedTime;
                    Status = "Will: (" + AfterPauseState + ") " + (int)TimeUntilAction.TotalMilliseconds + " milliseconds remaining";
                    if (TimeUntilAction < TimeSpan.Zero)
                    {
                        State = AfterPauseState;
                    }
                    return;
                case AutonomousState.ReadingLetters:
                    if (bot.Vocabulary.LastScrabbleResult != null)
                    {
                        if (bot.Recognizator.LastRecognitionResults.Keyboard.Count(rl => rl.IsRecognized) < 15 - MaximumUnrecognizedLettersToContinuePlaying)
                        {
                            Status = "Too few letters recognized.";
                            if (LastBackspace.AddSeconds(0.5) < DateTime.Now)
                            {
                                LastBackspace = DateTime.Now;
                                bot.Injection.PressButton(ByteKey.VK_BACK);
                            }
                            AttemptToRecognizeStartSituation();
                            return;
                        }
                        Word word = bot.Vocabulary.LastScrabbleResult.BestWord;
                        if (word != null)
                        {
                            AfterPauseState = AutonomousState.ConstructingWord;
                            State = AutonomousState.WakingUp;
                            TimeUntilAction = TimeSpan.FromMilliseconds(DELAY_BEFORE_SPELLING);
                            Status = "Constructing " + word.Text + "!";
                            ConstructingWord = word.Text.ToList();
                        }
                        else
                        {
                            Status = "No word found.";
                        }
                    }
                    AttemptToRecognizeStartSituation();
                    return;
                case AutonomousState.ConstructingWord:
                    if (ConstructingWord.Count == 0)
                    {
                        TimeUntilAction = TimeSpan.FromMilliseconds(DELAY_BEFORE_ATTACKING);
                        State = AutonomousState.WakingUp;
                        AfterPauseState = AutonomousState.FireTheWord;
                        return;
                    }
                    char nowchar = ConstructingWord[0];
                    ConstructingWord.RemoveAt(0);
                    Status = "Pressing " + nowchar + "!";
                    bot.Injection.PressChar(nowchar);
                    AfterPauseState = AutonomousState.ConstructingWord;
                    TimeUntilAction = TimeSpan.FromMilliseconds(DELAY_BEFORE_NEXT_LETTER);
                    State = AutonomousState.WakingUp;
                    return;
                case AutonomousState.FireTheWord:
                    Status = "Pressing Enter!";
                    bot.Injection.PressButton(ByteKey.VK_RETURN);
                    AfterPauseState = AutonomousState.ReadingLetters;
                    TimeUntilAction = TimeSpan.FromMilliseconds(DELAY_AFTER_ATTACKING);
                    State = AutonomousState.WakingUp;
                    return;
            }

        }

        public string SpecialSituationDescription = "";
        private void AttemptToRecognizeStartSituation()
        {
            SpecialSituationDescription = "";
            List<ScreenpartSample> scrSamples = new List<ScreenpartSample>();
            lock (bot.Database.DatabaseLock)
            {
                foreach (var smpl in bot.Database.Database.OfType<ScreenpartSample>())
                {
                    if (!smpl.Scanned)
                    {
                        smpl.ColorData = bot.PresageAndRecognize.SimplifyOtherBitmap(smpl.Bitmap, new System.Drawing.Rectangle(0, 0, smpl.Bitmap.Width, smpl.Bitmap.Height));
                        smpl.Scanned = true;
                    }
                    scrSamples.Add(smpl);
                }
            }

            int THRESHOLD = 1000;
            var snap = bot.Scan.LastSnapshot;
            int closest = Int32.MaxValue;
            ScreenpartSample closestS = null;
            foreach (var scrSample in scrSamples)
            {
                var compareData = snap.ColorDataScreenparts[scrSample.Screenpart];
                int dist = (bot.PresageAndRecognize.GetDistance(scrSample.ColorData, compareData));
                if (dist < closest)
                {
                    closest = dist;
                    closestS = scrSample;
                }
            }
            SpecialSituationDescription = "Recognized screenpart: " + closestS.Screenpart + " (distance " + closest + ")";
            if (closest < THRESHOLD)
            {
                switch(closestS.Screenpart)
                {
                    case Scan.Screenpart.TREASURE_CHEST_TOP_ROW:
                        for (char i = 'A'; i <= 'Z'; i++)
                        {
                            bot.Injection.PressChar(i);
                        }

                        AfterPauseState = AutonomousState.ReadingLetters;
                        State = AutonomousState.WakingUp;
                        TimeUntilAction = TimeSpan.FromMilliseconds(DELAY_AFTER_TREASURE_ROW_COMPLETE);
                        break;
                }
            }
            
        }
    }
}
