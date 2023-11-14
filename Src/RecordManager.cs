using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IMKCode.APIX
{
    public class RecordManager
    {
        public long _lastInputTick;
        public List<string> _recCmds;
        private string _lcmd;
        private int _lx, _ly;

        private bool m_messageMode;
        internal IntPtr m_webhandle;

        private bool m_absolute_point;
        public bool IsAbsolutePoint { get { return m_absolute_point; } set { m_absolute_point = value; } }
        /// <summary>
        /// IMK: 获取坐标点模式(global/local)
        /// </summary>
        public string PointMode { get { return m_absolute_point ? "global" : "local"; } }

        public bool UseMessageMode(IntPtr handle)
        {
            if (handle != IntPtr.Zero)
            {
                m_webhandle = handle;
                m_messageMode = true;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool UseSystemMode(IntPtr web_handle)
        {
            m_messageMode = false;
            m_webhandle = web_handle;
            return true;
        }

        public string Scripts
        {
            get
            {
                return string.Join("\r\n", _recCmds);
            }
        }
        public RecordManager(IntPtr web_handle)
        {
            this.m_webhandle = web_handle;
            _recCmds = new List<string>();
        }

        #region 脚本录制
        public void StartRec()
        {
            _recCmds.Clear();
            _lastInputTick = DateTime.Now.Ticks;
            _lcmd = "";
            _lx = 0;
            _ly = 0;
        }
        public void StopRec()
        {
            if (_recCmds.Count > 0)
            {
                _recCmds.Insert(0, "var PRE_POINT_MODE = GetPointMode();\r\nSetPointMode(\"" + PointMode + "\");");
                addCmdSleep();
                _recCmds.Add("SetPointMode(PRE_POINT_MODE);");

            }
        }

        void addCmdSleep(int minDiff = 10)
        {
            long _diff = (DateTime.Now.Ticks - _lastInputTick) / 10000;
            _lastInputTick = DateTime.Now.Ticks;
            if (_diff >= minDiff)
            {
                string cmd = string.Format("Sleep({0});", _diff);
                _recCmds.Add(cmd);
            }
        }
        void RecAddCommand(string cmdText)
        {
            if (_lcmd != cmdText)
            {
                addCmdSleep();
                _lcmd = cmdText;
                _recCmds.Add(cmdText);
            }
        }

        public void RecMouseMove(int x, int y)
        {
            if (_lx != x || _ly != y)
            {
                _lx = x;
                _ly = y;
                //|Api.ScreenToClient(m_webhandle, ref _lx, ref _ly); //| IMK: 2023-11-08
                IfPointToClient(ref _lx, ref _ly);
                string cmd = string.Format("MouseMove({0},{1});", _lx, _ly);
                RecAddCommand(cmd);
            }
        }
        public void RecMouseDown(MouseButtons button)
        {
            string cmd = string.Format("MouseDown(\"{0}\");", button);
            RecAddCommand(cmd);
        }
        public void RecMouseUp(MouseButtons button)
        {
            string cmd = string.Format("MouseUp(\"{0}\");", button);
            RecAddCommand(cmd);
        }
        public void RecKeyDown(Keys key)
        {
            string cmd = string.Format("KeyDown(\"{0}\");", key);
            RecAddCommand(cmd);
        }
        public void RecKeyUp(Keys key)
        {
            string cmd = string.Format("KeyUp(\"{0}\");", key);
            RecAddCommand(cmd);
        }
        #endregion

        #region 脚本执行

        public event ScriptExecuteHandler ScriptExecuting;
        //private string[] cmds;
        //private int loopTasks;
        //private int loopIndex;
        private bool breakExe;
        System.Threading.Thread th;
        public void Execute(string scripts, bool delayStart = true, int loopCount = -1)
        {
            innerTasks iPar = new innerTasks();
            iPar.cmds = scripts.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            iPar.loopTasks = loopCount;
            iPar.delayStart = delayStart;
            breakExe = false;
            if (iPar.cmds.Length > 1)
            {
                System.Threading.ParameterizedThreadStart pts = new System.Threading.ParameterizedThreadStart(innerExecuting);
                if (th != null) { th.Abort(); }
                th = new System.Threading.Thread(pts);
                th.Start(iPar); //| 延时开始
            }
            else
            {
                if (ScriptExecuting != null)
                {
                    ScriptExecuting(this, new ExecuteStatus(true, iPar.loopTasks, 0, -1, -1, "脚本为空"));
                }
            }
        }
        public void BreakExec()
        {
            breakExe = true;
            if (th.ThreadState == System.Threading.ThreadState.WaitSleepJoin)
            {
                th.Abort();

                if (ScriptExecuting != null)
                {
                    ScriptExecuting(this, new ExecuteStatus(true, -1, -1, -1, -1, "任务已中止"));
                }
            }
        }
        struct innerTasks
        {
            public string[] cmds;
            public int loopTasks;
            public bool delayStart;
        }
        private void innerExecuting(object arg)
        {
            innerTasks iPar = (innerTasks)arg;
            int loopIndex = 0;
            if (iPar.delayStart)
            {
                if (ScriptExecuting != null)
                {
                    ScriptExecuting(this, new ExecuteStatus(false, iPar.loopTasks, loopIndex, -1, -1, "1 秒后开始,[F10]中止"));
                }
                //| 等待 1 秒开始
                System.Threading.Thread.Sleep(1000);
            }

            string func;
            string parstr;
            string[] pars;
        jmpLoop:
            int rowIndex = 0;
            int lines = iPar.cmds.Length;
            foreach (string cmd in iPar.cmds)
            {
                if (cmd != null && !cmd.StartsWith(";"))
                {
                    func = cmd.Split('(')[0];
                    parstr = cmd.Replace(func +"(", "").Split(')')[0];
                    pars = parstr.Replace("'", "").Replace("\"", "").Split(',');

                    if (breakExe){ break; }

                    if (ScriptExecuting != null)
                    {
                        ScriptExecuting(this, new ExecuteStatus(false, iPar.loopTasks, loopIndex, lines, (rowIndex +1), cmd));
                    }

                    try
                    {
                        this.GetType().InvokeMember(func,  System.Reflection.BindingFlags.InvokeMethod 
                            | System.Reflection.BindingFlags.Public 
                            | System.Reflection.BindingFlags.Instance, null, this, pars);
                    }
                    catch { }
                }
                rowIndex += 1;
            }
            if (!breakExe)
            {
                loopIndex += 1;
                if (iPar.loopTasks > 0)
                {
                    if (loopIndex < iPar.loopTasks)
                    {
                        goto jmpLoop;
                    }
                }
                else
                {
                    System.Threading.Thread.Sleep(100);
                    goto jmpLoop;
                }
            }

            if (ScriptExecuting != null)
            {
                ScriptExecuting(this, new ExecuteStatus(true, iPar.loopTasks, loopIndex, lines, rowIndex, string.Empty));
            }
        }

        /// <summary>
        /// IMK: 如果不是全局绝度坐标模式时，则转换为本地坐标
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void IfPointToClient(ref int x, ref int y)
        {
            if (!m_absolute_point)
            {
                Api.ScreenToClient(m_webhandle, ref x, ref y);
            }
        }
        /// <summary>
        /// IMK: 如果不是全局绝度坐标模式时，则转换为屏幕坐标
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void IfPointToScreen(ref int x, ref int y)
        {
            if (!m_absolute_point)
            {
                Api.ClientToScreen(m_webhandle, ref x, ref y);
            }
        }


        public void Sleep(string svalue)
        {
            System.Threading.Thread.Sleep(int.Parse(svalue));
        }

        public void MouseMove(string x, string y)
        {
            _lx = int.Parse(x); _ly = int.Parse(y);
            if (m_messageMode)
            {
                int _mx = _lx;
                int _my = _ly;
                //- Api.ScreenToClient(m_webhandle, ref _mx, ref _my);

                IntPtr lParam = (IntPtr)((_my << 16) | _mx); // The coordinates 
                IntPtr wParam = IntPtr.Zero; // Additional parameters for the click (e.g. Ctrl) 
                Api.PostMessage(m_webhandle, Api.WM_MOUSEMOVE, wParam, lParam); // Mouse button down 
            }
            else
            {
                int _mx = _lx;
                int _my = _ly;
                //|Api.ClientToScreen(m_webhandle, ref _mx, ref _my);
                IfPointToScreen(ref _mx, ref _my);

                Api.SetCursorPos(_mx, _my);
                //Api.mouse_event((int)Api.MouseEventFlag.Move, dpx, dpy, 0, 0);
            }
        }
        public void MouseDown(string button)
        {
            int downFlag = 0;
            uint downMsg = 0;
            MouseButtons ebtn = (MouseButtons)Enum.Parse(typeof(MouseButtons), button, true);
            switch(ebtn)
            {
                case MouseButtons.Left:
                {
                    downFlag = (int)Api.MouseEventFlag.LeftDown;
                    downMsg = Api.WM_LBUTTONDOWN;
                    break;
                }
                case MouseButtons.Middle:
                {
                    downFlag = (int)Api.MouseEventFlag.MiddleDown;
                    downMsg = Api.WM_MBUTTONDOWN;
                    break;
                }
                case MouseButtons.Right:
                {
                    downFlag = (int)Api.MouseEventFlag.RightDown;
                    downMsg = Api.WM_RBUTTONDOWN;
                    break;
                }
            }
            if (downFlag != 0)
            {
                if (m_messageMode)
                {
                    int _mx = _lx;
                    int _my = _ly;
                    Api.ScreenToClient(m_webhandle, ref _mx, ref _my);

                    IntPtr lParam = (IntPtr)((_my << 16) | _mx); // The coordinates 
                    IntPtr wParam = IntPtr.Zero; // Additional parameters for the click (e.g. Ctrl) 
                    Api.PostMessage(m_webhandle, downMsg, wParam, lParam); // Mouse button down 
                }
                else
                {
                    Api.mouse_event(downFlag, _lx, _ly, 0, 0);
                }
            }
        }
        public void MouseUp(string button)
        {
            int upFlag = 0;
            uint upMsg = 0;
            MouseButtons ebtn = (MouseButtons)Enum.Parse(typeof(MouseButtons), button, true);
            switch (ebtn)
            {
                case MouseButtons.Left:
                    {
                        upFlag = (int)Api.MouseEventFlag.LeftUp;
                        upMsg = Api.WM_LBUTTONUP;
                        break;
                    }
                case MouseButtons.Middle:
                    {
                        upFlag = (int)Api.MouseEventFlag.MiddleUp;
                        upMsg = Api.WM_MBUTTONUP;
                        break;
                    }
                case MouseButtons.Right:
                    {
                        upFlag = (int)Api.MouseEventFlag.RightUp;
                        upMsg = Api.WM_RBUTTONUP;
                        break;
                    }
            }
            if (m_messageMode && upMsg != 0)
            {
                int _mx = _lx;
                int _my = _ly;
                //Api.ScreenToClient(m_webhandle, ref _mx, ref _my);

                IntPtr lParam = (IntPtr)((_my << 16) | _mx); // The coordinates 
                IntPtr wParam = IntPtr.Zero; // Additional parameters for the click (e.g. Ctrl) 
                Api.PostMessage(m_webhandle, upMsg, wParam, lParam); // Mouse button up 
            }
            else if (upFlag != 0)
            {
                Api.mouse_event(upFlag, _lx, _ly, 0, 0);
            }
        }
        public void KeyDown(string key)
        {
            Keys ek = (Keys)Enum.Parse(typeof(Keys), key, true);
            if (m_messageMode)
            {
                IntPtr wParam = (IntPtr)ek;
                Api.PostMessage(m_webhandle, Api.WM_KEYDOWN, wParam, IntPtr.Zero);
            }
            else
            {
                Api.keybd_event((byte)ek, 0, 1, 0);
            }
        }
        public void KeyUp(string key)
        {
            Keys ek = (Keys)Enum.Parse(typeof(Keys), key, true);
            if (m_messageMode)
            {
                IntPtr wParam = (IntPtr)ek;
                Api.PostMessage(m_webhandle, Api.WM_KEYUP, wParam, IntPtr.Zero);
            }
            else
            {
                Api.keybd_event((byte)ek, 0, 2, 0);
            }
        }

        public delegate void ScriptExecuteHandler(object sender, ExecuteStatus e);
        public class ExecuteStatus
        {
            public bool IsEnd { get; set; }
            public int LoopTasks { get; set; }
            public int LoopIndex { get; set; }
            public int ScriptLines { get; set; }
            public int ExecCursor { get; set; }
            public string CurrentCommand { get; set; }

            public ExecuteStatus(bool end, int tasks, int index, int lines, int cursor, string cmd)
            {
                this.IsEnd = end;
                this.LoopTasks = tasks;
                this.LoopIndex = index;
                this.ScriptLines = lines;
                this.ExecCursor = cursor;
                this.CurrentCommand = cmd;
            }
        }
        #endregion
    }
}
