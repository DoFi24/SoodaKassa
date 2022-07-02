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
        MainNavigationWindowViewModel _view;
        public ProductsAndCategoriesPage(MainNavigationWindowViewModel view)
        {
            InitializeComponent();
            _view = view;
            ProductsAndCategoriesPageViewModel model = new ProductsAndCategoriesPageViewModel(view);
            if (model.CloseAction == null)
                model.CloseAction = new Action(() => { Close(); view.mainWindow.Effect = null;});
            DataContext = model;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _view.mainWindow.Effect = null;
            Close();
        }
    }
}
