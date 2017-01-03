using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Simple7DTDServer
{
    public class PortChecker
    {
        public static bool isOpen(int port)
        {
            TCPServer server = new TCPServer(port);
            server.Start();
            bool flag = false;
            try
            {
                string url = "http://pomepome.chobi.net/port.php?port=" + port;
                if(File.Exists("portcheck_url.txt"))
                {
                    string[] strs = File.ReadAllLines("portcheck_url.txt");
                    if(strs.Length > 0)
                    {
                        url = strs[0].Replace("{PORT_NUM}", port + "");
                    }
                }
                Console.WriteLine("URL:{0}",url);
                WebClient wc = new WebClient();
                wc.Encoding = Encoding.UTF8;
                string phpContent = wc.DownloadString(url);
                Console.WriteLine("Content:\r\n{0}",phpContent);
                flag = phpContent.Contains("開いています");
            }
            catch { }
            server.Stop();
            return flag;
        }
    }
}
