using ClassesModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Variables;

namespace BotModule
{
    public class Buttons
    {
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        public static void CheckForMove(MessageClass msg)
        {
            if (msg.message[0] == '!')
            {
                if (msg.message.Contains("!стопмув"))
                {
                    SendKeys.SendWait("{" + ValuesProject.StopMove + "}");
                }
                if (msg.message.Contains("!й"))
                {
                    SendKeys.SendWait("{" + ValuesProject.SpellOne + "}");
                }
            }
        }
        //-fullscreen




    }
}
