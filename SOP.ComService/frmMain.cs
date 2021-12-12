using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOP.ComService
{
    public partial class frmMain : Form
    {
        private Parity parity = Parity.None;
        private int databit = 8;
        private StopBits stopbit = StopBits.One;
        private SerialPort serialPort;

        public frmMain()
        {
            InitializeComponent();
            this.Icon = Properties.Resources._6074_brainstorm_bulb_idea_jabber_light_icon;
            Init();

            if (StaticFields.isAutorun)
                btnStart.PerformClick();
        }

        private void Init()
        {
            //List Com port
            var listComPort = SerialPort.GetPortNames();
            cbComPort.DataSource = listComPort;

            //List Baudate
            var listBaudrate = new List<int>()
            {
                1200,
                2400,
                4800,
                9600,
                19200,
                38400,
                57600,
                115200,
            };

            cbBaudrate.DataSource = listBaudrate;
            cbBaudrate.SelectedIndex = 3;
        }

        private void AppendLog(string text)
        {
            InvokeMethod(() =>
            {
                textBox1.AppendText($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)} : {text}");
                textBox1.AppendText("\r\n");
            });
        }

        private void InvokeMethod(Action action)
        {
            this.Invoke(action);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            var port = cbComPort.SelectedValue.ToString();
            var baud = int.Parse(cbBaudrate.SelectedValue.ToString());

            var testserialPort = new SerialPort(port, baud, parity, databit, stopbit);
            try
            {
                testserialPort.Open();
                string message = $"[{port}][{baud}] Port test OK";
                AppendLog(message);
                Log.Information(message);
            }
            catch (Exception)
            {
                string message = $"[{port}][{baud}] Port test failed";
                AppendLog(message);
                Log.Error(message);
            }
            finally
            {
                if (testserialPort.IsOpen)
                    testserialPort.Close();

                testserialPort.Dispose();
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //Stop
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.Close();
                serialPort.DataReceived -= SerialPort_DataReceived;
                serialPort.Dispose();
                btnStart.Text = "Start";
                groupBox1.Enabled = true;
                btnTest.Enabled = true;
                AppendLog("Service Stopped");
                Log.Information("Service Stopped");
            }
            //Start
            else
            {
                var port = cbComPort.SelectedValue.ToString();
                var baud = int.Parse(cbBaudrate.SelectedValue.ToString());
                serialPort = new SerialPort(port, baud, parity, databit, stopbit);

                try
                {
                    serialPort.DataReceived += SerialPort_DataReceived;
                    serialPort.Open();

                    btnStart.Text = "Stop";
                    groupBox1.Enabled = false;
                    btnTest.Enabled = false;
                    AppendLog("Service Started Successfully");
                    Log.Information("Service Started Successfully");
                }
                catch (Exception)
                {
                    AppendLog("Service Failed To Start");
                    Log.Error("Service Failed To Start");
                }
                finally
                {
                    if (!serialPort.IsOpen)
                    {
                        serialPort.DataReceived -= SerialPort_DataReceived;
                        serialPort.Dispose();
                    }
                }
            }

        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var dataReceived = serialPort?.ReadExisting();

            ProcessMessage(dataReceived);
        }

        private void ProcessMessage(string message)
        {
            switch (message)
            {
                //Rất Hài Lòng
                case ("a5b"):
                    {
                        Log.Information($"Rated 5");
                        break;
                    };

                //Hài Lòng
                case ("c4d"):
                    {
                        Log.Information($"Rated 4");
                        break;
                    };

                //Chờ Lâu
                case ("e3f"):
                    {
                        Log.Information($"Rated 3");
                        break;
                    };

                //Nghiệp vụ kém
                case ("g2h"):
                    {
                        Log.Information($"Rated 2");
                        break;
                    };

                //Thái độ lồi lõm
                case ("i1k"):
                    {
                        Log.Information($"Rated 1");
                        break;
                    };

                default:
                    break;
            }
        }
    }
}
