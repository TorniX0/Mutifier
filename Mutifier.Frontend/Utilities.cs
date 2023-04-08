using System;

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
