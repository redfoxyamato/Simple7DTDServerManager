using System;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

namespace Simple7DTDServer
{
    enum Verbs
    {
        WILL = 251,
        WONT = 252,
        DO = 253,
        DONT = 254,
        IAC = 255
    }

    enum Options
    {
        SGA = 3
    }
    public class ListCopier<T>
    {
        public List<T> Copy(List<T> t)
        {
            if (t == null)
            {
                return null;
            }
            List<T> list = new List<T>();
            foreach (T val in t)
            {
                list.Add(val);
            }
            return list;
        }
    }

    public class Position
    {
        public double x;
        public double y;
        public double z;

        public override string ToString()
        {
            return string.Format("({0},{1},{2})", x, y, z);
        }
    }

    public class PlayerInfo
    {
        public int id;
        public int playerId;
        public string playerName;
        public Position playerPos;
        public Position rotate;
        public bool remote;
        public int health;
        public int deaths;
        public int zombieKills;
        public int playerKills;
        public int score;
        public int level;
        public string steamid;
        public string ip;
        public int ping;

        public static List<PlayerInfo> GetPlayerInfos(string str)
        {
            if (!str.Contains("steamid"))
            {
                return null;
            }

            str = str.Replace("\r", "").Replace(". id", ",id").Replace("(", "").Replace(")", "");

            List<PlayerInfo> ret = new List<PlayerInfo>();

            foreach (string temp in str.Split("\n".ToCharArray()))
            {
                string line = temp;
                if (!line.Contains("steamid"))
                {
                    continue;
                }

                line = line.Replace(" ", "").Replace("\n", "");

                string[] strs = line.Split(",".ToCharArray());

                PlayerInfo pInfo = null;

                foreach (string s in strs)
                {
                    Util.WriteConsole(s);
                }
                if (strs.Length == 19)
                {
                    pInfo = new PlayerInfo();

                    pInfo.playerPos = new Position();
                    pInfo.rotate = new Position();

                    pInfo.id = parseInt(GetAfterValue(strs[0]));
                    pInfo.playerId = parseInt(GetAfterValue(strs[1]));
                    pInfo.playerName = GetAfterValue(strs[2]);
                    pInfo.playerPos.x = parseDouble(GetAfterValue(strs[3]));
                    pInfo.playerPos.y = parseDouble(GetAfterValue(strs[4]));
                    pInfo.playerPos.z = parseDouble(GetAfterValue(strs[5]));
                    pInfo.rotate.x = parseDouble(GetAfterValue(strs[6]));
                    pInfo.rotate.y = parseDouble(GetAfterValue(strs[7]));
                    pInfo.rotate.z = parseDouble(GetAfterValue(strs[8]));
                    pInfo.remote = parseBool((GetAfterValue(strs[9])));
                    pInfo.health = parseInt(GetAfterValue(strs[10]));
                    pInfo.deaths = parseInt(GetAfterValue(strs[11]));
                    pInfo.zombieKills = parseInt(GetAfterValue(strs[12]));
                    pInfo.playerKills = parseInt(GetAfterValue(strs[13]));
                    pInfo.score = parseInt(GetAfterValue(strs[14]));
                    pInfo.level = parseInt(GetAfterValue(strs[15]));
                    pInfo.steamid = GetAfterValue(strs[16]);
                    pInfo.ip = GetAfterValue(strs[17]);
                    pInfo.ping = parseInt(GetAfterValue(strs[18]));
                }
                if (pInfo != null)
                {
                    ret.Add(pInfo);
                }
            }
            return ret;
        }

        public override string ToString()
        {
            if (this == null)
            {
                return "";
            }
            return string.Format("ID:{0} playerID:{1} Name:{2} Pos:({3},{4},{5}) Rot:({6},{7},{8})\r\n"
                               + "Remote:{9} Health:{10} Deaths:{11} ZombieKilled:{12} PlayerKilled:{13}\r\n" +
                                 "Score:{14} Level:{15} SteamID:{16} IP:{17} PING:{18}",
                                 id, playerId, playerName, playerPos.x, playerPos.y, playerPos.z, rotate.x, rotate.y, rotate.z,
                                 remote, health, deaths, zombieKills, playerKills,
                                 score, level, steamid, ip, ping);
        }

