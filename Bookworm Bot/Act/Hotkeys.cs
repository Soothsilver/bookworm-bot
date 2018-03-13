using Bookworm.Recognize;
using ManagedWinapi;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bookworm.Act
{
    public class Hotkeys
    {
        List<Hotkey> ListOfHotkeys = new List<Hotkey>();
        Bot Bot;
        BookwormForm Form;
        
        public Hotkeys(Bot bot, BookwormForm form)
        {
            Form = form;
            Bot = bot;
        }

        public void LoadHotkeys()
        {
            AddHotkey(Keys.Left, switchTabLeft, alt: true);
            AddHotkey(Keys.Right, switchTabRight, alt: true);
            AddHotkey(Keys.R, saveUnknownsIntoDatabase, alt: true);
            AddHotkey(Keys.R, saveAllIntoDatabase, alt: true, shift: true);
            AddHotkey(Keys.L, toggleLockMode, alt: true);
            AddHotkey(Keys.Q, switchAutonomity, alt: true);
            AddHotkey(Keys.NumPad4, () => moveReticle(-1, 0));
            AddHotkey(Keys.NumPad6, () => moveReticle(1, 0));
            AddHotkey(Keys.NumPad8, () => moveReticle(0, -1));
            AddHotkey(Keys.NumPad2, () => moveReticle(0, 1));
            AddHotkey(Keys.NumPad5, rememberLockTarget);
            AddHotkey(Keys.NumPad0, rememberAndSortLockTarget);
        }

        private void rememberLockTarget()
        {
            if (Form.LockMode.Active)
            {
                if (Form.Bot.Scan.LastSnapshot != null)
                {
                    var letter = Form.Bot.Scan.LastSnapshot.Keyboard[Form.LockMode.Y * 5 + Form.LockMode.X];
                    Form.Bot.Database.AddSingle(letter);
                }
            }
        }
        private void rememberAndSortLockTarget()
        {
            if (Form.LockMode.Active)
            {
                if (Form.Bot.Scan.LastSnapshot != null && Form.Bot.Recognizator.LastRecognitionResults != null)
                {
                    var letter = Form.Bot.Scan.LastSnapshot.Keyboard[Form.LockMode.Y * 5 + Form.LockMode.X];
                    LetterSample ls = new LetterSample(letter);
                    ls.Kind = SampleKind.Known;
                    var recLetter = Form.Bot.Recognizator.LastRecognitionResults.Keyboard[Form.LockMode.Y * 5 + Form.LockMode.X];
                    if (recLetter.Letter.Length >= 2)
                    {
                        ls.Letter = recLetter.Letter.ToUpper()[1];
                        Form.Bot.Database.AddSingle(ls);
                    }
                }
            }
        }

        private void moveReticle(int x, int y)
        {
            if (Form.LockMode.Active)
            {
                Form.LockMode.X += x;
                Form.LockMode.Y += y;
                if (Form.LockMode.X < 0) Form.LockMode.X = 0;
                if (Form.LockMode.Y < 0) Form.LockMode.Y = 0;
                if (Form.LockMode.X > 4) Form.LockMode.X = 4;
                if (Form.LockMode.Y > 2) Form.LockMode.Y = 2;
            }
        }

        private void toggleLockMode()
        {
            Form.LockMode.Active = !Form.LockMode.Active;
        }

        private void saveAllIntoDatabase()
        {
            if (Bot.Scan.LastSnapshot != null)
            {
                Bot.Database.SaveAllSnapshotLettersIntoDatabase(Bot.Scan.LastSnapshot);
            }
        }

        private void saveUnknownsIntoDatabase()
        {
            if (Bot.Recognizator.LastRecognitionResults != null)
            {
                Bot.Database.SaveUnknownSnapshotLettersIntoDatabase(Bot.Recognizator.LastRecognitionResults);
            }
        }

        void switchTabLeft()
        {
            if (Form.tabControl.SelectedIndex > 0)
            {
                Form.tabControl.SelectedIndex--;
            } else
            {
                Form.tabControl.SelectedIndex = Form.tabControl.TabCount - 1;
            }
        }
        void switchTabRight()
        {
            if (Form.tabControl.SelectedIndex < Form.tabControl.TabPages.Count - 1)
            {
                Form.tabControl.SelectedIndex++;
            } 
            else
            {
                Form.tabControl.SelectedIndex = 0;
            }
        }
        public void switchAutonomity()
        {
            if (Bot.Autonomous.IsAutonomous)
            {
                Bot.Autonomous.IsAutonomous = false;
            }
            else
            {
                Bot.Autonomous.WakeUp();
                Bot.Autonomous.IsAutonomous = true;
            }
        }
        
        public void AddHotkey(
            Keys code,
            Action hotkeyAction,
            bool ctrl = false, 
            bool shift = false,
            bool alt = false)
        {
            Hotkey h = new Hotkey();
            h.KeyCode = code;
            h.Ctrl = ctrl;
            h.Shift = shift;
            h.Alt = alt;
            h.HotkeyPressed += new EventHandler((obj, e)=>
            {
                hotkeyAction();
            });
            h.Enabled = true;
            ListOfHotkeys.Add(h);
        }
    }
}
