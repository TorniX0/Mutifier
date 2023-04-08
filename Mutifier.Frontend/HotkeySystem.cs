using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;
using System.Linq;

namespace Mutifier.Frontend
{
    /// <summary>
    /// Windows message type
    /// </summary>
    /// <remarks>
    /// <a href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-msg" />.
    /// </remarks>
    public enum MessageType : int
    {
        HOTKEY_TRIGGER = 0x0312,
        KEY_PRESS = 0x0100
    }

    /// <summary>
    /// Hotkey class to handle all of our global inputs in the app.
    /// TODO: Extend the hotkeys to mouse buttons as well.
    /// </summary>

    public partial class HotkeySystem : IDisposable
    {
        private HashSet<KeyValuePair<VirtualKeys, Action>> hotkeys;
        private readonly IntPtr handle = IntPtr.Zero;
        public event EventHandler<VirtualKeys>? RawKeyInput;

        private SUBCLASSPROC? callback;
        private readonly object _lockObject = new();

        public HotkeySystem(IntPtr handle)
        {
            this.hotkeys = new HashSet<KeyValuePair<VirtualKeys, Action>>();
            this.handle = handle;
            Subscribe();
        }

        /// <returns>A read only set containing all the hotkeys</returns>
        public IReadOnlySet<KeyValuePair<VirtualKeys, Action>> HotkeySet()
            => hotkeys;

        protected virtual void HandleHotkeys(uint msg, nuint wParam)
        {
            if (msg == (uint)MessageType.KEY_PRESS)
            {
                RawKeyInput?.Invoke(null, (VirtualKeys)wParam);
            }
            else if (msg == (uint)MessageType.HOTKEY_TRIGGER)
            {
                int id = (int)wParam;
                KeyValuePair<VirtualKeys, Action> element = hotkeys.ElementAtOrDefault(id);
                if (!element.Equals(default(KeyValuePair<VirtualKeys, Action>)))
                {
                    element.Value?.Invoke();
                }
            }
        }

        /// <param name="key">Key to register the hotkey for</param>
        /// <param name="hotkeyFunction">Action to execute on hotkey press</param>
        /// <returns>The id of the registered key, -1 if the key already exists or if the key registration failed</returns>
        public int RegisterHotkey(VirtualKeys key, Action hotkeyFunction)
        {
            if (hotkeys.Add(new KeyValuePair<VirtualKeys, Action>(key, hotkeyFunction)))
            {
                int id = hotkeys.ToList().FindIndex(x => x.Key == key);
                bool result = RegisterHotKey(handle, id, 0, key.GetHashCode());
                return result ? id : -1;
            }
            else
            {
                return -1;
            }
        }

        /// <param name="id">Id of the registered hotkey you want to remove</param>
        /// <returns>If the hotkey removal succeeded, this will return true, otherwise, if the id introduced doesn't exist or the hotkey removal fails, this will return false</returns>
        public bool RemoveHotkey(int id)
        {
            if (hotkeys.Remove(hotkeys.ElementAtOrDefault(id)))
            {
                return UnregisterHotKey(handle, id);
            }
            else
            {
                return false;
            }
        }

        public void RemoveAllHotkeys()
        {
            for (int i = 0; i < hotkeys.Count; i++)
            {
                RemoveHotkey(i);
            }
        }

        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);

        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool UnregisterHotKey(IntPtr hWnd, int id);


        protected virtual nint WndProc(IntPtr hWnd, uint uMsg, nuint wParam, nint lParam, nuint uIdSubclass, nuint dwRefData)
        {
            HandleHotkeys(uMsg, wParam);

            return DefSubclassProc(hWnd, uMsg, wParam, lParam);
        }

        protected void Subscribe()
        {
            lock (_lockObject)
            {
                if (callback == null)
                {
                    callback = new SUBCLASSPROC(WndProc);
                    SetWindowSubclass(handle, callback, 101, 0);
                }
            }
        }

        protected void Unsubscribe()
        {
            lock (_lockObject)
            {
                if (callback != null)
                {
                    RemoveWindowSubclass(handle, callback, 101);
                    callback = null;
                }
            }
        }

        [LibraryImport("comctl32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool SetWindowSubclass(IntPtr hWnd, SUBCLASSPROC pfnSubclass, nuint uIdSubclass, nuint dwRefData);

        [LibraryImport("comctl32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool RemoveWindowSubclass(IntPtr hWnd, SUBCLASSPROC pfnSubclass, nuint uIdSubclass);

        [LibraryImport("comctl32.dll")]
        private static partial nint DefSubclassProc(IntPtr hWnd, uint uMsg, nuint wParam, nint lParam);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        private delegate nint SUBCLASSPROC(IntPtr hWnd, uint uMsg, nuint wParam, nint lParam, nuint uIdSubclass, nuint dwRefData);

        public void Dispose()
        {
            RemoveAllHotkeys();
            Unsubscribe();
        }
    }
}