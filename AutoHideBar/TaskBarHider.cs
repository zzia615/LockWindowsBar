using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace AutoHideBar
{
    public class TaskBarHider
    {
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
        public struct APPBARDATA
        {
            public int cbSize;
            public int hwnd;
            public int uCallbackMessage;
            public int uEdge;
            public RECT rc;
            public int lParam;
        }
        public enum AppBarStates
        {
            AutoHide = 0x001,
            AlwaysOnTop = 0x002
        }
        public const int ABS_ALWAYSONTOP = 0x002;
        public const int ABS_AUTOHIDE = 0x001;
        public const int ABM_SETSTATE = 0x00A;
        public const int ABM_GETSTATE = 0x004;

        [DllImport("shell32.dll")]
        public static extern int SHAppBarMessage(int dwmsg, ref APPBARDATA app);
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern int FindWindow(string lpClassName, string lpWindowName);

        public static void SetAppBarAutoDisplay(bool IsAuto)
        {
            APPBARDATA abd = new APPBARDATA();
            abd.hwnd = FindWindow("Shell_TrayWnd", "");
            if (IsAuto)
            {
                abd.lParam = ABS_AUTOHIDE;
                SHAppBarMessage(ABM_SETSTATE, ref abd);
            }
            else
            {
                abd.lParam = ABS_ALWAYSONTOP;
                SHAppBarMessage(ABM_SETSTATE, ref abd);
            }
        }

        public static AppBarStates GetAppBarState()
        {
            APPBARDATA abd = new APPBARDATA();
            abd.hwnd = FindWindow("Shell_TrayWnd", "");
            return (AppBarStates)SHAppBarMessage(ABM_GETSTATE, ref abd);
        }
    }
}
