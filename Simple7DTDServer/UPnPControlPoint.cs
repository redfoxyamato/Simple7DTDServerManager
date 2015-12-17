using System;
using System.Runtime.InteropServices;
using UPNPLib;
using NATUPNPLib;
using System.Net;
using System.Net.Sockets;

namespace Simple7DTDServer
{
    class UPnPControlPoint
    {
        private UPnPService Service { get; set; }
        private UPnPDevice GetDevice(IUPnPDeviceFinder finder, string typeUri)
        {
            foreach (UPnPDevice item in finder.FindByType(typeUri, 0))
            {
                return item;
            }
            return null;
        }
        private UPnPDevice GetDevice(IUPnPDeviceFinder finder)
        {
            UPnPDevice device = GetDevice(finder, "urn:schemas-upnp-org:service:WANPPPConnection:1");
            if (device == null)
            {
                device = GetDevice(finder, "urn:schemas-upnp-org:service:WANIPConnection:1");
            }
            return device;
        }
        private UPnPService GetService(UPnPDevice device, string serviceId)
        {
            try
            {
                return device.Services[serviceId];
            }
            catch
            {
                return null;
            }
        }
        private UPnPService GetService(UPnPDevice device)
        {
            UPnPService service = GetService(device, "urn:upnp-org:serviceId:WANPPPConn1");
            if (service == null)
            {
                service = GetService(device, "urn:upnp-org:serviceId:WANIPConn1");
            }
            if (service == null)
            {
                throw new Exception("Could not find device");
            }
            return service;
        }
        private UPnPService GetService()
        {
            UPnPDevice device = GetDevice(new UPnPDeviceFinder());
            if (device == null)
            {
                return null;
            }
            return GetService(device);
        }
        public UPnPControlPoint()
        {
            Service = GetService();
        }
        private object InvokeAction(string bstrActionName, object vInActionArgs)
        {
            if (Service == null)
            {
                return null;
            }
            try
            {
                object result = new object();
                Service.InvokeAction(bstrActionName, vInActionArgs, ref result);
                return result;
            }
            catch (COMException)
            {
                return null;
            }
        }
        public string GetExternalIPAddress()
        {
            object result = InvokeAction("GetExternalIPAddress", new object[] { });
            if (result == null)
            {
                return null;
            }
            return (string)((object[])result)[0];
        }
        public bool AddPortMapping(ushort port,string description)
        {
            string internalIP = getInternalIP();
            UPnPNAT upnp = new UPnPNAT();
            IStaticPortMappingCollection portmaps = upnp.StaticPortMappingCollection;
            if(portmaps == null)
            {
                //could not communicate with router.UPnP is disabled or network is not available
                return false;
            }
            try
            {
                portmaps.Add(port, "TCP", port, internalIP, true, description);
            }
            catch(COMException ex)
            {
                throw ex;
            }
            return true;
        }
        public bool DeletePortMapping(ushort port)
        {
            getInternalIP();
            UPnPNAT upnp = new UPnPNAT();
            IStaticPortMappingCollection portmaps = upnp.StaticPortMappingCollection;
            if (portmaps == null)
            {
                //could not communicate with router.UPnP is disabled or network is not available
                return false;
            }
            try
            {
                portmaps.Remove(port, "TCP");
            }
            catch (COMException)
            {
                return false;
            }
            return true;
        }

        private static string getInternalIP()
        {
            IPAddress[] entry = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress ip in entry)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "";
        }

        public static bool isUPnPEnabled()
        {
            try
            {
                UPnPNAT nat = new UPnPNAT();
                return nat.StaticPortMappingCollection != null;
            }
            catch
            {
                return false;
            }
        }
    }
}