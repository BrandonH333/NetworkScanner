using System.Windows.Forms;

namespace NetworkScanner
{
    public partial class NetworkScanner : Form
    {
        NetPinger netPing = new NetPinger();

        public NetworkScanner()
        {
            InitializeComponent();            
            netPing.RunPingSweepAsync();
            while (netPing.print == false)
            {

            }
            PrintResults();
            netPing.ClearEvent += ClearList;
        }

        private void PrintResults()
        {
            IpAddress.Text = netPing.myIpAddress;
            foreach (var ip in netPing.ips)
            {                
                AddressList.Text = AddressList.Text + $"{ip}\r\n\r\n";
            }
        }

        private void ClearList(object sender, string e)
        {
            if (AddressList.Text != string.Empty)
            {
                if (InvokeRequired)
                {
                    // after we've done all the processing, 
                    this.Invoke(new MethodInvoker(delegate {
                        AddressList.Text = string.Empty;
                    }));
                    return;
                }
            }
        }

        private void StopButton_Click(object sender, System.EventArgs e)
        {
            netPing.Continue = false;
        }
    }
}
