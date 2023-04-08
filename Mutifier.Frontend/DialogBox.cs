using System.Diagnostics;
using System.Windows.Forms;

namespace Mutifier.Frontend
{

    /// <summary>
    /// MessageBox.Avalonia extension
    /// </summary>
    public static class DialogBox
    {
        /*
         * WONTFIX: Avalonia's MessageBox is broken;
         * 
        private static IMsBoxWindow<ButtonResult> GetMessageBoxStandardWindow(MessageBoxStandardParams @params)
        {
            MsBoxStandardWindow msBoxStandardWindow = new();
            msBoxStandardWindow.DataContext = new MsBoxStandardViewModel(@params, msBoxStandardWindow);
            msBoxStandardWindow.ShowInTaskbar = true;
            return new MsBoxWindowBase<MsBoxStandardWindow, ButtonResult>(msBoxStandardWindow);
        }

        private static IMsBoxWindow<ButtonResult> GetMessageBoxCustomWindow(string text, ButtonEnum @enum = ButtonEnum.Ok, Icon icon = Icon.None, WindowStartupLocation windowStartupLocation = WindowStartupLocation.CenterScreen)
        {
            WindowIcon windowIcon = new(AvaloniaLocator.Current?.GetService<IAssetLoader>()?
                .Open(new Uri($"avares://{Assembly.GetExecutingAssembly().GetName().Name}/assets/icon.ico")));

            return GetMessageBoxStandardWindow(new MessageBoxStandardParams
            {
                ContentTitle = "Mutifier",
                CanResize = false,
                ContentMessage = text,
                ButtonDefinitions = @enum,
                Icon = icon,
                WindowIcon = windowIcon,
                WindowStartupLocation = windowStartupLocation
            });
        }

        public static ButtonResult Show(string text, ButtonEnum @enum = ButtonEnum.Ok, Icon icon = Icon.None, WindowStartupLocation windowStartupLocation = WindowStartupLocation.CenterScreen)
        {
            using CancellationTokenSource source = new();
            var window = GetMessageBoxCustomWindow(text, @enum, icon, windowStartupLocation);
            var task = window.Show().ContinueWith((t) =>
            {
                source.Cancel();
                return t;
            }, TaskScheduler.FromCurrentSynchronizationContext());
            Dispatcher.UIThread.MainLoop(source.Token);

            return task.Result.Result;
        }
        */

        public static DialogResult Show(string text, MessageBoxButtons buttons, MessageBoxIcon icon)
            => MessageBox.Show(text, "Mutifier", buttons, icon);
    }
}
