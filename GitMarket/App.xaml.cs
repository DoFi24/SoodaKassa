global using Setts = GitMarket.Properties.Settings;
using GitMarket.Domain.Models.APIResponseRequest;
using GitMarket.Infrastructure.APIs;
using GitMarket.Views.MainWindows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;

namespace GitMarket
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 
    public partial class App : Application
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        protected override async void OnStartup(StartupEventArgs e)
        {

            if (IsOnce())
            {
                try
                {
                    using (var sr = new StreamReader(Directory.GetCurrentDirectory() + "/ipAdres.txt"))
                    {
                        string[] configurations = sr.ReadToEnd().Split();
                        APIRequests.API_PATH = configurations[0];
                        Setts.Default.ShopId = (uint)Convert.ToInt32(configurations[1]);
                        Setts.Default.SkladId = (uint)Convert.ToInt32(configurations[2]);
                        Setts.Default.KassaId = (uint)Convert.ToInt32(configurations[3]);
                        Setts.Default.Save();

                       sr.Close();
                    }
                }
                catch { }

                if (Setts.Default.IsSaveLoginPassword)
                {
                    if (await APIRequests.GetIsValidAsync())
                    {
                        new MainNavigationWindow(true).Show();
                    }   
                    else
                    {
                        new LoginWindow().Show();
                        MessageBox.Show("Не валидный токен!");
                    }
                }
                else
                {
                    new LoginWindow().Show();
                }
            }
            else
            {
                MessageBox.Show("Программа уже была открыта!", "Окно", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            base.OnStartup(e);
        }
        private static bool IsOnce()
        {
            int countMyApp = 0;
            foreach (Process process in Process.GetProcesses())
            {
                if (System.AppDomain.CurrentDomain.FriendlyName.IndexOf(process.ProcessName) == 0)
                {
                    countMyApp++;
                }
            }
            if (countMyApp > 1)
            {
                return false;
            }
            return true;
        }
        protected async override void OnExit(ExitEventArgs e)
        {
            await APIRequests.SaveChangesAsync(new JournalApiModel
            {
                parameter = "window",
                data = new JournalApiModelData
                {
                    shop_id = Setts.Default.ShopId,
                    staff_id = Setts.Default.StaffId,
                    rows = new List<JournalValue>
                    {
                        new JournalValue {value = $"Закрытие программы"}
                    }
                }
            });

            base.OnExit(e);
        }
    }
}
