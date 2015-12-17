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
        public static bool isOpen(string binPath,int port)
        {
            {
                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName = binPath + "TCPServer.exe";
                info.Arguments = port.ToString();
                info.WorkingDirectory = binPath;
                info.UseShellExecute = false;
                info.CreateNoWindow = true;
                Process p = Process.Start(info);
            }
            try
            {
                string phpContent = new WebClient().DownloadString("http://redfox32.info/port.php?port=" + port);
                return phpContent.Contains("opened");
            }
            catch
            {
                return false;
            }
        }
    }
}