        public static int parseInt(string str)
        {
            int ret;
            if (int.TryParse(str, out ret))
            {
                return ret;
            }
            return 0;
        }

        public static double parseDouble(string str)
        {
            double ret;
            if (double.TryParse(str, out ret))
            {
                return ret;
            }
            return 0;
        }

        public static bool parseBool(string str)
        {
            bool ret;
            if (bool.TryParse(str, out ret))
            {
                return ret;
            }
            return false;
        }

        public static string GetAfterValue(string str)
        {
            string[] strs = str.Split("=".ToCharArray());
            if (strs.Length > 1)
            {
                return strs[1];
            }
            return strs[0];
        }
    }
    public class TelnetConnection
    {
        public delegate void PlayersEventHandler(List<PlayerInfo> pInfos, int playerCount);
        public event PlayersEventHandler OnPlayerListed;
        public enum Phase
        {
            WAITING_FOR_COMMAND_INPUT,
            WAITING_FOR_COMMAND_EXECUTION,
            WAITING_PLAYER_INFO
        }
        public Phase PHASE
        {
            get { return phase; }
        }

        public bool OnPlayerListedEnabled
        {
            get { return OnPlayerListed != null; }
        }
        private Phase phase = Phase.WAITING_FOR_COMMAND_INPUT;

        private static bool hideLPOutput;
        private static bool hideSAOutput;

        TcpClient tcpSocket;

        bool doResponseShow;

        private int TimeOutMs = 120;

        TextBox tBox;

        private List<PlayerInfo> pInfos;

        private List<string> waitingCue = new List<string>();

        private string buffer = "";

        public TelnetConnection(string Hostname, int Port, TextBox box,bool response)
        {
            tcpSocket = new TcpClient(Hostname, Port);
            tBox = box;
            doResponseShow = response;
            //OnPlayerListed += new PlayersEventHandler(OnPlayerUpdated);
        }

        public static void SetHideLPOutput(bool flag)
        {
            hideLPOutput = flag;
        }

        public static void SetHideSAOutput(bool flag)
        {
            hideSAOutput = flag;
        }

        public string Login(string Password)
        {
            int oldTimeOutMs = TimeOutMs;

            string s = Read();

            WriteLine(Password);
            TimeOutMs = oldTimeOutMs;
            return s;
        }

        [Obsolete("Use AddSendingCue(string command) instead.It's safer.")]
        public void WriteLine(string cmd)
        {
            Write(cmd + "\n");
        }

        public static string getTimeStamp()
        {
            DateTime dt = DateTime.Now;
            return string.Format("[{0}/{1}/{2} {3}:{4}:{5}.{6}]", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond);
        }

        [Conditional("DEBUG")]
        public void WritePlayerInfo(List<PlayerInfo> infos)
        {
            if (infos != null)
            {
                Console.WriteLine("Count:" + infos.Count);
                foreach (PlayerInfo info in infos)
                {
                    Console.WriteLine(info);
                }
            }
        }
        public void AddSendingCue(string cmd)
        {
            waitingCue.Add(cmd);
        }
        public void Write(string cmd)
        {
            try
            {
                if (!tcpSocket.Connected) return;
                byte[] buf = Encoding.ASCII.GetBytes(cmd.Replace("\0xFF", "\0xFF\0xFF"));
                tcpSocket.GetStream().Write(buf, 0, buf.Length);
            }
            catch { }
        }

