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
        public static void ValidateFiles()
        {
            Wrapper.CheckBackend();
            Sounds.ValidateSounds();
        }

        public static string GetCurrentPath() => AppDomain.CurrentDomain.BaseDirectory;
    }
}
