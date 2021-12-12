using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOP.ComService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Async(a => a.File("Logs\\log.txt", rollingInterval: RollingInterval.Day, shared:true))
                .CreateLogger();

            Log.Information("Application Started");

            string _AppName = "SOP.ComService";

            using (Mutex mutex = new Mutex(true, _AppName, out bool isMutexOwner))
            {
                //if (mutex.WaitOne(new TimeSpan(0, 0, 0), true))
                if (isMutexOwner)
                {
                Login:
                    var loginForm = new frmLogin();

                    if (loginForm.ShowDialog() == DialogResult.OK)
                    {
                        Application.Run(new frmMain());
                    }

                    if (StaticFields.isLogOut)
                        goto Login;

                    mutex.ReleaseMutex();
                }
                else
                {
                    MessageBox.Show($"{_AppName} đang mở hoặc chạy ngầm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            Log.CloseAndFlush();
        }
    }
}
