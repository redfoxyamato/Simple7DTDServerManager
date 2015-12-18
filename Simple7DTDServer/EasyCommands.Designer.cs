namespace Simple7DTDServer
{
    partial class EasyCommands
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EasyCommands));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.say = new System.Windows.Forms.Button();
            this.message = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.update = new System.Windows.Forms.Timer(this.components);
            this.serverMessage = new System.Windows.Forms.GroupBox();
            this.save = new System.Windows.Forms.Button();
            this.autosave = new System.Windows.Forms.GroupBox();
            this.enabled = new System.Windows.Forms.CheckBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.saveTimer = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.serverMessage.SuspendLayout();
            this.autosave.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(417, 291);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.serverMessage);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(409, 265);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Chat";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // say
            // 
            this.say.Location = new System.Drawing.Point(285, 55);
            this.say.Name = "say";
            this.say.Size = new System.Drawing.Size(75, 23);
            this.say.TabIndex = 1;
            this.say.Text = "Say";
            this.say.UseVisualStyleBackColor = true;
            this.say.Click += new System.EventHandler(this.button1_Click);
            // 
            // message
            // 
            this.message.Location = new System.Drawing.Point(6, 30);
            this.message.Name = "message";
            this.message.Size = new System.Drawing.Size(354, 19);
            this.message.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.autosave);
            this.tabPage2.Controls.Add(this.save);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(409, 265);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Save";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(409, 265);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // update
            // 
            this.update.Enabled = true;
            this.update.Interval = 500;
            this.update.Tick += new System.EventHandler(this.update_Tick);
            // 
            // serverMessage
            // 
            this.serverMessage.Controls.Add(this.message);
            this.serverMessage.Controls.Add(this.say);
            this.serverMessage.Location = new System.Drawing.Point(9, 7);
            this.serverMessage.Name = "serverMessage";
            this.serverMessage.Size = new System.Drawing.Size(379, 100);
            this.serverMessage.TabIndex = 2;
            this.serverMessage.TabStop = false;
            this.serverMessage.Text = "Server Message";
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(249, 210);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(120, 23);
            this.save.TabIndex = 0;
            this.save.Text = "Manual Save";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // autosave
            // 
            this.autosave.Controls.Add(this.label1);
            this.autosave.Controls.Add(this.numericUpDown1);
            this.autosave.Controls.Add(this.enabled);
            this.autosave.Location = new System.Drawing.Point(31, 99);
            this.autosave.Name = "autosave";
            this.autosave.Size = new System.Drawing.Size(370, 85);
            this.autosave.TabIndex = 1;
            this.autosave.TabStop = false;
            this.autosave.Text = "AutoSave";
            // 
            // enabled
            // 
            this.enabled.AutoSize = true;
            this.enabled.Location = new System.Drawing.Point(21, 27);
            this.enabled.Name = "enabled";
            this.enabled.Size = new System.Drawing.Size(64, 16);
            this.enabled.TabIndex = 0;
            this.enabled.Text = "Enabled";
            this.enabled.UseVisualStyleBackColor = true;
            this.enabled.CheckedChanged += new System.EventHandler(this.enabled_CheckedChanged);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Enabled = false;
            this.numericUpDown1.Location = new System.Drawing.Point(104, 26);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(58, 19);
            this.numericUpDown1.TabIndex = 1;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(168, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "s";
            // 
            // saveTimer
            // 
            this.saveTimer.Enabled = true;
            this.saveTimer.Tick += new System.EventHandler(this.saveTimer_Tick);
            // 
            // EasyCommands
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 291);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EasyCommands";
            this.Text = "Easy Commands";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EasyCommands_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.serverMessage.ResumeLayout(false);
            this.serverMessage.PerformLayout();
            this.autosave.ResumeLayout(false);
            this.autosave.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Timer update;
        private System.Windows.Forms.Button say;
        private System.Windows.Forms.TextBox message;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox serverMessage;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.GroupBox autosave;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.CheckBox enabled;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer saveTimer;
    }
}