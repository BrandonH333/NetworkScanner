
namespace NetworkScanner
{
    public partial class NetworkScanner
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {            
            this.MyIpAddressHeader = new System.Windows.Forms.Label();
            this.IpAddress = new System.Windows.Forms.Label();
            this.NetworkAddressHeader = new System.Windows.Forms.Label();
            this.AddressList = new System.Windows.Forms.Label();
            this.StopButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // MyIpAddressHeader
            // 
            this.MyIpAddressHeader.AutoSize = true;
            this.MyIpAddressHeader.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MyIpAddressHeader.Location = new System.Drawing.Point(25, 9);
            this.MyIpAddressHeader.Name = "MyIpAddressHeader";
            this.MyIpAddressHeader.Size = new System.Drawing.Size(222, 37);
            this.MyIpAddressHeader.TabIndex = 0;
            this.MyIpAddressHeader.Text = "Your IP address is";
            // 
            // IpAddress
            // 
            this.IpAddress.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.IpAddress.Location = new System.Drawing.Point(242, 9);
            this.IpAddress.Name = "IpAddress";
            this.IpAddress.Size = new System.Drawing.Size(181, 37);
            this.IpAddress.TabIndex = 1;
            // 
            // NetworkAddressHeader
            // 
            this.NetworkAddressHeader.AutoSize = true;
            this.NetworkAddressHeader.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NetworkAddressHeader.Location = new System.Drawing.Point(25, 62);
            this.NetworkAddressHeader.Name = "NetworkAddressHeader";
            this.NetworkAddressHeader.Size = new System.Drawing.Size(247, 37);
            this.NetworkAddressHeader.TabIndex = 2;
            this.NetworkAddressHeader.Text = "Network addresses:";
            // 
            // AddressList
            // 
            this.AddressList.Location = new System.Drawing.Point(38, 116);
            this.AddressList.Name = "AddressList";
            this.AddressList.Size = new System.Drawing.Size(308, 315);
            this.AddressList.TabIndex = 3;
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(668, 22);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(75, 23);
            this.StopButton.TabIndex = 4;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // NetworkScanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.AddressList);
            this.Controls.Add(this.NetworkAddressHeader);
            this.Controls.Add(this.IpAddress);
            this.Controls.Add(this.MyIpAddressHeader);
            this.Name = "NetworkScanner";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label MyIpAddressHeader;
        private System.Windows.Forms.Label IpAddress;
        private System.Windows.Forms.Label NetworkAddressHeader;
        private System.Windows.Forms.Label AddressList;
        private System.Windows.Forms.Button StopButton;
    }
}

