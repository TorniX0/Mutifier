using System.Diagnostics;
using System.Reflection;

namespace Mutifier.Frontend
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                MessageBox.Show("Mutifier is already running. Only one instance of this application is allowed.", "Mutifier", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Environment.Exit(-1);
                return;
            }

            Utilities.ValidateFiles();

#if !DEBUG
             Update.CheckForUpdates();
#endif

            Application.Run(new MainWindow());
        }
    }
}