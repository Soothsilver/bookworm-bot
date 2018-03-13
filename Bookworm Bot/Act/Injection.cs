using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bookworm.Act
{
    public class Injection
    {
        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int
         dwData,
         int dwExtraInfo);

        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        public void LeftClick(int x, int y)
        {

            Cursor.Position = new Point(x, y);
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }
        public void RightClick(int x, int y)
        {

            Cursor.Position = new Point(x, y);
            mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
        }

        void PressKey(byte keyCode)
        {
            const int KEYEVENTF_KEYDOWN = 0x0;
            const int KEYEVENTF_KEYUP = 0x2;
            keybd_event(keyCode, 0, KEYEVENTF_KEYDOWN, 0);
            keybd_event(keyCode, 0, KEYEVENTF_KEYUP, 0);
        }
        public void PressChar(char c)
        {
            byte b = (Convert.ToByte(c));
            PressKey(b);
        }

        public void PressButton(ByteKey key)
        {
            byte b = (byte)key;
            PressKey(b);
        }
        const int MOUSEEVENTF_MOVE = 0x00000001;
        const int MOUSEEVENTF_LEFTDOWN = 0x00000002;
        const int MOUSEEVENTF_LEFTUP = 0x00000004;
        const int MOUSEEVENTF_RIGHTDOWN = 0x00000008;
        const int MOUSEEVENTF_RIGHTUP = 0x00000010;
        const int MOUSEEVENTF_MIDDLEDOWN = 0x00000020;
        const int MOUSEEVENTF_MIDDLEUP = 0x00000040;
        const int MOUSEEVENTF_WHEEL = 0x00000800;
        const int MOUSEEVENTF_ABSOLUTE = 0x00008000;
    }
}
