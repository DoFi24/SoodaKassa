using GitMarket.ViewModels.PagesViewModels;
using System.Windows.Controls;

namespace GitMarket.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductTransferPage.xaml
    /// </summary>
    public partial class ProductTransferPage : Page
    {
        public ProductTransferPage(ProductTransferPageViewModel model)
        {
            InitializeComponent();
            DataContext = model;
        }
    }
}
