using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Mutifier.Frontend
{
    public partial class MainWindow : Form
    {
        bool mutedMic = false;
        bool changingKeybind = false;

        HotkeySystem? hotkeySystem;

        private void CenterHotkeyUI()
        {
            changeKeybind.Left = (ClientSize.Width - changeKeybind.Width - keybindLabel.Width) / 2;
            keybindLabel.Left = (ClientSize.Width - keybindLabel.Width + changeKeybind.Width) / 2;
        }

        public MainWindow()
        {
            InitializeComponent();

            hotkeySystem = new(this.Handle);
            hotkeySystem.RegisterHotkey(Keys.Home, () => ToggleMicrophone());

            if (Wrapper.GetMicMuted())
            {
                ToggleMicrophone(false);
            }

            UpdateControls();
        }

        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if (!changingKeybind)
            {
                hotkeySystem?.HandleHotkeys(ref message);
            }
        }

        private void ToggleMicrophone(bool playSound = true)
        {
            mutedMic = !mutedMic;
            Wrapper.SetMicMuted(mutedMic);

            if (playSound)
            {
                SoundType sound = mutedMic ? SoundType.MicMuted : SoundType.MicUnmuted;
                Sounds.PlaySound(sound);
            }

            ToggleBeep(mutedMic);

            muteMic.Enabled = !mutedMic;
            enableMic.Enabled = mutedMic;
        }

        private void ToggleBeep(bool muted)
        {
            if (muted) mutedBeep.Start();
            else mutedBeep.Stop();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (msg.Msg == (int)MessageType.KEY_PRESS && changingKeybind)
            {
                ChangeKeybind(keyData);
                changingKeybind = false;
                DisableControls(false);
                UpdateControls();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void mutedBeep_Tick(object sender, EventArgs e)
        {
            if (!beepCheckBox.Checked)
                return;

            Sounds.PlaySound(SoundType.MicBeeps);
        }

        private void authorLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo() 
            { 
                FileName = "https://github.com/TorniX0",
                UseShellExecute = true
            });
        }

        private void ChangeKeybind(Keys key)
        {
            hotkeySystem?.RegisterHotkey(key, () => ToggleMicrophone());
            keybindLabel.Text = $"Keybind: {key.ToString().ToUpper()}";
            UpdateControls();
        }

        private void changeKeybind_Click(object sender, EventArgs e)
        {
            hotkeySystem?.RemoveHotkey(0);
            keybindLabel.Text = "Waiting..";
            UpdateControls();
            changingKeybind = true;
            DisableControls(true);
        }

        private void DisableControls(bool enabled)
        {
            muteMic.Enabled = !enabled;
            enableMic.Enabled = !enabled;
            changeKeybind.Enabled = !enabled;
            beepCheckBox.Enabled = !enabled;
        }

        private void UpdateControls()
        {
            muteMic.Enabled = !mutedMic;
            enableMic.Enabled = mutedMic;
            CenterHotkeyUI();
        }
    }
}