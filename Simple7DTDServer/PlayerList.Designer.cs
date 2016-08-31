namespace Simple7DTDServer
{
    partial class PlayerList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerList));
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeaderID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnPlayerID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnPlayerName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnPlayerPos = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnRotation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnRemote = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHealth = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDeaths = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnZombieKills = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnPlayerKills = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnScore = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnLevel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnSteamID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnPing = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.update = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonExecute = new System.Windows.Forms.Button();
            this.textBoxReason = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButtonBan = new System.Windows.Forms.RadioButton();
            this.radioButtonKick = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxHideLPOutput = new System.Windows.Forms.CheckBox();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.checkBoxAutomatic = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpdate = new System.Windows.Forms.NumericUpDown();
            this.timerAutomatic = new System.Windows.Forms.Timer(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.comboBoxTimeUnit = new System.Windows.Forms.ComboBox();
            this.numericDuration = new System.Windows.Forms.NumericUpDown();
            this.columnKDRatio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpdate)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericDuration)).BeginInit();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderID,
            this.columnPlayerID,
            this.columnPlayerName,
            this.columnPlayerPos,
            this.columnRotation,
            this.columnRemote,
            this.columnHealth,
            this.columnDeaths,
            this.columnZombieKills,
            this.columnPlayerKills,
            this.columnKDRatio,
            this.columnScore,
            this.columnLevel,
            this.columnSteamID,
            this.columnIP,
            this.columnPing});
            this.listView1.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.listView1.FullRowSelect = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 12);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(417, 327);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // columnHeaderID
            // 
            this.columnHeaderID.Text = "ID";
            this.columnHeaderID.Width = 40;
            // 
            // columnPlayerID
            // 
            this.columnPlayerID.Text = "PlayerID";
            // 
            // columnPlayerName
            // 
            this.columnPlayerName.Text = "Player Name";
            this.columnPlayerName.Width = 120;
            // 
            // columnPlayerPos
            // 
            this.columnPlayerPos.Text = "Position";
            this.columnPlayerPos.Width = 147;
            // 
            // columnRotation
            // 
            this.columnRotation.Text = "Rotation";
            this.columnRotation.Width = 120;
            // 
            // columnRemote
            // 
            this.columnRemote.Text = "Remote";
            // 
            // columnHealth
            // 
            this.columnHealth.Text = "Health";
            // 
            // columnDeaths
            // 
            this.columnDeaths.Text = "Deaths";
            // 
            // columnZombieKills
            // 
            this.columnZombieKills.Text = "Zombie Kills";
            this.columnZombieKills.Width = 80;
            // 
            // columnPlayerKills
            // 
            this.columnPlayerKills.Text = "Player Kills";
            this.columnPlayerKills.Width = 80;
            // 
            // columnScore
            // 
            this.columnScore.Text = "Score";
            this.columnScore.Width = 80;
            // 
            // columnLevel
            // 
            this.columnLevel.Text = "Level";
            // 
            // columnSteamID
            // 
            this.columnSteamID.Text = "SteamID";
            this.columnSteamID.Width = 150;
            // 
            // columnIP
            // 
            this.columnIP.Text = "IP";
            this.columnIP.Width = 80;
            // 
            // columnPing
            // 
            this.columnPing.Text = "Ping";
            // 
            // update
            // 
            this.update.Enabled = true;
            this.update.Tick += new System.EventHandler(this.update_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonExecute);
            this.groupBox1.Controls.Add(this.textBoxReason);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.radioButtonBan);
            this.groupBox1.Controls.Add(this.radioButtonKick);
            this.groupBox1.Location = new System.Drawing.Point(436, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(186, 117);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Kick/Ban";
            // 
            // buttonExecute
            // 
            this.buttonExecute.Location = new System.Drawing.Point(94, 39);
            this.buttonExecute.Name = "buttonExecute";
            this.buttonExecute.Size = new System.Drawing.Size(75, 23);
            this.buttonExecute.TabIndex = 4;
            this.buttonExecute.Text = "Execute";
            this.buttonExecute.UseVisualStyleBackColor = true;
            this.buttonExecute.Click += new System.EventHandler(this.buttonExecute_Click);
            // 
            // textBoxReason
            // 
            this.textBoxReason.Location = new System.Drawing.Point(9, 81);
            this.textBoxReason.Name = "textBoxReason";
            this.textBoxReason.Size = new System.Drawing.Size(171, 19);
            this.textBoxReason.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "reason";
            // 
            // radioButtonBan
            // 
            this.radioButtonBan.AutoSize = true;
            this.radioButtonBan.Location = new System.Drawing.Point(7, 42);
            this.radioButtonBan.Name = "radioButtonBan";
            this.radioButtonBan.Size = new System.Drawing.Size(43, 16);
            this.radioButtonBan.TabIndex = 1;
            this.radioButtonBan.Text = "Ban";
            this.radioButtonBan.UseVisualStyleBackColor = true;
            // 
            // radioButtonKick
            // 
            this.radioButtonKick.AutoSize = true;
            this.radioButtonKick.Checked = true;
            this.radioButtonKick.Location = new System.Drawing.Point(7, 19);
            this.radioButtonKick.Name = "radioButtonKick";
            this.radioButtonKick.Size = new System.Drawing.Size(45, 16);
            this.radioButtonKick.TabIndex = 0;
            this.radioButtonKick.TabStop = true;
            this.radioButtonKick.Text = "Kick";
            this.radioButtonKick.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxHideLPOutput);
            this.groupBox2.Controls.Add(this.buttonUpdate);
            this.groupBox2.Controls.Add(this.checkBoxAutomatic);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.numericUpdate);
            this.groupBox2.Location = new System.Drawing.Point(436, 149);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(270, 100);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Update";
            // 
            // checkBoxHideLPOutput
            // 
            this.checkBoxHideLPOutput.AutoSize = true;
            this.checkBoxHideLPOutput.Location = new System.Drawing.Point(156, 23);
            this.checkBoxHideLPOutput.Name = "checkBoxHideLPOutput";
            this.checkBoxHideLPOutput.Size = new System.Drawing.Size(100, 16);
            this.checkBoxHideLPOutput.TabIndex = 4;
            this.checkBoxHideLPOutput.Text = "Hide \'lp\' output";
            this.checkBoxHideLPOutput.UseVisualStyleBackColor = true;
            this.checkBoxHideLPOutput.CheckedChanged += new System.EventHandler(this.checkBoxHideLPOutput_CheckedChanged);
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(9, 71);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(137, 23);
            this.buttonUpdate.TabIndex = 3;
            this.buttonUpdate.Text = "Manual Update";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // checkBoxAutomatic
            // 
            this.checkBoxAutomatic.AutoSize = true;
            this.checkBoxAutomatic.Location = new System.Drawing.Point(9, 23);
            this.checkBoxAutomatic.Name = "checkBoxAutomatic";
            this.checkBoxAutomatic.Size = new System.Drawing.Size(116, 16);
            this.checkBoxAutomatic.TabIndex = 2;
            this.checkBoxAutomatic.Text = "Automatic Update";
            this.checkBoxAutomatic.UseVisualStyleBackColor = true;
            this.checkBoxAutomatic.CheckedChanged += new System.EventHandler(this.checkBoxAutomatic_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(135, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "s";
            // 
            // numericUpdate
            // 
            this.numericUpdate.DecimalPlaces = 1;
            this.numericUpdate.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpdate.Location = new System.Drawing.Point(9, 45);
            this.numericUpdate.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpdate.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpdate.Name = "numericUpdate";
            this.numericUpdate.Size = new System.Drawing.Size(120, 19);
            this.numericUpdate.TabIndex = 0;
            this.numericUpdate.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpdate.ValueChanged += new System.EventHandler(this.numericUpdate_ValueChanged);
            // 
            // timerAutomatic
            // 
            this.timerAutomatic.Enabled = true;
            this.timerAutomatic.Tick += new System.EventHandler(this.timerAutomatic_Tick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.comboBoxTimeUnit);
            this.groupBox3.Controls.Add(this.numericDuration);
            this.groupBox3.Location = new System.Drawing.Point(436, 256);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(186, 83);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Ban Duration";
            // 
            // comboBoxTimeUnit
            // 
            this.comboBoxTimeUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTimeUnit.FormattingEnabled = true;
            this.comboBoxTimeUnit.Items.AddRange(new object[] {
            "minute(s)",
            "hour(s)",
            "day(s)",
            "month(s)",
            "year(s)"});
            this.comboBoxTimeUnit.Location = new System.Drawing.Point(7, 45);
            this.comboBoxTimeUnit.Name = "comboBoxTimeUnit";
            this.comboBoxTimeUnit.Size = new System.Drawing.Size(121, 20);
            this.comboBoxTimeUnit.TabIndex = 1;
            // 
            // numericDuration
            // 
            this.numericDuration.Location = new System.Drawing.Point(9, 19);
            this.numericDuration.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericDuration.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericDuration.Name = "numericDuration";
            this.numericDuration.Size = new System.Drawing.Size(120, 19);
            this.numericDuration.TabIndex = 0;
            this.numericDuration.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // columnKDRatio
            // 
            this.columnKDRatio.Text = "K/D ratio";
            this.columnKDRatio.Width = 70;
            // 
            // PlayerList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 351);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.listView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "PlayerList";
            this.Text = "PlayerList";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PlayerList_FormClosing);
            this.Load += new System.EventHandler(this.PlayerList_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpdate)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericDuration)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeaderID;
        private System.Windows.Forms.ColumnHeader columnPlayerID;
        private System.Windows.Forms.ColumnHeader columnPlayerName;
        private System.Windows.Forms.ColumnHeader columnPlayerPos;
        private System.Windows.Forms.ColumnHeader columnRotation;
        private System.Windows.Forms.ColumnHeader columnRemote;
        private System.Windows.Forms.ColumnHeader columnHealth;
        private System.Windows.Forms.ColumnHeader columnDeaths;
        private System.Windows.Forms.ColumnHeader columnZombieKills;
        private System.Windows.Forms.ColumnHeader columnPlayerKills;
        private System.Windows.Forms.ColumnHeader columnScore;
        private System.Windows.Forms.ColumnHeader columnLevel;
        private System.Windows.Forms.ColumnHeader columnSteamID;
        private System.Windows.Forms.ColumnHeader columnIP;
        private System.Windows.Forms.ColumnHeader columnPing;
        private System.Windows.Forms.Timer update;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonKick;
        private System.Windows.Forms.RadioButton radioButtonBan;
        private System.Windows.Forms.TextBox textBoxReason;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonExecute;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numericUpdate;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.CheckBox checkBoxAutomatic;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timerAutomatic;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox comboBoxTimeUnit;
        private System.Windows.Forms.NumericUpDown numericDuration;
        private System.Windows.Forms.CheckBox checkBoxHideLPOutput;
        private System.Windows.Forms.ColumnHeader columnKDRatio;
    }
}