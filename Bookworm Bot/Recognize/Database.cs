using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bookworm.Recognize
{
    [Serializable]
    public class Database : List<Sample>
    {
        public bool FirstTimeLaunch { get; set; }

        public Database(bool firstTimeLaunch)
        {
            this.FirstTimeLaunch = firstTimeLaunch;
        }
    }
}
