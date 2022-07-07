using GitMarket.ViewModels.PagesViewModels;
using GitMarket.ViewModels.WindowsViewModels;
using System;
using System.Windows;

namespace GitMarket.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductsAndCategoriesPage.xaml
    /// </summary>
    public partial class ProductsAndCategoriesPage : Window
    {
        public ProductsAndCategoriesPage(MainNavigationWindowViewModel view)
        {
            InitializeComponent();
            ProductsAndCategoriesPageViewModel model = new ProductsAndCategoriesPageViewModel(view);
            if (model.CloseAction == null)
                model.CloseAction = new Action(() => Close());
            DataContext = model;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
