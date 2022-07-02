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
        MainNavigationWindowViewModel view;
        public ServiceSaleWindow(MainNavigationWindowViewModel view)
        {
            InitializeComponent();
            this.view = view;
            ServiceSaleWindowViewModel model = new ServiceSaleWindowViewModel(view);
            if (model.CloseAction == null)
                model.CloseAction = new Action(() => { Close(); view.mainWindow.Effect = null; });
            DataContext = model;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            view.mainWindow.Effect = null;
            Close();
        }
    }
}
