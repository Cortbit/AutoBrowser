using System;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;

namespace IMKCode
{
    public class Api
    {
        // Fields
        public const int MOD_ALT = 1;
        public const int MOD_CONTROL = 2;
        public const int MOD_SHIFT = 4;
        public const int VK_ESCAPE = 0x1b;
        public const int VK_SPACE = 0x20;
        public const int WM_CREATE = 1;
        public const int WM_DESTROY = 2;
        public const int WM_HOTKEY = 0x312;

        public const uint WM_MOUSEMOVE = 0x0200;    // mouse move
        public const uint WM_LBUTTONDOWN = 0x201;   // Left click down code 
        public const uint WM_LBUTTONUP = 0x202;     // Left click up code
        public const uint WM_RBUTTONDOWN = 0x204;   // Right click down
        public const uint WM_RBUTTONUP = 0x205;     // Right click up
        public const uint WM_MBUTTONDOWN = 0x207;   // Middle click down
        public const uint WM_MBUTTONUP = 0x208;     // Middle click up

        public const uint WM_KEYDOWN = 0x100;
        public const uint WM_KEYUP = 0x101;

        public enum MouseEventFlag
        {
            Absolute = 0x8000,
            LeftDown = 2,
            LeftUp = 4,
            MiddleDown = 0x20,
            MiddleUp = 0x40,
            Move = 1,
            RightDown = 8,
            RightUp = 0x10,
            VirtualDesk = 0x4000,
            Wheel = 0x800,
            XDown = 0x80,
            XUp = 0x100
        }

        
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "GetKeyState")]  
        public static extern int GetKeyState(int nVirtKey);

        // Methods
        [DllImport("user32.dll")]
        public static extern bool AttachThreadInput(IntPtr idAttach, IntPtr idAttachTo, bool fAttach);
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetCurrentThreadId();
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetCursor();
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(ref Point lpPoint);
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindow(IntPtr hwnd, uint wCmd);
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hwnd, IntPtr lpdwProcessId);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern void mouse_event(int flags, int dx, int dy, int dwData, int dwExtraInfo);
        [DllImport("user32.dll")]
        public static extern int RegisterHotKey(IntPtr hwnd, int id, int fsModifiers, int vk);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool SetCursorPos(int X, int Y);
        [DllImport("user32.dll")]
        public static extern int UnregisterHotKey(IntPtr hwnd, int id);
        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(int xPoint, int yPoint);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, int[] lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll")]
        public static extern int MessageBoxTimeoutA(IntPtr hWnd, string msg, string Caps, int type, int Id, int time);

        [DllImport("user32.dll")]
        public static extern uint ScreenToClient(IntPtr hwnd, ref Point p);
        public static uint ScreenToClient(IntPtr hwnd, ref int x, ref int y)
        {
            Point p = new Point(x,y);
            uint r = ScreenToClient(hwnd, ref p);
            x = p.X;
            y = p.Y;
            return r;
        }
        [DllImport("user32.dll")]
        public static extern uint ClientToScreen(IntPtr hwnd, ref Point p);
        public static uint ClientToScreen(IntPtr hwnd, ref int x, ref int y)
        {
            Point p = new Point(x,y);
            uint r = ClientToScreen(hwnd, ref p);
            x = p.X;
            y = p.Y;
            return r;
        }

        [Flags()]
        public enum KeyModifiers
        {
            None = 0,
            Alt = 1,
            Ctrl = 2,
            Shift = 4,
            WindowsKey = 8,
        }

        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole(); //关联一个控制台窗口
        [DllImport("kernel32.dll")]
        public static extern bool FreeConsole(); //释放关联的控制台窗口


        private static bool? m_isAdmin;
        public static bool IsAdministrator
        {
            get
            {
                if (!m_isAdmin.HasValue)
                {
                    System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
                    System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
                    //| System.Environment.OSVersion.Version.Major >= 6
                    if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
                    {
                        m_isAdmin = true;
                    }
                    else
                    {
                        m_isAdmin = false;
                    }
                }
                return m_isAdmin.Value;
            }
        }

        public static bool getSuportHTML5()
        {
            string _exeName = System.IO.Path.GetFileName(System.Windows.Forms.Application.ExecutablePath);
            var ev = Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", _exeName, null);
            if (ev == null) { return false; }

            return Convert.ToInt32(ev) >= 0x2000;
        }

        private const int FO_DELETE = 0x3;
        private const ushort FOF_NOCONFIRMATION = 0x10;
        private const ushort FOF_ALLOWUNDO = 0x40;

        [DllImport("shell32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern int SHFileOperation([In, Out] _SHFILEOPSTRUCT str);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public class _SHFILEOPSTRUCT
        {
            public IntPtr hwnd;
            public UInt32 wFunc;
            public string pFrom;
            public string pTo;
            public UInt16 fFlags;
            public Int32 fAnyOperationsAborted;
            public IntPtr hNameMappings;
            public string lpszProgressTitle;
        }
        /// <summary>
        /// 移动到回收站中
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static int MoveToTrash(string path)
        {
            _SHFILEOPSTRUCT pm = new _SHFILEOPSTRUCT();
            pm.wFunc = FO_DELETE;
            pm.pFrom = path + '\0';
            pm.pTo = null;
            pm.fFlags = FOF_ALLOWUNDO | FOF_NOCONFIRMATION;
            return SHFileOperation(pm);
        }

    }


}
