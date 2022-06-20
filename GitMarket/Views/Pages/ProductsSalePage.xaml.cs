using GitMarket.ViewModels.PagesViewModels;
using System.Windows.Controls;
using System.Windows;

namespace GitMarket.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductsSalePage.xaml
    /// </summary>
    public partial class ProductsSalePage : Page
    {
        public ProductsSalePage(ProductsSalePageViewModel model)
        {
            InitializeComponent();
            DataContext = model;
        }

        private void Page_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //PaidTextBox.Focus();
        }

        private void PaidTextBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }
    }
}
