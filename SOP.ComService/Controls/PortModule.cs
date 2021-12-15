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

        public event SerialDataReceivedEventHandler SerialDataReceived;
        public event TextDataReceived TextReceived;

        private SerialPort serialPort;

        public int userId { get; set; }
        public bool autoStart { get => chkAuto.Checked; }

        public PortModule()
        {
            InitializeComponent();

            var listComPort = SerialPort.GetPortNames();
            cbComPort.DataSource = listComPort;
        }

        public void DoAutoStart()
        {
            if (chkAuto.Checked)
                btnStart.PerformClick();
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialDataReceived?.Invoke(sender, e);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            var result = new TextResultEventArg();

            var port = cbComPort.SelectedText;

            var testserialPort = new SerialPort(port, baudrate, parity, databit, stopbit);

            try
            {
                testserialPort.Open();
                string message = $"[{port}][{baudrate}] Port test OK";
                result.Message = message;
                result.Succeeded = true;
            }
            catch (Exception)
            {
                string message = $"[{port}][{baudrate}] Port test failed";
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

        private void btnStart_Click(object sender, EventArgs e)
        {
            var result = new TextResultEventArg();

            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.Close();
                serialPort.DataReceived -= SerialPort_DataReceived;
                serialPort.Dispose();

                result.Message = "Port Closed";
                result.Succeeded = true;
            }
            else
            {
                var port = cbComPort.SelectedText;

                serialPort = new SerialPort(port, baudrate, parity, databit, stopbit);

                try
                {
                    serialPort.DataReceived += SerialPort_DataReceived;
                    serialPort.Open();

                    result.Message = "Port Opened Successfully";
                    result.Succeeded = true;
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
                finally
                {
                    if (serialPort.IsOpen)
                        serialPort.Close();
                    serialPort.DataReceived -= SerialPort_DataReceived;
                    serialPort.Dispose();
                }
            }

            TextReceived?.Invoke(this, result);
        }

        public delegate void TextDataReceived(object sender, TextResultEventArg result);
    }

    public class TextResultEventArg : EventArgs
    {
        public bool Succeeded { get; set; } = false;

        public string Message { get; set; } = string.Empty;
    }
}
