using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                string phpContent = new WebClient().DownloadString("http://pomepome.link/port.php?port=" + port);
                flag = phpContent.Contains("開いています");
            }
            catch { }
            server.Stop();
            return flag;
        }
    }
}
