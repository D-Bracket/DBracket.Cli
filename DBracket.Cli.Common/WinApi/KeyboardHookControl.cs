using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DBracket.Cli.Common.WinApi;

public class KeyboardHookControl : IDisposable
{
    #region "----------------------------- Private Fields ------------------------------"
    private const int WH_KEYBOARD_LL = 13;
    private const int WM_KEYDOWN = 0x0100;

    private static IntPtr _hookID = IntPtr.Zero;
    #endregion



    #region "------------------------------ Constructor --------------------------------"
    public KeyboardHookControl()
    {
        _hookID = SetHook(HookCallback);
        Debug.WriteLine("Listening for key presses. Press Enter to exit.");
    }
    #endregion



    #region "--------------------------------- Methods ---------------------------------"
    #region "----------------------------- Public Methods ------------------------------"
    public void Dispose()
    {
        UnhookWindowsHookEx(_hookID);
    }

    public (bool Control, bool Shift, bool Windows, bool Alt) GetKeyModifiers()
    {
        var control = (GetAsyncKeyState((int)Key.VK_Control) & 0x8000) != 0;
        var shift = (GetAsyncKeyState((int)Key.VK_Shift) & 0x8000) != 0;
        var windows = (GetAsyncKeyState((int)Key.VK_LWIN) & 0x8000) != 0 ||
                      (GetAsyncKeyState((int)Key.VK_RWIN) & 0x8000) != 0;
        var alt = (GetAsyncKeyState((int)Key.VK_Alt) & 0x8000) != 0;
        return (control, shift, windows, alt);
    }
    #endregion

    #region "----------------------------- Private Methods -----------------------------"
    private IntPtr SetHook(LowLevelKeyboardProc proc)
    {
        using (Process curProcess = Process.GetCurrentProcess())
        using (ProcessModule curModule = curProcess.MainModule)
        {
            return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                GetModuleHandle(curModule.ModuleName), 0);
        }
    }
    #endregion

    #region "------------------------------ Event Handling -----------------------------"
    private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
        {
            int vkCode = Marshal.ReadInt32(lParam);
            Console.WriteLine($"Key Pressed: {(ConsoleKey)vkCode} (VK Code: {vkCode})");
            var t = KeyPressReport?.Invoke(ConvertIntToKey(vkCode));
            if (t.Value.handled)
            {
                return 1;
                //return CallNextHookEx(_hookID, nCode, wParam, lParam);
            }

            Key ConvertIntToKey(int vkCode)
            {
                return (Key)Enum.Parse(typeof(Key), vkCode.ToString());
            }
        }

        return CallNextHookEx(_hookID, nCode, wParam, lParam);
    }
    #endregion


    [DllImport("user32.dll")]
    public static extern short GetAsyncKeyState(int vKey);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr SetWindowsHookEx(int idHook,
      LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool UnhookWindowsHookEx(IntPtr hhk);

    [DllImport("user32.dll")]
    private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
        IntPtr wParam, IntPtr lParam);

    [DllImport("kernel32.dll")]
    private static extern IntPtr GetModuleHandle(string lpModuleName);
    #endregion



    #region "--------------------------- Public Propterties ----------------------------"
    #region "------------------------------- Properties --------------------------------"

    #endregion

    #region "--------------------------------- Events ----------------------------------"
    public static event KeyPressedHandler? KeyPressReport;
    public delegate (bool handled, int t) KeyPressedHandler(Key key);
    public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
    #endregion
    #endregion
}

