using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Threading.Tasks;

namespace NetworkScanner
{
    public class NetPinger
    {
        public int StartIp = 1;
        public int StopIp = 255;
        public string ip;
        public int timeout = 100;
        public int nFound = 0;
        public string myIpAddress = string.Empty;

        static object lockObj = new object();
        Stopwatch stopwatch = new Stopwatch();
        public TimeSpan ts;       

        public IPHostEntry host;
        public List<string> ips = new List<string>();
        public List<Network> network = new List<Network>();
        public bool print = false;
        public bool Continue = true;
        public event EventHandler<string> ClearEvent;

        public async void RunPingSweepAsync()
        {
            while (Continue)
            {
                if (ips.Count > 0)
                {
                    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        NewLine = Environment.NewLine,
                    };
                }

                var tasks = new List<Task>();

                nFound = 0;

                GetMyIpAddress();
                var baseIp = GetBaseMyIpAddress();

                for (int i = StartIp; i <= StopIp; i++)
                {
                    ip = baseIp + i.ToString();

                    var p = new Ping();
                    var task = PingAndUpdateAsync(p, ip);
                    tasks.Add(task);
                }

                await Task.WhenAll(tasks).ContinueWith(t =>
                {
                    ClearEvent?.Invoke(this, string.Empty);
                    print = true;
                    var directory = new DirectoryInfo(Environment.CurrentDirectory);
                    using (var writer = new StreamWriter($"{directory.Parent.Parent.Parent.FullName}\\networkIpAddresses.csv"))
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.WriteRecords(network);
                        writer.Flush();
                    }

                    stopwatch.Restart();

                    while (ts < TimeSpan.FromMinutes(1) && Continue)
                    {
                        ts = stopwatch.Elapsed;
                    }

                    stopwatch.Stop();
                    ts = TimeSpan.Zero;
                    print = false;
                });
            }
        }

        private void GetMyIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    myIpAddress = ip.ToString();
                    break;
                }
            }
        }

        private async Task PingAndUpdateAsync(Ping ping, string ip)
        {
            var reply = await ping.SendPingAsync(ip, timeout).ConfigureAwait(false);

            if (reply.Status == IPStatus.Success)
            {
                ips.Add(ip);
                network.Add(new Network { IpAddress = ip, DateCreated = DateTime.Today });

                lock (lockObj)
                {
                    nFound++;
                }
            }
        }

        private string GetBaseMyIpAddress()
        {            
            var ipSplit = myIpAddress.Split(".");
            var baseIp = string.Empty;

            for (int i = 0; i < 3; i++)
            {
                baseIp = baseIp + $"{ipSplit[i]}.";
            }

            return baseIp;
        }
    }
}
