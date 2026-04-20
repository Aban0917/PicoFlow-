namespace CPUTempFanController
{
    partial class Form1
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

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblCPUTemp = new System.Windows.Forms.Label();
            this.lblTempValue = new System.Windows.Forms.Label();
            this.lblFanSpeed = new System.Windows.Forms.Label();
            this.lblSpeedValue = new System.Windows.Forms.Label();
            this.cmbComPorts = new System.Windows.Forms.ComboBox();
            this.btnRefreshPorts = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.lblConnectionStatus = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.chkAutoDetect = new System.Windows.Forms.CheckBox();
            this.grpConnection = new System.Windows.Forms.GroupBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.grpStatus = new System.Windows.Forms.GroupBox();
            this.progressBarFan = new System.Windows.Forms.ProgressBar();
            this.timerUpdate = new System.Windows.Forms.Timer(this.components);
            this.grpConnection.SuspendLayout();
            this.grpStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblCPUTemp
            // 
            this.lblCPUTemp.AutoSize = true;
            this.lblCPUTemp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblCPUTemp.Location = new System.Drawing.Point(20, 30);
            this.lblCPUTemp.Name = "lblCPUTemp";
            this.lblCPUTemp.Size = new System.Drawing.Size(150, 17);
            this.lblCPUTemp.TabIndex = 0;
            this.lblCPUTemp.Text = "CPU Temperature:";
            // 
            // lblTempValue
            // 
            this.lblTempValue.AutoSize = true;
            this.lblTempValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblTempValue.ForeColor = System.Drawing.Color.Blue;
            this.lblTempValue.Location = new System.Drawing.Point(180, 25);
            this.lblTempValue.Name = "lblTempValue";
            this.lblTempValue.Size = new System.Drawing.Size(62, 24);
            this.lblTempValue.TabIndex = 1;
            this.lblTempValue.Text = "-- °C";
            // 
            // lblFanSpeed
            // 
            this.lblFanSpeed.AutoSize = true;
            this.lblFanSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblFanSpeed.Location = new System.Drawing.Point(20, 65);
            this.lblFanSpeed.Name = "lblFanSpeed";
            this.lblFanSpeed.Size = new System.Drawing.Size(93, 17);
            this.lblFanSpeed.TabIndex = 2;
            this.lblFanSpeed.Text = "Fan Speed:";
            // 
            // lblSpeedValue
            // 
            this.lblSpeedValue.AutoSize = true;
            this.lblSpeedValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblSpeedValue.ForeColor = System.Drawing.Color.Green;
            this.lblSpeedValue.Location = new System.Drawing.Point(180, 63);
            this.lblSpeedValue.Name = "lblSpeedValue";
            this.lblSpeedValue.Size = new System.Drawing.Size(49, 20);
            this.lblSpeedValue.TabIndex = 3;
            this.lblSpeedValue.Text = "-- %";
            // 
            // cmbComPorts
            // 
            this.cmbComPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbComPorts.FormattingEnabled = true;
            this.cmbComPorts.Location = new System.Drawing.Point(80, 55);
            this.cmbComPorts.Name = "cmbComPorts";
            this.cmbComPorts.Size = new System.Drawing.Size(150, 21);
            this.cmbComPorts.TabIndex = 4;
            // 
            // btnRefreshPorts
            // 
            this.btnRefreshPorts.Location = new System.Drawing.Point(240, 53);
            this.btnRefreshPorts.Name = "btnRefreshPorts";
            this.btnRefreshPorts.Size = new System.Drawing.Size(75, 23);
            this.btnRefreshPorts.TabIndex = 5;
            this.btnRefreshPorts.Text = "Refresh";
            this.btnRefreshPorts.UseVisualStyleBackColor = true;
            this.btnRefreshPorts.Click += new System.EventHandler(this.btnRefreshPorts_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(80, 85);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(100, 30);
            this.btnConnect.TabIndex = 6;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Location = new System.Drawing.Point(190, 85);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(100, 30);
            this.btnDisconnect.TabIndex = 7;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // lblConnectionStatus
            // 
            this.lblConnectionStatus.AutoSize = true;
            this.lblConnectionStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblConnectionStatus.ForeColor = System.Drawing.Color.Red;
            this.lblConnectionStatus.Location = new System.Drawing.Point(10, 125);
            this.lblConnectionStatus.Name = "lblConnectionStatus";
            this.lblConnectionStatus.Size = new System.Drawing.Size(98, 15);
            this.lblConnectionStatus.TabIndex = 8;
            this.lblConnectionStatus.Text = "Disconnected";
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.Color.Black;
            this.txtLog.Font = new System.Drawing.Font("Consolas", 8F);
            this.txtLog.ForeColor = System.Drawing.Color.Lime;
            this.txtLog.Location = new System.Drawing.Point(15, 350);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(470, 150);
            this.txtLog.TabIndex = 9;
            // 
            // chkAutoDetect
            // 
            this.chkAutoDetect.AutoSize = true;
            this.chkAutoDetect.Checked = true;
            this.chkAutoDetect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoDetect.Location = new System.Drawing.Point(15, 30);
            this.chkAutoDetect.Name = "chkAutoDetect";
            this.chkAutoDetect.Size = new System.Drawing.Size(132, 17);
            this.chkAutoDetect.TabIndex = 10;
            this.chkAutoDetect.Text = "Auto-Detect Pico Port";
            this.chkAutoDetect.UseVisualStyleBackColor = true;
            this.chkAutoDetect.CheckedChanged += new System.EventHandler(this.chkAutoDetect_CheckedChanged);
            // 
            // grpConnection
            // 
            this.grpConnection.Controls.Add(this.lblPort);
            this.grpConnection.Controls.Add(this.chkAutoDetect);
            this.grpConnection.Controls.Add(this.cmbComPorts);
            this.grpConnection.Controls.Add(this.btnRefreshPorts);
            this.grpConnection.Controls.Add(this.btnConnect);
            this.grpConnection.Controls.Add(this.btnDisconnect);
            this.grpConnection.Controls.Add(this.lblConnectionStatus);
            this.grpConnection.Location = new System.Drawing.Point(15, 15);
            this.grpConnection.Name = "grpConnection";
            this.grpConnection.Size = new System.Drawing.Size(470, 150);
            this.grpConnection.TabIndex = 11;
            this.grpConnection.TabStop = false;
            this.grpConnection.Text = "Connection";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(15, 58);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(58, 13);
            this.lblPort.TabIndex = 11;
            this.lblPort.Text = "COM Port:";
            // 
            // grpStatus
            // 
            this.grpStatus.Controls.Add(this.progressBarFan);
            this.grpStatus.Controls.Add(this.lblCPUTemp);
            this.grpStatus.Controls.Add(this.lblTempValue);
            this.grpStatus.Controls.Add(this.lblFanSpeed);
            this.grpStatus.Controls.Add(this.lblSpeedValue);
            this.grpStatus.Location = new System.Drawing.Point(15, 180);
            this.grpStatus.Name = "grpStatus";
            this.grpStatus.Size = new System.Drawing.Size(470, 150);
            this.grpStatus.TabIndex = 12;
            this.grpStatus.TabStop = false;
            this.grpStatus.Text = "Status";
            // 
            // progressBarFan
            // 
            this.progressBarFan.Location = new System.Drawing.Point(23, 100);
            this.progressBarFan.Name = "progressBarFan";
            this.progressBarFan.Size = new System.Drawing.Size(420, 30);
            this.progressBarFan.TabIndex = 4;
            // 
            // timerUpdate
            // 
            this.timerUpdate.Interval = 1000;
            this.timerUpdate.Tick += new System.EventHandler(this.timerUpdate_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 520);
            this.Controls.Add(this.grpStatus);
            this.Controls.Add(this.grpConnection);
            this.Controls.Add(this.txtLog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "CPU Temperature Fan Controller";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.grpConnection.ResumeLayout(false);
            this.grpConnection.PerformLayout();
            this.grpStatus.ResumeLayout(false);
            this.grpStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblCPUTemp;
        private System.Windows.Forms.Label lblTempValue;
        private System.Windows.Forms.Label lblFanSpeed;
        private System.Windows.Forms.Label lblSpeedValue;
        private System.Windows.Forms.ComboBox cmbComPorts;
        private System.Windows.Forms.Button btnRefreshPorts;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Label lblConnectionStatus;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.CheckBox chkAutoDetect;
        private System.Windows.Forms.GroupBox grpConnection;
        private System.Windows.Forms.GroupBox grpStatus;
        private System.Windows.Forms.ProgressBar progressBarFan;
        private System.Windows.Forms.Timer timerUpdate;
        private System.Windows.Forms.Label lblPort;
    }
}