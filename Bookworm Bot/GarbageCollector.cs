using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Bookworm
{
    class GarbageCollector
    {
        public static void StartCollectingInBackground()
        {
            Timer garbageCollectingTimer = new Timer();
            garbageCollectingTimer.Elapsed += CollectGarbageEvent;
            garbageCollectingTimer.Interval = 5000;
            garbageCollectingTimer.Enabled = true;
        }

        private static void CollectGarbageEvent(object sender, ElapsedEventArgs e)
        {
            GC.Collect();
        }
    }
}
