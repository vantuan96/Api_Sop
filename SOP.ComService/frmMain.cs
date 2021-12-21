using Newtonsoft.Json;
using Serilog;
using SOP.ComService.Controls;
using SOP.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOP.ComService
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.laocai_icon_removebg;
            this.trayIcon.Icon = Properties.Resources.laocai_icon_removebg;
            Init();
        }

        private void Init()
        {
            portModule1.SerialDataReceived += PortModule_SerialDataReceived;
            portModule2.SerialDataReceived += PortModule_SerialDataReceived;
            portModule3.SerialDataReceived += PortModule_SerialDataReceived;
            portModule4.SerialDataReceived += PortModule_SerialDataReceived;
            portModule5.SerialDataReceived += PortModule_SerialDataReceived;
            portModule6.SerialDataReceived += PortModule_SerialDataReceived;

            portModule1.TextReceived += PortModule_TextReceived;
            portModule2.TextReceived += PortModule_TextReceived;
            portModule3.TextReceived += PortModule_TextReceived;
            portModule4.TextReceived += PortModule_TextReceived;
            portModule5.TextReceived += PortModule_TextReceived;
            portModule6.TextReceived += PortModule_TextReceived;

            SerialPortService.PortsChanged += SerialPortService_PortsChanged;
        }

        private void SerialPortService_PortsChanged(object sender, PortsChangedArgs e)
        {
            if (e.EventType == EventType.Insertion)
            {
                InvokeMethod(() =>
                {
                    foreach (var port in e.SerialPorts)
                    {
                        var msg = $"Phát hiện cổng [{port}] đc thêm vào hệ thống";
                        AppendLog(msg);
                        Log.Information(msg);
                    }
                });
            }
            else
            {
                InvokeMethod(() =>
                {
                    foreach (var port in e.SerialPorts)
                    {
                        var msg = $"Phát hiện cổng [{port}] gỡ khỏi hệ thống";
                        AppendLog(msg);
                        Log.Information(msg);
                    }
                });
            }
        }

        private void PortModule_TextReceived(object sender, TextResultEventArg result)
        {
            AppendLog(result.Message);
            if (result.Succeeded)
            {
                Log.Information(result.Message);
            }
            else
            {
                Log.Error(result.Message);
            }
        }

        private void PortModule_SerialDataReceived(object sender, TextResultEventArg e)
        {
            var _sender = sender as PortModule;
            ProcessMessage(_sender, e.Message);
        }

        private void AppendLog(string text)
        {
            InvokeMethod(() =>
            {
                if (textBox1.Lines.Count() > 200)
                    textBox1.Clear();

                textBox1.AppendText($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)} : {text}");
                textBox1.AppendText("\r\n");
            });
        }

        private void InvokeMethod(Action action)
        {
            this.Invoke(action);
        }

        private async void ProcessMessage(PortModule box, string message)
        {
            var ratingModel = new RatingResult()
            {
                RatingResult_UserId = box.userId,
            };

            StaticFields.RatingValue.TryGetValue(message, out int rating_id);

            if (rating_id == 0)
            {
                var msg = $"[{box.BoxName}]Lỗi tín hiệu: ({message})";
                Log.Error(msg);
                AppendLog(msg);
                return;
            }

            ratingModel.RatingResult_RatingId = rating_id;

            var report = await DataProvider.PushRating(ratingModel);

            if (report.Succeeded)
            {
                var msgRate = $"[{box.BoxName}]{box.userName} nhận đánh giá [{GetRatingTextByRatingId(rating_id)}]";
                Log.Information(msgRate);
                AppendLog(msgRate);
            }
        }

        private string GetRatingTextByRatingId(int id)
        {
            switch (id)
            {
                case 5:
                    return "Rất hài lòng";

                case 4:
                    return "Hài lòng";

                case 3:
                    return "Chờ lâu";

                case 2:
                    return "Nghiệp vụ kém";

                case 1:
                    return "Thái độ kém";

                default:
                    return "";
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            LoadFormData(AppDomain.CurrentDomain.BaseDirectory + "maindata.dat");

            if (StaticFields.isAutorun)
            {
                Task.Run(() =>
                {
                    InvokeMethod(() =>
                    {
                        portModule1.DoAutoStart();
                        portModule2.DoAutoStart();
                        portModule3.DoAutoStart();
                        portModule4.DoAutoStart();
                        portModule5.DoAutoStart();
                        portModule6.DoAutoStart();
                    });
                });
            }
        }

        private void SaveFormData(string filepath)
        {
            var listInfo = new List<PortInfoModel>()
            {
                new PortInfoModel() { Index = 1, AutoStart = portModule1.autoStart, ComPort = portModule1.comPort, UserId = portModule1.userId, UserName = portModule1.userName },
                new PortInfoModel() { Index = 2, AutoStart = portModule2.autoStart, ComPort = portModule2.comPort, UserId = portModule2.userId, UserName = portModule2.userName },
                new PortInfoModel() { Index = 3, AutoStart = portModule3.autoStart, ComPort = portModule3.comPort, UserId = portModule3.userId, UserName = portModule3.userName },
                new PortInfoModel() { Index = 4, AutoStart = portModule4.autoStart, ComPort = portModule4.comPort, UserId = portModule4.userId, UserName = portModule4.userName },
                new PortInfoModel() { Index = 5, AutoStart = portModule5.autoStart, ComPort = portModule5.comPort, UserId = portModule5.userId, UserName = portModule5.userName },
                new PortInfoModel() { Index = 6, AutoStart = portModule6.autoStart, ComPort = portModule6.comPort, UserId = portModule6.userId, UserName = portModule6.userName }
            };


            var data = JsonConvert.SerializeObject(listInfo);
            var encryptedData = DataProvider.Encrypt(data);

            var bytedata = Encoding.UTF8.GetBytes(encryptedData);
            File.WriteAllBytes(filepath, bytedata);
        }

        private void LoadFormData(string filepath)
        {
            try
            {
                var bytedata = File.ReadAllBytes(filepath);
                var encryptedData = Encoding.UTF8.GetString(bytedata);
                var data = DataProvider.Decrypt(encryptedData);

                var listInfo = JsonConvert.DeserializeObject<List<PortInfoModel>>(data);

                if (listInfo != null)
                {
                    //Module1
                    var info1 = listInfo.FirstOrDefault(p => p.Index == 1);
                    if (info1 != null)
                    {
                        portModule1.autoStart = info1.AutoStart;
                        portModule1.comPort = info1.ComPort;
                        portModule1.userId = info1.UserId;
                        portModule1.userName = info1.UserName;
                    }

                    //Module2
                    var info2 = listInfo.FirstOrDefault(p => p.Index == 2);
                    if (info2 != null)
                    {
                        portModule2.autoStart = info2.AutoStart;
                        portModule2.comPort = info2.ComPort;
                        portModule2.userId = info2.UserId;
                        portModule2.userName = info2.UserName;
                    }

                    //Module3
                    var info3 = listInfo.FirstOrDefault(p => p.Index == 3);
                    if (info3 != null)
                    {
                        portModule3.autoStart = info3.AutoStart;
                        portModule3.comPort = info3.ComPort;
                        portModule3.userId = info3.UserId;
                        portModule3.userName = info3.UserName;
                    }

                    //Module4
                    var info4 = listInfo.FirstOrDefault(p => p.Index == 4);
                    if (info4 != null)
                    {
                        portModule4.autoStart = info4.AutoStart;
                        portModule4.comPort = info4.ComPort;
                        portModule4.userId = info4.UserId;
                        portModule4.userName = info4.UserName;
                    }

                    //Module5
                    var info5 = listInfo.FirstOrDefault(p => p.Index == 5);
                    if (info5 != null)
                    {
                        portModule5.autoStart = info5.AutoStart;
                        portModule5.comPort = info5.ComPort;
                        portModule5.userId = info5.UserId;
                        portModule5.userName = info5.UserName;
                    }

                    //Module6
                    var info6 = listInfo.FirstOrDefault(p => p.Index == 6);
                    if (info6 != null)
                    {
                        portModule6.autoStart = info6.AutoStart;
                        portModule6.comPort = info6.ComPort;
                        portModule6.userId = info6.UserId;
                        portModule6.userName = info6.UserName;
                    }
                }
            }
            catch { }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Hide();
                e.Cancel = true;
            }
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveFormData(AppDomain.CurrentDomain.BaseDirectory + "maindata.dat");
        }

        private void trayIcon_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Thoát chương trình ?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                trayIcon.Dispose();
                Application.Exit();
            }
        }
    }
}