public enum Key
{
    VK_LeftMouse = 0x01,     // 001 - Left mouse button
    VK_RightMouse = 0x02,     // 002 - Right mouse button
    VK_Cancel = 0x03,     // 003 - Control-break processing
    VK_MiddleMouse = 0x04,     // 004 - Middle mouse button
    VK_XButton1 = 0x05,     // 005 - X1 mouse button
    VK_XButton2 = 0x06,     // 006 - X2 mouse button
                            //                      = 0x07,     // 007 - Reserved
    VK_Back = 0x08,     // 008 - Backspace key
    VK_Tab = 0x09,     // 009 - Tab key
                       //                      = 0x0A,     // 010 - Reserved
                       //                      = 0x0B,     // 011 - Reserved
    VK_Clear = 0x0C,     // 012 - Clear key
    VK_Return = 0x0D,     // 013 - Enter key
                          //                      = 0x0E,     // 014 - Reserved
                          //                      = 0x0F,     // 015 - Reserved
    VK_Shift = 0x10,     // 016 - Shift key
    VK_Control = 0x11,  // 017 - Ctrl key
    VK_Alt = 0x12,  // 018 - Alt key
    VK_Pause = 0x13,    // 019 - Pause key
    VK_Capital = 0x14,  // 020 - Caps lock key
    VK_Kana = 0x15,     // 021 - IME Kana mode
                        //VK_Hangul             = 0x15, 	// 021 - IME Hangul mode
    VK_IME_On = 0x16,   // 022 - IME On
    VK_Junja = 0x17,    // 023 - IME Junja mode
    VK_Final = 0x18,    // 024 - IME final mode
                        //VK_Hanja 	            = 0x19, 	// 024 - IME Hanja mode
    VK_Kanji = 0x19,    // 025 - IME Kanji mode
    VK_IME_Off = 0x1A,  // 026 - IME Off
    VK_Escape = 0x1B,   // 027 - Esc key
    VK_Convert = 0x1C,  // 028 - IME convert
    VK_NonConvert = 0x1D,   // 029 - IME nonconvert
    VK_Accept = 0x1E,   // 030 - IME accept
    VK_ModeChange = 0x1F,   // 031 - IME mode change request
    VK_Space = 0x20,    // 032 - Spacebar key
    VK_Prior = 0x21,    // 033 - Page up key
    VK_Next = 0x22,     // 034 - Page down key
    VK_End = 0x23,  // 035 - End key
    VK_Home = 0x24,     // 036 - Home key
    VK_Left = 0x25,     // 037 - Left arrow key
    VK_Up = 0x26,   // 038 - Up arrow key
    VK_Right = 0x27,    // 039 - Right arrow key
    VK_Down = 0x28,     // 040 - Down arrow key
    VK_Select = 0x29,   // 041 - Select key
    VK_Print = 0x2A,    // 042 - Print key
    VK_Execute = 0x2B,  // 043 - Execute key
    VK_Snapshot = 0x2C,     // 044 - Print screen key
    VK_Insert = 0x2D,   // 045 - Insert key
    VK_Delete = 0x2E,   // 046 - Delete key
    VK_Help = 0x2F,     // 047 - Help key
    VK_0 = 0x30,    // 048 - 0 key
    VK_1 = 0x31,    // 049 - 1 key
    VK_2 = 0x32,    // 050 - 2 key
    VK_3 = 0x33,    // 051 - 3 key
    VK_4 = 0x34,    // 052 - 4 key
    VK_5 = 0x35,    // 053 - 5 key
    VK_6 = 0x36,    // 054 - 6 key
    VK_7 = 0x37,    // 055 - 7 key
    VK_8 = 0x38,    // 056 - 8 key
    VK_9 = 0x39,     // 057 - 9 key
                     //                      = 0x3A,     // 058 - Undefined
                     //                      = 0x3B,     // 059 - Undefined
                     //                      = 0x3C,     // 060 - Undefined
                     //                      = 0x3D,     // 061 - Undefined
                     //                      = 0x3E,     // 062 - Undefined
                     //                      = 0x3F,     // 063 - Undefined
                     //                      = 0x40,     // 064 - Undefined
    VK_A = 0x41,    // 065 - A key
    VK_B = 0x42,    // 066 - B key
    VK_C = 0x43,    // 067 - C key
    VK_D = 0x44,    // 068 - D key
    VK_E = 0x45,    // 069 - E key
    VK_F = 0x46,    // 070 - F key
    VK_G = 0x47,    // 071 - G key
    VK_H = 0x48,    // 072 - H key
    VK_I = 0x49,    // 073 - I key
    VK_J = 0x4A,    // 074 - J key
    VK_K = 0x4B,    // 075 - K key
    VK_L = 0x4C,    // 076 - L key
    VK_M = 0x4D,    // 077 - M key
    VK_N = 0x4E,    // 078 - N key
    VK_O = 0x4F,    // 079 - O key
    VK_P = 0x50,    // 080 - P key
    VK_Q = 0x51,    // 081 - Q key
    VK_R = 0x52,    // 082 - R key
    VK_S = 0x53,    // 083 - S key
    VK_T = 0x54,    // 084 - T key
    VK_U = 0x55,    // 085 - U key
    VK_V = 0x56,    // 086 - V key
    VK_W = 0x57,    // 087 - W key
    VK_X = 0x58,    // 088 - X key
    VK_Y = 0x59,    // 089 - Y key
    VK_Z = 0x5A,    // 090 - Z key
    VK_LWIN = 0x5B,     // 091 - Left Windows logo key
    VK_RWIN = 0x5C,     // 092 - Right Windows logo key
    VK_APPS = 0x5D,     // 093 - Application key
                        //                      = 0x5E,     // 094 - Reserved
    VK_SLEEP = 0x5F,    // 095 - Computer Sleep key
    VK_NUMPAD0 = 0x60,  // 096 - Numeric keypad 0 key
    VK_NUMPAD1 = 0x61,  // 097 - Numeric keypad 1 key
    VK_NUMPAD2 = 0x62,  // 098 - Numeric keypad 2 key
    VK_NUMPAD3 = 0x63,  // 099 - Numeric keypad 3 key
    VK_NUMPAD4 = 0x64,  // 100 - Numeric keypad 4 key
    VK_NUMPAD5 = 0x65,  // 101 - Numeric keypad 5 key
    VK_NUMPAD6 = 0x66,  // 102 - Numeric keypad 6 key
    VK_NUMPAD7 = 0x67,  // 103 - Numeric keypad 7 key
    VK_NUMPAD8 = 0x68,  // 104 - Numeric keypad 8 key
    VK_NUMPAD9 = 0x69,  // 105 - Numeric keypad 9 key
    VK_MULTIPLY = 0x6A,     // 106 - Multiply key
    VK_ADD = 0x6B,  // 107 - Add key
    VK_SEPARATOR = 0x6C,    // 108 - Separator key
    VK_SUBTRACT = 0x6D,     // 109 - Subtract key
    VK_DECIMAL = 0x6E,  // 110 - Decimal key
    VK_DIVIDE = 0x6F,   // 111 - Divide key
    VK_F1 = 0x70,   // 112 - F1 key
    VK_F2 = 0x71,   // 113 - F2 key
    VK_F3 = 0x72,   // 114 - F3 key
    VK_F4 = 0x73,   // 115 - F4 key
    VK_F5 = 0x74,   // 116 - F5 key
    VK_F6 = 0x75,   // 117 - F6 key
    VK_F7 = 0x76,   // 118 - F7 key
    VK_F8 = 0x77,   // 119 - F8 key
    VK_F9 = 0x78,   // 120 - F9 key
    VK_F10 = 0x79,  // 121 - F10 key
    VK_F11 = 0x7A,  // 122 - F11 key
    VK_F12 = 0x7B,  // 123 - F12 key
    VK_F13 = 0x7C,  // 124 - F13 key
    VK_F14 = 0x7D,  // 125 - F14 key
    VK_F15 = 0x7E,  // 126 - F15 key
    VK_F16 = 0x7F,  // 127 - F16 key
    VK_F17 = 0x80,  // 128 - F17 key
    VK_F18 = 0x81,  // 129 - F18 key
    VK_F19 = 0x82,  // 130 - F19 key
    VK_F20 = 0x83,  // 131 - F20 key
    VK_F21 = 0x84,  // 132 - F21 key
    VK_F22 = 0x85,  // 133 - F22 key
    VK_F23 = 0x86,  // 134 - F23 key
    VK_F24 = 0x87,  // 135 - F24 key
                    //                      = 0x88,     // 136 - Reserved
                    //                      = 0x89,     // 137 - Reserved
                    //                      = 0x8A,     // 138 - Reserved
                    //                      = 0x8B,     // 139 - Reserved
                    //                      = 0x8C,     // 140 - Reserved
                    //                      = 0x8D,     // 141 - Reserved
                    //                      = 0x8E,     // 142 - Reserved
                    //                      = 0x8F,     // 143 - Reserved
    VK_NUMLOCK = 0x90,  // 144 - Num lock key
    VK_SCROLL = 0x91,   // 145 - Scroll lock key
                        //                      = 0x92,     // 146 - OEM specific
                        //                      = 0x93,     // 147 - OEM specific
                        //                      = 0x94,     // 148 - OEM specific
                        //                      = 0x95,     // 149 - OEM specific
                        //                      = 0x96,     // 150 - OEM specific
                        //                      = 0x97,     // 151 - Unassigned
                        //                      = 0x98,     // 152 - Unassigned
                        //                      = 0x99,     // 153 - Unassigned
                        //                      = 0x9A,     // 154 - Unassigned
                        //                      = 0x9B,     // 155 - Unassigned
                        //                      = 0x9C,     // 156 - Unassigned
                        //                      = 0x9D,     // 157 - Unassigned
                        //                      = 0x9E,     // 158 - Unassigned
                        //                      = 0x9F,     // 159 - Unassigned
    VK_LSHIFT = 0xA0,     // 160 - Left Shift key
    VK_RSHIFT = 0xA1,   // 161 - Right Shift key
    VK_LCONTROL = 0xA2,     // 162 - Left Ctrl key
    VK_RCONTROL = 0xA3,     // 163 - Right Ctrl key
    VK_LMENU = 0xA4,    // 164 - Left Alt key
    VK_RMENU = 0xA5,    // 165 - Right Alt key
    VK_BROWSER_BACK = 0xA6,     // 166 - Browser Back key
    VK_BROWSER_FORWARD = 0xA7,  // 167 - Browser Forward key
    VK_BROWSER_REFRESH = 0xA8,  // 168 - Browser Refresh key
    VK_BROWSER_STOP = 0xA9,     // 169 - Browser Stop key
    VK_BROWSER_SEARCH = 0xAA,   // 170 - Browser Search key
    VK_BROWSER_FAVORITES = 0xAB,    // 171 - Browser Favorites key
    VK_BROWSER_HOME = 0xAC,     // 172 - Browser Start and Home key
    VK_VOLUME_MUTE = 0xAD,  // 173 - Volume Mute key
    VK_VOLUME_DOWN = 0xAE,  // 174 - Volume Down key
    VK_VOLUME_UP = 0xAF,    // 175 - Volume Up key
    VK_MEDIA_NEXT_TRACK = 0xB0,     // 176 - Next Track key
    VK_MEDIA_PREV_TRACK = 0xB1,     // 177 - Previous Track key
    VK_MEDIA_STOP = 0xB2,   // 178 - Stop Media key
    VK_MEDIA_PLAY_PAUSE = 0xB3,     // 179 - Play/Pause Media key
    VK_LAUNCH_MAIL = 0xB4,  // 180 - Start Mail key
    VK_LAUNCH_MEDIA_SELECT = 0xB5,  // 181 - Select Media key
    VK_LAUNCH_APP1 = 0xB6,  // 182 - Start Application 1 key
    VK_LAUNCH_APP2 = 0xB7,  // 183 - Start Application 2 key
                            //                      = 0xB8,     // 184 - Reserved
                            //                      = 0xB9,     // 185 - Reserved
    VK_OEM_1 = 0xBA,    // 186 - It can vary by keyboard. For the US ANSI keyboard, the Semiсolon and Colon key
    VK_OEM_PLUS = 0xBB,     // 187 - For any country/region, the Equals and Plus key
    VK_OEM_COMMA = 0xBC,    // 188 - For any country/region, the Comma and Less Than key
    VK_OEM_MINUS = 0xBD,    // 189 - For any country/region, the Dash and Underscore key
    VK_OEM_PERIOD = 0xBE,   // 190 - For any country/region, the Period and Greater Than key
    VK_OEM_2 = 0xBF,    // 191 - It can vary by keyboard. For the US ANSI keyboard, the Forward Slash and Question Mark key
    VK_OEM_3 = 0xC0,    // 192 - It can vary by keyboard. For the US ANSI keyboard, the Grave Accent and Tilde key
                        //                      = 0xC1,     // 193 - Reserved
                        //                      = 0xC2,     // 194 - Reserved
                        //                      = 0xC3,     // 195 - Reserved
                        //                      = 0xC4,     // 196 - Reserved
                        //                      = 0xC5,     // 197 - Reserved
                        //                      = 0xC6,     // 198 - Reserved
                        //                      = 0xC7,     // 199 - Reserved
                        //                      = 0xC8,     // 200 - Reserved
                        //                      = 0xC9,     // 201 - Reserved
                        //                      = 0xCA,     // 202 - Reserved
                        //                      = 0xCB,     // 203 - Reserved
                        //                      = 0xCC,     // 204 - Reserved
                        //                      = 0xCD,     // 205 - Reserved
                        //                      = 0xCE,     // 206 - Reserved
                        //                      = 0xCF,     // 207 - Reserved
                        //                      = 0xD0,     // 208 - Reserved
                        //                      = 0xD1,     // 209 - Reserved
                        //                      = 0xD2,     // 210 - Reserved
                        //                      = 0xD3,     // 211 - Reserved
                        //                      = 0xD4,     // 212 - Reserved
                        //                      = 0xD5,     // 213 - Reserved
                        //                      = 0xD6,     // 214 - Reserved
                        //                      = 0xD7,     // 215 - Reserved
                        //                      = 0xD8,     // 216 - Reserved
                        //                      = 0xD9,     // 217 - Reserved
                        //                      = 0xDA,     // 218 - Reserved
    VK_OEM_4 = 0xDB,    // 219 - It can vary by keyboard. For the US ANSI keyboard, the Left Brace key
    VK_OEM_5 = 0xDC,    // 220 - It can vary by keyboard. For the US ANSI keyboard, the Backslash and Pipe key
    VK_OEM_6 = 0xDD,    // 221 - It can vary by keyboard. For the US ANSI keyboard, the Right Brace key
    VK_OEM_7 = 0xDE,    // 222 - It can vary by keyboard. For the US ANSI keyboard, the Apostrophe and Double Quotation Mark key
    VK_OEM_8 = 0xDF,     // 223 - It can vary by keyboard. For the Canadian CSA keyboard, the Right Ctrl key
                         //                      = 0xE0,     // 224 - Reserved
                         //                      = 0xE1,     // 225 - OEM specific
    VK_OEM_102 = 0xE2,  // 226 - It can vary by keyboard. For the European ISO keyboard, the Backslash and Pipe key
                        //                      = 0xE3,     // 227 - OEM specific
                        //                      = 0xE4,     // 228 - OEM specific
    VK_PROCESSKEY = 0xE5,   // 229 - IME PROCESS key
                            //                      = 0xE6,     // 230 - OEM specific
    VK_PACKET = 0xE7,     // 231 - Used to pass Unicode characters as if they were keystrokes. The VK_PACKET key is the low word of a 32-bit Virtual Key value used for non-keyboard input methods. For more information, see Remark in KEYBDINPUT, SendInput, WM_KEYDOWN, and WM_KEYUP
                          //                      = 0xE8,     // 232 - Unassigned
                          //                      = 0xE9,     // 233 - OEM specific
                          //                      = 0xEA,     // 234 - OEM specific
                          //                      = 0xEB,     // 235 - OEM specific
                          //                      = 0xEC,     // 236 - OEM specific
                          //                      = 0xED,     // 237 - OEM specific
                          //                      = 0xEE,     // 238 - OEM specific
                          //                      = 0xEF,     // 239 - OEM specific
                          //                      = 0xF0,     // 240 - OEM specific
                          //                      = 0xF1,     // 241 - OEM specific
                          //                      = 0xF2,     // 242 - OEM specific
                          //                      = 0xF3,     // 243 - OEM specific
                          //                      = 0xF4,     // 244 - OEM specific
                          //                      = 0xF5,     // 245 - OEM specific
    VK_ATTN = 0xF6,     // 246 - Attn key
    VK_CRSEL = 0xF7,    // 247 - CrSel key
    VK_EXSEL = 0xF8,    // 248 - ExSel key
    VK_EREOF = 0xF9,    // 249 - Erase EOF key
    VK_PLAY = 0xFA,     // 250 - Play key
    VK_ZOOM = 0xFB,     // 251 - Zoom key
    VK_NONAME = 0xFC,   // 252 - Reserved
    VK_PA1 = 0xFD,  // 253 - PA1 key
    VK_OEM_CLEAR = 0xFE,    // 254 - Clear key
}