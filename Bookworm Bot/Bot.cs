using Bookworm.Act;
using Bookworm.Recognize;
using Bookworm.Scan;
using Bookworm.Scrabble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bookworm
{
    public class Bot
    {
        public PresageAndRecognize PresageAndRecognize { get; } = new PresageAndRecognize();
        public Autonomous Autonomous { get; }
        public Scanning Scan { get; }
        public GameStyle Style { get; } = GameStyle.LetterQuest;
        public DatabaseManager Database { get; } = new DatabaseManager();
        public Recognizator Recognizator { get; }
        public Injection Injection { get; } = new Injection();
        public VocabularyManager Vocabulary { get; } = new VocabularyManager();

        public Bot()
        {
            Scan = new Scanning(this);
            Recognizator = new Recognizator(this);
            Autonomous = new Autonomous(this);
        }
    }
}
