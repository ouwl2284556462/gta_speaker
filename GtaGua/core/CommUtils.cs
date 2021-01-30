using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace GtaGua.core
{
    class CommUtils
    {
        //按下某键，并已发出WM_KEYDOWN， WM_KEYUP消息
        const int WM_CHAR = 0x102;

        [DllImport("user32.dll", EntryPoint = "SendMessageW")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        public static void sendMessage(int hwnd, String msg)
        {
            byte[] buffers = Encoding.Unicode.GetBytes(msg);
            IntPtr p = (IntPtr)hwnd;
            for (int i = 0; i < buffers.Length; i += 2)
            {
                SendMessage(p, WM_CHAR, BitConverter.ToInt16(buffers, i), 0);
            }
        }

    }
}
