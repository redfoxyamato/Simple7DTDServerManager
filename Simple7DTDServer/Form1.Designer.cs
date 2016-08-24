namespace Simple7DTDServer
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.information = new System.Windows.Forms.ToolStripMenuItem();
            this.serverInfomation = new System.Windows.Forms.ToolStripMenuItem();
            this.connection = new System.Windows.Forms.ToolStripMenuItem();
            this.manipulation = new System.Windows.Forms.ToolStripMenuItem();
            this.startServer = new System.Windows.Forms.ToolStripMenuItem();
            this.stopServer = new System.Windows.Forms.ToolStripMenuItem();
            this.configuration = new System.Windows.Forms.ToolStripMenuItem();
            this.manager = new System.Windows.Forms.ToolStripMenuItem();
            this.languageMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.portMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.autoMapServer = new System.Windows.Forms.ToolStripMenuItem();
            this.Check = new System.Windows.Forms.ToolStripMenuItem();
            this.window = new System.Windows.Forms.ToolStripMenuItem();
            this.easyCommandsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.send = new System.Windows.Forms.Button();
            this.connectCheck = new System.Windows.Forms.Timer(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timerServerUpdate = new System.Windows.Forms.Timer(this.components);
            this.connectionType = new System.Windows.Forms.GroupBox();
            this.serverExternalPort = new System.Windows.Forms.NumericUpDown();
            this.serverExtPort = new System.Windows.Forms.Label();
            this.externalIP = new System.Windows.Forms.TextBox();
            this.serverIP = new System.Windows.Forms.Label();
            this.externalMode = new System.Windows.Forms.RadioButton();
            this.internalMode = new System.Windows.Forms.RadioButton();
            this.killServer = new System.Windows.Forms.Button();
            this.playerListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.banListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.connectionType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.serverExternalPort)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.information,
            this.manipulation,
            this.configuration,
            this.portMenu,
            this.window});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(726, 26);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // information
            // 
            this.information.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serverInfomation,
            this.connection});
            this.information.Name = "information";
            this.information.Size = new System.Drawing.Size(89, 22);
            this.information.Text = "Information";
            // 
            // serverInfomation
            // 
            this.serverInfomation.Name = "serverInfomation";
            this.serverInfomation.Size = new System.Drawing.Size(202, 22);
            this.serverInfomation.Text = "server information(&S)";
            this.serverInfomation.Click += new System.EventHandler(this.ServerInfomationToolStripMenuItem_Click);
            // 
            // connection
            // 
            this.connection.Name = "connection";
            this.connection.Size = new System.Drawing.Size(202, 22);
            this.connection.Text = "Connection";
            this.connection.Click += new System.EventHandler(this.connect_Click);
            // 
            // manipulation
            // 
            this.manipulation.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startServer,
            this.stopServer});
            this.manipulation.Name = "manipulation";
            this.manipulation.Size = new System.Drawing.Size(93, 22);
            this.manipulation.Text = "Manipulation";
            // 
            // startServer
            // 
            this.startServer.Name = "startServer";
            this.startServer.Size = new System.Drawing.Size(152, 22);
            this.startServer.Text = "Connect";
            this.startServer.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // stopServer
            // 
            this.stopServer.Enabled = false;
            this.stopServer.Name = "stopServer";
            this.stopServer.Size = new System.Drawing.Size(152, 22);
            this.stopServer.Text = "Stop";
            this.stopServer.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // configuration
            // 
            this.configuration.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manager,
            this.languageMenu});
            this.configuration.Name = "configuration";
            this.configuration.Size = new System.Drawing.Size(56, 22);
            this.configuration.Text = "Config";
            // 
            // manager
            // 
            this.manager.Name = "manager";
            this.manager.Size = new System.Drawing.Size(132, 22);
            this.manager.Text = "Manager";
            this.manager.Click += new System.EventHandler(this.manageToolStripMenuItem_Click);
            // 
            // languageMenu
            // 
            this.languageMenu.Name = "languageMenu";
            this.languageMenu.Size = new System.Drawing.Size(132, 22);
            this.languageMenu.Text = "Language";
            this.languageMenu.Click += new System.EventHandler(this.languageMenu_Click);
            // 
            // portMenu
            // 
            this.portMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.autoMapServer,
            this.Check});
            this.portMenu.Name = "portMenu";
            this.portMenu.Size = new System.Drawing.Size(44, 22);
            this.portMenu.Text = "Port";
            // 
            // autoMapServer
            // 
            this.autoMapServer.CheckOnClick = true;
            this.autoMapServer.Name = "autoMapServer";
            this.autoMapServer.Size = new System.Drawing.Size(165, 22);
            this.autoMapServer.Text = "autoMapServer";
            this.autoMapServer.Click += new System.EventHandler(this.autoMapServer_Click);
            // 
            // Check
            // 
            this.Check.Name = "Check";
            this.Check.Size = new System.Drawing.Size(165, 22);
            this.Check.Text = "Check";
            this.Check.Click += new System.EventHandler(this.Check_Click);
            // 
            // window
            // 
            this.window.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.easyCommandsToolStripMenuItem,
            this.playerListToolStripMenuItem,
            this.banListToolStripMenuItem});
            this.window.Name = "window";
            this.window.Size = new System.Drawing.Size(66, 22);
            this.window.Text = "Window";
            // 
            // easyCommandsToolStripMenuItem
            // 
            this.easyCommandsToolStripMenuItem.Checked = true;
            this.easyCommandsToolStripMenuItem.CheckOnClick = true;
            this.easyCommandsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.easyCommandsToolStripMenuItem.Name = "easyCommandsToolStripMenuItem";
            this.easyCommandsToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.easyCommandsToolStripMenuItem.Text = "Easy Commands";
            this.easyCommandsToolStripMenuItem.Click += new System.EventHandler(this.easyCommandsToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "output";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(16, 59);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(474, 254);
            this.textBox1.TabIndex = 2;
            this.textBox1.WordWrap = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 323);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "command";
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(16, 338);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(634, 19);
            this.textBox2.TabIndex = 4;
            // 
            // send
            // 
            this.send.Location = new System.Drawing.Point(656, 336);
            this.send.Name = "send";
            this.send.Size = new System.Drawing.Size(52, 23);
            this.send.TabIndex = 5;
            this.send.Text = "Send";
            this.send.UseVisualStyleBackColor = true;
            this.send.Click += new System.EventHandler(this.button1_Click);
            // 
            // connectCheck
            // 
            this.connectCheck.Enabled = true;
            this.connectCheck.Interval = 1000;
            this.connectCheck.Tick += new System.EventHandler(this.connectCheck_Tick);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timerServerUpdate
            // 
            this.timerServerUpdate.Enabled = true;
            this.timerServerUpdate.Interval = 10;
            this.timerServerUpdate.Tick += new System.EventHandler(this.timerServerUpdate_Tick);
            // 
            // connectionType
            // 
            this.connectionType.Controls.Add(this.serverExternalPort);
            this.connectionType.Controls.Add(this.serverExtPort);
            this.connectionType.Controls.Add(this.externalIP);
            this.connectionType.Controls.Add(this.serverIP);
            this.connectionType.Controls.Add(this.externalMode);
            this.connectionType.Controls.Add(this.internalMode);
            this.connectionType.Location = new System.Drawing.Point(497, 59);
            this.connectionType.Name = "connectionType";
            this.connectionType.Size = new System.Drawing.Size(217, 147);
            this.connectionType.TabIndex = 6;
            this.connectionType.TabStop = false;
            this.connectionType.Text = "connectionType";
            // 
            // serverExternalPort
            // 
            this.serverExternalPort.Enabled = false;
            this.serverExternalPort.Location = new System.Drawing.Point(123, 93);
            this.serverExternalPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.serverExternalPort.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.serverExternalPort.Name = "serverExternalPort";
            this.serverExternalPort.Size = new System.Drawing.Size(94, 19);
            this.serverExternalPort.TabIndex = 5;
            this.serverExternalPort.Value = new decimal(new int[] {
            23,
            0,
            0,
            0});
            // 
            // serverExtPort
            // 
            this.serverExtPort.AutoSize = true;
            this.serverExtPort.Location = new System.Drawing.Point(7, 93);
            this.serverExtPort.Name = "serverExtPort";
            this.serverExtPort.Size = new System.Drawing.Size(75, 12);
            this.serverExtPort.TabIndex = 4;
            this.serverExtPort.Text = "serverExtPort";
            // 
            // externalIP
            // 
            this.externalIP.Enabled = false;
            this.externalIP.Location = new System.Drawing.Point(123, 62);
            this.externalIP.Name = "externalIP";
            this.externalIP.Size = new System.Drawing.Size(94, 19);
            this.externalIP.TabIndex = 3;
            // 
            // serverIP
            // 
            this.serverIP.AutoSize = true;
            this.serverIP.Location = new System.Drawing.Point(7, 65);
            this.serverIP.Name = "serverIP";
            this.serverIP.Size = new System.Drawing.Size(47, 12);
            this.serverIP.TabIndex = 2;
            this.serverIP.Text = "serverIP";
            // 
            // externalMode
            // 
            this.externalMode.AutoSize = true;
            this.externalMode.Location = new System.Drawing.Point(7, 42);
            this.externalMode.Name = "externalMode";
            this.externalMode.Size = new System.Drawing.Size(91, 16);
            this.externalMode.TabIndex = 1;
            this.externalMode.Text = "externalMode";
            this.externalMode.UseVisualStyleBackColor = true;
            this.externalMode.CheckedChanged += new System.EventHandler(this.externalMode_CheckedChanged);
            // 
            // internalMode
            // 
            this.internalMode.AutoSize = true;
            this.internalMode.Checked = true;
            this.internalMode.Location = new System.Drawing.Point(7, 19);
            this.internalMode.Name = "internalMode";
            this.internalMode.Size = new System.Drawing.Size(88, 16);
            this.internalMode.TabIndex = 0;
            this.internalMode.TabStop = true;
            this.internalMode.Text = "internalMode";
            this.internalMode.UseVisualStyleBackColor = true;
            // 
            // killServer
            // 
            this.killServer.BackColor = System.Drawing.Color.Red;
            this.killServer.Enabled = false;
            this.killServer.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.killServer.ForeColor = System.Drawing.Color.Black;
            this.killServer.Location = new System.Drawing.Point(506, 289);
            this.killServer.Name = "killServer";
            this.killServer.Size = new System.Drawing.Size(75, 23);
            this.killServer.TabIndex = 7;
            this.killServer.Text = "Kill servers";
            this.killServer.UseVisualStyleBackColor = false;
            this.killServer.Click += new System.EventHandler(this.killServer_Click);
            // 
            // playerListToolStripMenuItem
            // 
            this.playerListToolStripMenuItem.Checked = true;
            this.playerListToolStripMenuItem.CheckOnClick = true;
            this.playerListToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.playerListToolStripMenuItem.Name = "playerListToolStripMenuItem";
            this.playerListToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.playerListToolStripMenuItem.Text = "Player List";
            this.playerListToolStripMenuItem.Click += new System.EventHandler(this.playerListToolStripMenuItem_Click);
            // 
            // banListToolStripMenuItem
            // 
            this.banListToolStripMenuItem.Checked = true;
            this.banListToolStripMenuItem.CheckOnClick = true;
            this.banListToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.banListToolStripMenuItem.Name = "banListToolStripMenuItem";
            this.banListToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.banListToolStripMenuItem.Text = "Ban List";
            this.banListToolStripMenuItem.Click += new System.EventHandler(this.banListToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AcceptButton = this.send;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 369);
            this.Controls.Add(this.killServer);
            this.Controls.Add(this.connectionType);
            this.Controls.Add(this.send);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Simple server manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.connectionType.ResumeLayout(false);
            this.connectionType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.serverExternalPort)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem information;
        private System.Windows.Forms.ToolStripMenuItem serverInfomation;
        private System.Windows.Forms.ToolStripMenuItem manipulation;
        private System.Windows.Forms.ToolStripMenuItem startServer;
        private System.Windows.Forms.ToolStripMenuItem stopServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button send;
        private System.Windows.Forms.Timer connectCheck;
        private System.Windows.Forms.ToolStripMenuItem connection;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timerServerUpdate;
        private System.Windows.Forms.ToolStripMenuItem configuration;
        private System.Windows.Forms.ToolStripMenuItem manager;
        private System.Windows.Forms.ToolStripMenuItem languageMenu;
        private System.Windows.Forms.ToolStripMenuItem portMenu;
        private System.Windows.Forms.GroupBox connectionType;
        private System.Windows.Forms.RadioButton internalMode;
        private System.Windows.Forms.RadioButton externalMode;
        private System.Windows.Forms.Label serverIP;
        private System.Windows.Forms.Label serverExtPort;
        private System.Windows.Forms.TextBox externalIP;
        private System.Windows.Forms.NumericUpDown serverExternalPort;
        private System.Windows.Forms.ToolStripMenuItem autoMapServer;
        private System.Windows.Forms.Button killServer;
        private System.Windows.Forms.ToolStripMenuItem Check;
        private System.Windows.Forms.ToolStripMenuItem window;
        private System.Windows.Forms.ToolStripMenuItem easyCommandsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playerListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem banListToolStripMenuItem;
    }
}

