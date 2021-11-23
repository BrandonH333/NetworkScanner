using System;

namespace NetworkScanner.Models
{
    public class Network
    {
        public string Name { get; set; }

        public string IpAddress { get; set; }

        public string Manufacturer { get; set; }

        public string MacAddress { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
