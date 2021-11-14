﻿using System.Net;

namespace NetworkScanner
{
    public class HostData
    {
        public string hostName;
        public string ipAddress;
        public string[] hostNameArray;

        public HostData(IPHostEntry host, string ipAddress)
        {
            hostNameArray = host.HostName.ToString().Split('.');
            this.hostName = hostNameArray[0];
            this.ipAddress = ipAddress;
        }
    }
}