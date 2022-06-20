global using Setts = GitMarket.Properties.Settings;
using GitMarket.Domain.Models.APIResponseRequest;
using GitMarket.Infrastructure.APIs;
using GitMarket.Views.MainWindows;
using GitMarket.Views.Pages;
using System.Collections.Generic;
using System.Windows;
using GitMarket.ViewModels.PagesViewModels;
using GitMarket.Views.Dialogs;

namespace GitMarket
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
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
