﻿using System;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;

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

    class TelnetConnection
    {
        TcpClient tcpSocket;

        int TimeOutMs = 100;

        TextBox tBox;

        public TelnetConnection(string Hostname, int Port, TextBox box)
        {
            tcpSocket = new TcpClient(Hostname, Port);
            tBox = box;
        }

        public string Login(string Password)
        {
            int oldTimeOutMs = TimeOutMs;

            string s = Read();

            WriteLine(Password);
            TimeOutMs = oldTimeOutMs;
            return s;
        }

        public void WriteLine(string cmd)
        {
            Write(cmd + "\n");
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
            if (!tcpSocket.Connected) return null;
            StringBuilder sb = new StringBuilder();
            do
            {
                ParseTelnet(sb);
                Thread.Sleep(TimeOutMs);
            } while (tcpSocket.Available > 0);
            tBox.AppendText(sb.ToString());
            tBox.SelectionStart = tBox.Text.Length - 1;
            return sb.ToString();
        }
        public int getStoredSize()
        {
            return tcpSocket.Available;
        }
        public bool IsConnected
        {
            get { return tcpSocket.Connected; }
        }

        public void Disconnect(Form1 form)
        {
            if (form != null)
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
                Console.WriteLine(ex.Message);
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