        public string Read()
        {
            try
            {
                if (!IsConnected || tcpSocket == null)
                {
                    return "";
                }
                StringBuilder sb = new StringBuilder();
                do
                {
                    ParseTelnet(sb);
                    Thread.Sleep(TimeOutMs);
                } while (tcpSocket.Available > 0 && !sb.ToString().EndsWith("\n"));
                if (sb.ToString().Trim() != "")
                {
                    Util.WriteConsole("hideLP:" + hideLPOutput);
                    Util.WriteConsole("hideSA:" + hideSAOutput);

                    string line = sb.ToString().Trim();
                    if (phase == Phase.WAITING_FOR_COMMAND_EXECUTION)
                    {
                        Thread.Sleep(TimeOutMs);
                        if ((line.Contains("command 'lp'") || line.Contains("command 'listplayers'")) && line.Contains("Telnet"))
                        {
                            phase = Phase.WAITING_PLAYER_INFO;
                            Util.WriteConsole("Changing telnet phase to lp waiting mode.");
                            pInfos = new List<PlayerInfo>();
                            buffer = line + "\n";
                        }
                        else if((line.Contains("command 'sa'") || line.Contains("command 'saveworld'")))
                        {
                            Util.WriteConsole("Changing phase to waiting for command input.");
                            phase = Phase.WAITING_FOR_COMMAND_INPUT;
                            if (hideSAOutput)
                            {
                                sb = new StringBuilder();
                            }
                        }
                        else if (line.Contains("Executing command"))
                        {
                            Util.WriteConsole("Server executing command.");
                            Thread.Sleep(TimeOutMs);
                            Util.WriteConsole("Changing phase to waiting for command input.");
                            phase = Phase.WAITING_FOR_COMMAND_INPUT;
                        }
                        if (phase == Phase.WAITING_PLAYER_INFO && hideLPOutput)
                        {
                            sb = new StringBuilder();
                        }
                        if (phase == Phase.WAITING_PLAYER_INFO && line.Contains("Total"))
                        {

                            Util.WriteConsole("RETRIEVING PLAYER INFO:FAST");

                            pInfos = PlayerInfo.GetPlayerInfos(buffer);

                            if (pInfos != null && pInfos.Count > 0)
                            {
                                WritePlayerInfo(pInfos);
                            }

                            if (OnPlayerListed != null)
                            {
                                Util.WriteConsole("Player info event firing.");
                                if (pInfos == null || pInfos.Count == 0)
                                {
                                    OnPlayerListed(new List<PlayerInfo>(), 0);
                                }
                                else
                                {
                                    //int playerCount = PlayerInfo.parseInt(sb.ToString());
                                    ListCopier<PlayerInfo> copier = new ListCopier<PlayerInfo>();
                                    OnPlayerListed(copier.Copy(pInfos), pInfos.Count);
                                }
                            }
                            if(hideLPOutput)
                            {
                                sb = new StringBuilder();
                            }
                        }
                        else
                        {
                            //Console.WriteLine("New Line:" + line);
                            buffer += line + "\n";
                        }
                        if (line.Contains("sav") && hideSAOutput)
                        {
                            sb = new StringBuilder();
                        }
                        Util.WriteConsole("Changing phase to waiting for command input.");
                        phase = Phase.WAITING_FOR_COMMAND_INPUT;
                    }
                    else
                    {
                        if (phase == Phase.WAITING_PLAYER_INFO && line.Contains("Total"))
                        {
                            Util.WriteConsole("RETRIEVING PLAYER INFO:SLOW");

                            pInfos = PlayerInfo.GetPlayerInfos(buffer);

                            if (pInfos != null && pInfos.Count > 0)
                            {
                                WritePlayerInfo(pInfos);
                            }

                            if (OnPlayerListed != null)
                            {
                                Util.WriteConsole("Player info event firing.");
                                if (pInfos == null || pInfos.Count == 0)
                                {
                                    OnPlayerListed(new List<PlayerInfo>(), 0);
                                }
                                else
                                {
                                    //int playerCount = PlayerInfo.parseInt(sb.ToString());
                                    ListCopier<PlayerInfo> copier = new ListCopier<PlayerInfo>();
                                    OnPlayerListed(copier.Copy(pInfos), pInfos.Count);
                                }
                            }
                            if(hideLPOutput)
                            {
                                sb = new StringBuilder();
                            }
                            Util.WriteConsole("Changing phase to waiting for command input.");
                            phase = Phase.WAITING_FOR_COMMAND_INPUT;
                        }
                        else if(phase == Phase.WAITING_PLAYER_INFO)
                        {
                            //Console.WriteLine("New Line:" + line);
                            buffer += line + "\n";
                        }
                        if (line.Contains("sav") && hideSAOutput)
                        {
                            sb = new StringBuilder();
                        }
                        if(line.Contains("command 'lp'") && hideLPOutput)
                        {
                            sb = new StringBuilder();
                        }
                    }
                    Thread.Sleep(TimeOutMs);
                }
                if (phase == Phase.WAITING_FOR_COMMAND_INPUT && waitingCue.Count > 0)
                {
                    try
                    {
                        string cmd = waitingCue[0];
                        waitingCue.RemoveAt(0);

                        WriteLine(cmd);
                        phase = Phase.WAITING_FOR_COMMAND_EXECUTION;
                        Util.WriteConsole("Changing phase to waiting for command execution.");
                        Thread.Sleep(TimeOutMs);
                    }
                    catch { }
                }
                return sb.ToString();
            }
            catch(Exception ex)
            {
                Console.WriteLine("StackTrace:" + ex.StackTrace);
                return "";
            }
        }
        public void OnPlayerUpdated(List<PlayerInfo> pList, int count)
        {
            Console.WriteLine("Player Updated.");
        }
        public int getStoredSize()
        {
            return tcpSocket.Available;
        }
        public bool IsConnected
        {
            get { return tcpSocket.Connected; }
        }
        public string Buffer
        {
            get { return buffer; }
        }
        public void Disconnect(Form1 form)
        {
            if (form != null && doResponseShow)
            {
                tBox.AppendText(Form1.translateTo("disconnected") + "\r\n");
            }
            tcpSocket.Close();
        }

