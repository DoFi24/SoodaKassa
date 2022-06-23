global using Setts = GitMarket.Properties.Settings;
using GitMarket.Domain.Models.APIResponseRequest;
using GitMarket.Infrastructure.APIs;
using GitMarket.Views.MainWindows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                if (Setts.Default.IsSaveLoginPassword)
                {
                    if (await APIRequests.GetIsValidAsync())
                    {
                        new MainNavigationWindow().Show();
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
                base.OnStartup(e);
            }
            else 
            {
                MessageBox.Show("Программа уже была открыта!","Окно",MessageBoxButton.OK,MessageBoxImage.Error);
            }
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
