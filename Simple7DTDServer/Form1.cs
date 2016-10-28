using System;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Diagnostics;
using System.Xml;
using System.Text;
using Simple7DTDServer.Properties;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Simple7DTDServer
{
    public partial class Form1 : Form
    {
        private static string extIP,baseDir,gameExe,serverCfg,serveradmin,current_version;
        public static int serverPort,telnetPort;
        private static bool hasServerStarted;
        private static bool portWarning;

        private bool autoSave, hideSA;
        private int day, hour, saveInterval;

        private bool kickMode, autoUpdate, hideLP;
        private int banDuration, comboBoxIndex;
        private double updateInterval;

        private static string language = "English";

        private static Process p;
        private static TelnetConnection tel;

        private static Settings settings = null;
        private static LanguageSelector langSelector;
        private static Config_Editor editor;
        private static EasyCommands commands;
        private static BanList banList;
        private static PlayerList playerList;
        private static string mainFolder;

        private static bool isServerPortOpening;

        public static TelnetConnection TELNET
        {
            get { return tel; }
        }

        private static UPnPControlPoint upnp = new UPnPControlPoint();

        public static bool isConnectedToServer
        {
            get { return tel == null ? false : tel.IsConnected; }
        }

        public static Form1 INSTANCE
        {
            get { return instance; }
        }
        private static Form1 instance;

        public static string SERVERADMIN
        {
            get { return serveradmin + getNode("AdminFileName"); }
        }

        public Form1()
        {
            InitializeComponent();
            instance = this;
            extIP = "";
            connectCheck.Stop();
            timerServerUpdate.Stop();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            getSetting();
            setLanguage(settings.language);
            langSelector = new LanguageSelector(this, language);
            AddOwnedForm(langSelector);

            TelnetConnection.SetHideLPOutput(hideLP);
            TelnetConnection.SetHideSAOutput(hideSA);

            commands = new EasyCommands(this,autoSave,day,hour,saveInterval,hideSA);
            commands.Show();
            commands.Visible = false;

            translateComponents();


            //MessageBox.Show(translateTo("autoDetectGame"));
            baseDir = @"C:\Program Files (x86)\Steam\steamapps\common\7 Days to Die";
            if (!Directory.Exists(baseDir))
            {
                MessageBox.Show(translateTo("cantFoundGameDirectory"));
                Close();
            }
            
            serverCfg = baseDir + @"\serverconfig.xml";
            if (!File.Exists(serverCfg))
            {
                MessageBox.Show(translateTo("cantFoundServerCfg"));
                Close();
            }
            serveradmin = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\7DaysToDie\Saves\";
            
            if (!Directory.Exists(serveradmin))
            {
                MessageBox.Show("serveradmin.cfg couldn't find. Try start up server once.");
                Close();
            }

            banList = new BanList();
            banList.Show();
            banList.Visible = false;

            playerList = new PlayerList(kickMode, autoUpdate, hideLP, updateInterval, banDuration, comboBoxIndex);
            playerList.Show();
            playerList.Visible = false;

            gameExe = baseDir + "\\7DaysToDie.exe";
            if (getNode("TelnetEnabled") != "true")
            {
                MessageBox.Show(translateTo("cantUseTelnet"));
                Close();
            }
            externalIP.Text = settings.extServerIP;
            serverExternalPort.Value = settings.extServerPort;
            /*
            if (UPnPControlPoint.isUPnPEnabled())
            {
                MessageBox.Show(translateTo("getGlobalIP"));
                extIP = "RETRIEVING";
                getLocalHostName();
            }
            */
            if (isInternetConnected())
            {
                //extIP = new WebClient().DownloadString("http://pomepome.link/ip.php");
            }
            if (settings.extServer)
            {
                internalMode.Checked = false;
                externalMode.Checked = true;
                externalIP.Enabled = true;
                serverExternalPort.Enabled = true;
                serverInfomation.Enabled = false;
            }
            autoMapServer.Checked = settings.serverAutoMapping;

            portWarning = settings.portWarning;
            serverPort = getPort();
            telnetPort = getTelnetPort();
            killServerProcesses();
            test();
            mainFolder = Path.GetDirectoryName(Application.ExecutablePath) + "\\";
        }

        public void setEasyCommandsSettings(bool autoSaveEnabled, int days, int hours,int autoSaveInterval,bool hideSAOutput)
        {
            autoSave = autoSaveEnabled;
            day = days;
            hour = hours;
            saveInterval = autoSaveInterval;
            hideSA = hideSAOutput;
        }

        public void SetPlayerListSettings(bool isKickMode, bool automaticUpdate, bool hideLPOutput, double updateInterval, int banDuration, int comboBoxIndex)
        {
            kickMode = isKickMode;
            autoUpdate = automaticUpdate;
            hideLP = hideLPOutput;
            this.updateInterval = updateInterval;
            this.banDuration = banDuration;
            this.comboBoxIndex = comboBoxIndex;
        }

        public void SetCurrentVersion(string currentVersion)
        {
            current_version = currentVersion;
        }

        private Settings getSetting()
        {
            if(settings != null)
            {
                return settings;
            }
            settings = Settings.Default;
            current_version = settings.currentVersion;
            autoSave = settings.autoSaveEnabled;
            day = settings.day;
            hour = settings.hour;
            saveInterval = settings.autoSaveInterval;
            hideSA = settings.hideSAOutput;
            kickMode = settings.kickMode;
            autoUpdate = settings.automaticUpdate;
            hideLP = settings.hideLPOutput;
            updateInterval = settings.updateInterval;
            banDuration = settings.banDuration;
            comboBoxIndex = settings.comboBoxIndex;
            return settings;
        }

        private void saveSettings()
        {
            settings.language = language;
            settings.extServer = externalMode.Checked;
            settings.extServerIP = externalIP.Text;
            settings.extServerPort = (int)serverExternalPort.Value;
            settings.portWarning = portWarning;
            settings.serverAutoMapping = autoMapServer.Checked;
            settings.currentVersion = current_version;

            settings.autoSaveEnabled = autoSave;
            settings.day = day;
            settings.hour = hour;
            settings.autoSaveInterval = saveInterval;
            settings.hideSAOutput = hideSA;

            settings.kickMode = kickMode;
            settings.automaticUpdate = autoUpdate;
            settings.hideLPOutput = hideLP;
            settings.updateInterval = updateInterval;
            settings.banDuration = banDuration;
            settings.comboBoxIndex = comboBoxIndex;

            settings.showBanList = banList.Visible;
            settings.showEasyCommands = commands.Visible;
            settings.showPlayerList = playerList.Visible;

            settings.Save();
        }

        private static void killServerProcesses()
        {
            Process[] procs = Process.GetProcessesByName("7DaysToDie");
            foreach (Process proc in procs)
            {
                if (proc.MainWindowHandle == IntPtr.Zero)
                {
                    //ウィンドウなしの場合(つまりサーバープロセスの場合)
                    Console.WriteLine("Killed ProcID=" + proc.Id);
                    proc.Kill();//切っておきます
                }
            }
        }
        private static bool IsFileLocked(string path)
        {
            FileStream stream = null;
            bool flag = false;
            try
            {
                stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch
            {
                flag = true;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }

            return flag;
        }
        public bool getExistingServer()
        {
            Process[] procs = Process.GetProcessesByName("7DaysToDie");
            foreach (Process proc in procs)
            {
                if (proc.MainWindowHandle == IntPtr.Zero)
                {
                    //ウィンドウなしの場合(つまりサーバープロセスの場合)
                    return true;
                }
            }
            return false;
        }
        public void startProcess(string name)
        {
            if(externalMode.Checked)
            {
                return;
            }
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = name;
            info.Arguments = string.Format("-quit -logfile {0}\\logs\\output_log_{1} -batchmode -nographics -configfile=serverconfig.xml -dedicated",Application.StartupPath,getTimeStanp());
            Console.WriteLine("GameExe:{0}",gameExe);
            p = Process.Start(info);
            p.WaitForInputIdle();
        }
        public static string getTimeStanp()
        {
            int year, month, day, hour, minutes, second,millisecond;
            DateTime date = DateTime.Now;
            year = date.Year;
            month = date.Month;
            day = date.Day;
            hour = date.Hour;
            minutes = date.Minute;
            second = date.Second;
            millisecond = date.Millisecond;
            return string.Format("{0}_{1}_{2}_{3}_{4}_{5}.{6}",year,month,day,hour,minutes,second,millisecond);
        }
        public static string getNode(string node_name,string name="")
        {
            string ret = "";
            if(name == "")
            {
                name = serverCfg;
            }
            FileStream fs = null;
            try
            {
                fs = new FileStream(name, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            }
            catch
            {
                if (fs != null)
                {
                    fs.Close();
                }
                return "";
            }
            XmlTextReader reader = null;
            try
            {
                reader = new XmlTextReader(new StreamReader(fs));
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.MoveToFirstAttribute())
                        {
                            if (reader.Value == node_name && reader.MoveToNextAttribute())
                            {
                                ret = reader.Value;
                                break;
                            }
                        }
                    }
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return ret;
        }
        public static string getLanguageFile()
        {
            return Path.GetDirectoryName(Application.ExecutablePath) + "\\" + "lang\\" + language + ".xml";
        }
        public int getPort()
        {
            if (serverPort != 0)
            {
                return serverPort;
            }
            try
            {
                string port_str = getNode("ServerPort");
                return int.Parse(port_str);
            }
            catch
            {
                return 0;
            }
        }
        public int getTelnetPort()
        {
            if(telnetPort != 0)
            {
                return telnetPort;
            }
            telnetPort = int.Parse(getNode("TelnetPort"));
            return telnetPort;
        }
        private bool getIsConnected()
        {
            if(tel == null)
            {
                return false;
            }
            return tel.IsConnected;
        }
        private void ServerInfomationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (getNode("ServerName") == null)
            {
                Console.WriteLine("Name null");
                return;
            }
            if (getNode("ServerPassword") == null)
            {
                Console.WriteLine("pass null");
                return;
            }
            if(getNode("ServerPort") == null)
            {
                Console.WriteLine("port null");
                return;
            }
            MessageBox.Show
                (
                    string.Format(
                        "{0}:\"{1}\"\r\n{2}:\"{3}\"\r\n{4}:{5}\r\n{6}:{7}"
                        , translateTo("serverName"), getNode("ServerName")
                        , translateTo("serverPass"), getNode("ServerPassword")
                        , translateTo("serverIP")  , extIP
                        , translateTo("serverPort"), getNode("ServerPort")
                    )        
                )
                               ;
        }
        private void openPort(int port, string description)
        {
            if(!upnp.isUPnPEnabled())
            {
                return;
            }
            textBox1.Text += translateTo("openPort") + ":" + port + "\r\n";
            Task.Factory.StartNew(() =>
            {
                if (upnp.AddPortMapping((ushort)port))
                {
                    if (port == serverPort)
                    {
                        MessageBox.Show(translateTo("openedServerPort"));
                        isServerPortOpening = true;
                    }
                }
                else
                {
                    Console.WriteLine("Portmapping failed.");
                    MessageBox.Show(translateTo("failedPortOpen"));
                }
            });
        }

        private void closePort(int port)
        {
            if(!upnp.isUPnPEnabled())
            {
                return;
            }
            textBox1.Text += translateTo("closePort") + ":" + port + "\r\n";
            Task.Factory.StartNew(() =>
            {

                if (upnp.RemovePortMapping((ushort)port))
                {
                    Console.WriteLine("closed port:" + port + " successfully");
                    if (port == serverPort)
                    {
                        MessageBox.Show(translateTo("closedServerPort"));
                        isServerPortOpening = false;
                    }
                }
                else
                {
                    Console.WriteLine("Portmapping failed.");
                    MessageBox.Show(translateTo("failedPortClose"));
                }
            });
        }
        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(editor != null)
            {
                MessageBox.Show("cantStartWhenEditing");
                return;
            }
            if (!hasServerStarted)
            {
                killServerProcesses();
                if (externalMode.Checked)
                {
                    if(externalIP.Text.Trim() == "")
                    {
                        MessageBox.Show(translateTo("notEnoughInformation"));
                        return;
                    }

                }
                else
                {
                    if(autoMapServer.Enabled && autoMapServer.Checked)
                    {
                        Console.WriteLine("Start port opening...");
                        MessageBox.Show(translateTo("openServerPort"));
                        openPort(serverPort,"7DTD server");
                        Console.WriteLine("port opened.");
                    }
                }
                hasServerStarted = true;
                startProcess(gameExe);
                connectCheck.Start();
                textBox1.Text = translateTo("startconnecting") + "\r\n";
            }
            else
            {
                MessageBox.Show(translateTo("alreadyExecuted"));
            }
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (hasServerStarted)
            {
                hasServerStarted = false;
                if (getIsConnected())
                {
                    tel.AddSendingCue("shutdown");
                }
            }
        }

        private void connectCheck_Tick(object sender, EventArgs e)
        {
            string host = externalMode.Checked ? externalIP.Text : "localhost";
            int port = externalMode.Checked ? (int)serverExternalPort.Value : telnetPort;
            if (TelnetConnection.canConnect(host,port))
            {
                
                textBox1.Text = translateTo("connected") + "\r\n\r\n";
                connectCheck.Stop();
                tel = new TelnetConnection(host,port,textBox1,true);
                if (getNode("TelnetPassword") == "")
                {
                    timerServerUpdate.Start();
                }
                else
                {
                    string loginResult = tel.Login(getNode("TelnetPassword"));
                    if (tel.IsConnected && loginResult.Contains("password:"))
                    {
                        timerServerUpdate.Start();
                    }
                    else
                    {
                        tel.Disconnect(this);
                    }
                }
            }
            else
            {

            }
        }
        private void translateComponents()
        {
            label1.Text = translateTo("output");
            label2.Text = translateTo("command");
            information.Text = translateTo("information");
            serverInfomation.Text = translateTo("serverInformation");
            connection.Text = translateTo("telnetConnection");
            manipulation.Text = translateTo("manipulation");
            startServer.Text = translateTo("startServer");
            stopServer.Text = translateTo("stopServer");
            configuration.Text = translateTo("configuration");
            manager.Text = translateTo("manager");
            send.Text = translateTo("send");
            languageMenu.Text = translateTo("language");
            portMenu.Text = translateTo("port");
            autoMapServer.Text = translateTo("autoMapServer");
            connectionType.Text = translateTo("connectionType");
            internalMode.Text = translateTo("internalMode");
            externalMode.Text = translateTo("externalMode");
            serverIP.Text = translateTo("serverIP");
            serverExtPort.Text = translateTo("serverPort");
            Check.Text = translateTo("check");
        }
        public static string translateTo(string str)
        {
            string path = getLanguageFile();
            string translated = getNode(str,path);
            if(translated != "")
            {
                return translated;
            }
            return str;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(!hasServerStarted && p != null && getExistingServer())
            {
                killServer.Enabled = true;
            }
            else
            {
                killServer.Enabled = false;
            }
            manager.Enabled = !getExistingServer();
            bool flag = getIsConnected() && hasServerStarted;
            bool flag2 = !flag && isInternetConnected();
            textBox2.Enabled = flag;
            send.Enabled = flag;
            flag = hasServerStarted;
            stopServer.Enabled = flag;
            startServer.Enabled = !flag;
            manager.Enabled = !flag ;
            autoMapServer.Enabled = flag2 && internalMode.Checked && upnp.isUPnPEnabled();
            Check.Enabled = isInternetConnected();
            internalMode.Enabled = !flag;
            externalMode.Enabled = flag2;
            externalIP.Enabled = flag2 && externalMode.Checked;
            serverExternalPort.Enabled = flag2 && externalMode.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tel.AddSendingCue(textBox2.Text);
            textBox2.Text = "";
        }

        private void timerServerUpdate_Tick(object sender, EventArgs e)
        {
            if(!p.HasExited && getIsConnected())
            {
                Task.Factory.StartNew(() =>
                {
                    string ret = tel.Read();
                    this.Invoke((MethodInvoker)delegate ()
                    {
                        textBox1.AppendText(ret);
                        textBox1.SelectionStart = textBox1.Text.Length - 1;
                    });
                });
            }
            else
            {
                timerServerUpdate.Stop();
                if (tel != null)
                {
                    tel.Disconnect(this);
                    hasServerStarted = false;
                    tel = null;

                }

                ClosePorts();
            }
        }

        private void ClosePorts()
        {
            if (isServerPortOpening)
            {
                closePort(serverPort);
                isServerPortOpening = false;
            }
        }

        private bool isInternetConnected()
        {
            return System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
        }

        private void manageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor = new Config_Editor(language,mainFolder+@"lang\settings\",serverCfg,current_version);
            AddOwnedForm(editor);
            editor.ShowDialog();
            editor = null;
        }
        public static bool isPortOpen(int port)
        {
            return PortChecker.isOpen(port);
        }
        private void connect_Click(object sender, EventArgs e)
        {
            MessageBox.Show(getIsConnected() ? translateTo("telnetConnected") : translateTo("telnetNotConnected"));
        }

        private void languageMenu_Click(object sender, EventArgs e)
        {
            langSelector.ShowDialog();
        }

        private void externalMode_CheckedChanged(object sender, EventArgs e)
        {
            bool flag = externalMode.Checked;
            serverExternalPort.Enabled = flag;
            externalIP.Enabled = flag;
            serverInfomation.Enabled = !flag;
        }

        private void autoMapServer_Click(object sender, EventArgs e)
        {
            if (!portWarning)
            {
                DialogResult dr = MessageBox.Show(this,translateTo("portSecurity"),translateTo("warning"),MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
                if(dr == DialogResult.No)
                {
                    autoMapServer.Checked = false;
                }
                else
                {
                    portWarning = true;
                }
            }
        }

        private void killServer_Click(object sender, EventArgs e)
        {
            killServerProcesses();

            ClosePorts();
        }

        private void Check_Click(object sender, EventArgs e)
        {
            MessageBox.Show(isPortOpen(serverPort) ? translateTo("portOpening") : translateTo("portClosing"));
        }

        private void easyCommandsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setEasyCommandsVisible(easyCommandsToolStripMenuItem.Checked);
        }
        public void setEasyCommandsVisible(bool flag)
        {
            easyCommandsToolStripMenuItem.Checked = flag;
            commands.Visible = flag;
        }

        private void reToolStripMenuItem_Click(object sender, EventArgs e)
        {
            upnp.getAllMappings();
        }

        private void getExternalIP_Tick(object sender, EventArgs e)
        {
            if(!externalMode.Checked && extIP.Trim() == "")
            {
                if(upnp.isUPnPEnabled())
                {
                    extIP = upnp.getExternalIPAddress();
                }
            }
        }

        private void portMenu_Click(object sender, EventArgs e)
        {
        }

        private void openWindow_Tick(object sender, EventArgs e)
        {
            openWindow.Stop();
            SetBanListVisible(settings.showBanList);
            setEasyCommandsVisible(settings.showEasyCommands);
            SetPlayerListVisible(settings.showPlayerList);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(p != null && tel != null && tel.IsConnected)
            {
                tel.WriteLine("shutdown");
            }
            ClosePorts();
            saveSettings();
        }

        public void setLanguage(string lang)
        {
            if(lang == "English" || lang == "Japanese")
            {
                language = lang;
                Console.WriteLine(language);
                translateComponents();
                return;
            }
            MessageBox.Show("不正な言語が渡されました。");
            language = "English";
        }

        public void getLocalHostName()
        {
            try
            {
                Task.Factory.StartNew(() =>
                {
                    var result = upnp.getExternalIPAddress();
                    if (result == null || result.Trim() == "")
                    {
                        extIP = "UNKNOWN";
                        MessageBox.Show(translateTo("failedGetGlobalIP"));
                    }
                    else
                    {
                        MessageBox.Show(translateTo("successGetGlobalIP"));
                        extIP = result;
                    }
                });
            }
            catch
            {
            }
        }

        private void playerListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetPlayerListVisible(playerListToolStripMenuItem.Checked);
        }

        public static void SetBanListVisible(bool flag)
        {
            if (instance != null && banList != null)
            {
                banList.Visible = flag;
                instance.banListToolStripMenuItem.Checked = flag;
            }
        }

        public static void SetPlayerListVisible(bool flag)
        {
            if (instance != null && playerList != null)
            {
                playerList.Visible = flag;
                instance.playerListToolStripMenuItem.Checked = flag;
            }
        }

        private void banListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetBanListVisible(banListToolStripMenuItem.Checked);
        }

        private void test()
        {
        }
        public bool isExternalMode
        {
            get { return externalMode.Checked; }
        }
        public string externalIPAddress
        {
            get { return externalIP.Text; }
        }
        public int externalPortNum
        {
            get { return (int)serverExternalPort.Value; }
        }
        public int telPort
        {
            get { return telnetPort; }
        }
    }
}
