using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bookworm.Act
{
    public enum AutoPhase
    {
        GoSearchWord,
        GoClickFirstLetter,
        GoClickRestOfWord,
        GoPressEnter,
        GoWaitForEnemy,
        GoClickPowerUp,
        GoClickHealthPotion,
        GoScramble
    }
}
