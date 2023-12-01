using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MiniBlinkPinvoke;
using IMKCode.APIX;

namespace AutoBrowser
{
    public partial class FrmBrowser : Form
    {
        /// <summary>
        /// IMK: 脚本录制中标识
        /// 0 - 未录制
        /// 1 - 准备中
        /// 2 - 录制中
        /// 3 - 结束中
        /// </summary>
        private int m_flag_script_record_status = 0;

        private bool m_flag_script_runing_status = false;
        private bool m_flag_work_area_full_screen = false;

        /// <summary>
        /// 未录制
        /// </summary>
        private bool IsRecordIdle { get { return m_flag_script_record_status == 0; } }
        /// <summary>
        /// 准备录制中
        /// </summary>
        private bool IsRecordReadying { get { return m_flag_script_record_status == 1; } }
        /// <summary>
        /// 录制中
        /// </summary>
        private bool IsRecording { get { return m_flag_script_record_status == 2; } }
        /// <summary>
        /// 结束中
        /// </summary>
        private bool IsRecordStoping { get { return m_flag_script_record_status == 3; } }

        private RecordHooks recordHook = new RecordHooks();
        private RecordManager recordManager;


        /// <summary>
        /// IMK: 显示右侧工具栏标识
        /// </summary>
        private bool m_flag_show_script_tools = true;

        /// <summary>
        /// IMK: 脚本保存目录
        /// </summary>
        private System.IO.DirectoryInfo m_script_folder;

        private System.IO.DirectoryInfo m_target_folder;

