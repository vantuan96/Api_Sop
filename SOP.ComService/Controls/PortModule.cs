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

        public int userId { get; set; }
        public string userName { get; set; }
        public bool autoStart { get => chkAuto.Checked; }

        public PortModule()
        {
            InitializeComponent();
            RefreshPortList();
        }

        public void RefreshPortList()
        {
            var listComPort = SerialPort.GetPortNames();
            cbComPort.DataSource = listComPort;
        }

        public void DoAutoStart()
        {
            if (chkAuto.Checked)
                btnStart.PerformClick();
        }

        private void ToggleControlState(bool isEnabled)
        {
            lbStatus.Text = isEnabled ? "Dừng" : "Hoạt động";
            lbStatus.ForeColor = isEnabled ? Color.Red : Color.Green;
            cbComPort.Enabled = isEnabled;
            txtId.Enabled = isEnabled;
            chkAuto.Enabled = isEnabled;
            btnTest.Enabled = isEnabled;

            btnStart.Text = isEnabled ? "Khởi động" : "Dừng";
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

            var user = await DataProvider.GetUserByUsername(txtId.Text.Trim());

            if (user != null && user.User_Id != 0)
            {
                result.Message = $"[{groupBox1.Text}]Người dùng hợp lệ ({user.User_Id} - {user.User_UserName} - {user.User_FullName})";
                result.Succeeded = true;
                userId = user.User_Id;
                userName = user.User_UserName;
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

            var port = cbComPort.SelectedItem.ToString();

            SerialPort testserialPort = new SerialPort();

            try
            {
                testserialPort = new SerialPort(port, baudrate, parity, databit, stopbit);
                testserialPort.Open();
                string message = $"[{groupBox1.Text}]Kiểm tra kết nối thành công ({port})";
                result.Message = message;
                result.Succeeded = true;
            }
            catch (Exception)
            {
                string message = $"[{groupBox1.Text}]Kiểm tra kết nối thất bại ({port}) ";
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

            if (serialPort != null && serialPort.IsOpen)
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
                var port = cbComPort.SelectedItem.ToString();

                serialPort = new SerialPort();

                try
                {
                    serialPort = new SerialPort(port, baudrate, parity, databit, stopbit);
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
