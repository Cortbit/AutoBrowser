using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace IMKCode.APIX
{
    public class MouseHookEx
    {
        //安装钩子 
        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr SetWindowsHookEx(WH_Codes idHook, HookProc lpfn, IntPtr pInstance, int threadId);

        //卸载钩子
        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(IntPtr pHookHandle);

        //传递钩子
        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(IntPtr pHookHandle, int nCode, Int32 wParam, IntPtr lParam);


        [DllImport("user32", EntryPoint = "GetKeyboardState")]
        private static extern int GetKeyboardState(byte[] pbKeyState);

        //获取当前鼠标位置
        [DllImport("user32.dll")]
        public extern static int GetCursorPos(ref POINT lpPoint);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

        }
        /// 钩子委托声明
        public delegate int HookProc(int nCode, Int32 wParam, IntPtr lParam);

        /// 无返回委托声明
        public delegate void VoidCallback();

        public enum WH_Codes : int
        {
            /// <summary>
            /// 底层鼠标钩子
            /// </summary>
            WH_MOUSE = 7,

            WH_MOUSE_LL = 14,
            
            WH_KEYBOARD = 2,

            WH_KEYBOARD_LL = 13


        }
        public event KeyEventExHandler OnKeyActivity;
        // 添加
        public event MouseEventExHandler OnMouseActivity;
        public enum WM_MOUSE : int
        {
            /// <summary>
            /// 鼠标开始
            /// </summary>
            WM_MOUSEFIRST = 0x200,

            /// <summary>
            /// 鼠标移动
            /// </summary>
            WM_MOUSEMOVE = 0x200,

            /// <summary>
            /// 左键按下
            /// </summary>
            WM_LBUTTONDOWN = 0x201,

            /// <summary>
            /// 左键释放
            /// </summary>
            WM_LBUTTONUP = 0x202,

            /// <summary>
            /// 左键双击
            /// </summary>
            WM_LBUTTONDBLCLK = 0x203,

            /// <summary>
            /// 右键按下
            /// </summary>
            WM_RBUTTONDOWN = 0x204,

            /// <summary>
            /// 右键释放
            /// </summary>
            WM_RBUTTONUP = 0x205,

            /// <summary>
            /// 右键双击
            /// </summary>
            WM_RBUTTONDBLCLK = 0x206,

            /// <summary>
            /// 中键按下
            /// </summary>
            WM_MBUTTONDOWN = 0x207,

            /// <summary>
            /// 中键释放
            /// </summary>
            WM_MBUTTONUP = 0x208,

            /// <summary>
            /// 中键双击
            /// </summary>
            WM_MBUTTONDBLCLK = 0x209,

            /// <summary>
            /// 滚轮滚动
            /// </summary>
            /// <remarks>WINNT4.0以上才支持此消息</remarks>
            WM_MOUSEWHEEL = 0x020A

        }
        public enum WM_KEY
        {
            WM_KEYDOWN = 0x100,
            WM_KEYUP = 0x101,
            WM_SYSKEYDOWN = 0x104,
            WM_SYSKEYUP = 0x105
        }



        /// 鼠标钩子句柄
        private IntPtr m_pMouseHook = IntPtr.Zero;

        /// 鼠标钩子委托实例
        private HookProc m_MouseHookProcedure;


        /// 键盘钩子句柄
        private IntPtr m_pKeyHook = IntPtr.Zero;

        /// 键盘钩子委托实例
        private HookProc m_KeyHookProcedure;


        [DllImport("kernel32.dll")]
        public static extern int GetCurrentThreadId();

        #region 鼠标钩子事件结构定义
        [StructLayout(LayoutKind.Sequential)]
        public struct MouseHookStruct
        {
            public POINT Point;
            public UInt32 MouseData;
            public UInt32 Flags;
            public UInt32 Time;
            public UInt32 ExtraInfo;
        }
        [StructLayout(LayoutKind.Sequential)]
        public class KeyboardHookStruct
        {
            public int vkCode; //表示一个在1到254间的虚似键盘码 
            public int scanCode; //表示硬件扫描码 
            public int flags;
            public int time;
            public int dwExtraInfo;
        }

        #endregion

        #region 钩子处理函数
        private int MouseHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            if ((nCode >= 0) && (OnMouseActivity != null))
            {
                MouseHookStruct mouseHookStruct = (MouseHookStruct)Marshal.PtrToStructure(lParam, typeof(MouseHookStruct));
                MouseButtons button = MouseButtons.None;
                ButtonStatus btnStatus = ButtonStatus.None;
                short mouseDelta = 0;
                switch (wParam)
                {
                    //case (int)WM_MOUSE.WM_MOUSEMOVE:
                    //    button = MouseButtons.None;
                    //    btnStatus = ButtonStatus.Move;
                    //    break;
                    case (int)WM_MOUSE.WM_LBUTTONDOWN:
                        button = MouseButtons.Left;
                        btnStatus = ButtonStatus.Down;
                        break;
                    case (int)WM_MOUSE.WM_LBUTTONUP:
                        button = MouseButtons.Left;
                        btnStatus = ButtonStatus.Up;
                        break;
                    case (int)WM_MOUSE.WM_RBUTTONDOWN:
                        button = MouseButtons.Right;
                        btnStatus = ButtonStatus.Down;
                        break;
                    case (int)WM_MOUSE.WM_RBUTTONUP:
                        button = MouseButtons.Right;
                        btnStatus = ButtonStatus.Up;
                        break;
                    case (int)WM_MOUSE.WM_MBUTTONDOWN:
                        button = MouseButtons.Middle;
                        btnStatus = ButtonStatus.Down;
                        break;
                    case (int)WM_MOUSE.WM_MBUTTONUP:
                        button = MouseButtons.Middle;
                        btnStatus = ButtonStatus.Up;
                        break;
                    case (int)WM_MOUSE.WM_MOUSEWHEEL:
                        mouseDelta = (short)((mouseHookStruct.MouseData >> 16) & 0xffff);
                        break;
                }

                //double clicks
                int clickCount = 0;
                if (button != MouseButtons.None)
                {
                    if (wParam == (int)WM_MOUSE.WM_LBUTTONDBLCLK || wParam == (int)WM_MOUSE.WM_RBUTTONDBLCLK)
                    {
                        clickCount = 2;
                    }
                    else { clickCount = 1; }
                }

                //generate event 
                MouseEventExArgs e = new MouseEventExArgs(button, clickCount, mouseHookStruct.Point.X, mouseHookStruct.Point.Y, mouseDelta, btnStatus);
                //raise it
                this.OnMouseActivity(this, e);
            }
            return CallNextHookEx(this.m_pMouseHook, nCode, wParam, lParam);
        }
        private int KeyHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            if ((nCode >= 0) && (OnKeyActivity != null))
            {
                KeyboardHookStruct MyKeyboardHookStruct = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));                
                if ((OnKeyActivity != null) && (wParam == (int)WM_KEY.WM_KEYDOWN || wParam == (int)WM_KEY.WM_SYSKEYDOWN))
                {
                    Keys keyData = (Keys)MyKeyboardHookStruct.vkCode;
                    OnKeyActivity(this, new KeyEventExArgs(keyData, ButtonStatus.Down));
                }
                if ((OnKeyActivity != null) && (wParam == (int)WM_KEY.WM_KEYUP || wParam == (int)WM_KEY.WM_SYSKEYUP))
                {
                    Keys keyData = (Keys)MyKeyboardHookStruct.vkCode;
                    OnKeyActivity(this, new KeyEventExArgs(keyData, ButtonStatus.Up));
                }
            }
            return CallNextHookEx(this.m_pKeyHook, nCode, wParam, lParam);
        }

        private Keys toKey(int keyCode)
        {
            Keys key  = (Keys)Enum.Parse(typeof(Keys), keyCode.ToString());
            return key;
        }

        #endregion

        #region 安装钩子
        public bool InstallHook(bool useLowLevel)
        {
            IntPtr pInstance = Marshal.GetHINSTANCE(System.Reflection.Assembly.GetExecutingAssembly().ManifestModule);
            if (this.m_pMouseHook == IntPtr.Zero)
            {
                m_MouseHookProcedure = new HookProc(this.MouseHookProc);
                if (useLowLevel)
                { this.m_pMouseHook = SetWindowsHookEx(WH_Codes.WH_MOUSE_LL, m_MouseHookProcedure, (IntPtr)0, 0); }
                else { this.m_pMouseHook = SetWindowsHookEx(WH_Codes.WH_MOUSE, m_MouseHookProcedure, (IntPtr)0, GetCurrentThreadId()); }
                if (this.m_pMouseHook == IntPtr.Zero)
                {
                    this.UnInstallHook();
                    return false;
                }
            }
            if (this.m_pKeyHook == IntPtr.Zero)
            {
                m_KeyHookProcedure = new HookProc(this.KeyHookProc);
                this.m_pKeyHook = SetWindowsHookEx(WH_Codes.WH_KEYBOARD_LL, m_KeyHookProcedure, (IntPtr)0, 0);
                if (this.m_pKeyHook == IntPtr.Zero)
                {
                    this.UnInstallHook();
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region 卸载钩子
        public bool UnInstallHook()
        {
            bool result = true;
            if (this.m_pMouseHook != IntPtr.Zero)
            {
                result = (UnhookWindowsHookEx(this.m_pMouseHook) && result);
                this.m_pMouseHook = IntPtr.Zero;
            }
            if (this.m_pKeyHook != IntPtr.Zero)
            {
                result = (UnhookWindowsHookEx(this.m_pKeyHook) && result);
                this.m_pKeyHook = IntPtr.Zero;
            }

            return result;
        }
        #endregion
    }
    public enum ButtonStatus
    {
        None,
        //Move,
        Down,
        Up
    }

    public delegate void MouseEventExHandler(object sender, MouseEventExArgs e);
    public class MouseEventExArgs : MouseEventArgs
    {
        public ButtonStatus wmEvent { get; set; }

        public MouseEventExArgs(MouseButtons button, int clicks, int x, int y, int delta, ButtonStatus wmEvt)
            : base(button, clicks, x, y, delta)
        {
            this.wmEvent = wmEvt;
        }
    }
    
    public delegate void KeyEventExHandler(object sender, KeyEventExArgs e);
    public class KeyEventExArgs : KeyEventArgs
    {
        public ButtonStatus wmEvent { get; set; }

        public KeyEventExArgs(Keys key, ButtonStatus wmEvt)
            : base(key)
        {
            this.wmEvent = wmEvt;
        }
    }
}
