using GitMarket.ViewModels.PagesViewModels;
using System.Windows.Controls;

namespace GitMarket.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductReceptionPage.xaml
    /// </summary>
    public partial class ProductReceptionPage : Page
    {
        public ProductReceptionPage(ProductReceptionPageViewModel model)
        {
            InitializeComponent();
            DataContext = model;
        }
    }
}
