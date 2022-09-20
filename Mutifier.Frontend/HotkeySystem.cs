using Microsoft.VisualBasic.Devices;
using System.Runtime.InteropServices;

namespace Mutifier.Frontend
{
    internal enum MessageType : int
    {
        HOTKEY_TRIGGER = 0x0312,
        KEY_PRESS = 0x0100
    }

    /// <summary>
    /// Hotkey class to handle all of our inputs in the app.
    /// TODO: Extend the hotkeys to mouse buttons as well.
    /// </summary>

    internal class HotkeySystem
    {
        private HashSet<KeyValuePair<Keys, Action>> hotkeys;
        private IntPtr handle = IntPtr.Zero;

        internal HotkeySystem(IntPtr handle)
        {
            this.hotkeys = new HashSet<KeyValuePair<Keys, Action>>();
            this.handle = handle;
        }

        ~HotkeySystem()
        {
            for (int i = 0; i < hotkeys.Count; i++)
            {
                RemoveHotkey(i);
            }
        }

        internal void HandleHotkeys(ref Message message)
        {
            if (message.Msg == (int)MessageType.HOTKEY_TRIGGER)
            {
                int id = message.WParam.ToInt32();
                KeyValuePair<Keys, Action> element = hotkeys.ElementAtOrDefault(id);
                if (!element.Equals(default(KeyValuePair<Keys, Action>)))
                {
                    element.Value?.Invoke();
                }
            }
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        internal int RegisterHotkey(Keys key, Action hotkeyFunction)
        {
            if (hotkeys.Add(new KeyValuePair<Keys, Action>(key, hotkeyFunction)))
            {
                int id = hotkeys.ToList().FindIndex(x => x.Key == key);
                RegisterHotKey(handle, id, 0, key.GetHashCode());
                return id;
            }
            else
            {
                return -1;
            }
        }

        internal bool RemoveHotkey(int id)
        {
            if (hotkeys.Count > 0 && hotkeys.Remove(hotkeys.ElementAt(id)))
            {
                return UnregisterHotKey(handle, id);
            }
            else
            {
                return false;
            }
        }
    }
}
