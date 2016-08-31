using System;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Sockets;
using Mono.Nat;
using System.Collections.Generic;

namespace Simple7DTDServer
{
    public class UPnPControlPoint
    {
        private List<INatDevice> deviceList = new List<INatDevice>();
        public UPnPControlPoint()
        {
            Console.WriteLine("[UPnPControlPoint] starting discovery");
            NatUtility.DeviceFound += DeviceFound;
            NatUtility.DeviceLost  += DeviceLost;
            NatUtility.StartDiscovery();
        }

        private void DeviceFound(object sender, DeviceEventArgs args)
        {
            if(!deviceList.Contains(args.Device))
            {
                Console.WriteLine("Device found:" + args.Device);
                deviceList.Add(args.Device);
            }
        }

        private void DeviceLost(object sender, DeviceEventArgs args)
        {
            if(deviceList.Contains(args.Device))
            {
                deviceList.Remove(args.Device);
            }
        }

        public bool AddPortMapping(int port)
        {
            if(deviceList.Count == 0)
            {
                Console.WriteLine("There is no device to communicate.");
                return false;
            }
            bool flag = false;
            foreach (INatDevice device in deviceList)
            {
                try
                {
                    if (!isPortOpening(device, port))
                    {
                        device.CreatePortMap(new Mapping(Protocol.Tcp, port, port));
                        flag = true;
                        Console.WriteLine("[UPnPControlPoint] port {0} opened.",port);
                        getAllMappings();
                    }
                    else
                    {
                        Console.WriteLine("[UPnPControlPoint] Denied to open port.Removing port and re-adding...");
                        device.DeletePortMap(new Mapping(Protocol.Tcp, port, port));
                        return AddPortMapping(port);
                    }
                }
                catch
                {
                    continue;
                }
            }
            return flag;
        }
        public bool isPortOpening(INatDevice device,int port)
        {
            if(device == null)
            {
                return false;
            }
            foreach(Mapping mapping in device.GetAllMappings())
            {
                if(mapping.PublicPort == port)
                {
                    return true;
                }
            }
            return false;
        }
        public void getAllMappings()
        {
            if(isUPnPEnabled())
            {
                foreach(Mapping mapping in deviceList[0].GetAllMappings())
                {
                    Util.WriteConsole(string.Format("[UPnPControlPoint] Prot:{0} Expi:{1} pri_port:{2} pub_port:{3} ", mapping.Protocol,mapping.Expiration,mapping.PublicPort, mapping.PublicPort));
                }
            }
        }
        public bool RemovePortMapping(int port)
        {
            if (deviceList.Count == 0)
            {
                Console.WriteLine("[UPnPControlPoint] There is no device to communicate.");
                return false;
            }
            bool flag = false;
            foreach (INatDevice device in deviceList)
            {
                try
                {
                    if (isPortOpening(device, port))
                    {
                        device.DeletePortMap(new Mapping(Protocol.Tcp, port, port));
                        Console.WriteLine("[UPnPControlPoint] port {0} opened.",port);
                        getAllMappings();
                        flag = true;
                    }
                    else
                    {
                        Console.WriteLine("[UPnPControlPoint] Denied to close port.");
                    }
                }
                catch
                {
                    continue;
                }
            }
            return flag;
        }
        public string getExternalIPAddress()
        {
            if(deviceList.Count == 0)
            {
                Console.WriteLine("[UPnPControlPoint] There is no device to communicate.");
                return "";
            }
            return deviceList[0].GetExternalIP().ToString();
        }
        public bool isUPnPEnabled()
        {
            return deviceList.Count > 0;
        }
    }
}