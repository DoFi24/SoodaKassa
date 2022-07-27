using GitMarket.ViewModels.PagesViewModels;
using GitMarket.ViewModels.WindowsViewModels;
using System;
using System.Windows;
using System.Windows.Input;

namespace GitMarket.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для ServiceSaleWindow.xaml
    /// </summary>
    public partial class ServiceSaleWindow : Window
    {
        private readonly ServiceSaleWindowViewModel model;
        public ServiceSaleWindow(MainNavigationWindowViewModel view)
        {
            InitializeComponent();
            model = new ServiceSaleWindowViewModel(view);
            if (model.CloseAction == null)
                model.CloseAction = new Action(() => Close());
            DataContext = model;
            SearchTextBox.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    Close();
                    break;
                case Key.Enter:
                    model.SearchProductCommandExecute(null);
                    break;
                case Key.Add:
                    model.SelectProductCommandExecute(null);
                    break;
                case Key.Decimal:
                    SearchTextBox.Focus();
                    break;
                case Key.Up:
                    model.UpDown(false);
                    break;
                case Key.Down:
                    model.UpDown(true);
                    break;
            }
        }
    }
}
