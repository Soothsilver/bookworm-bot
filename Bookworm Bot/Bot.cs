﻿using Bookworm.Act;
using Bookworm.Recognize;
using Bookworm.Scan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bookworm
{
    public class Bot
    {
        public Autonomous Autonomous { get; } = new Autonomous();
        public Scanning Scan { get; }
        public GameStyle Style { get; } = GameStyle.LetterQuest;
        public DatabaseManager Database { get; set; } = new DatabaseManager();
        public Recognizator Recognizator { get; set; }

        public Bot()
        {
            Scan = new Scanning(this);
            Recognizator = new Recognizator(this);
        }
    }
}
