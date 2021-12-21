using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOP.ComService.Controls
{
    public partial class PortModule : UserControl
    {
        public string BoxName { get => groupBox1.Text; set => groupBox1.Text = value; }

        public Parity parity { get; set; } = Parity.None;

        public int databit { get; set; } = 8;

        public StopBits stopbit { get; set; } = StopBits.One;

        public int baudrate { get; set; } = 9600;

        public event SerialDataReceived SerialDataReceived;
        public event TextDataReceived TextReceived;

        private SerialPort serialPort;
        private BindingList<string> listPort = new BindingList<string>();
        private bool isRunning = false;

        public int userId { get; set; }
        public string userName { get => txtUsername.Text; set => txtUsername.Text = value; }
        public bool autoStart { get => chkAuto.Checked; set => chkAuto.Checked = value; }

        public string comPort
        {
            get
            {
                var selectedItem = cbComPort.SelectedItem;
                return selectedItem != null ? selectedItem.ToString() : "";
            }

            set
            {
                var selectedItem = listPort.FirstOrDefault(p => p == value);
                if (selectedItem != null)
                    cbComPort.SelectedItem = selectedItem;
            }
        }

        public PortModule()
        {
            InitializeComponent();
            bsPortList.DataSource = listPort;
            cbComPort.DataSource = bsPortList;
            foreach (var item in SerialPortService.GetAvailableSerialPorts().ToList())
            {
                listPort.Add(item);
            }
            SerialPortService.PortsChanged += SerialPortService_PortsChanged;
        }

        private void SerialPortService_PortsChanged(object sender, PortsChangedArgs e)
        {
            if (e.EventType == EventType.Insertion)
            {
                foreach (var port in e.SerialPorts)
                {
                    this.Invoke(new Action(() =>
                    {
                        listPort.Add(port);
                    }));
                }
            }
            else
            {
                foreach (var port in e.SerialPorts)
                {
                    this.Invoke(new Action(() =>
                    {
                        var _currentPort = comPort;
                        var _portToRemove = listPort.FirstOrDefault(p => p == port);

                        if (_portToRemove != null)
                        {
                            if (_portToRemove == _currentPort && serialPort != null && isRunning)
                                btnStart.PerformClick();
                        }

                        listPort.Remove(_portToRemove);
                    }));
                }
            }

            this.Invoke(new Action(() =>
            {
                bsPortList.DataSource = listPort;
            }));
        }

        public void DoAutoStart()
        {
            if (chkAuto.Checked)
                btnStart.PerformClick();
        }

        public void LoadModuleInfo(PortInfoModel portInfo)
        {
            this.userId = portInfo.UserId;
            this.userName = portInfo.UserName;
            this.autoStart = portInfo.AutoStart;
            this.cbComPort.SelectedItem = portInfo.ComPort;
        }

        private void ToggleControlState(bool isEnabled)
        {
            lbStatus.Text = isEnabled ? "Dừng" : "Hoạt động";
            lbStatus.ForeColor = isEnabled ? Color.Red : Color.Green;
            cbComPort.Enabled = isEnabled;
            txtUsername.Enabled = isEnabled;
            chkAuto.Enabled = isEnabled;
            btnTest.Enabled = isEnabled;

            btnStart.Text = isEnabled ? "Kết nối" : "Ngắt kết nối";

            isRunning = !isEnabled;
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var msg = serialPort.ReadExisting();
            SerialDataReceived?.Invoke(this, new TextResultEventArg() { Message = msg });
        }

        private async void btnTest_Click(object sender, EventArgs e)
        {
            if (await TryGetUser())
                TestComPort();
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            if (await TryGetUser())
                StartComPort();
        }


        private async Task<bool> TryGetUser()
        {
            var result = new TextResultEventArg();

            var user = await DataProvider.GetUserByUsername(userName.Trim());

            if (user != null && user.User_Id != 0)
            {
                result.Message = $"[{groupBox1.Text}]Người dùng hợp lệ ({user.User_Id} - {user.User_UserName} - {user.User_FullName})";
                result.Succeeded = true;
                userId = user.User_Id;
            }
            else
            {
                result.Message = $"[{groupBox1.Text}]Người dùng không tồn tại";
            }

            TextReceived?.Invoke(this, result);

            return result.Succeeded;
        }

        private void TestComPort()
        {
            var result = new TextResultEventArg();

            SerialPort testserialPort = new SerialPort();

            try
            {
                testserialPort = new SerialPort(comPort, baudrate, parity, databit, stopbit);
                testserialPort.Open();
                string message = $"[{groupBox1.Text}]Kiểm tra kết nối thành công ({comPort})";
                result.Message = message;
                result.Succeeded = true;
            }
            catch (Exception)
            {
                string message = $"[{groupBox1.Text}]Kiểm tra kết nối thất bại ({comPort}) ";
                result.Message = message;
            }
            finally
            {
                if (testserialPort.IsOpen)
                    testserialPort.Close();

                testserialPort.Dispose();
            }

            TextReceived?.Invoke(this, result);
        }

        private void StartComPort()
        {
            var result = new TextResultEventArg();

            if (serialPort != null && (serialPort.IsOpen|| isRunning))
            {
                serialPort.Close();
                serialPort.DataReceived -= SerialPort_DataReceived;
                serialPort.Dispose();

                result.Message = $"[{groupBox1.Text}]Ngắt kết nối";
                result.Succeeded = true;
                ToggleControlState(true);
            }
            else
            {
                serialPort = new SerialPort();

                try
                {
                    serialPort = new SerialPort(comPort, baudrate, parity, databit, stopbit);
                    serialPort.DataReceived += SerialPort_DataReceived;
                    serialPort.Open();

                    result.Message = $"[{groupBox1.Text}]Kết nối port thành công";
                    result.Succeeded = true;
                    ToggleControlState(false);
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
                finally
                {
                    if (!serialPort.IsOpen)
                    {
                        serialPort.DataReceived -= SerialPort_DataReceived;
                        serialPort.Dispose();
                        ToggleControlState(true);
                    }
                }
            }

            TextReceived?.Invoke(this, result);
        }
    }

    public delegate void TextDataReceived(object sender, TextResultEventArg e);

    public delegate void SerialDataReceived(object sender, TextResultEventArg e);

    public class TextResultEventArg : EventArgs
    {
        public bool Succeeded { get; set; } = false;

        public string Message { get; set; } = string.Empty;
    }
}
