using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Simple7DTDServer
{
    public class TCPServer
    {
        int port;

        TcpListener listener;

        public TCPServer(int port_num)
        {
            port = port_num;
            listener = new TcpListener(IPAddress.Any, port_num);
        }

        public void Start()
        {
            Console.WriteLine("[TCPServer] Start listening TCP socket:{0}.", port);
            try
            {
                listener.Start();
            }
            catch
            {
                Console.WriteLine("[TCPServer] Cannot start TCP server. Maybe port number is already in use.");
            }
        }

        public void Stop()
        {
            Console.WriteLine("[TCPServer] Stop listening...");
            listener.Stop();
        }
    }
}
