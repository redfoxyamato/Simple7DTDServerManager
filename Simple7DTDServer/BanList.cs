using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using static Simple7DTDServer.Form1;

namespace Simple7DTDServer
{
    public partial class BanList : Form
    {

        public BanList()
        {
            InitializeComponent();
            Util.SetDoubleBuffering(listView1, true);
            updateBanList.Stop();
        }

        private void BanList_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Form1.SetBanListVisible(false);
        }

        public static List<BanInfo> getBanInfos()
        {

            string xmlPath = getNode("SaveGameFolder") == "" ? SERVERADMIN : getNode("SaveGameFolder") + "\\" + getNode("AdminFileName");

            List<BanInfo> banList = new List<BanInfo>();

            if (!File.Exists(xmlPath))
            {
                return banList;
            }

            FileStream fs = new FileStream(xmlPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            XmlTextReader reader = new XmlTextReader(fs);

            while(reader.Read())
            {
                if(reader.NodeType == XmlNodeType.Element)
                {
                    if(reader.Name == "blacklisted")
                    {
                        if(reader.MoveToFirstAttribute() && reader.Name == "steamID")
                        {
                            string steamid = reader.Value;
                            if(reader.MoveToNextAttribute() && reader.Name == "unbandate")
                            {
                                string unbanDate = reader.Value;
                                if (reader.MoveToNextAttribute() && reader.Name == "reason")
                                {
                                    string reason = reader.Value;
                                    DateTime dt = DateTime.Parse(unbanDate);

                                    BanInfo bi = new BanInfo(steamid, dt, reason);

                                    banList.Add(bi);

                                    //Util.WriteConsole("BanInfo Created.\n" + bi);
                                }
                            }
                        }
                    }
                }
            }
            reader.Close();

            return banList;
        }

        public bool isThereSteamid(string steamid)
        {
            foreach(ListViewItem item in listView1.Items)
            {
                if(item.Text == steamid)
                {
                    return true;
                }
            }
            return false;
        }

        public void updateBanList_Tick(object sender, EventArgs e)
        {
            List<BanInfo> banInfos = getBanInfos();

            List<string> steamidList = new List<string>();
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                steamidList.Add(item.Text);
            }

            for (int i = 0; i < banInfos.Count; i++)
            {
                if(banInfos[i].EXPIRATION < DateTime.Now)
                {
                    if (isConnectedToServer)
                    {
                        string steamid = banInfos[i].STEAMID;
                        TELNET.AddSendingCue("ban remove " + steamid);
                    }
                    Util.WriteConsole("Detected expired ban. Removing...");
                    banInfos.RemoveAt(i);
                }
            }


            listView1.Items.Clear();
            foreach(BanInfo info in banInfos)
            {
                if (!isThereSteamid(info.STEAMID))
                {
                    Util.WriteConsole("Adding ban item...");
                    string steamid = info.STEAMID;
                    string unbandate = info.EXPIRATION.ToString();
                    string reason = info.REASON;

                    ListViewItem item = new ListViewItem();

                    item.Text = steamid;
                    item.SubItems.Add(unbandate);
                    item.SubItems.Add(reason);

                    listView1.Items.Add(item);
                }
            }

            foreach(ListViewItem item in listView1.Items)
            {
                if(steamidList.Contains(item.Text))
                {
                    item.Selected = true;
                }
            }

            if (TELNET == null || !TELNET.IsConnected || listView1.SelectedItems.Count == 0)
            {
                button1.Enabled = false;
                return;
            }
            button1.Enabled = true;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BanList_Load(object sender, EventArgs e)
        {
            updateBanList.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach(ListViewItem item in listView1.SelectedItems)
            {
                string steamid = item.Text;
                TELNET.AddSendingCue("ban remove " + steamid);
                Util.WriteConsole("Removed ban from steamid:" + steamid + ".");
            }
        }

        private void updateLanguage_Tick(object sender, EventArgs e)
        {
            button1.Text = translateTo("banlist.remove");
        }
    }
    public class BanInfo
    {
        private string steamid, reason;
        private DateTime banExpiration;

        public string STEAMID
        {
            get { return steamid; }
        }
        public string REASON
        {
            get { return reason; }
        }
        public DateTime EXPIRATION
        {
            get { return banExpiration; }
        }

        public BanInfo(string id, DateTime dt,string reason)
        {
            steamid = id;
            banExpiration = dt;
            this.reason = reason;
        }

        public override string ToString()
        {
            return "[BanInfo]steamid:" + steamid + " exp:" + banExpiration + " reason:" + reason;
        }
    }
}
