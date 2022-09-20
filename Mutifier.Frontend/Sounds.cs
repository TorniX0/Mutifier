using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Mutifier.Frontend
{
    public enum SoundType : int
    {
        MicMuted,
        MicUnmuted,
        MicBeeps
    }

    internal static class Sounds
    {
        public static void ValidateSounds()
        {
            List<string> missingSounds = new();

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

                MessageBox.Show(sb.ToString(), "Mutifier", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private static string GetSound(SoundType type) => type switch
        {
            SoundType.MicMuted => Utilities.GetCurrentPath() + @"sounds\muted.wav",
            SoundType.MicUnmuted => Utilities.GetCurrentPath() + @"sounds\unmuted.wav",
            SoundType.MicBeeps => Utilities.GetCurrentPath() + @"sounds\beep.wav",
            _ => string.Empty
        };

        public static void PlaySound(SoundType type)
        {
            string path = GetSound(type);

            if (File.Exists(path))
            {
                using var soundPlayer = new SoundPlayer(path);
                soundPlayer.Play();
            }
        }
    }
}
