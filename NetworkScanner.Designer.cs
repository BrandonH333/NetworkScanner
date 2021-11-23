
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
            this.components = new System.ComponentModel.Container();
            this.MyIpAddressHeader = new System.Windows.Forms.Label();
            this.IpAddress = new System.Windows.Forms.Label();
            this.NetworkAddressHeader = new System.Windows.Forms.Label();
            this.StopButton = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SecurityLabel = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.EmailCheckbox = new System.Windows.Forms.CheckBox();
            this.SmsCheckBox = new System.Windows.Forms.CheckBox();
            this.EmailTextBox = new System.Windows.Forms.TextBox();
            this.SmsTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // MyIpAddressHeader
            // 
            this.MyIpAddressHeader.AutoSize = true;
            this.MyIpAddressHeader.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MyIpAddressHeader.Location = new System.Drawing.Point(25, 9);
            this.MyIpAddressHeader.Name = "MyIpAddressHeader";
            this.MyIpAddressHeader.Size = new System.Drawing.Size(198, 32);
            this.MyIpAddressHeader.TabIndex = 0;
            this.MyIpAddressHeader.Text = "Your IP address is";
            // 
            // IpAddress
            // 
            this.IpAddress.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.IpAddress.Location = new System.Drawing.Point(218, 9);
            this.IpAddress.Name = "IpAddress";
            this.IpAddress.Size = new System.Drawing.Size(181, 37);
            this.IpAddress.TabIndex = 1;
            // 
            // NetworkAddressHeader
            // 
            this.NetworkAddressHeader.AutoSize = true;
            this.NetworkAddressHeader.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NetworkAddressHeader.Location = new System.Drawing.Point(25, 64);
            this.NetworkAddressHeader.Name = "NetworkAddressHeader";
            this.NetworkAddressHeader.Size = new System.Drawing.Size(184, 32);
            this.NetworkAddressHeader.TabIndex = 2;
            this.NetworkAddressHeader.Text = "Network record:";
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
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 60000;
            this.timer1.Tick += new System.EventHandler(this.timer);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Desktop;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column1,
            this.Column4,
            this.Column5,
            this.Column2});
            this.dataGridView1.Location = new System.Drawing.Point(25, 104);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(745, 334);
            this.dataGridView1.TabIndex = 5;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Name";
            this.Column3.Name = "Column3";
            // 
            // Column1
            // 
            this.Column1.HeaderText = "IP Address";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Manufacturer";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "MAC Address";
            this.Column5.Name = "Column5";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Date Added";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // SecurityLabel
            // 
            this.SecurityLabel.AutoSize = true;
            this.SecurityLabel.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point);
            this.SecurityLabel.Location = new System.Drawing.Point(917, 69);
            this.SecurityLabel.Name = "SecurityLabel";
            this.SecurityLabel.Size = new System.Drawing.Size(99, 32);
            this.SecurityLabel.TabIndex = 6;
            this.SecurityLabel.Text = "Security";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.MenuBar;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.textBox1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox1.Location = new System.Drawing.Point(805, 104);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(329, 58);
            this.textBox1.TabIndex = 7;
            this.textBox1.Text = "Please choose your security options below if you would like to receive alerts whe" +
    "n a new IP address has entered your network.";
            // 
            // EmailCheckbox
            // 
            this.EmailCheckbox.AutoSize = true;
            this.EmailCheckbox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.EmailCheckbox.Location = new System.Drawing.Point(814, 177);
            this.EmailCheckbox.Name = "EmailCheckbox";
            this.EmailCheckbox.Size = new System.Drawing.Size(60, 23);
            this.EmailCheckbox.TabIndex = 8;
            this.EmailCheckbox.Text = "Email";
            this.EmailCheckbox.UseVisualStyleBackColor = true;
            // 
            // SmsCheckBox
            // 
            this.SmsCheckBox.AutoSize = true;
            this.SmsCheckBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SmsCheckBox.Location = new System.Drawing.Point(814, 245);
            this.SmsCheckBox.Name = "SmsCheckBox";
            this.SmsCheckBox.Size = new System.Drawing.Size(110, 23);
            this.SmsCheckBox.TabIndex = 9;
            this.SmsCheckBox.Text = "Text Message";
            this.SmsCheckBox.UseVisualStyleBackColor = true;
            // 
            // EmailTextBox
            // 
            this.EmailTextBox.Location = new System.Drawing.Point(814, 207);
            this.EmailTextBox.Name = "EmailTextBox";
            this.EmailTextBox.PlaceholderText = "Please enter a valid email address...";
            this.EmailTextBox.Size = new System.Drawing.Size(320, 23);
            this.EmailTextBox.TabIndex = 10;
            // 
            // SmsTextBox
            // 
            this.SmsTextBox.Location = new System.Drawing.Point(814, 275);
            this.SmsTextBox.Name = "SmsTextBox";
            this.SmsTextBox.PlaceholderText = "Please enter a valid phone number...";
            this.SmsTextBox.Size = new System.Drawing.Size(320, 23);
            this.SmsTextBox.TabIndex = 11;
            // 
            // NetworkScanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1165, 450);
            this.Controls.Add(this.SmsTextBox);
            this.Controls.Add(this.EmailTextBox);
            this.Controls.Add(this.SmsCheckBox);
            this.Controls.Add(this.EmailCheckbox);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.SecurityLabel);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.NetworkAddressHeader);
            this.Controls.Add(this.IpAddress);
            this.Controls.Add(this.MyIpAddressHeader);
            this.Name = "NetworkScanner";
            this.Text = "Network Scanner";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label MyIpAddressHeader;
        private System.Windows.Forms.Label IpAddress;
        private System.Windows.Forms.Label NetworkAddressHeader;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Label SecurityLabel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox EmailCheckbox;
        private System.Windows.Forms.CheckBox SmsCheckBox;
        private System.Windows.Forms.TextBox EmailTextBox;
        private System.Windows.Forms.TextBox SmsTextBox;
    }
}

