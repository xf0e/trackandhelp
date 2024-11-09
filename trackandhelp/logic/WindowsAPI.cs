using System;
using System.Text;
using trackandhelp.logic.ActiveWindow;
using System.Runtime.InteropServices;

using trackandhelp.common;
using System.Diagnostics;

namespace trackandhelp.logic
{
    public static class WindowAPI
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString,
    int nMaxCount);

        public static Option<ActiveWindowModel> GetActiveWindowTitle()
        {
            const int nChars = 256;
            //byte[] Buff = new byte[nChars];    
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                //Debug.WriteLine(Buff);
                return Option<ActiveWindowModel>.Some(ActiveWindowModel.Create(handle, Buff.ToString()));
            }

            return Option<ActiveWindowModel>.None();
        }
    }
}
