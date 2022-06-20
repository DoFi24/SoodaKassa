using GitMarket.ViewModels.PagesViewModels;
using GitMarket.ViewModels.WindowsViewModels;
using System;
using System.Windows;

namespace GitMarket.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для ServiceSaleWindow.xaml
    /// </summary>
    public partial class ServiceSaleWindow : Window
    {
        public ServiceSaleWindow(MainNavigationWindowViewModel view)
        {
            InitializeComponent();
            ServiceSaleWindowViewModel model = new ServiceSaleWindowViewModel(view);
            if (model.CloseAction == null)
                model.CloseAction = new Action(() => { Close(); });
            DataContext = model;
        }
    }
}
