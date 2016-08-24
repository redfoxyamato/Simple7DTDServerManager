using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static Simple7DTDServer.Form1;

namespace Simple7DTDServer
{
    public partial class PlayerList : Form
    {
        private static string[] timeunits = new string[] { "minute", "hour", "day", "month", "year" };

        public PlayerList(bool isKickMode,bool automaticUpdate,bool hideLPOutput,int updateInterval,int banDuration,int comboBoxIndex)
        {
            InitializeComponent();
            Util.SetDoubleBuffering(listView1, true);
            timerAutomatic.Stop();
            timerAutomatic.Interval = updateInterval * 1000;
            if(automaticUpdate)
            {
                timerAutomatic.Start();
            }
            checkBoxAutomatic.Checked = automaticUpdate;
            checkBoxHideLPOutput.Checked = hideLPOutput;
            radioButtonKick.Checked = isKickMode;
            radioButtonBan.Checked = !isKickMode;
            numericUpdate.Value = updateInterval;
            numericDuration.Value = banDuration;
            comboBoxTimeUnit.SelectedIndex = comboBoxIndex;

        }

        private void update_Tick(object sender, EventArgs e)
        {
            if (TELNET != null && !Form1.TELNET.OnPlayerListedEnabled)
            {
                TELNET.OnPlayerListed += new TelnetConnection.PlayersEventHandler(OnPlayerListUpdated); // Subscribe event to retrieve information.
                Util.WriteConsole("PlayerInfo event subscribed.");
            }
            if (isConnectedToServer)
            {
                textBoxReason.Enabled = true;
                checkBoxAutomatic.Enabled = true;
                numericUpdate.Enabled = true;
                buttonUpdate.Enabled = true;
                if (listView1.SelectedItems.Count > 0)
                {
                    radioButtonBan.Enabled = true;
                    radioButtonKick.Enabled = true;
                    buttonExecute.Enabled = true;

                    numericDuration.Enabled = true;
                    comboBoxTimeUnit.Enabled = true;
                }
                else
                {
                    radioButtonBan.Enabled = false;
                    radioButtonKick.Enabled = false;
                    buttonExecute.Enabled = false;

                    numericDuration.Enabled = false;
                    comboBoxTimeUnit.Enabled = false;
                }
                checkBoxHideLPOutput.Enabled = true;
            }
            else
            {
                listView1.Items.Clear();
                radioButtonBan.Enabled = false;
                radioButtonKick.Enabled = false;
                buttonExecute.Enabled = false;
                textBoxReason.Enabled = false;
                checkBoxAutomatic.Enabled = false;
                numericUpdate.Enabled = false;
                buttonUpdate.Enabled = false;
                numericDuration.Enabled = false;
                comboBoxTimeUnit.Enabled = false;
                checkBoxHideLPOutput.Enabled = false;
            }
            INSTANCE.SetPlayerListSettings(radioButtonKick.Checked, checkBoxAutomatic.Checked, checkBoxHideLPOutput.Checked, (int)numericUpdate.Value, (int)numericDuration.Value, comboBoxTimeUnit.SelectedIndex);
            translateComponents();
        }
        private void PlayerList_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            SetPlayerListVisible(false);
        }

        private void translateComponents()
        {
            buttonExecute.Text = translateTo("playerlist.execute");
            label1.Text = translateTo("playerlist.reason");
            label2.Text = translateTo("playerlist.seconds");
            groupBox2.Text = translateTo("playerlist.update");
            checkBoxAutomatic.Text = translateTo("playerlist.automatic");
            checkBoxHideLPOutput.Text = translateTo("playerlist.hideLP");
            buttonUpdate.Text = translateTo("playerlist.manual");
            groupBox3.Text = translateTo("playerlist.duration");
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void PlayerList_Load(object sender, EventArgs e)
        {
            Size size = listView1.Size;
            size.Width += 1;
            listView1.Size = size;
        }

        public void OnPlayerListUpdated(List<PlayerInfo> pList, int pCount)
        {
            Util.WriteConsole("PlayerList.OnPlayerListUpdated");
            Invoke((MethodInvoker)delegate
            {
                List<string> selectedIds = new List<string>();
                foreach(ListViewItem item in listView1.SelectedItems)
                {
                    selectedIds.Add(item.SubItems[1].Text);
                }
                listView1.Items.Clear();
                foreach (PlayerInfo info in pList)
                {
                    ListViewItem item = new ListViewItem();

                    item.Text = info.id.ToString();
                    item.SubItems.Add(info.playerId.ToString());
                    item.SubItems.Add(info.playerName);
                    item.SubItems.Add(info.playerPos.ToString());
                    item.SubItems.Add(info.rotate.ToString());
                    item.SubItems.Add(info.remote.ToString());
                    item.SubItems.Add(info.health.ToString());
                    item.SubItems.Add(info.deaths.ToString());
                    item.SubItems.Add(info.zombieKills.ToString());
                    item.SubItems.Add(info.playerKills.ToString());
                    item.SubItems.Add(info.score.ToString());
                    item.SubItems.Add(info.level.ToString());
                    item.SubItems.Add(info.steamid);
                    item.SubItems.Add(info.ip);
                    item.SubItems.Add(info.ping.ToString());

                    listView1.Items.Add(item);
                }

                foreach(ListViewItem item in listView1.Items)
                {
                    if (selectedIds.Contains(item.SubItems[1].Text))
                    {
                        item.Selected = true;
                    }
                }
            });
        }


        private void numericUpdate_ValueChanged(object sender, EventArgs e)
        {
            timerAutomatic.Interval = (int)numericUpdate.Value * 1000;
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if(isConnectedToServer)
            {
                Util.WriteConsole("Updating player list...");
                TELNET.AddSendingCue("lp");
            }
        }

        private void checkBoxAutomatic_CheckedChanged(object sender, EventArgs e)
        {
            if(!isConnectedToServer)
            {
                return;
            }
            if(checkBoxAutomatic.Checked)
            {
                timerAutomatic.Start();
            }
            else
            {
                timerAutomatic.Stop();
            }
        }

        private void timerAutomatic_Tick(object sender, EventArgs e)
        {
            if (isConnectedToServer)
            {
                //Util.WriteConsole("Updating player list...");
                TELNET.AddSendingCue("lp");
            }
        }

        private void buttonExecute_Click(object sender, EventArgs e)
        {
            string cmd = "";
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                if (radioButtonBan.Checked)
                {
                    cmd = "ban add ";
                    cmd += item.SubItems[2].Text + " ";
                    cmd += numericDuration.Value + " ";
                    cmd += timeunits[comboBoxTimeUnit.SelectedIndex];
                }
                else
                {
                    cmd = "kick ";
                    cmd += item.SubItems[2].Text;
                }
                if(textBoxReason.Text.Trim() != "")
                {
                    cmd += " " + textBoxReason.Text;
                }
                if (isConnectedToServer && cmd.Trim() != "")
                {
                   TELNET.AddSendingCue(cmd);
                }
            }
        }

        private void checkBoxHideLPOutput_CheckedChanged(object sender, EventArgs e)
        {
            TelnetConnection.SetHideLPOutput(checkBoxHideLPOutput.Checked);
        }
    }
}
