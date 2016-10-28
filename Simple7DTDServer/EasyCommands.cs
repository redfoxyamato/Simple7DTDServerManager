using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static Simple7DTDServer.Form1;

namespace Simple7DTDServer
{
    public partial class EasyCommands : Form
    {
        Form1 mainForm;

        public EasyCommands(Form1 main,bool autoSaveEnabled,int day,int hour,int autoSaveInterval,bool hideSAOutput)
        {
            InitializeComponent();
            saveTimer.Stop();
            saveTimer.Interval = autoSaveInterval * 1000;
            mainForm = main;
            enabled.Checked = autoSaveEnabled;
            checkBoxHideSAOutput.Checked = hideSAOutput;
            if(enabled.Checked)
            {
                saveTimer.Start();
            }
            numericDay.Value = day;
            numericHour.Value = hour;
            numericAutoSaveInterval.Value = autoSaveInterval;
        }

        private void update_Tick(object sender, EventArgs e)
        {
            //TelnetConnection.SetHideSAOutput(checkBoxHideSAOutput.Checked);
            tabControl1.Enabled = TELNET != null && TELNET.IsConnected;
            mainForm.setEasyCommandsSettings(enabled.Checked, (int)numericDay.Value, (int)numericHour.Value, (int)numericAutoSaveInterval.Value, checkBoxHideSAOutput.Checked);
            TranslateComponents();
        }

        private void TranslateComponents()
        {
            tabPage1.Text = translateTo("tabpage.chat");
            tabPage2.Text = translateTo("tabpage.save");
            tabPage3.Text = translateTo("tabpage.time");
            serverMessage.Text = translateTo("chat.message");
            say.Text = translateTo("chat.say");
            enabled.Text = translateTo("save.enabled");
            label1.Text = translateTo("playerlist.seconds");
            checkBoxHideSAOutput.Text = translateTo("save.hideSA");
            save.Text = translateTo("save.manual");
            groupBox1.Text = translateTo("tabpage.time");
            label2.Text = translateTo("time.day");
            label3.Text = translateTo("time.oclock");
            button1.Text = translateTo("time.change");
        }

        private void EasyCommands_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            mainForm.setEasyCommandsVisible(false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (TELNET != null && TELNET.IsConnected)
            {
                if (message.Text == "")
                {
                    return;
                }
                string str = message.Text;
                if (str.Contains(" "))
                {
                    str = string.Format("{0}{1}{0}", "\"", str);
                }
                TELNET.AddSendingCue("say " + str);
                message.Text = "";
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (TELNET != null && TELNET.IsConnected)
            {
                TELNET.AddSendingCue("sa");
            }
        }

        private void enabled_CheckedChanged(object sender, EventArgs e)
        {
            setAutoSaveEnabled(enabled.Checked);
        }

        public void setAutoSaveEnabled(bool b)
        {
            enabled.Checked = b;
            numericAutoSaveInterval.Enabled = b;
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
            saveTimer.Interval = (int)numericAutoSaveInterval.Value * 1000;
        }

        private void saveTimer_Tick(object sender, EventArgs e)
        {
            if(TELNET != null && TELNET.IsConnected)
            {
                TELNET.AddSendingCue("sa");
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            int time = (int)(numericDay.Value - 1) * 24000 + (int)numericHour.Value * 1000;
            if (TELNET != null && TELNET.IsConnected)
            {
                TELNET.AddSendingCue("settime " + time);
            }
        }

        private void checkBoxHideSAOutput_CheckedChanged(object sender, EventArgs e)
        {
            TelnetConnection.SetHideSAOutput(checkBoxHideSAOutput.Checked);
        }

        private void EasyCommands_Load_1(object sender, EventArgs e)
        {

        }
    }
}
