using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Simple7DTDServer
{
    public partial class EasyCommands : Form
    {
        Form1 mainForm;

        public EasyCommands(Form1 main)
        {
            InitializeComponent();
            saveTimer.Stop();
            mainForm = main;
        }

        private void update_Tick(object sender, EventArgs e)
        {
            tabControl1.Enabled = Form1.TELNET != null;
        }

        private void EasyCommands_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            mainForm.setEasyCommandsVisible(false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(message.Text == "")
            {
                return;
            }
            string str = message.Text;
            if(str.Contains(" "))
            {
                str = string.Format("{0}{1}{0}","\"",str);
            }
            Form1.TELNET.WriteLine("say " + message.Text,true);
            message.Text = "";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form1.TELNET.WriteLine("sa",true);
        }

        private void enabled_CheckedChanged(object sender, EventArgs e)
        {
            setAutoSaveEnabled(enabled.Checked);
        }

        public void setAutoSaveEnabled(bool b)
        {
            enabled.Checked = b;
            numericUpDown1.Enabled = b;
            if(!b)
            {
                saveTimer.Stop();
            }
            else
            {
                saveTimer.Start();
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            saveTimer.Interval = (int)numericUpDown1.Value * 1000;
        }

        private void saveTimer_Tick(object sender, EventArgs e)
        {
            if(Form1.TELNET != null)
            {
                Form1.TELNET.WriteLine("sa",false);
            }
        }
    }
}
