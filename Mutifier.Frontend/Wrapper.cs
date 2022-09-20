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


        /*
        [DllImport(backendName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void ChangeMicVolumeA(float volume);

        [DllImport(backendName, CallingConvention = CallingConvention.Cdecl)]
        private static extern float GetMicVolumeA();

        [DllImport(backendName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void SetMicMutedA(bool muted);

        
        public static void ChangeMicVolume(float volume)
        {
            try
            {
                ChangeMicVolumeA(volume);
            }
            catch
            {
                MessageBox.Show("An error occured with the backend!", "Mutifier", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static float GetMicVolume()
        {
            try
            {
                return GetMicVolumeA();
            }
            catch
            {
                MessageBox.Show("An error occured with the backend!", "Mutifier", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        public static void SetMicMuted(bool muted)
        {
            try
            {
                SetMicMutedA(muted);
            }
            catch
            {
                MessageBox.Show("An error occured with the backend!", "Mutifier", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        */

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
