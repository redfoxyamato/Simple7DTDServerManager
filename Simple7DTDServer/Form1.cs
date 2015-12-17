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

namespace Simple7DTDServer
{
    public partial class Form1 : Form
    {
        private static string extIP,baseDir,gameExe,serverCfg;
        private static int serverPort,telnetPort;
        private static bool hasServerStarted;
        private static bool portWarning;

        private static string language = "English";

        private static Process p;
        private static TelnetConnection tel;

        private static Settings settings = null;
        private static LanguageSelector langSelector;
        private static Config_Editor editor;
        private static string mainFolder;

        private static bool isServerPortOpening;

        public Form1()
        {
            InitializeComponent();
            extIP = "";
            connectCheck.Stop();
            timerServerUpdate.Stop();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            getSetting();
            setLanguage(settings.language);
            langSelector = new LanguageSelector(this,language);
            AddOwnedForm(langSelector);
            translateComponents();
            MessageBox.Show(translateTo("autoDetectGame"));
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
            gameExe = @"C:\Program Files (x86)\Steam\steamapps\common\7 Days To Die\7DaysToDie.exe";
            if(!File.Exists(gameExe))
            {
                MessageBox.Show(translateTo("cantFoundGame"));
                Close();
            }
            if(getNode("TelnetEnabled") !="true")
            {
                MessageBox.Show(translateTo("cantUseTelnet"));
                Close();
            }
            externalIP.Text = settings.extServerIP;
            serverExternalPort.Value = settings.extServerPort;
            if (UPnPControlPoint.isUPnPEnabled())
            {
                MessageBox.Show(translateTo("getGlobalIP"));
                extIP = "RETRIEVING";
                getLocalHostName();
            }
            else if(isInternetConnected())
            {
                MessageBox.Show("upnpNotAvailable");
                extIP = new WebClient().DownloadString("http://redfox32.info/ip.php");
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

        private Settings getSetting()
        {
            if(settings != null)
            {
                return settings;
            }
            settings = Settings.Default;
            return settings;
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
            return String.Format("{0}_{1}_{2}_{3}_{4}_{5}.{6}",year,month,day,hour,minutes,second,millisecond);
        }
        public static string getNode(string node_name,string name="")
        {
            string ret = "";
            if(name == "")
            {
                name = serverCfg;
            }
            if (IsFileLocked(name))
            {
                hasServerStarted = false;
                if(p != null)
                {
                    p.Kill();
                }
                return "";
            }
            XmlTextReader reader = new XmlTextReader(new StreamReader(name,Encoding.UTF8));
            while(reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if(reader.MoveToFirstAttribute())
                    {
                        if(reader.Value == node_name && reader.MoveToNextAttribute())
                        {
                            ret = reader.Value;
                            break;
                        }
                    }
                }
            }
            reader.Close();
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
            textBox1.Text += translateTo("openPort") + ":" + port + "\r\n";
            new Thread(() =>
            {
                UPnPControlPoint upnp = new UPnPControlPoint();
                if (upnp.AddPortMapping((ushort)port, description))
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
            }).Start();
        }

        private void closePort(int port)
        {
            textBox1.Text += translateTo("closePort") + ":" + port + "\r\n";
            new Thread(() =>
            {
                UPnPControlPoint upnp = new UPnPControlPoint();

                if (upnp.DeletePortMapping((ushort)port))
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
            }).Start();
        }
        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(editor != null)
            {
                MessageBox.Show("cantStartWhenEditting");
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
                    if(portMenu.Enabled && autoMapServer.Checked)
                    {
                        MessageBox.Show(translateTo("openServerPort"));
                        openPort(serverPort,"7DTD server");
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
                    tel.WriteLine("shutdown");
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
                tel = new TelnetConnection(host,port,textBox1);
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
                Console.WriteLine(translated);
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
            autoMapServer.Enabled = !flag && internalMode.Checked && UPnPControlPoint.isUPnPEnabled();
            Check.Enabled = isInternetConnected();
            internalMode.Enabled = !flag;
            externalMode.Enabled = flag2;
            externalIP.Enabled = flag2 && externalMode.Checked;
            serverExternalPort.Enabled = flag2 && externalMode.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tel.WriteLine(textBox2.Text);
            textBox2.Text = "";
        }

        private void timerServerUpdate_Tick(object sender, EventArgs e)
        {
            if(!p.HasExited && getIsConnected())
            {
                if (tel.getStoredSize() > 0)
                {
                    //サーバーから情報が送られてきた時のみ処理する
                    tel.Read();
                }
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
                MessageBox.Show(translateTo("closeServerPort"));
                closePort(serverPort);
                isServerPortOpening = false;
            }
        }

        private void saveSettings()
        {
            settings.language = language;
            settings.extServer = externalMode.Checked;
            settings.extServerIP = externalIP.Text;
            settings.extServerPort = (int)serverExternalPort.Value;
            settings.portWarning = portWarning;
            settings.serverAutoMapping = autoMapServer.Checked;
            settings.Save();
        }

        private bool isInternetConnected()
        {
            return System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
        }

        private void manageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor = new Config_Editor(language,mainFolder+@"lang\settings\",serverCfg);
            AddOwnedForm(editor);
            editor.ShowDialog();
            editor = null;
        }
        public static bool isPortOpen(int port)
        {
            if (!File.Exists("bin/PortChecker.exe") || !File.Exists("bin/TCPServer.exe"))
            {
                MessageBox.Show(translateTo("notEnoughFile"));
                return true;
            }
            string binPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\bin\";
            return PortChecker.isOpen(binPath,port);
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
            new Thread(() =>
            {
                MessageBox.Show(isPortOpen(serverPort) ? translateTo("portOpening") : translateTo("portClosing"));

            }).Start();
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
                new Thread(() =>
                {
                    UPnPControlPoint p = new UPnPControlPoint();
                    var result = p.GetExternalIPAddress();
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
                }).Start();
            }
            catch
            {
            }
        }
        private void test()
        {
        }
    }
}
