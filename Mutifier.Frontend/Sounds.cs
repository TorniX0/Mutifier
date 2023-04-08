using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Mutifier.Frontend
{
    public enum SoundType : int
    {
        Muted,
        Unmuted,
        Beeps
    }

    internal static class Sounds
    {
        private static WaveOutEvent? soundPlayer;
        private static AudioFileReader audio = null!;
        public static void ValidateSounds()
        {
            HashSet<string> missingSounds = new();

            foreach (SoundType type in (SoundType[])Enum.GetValues(typeof(SoundType)))
            {
                string path = GetSound(type);

                if (!File.Exists(path))
                {
                    missingSounds.Add(path);
                }
            }

            if (missingSounds.Count > 0)
            {
                StringBuilder sb = new();
                sb.AppendLine("The following sounds are missing:");

                foreach (string sound in missingSounds)
                {
                    sb.AppendLine(Path.GetFileName(sound));
                }

                sb.AppendLine();
                sb.AppendLine("These sounds will work if they are back in place.");

                DialogBox.Show(sb.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string GetSound(SoundType type) => Utilities.GetCurrentPath() + @"assets\sounds\" + 
        type switch
        {
            SoundType.Muted => "muted.wav",
            SoundType.Unmuted => "unmuted.wav",
            SoundType.Beeps => "beep.wav",
            _ => string.Empty
        };

        public static void PlaySound(SoundType type)
        {
            string path = GetSound(type);

            if (File.Exists(path))
            {
                soundPlayer?.Stop();

                try
                {
                    audio = new(path);
                }
                catch (FormatException e)
                {
                    DialogBox.Show(e.Message.Replace(Environment.NewLine, " "), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                soundPlayer = new();
                soundPlayer.Init(audio);
                soundPlayer.Play();
            }
        }
    }
}
