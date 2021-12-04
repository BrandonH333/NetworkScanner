using CsvHelper;
using CsvHelper.Configuration;
using MailKit.Net.Smtp;
using MimeKit;
using NetworkScanner.Constants;
using NetworkScanner.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace NetworkScanner
{
    public class NetPinger
    {
        // Global IP variables        
        public string myIpAddress;
        public string baseIp;

        public int nFound = 0;
        static object lockObj = new object();

        // Network lists
        public List<Network> networkIpRecords = new List<Network>();
        private List<Network> networkDataList = new List<Network>();

        // Events
        public event EventHandler<string> PrintEvent;

        // Set CsvHelper configuration
        private CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            NewLine = Environment.NewLine,
        };

        // NetworkScanner data from WindowsForm
        public string EmailAddress;
        public bool SendEmail;
        public string PhoneNumber;
        public bool SendText;

        public async void RunPingSweepAsync()
        {
            // If our base ip is null, we need to get our current ip address and build our base ip
            if (string.IsNullOrEmpty(baseIp))
            {
                await GetMyIpAddress();
                baseIp = GetBaseIpAddress();
            }

            var directory = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;

            // Get our known ips from csv file
            try
            {
                using (var reader = new StreamReader($"{directory}\\CsvFiles\\NetworkIpRecords.csv"))
                using (var csv = new CsvReader(reader, config))
                {
                    networkIpRecords = csv.GetRecords<Network>().ToList();
                }
            }
            catch
            {
                // If a csv file does not exist, catch the exception but continue
            }

            // Empty our current list of ips
            networkDataList.Clear();

            var tasks = new List<Task>();

            nFound = 0;

            // Cycle through every ip from starting base + (StartIp through StopIp)
            for (int i = NetworkConstants.StartIp; i <= NetworkConstants.StopIp; i++)
            {
                // Create ip using base + i
                var ip = baseIp + i.ToString();

                var p = new Ping();

                // Make async request to ping the ip address and return completed task
                var task = await Task.FromResult(PingAndUpdateAsync(p, ip));
                tasks.Add(task);
            }

            // Due to multi threading, we need to make sure all tasks are completed before continuing
            await Task.WhenAll(tasks).ContinueWith(async t =>
            {
                // Get latest ping results and saved ping results
                var latestPingResults = networkDataList.Select(x => x.IpAddress);
                var savedPingResults = networkIpRecords.Select(x => x.IpAddress);

                // Return any difference between latest and saved ping results
                var pingDifference = latestPingResults.Except(savedPingResults);

                // If there is a difference, we want to invoke an email and send a text message as well as save the new IP to our csv file
                if (pingDifference.Any())
                {
                    foreach (var ipNotInCurrentRecords in pingDifference)
                    {
                        // Get the network data from our network list for the ip that is not in our current records
                        var newNetworkData = networkDataList.Find(x => x.IpAddress == ipNotInCurrentRecords);
                        newNetworkData.DateCreated = DateTime.Now;

                        // Get Mac Address data
                        newNetworkData = await GetMacAddressData(newNetworkData);

                        networkIpRecords.Add(newNetworkData);

                        // Record the network data in a csv file
                        using (var writer = new StreamWriter($"{directory}\\CsvFiles\\NetworkIpRecords.csv"))
                        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                        {
                            csv.WriteRecords(networkIpRecords);
                            writer.Flush();
                        }

                        if (SendEmail)
                        {
                            try
                            {
                                // Create message for email
                                var message = new MimeMessage();
                                message.From.Add(new MailboxAddress(EmailConstants.FromName, EmailConstants.FromAddress));
                                message.To.Add(new MailboxAddress(EmailConstants.ToName, EmailAddress));
                                message.Subject = EmailConstants.Subject;

                                message.Body = new TextPart("plain")
                                {
                                    Text = $"Network scanner has detected a new IP address on your network.  Please review the details below: \n" +
                                            $"Name: {newNetworkData.Name}\n" +
                                            $"IP Address: {newNetworkData.IpAddress}\n" +
                                            $"Manufacturer: {newNetworkData.Manufacturer}\n" +
                                            $"MAC Address: {newNetworkData.MacAddress}\n" +
                                            $"Detected: {newNetworkData.DateCreated}"
                                };

                                // Send the email
                                using (var client = new SmtpClient())
                                {
                                    await client.ConnectAsync(EmailConstants.SmtpEndpoint, EmailConstants.SmtpPort, EmailConstants.SslEnabled);
                                    await client.AuthenticateAsync(EmailConstants.SslUsername, EmailConstants.SslPassword);
                                    await client.SendAsync(message);
                                    await client.DisconnectAsync(true);
                                }
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine(ex);
                            }
                        }

                        if (SendText)
                        {
                            try
                            {
                                // Initialize the Twilio client
                                TwilioClient.Init(PhoneConstants.AccountSid, PhoneConstants.AuthToken);

                                // Send text message
                                var message = MessageResource.Create(
                                    body: $"Network scanner has detected a new IP address on your network.\n" +
                                            $"Name: {newNetworkData.Name}\n" +
                                            $"IP Address: {newNetworkData.IpAddress}\n" +
                                            $"Manufacturer: {newNetworkData.Manufacturer}\n" +
                                            $"MAC Address: {newNetworkData.MacAddress}\n" +
                                            $"Detected: {newNetworkData.DateCreated}",
                                    from: new Twilio.Types.PhoneNumber(PhoneConstants.FromNumber),
                                    to: new Twilio.Types.PhoneNumber(PhoneNumber)
                                );
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine(ex);
                            }
                        }
                    }
                }
            });

            // Invoke print event to display network data on windows form app
            PrintEvent?.Invoke(this, string.Empty);
        }

        private async Task<Network> GetMacAddressData(Network networkData)
        {
            var hostName = string.Empty;

            // Get the host name and if it doesn't exist, assign the ip address as the host name
            try
            {
                var host = await Dns.GetHostEntryAsync(networkData.IpAddress);
                var hostNameArray = host.HostName.ToString().Split('.');
                hostName = hostNameArray[0];
            }
            catch
            {
                hostName = networkData.IpAddress;
            }

            var macAddress = string.Empty;
            var manufacturer = string.Empty;

            // Get the mac address and look up the manufacturer
            try
            {
                macAddress = await GetMacAddress(networkData.IpAddress);
                manufacturer = await LookupMac(macAddress);

                if (manufacturer.Length > 30)
                {
                    manufacturer = string.Empty;
                }
            }
            catch
            {

            }

            networkData.Name = hostName;
            networkData.Manufacturer = manufacturer;
            networkData.MacAddress = macAddress;

            return networkData;
        }

        private async Task GetMyIpAddress()
        {
            // Get the host ip address and return the ip that is in the InterNetwork
            var host = await Dns.GetHostEntryAsync(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork && ip.ToString().StartsWith("192"))
                {
                    myIpAddress = ip.ToString();
                    break;
                }
            }
        }

        private string GetBaseIpAddress()
        {
            // Get array of ip numbers within PC ip address
            var ipSplit = myIpAddress.Split(".");
            var baseIp = string.Empty;

            // Build base IP with first 3 numbers of IP set since the last number we want to cycle 1 - 255
            for (int i = 0; i < 3; i++)
            {
                baseIp = baseIp + $"{ipSplit[i]}.";
            }

            return baseIp;
        }

        private async Task PingAndUpdateAsync(Ping ping, string ip)
        {
            // Send a ping request with given ip
            var reply = await ping.SendPingAsync(ip, NetworkConstants.Timeout).ConfigureAwait(false);

            // If the status is success then we can add it to our list of network ips
            if (reply.Status == IPStatus.Success)
            {
                networkDataList.Add(new Network { IpAddress = ip });

                // Ensures synchronization in the event two threads attempt to call this PingAndUpdateAsync method simultanteously because only one thread
                // will be able to update nFound which means the other thread will need to wait its turn.
                lock (lockObj)
                {
                    nFound++;
                }
            }
        }

        private async Task<string> GetMacAddress(string ipAddress)
        {
            var macAddress = string.Empty;
            System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
            pProcess.StartInfo.FileName = "arp";
            pProcess.StartInfo.Arguments = "-a " + ipAddress;
            pProcess.StartInfo.UseShellExecute = false;
            pProcess.StartInfo.RedirectStandardOutput = true;
            pProcess.StartInfo.CreateNoWindow = true;
            pProcess.Start();
            var strOutput = pProcess.StandardOutput.ReadToEnd();
            var substrings = strOutput.Split('-');

            if (substrings.Length >= 8)
            {
                macAddress = substrings[3].Substring(Math.Max(0, substrings[3].Length - 2))
                    + "-" + substrings[4] + "-" + substrings[5] + "-" + substrings[6]
                    + "-" + substrings[7] + "-"
                    + substrings[8].Substring(0, 2);

                return await Task.FromResult(macAddress);
            }
            else
            {
                return await Task.FromResult(string.Empty);
            }
        }

        private async Task<string> LookupMac(string MacAddress)
        {
            // Using API provided below, find the mac vendor
            var uri = new Uri("http://api.macvendors.com/" + WebUtility.UrlEncode(MacAddress));
            using (var wc = new HttpClient())
            {
                var name = await wc.GetStringAsync(uri);
                return name;
            }
        }
    }
}
