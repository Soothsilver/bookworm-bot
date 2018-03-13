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
        public int TicksUntilWakeUp = 50;
        public int MaximumUnrecognizedLettersToContinuePlaying = 2;

        public void WakeUp()
        {
            TicksUntilWakeUp = 15;
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


        public void Tick()
        {
            if (!IsAutonomous)
            {
                Status = "Turned off";
                return;
            }
            switch (State)
            {
                case AutonomousState.WakingUp:
                    TicksUntilWakeUp--;
                    Status = "Waking up... " + TicksUntilWakeUp + " ticks remaining";
                    if (TicksUntilWakeUp <= 0)
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
                            if (LastBackspace.AddSeconds(1) < DateTime.Now)
                            {
                                LastBackspace = DateTime.Now;
                                bot.Injection.PressButton(ByteKey.VK_BACK);
                            }
                            return;
                        }
                        Word word = bot.Vocabulary.LastScrabbleResult.BestWord;
                        if (word != null)
                        {
                            AfterPauseState = AutonomousState.ConstructingWord;
                            State = AutonomousState.WakingUp;
                            TicksUntilWakeUp = 10;
                            Status = "Constructing " + word.Text + "!";
                            ConstructingWord = word.Text.ToList();
                        }
                        else
                        {
                            Status = "No word found.";
                        }                    
                    }                    
                    return;
                case AutonomousState.ConstructingWord:
                    if (ConstructingWord.Count == 0)
                    {
                        TicksUntilWakeUp = 30;
                        State = AutonomousState.WakingUp;
                        AfterPauseState = AutonomousState.FireTheWord;
                        return;
                    }
                    char nowchar = ConstructingWord[0];
                    ConstructingWord.RemoveAt(0);
                    Status = "Pressing " + nowchar + "!";
                    bot.Injection.PressChar(nowchar);
                    AfterPauseState = AutonomousState.ConstructingWord;
                    TicksUntilWakeUp = 5;
                    State = AutonomousState.WakingUp;
                    return;
                case AutonomousState.FireTheWord:
                    Status = "Pressing Enter!";
                    bot.Injection.PressButton(ByteKey.VK_RETURN);
                    AfterPauseState = AutonomousState.ReadingLetters;
                    TicksUntilWakeUp = 5;
                    State = AutonomousState.WakingUp;
                    return;
            }

        }
    }
}
