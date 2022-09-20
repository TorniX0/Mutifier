using System.Windows.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Reflection.Metadata.Ecma335;
using System.Reflection;
using System.Security.Policy;
using System.Net.Http.Headers;

namespace Mutifier.Frontend
{
    internal static class Utilities
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern short GetKeyState(int keyCode);

        public static void ValidateFiles()
        {
            Wrapper.CheckBackend();
            Sounds.ValidateSounds();
            //if ()
            //if ()
        }

        public static string GetCurrentPath() => AppDomain.CurrentDomain.BaseDirectory;
    }
}
