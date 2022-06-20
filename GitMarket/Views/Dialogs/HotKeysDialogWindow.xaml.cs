using GitMarket.Domain.Models.TitiModels.ProductsModel;
using GitMarket.ViewModels.DialogsViewModel;
using GitMarket.ViewModels.PagesViewModels;
using System;
using System.Collections.Generic;
using System.Windows;

namespace GitMarket.Views.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для HotKeysDialogWindow.xaml
    /// </summary>
    public partial class HotKeysDialogWindow : Window
    {
        public HotKeysDialogWindowViewModel hotModel;
        public HotKeysDialogWindow(ViewModels.WindowsViewModels.MainNavigationWindowViewModel salePage, List<SaleProduct> products)
        {
            InitializeComponent();
            hotModel = new HotKeysDialogWindowViewModel(this, salePage, products);
            DataContext = hotModel;
        }
        private void Window_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape)
                Close();
            else if (e.Key == System.Windows.Input.Key.Return)
                hotModel.ReturnProduct();
            else if (e.Key == System.Windows.Input.Key.Up || e.Key == System.Windows.Input.Key.Down || e.Key == System.Windows.Input.Key.Left || e.Key == System.Windows.Input.Key.Right)
                hotModel.ChangeSelectedProductUPLR(e.Key.ToString());
        }

        private void CLoseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ProductListBox_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            hotModel.ReturnProduct();
        }
    }
}
