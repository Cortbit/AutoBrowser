
namespace AutoBrowser
{
    partial class FrmBrowser
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pelBrowser = new System.Windows.Forms.Panel();
            this.blinkBrowser1 = new MiniBlinkPinvoke.BlinkBrowser();
            this.pelUrl = new System.Windows.Forms.Panel();
            this.btnToggleTools = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.cbxUrl = new System.Windows.Forms.ComboBox();
            this.grpScriptBody = new System.Windows.Forms.GroupBox();
            this.txtScriptBody = new System.Windows.Forms.TextBox();
            this.grpCursorInfo = new System.Windows.Forms.GroupBox();
            this.picCursorInfo = new System.Windows.Forms.PictureBox();
            this.txtCursorInfo = new System.Windows.Forms.TextBox();
            this.cmnuCursorInfo = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmnuCursorScript = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuCursorScriptLClick = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuCursorScriptRClick = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmnuCursorScriptColorRGB = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuCursorScriptColorValue = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuCursorScriptLikeColor = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuCursorMoveTo = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuCursorGetColor = new System.Windows.Forms.ToolStripMenuItem();
            this.lbtnCursorInfoClose = new System.Windows.Forms.Label();
            this.splitTargetList = new System.Windows.Forms.Splitter();
            this.grpTargets = new System.Windows.Forms.GroupBox();
            this.lblTargetsCount = new System.Windows.Forms.Label();
            this.lbtnTargetsToggleView = new System.Windows.Forms.Label();
            this.lbtnRefreshTargets = new System.Windows.Forms.Label();
            this.lvTargets = new System.Windows.Forms.ListView();
            this.colKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colBytes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmnuTargetItems = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmnuTargetItemTestFind = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuTargetItemCodes = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuTargetItemCodeFind = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmnuTargetItemExplorer = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuTargetItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.imgListTargets = new System.Windows.Forms.ImageList(this.components);
            this.grpScripts = new System.Windows.Forms.GroupBox();
            this.lblScriptsCount = new System.Windows.Forms.Label();
            this.lbtnRefreshScripts = new System.Windows.Forms.Label();
            this.lblScriptStatus = new System.Windows.Forms.Label();
            this.cbxScriptFiles = new System.Windows.Forms.ComboBox();
            this.btnScriptRun = new System.Windows.Forms.Button();
            this.cmnuScriptRun = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmnuScriptRunCompileOnly = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuScriptRunOnce = new System.Windows.Forms.ToolStripMenuItem();
            this.btnScriptSave = new System.Windows.Forms.Button();
            this.btnScriptRecord = new System.Windows.Forms.Button();
            this.grpLookInfos = new System.Windows.Forms.GroupBox();
            this.pelBorderMP = new System.Windows.Forms.Panel();
            this.chkAutoMP = new System.Windows.Forms.CheckBox();
            this.pelBorderHP = new System.Windows.Forms.Panel();
            this.chkAutoHP = new System.Windows.Forms.CheckBox();
            this.lblTipHP = new System.Windows.Forms.Label();
            this.lblLookMPValue = new System.Windows.Forms.Label();
            this.lblTipMP = new System.Windows.Forms.Label();
            this.lblLookHPValue = new System.Windows.Forms.Label();
            this.tbsBar = new System.Windows.Forms.ToolStrip();
            this.tbsWorkArea = new System.Windows.Forms.ToolStripButton();
            this.tbsCursor = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tbsAddTarget = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tbsLookHP = new System.Windows.Forms.ToolStripButton();
            this.tbsLookMP = new System.Windows.Forms.ToolStripButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.grpConsole = new System.Windows.Forms.GroupBox();
            this.lblToggleConsole = new System.Windows.Forms.Label();
            this.axtConsole = new System.Windows.Forms.TextBox();
            this.splitConsole = new System.Windows.Forms.Splitter();
            this.lbtnClearLog = new System.Windows.Forms.Label();
            this.lblConsolePop = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.txtConsole = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pelBrowser.SuspendLayout();
            this.pelUrl.SuspendLayout();
            this.grpScriptBody.SuspendLayout();
            this.grpCursorInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCursorInfo)).BeginInit();
            this.cmnuCursorInfo.SuspendLayout();
            this.grpTargets.SuspendLayout();
            this.cmnuTargetItems.SuspendLayout();
            this.grpScripts.SuspendLayout();
            this.cmnuScriptRun.SuspendLayout();
            this.grpLookInfos.SuspendLayout();
            this.pelBorderMP.SuspendLayout();
            this.pelBorderHP.SuspendLayout();
            this.tbsBar.SuspendLayout();
            this.grpConsole.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pelBrowser);
            this.splitContainer1.Panel1.Controls.Add(this.pelUrl);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grpScriptBody);
            this.splitContainer1.Panel2.Controls.Add(this.splitConsole);
            this.splitContainer1.Panel2.Controls.Add(this.grpConsole);
            this.splitContainer1.Panel2.Controls.Add(this.grpCursorInfo);
            this.splitContainer1.Panel2.Controls.Add(this.splitTargetList);
            this.splitContainer1.Panel2.Controls.Add(this.grpTargets);
            this.splitContainer1.Panel2.Controls.Add(this.grpScripts);
            this.splitContainer1.Panel2.Controls.Add(this.grpLookInfos);
            this.splitContainer1.Panel2.Controls.Add(this.tbsBar);
            this.splitContainer1.Size = new System.Drawing.Size(800, 520);
            this.splitContainer1.SplitterDistance = 610;
            this.splitContainer1.TabIndex = 0;
            // 
            // pelBrowser
            // 
            this.pelBrowser.Controls.Add(this.blinkBrowser1);
            this.pelBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pelBrowser.Location = new System.Drawing.Point(0, 28);
            this.pelBrowser.Name = "pelBrowser";
            this.pelBrowser.Size = new System.Drawing.Size(610, 492);
            this.pelBrowser.TabIndex = 1;
            // 
            // blinkBrowser1
            // 
            this.blinkBrowser1._ZoomFactor = 0F;
            this.blinkBrowser1.CookiePath = null;
            this.blinkBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.blinkBrowser1.HTML = "";
            this.blinkBrowser1.Location = new System.Drawing.Point(0, 0);
            this.blinkBrowser1.Name = "blinkBrowser1";
            this.blinkBrowser1.Size = new System.Drawing.Size(610, 492);
            this.blinkBrowser1.TabIndex = 1;
            this.blinkBrowser1.Text = "blinkBrowser1";
            this.blinkBrowser1.Url = "";
            this.blinkBrowser1.ZoomFactor = 0F;
            // 
            // pelUrl
            // 
            this.pelUrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pelUrl.Controls.Add(this.btnToggleTools);
            this.pelUrl.Controls.Add(this.btnNext);
            this.pelUrl.Controls.Add(this.btnBack);
            this.pelUrl.Controls.Add(this.cbxUrl);
            this.pelUrl.Dock = System.Windows.Forms.DockStyle.Top;
            this.pelUrl.Location = new System.Drawing.Point(0, 0);
            this.pelUrl.Name = "pelUrl";
            this.pelUrl.Size = new System.Drawing.Size(610, 28);
            this.pelUrl.TabIndex = 0;
            // 
            // btnToggleTools
            // 
            this.btnToggleTools.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnToggleTools.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggleTools.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnToggleTools.Location = new System.Drawing.Point(577, 3);
            this.btnToggleTools.Name = "btnToggleTools";
            this.btnToggleTools.Size = new System.Drawing.Size(23, 20);
            this.btnToggleTools.TabIndex = 12;
            this.btnToggleTools.Text = "→";
            this.toolTip1.SetToolTip(this.btnToggleTools, "隐藏/显示 右侧操作面板");
            this.btnToggleTools.UseVisualStyleBackColor = true;
            this.btnToggleTools.Click += new System.EventHandler(this.btnToggleTools_Click);
            // 
            // btnNext
            // 
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Location = new System.Drawing.Point(26, 3);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(23, 20);
            this.btnNext.TabIndex = 12;
            this.btnNext.Text = ">";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnBack
            // 
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Location = new System.Drawing.Point(4, 3);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(23, 20);
            this.btnBack.TabIndex = 11;
            this.btnBack.Text = "<";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // cbxUrl
            // 
            this.cbxUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxUrl.FormattingEnabled = true;
            this.cbxUrl.Location = new System.Drawing.Point(54, 3);
            this.cbxUrl.Name = "cbxUrl";
            this.cbxUrl.Size = new System.Drawing.Size(517, 20);
            this.cbxUrl.TabIndex = 0;
            this.cbxUrl.SelectedIndexChanged += new System.EventHandler(this.cbxUrl_SelectedIndexChanged);
            this.cbxUrl.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cbxUrl_KeyUp);
            // 
            // grpScriptBody
            // 
            this.grpScriptBody.Controls.Add(this.lblToggleConsole);
            this.grpScriptBody.Controls.Add(this.txtScriptBody);
            this.grpScriptBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpScriptBody.Location = new System.Drawing.Point(0, 164);
            this.grpScriptBody.Name = "grpScriptBody";
            this.grpScriptBody.Size = new System.Drawing.Size(186, 75);
            this.grpScriptBody.TabIndex = 2;
            this.grpScriptBody.TabStop = false;
            this.grpScriptBody.Text = "脚本内容";
            // 
            // txtScriptBody
            // 
            this.txtScriptBody.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtScriptBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtScriptBody.Location = new System.Drawing.Point(3, 17);
            this.txtScriptBody.Multiline = true;
            this.txtScriptBody.Name = "txtScriptBody";
            this.txtScriptBody.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtScriptBody.Size = new System.Drawing.Size(180, 55);
            this.txtScriptBody.TabIndex = 0;
            this.txtScriptBody.WordWrap = false;
            // 
            // grpCursorInfo
            // 
            this.grpCursorInfo.Controls.Add(this.picCursorInfo);
            this.grpCursorInfo.Controls.Add(this.txtCursorInfo);
            this.grpCursorInfo.Controls.Add(this.lbtnCursorInfoClose);
            this.grpCursorInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpCursorInfo.Location = new System.Drawing.Point(0, 332);
            this.grpCursorInfo.Name = "grpCursorInfo";
            this.grpCursorInfo.Size = new System.Drawing.Size(186, 100);
            this.grpCursorInfo.TabIndex = 7;
            this.grpCursorInfo.TabStop = false;
            this.grpCursorInfo.Text = "坐标信息";
            this.grpCursorInfo.Visible = false;
            // 
            // picCursorInfo
            // 
            this.picCursorInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.picCursorInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picCursorInfo.Location = new System.Drawing.Point(4, 16);
            this.picCursorInfo.Name = "picCursorInfo";
            this.picCursorInfo.Size = new System.Drawing.Size(80, 78);
            this.picCursorInfo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picCursorInfo.TabIndex = 7;
            this.picCursorInfo.TabStop = false;
            // 
            // txtCursorInfo
            // 
            this.txtCursorInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCursorInfo.ContextMenuStrip = this.cmnuCursorInfo;
            this.txtCursorInfo.Location = new System.Drawing.Point(83, 16);
            this.txtCursorInfo.Multiline = true;
            this.txtCursorInfo.Name = "txtCursorInfo";
            this.txtCursorInfo.ReadOnly = true;
            this.txtCursorInfo.Size = new System.Drawing.Size(100, 78);
            this.txtCursorInfo.TabIndex = 6;
            // 
            // cmnuCursorInfo
            // 
            this.cmnuCursorInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmnuCursorScript,
            this.cmnuCursorMoveTo,
            this.cmnuCursorGetColor});
            this.cmnuCursorInfo.Name = "cmnuCursorInfo";
            this.cmnuCursorInfo.Size = new System.Drawing.Size(125, 70);
            // 
            // cmnuCursorScript
            // 
            this.cmnuCursorScript.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmnuCursorScriptLClick,
            this.cmnuCursorScriptRClick,
            this.toolStripMenuItem2,
            this.cmnuCursorScriptColorRGB,
            this.cmnuCursorScriptColorValue,
            this.cmnuCursorScriptLikeColor});
            this.cmnuCursorScript.Name = "cmnuCursorScript";
            this.cmnuCursorScript.Size = new System.Drawing.Size(124, 22);
            this.cmnuCursorScript.Text = "脚本";
            // 
            // cmnuCursorScriptLClick
            // 
            this.cmnuCursorScriptLClick.Name = "cmnuCursorScriptLClick";
            this.cmnuCursorScriptLClick.Size = new System.Drawing.Size(144, 22);
            this.cmnuCursorScriptLClick.Text = "点击坐标(左)";
            this.cmnuCursorScriptLClick.Click += new System.EventHandler(this.cmnuCursorScriptLClick_Click);
            // 
            // cmnuCursorScriptRClick
            // 
            this.cmnuCursorScriptRClick.Name = "cmnuCursorScriptRClick";
            this.cmnuCursorScriptRClick.Size = new System.Drawing.Size(144, 22);
            this.cmnuCursorScriptRClick.Text = "点击坐标(右)";
            this.cmnuCursorScriptRClick.Click += new System.EventHandler(this.cmnuCursorScriptRClick_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(141, 6);
            // 
            // cmnuCursorScriptColorRGB
            // 
            this.cmnuCursorScriptColorRGB.Name = "cmnuCursorScriptColorRGB";
            this.cmnuCursorScriptColorRGB.Size = new System.Drawing.Size(144, 22);
            this.cmnuCursorScriptColorRGB.Text = "查看RGB";
            this.cmnuCursorScriptColorRGB.Click += new System.EventHandler(this.cmnuCursorScriptColorRGB_Click);
            // 
            // cmnuCursorScriptColorValue
            // 
            this.cmnuCursorScriptColorValue.Name = "cmnuCursorScriptColorValue";
            this.cmnuCursorScriptColorValue.Size = new System.Drawing.Size(144, 22);
            this.cmnuCursorScriptColorValue.Text = "对比颜色";
            this.cmnuCursorScriptColorValue.Click += new System.EventHandler(this.cmnuCursorScriptColorValue_Click);
            // 
            // cmnuCursorScriptLikeColor
            // 
            this.cmnuCursorScriptLikeColor.Name = "cmnuCursorScriptLikeColor";
            this.cmnuCursorScriptLikeColor.Size = new System.Drawing.Size(144, 22);
            this.cmnuCursorScriptLikeColor.Text = "模糊比色";
            this.cmnuCursorScriptLikeColor.Click += new System.EventHandler(this.cmnuCursorScriptLikeColor_Click);
            // 
            // cmnuCursorMoveTo
            // 
            this.cmnuCursorMoveTo.Name = "cmnuCursorMoveTo";
            this.cmnuCursorMoveTo.Size = new System.Drawing.Size(124, 22);
            this.cmnuCursorMoveTo.Text = "指向坐标";
            this.cmnuCursorMoveTo.Click += new System.EventHandler(this.cmnuCursorMoveTo_Click);
            // 
            // cmnuCursorGetColor
            // 
            this.cmnuCursorGetColor.Name = "cmnuCursorGetColor";
            this.cmnuCursorGetColor.Size = new System.Drawing.Size(124, 22);
            this.cmnuCursorGetColor.Text = "重新取色";
            this.cmnuCursorGetColor.Click += new System.EventHandler(this.cmnuCursorGetColor_Click);
            // 
            // lbtnCursorInfoClose
            // 
            this.lbtnCursorInfoClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbtnCursorInfoClose.AutoSize = true;
            this.lbtnCursorInfoClose.BackColor = System.Drawing.Color.Transparent;
            this.lbtnCursorInfoClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbtnCursorInfoClose.Location = new System.Drawing.Point(168, 1);
            this.lbtnCursorInfoClose.Name = "lbtnCursorInfoClose";
            this.lbtnCursorInfoClose.Size = new System.Drawing.Size(17, 12);
            this.lbtnCursorInfoClose.TabIndex = 5;
            this.lbtnCursorInfoClose.Text = "×";
            this.toolTip1.SetToolTip(this.lbtnCursorInfoClose, "关闭坐标信息窗口");
            this.lbtnCursorInfoClose.Click += new System.EventHandler(this.lbtnCursorInfoClose_Click);
            // 
            // splitTargetList
            // 
            this.splitTargetList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitTargetList.Location = new System.Drawing.Point(0, 432);
            this.splitTargetList.Name = "splitTargetList";
            this.splitTargetList.Size = new System.Drawing.Size(186, 3);
            this.splitTargetList.TabIndex = 6;
            this.splitTargetList.TabStop = false;
            // 
            // grpTargets
            // 
            this.grpTargets.Controls.Add(this.lblTargetsCount);
            this.grpTargets.Controls.Add(this.lbtnTargetsToggleView);
            this.grpTargets.Controls.Add(this.lbtnRefreshTargets);
            this.grpTargets.Controls.Add(this.lvTargets);
            this.grpTargets.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpTargets.Location = new System.Drawing.Point(0, 435);
            this.grpTargets.Name = "grpTargets";
            this.grpTargets.Size = new System.Drawing.Size(186, 85);
            this.grpTargets.TabIndex = 2;
            this.grpTargets.TabStop = false;
            this.grpTargets.Text = "目标对象";
            // 
            // lblTargetsCount
            // 
            this.lblTargetsCount.AutoSize = true;
            this.lblTargetsCount.BackColor = System.Drawing.Color.Transparent;
            this.lblTargetsCount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblTargetsCount.Location = new System.Drawing.Point(54, 1);
            this.lblTargetsCount.Name = "lblTargetsCount";
            this.lblTargetsCount.Size = new System.Drawing.Size(23, 12);
            this.lblTargetsCount.TabIndex = 4;
            this.lblTargetsCount.Text = "(0)";
            this.lblTargetsCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTargetsCount.Click += new System.EventHandler(this.lbtnTargetsToggleView_Click);
            // 
            // lbtnTargetsToggleView
            // 
            this.lbtnTargetsToggleView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbtnTargetsToggleView.AutoSize = true;
            this.lbtnTargetsToggleView.BackColor = System.Drawing.Color.Transparent;
            this.lbtnTargetsToggleView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbtnTargetsToggleView.Location = new System.Drawing.Point(149, 0);
            this.lbtnTargetsToggleView.Name = "lbtnTargetsToggleView";
            this.lbtnTargetsToggleView.Size = new System.Drawing.Size(13, 12);
            this.lbtnTargetsToggleView.TabIndex = 4;
            this.lbtnTargetsToggleView.Text = "☷";
            this.toolTip1.SetToolTip(this.lbtnTargetsToggleView, "切换视图");
            this.lbtnTargetsToggleView.Click += new System.EventHandler(this.lbtnTargetsToggleView_Click);
            // 
            // lbtnRefreshTargets
            // 
            this.lbtnRefreshTargets.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbtnRefreshTargets.AutoSize = true;
            this.lbtnRefreshTargets.BackColor = System.Drawing.Color.Transparent;
            this.lbtnRefreshTargets.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbtnRefreshTargets.Location = new System.Drawing.Point(168, 0);
            this.lbtnRefreshTargets.Name = "lbtnRefreshTargets";
            this.lbtnRefreshTargets.Size = new System.Drawing.Size(13, 12);
            this.lbtnRefreshTargets.TabIndex = 4;
            this.lbtnRefreshTargets.Text = "↺";
            this.toolTip1.SetToolTip(this.lbtnRefreshTargets, "重新加载目标");
            this.lbtnRefreshTargets.Click += new System.EventHandler(this.lbtnRefreshTargets_Click);
            // 
            // lvTargets
            // 
            this.lvTargets.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colKey,
            this.colSize,
            this.colBytes,
            this.colTime});
            this.lvTargets.ContextMenuStrip = this.cmnuTargetItems;
            this.lvTargets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvTargets.FullRowSelect = true;
            this.lvTargets.HideSelection = false;
            this.lvTargets.LargeImageList = this.imgListTargets;
            this.lvTargets.Location = new System.Drawing.Point(3, 17);
            this.lvTargets.MultiSelect = false;
            this.lvTargets.Name = "lvTargets";
            this.lvTargets.Size = new System.Drawing.Size(180, 65);
            this.lvTargets.TabIndex = 0;
            this.lvTargets.UseCompatibleStateImageBehavior = false;
            // 
            // colKey
            // 
            this.colKey.Text = "Target";
            this.colKey.Width = 100;
            // 
            // colSize
            // 
            this.colSize.Text = "Size";
            // 
            // colBytes
            // 
            this.colBytes.Text = "Bytes";
            // 
            // colTime
            // 
            this.colTime.Text = "Time";
            this.colTime.Width = 100;
            // 
            // cmnuTargetItems
            // 
            this.cmnuTargetItems.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmnuTargetItemTestFind,
            this.cmnuTargetItemCodes,
            this.toolStripMenuItem1,
            this.cmnuTargetItemExplorer,
            this.cmnuTargetItemDelete});
            this.cmnuTargetItems.Name = "cmnuTargetItems";
            this.cmnuTargetItems.Size = new System.Drawing.Size(110, 98);
            // 
            // cmnuTargetItemTestFind
            // 
            this.cmnuTargetItemTestFind.Name = "cmnuTargetItemTestFind";
            this.cmnuTargetItemTestFind.Size = new System.Drawing.Size(109, 22);
            this.cmnuTargetItemTestFind.Text = "测试...";
            this.cmnuTargetItemTestFind.Click += new System.EventHandler(this.cmnuTargetItemTestFind_Click);
            // 
            // cmnuTargetItemCodes
            // 
            this.cmnuTargetItemCodes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmnuTargetItemCodeFind});
            this.cmnuTargetItemCodes.Name = "cmnuTargetItemCodes";
            this.cmnuTargetItemCodes.Size = new System.Drawing.Size(109, 22);
            this.cmnuTargetItemCodes.Text = "脚本";
            // 
            // cmnuTargetItemCodeFind
            // 
            this.cmnuTargetItemCodeFind.Name = "cmnuTargetItemCodeFind";
            this.cmnuTargetItemCodeFind.Size = new System.Drawing.Size(124, 22);
            this.cmnuTargetItemCodeFind.Text = "查找对象";
            this.cmnuTargetItemCodeFind.Click += new System.EventHandler(this.cmnuTargetItemCodeFind_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(106, 6);
            // 
            // cmnuTargetItemExplorer
            // 
            this.cmnuTargetItemExplorer.Name = "cmnuTargetItemExplorer";
            this.cmnuTargetItemExplorer.Size = new System.Drawing.Size(109, 22);
            this.cmnuTargetItemExplorer.Text = "浏览...";
            this.cmnuTargetItemExplorer.Click += new System.EventHandler(this.cmnuTargetItemExplorer_Click);
            // 
            // cmnuTargetItemDelete
            // 
            this.cmnuTargetItemDelete.Name = "cmnuTargetItemDelete";
            this.cmnuTargetItemDelete.Size = new System.Drawing.Size(109, 22);
            this.cmnuTargetItemDelete.Text = "删除";
            this.cmnuTargetItemDelete.Click += new System.EventHandler(this.cmnuTargetItemDelete_Click);
            // 
            // imgListTargets
            // 
            this.imgListTargets.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imgListTargets.ImageSize = new System.Drawing.Size(32, 32);
            this.imgListTargets.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // grpScripts
            // 
            this.grpScripts.Controls.Add(this.lblScriptsCount);
            this.grpScripts.Controls.Add(this.lbtnRefreshScripts);
            this.grpScripts.Controls.Add(this.lblScriptStatus);
            this.grpScripts.Controls.Add(this.cbxScriptFiles);
            this.grpScripts.Controls.Add(this.btnScriptRun);
            this.grpScripts.Controls.Add(this.btnScriptSave);
            this.grpScripts.Controls.Add(this.btnScriptRecord);
            this.grpScripts.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpScripts.Location = new System.Drawing.Point(0, 80);
            this.grpScripts.Name = "grpScripts";
            this.grpScripts.Size = new System.Drawing.Size(186, 84);
            this.grpScripts.TabIndex = 3;
            this.grpScripts.TabStop = false;
            this.grpScripts.Text = "脚本管理";
            // 
            // lblScriptsCount
            // 
            this.lblScriptsCount.AutoSize = true;
            this.lblScriptsCount.BackColor = System.Drawing.Color.Transparent;
            this.lblScriptsCount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblScriptsCount.Location = new System.Drawing.Point(54, 1);
            this.lblScriptsCount.Name = "lblScriptsCount";
            this.lblScriptsCount.Size = new System.Drawing.Size(23, 12);
            this.lblScriptsCount.TabIndex = 5;
            this.lblScriptsCount.Text = "(0)";
            this.lblScriptsCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbtnRefreshScripts
            // 
            this.lbtnRefreshScripts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbtnRefreshScripts.AutoSize = true;
            this.lbtnRefreshScripts.BackColor = System.Drawing.Color.Transparent;
            this.lbtnRefreshScripts.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbtnRefreshScripts.Location = new System.Drawing.Point(168, 0);
            this.lbtnRefreshScripts.Name = "lbtnRefreshScripts";
            this.lbtnRefreshScripts.Size = new System.Drawing.Size(13, 12);
            this.lbtnRefreshScripts.TabIndex = 3;
            this.lbtnRefreshScripts.Text = "↺";
            this.toolTip1.SetToolTip(this.lbtnRefreshScripts, "重新加载脚本");
            this.lbtnRefreshScripts.Click += new System.EventHandler(this.lbtnRefreshScripts_Click);
            // 
            // lblScriptStatus
            // 
            this.lblScriptStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblScriptStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblScriptStatus.ForeColor = System.Drawing.Color.YellowGreen;
            this.lblScriptStatus.Location = new System.Drawing.Point(4, 59);
            this.lblScriptStatus.Name = "lblScriptStatus";
            this.lblScriptStatus.Size = new System.Drawing.Size(179, 19);
            this.lblScriptStatus.TabIndex = 2;
            this.lblScriptStatus.Text = "[Script Status]";
            this.lblScriptStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbxScriptFiles
            // 
            this.cbxScriptFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxScriptFiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxScriptFiles.FormattingEnabled = true;
            this.cbxScriptFiles.Location = new System.Drawing.Point(4, 14);
            this.cbxScriptFiles.Name = "cbxScriptFiles";
            this.cbxScriptFiles.Size = new System.Drawing.Size(179, 20);
            this.cbxScriptFiles.TabIndex = 0;
            this.cbxScriptFiles.SelectedIndexChanged += new System.EventHandler(this.cbxScriptFiles_SelectedIndexChanged);
            // 
            // btnScriptRun
            // 
            this.btnScriptRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnScriptRun.ContextMenuStrip = this.cmnuScriptRun;
            this.btnScriptRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnScriptRun.Location = new System.Drawing.Point(123, 36);
            this.btnScriptRun.Name = "btnScriptRun";
            this.btnScriptRun.Size = new System.Drawing.Size(60, 22);
            this.btnScriptRun.TabIndex = 1;
            this.btnScriptRun.Text = "运行";
            this.btnScriptRun.UseVisualStyleBackColor = true;
            this.btnScriptRun.Click += new System.EventHandler(this.btnScriptRun_Click);
            // 
            // cmnuScriptRun
            // 
            this.cmnuScriptRun.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmnuScriptRunCompileOnly,
            this.cmnuScriptRunOnce});
            this.cmnuScriptRun.Name = "cmnuScriptRun";
            this.cmnuScriptRun.Size = new System.Drawing.Size(125, 48);
            // 
            // cmnuScriptRunCompileOnly
            // 
            this.cmnuScriptRunCompileOnly.Name = "cmnuScriptRunCompileOnly";
            this.cmnuScriptRunCompileOnly.Size = new System.Drawing.Size(124, 22);
            this.cmnuScriptRunCompileOnly.Text = "仅编译";
            this.cmnuScriptRunCompileOnly.Click += new System.EventHandler(this.cmnuScriptRunCompileOnly_Click);
            // 
            // cmnuScriptRunOnce
            // 
            this.cmnuScriptRunOnce.Name = "cmnuScriptRunOnce";
            this.cmnuScriptRunOnce.Size = new System.Drawing.Size(124, 22);
            this.cmnuScriptRunOnce.Text = "运行一次";
            this.cmnuScriptRunOnce.Click += new System.EventHandler(this.cmnuScriptRunOnce_Click);
            // 
            // btnScriptSave
            // 
            this.btnScriptSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnScriptSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnScriptSave.Location = new System.Drawing.Point(64, 36);
            this.btnScriptSave.Name = "btnScriptSave";
            this.btnScriptSave.Size = new System.Drawing.Size(60, 22);
            this.btnScriptSave.TabIndex = 1;
            this.btnScriptSave.Text = "保存";
            this.btnScriptSave.UseVisualStyleBackColor = true;
            this.btnScriptSave.Click += new System.EventHandler(this.btnScriptSave_Click);
            // 
            // btnScriptRecord
            // 
            this.btnScriptRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnScriptRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnScriptRecord.Location = new System.Drawing.Point(4, 36);
            this.btnScriptRecord.Name = "btnScriptRecord";
            this.btnScriptRecord.Size = new System.Drawing.Size(61, 22);
            this.btnScriptRecord.TabIndex = 1;
            this.btnScriptRecord.Text = "录制";
            this.btnScriptRecord.UseVisualStyleBackColor = true;
            this.btnScriptRecord.Click += new System.EventHandler(this.btnScriptRecord_Click);
            // 
            // grpLookInfos
            // 
            this.grpLookInfos.Controls.Add(this.pelBorderMP);
            this.grpLookInfos.Controls.Add(this.pelBorderHP);
            this.grpLookInfos.Controls.Add(this.lblTipHP);
            this.grpLookInfos.Controls.Add(this.lblLookMPValue);
            this.grpLookInfos.Controls.Add(this.lblTipMP);
            this.grpLookInfos.Controls.Add(this.lblLookHPValue);
            this.grpLookInfos.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpLookInfos.Location = new System.Drawing.Point(0, 25);
            this.grpLookInfos.Name = "grpLookInfos";
            this.grpLookInfos.Size = new System.Drawing.Size(186, 55);
            this.grpLookInfos.TabIndex = 2;
            this.grpLookInfos.TabStop = false;
            this.grpLookInfos.Text = "角色监控";
            // 
            // pelBorderMP
            // 
            this.pelBorderMP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pelBorderMP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pelBorderMP.Controls.Add(this.chkAutoMP);
            this.pelBorderMP.Location = new System.Drawing.Point(76, 31);
            this.pelBorderMP.Name = "pelBorderMP";
            this.pelBorderMP.Size = new System.Drawing.Size(100, 18);
            this.pelBorderMP.TabIndex = 4;
            // 
            // chkAutoMP
            // 
            this.chkAutoMP.AutoSize = true;
            this.chkAutoMP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkAutoMP.Location = new System.Drawing.Point(3, 1);
            this.chkAutoMP.Name = "chkAutoMP";
            this.chkAutoMP.Size = new System.Drawing.Size(39, 16);
            this.chkAutoMP.TabIndex = 2;
            this.chkAutoMP.Text = "MPP";
            this.chkAutoMP.UseVisualStyleBackColor = true;
            // 
            // pelBorderHP
            // 
            this.pelBorderHP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pelBorderHP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pelBorderHP.Controls.Add(this.chkAutoHP);
            this.pelBorderHP.Location = new System.Drawing.Point(76, 14);
            this.pelBorderHP.Name = "pelBorderHP";
            this.pelBorderHP.Size = new System.Drawing.Size(100, 18);
            this.pelBorderHP.TabIndex = 3;
            // 
            // chkAutoHP
            // 
            this.chkAutoHP.AutoSize = true;
            this.chkAutoHP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkAutoHP.Location = new System.Drawing.Point(3, 1);
            this.chkAutoHP.Name = "chkAutoHP";
            this.chkAutoHP.Size = new System.Drawing.Size(39, 16);
            this.chkAutoHP.TabIndex = 2;
            this.chkAutoHP.Text = "HPP";
            this.chkAutoHP.UseVisualStyleBackColor = true;
            // 
            // lblTipHP
            // 
            this.lblTipHP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTipHP.Location = new System.Drawing.Point(35, 14);
            this.lblTipHP.Name = "lblTipHP";
            this.lblTipHP.Size = new System.Drawing.Size(42, 18);
            this.lblTipHP.TabIndex = 0;
            this.lblTipHP.Text = "(%)HP";
            this.lblTipHP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLookMPValue
            // 
            this.lblLookMPValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLookMPValue.Location = new System.Drawing.Point(4, 31);
            this.lblLookMPValue.Name = "lblLookMPValue";
            this.lblLookMPValue.Size = new System.Drawing.Size(32, 18);
            this.lblLookMPValue.TabIndex = 1;
            this.lblLookMPValue.Text = "--";
            this.lblLookMPValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTipMP
            // 
            this.lblTipMP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTipMP.Location = new System.Drawing.Point(35, 31);
            this.lblTipMP.Name = "lblTipMP";
            this.lblTipMP.Size = new System.Drawing.Size(42, 18);
            this.lblTipMP.TabIndex = 0;
            this.lblTipMP.Text = "(%)MP";
            this.lblTipMP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLookHPValue
            // 
            this.lblLookHPValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLookHPValue.Location = new System.Drawing.Point(4, 14);
            this.lblLookHPValue.Name = "lblLookHPValue";
            this.lblLookHPValue.Size = new System.Drawing.Size(32, 18);
            this.lblLookHPValue.TabIndex = 1;
            this.lblLookHPValue.Text = "--";
            this.lblLookHPValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbsBar
            // 
            this.tbsBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbsWorkArea,
            this.tbsCursor,
            this.toolStripSeparator1,
            this.tbsAddTarget,
            this.toolStripSeparator2,
            this.tbsLookHP,
            this.tbsLookMP});
            this.tbsBar.Location = new System.Drawing.Point(0, 0);
            this.tbsBar.Name = "tbsBar";
            this.tbsBar.Size = new System.Drawing.Size(186, 25);
            this.tbsBar.TabIndex = 1;
            this.tbsBar.Text = "toolStrip1";
            // 
            // tbsWorkArea
            // 
            this.tbsWorkArea.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbsWorkArea.Image = global::AutoBrowser.Properties.Resources.app_only;
            this.tbsWorkArea.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbsWorkArea.Name = "tbsWorkArea";
            this.tbsWorkArea.Size = new System.Drawing.Size(23, 22);
            this.tbsWorkArea.Text = "WorkArea";
            this.tbsWorkArea.ToolTipText = "查找区域(当前仅应用)";
            this.tbsWorkArea.Click += new System.EventHandler(this.tbsWorkArea_Click);
            // 
            // tbsCursor
            // 
            this.tbsCursor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbsCursor.Image = global::AutoBrowser.Properties.Resources.pin_grey;
            this.tbsCursor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbsCursor.Name = "tbsCursor";
            this.tbsCursor.Size = new System.Drawing.Size(23, 22);
            this.tbsCursor.Text = "Cursor";
            this.tbsCursor.ToolTipText = "取位置和颜色";
            this.tbsCursor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tbsCursor_MouseDown);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tbsAddTarget
            // 
            this.tbsAddTarget.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbsAddTarget.Image = global::AutoBrowser.Properties.Resources.selection_view;
            this.tbsAddTarget.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbsAddTarget.Name = "tbsAddTarget";
            this.tbsAddTarget.Size = new System.Drawing.Size(23, 22);
            this.tbsAddTarget.Text = "AddTarget";
            this.tbsAddTarget.ToolTipText = "框选目标";
            this.tbsAddTarget.Click += new System.EventHandler(this.tbsAddTarget_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tbsLookHP
            // 
            this.tbsLookHP.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbsLookHP.Image = global::AutoBrowser.Properties.Resources.heart;
            this.tbsLookHP.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbsLookHP.Name = "tbsLookHP";
            this.tbsLookHP.Size = new System.Drawing.Size(23, 22);
            this.tbsLookHP.Text = "LookHP";
            this.tbsLookHP.ToolTipText = "HP 监控区";
            // 
            // tbsLookMP
            // 
            this.tbsLookMP.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbsLookMP.Image = global::AutoBrowser.Properties.Resources.chrystal_ball;
            this.tbsLookMP.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbsLookMP.Name = "tbsLookMP";
            this.tbsLookMP.Size = new System.Drawing.Size(23, 22);
            this.tbsLookMP.Text = "LookMP";
            this.tbsLookMP.ToolTipText = "MP 监控区";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // grpConsole
            // 
            this.grpConsole.Controls.Add(this.lblConsolePop);
            this.grpConsole.Controls.Add(this.lbtnClearLog);
            this.grpConsole.Controls.Add(this.axtConsole);
            this.grpConsole.Controls.Add(this.txtConsole);
            this.grpConsole.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpConsole.Location = new System.Drawing.Point(0, 242);
            this.grpConsole.Name = "grpConsole";
            this.grpConsole.Size = new System.Drawing.Size(186, 90);
            this.grpConsole.TabIndex = 8;
            this.grpConsole.TabStop = false;
            this.grpConsole.Text = "控制台";
            this.grpConsole.Visible = false;
            // 
            // lblToggleConsole
            // 
            this.lblToggleConsole.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblToggleConsole.AutoSize = true;
            this.lblToggleConsole.BackColor = System.Drawing.Color.Transparent;
            this.lblToggleConsole.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblToggleConsole.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblToggleConsole.Location = new System.Drawing.Point(168, 58);
            this.lblToggleConsole.Name = "lblToggleConsole";
            this.lblToggleConsole.Size = new System.Drawing.Size(13, 12);
            this.lblToggleConsole.TabIndex = 6;
            this.lblToggleConsole.Text = "▀";
            this.toolTip1.SetToolTip(this.lblToggleConsole, "显示/隐藏 控制台窗口");
            this.lblToggleConsole.Click += new System.EventHandler(this.lblToggleConsole_Click);
            // 
            // axtConsole
            // 
            this.axtConsole.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.axtConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axtConsole.Location = new System.Drawing.Point(3, 17);
            this.axtConsole.Multiline = true;
            this.axtConsole.Name = "axtConsole";
            this.axtConsole.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.axtConsole.Size = new System.Drawing.Size(180, 49);
            this.axtConsole.TabIndex = 7;
            this.axtConsole.WordWrap = false;
            // 
            // splitConsole
            // 
            this.splitConsole.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitConsole.Location = new System.Drawing.Point(0, 239);
            this.splitConsole.Name = "splitConsole";
            this.splitConsole.Size = new System.Drawing.Size(186, 3);
            this.splitConsole.TabIndex = 9;
            this.splitConsole.TabStop = false;
            // 
            // lbtnClearLog
            // 
            this.lbtnClearLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbtnClearLog.AutoSize = true;
            this.lbtnClearLog.BackColor = System.Drawing.Color.Transparent;
            this.lbtnClearLog.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbtnClearLog.Location = new System.Drawing.Point(148, 1);
            this.lbtnClearLog.Name = "lbtnClearLog";
            this.lbtnClearLog.Size = new System.Drawing.Size(14, 12);
            this.lbtnClearLog.TabIndex = 8;
            this.lbtnClearLog.Text = "⛞";
            this.toolTip1.SetToolTip(this.lbtnClearLog, "清除控制台");
            this.lbtnClearLog.Click += new System.EventHandler(this.lbtnClearLog_Click);
            // 
            // lblConsolePop
            // 
            this.lblConsolePop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblConsolePop.AutoSize = true;
            this.lblConsolePop.BackColor = System.Drawing.Color.Transparent;
            this.lblConsolePop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblConsolePop.Location = new System.Drawing.Point(168, 1);
            this.lblConsolePop.Name = "lblConsolePop";
            this.lblConsolePop.Size = new System.Drawing.Size(14, 12);
            this.lblConsolePop.TabIndex = 8;
            this.lblConsolePop.Text = "⧉";
            this.toolTip1.SetToolTip(this.lblConsolePop, "控制台浮动显示");
            this.lblConsolePop.Click += new System.EventHandler(this.lblConsolePop_Click);
            // 
            // txtConsole
            // 
            this.txtConsole.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtConsole.Location = new System.Drawing.Point(3, 66);
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.Size = new System.Drawing.Size(180, 21);
            this.txtConsole.TabIndex = 9;
            this.txtConsole.Visible = false;
            // 
            // FrmBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 520);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FrmBrowser";
            this.Text = "AutoBrowser";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmBrowser_FormClosed);
            this.Load += new System.EventHandler(this.FrmBrowser_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.pelBrowser.ResumeLayout(false);
            this.pelUrl.ResumeLayout(false);
            this.grpScriptBody.ResumeLayout(false);
            this.grpScriptBody.PerformLayout();
            this.grpCursorInfo.ResumeLayout(false);
            this.grpCursorInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCursorInfo)).EndInit();
            this.cmnuCursorInfo.ResumeLayout(false);
            this.grpTargets.ResumeLayout(false);
            this.grpTargets.PerformLayout();
            this.cmnuTargetItems.ResumeLayout(false);
            this.grpScripts.ResumeLayout(false);
            this.grpScripts.PerformLayout();
            this.cmnuScriptRun.ResumeLayout(false);
            this.grpLookInfos.ResumeLayout(false);
            this.pelBorderMP.ResumeLayout(false);
            this.pelBorderMP.PerformLayout();
            this.pelBorderHP.ResumeLayout(false);
            this.pelBorderHP.PerformLayout();
            this.tbsBar.ResumeLayout(false);
            this.tbsBar.PerformLayout();
            this.grpConsole.ResumeLayout(false);
            this.grpConsole.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel pelBrowser;
        private System.Windows.Forms.Panel pelUrl;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.ComboBox cbxUrl;
        private MiniBlinkPinvoke.BlinkBrowser blinkBrowser1;
        private System.Windows.Forms.Button btnToggleTools;
        private System.Windows.Forms.ToolStrip tbsBar;
        private System.Windows.Forms.ToolStripButton tbsCursor;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tbsAddTarget;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tbsLookHP;
        private System.Windows.Forms.ToolStripButton tbsLookMP;
        private System.Windows.Forms.Label lblLookMPValue;
        private System.Windows.Forms.Label lblLookHPValue;
        private System.Windows.Forms.Label lblTipMP;
        private System.Windows.Forms.Label lblTipHP;
        private System.Windows.Forms.CheckBox chkAutoMP;
        private System.Windows.Forms.CheckBox chkAutoHP;
        private System.Windows.Forms.Panel pelBorderMP;
        private System.Windows.Forms.Panel pelBorderHP;
        private System.Windows.Forms.TextBox txtScriptBody;
        private System.Windows.Forms.Splitter splitTargetList;
        private System.Windows.Forms.Button btnScriptSave;
        private System.Windows.Forms.ComboBox cbxScriptFiles;
        private System.Windows.Forms.Button btnScriptRun;
        private System.Windows.Forms.Button btnScriptRecord;
        private System.Windows.Forms.Label lblScriptStatus;
        private System.Windows.Forms.ListView lvTargets;
        private System.Windows.Forms.GroupBox grpScriptBody;
        private System.Windows.Forms.GroupBox grpScripts;
        private System.Windows.Forms.GroupBox grpLookInfos;
        private System.Windows.Forms.GroupBox grpTargets;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ContextMenuStrip cmnuTargetItems;
        private System.Windows.Forms.ToolStripMenuItem cmnuTargetItemExplorer;
        private System.Windows.Forms.ToolStripMenuItem cmnuTargetItemDelete;
        private System.Windows.Forms.ToolStripMenuItem cmnuTargetItemTestFind;
        private System.Windows.Forms.ImageList imgListTargets;
        private System.Windows.Forms.Label lbtnRefreshTargets;
        private System.Windows.Forms.Label lbtnRefreshScripts;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem cmnuTargetItemCodes;
        private System.Windows.Forms.ToolStripMenuItem cmnuTargetItemCodeFind;
        private System.Windows.Forms.Label lbtnTargetsToggleView;
        private System.Windows.Forms.ColumnHeader colKey;
        private System.Windows.Forms.ColumnHeader colSize;
        private System.Windows.Forms.ColumnHeader colBytes;
        private System.Windows.Forms.ColumnHeader colTime;
        private System.Windows.Forms.Label lblTargetsCount;
        private System.Windows.Forms.Label lblScriptsCount;
        private System.Windows.Forms.ToolStripButton tbsWorkArea;
        private System.Windows.Forms.GroupBox grpCursorInfo;
        private System.Windows.Forms.Label lbtnCursorInfoClose;
        private System.Windows.Forms.TextBox txtCursorInfo;
        private System.Windows.Forms.PictureBox picCursorInfo;
        private System.Windows.Forms.ContextMenuStrip cmnuScriptRun;
        private System.Windows.Forms.ToolStripMenuItem cmnuScriptRunCompileOnly;
        private System.Windows.Forms.ToolStripMenuItem cmnuScriptRunOnce;
        private System.Windows.Forms.ContextMenuStrip cmnuCursorInfo;
        private System.Windows.Forms.ToolStripMenuItem cmnuCursorScript;
        private System.Windows.Forms.ToolStripMenuItem cmnuCursorScriptColorValue;
        private System.Windows.Forms.ToolStripMenuItem cmnuCursorScriptColorRGB;
        private System.Windows.Forms.ToolStripMenuItem cmnuCursorScriptLClick;
        private System.Windows.Forms.ToolStripMenuItem cmnuCursorGetColor;
        private System.Windows.Forms.ToolStripMenuItem cmnuCursorMoveTo;
        private System.Windows.Forms.ToolStripMenuItem cmnuCursorScriptRClick;
        private System.Windows.Forms.ToolStripMenuItem cmnuCursorScriptLikeColor;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.Splitter splitConsole;
        private System.Windows.Forms.GroupBox grpConsole;
        private System.Windows.Forms.TextBox axtConsole;
        private System.Windows.Forms.Label lblToggleConsole;
        private System.Windows.Forms.Label lbtnClearLog;
        private System.Windows.Forms.Label lblConsolePop;
        private System.Windows.Forms.TextBox txtConsole;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

