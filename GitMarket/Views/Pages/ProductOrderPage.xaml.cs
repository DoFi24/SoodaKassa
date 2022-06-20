using GitMarket.ViewModels.PagesViewModels;
using System.Windows.Controls;

namespace GitMarket.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductOrderPage.xaml
    /// </summary>
    public partial class ProductOrderPage : Page
    {
        public ProductOrderPage(ProductOrderPageViewModel model)
        {
            InitializeComponent();
            DataContext = model;
        }
    }
}
