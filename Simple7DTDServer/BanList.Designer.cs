namespace Simple7DTDServer
{
    partial class BanList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BanList));
            this.listView1 = new System.Windows.Forms.ListView();
            this.steamid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.unbandate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.reason = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.updateBanList = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.updateLanguage = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.steamid,
            this.unbandate,
            this.reason});
            this.listView1.FullRowSelect = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(13, 13);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(425, 237);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // steamid
            // 
            this.steamid.Text = "steamid";
            this.steamid.Width = 140;
            // 
            // unbandate
            // 
            this.unbandate.Text = "unbandate";
            this.unbandate.Width = 120;
            // 
            // reason
            // 
            this.reason.Text = "reason";
            this.reason.Width = 160;
            // 
            // updateBanList
            // 
            this.updateBanList.Enabled = true;
            this.updateBanList.Tick += new System.EventHandler(this.updateBanList_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(444, 199);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Remove";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // updateLanguage
            // 
            this.updateLanguage.Enabled = true;
            this.updateLanguage.Tick += new System.EventHandler(this.updateLanguage_Tick);
            // 
            // BanList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 262);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "BanList";
            this.Text = "BanList";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BanList_FormClosing);
            this.Load += new System.EventHandler(this.BanList_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Timer updateBanList;
        private System.Windows.Forms.ColumnHeader steamid;
        private System.Windows.Forms.ColumnHeader unbandate;
        private System.Windows.Forms.ColumnHeader reason;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer updateLanguage;
    }
}