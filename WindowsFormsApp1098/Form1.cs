using System;
using System.IO.Ports;
using System.Windows.Forms;
using LibreHardwareMonitor.Hardware;

namespace CPUTempFanController
{
    public partial class Form1 : Form
    {
        private SerialPort serialPort;
        private Computer computer;
        private float currentCPUTemp = 0;
        private int currentFanSpeed = 0;
        private bool isConnected = false;
        private string dataBuffer = "";  // NEW: Buffer for incomplete data

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                computer = new Computer
                {
                    IsCpuEnabled = true,
                    IsMotherboardEnabled = true
                };
                computer.Open();
                computer.Accept(new UpdateVisitor());
                LogMessage("Hardware monitor initialized successfully");
            }
            catch (Exception ex)
            {
                LogMessage($"Error: {ex.Message}");
                MessageBox.Show("Failed to initialize. Run as Administrator!",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            RefreshComPorts();

            serialPort = new SerialPort
            {
                BaudRate = 115200,
                DataBits = 8,
                Parity = Parity.None,
                StopBits = StopBits.One,
                ReadTimeout = 3000,        // Increased timeout
                WriteTimeout = 3000,       // Increased timeout
                ReadBufferSize = 8192,     // Larger buffer
                WriteBufferSize = 8192,    // Larger buffer
                NewLine = "\n",            // Explicit newline character
                DtrEnable = true,          // Enable DTR
                RtsEnable = true           // Enable RTS
            };

            serialPort.DataReceived += SerialPort_DataReceived;

            if (chkAutoDetect.Checked)
            {
                AutoDetectPico();
            }
        }

        private void RefreshComPorts()
        {
            cmbComPorts.Items.Clear();
            string[] ports = SerialPort.GetPortNames();

            if (ports.Length > 0)
            {
                cmbComPorts.Items.AddRange(ports);
                cmbComPorts.SelectedIndex = 0;
                LogMessage($"Found {ports.Length} COM port(s)");
            }
            else
            {
                LogMessage("No COM ports found");
            }
        }

        private void AutoDetectPico()
        {
            string[] ports = SerialPort.GetPortNames();

            foreach (string port in ports)
            {
                try
                {
                    LogMessage($"Testing {port}...");
                    SerialPort testPort = new SerialPort(port, 115200);
                    testPort.ReadTimeout = 1000;
                    testPort.Open();
                    testPort.WriteLine("PING");
                    System.Threading.Thread.Sleep(500);

                    if (testPort.BytesToRead > 0)
                    {
                        string response = testPort.ReadExisting();
                        if (response.Contains("PONG") || response.Contains("PICO_READY"))
                        {
                            testPort.Close();
                            LogMessage($"Pico found on {port}");

                            for (int i = 0; i < cmbComPorts.Items.Count; i++)
                            {
                                if (cmbComPorts.Items[i].ToString() == port)
                                {
                                    cmbComPorts.SelectedIndex = i;
                                    break;
                                }
                            }
                            return;
                        }
                    }
                    testPort.Close();
                }
                catch { }
            }

            LogMessage("Pico not found");
        }

        private void btnRefreshPorts_Click(object sender, EventArgs e)
        {
            RefreshComPorts();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (cmbComPorts.SelectedItem == null)
            {
                MessageBox.Show("Select a COM port", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                serialPort.PortName = cmbComPorts.SelectedItem.ToString();

                // Clear any old data before opening
                dataBuffer = "";

                serialPort.Open();

                // Clear buffers after opening
                serialPort.DiscardInBuffer();
                serialPort.DiscardOutBuffer();

                isConnected = true;
                lblConnectionStatus.Text = $"Connected to {serialPort.PortName}";
                lblConnectionStatus.ForeColor = System.Drawing.Color.Green;

                btnConnect.Enabled = false;
                btnDisconnect.Enabled = true;
                cmbComPorts.Enabled = false;
                btnRefreshPorts.Enabled = false;
                chkAutoDetect.Enabled = false;

                timerUpdate.Start();
                LogMessage($"Connected to {serialPort.PortName}");
            }
            catch (Exception ex)
            {
                LogMessage($"Connection error: {ex.Message}");
                MessageBox.Show($"Failed: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            DisconnectSerial();
        }

        private void DisconnectSerial()
        {
            try
            {
                timerUpdate.Stop();

                if (serialPort != null && serialPort.IsOpen)
                {
                    try
                    {
                        serialPort.WriteLine("SPEED:0");
                        System.Threading.Thread.Sleep(200);
                    }
                    catch { }

                    serialPort.Close();
                }

                isConnected = false;
                lblConnectionStatus.Text = "Disconnected";
                lblConnectionStatus.ForeColor = System.Drawing.Color.Red;

                btnConnect.Enabled = true;
                btnDisconnect.Enabled = false;
                cmbComPorts.Enabled = true;
                btnRefreshPorts.Enabled = true;
                chkAutoDetect.Enabled = true;

                LogMessage("Disconnected");
            }
            catch (Exception ex)
            {
                LogMessage($"Disconnect error: {ex.Message}");
            }
        }

        private void chkAutoDetect_CheckedChanged(object sender, EventArgs e)
        {
            cmbComPorts.Enabled = !chkAutoDetect.Checked;
            btnRefreshPorts.Enabled = !chkAutoDetect.Checked;

            if (chkAutoDetect.Checked && !isConnected)
            {
                AutoDetectPico();
            }
        }

        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            if (!isConnected || serialPort == null || !serialPort.IsOpen)
                return;

            try
            {
                // Clear buffer if it's getting too full
                if (serialPort.BytesToRead > 2048)
                {
                    serialPort.DiscardInBuffer();
                    dataBuffer = "";
                    LogMessage("Warning: Buffer cleared (overflow prevention)");
                }

                currentCPUTemp = GetCPUTemperature();
                lblTempValue.Text = $"{currentCPUTemp:F1} °C";

                if (currentCPUTemp < 50)
                    lblTempValue.ForeColor = System.Drawing.Color.Blue;
                else if (currentCPUTemp < 70)
                    lblTempValue.ForeColor = System.Drawing.Color.Orange;
                else
                    lblTempValue.ForeColor = System.Drawing.Color.Red;

                string command = $"TEMP:{currentCPUTemp:F1}\n";  // Explicit newline
                serialPort.Write(command);
                LogMessage($"Sent: TEMP:{currentCPUTemp:F1}");
            }
            catch (TimeoutException)
            {
                LogMessage("Send timeout - recovering...");
                try
                {
                    serialPort.DiscardInBuffer();
                    serialPort.DiscardOutBuffer();
                    dataBuffer = "";
                }
                catch { }
            }
            catch (Exception ex)
            {
                LogMessage($"Error: {ex.Message}");
            }
        }

        private float GetCPUTemperature()
        {
            if (computer == null)
                return 0;

            float maxTemp = 0;

            foreach (var hardware in computer.Hardware)
            {
                hardware.Update();

                if (hardware.HardwareType == HardwareType.Cpu)
                {
                    foreach (var sensor in hardware.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Temperature)
                        {
                            if (sensor.Value.HasValue && sensor.Value.Value > maxTemp)
                            {
                                maxTemp = sensor.Value.Value;
                            }
                        }
                    }
                }
            }

            return maxTemp;
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (!isConnected)
                return;

            try
            {
                // Use ReadExisting instead of ReadLine to avoid timeout
                string incomingData = serialPort.ReadExisting();

                if (string.IsNullOrEmpty(incomingData))
                    return;

                // Add to buffer
                dataBuffer += incomingData;

                // Process complete lines
                while (dataBuffer.Contains("\n") || dataBuffer.Contains("\r"))
                {
                    int newlineIndex = dataBuffer.IndexOfAny(new char[] { '\n', '\r' });
                    if (newlineIndex >= 0)
                    {
                        string line = dataBuffer.Substring(0, newlineIndex).Trim();
                        dataBuffer = dataBuffer.Substring(newlineIndex + 1);

                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            ProcessReceivedLine(line);
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                // Prevent buffer from growing too large
                if (dataBuffer.Length > 1024)
                {
                    dataBuffer = "";
                }
            }
            catch (TimeoutException)
            {
                // Silently ignore timeout in read
            }
            catch (Exception ex)
            {
                LogMessage($"Receive error: {ex.Message}");
            }
        }

        private void ProcessReceivedLine(string data)
        {
            try
            {
                if (data.StartsWith("ACK:"))
                {
                    LogMessage($"Received: {data}");
                }
                else if (data.StartsWith("STATUS:"))
                {
                    string[] parts = data.Substring(7).Split(',');
                    if (parts.Length >= 3)
                    {
                        if (int.TryParse(parts[2], out int speed))
                        {
                            currentFanSpeed = speed;

                            if (InvokeRequired)
                            {
                                Invoke(new Action(() =>
                                {
                                    lblSpeedValue.Text = $"{currentFanSpeed} %";
                                    progressBarFan.Value = Math.Min(Math.Max(currentFanSpeed, 0), 100);

                                    if (currentFanSpeed == 0)
                                        lblSpeedValue.ForeColor = System.Drawing.Color.Gray;
                                    else if (currentFanSpeed < 50)
                                        lblSpeedValue.ForeColor = System.Drawing.Color.Green;
                                    else if (currentFanSpeed < 75)
                                        lblSpeedValue.ForeColor = System.Drawing.Color.Orange;
                                    else
                                        lblSpeedValue.ForeColor = System.Drawing.Color.Red;
                                }));
                            }
                        }
                    }
                    LogMessage($"Received: {data}");
                }
                else if (data.StartsWith("PICO_READY") || data.StartsWith("PONG"))
                {
                    LogMessage($"Pico: {data}");
                }
                else if (data.StartsWith("SPEED_SET:"))
                {
                    LogMessage($"Received: {data}");
                }
            }
            catch (Exception ex)
            {
                LogMessage($"Process error: {ex.Message}");
            }
        }

        private void LogMessage(string message)
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            string logEntry = $"[{timestamp}] {message}";

            if (InvokeRequired)
            {
                try
                {
                    Invoke(new Action(() =>
                    {
                        txtLog.AppendText(logEntry + Environment.NewLine);
                        txtLog.SelectionStart = txtLog.Text.Length;
                        txtLog.ScrollToCaret();

                        // Limit log size
                        if (txtLog.Lines.Length > 1000)
                        {
                            txtLog.Text = string.Join(Environment.NewLine,
                                txtLog.Lines, txtLog.Lines.Length - 500, 500);
                        }
                    }));
                }
                catch { }
            }
            else
            {
                txtLog.AppendText(logEntry + Environment.NewLine);
                txtLog.SelectionStart = txtLog.Text.Length;
                txtLog.ScrollToCaret();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                timerUpdate.Stop();

                if (serialPort != null && serialPort.IsOpen)
                {
                    try
                    {
                        serialPort.WriteLine("SPEED:0");
                        System.Threading.Thread.Sleep(200);
                    }
                    catch { }
                    serialPort.Close();
                }

                if (computer != null)
                {
                    computer.Close();
                }
            }
            catch { }
        }
    }

    public class UpdateVisitor : IVisitor
    {
        public void VisitComputer(IComputer computer)
        {
            computer.Traverse(this);
        }

        public void VisitHardware(IHardware hardware)
        {
            hardware.Update();
            foreach (IHardware subHardware in hardware.SubHardware)
                subHardware.Accept(this);
        }

        public void VisitSensor(ISensor sensor) { }
        public void VisitParameter(IParameter parameter) { }
    }
}