        public static bool canConnect(string host,int port)
        {
            bool ret = false;
                TcpClient cli = new TcpClient();
                try
                {
                    cli.Connect(host, port);
                    ret = true;
                }
                catch (SocketException ex)
                {
                }
                finally
                {
                    cli.Close();
                }
            return ret;
        }

        void ParseTelnet(StringBuilder sb)
        {
            while (tcpSocket.Available > 0)
            {
                int input = tcpSocket.GetStream().ReadByte();
                switch (input)
                {
                    case -1:
                        break;
                    case (int)Verbs.IAC:
                        // interpret as command
                        int inputverb = tcpSocket.GetStream().ReadByte();
                        if (inputverb == -1) break;
                        switch (inputverb)
                        {
                            case (int)Verbs.IAC:
                                //literal IAC = 255 escaped, so append char 255 to string
                                sb.Append(inputverb);
                                break;
                            case (int)Verbs.DO:
                            case (int)Verbs.DONT:
                            case (int)Verbs.WILL:
                            case (int)Verbs.WONT:
                                // reply to all commands with "WONT", unless it is SGA (suppres go ahead)
                                int inputoption = tcpSocket.GetStream().ReadByte();
                                if (inputoption == -1) break;
                                tcpSocket.GetStream().WriteByte((byte)Verbs.IAC);
                                if (inputoption == (int)Options.SGA)
                                    tcpSocket.GetStream().WriteByte(inputverb == (int)Verbs.DO ? (byte)Verbs.WILL : (byte)Verbs.DO);
                                else
                                    tcpSocket.GetStream().WriteByte(inputverb == (int)Verbs.DO ? (byte)Verbs.WONT : (byte)Verbs.DONT);
                                tcpSocket.GetStream().WriteByte((byte)inputoption);
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        sb.Append((char)input);
                        break;
                }
            }
        }
    }
}
