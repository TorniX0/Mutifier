using Avalonia.Controls;
using System.Diagnostics;
using System;
using Avalonia.Threading;
using Avalonia.Interactivity;
using System.ComponentModel;
using System.Windows.Forms;

namespace Mutifier.Frontend
{
    public partial class MainWindow : Window
    {
        bool mutedMic = false;

        HotkeySystem hotkeySystem;
        DispatcherTimer mutedBeep;

        public MainWindow()
        {
            if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                DialogBox.Show("Mutifier is already running. Only one instance of this application is allowed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-1);
                return;
            }

            Utilities.ValidateFiles();
            Update.CheckForUpdates();

            mutedBeep = new();
            mutedBeep.Tick += mutedBeep_Tick;
            mutedBeep.Interval = TimeSpan.FromSeconds(4);

            hotkeySystem = new(this.PlatformImpl.Handle.Handle);
            hotkeySystem.RegisterHotkey(VirtualKeys.Home, () => ToggleMicrophone());

            InitializeComponent();

            if (Wrapper.IsMicMuted())
            {
                ToggleMicrophone(false);
            }

            UpdateControls();
        }

        /*
        
        TODO (maybe): Tray minimization

        *
        * C# (MainWindow.axaml.cs)
        * 

        protected override void HandleWindowStateChanged(WindowState state)
        {
            WindowState = state;

            if (state == WindowState.Minimized)
            {
                Renderer.Stop();
                this.Hide();
            }
            else
            {
                Renderer.Start();
                this.Show();
            }
        }

        *
        * XAML (App.axaml)
        *
        
        <TrayIcon.Icons>
		    <TrayIcons>
			    <TrayIcon Icon="/assets/icon.ico" ToolTipText="Mutifier">
				    <TrayIcon.Menu>
					    <NativeMenu>
						    <NativeMenuItem Header="Exit"/>
					    </NativeMenu>
				    </TrayIcon.Menu>
			    </TrayIcon>
		    </TrayIcons>
	    </TrayIcon.Icons>

        */

        private void ToggleMicrophone(bool playSound = true)
        {
            mutedMic = !mutedMic;
            Wrapper.SetMicMuted(mutedMic);

            if (playSound)
            {
                SoundType sound = mutedMic ? SoundType.Muted : SoundType.Unmuted;
                Sounds.PlaySound(sound);
            }

            ToggleBeep(mutedMic);

            UpdateControls();
        }

        private void MuteButton(object sender, RoutedEventArgs args)
        {
            ToggleMicrophone();
        }

        private void OpenRepoLink(object sender, RoutedEventArgs args) 
        => Process.Start(new ProcessStartInfo()
        {
            FileName = "https://github.com/TorniX0/Mutifier",
            UseShellExecute = true
        });

        private void ToggleBeep(bool muted)
        {
            if (muted)
            {
                mutedBeep.Start();
            }
            else
            {
                mutedBeep.Stop();
            }
        }

        private void mutedBeep_Tick(object? sender, EventArgs? e)
        {
            if (beepCheckBox.IsChecked != true)
                return;

            Sounds.PlaySound(SoundType.Beeps);
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            hotkeySystem.Dispose();
        }

        private void ChangeKeybind(VirtualKeys key)
        {
            hotkeySystem.RegisterHotkey(key, () => ToggleMicrophone());
            keybindLabel.Text = $"Keybind: {key.ToString().ToUpper()}";
            EnableControls(true);
            UpdateControls();
        }
        
        private void changeKeybind_Click(object sender, RoutedEventArgs e)
        {
            hotkeySystem.RemoveAllHotkeys();
            keybindLabel.Text = "Waiting..";
            EventHandler<VirtualKeys>? handler = null;
            handler = (s, e) =>
            {
                ChangeKeybind(e);
                hotkeySystem.RawKeyInput -= handler;
            };
            hotkeySystem.RawKeyInput += handler;
            EnableControls(false);
        }

        private void EnableControls(bool enabled)
        {
            muteMic.IsEnabled = enabled;
            enableMic.IsEnabled = enabled;
            changeKeybind.IsEnabled = enabled;
            beepCheckBox.IsEnabled = enabled;
        }

        private void UpdateControls()
        {
            muteMic.IsEnabled = !mutedMic;
            enableMic.IsEnabled = mutedMic;
        }
    }
}
