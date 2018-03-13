using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Bookworm.Act
{
    static class Positions
    {
        public static BookwormAdventuresPositions Bookworm = new BookwormAdventuresPositions();
        public static LetterQuestPositions1920x1080 LetterQuest { get; } = new LetterQuestPositions1920x1080();
    }

    public class LetterQuestPositions1920x1080
    {
        public Rectangle Keyboard = new Rectangle(660, 675, 600, 390);
        public List<Rectangle> Letters = new List<Rectangle>();
        public int Tilesize { get; } = 120;
        public Rectangle ImportantLetterSection = new Rectangle(10, 11, 100, 100);
        public LetterQuestPositions1920x1080()
        {
            for (int row = 0; row < 3; row++)
            {
                int y = 675 + 135 * row;
                for (int column = 0; column < 5; column++)
                {
                    int x = 660 + Tilesize * column;
                    Letters.Add(new Rectangle(x, y, Tilesize, Tilesize));
                }
            }
        }
    }

    class BookwormAdventuresPositions
    {
        public Rectangle rectAttack = new Rectangle(683, 958, 317, 71);
        public Rectangle rectScramble = new Rectangle(282, 1000, 266, 37);
        public Rectangle rectLifePotion = new Rectangle(226, 532, 66, 90);
        public Rectangle rectPowerupPotion = new Rectangle(357, 532, 66, 90);
        public Rectangle rectPurifyPotion = new Rectangle(490, 532, 66, 90);
    }
}
