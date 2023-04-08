//using MessageBox.Avalonia.Enums;
using System;
using System.Windows;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Mutifier.Frontend
{
    internal static partial class Wrapper
    {
        /* Backend dll name */
        const string backendName = "Mutifier.Backend.dll";

        [LibraryImport(backendName)]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
        internal static partial void ChangeMicVolume(float volume);

        [LibraryImport(backendName)]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
        internal static partial float GetMicVolume();

        [LibraryImport(backendName)]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool IsMicMuted();

        [LibraryImport(backendName)]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
        internal static partial void SetMicMuted([MarshalAs(UnmanagedType.Bool)] bool muted);

        /// <summary>
        /// Self-explanatory; checks if the backend dll is present and if it's corrupted/invalid
        /// </summary>
        public static void CheckBackend()
        {
            if (!File.Exists(backendName))
            {
                DialogBox.Show("The backend dll was not found!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-1);
            }

            try
            {
                GetMicVolume();
            }
            catch (Exception ex) when (ex is BadImageFormatException || ex is EntryPointNotFoundException || ex is DllNotFoundException)
            {
                DialogBox.Show("Something is wrong with the backend file!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-1);
            }
        }
    }
}
