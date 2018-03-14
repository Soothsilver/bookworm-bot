using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bookworm.Scan
{
    [Serializable]
    public enum Screenpart
    {
        ATTACK_POSSIBLE,
        ATTACK_DISABLED,
        TREASURE_CHEST_TOP_ROW,
        THREE_BUTTON_NEXT,
        FOUR_BUTTON_NEXT
    }
}