        private Form loadForm(string formName)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name.Equals(formName)){ return f; }
            }
            try
            {
                Type t = Type.GetType(formName);
                if (t != null || (t = Type.GetType("AutoBrowser." + formName)) != null)
                {
                    object o = Activator.CreateInstance(t);
                    if (o is Form) { return (Form)o; }
                }
            }
            catch { }

            Form frm = new Form();
            frm.Icon = this.Icon;
            frm.Name = formName;
            return frm;
        }
        private void PopControl(Control ctrl, string title=null, EventHandler onclose=null)
        {
            if (ctrl == null) { return; }
            string _name = ctrl.Name;
            Form frm = loadForm(_name);
            frm.Text = title ?? _name;
            if (frm.Tag == null)
            {
                frm.Tag = ctrl.Parent;
                frm.Dock = ctrl.Dock;

                ctrl.Parent = frm;
                ctrl.Dock = DockStyle.Fill;
                
                EventHandler _ctrl_visable_changed = (s, e) => {
                    frm.Visible = true;
                    frm.Visible = ctrl.Visible; 
                };

                ctrl.VisibleChanged += _ctrl_visable_changed;
                frm.FormClosed += (s,e) => { 
                    ctrl.Dock = frm.Dock; 
                    ctrl.Parent = (Control)frm.Tag;
                    ctrl.VisibleChanged -= _ctrl_visable_changed;
                    if (onclose != null)
                    {
                        onclose.Invoke(s, e);
                    }
                };
            }
            frm.Show();
            frm.Left = this.Right;
            frm.Top = this.Top;
            frm.Height = this.Height;
        }


        class ConsoleLogTextWriter : System.IO.TextWriter
        {
            TextBox txtLog;
            public ConsoleLogTextWriter(TextBox txt_log) : base() { this.txtLog = txt_log; }

            public override Encoding Encoding { get { return Encoding.UTF8; } }

            public override void Write(string value)
            {
                if (txtLog != null)
                {
                    try { txtLog.Invoke((Action)delegate { txtLog.Text += value; }); } catch { }
                }
            }
            public override void WriteLine(string value)
            {
                if (txtLog != null)
                {
                    try { txtLog.Invoke((Action)delegate { txtLog.Text += value + "\r\n"; }); }catch { }
                }
            }
            public override void Close()
            {
                base.Close();
            }
        }

        ConsoleLogTextWriter clog;

        public FrmBrowser()
        {
            InitializeComponent();

            lblToggleConsole.Top = grpScriptBody.Height - 17;

            this.Icon = IMKCode.API.IconHelper.ExtractIcon(0);

            clog = new ConsoleLogTextWriter(axtConsole);
            Console.SetOut(clog);


        }

        public void Navigate(string url)
        {
            blinkBrowser1.Url = url;
            blinkBrowser1.Dock = DockStyle.Fill;
        }

        private void FrmBrowser_Load(object sender, EventArgs e)
        {
            ScriptUtils.RM = new RecordManager(this.Handle);
            recordManager = new RecordManager(this.Handle);

            m_script_folder = new System.IO.DirectoryInfo(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "scripts"));
            if (!m_script_folder.Exists) { m_script_folder.Create(); }

            m_target_folder = new System.IO.DirectoryInfo(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "targets"));
            if (!m_target_folder.Exists) { m_target_folder.Create(); }

            blinkBrowser1.OnUrlChange2Call += BlinkBrowser1_OnUrlChange2Call;
            blinkBrowser1.OnUrlChangeCall += BlinkBrowser1_OnUrlChangeCall;
            blinkBrowser1.OnTitleChangeCall += BlinkBrowser1_OnTitleChangeCall;
            blinkBrowser1.DocumentReadyCallback += BlinkBrowser1_DocumentReadyCallback;
            blinkBrowser1.OnDownloadFile += BlinkBrowser1_OnDownloadFile;
            blinkBrowser1.OnCreateViewEvent += BlinkBrowser1_OnCreateViewEvent;
            blinkBrowser1.Url = "https://www.baidu.com/";

            txtScriptBody.Text = @"/** 请选择脚本文件 **/";

            IMKCode.Api.RegisterHotKey(this.Handle, 9001, (int)IMKCode.Api.KeyModifiers.None, (int)Keys.F10);

            System.Threading.ThreadPool.QueueUserWorkItem(delegate
            {
                loadScriptFiles();
                loadTargetFiles();
            });

            timer1.Enabled = true;
        }



        private IntPtr BlinkBrowser1_OnCreateViewEvent(IntPtr webView, IntPtr param, wkeNavigationType navigationType, string url)
        {
            blinkBrowser1.Url = url;
            return webView;
        }

        private void BlinkBrowser1_OnDownloadFile(string url)
        {

        }


        private void BlinkBrowser1_DocumentReadyCallback()
        {
        }

        private void BlinkBrowser1_OnTitleChangeCall(string title)
        {
            this.Text = title + " -- IMKCode with MiniBlink";
        }

        private void BlinkBrowser1_OnUrlChange2Call(string url)
        {
            this.cbxUrl.Text = url;
        }
        private void BlinkBrowser1_OnUrlChangeCall(string url)
        {
            //this.cbxUrl.Text = blinkBrowser1.Url; //|x
            //this.cbxUrl.Text = url; //| iframe
        }
        private void cbxUrl_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbxUrl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                blinkBrowser1.Url = ((cbxUrl.Text.IndexOf(":") <= 0) ? "http://" : "") + cbxUrl.Text;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            blinkBrowser1.InvokeJS("history.back()");
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            blinkBrowser1.InvokeJS("history.forward()");

        }

        private void btnToggleTools_Click(object sender, EventArgs e)
        {
            //blinkBrowser1.InvokeJS("alert('abc123')");
            m_flag_show_script_tools = !m_flag_show_script_tools;
            splitContainer1.SplitterDistance = this.Width - (m_flag_show_script_tools ? 200 : 0);
            btnToggleTools.Text = m_flag_show_script_tools ? "→" : "←"; //| 》《 ❯❮ >< →←
            if (m_flag_show_script_tools)
            {
                splitContainer1.Panel2.Show();
            }
            else
            {
                splitContainer1.Panel2.Hide();
            }
        }

        private void loadScriptFiles()
        {
            var files = m_script_folder.GetFiles("*.abs", System.IO.SearchOption.AllDirectories);
            this.Invoke((Action)delegate
            {
                cbxScriptFiles.Items.Clear();
                foreach (var file in files)
                {
                    cbxScriptFiles.Items.Add(file);
                }

                lblScriptsCount.Text = string.Format("({0})", cbxScriptFiles.Items.Count);
            });
        }

        private void loadTargetFiles()
        {
            var files = m_target_folder.EnumerateFiles().Where(n => System.Text.RegularExpressions.Regex.IsMatch(n.Name, @"^.+\.(jpg|jpeg|png|bmp)$"));
            this.Invoke((Action)delegate
            {
                lvTargets.Items.Clear();
                imgListTargets.Images.Clear();
                ScriptUtils.TMG.Clear();
                foreach (var file in files)
                {
                    ListViewItem li = new ListViewItem();
                    li.ImageKey = file.Name;
                    li.Text = file.Name;
                    li.ToolTipText = file.FullName;
                    using (Image _img = Image.FromFile(file.FullName))
                    {
                        imgListTargets.Images.Add(file.Name, new System.Drawing.Bitmap(_img));
                        try { ScriptUtils.TMG.Add(file.Name, new System.Drawing.Bitmap(_img)); } catch { /*重名*/}
                        li.SubItems.Add(_img.Width + "x" + _img.Height);
                    }
                    li.SubItems.Add(file.Length.ToString());
                    li.SubItems.Add(file.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss"));

                    lvTargets.Items.Add(li);
                }
                lblTargetsCount.Text = string.Format("({0})", lvTargets.Items.Count);
            });
        }

        private void cbxScriptFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.IO.FileInfo _fi = cbxScriptFiles.SelectedItem as System.IO.FileInfo;
            if (_fi != null && _fi.Exists)
            {
                txtScriptBody.Text = System.IO.File.ReadAllText(_fi.FullName);
                lblScriptStatus.Text = _fi.Name +"/"+ _fi.Length + "bytes/" + ScriptCore.calcScriptTimes(txtScriptBody.Text) + "s";
            }
            else
            {
                txtScriptBody.Text = "/** IMK: file "+ _fi.FullName +" is not found **/";
            }
        }

        private void btnScriptRecord_Click(object sender, EventArgs e)
        {
            if (m_flag_work_area_full_screen && MessageBox.Show("* 注意当前为【全局坐标】方式，窗口内坐标可能发生变化！\r\n\r\n是否继续以全局坐标录制？", "全局坐标：适合窗口外操作", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
            {
                return;
            }
            else if (!m_flag_work_area_full_screen && MessageBox.Show("* 注意当前为【局部坐标】方式，窗口以外坐标可能发生变化！\r\n\r\n是否继续以局部坐标录制？", "局部坐标：适合窗口内操作", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
            {
                return;
            }

            if (m_flag_script_record_status == 0)
            {
                m_flag_script_record_status = 1;

                recordHook.InstallHook(true);

                btnScriptRecord.Enabled = false;

                System.Threading.ThreadPool.QueueUserWorkItem(delegate { this.scriptRecording(); });

            }
        }

        private void scriptRecording()
        {
            _setScriptStatus("准备录制(3)");
            System.Threading.Thread.Sleep(1000);
            if (!IsRecordReadying) { _setScriptStatus("录制"); return; }
            _setScriptStatus("准备录制(2)");
            System.Threading.Thread.Sleep(1000);
            if (!IsRecordReadying) { _setScriptStatus("录制"); return; }
            _setScriptStatus("准备录制(1)");
            System.Threading.Thread.Sleep(1000);
            if (!IsRecordReadying) { _setScriptStatus("录制"); return; }
            m_flag_script_record_status = 2;

            DateTime _dtm = DateTime.Now;
            recordManager.StartRec(); //| 开始记录和走时
            recordHook.OnMouseActivity += RecordHook_OnMouseActivity;
            recordHook.OnKeyActivity += RecordHook_OnKeyActivity;

            while (IsRecording)
            {
                System.Threading.Thread.Sleep(100);
                if (!IsRecording)
                {
                    _setScriptStatus("录制结束");
                    recordManager.StopRec(); //| 结束记录
                    recordHook.OnMouseActivity -= RecordHook_OnMouseActivity;
                    recordHook.OnKeyActivity -= RecordHook_OnKeyActivity;
                    recordHook.UnInstallHook();
                    m_flag_script_record_status = 0;
                    this.Invoke((Action)delegate
                    {
                        txtScriptBody.Text = recordManager.Scripts;
                        lblScriptStatus.Text = txtScriptBody.Text.Length + "bytes/" + ScriptCore.calcScriptTimes(txtScriptBody.Text) + "s";
                        btnScriptRecord.Enabled = true;
                    });
                    return;
                }
                else
                {
                    var _tm = DateTime.Now.Subtract(_dtm).TotalSeconds.ToString("0.00");
                    _setScriptStatus("录制中..." + _tm +"\t（按Esc结束）");
                }
            }
        }

        private void RecordHook_OnMouseActivity(object sender, MouseEventExArgs e)
        {
            //Point _point = this.blinkBrowser1.PointToClient(new Point(e.X, e.Y));
            //this.Text = e.X + ", " + e.Y +" => "+ _point.X +","+ _point.Y;

            if (e.wmEvent == ButtonStatus.Down)
            {
                recordManager.RecMouseMove(e.X, e.Y);
                recordManager.RecMouseDown(e.Button);
            }
            else if (e.wmEvent == ButtonStatus.Up)
            {
                recordManager.RecMouseMove(e.X, e.Y);
                recordManager.RecMouseUp(e.Button);
            }

        }
        private void RecordHook_OnKeyActivity(object sender, KeyEventExArgs e)
        {
            if (e.wmEvent == ButtonStatus.Down)
            {
                if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.F10)
                {
                    if (IsRecording) { m_flag_script_record_status = 3; }
                }
                else
                {
                    recordManager.RecKeyDown(e.KeyCode);
                }
            }
            else if (e.wmEvent == ButtonStatus.Up)
            {
                if (IsRecording)
                {
                    recordManager.RecKeyUp(e.KeyCode);
                }
            }
        }


        private void _setScriptStatus(string text)
        {
            this.Invoke((Action)delegate { lblScriptStatus.Text = text; });
        }

        private void btnScriptSave_Click(object sender, EventArgs e)
        {
            ScriptCore scTemp;
            if (checkCode(txtScriptBody.Text, out scTemp))
            {
                SaveFileDialog saveDiag = new SaveFileDialog();
                saveDiag.Filter = "Script Files|*.abs|All Files|*.*";
                saveDiag.DefaultExt = ".abs";
                saveDiag.InitialDirectory = "scripts";
                if (saveDiag.ShowDialog() == DialogResult.OK)
                {
                    System.IO.File.WriteAllText(saveDiag.FileName, txtScriptBody.Text);
                    this.loadScriptFiles();
                }
            }
            else
            {
                MessageBox.Show("Script error: " + scTemp.CompileMsg);
            }
        }

        private bool checkCode(string code, out ScriptCore scc)
        {
            scc = new ScriptCore(txtScriptBody.Text);
            if (scc.compiler())
            {
                return true;
            }
            return false;
        }

        private void btnScriptRun_Click(object sender, EventArgs e)
        {
            //recordManager.Execute(txtScriptBody.Text);
            //return;
            ScriptCore scTemp;
            if (checkCode(txtScriptBody.Text, out scTemp))
            {
                btnScriptRun.Text = "运行中";
                btnScriptRun.Enabled = false;
                _setScriptStatus("运行中（按F10中结束)");
                m_flag_script_runing_status = true;
                System.Threading.ThreadPool.QueueUserWorkItem(delegate
                {
                    scTemp.Run();
                    while (m_flag_script_runing_status)
                    {
                        System.Threading.Thread.Sleep(100);
                    }
                    scTemp.Abort();
                    this.Invoke((Action)delegate { btnScriptRun.Text = "运行"; btnScriptRun.Enabled = true; _setScriptStatus("结束运行"); });
                });
            }
            else
            {
                MessageBox.Show("Script error: " + scTemp.CompileMsg);
            }
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;
            switch (m.Msg)
            {
                case WM_HOTKEY:
                    {
                        switch (m.WParam.ToInt32())
                        {
                            case 9001:
                            {
                                if (IsRecording)
                                {
                                    m_flag_script_record_status = 3;
                                }
                                else if (m_flag_script_runing_status)
                                {
                                    m_flag_script_runing_status = false;
                                }
                                break;
                            }
                        }
                        break;
                    }
            }

            base.WndProc(ref m);
        }

        private void FrmBrowser_FormClosed(object sender, FormClosedEventArgs e)
        {
            IMKCode.Api.UnregisterHotKey(this.Handle, 9001);
            timer1.Enabled = false;
        }

        private void tbsAddTarget_Click(object sender, EventArgs e)
        {
            Rectangle rectArea;

            if (m_flag_work_area_full_screen)
            {
                rectArea = Screen.AllScreens[0].Bounds;
            }
            else
            {
                rectArea = this.RectangleToScreen(this.ClientRectangle);
            }

            Image img = new Bitmap(rectArea.Width, rectArea.Height);
            Graphics g = Graphics.FromImage(img);
            g.CopyFromScreen(rectArea.Location, new Point(0, 0), rectArea.Size);
            FrmScreen body = new FrmScreen();
            body.BackgroundImage = img;
            body.BackgroundImageLayout = ImageLayout.None;
            body.Icon = this.Icon;
            if (body.ShowDialog() == DialogResult.OK)
            {
                this.loadTargetFiles();
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (m_flag_work_area_full_screen)
            {
                ScriptUtils.WorkRect = Screen.AllScreens[0].Bounds;
            }
            else
            {
                ScriptUtils.WorkRect = this.RectangleToScreen(this.ClientRectangle);
            }

        }
        private void cmnuTargetItemTestFind_Click(object sender, EventArgs e)
        {
            _setScriptStatus("Finding...");
            if (lvTargets.SelectedItems.Count > 0)
            {
                string _itemName = lvTargets.SelectedItems[0].Text;

                System.Threading.ThreadPool.QueueUserWorkItem(delegate
                {
                    DateTime _st = DateTime.Now;
                    var points = ScriptUtils.FindTarget(_itemName);
                    var _ms = DateTime.Now.Subtract(_st).TotalMilliseconds.ToString("0.##");
                    _setScriptStatus("Point: " + points.Length + " ("+ _ms +"ms)");

                    foreach (var pt in points)
                    {
                        System.Threading.Thread.Sleep(1000);
                        recordManager.MouseMove(pt.X.ToString(), pt.Y.ToString());
                    }
                });
            }
        }

        private void cmnuTargetItemExplorer_Click(object sender, EventArgs e)
        {
            if (lvTargets.SelectedItems.Count > 0)
            {
                Utils.ExplorerTo(lvTargets.SelectedItems[0].ToolTipText);
            }
        }

        private void cmnuTargetItemDelete_Click(object sender, EventArgs e)
        {
            if (lvTargets.SelectedItems.Count > 0)
            {
                ListViewItem li = lvTargets.SelectedItems[0];
                string _file = li.ToolTipText;
                if (MessageBox.Show("确认要删除此项吗？", "删除", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    Console.WriteLine("IMK: Confirm Delete " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                    ScriptUtils.TMG.Remove(li.Text);
                    imgListTargets.Images.RemoveByKey(li.Text);
                    lvTargets.Items.Remove(li);

                    //try { System.IO.File.Delete(_file); } catch { }
                    try { IMKCode.Api.MoveToTrash(_file); } catch { }
                }
            }
        }

        private void cmnuTargetItemCodeFind_Click(object sender, EventArgs e)
        {
            if (lvTargets.SelectedItems.Count > 0)
            {
                ListViewItem li = lvTargets.SelectedItems[0];
                string _file = li.ToolTipText;
                txtScriptBody.Text += string.Format(
@"
var points = FindTarget(""{0}"");
foreach (var pt in points)
{{
  MouseMove(pt.X, pt.Y);
}}
", li.Text);
            }
        }

        private void lbtnRefreshScripts_Click(object sender, EventArgs e)
        {
            loadScriptFiles();
        }

        private void lbtnRefreshTargets_Click(object sender, EventArgs e)
        {
            loadTargetFiles();
        }

        private void lbtnTargetsToggleView_Click(object sender, EventArgs e)
        {
            //"☰☷"
            lvTargets.View = lvTargets.View == View.LargeIcon ? View.Details : View.LargeIcon;
            lbtnTargetsToggleView.Text = lvTargets.View == View.LargeIcon ? "☰" : "☷";

        }

        private void lblToggleConsole_Click(object sender, EventArgs e)
        {
            splitConsole.Visible =
            grpConsole.Visible = !grpConsole.Visible;
            lblToggleConsole.Text = grpConsole.Visible ? "〓" : "▀"; //⬓⬜□■〓▀

        }

        private void lbtnClearLog_Click(object sender, EventArgs e)
        {
            axtConsole.Text = "";
        }
        private void lblConsolePop_Click(object sender, EventArgs e)
        {
            PopControl(grpConsole, "Console", (a,b) => { splitTargetList.SendToBack(); grpTargets.SendToBack();});
        }

        private void tbsWorkArea_Click(object sender, EventArgs e)
        {
            m_flag_work_area_full_screen = !m_flag_work_area_full_screen;
            tbsWorkArea.Image = m_flag_work_area_full_screen ? global::AutoBrowser.Properties.Resources.pub_apps : global::AutoBrowser.Properties.Resources.app_only;
            tbsWorkArea.ToolTipText = m_flag_work_area_full_screen ? "查找区域(当前为全屏)" : "查找区域(当前仅应用)";
            ScriptUtils.RM.IsAbsolutePoint = m_flag_work_area_full_screen;
            recordManager.IsAbsolutePoint = m_flag_work_area_full_screen;
        }

        private void lbtnCursorInfoClose_Click(object sender, EventArgs e)
        {
            grpCursorInfo.Visible = false;
        }

        private void tbsCursor_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                grpCursorInfo.Visible = true;
                Cursor.Current = Cursors.Cross;
                mouseCaptionBegin();
            }
        }


        bool flag_cursorColor;
        const int cc_sz = 70;
        int cc_oo = 4;
        int cc_pw = (int)(cc_sz / 7);
        Color cc_c;
        Color cc_cursorColor;
        Point cc_cursorPoint;
        Rectangle cc_rect;

        Bitmap cc_bmp_display;
        Graphics cc_g_display;
        Pen ccPen;
        SolidBrush ccBrush;
        Bitmap cc_bmp_caption;
        Graphics cc_g_caption;

        private void mouseCaptionBegin()
        {
            if (!flag_cursorColor)
            {
                flag_cursorColor = true;
                cc_bmp_display = new Bitmap(cc_sz, cc_sz);
                cc_g_display = Graphics.FromImage(cc_bmp_display);
                ccPen = new Pen(Color.Black);
                ccPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

                ccBrush = new SolidBrush(Color.Black);
                cc_bmp_caption = new Bitmap(cc_oo * 2 + 1, cc_oo * 2 + 1);
                cc_g_caption = Graphics.FromImage(cc_bmp_caption);

                System.Threading.ThreadPool.QueueUserWorkItem(delegate
                {
                    while(flag_cursorColor)
                    {
                        try { mouseCaptioning(); } catch { }
                        System.Threading.Thread.Sleep(100);
                        if ((Control.MouseButtons & MouseButtons.Left) != MouseButtons.Left)
                        {
                            mouseCaptionEnd();
                        }
                    }
                });
            }
        }
        private void mouseCaptionEnd()
        {
            flag_cursorColor = false;
            if (cc_g_caption != null) { cc_g_caption.Dispose(); cc_g_caption = null; }
            if (cc_g_display != null) { cc_g_display.Dispose(); cc_g_display = null; }
        }

        private void mouseCaptioning()
        {
            Point ppt = Control.MousePosition;
            capturePoint(ppt);
        }

        private void capturePoint(Point ppt)
        {
            cc_g_caption.CopyFromScreen(ppt.X - cc_oo, ppt.Y - cc_oo, 0, 0, cc_bmp_caption.Size);
            for (int xx = -cc_oo; xx <= cc_oo; xx++)
            {
                for (int yy = -cc_oo; yy <= cc_oo; yy++)
                {
                    cc_c = cc_bmp_caption.GetPixel(cc_oo - xx, cc_oo - yy);
                    cc_rect = new Rectangle(cc_pw * (cc_oo - 1 - xx), cc_pw * (cc_oo - 1 - yy), cc_pw, cc_pw);
                    ccBrush.Color = cc_c;
                    cc_g_display.FillRectangle(ccBrush, cc_rect);
                    if (xx == 0 && yy == 0)
                    {
                        cc_g_display.DrawRectangle(ccPen, cc_rect);

                        this.cc_cursorColor = cc_c;

                        this.Invoke((Action)delegate
                        {
                            int _rx = ppt.X;
                            int _ry = ppt.Y;
                            recordManager.IfPointToClient(ref _rx, ref _ry);

                            this.cc_cursorPoint = new Point(_rx, _ry);

                            txtCursorInfo.Text = string.Format("POS({0},{1})\r\nX={2},Y={3}\r\nRGB({4},{5},{6})\r\nHEX:0x{7}{8}{9}\r\nDEC:{10}",
                                ppt.X, ppt.Y,
                                _rx, _ry,
                                cc_c.R, cc_c.G, cc_c.B,
                                cc_c.R.ToString("X2"), cc_c.G.ToString("X2"), cc_c.B.ToString("X2"),
                                cc_c.GetValue());
                        });
                    }
                }
            }
            this.Invoke((Action)delegate
            {
                picCursorInfo.Image = cc_bmp_display;
            });
        }

        private void ReGetPointColor()
        {
            int _x = this.cc_cursorPoint.X;
            int _y = this.cc_cursorPoint.Y;
            recordManager.IfPointToScreen(ref _x, ref _y);
            Point ppt = new Point(_x, _y);

            if (cc_bmp_caption == null){ cc_bmp_caption = new Bitmap(cc_oo * 2 + 1, cc_oo * 2 + 1);}
            if (cc_bmp_display == null) { cc_bmp_display = new Bitmap(cc_sz, cc_sz); }

            using (cc_g_caption = Graphics.FromImage(cc_bmp_caption))
            {
                using (cc_g_display = Graphics.FromImage(cc_bmp_display))
                {
                    capturePoint(ppt);
                }
            }
        }

        private void cmnuScriptRunCompileOnly_Click(object sender, EventArgs e)
        {
            ScriptCore scTemp;
            if (checkCode(txtScriptBody.Text, out scTemp))
            {
                _setScriptStatus("编译成功");
            }
            else
            {
                MessageBox.Show("Script error: " + scTemp.CompileMsg);
            }
        }

        private void cmnuScriptRunOnce_Click(object sender, EventArgs e)
        {

            ScriptCore scTemp;
            if (checkCode(txtScriptBody.Text, out scTemp))
            {
                btnScriptRun.Text = "运行中";
                btnScriptRun.Enabled = false;
                _setScriptStatus("运行中（按F10中结束)");
                m_flag_script_runing_status = true;
                System.Threading.ThreadPool.QueueUserWorkItem(delegate
                {
                    scTemp.RunOnce();
                    this.Invoke((Action)delegate { btnScriptRun.Text = "运行"; btnScriptRun.Enabled = true; _setScriptStatus("结束运行"); });
                });
            }
            else
            {
                MessageBox.Show("Script error: " + scTemp.CompileMsg);
            }

        }

        private void cmnuCursorScriptColorValue_Click(object sender, EventArgs e)
        {
            txtScriptBody.Text += string.Format(
@"
//| POS({0},{1}): RGB({2},{3},{4}), HEX:0x{5}, DEC:{6}
if (GetColor({0},{1}).GetValue() == 0x{5})
{{
  MsgBox(""颜色值相同: "" + GetColor({0},{1}));
}}
else
{{
  MsgBox(""颜色值不相同!"" + GetColor({0},{1}));
}}
", cc_cursorPoint.X, cc_cursorPoint.Y
, cc_cursorColor.R, cc_cursorColor.G, cc_cursorColor.B
, cc_cursorColor.GetValue().ToString("X2")
, cc_cursorColor.GetValue());
        }

        private void cmnuCursorScriptColorRGB_Click(object sender, EventArgs e)
        {
            txtScriptBody.Text += string.Format(
@"
//| POS({0},{1}): RGB({2},{3},{4}), HEX:0x{5}, DEC:{6}
var title = ""POS({0},{1})"";
var color = GetColor({0},{1});
var oldValue = 0x{5};
MsgBox(""红色值(R):""+ color.R +""\r\n R={2} ? ""+ (color.R=={2}), title);
MsgBox(""绿色值(G):""+ color.G +""\r\n G={3} ? ""+ (color.G=={3}), title);
MsgBox(""蓝色值(B):""+ color.B +""\r\n B={4} ? ""+ (color.B=={4}), title);
MsgBox(""颜色等于某值？\r\n""+ color.GetValue() +""=""+ oldValue +""\r\nResult: ""+ (color.GetValue() == oldValue));
MsgBox(""颜色相似某值(±5)？\r\nLikeColorValue(""+ color.GetValue() +"", ""+ oldValue +"");\r\nResult: ""+ color.LikeColorValue(oldValue));
MsgBox(""颜色相似某值(±15)？\r\nLikeColorValue(""+ color.GetValue() +"", ""+ oldValue +"", 15);\r\nResult: ""+ LikeColorValue(color,oldValue,15));
", cc_cursorPoint.X, cc_cursorPoint.Y
, cc_cursorColor.R, cc_cursorColor.G, cc_cursorColor.B
, cc_cursorColor.GetValue().ToString("X2")
, cc_cursorColor.GetValue());
        }

        private void cmnuCursorScriptLClick_Click(object sender, EventArgs e)
        {

            txtScriptBody.Text += string.Format(
@"
//| POS({0},{1}), Tip:{2}
MouseMove({0},{1});
MouseDown(""Left"");
Sleep(200);
MouseUp(""Left"");
            ", cc_cursorPoint.X, cc_cursorPoint.Y,
m_flag_work_area_full_screen ? "public point" : "local point");
        }

        private void cmnuCursorScriptRClick_Click(object sender, EventArgs e)
        {

            txtScriptBody.Text += string.Format(
@"
//| POS({0},{1}), Tip:{2}
MouseMove({0},{1});
MouseDown(""Right"");
Sleep(200);
MouseUp(""Right"");
            ", cc_cursorPoint.X, cc_cursorPoint.Y,
m_flag_work_area_full_screen ? "public point" : "local point");
        }

        private void cmnuCursorGetColor_Click(object sender, EventArgs e)
        {
            ReGetPointColor();
        }

        private void cmnuCursorMoveTo_Click(object sender, EventArgs e)
        {
            int _x = this.cc_cursorPoint.X;
            int _y = this.cc_cursorPoint.Y;
            recordManager.MouseMove(_x.ToString(), _y.ToString());
        }

        private void cmnuCursorScriptLikeColor_Click(object sender, EventArgs e)
        {
            txtScriptBody.Text += string.Format(
@"
//| POS({0},{1}): RGB({2},{3},{4}), HEX:0x{5}, DEC:{6}
if (GetColor({0},{1}).GetValue().LikeColorValue(0x{5}))
{{
  MsgBox(""颜色值相近(±5): "" + GetColor({0},{1}).GetColorDiff(0x{5}));
}}
else
{{
  MsgBox(""颜色值不相近(±5)!"" + GetColor({0},{1}).GetColorDiff(0x{5}));
}}


if (GetColor({0},{1}).GetValue().LikeColorValue(0x{5}, 15))
{{
  MsgBox(""颜色值相近(±15): "" + GetColor({0},{1}).GetColorDiff(0x{5}));
}}
else
{{
  MsgBox(""颜色值不相近(±15)!"" + GetColor({0},{1}).GetColorDiff(0x{5}));
}}
", cc_cursorPoint.X, cc_cursorPoint.Y
, cc_cursorColor.R, cc_cursorColor.G, cc_cursorColor.B
, cc_cursorColor.GetValue().ToString("X2")
, cc_cursorColor.GetValue());
        }

    }
}
