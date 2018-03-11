using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Bookworm.Scan
{
    class Hooks
    {
        public static int HookCalledNum = 0;
        public static IntPtr _hookID = IntPtr.Zero;
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static LowLevelKeyboardProc _proc2 = _proc;
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        
        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }
        public static bool Num6Down = false;
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            HookCalledNum++;
            /*
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                if ((Keys)vkCode == Keys.LShiftKey)
                {
                    FormInstance.HookOrder = HookOrderType.PauseAutomode;
                }
                if ((Keys)vkCode == Keys.NumPad6)
                {
                    Num6Down = true;
                }
            } 
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYUP)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                if ((Keys)vkCode == Keys.LShiftKey)
                {
                    FormInstance.HookOrder = HookOrderType.ResumeAutomode;
                }
                if ((Keys)vkCode == Keys.NumPad6)
                {
                    if (Num6Down)
                    {
                        FormInstance.HookOrder = HookOrderType.ScanGrid;
                        Num6Down = false;
                    }
                }
                if ((Keys)vkCode == Keys.CapsLock)
                {
                    FormInstance.HookOrder = HookOrderType.ToggleAutomode;
                }
                if ((Keys)vkCode == Keys.Scroll)
                {
                    FormInstance.HookOrder = HookOrderType.ScanGridAndPutAllInDB;
                }
                if ((Keys)vkCode == Keys.W)
                {
                    FormInstance.HookOrder = HookOrderType.ScanGrid;
                }
                if ((Keys)vkCode == Keys.N)
                {
                    FormInstance.HookOrder = HookOrderType.FindBestWord;
                } 
                if ((Keys)vkCode == Keys.M)
                {
                    FormInstance.HookOrder = HookOrderType.NextBestWord;
                }
                if ((Keys)vkCode == Keys.NumLock)
                {
                    FormInstance.HookOrder = HookOrderType.TrustAllInThisGrud;
                }
                if ((Keys)vkCode == Keys.Up)
                {
                    FormInstance.bGrabNewPossibleAttack_Click(null, EventArgs.Empty);
                }
                if ((Keys)vkCode == Keys.Down)
                {
                    FormInstance.bGrabImpossibleAttack_Click(null, EventArgs.Empty);
                }
            }
            */
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }


        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }
    public enum HookOrderType
    {
        Null,
        ScanGrid,
        StartAutomode,
        StopAutomode,
        PauseAutomode,
        ResumeAutomode,
        ScanGridAndPutAllInDB,
        TrustAllInThisGrud,
        ToggleAutomode,
        FindBestWord,
        NextBestWord
    }
}
