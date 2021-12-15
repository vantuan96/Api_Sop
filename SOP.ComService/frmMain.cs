using Serilog;
using SOP.ComService.Controls;
using SOP.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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
            this.Icon = Properties.Resources._6074_brainstorm_bulb_idea_jabber_light_icon;
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
        }

        private void PortModule_TextReceived(object sender, Controls.TextResultEventArg result)
        {
            AppendLog(result.Message);
            if(result.Succeeded)
            {
                Log.Information(result.Message);
            }
            else
            {
                Log.Error(result.Message);
            }
        }

        private void PortModule_SerialDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var _sender = sender as PortModule;
            ProcessMessage(_sender.userId, e.ToString());
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

        private async void ProcessMessage(int userId, string message)
        {
            var ratingModel = new RatingResult()
            {
                RatingResult_UserId = userId,
            };

            StaticFields.RatingValue.TryGetValue(message, out int rating_id);

            if (rating_id == 0)
            {
                Log.Error($"Rating error : ({message})");
                AppendLog($"Rating error : ({message})");
                return;
            }

            ratingModel.RatingResult_RatingId = rating_id;

            var report = await DataProvider.PushRating(ratingModel);

            if (report.Succeeded)
            {
                var msgRate = $"[{ratingModel.RatingResult_UserId}] received rate grade {GetRatingTextByRatingId(rating_id)}";
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
    }
}
