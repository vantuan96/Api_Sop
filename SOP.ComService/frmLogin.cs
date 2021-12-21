using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOP.ComService
{
    public partial class frmLogin : Form
    {
        System.Timers.Timer autoLoginTimer;
        int remainingSecond = 15;

        public frmLogin()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.laocai_icon_removebg;
            this.DialogResult = DialogResult.OK;
            autoLoginTimer = new System.Timers.Timer()
            {
                Enabled = false,
                Interval = 1000,
                AutoReset = false
            };

            autoLoginTimer.Elapsed += AutoLoginTimer_Elapsed;
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            try
            {
                LoadLoginData(AppDomain.CurrentDomain.BaseDirectory + "logindata.dat");

                if (chkAuto.Checked)
                    autoLoginTimer.Start();
            }
            catch { }
        }

        private void AutoLoginTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            remainingSecond--;

            if (remainingSecond <= 0)
                this.BeginInvoke(new Action(() =>
                {
                    btnOK.PerformClick();
                }));

            else if (chkAuto.Checked)
            {
                this.BeginInvoke(new Action(() =>
                {
                    chkAuto.Text = $"Tự động đăng nhập trong ({remainingSecond}s)";
                    autoLoginTimer?.Start();
                }));
            }
            else
            {
                this.BeginInvoke(new Action(() =>
                {
                    chkAuto.Text = "Tự động đăng nhập";
                }));
            }
        }

        private async void btnOK_Click(object sender, EventArgs e)
        {
            autoLoginTimer.Elapsed -= AutoLoginTimer_Elapsed;

            if (ValidateInfo())
            {
                StaticFields.APIURL = txtAPI.Text;
                var report = await DataProvider.CheckLogin(txtUsername.Text, txtPassword.Text);

                if (report.Succeeded)
                {
                    StaticFields.Username = txtUsername.Text;
                    StaticFields.Password = txtPassword.Text;

                    this.DialogResult = DialogResult.OK;
                    SaveLoginData(AppDomain.CurrentDomain.BaseDirectory + "logindata.dat");
                    Log.Information("Logged Successfully");
                    this.Close();
                }
                else
                {
                    Log.Information("Login Failed");
                    MessageBox.Show("Login Failed");
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public bool ValidateInfo()
        {
            err.Clear();

            var result = true;

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                err.SetError(txtUsername, "Tên đăng nhập không được bỏ trống");
                result = false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                err.SetError(txtPassword, "Mật khẩu không được bỏ trống");
                result = false;
            }

            if (string.IsNullOrWhiteSpace(txtAPI.Text))
            {
                err.SetError(txtAPI, "Đường dẫn API không được bỏ trống");
                result = false;
            }
            return result;
        }

        private void SaveLoginData(string filepath)
        {
            var loginModel = new CredentialModel()
            {
                Id = txtUsername.Text,
                Password = txtPassword.Text,
                Api = txtAPI.Text,
                AutoLogin = chkAuto.Checked
            };

            var data = JsonConvert.SerializeObject(loginModel);
            var encryptedData = DataProvider.Encrypt(data);

            var bytedata = Encoding.UTF8.GetBytes(encryptedData);
            File.WriteAllBytes(filepath, bytedata);
        }

        private void LoadLoginData(string filepath)
        {
            var bytedata = File.ReadAllBytes(filepath);
            var encryptedData = Encoding.UTF8.GetString(bytedata);
            var data = DataProvider.Decrypt(encryptedData);

            var loginModel = JsonConvert.DeserializeObject<CredentialModel>(data);

            if (loginModel != null)
            {
                txtUsername.Text = loginModel.Id;
                txtPassword.Text = loginModel.Password;
                txtAPI.Text = loginModel.Api;
                chkAuto.Checked = loginModel.AutoLogin;
            }
        }

        private void chkAuto_CheckedChanged(object sender, EventArgs e)
        {
            if(!chkAuto.Checked)
            {
                autoLoginTimer.Elapsed -= AutoLoginTimer_Elapsed;
                chkAuto.Text = "Tự động đăng nhập";
            }

            StaticFields.isAutorun = chkAuto.Checked;
        }
    }
}
