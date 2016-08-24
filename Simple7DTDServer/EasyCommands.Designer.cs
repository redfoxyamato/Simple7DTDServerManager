using System;

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
            this.serverMessage = new System.Windows.Forms.GroupBox();
            this.message = new System.Windows.Forms.TextBox();
            this.say = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.autosave = new System.Windows.Forms.GroupBox();
            this.checkBoxHideSAOutput = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numericAutoSaveInterval = new System.Windows.Forms.NumericUpDown();
            this.enabled = new System.Windows.Forms.CheckBox();
            this.save = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.numericHour = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericDay = new System.Windows.Forms.NumericUpDown();
            this.update = new System.Windows.Forms.Timer(this.components);
            this.saveTimer = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.serverMessage.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.autosave.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericAutoSaveInterval)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericHour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDay)).BeginInit();
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
            this.tabControl1.Size = new System.Drawing.Size(417, 166);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.serverMessage);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(409, 140);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Chat";
            this.tabPage1.UseVisualStyleBackColor = true;
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
            // message
            // 
            this.message.Location = new System.Drawing.Point(6, 30);
            this.message.Name = "message";
            this.message.Size = new System.Drawing.Size(354, 19);
            this.message.TabIndex = 0;
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
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.autosave);
            this.tabPage2.Controls.Add(this.save);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(409, 140);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Save";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // autosave
            // 
            this.autosave.Controls.Add(this.checkBoxHideSAOutput);
            this.autosave.Controls.Add(this.label1);
            this.autosave.Controls.Add(this.numericAutoSaveInterval);
            this.autosave.Controls.Add(this.enabled);
            this.autosave.Location = new System.Drawing.Point(8, 6);
            this.autosave.Name = "autosave";
            this.autosave.Size = new System.Drawing.Size(370, 85);
            this.autosave.TabIndex = 1;
            this.autosave.TabStop = false;
            this.autosave.Text = "AutoSave";
            // 
            // checkBoxHideSAOutput
            // 
            this.checkBoxHideSAOutput.AutoSize = true;
            this.checkBoxHideSAOutput.Location = new System.Drawing.Point(21, 50);
            this.checkBoxHideSAOutput.Name = "checkBoxHideSAOutput";
            this.checkBoxHideSAOutput.Size = new System.Drawing.Size(101, 16);
            this.checkBoxHideSAOutput.TabIndex = 3;
            this.checkBoxHideSAOutput.Text = "hide \'sa\' output";
            this.checkBoxHideSAOutput.UseVisualStyleBackColor = true;
            this.checkBoxHideSAOutput.CheckedChanged += new System.EventHandler(this.checkBoxHideSAOutput_CheckedChanged);
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
            // numericAutoSaveInterval
            // 
            this.numericAutoSaveInterval.Enabled = false;
            this.numericAutoSaveInterval.Location = new System.Drawing.Point(104, 26);
            this.numericAutoSaveInterval.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericAutoSaveInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericAutoSaveInterval.Name = "numericAutoSaveInterval";
            this.numericAutoSaveInterval.Size = new System.Drawing.Size(58, 19);
            this.numericAutoSaveInterval.TabIndex = 1;
            this.numericAutoSaveInterval.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericAutoSaveInterval.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
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
            // save
            // 
            this.save.Location = new System.Drawing.Point(258, 97);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(120, 23);
            this.save.TabIndex = 0;
            this.save.Text = "Manual Save";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(409, 140);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Time";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.numericHour);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numericDay);
            this.groupBox1.Location = new System.Drawing.Point(9, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(392, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Time";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(215, 60);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Change";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(135, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "O\'clock";
            // 
            // numericHour
            // 
            this.numericHour.Location = new System.Drawing.Point(7, 54);
            this.numericHour.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.numericHour.Name = "numericHour";
            this.numericHour.Size = new System.Drawing.Size(120, 19);
            this.numericHour.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(133, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Day";
            // 
            // numericDay
            // 
            this.numericDay.Location = new System.Drawing.Point(7, 19);
            this.numericDay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericDay.Name = "numericDay";
            this.numericDay.Size = new System.Drawing.Size(120, 19);
            this.numericDay.TabIndex = 0;
            this.numericDay.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // update
            // 
            this.update.Enabled = true;
            this.update.Interval = 500;
            this.update.Tick += new System.EventHandler(this.update_Tick);
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
            this.ClientSize = new System.Drawing.Size(417, 166);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EasyCommands";
            this.Text = "Easy Commands";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EasyCommands_FormClosing);
            this.Load += new System.EventHandler(this.EasyCommands_Load_1);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.serverMessage.ResumeLayout(false);
            this.serverMessage.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.autosave.ResumeLayout(false);
            this.autosave.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericAutoSaveInterval)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericHour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDay)).EndInit();
            this.ResumeLayout(false);

        }

        private void EasyCommands_Load(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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
        private System.Windows.Forms.NumericUpDown numericAutoSaveInterval;
        private System.Windows.Forms.CheckBox enabled;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer saveTimer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericHour;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericDay;
        private System.Windows.Forms.CheckBox checkBoxHideSAOutput;
    }
}