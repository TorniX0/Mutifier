using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Mutifier.Frontend
{
    internal static class Wrapper
    {
        const string backendName = "Mutifier.Backend.dll";

        [DllImport(backendName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ChangeMicVolume(float volume);

        [DllImport(backendName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern float GetMicVolume();

        [DllImport(backendName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetMicMuted();

        [DllImport(backendName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SetMicMuted(bool muted);

        public static void CheckBackend()
        {
            if (!File.Exists(backendName))
            {
                MessageBox.Show("The backend dll was not found!", "Mutifier", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-1);
            }

            try
            {
                GetMicVolume();
            }
            catch (Exception ex) when (ex is BadImageFormatException || ex is EntryPointNotFoundException || ex is DllNotFoundException)
            {
                MessageBox.Show("Something is wrong with the backend file!", "Mutifier", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-1);
            }
        }
    }
}
