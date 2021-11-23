using System;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace NetworkScanner
{
    public partial class NetworkScanner : Form
    {
        NetPinger netPing = new NetPinger();
        bool continueScan = true;

        public NetworkScanner()
        {
            InitializeComponent();            
            netPing.RunPingSweepAsync();
            netPing.PrintEvent += PrintResults;
        }

        private void PrintResults(object sender, string e)
        {
            IpAddress.Text = netPing.myIpAddress;
            dataGridView1.Rows.Clear();
            foreach (var record in netPing.networkIpRecords)
            {
                dataGridView1.Rows.Add(record.Name, record.IpAddress, record.Manufacturer, record.MacAddress, record.DateCreated);
            }
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            if(StopButton.Text == "Stop")
            {
                continueScan = false;
                StopButton.Text = "Start";
            }
            else
            {
                continueScan = true;
                StopButton.Text = "Stop";
            }
        }

        private void timer(object sender, EventArgs e)
        {
            if(continueScan)
            {
                ValidateSecurityDetails();
                netPing.RunPingSweepAsync();
                netPing.PrintEvent += PrintResults;
            }
        }

        private void ValidateSecurityDetails()
        {
            if (EmailCheckbox.Checked && IsValidEmail(EmailTextBox.Text))
            {
                netPing.SendEmail = true;
                netPing.EmailAddress = EmailTextBox.Text;
            }
            else
            {
                netPing.SendEmail = false;
            }

            if (SmsCheckBox.Checked && !string.IsNullOrEmpty(SmsTextBox.Text))
            {
                netPing.SendText = true;
                netPing.PhoneNumber = SmsTextBox.Text;
            }
            else 
            {
                netPing.SendText = false;
            }
        }

        private bool IsValidEmail(string email)
        {
            if (email.Trim().EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
