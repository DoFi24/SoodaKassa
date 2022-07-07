using GitMarket.Domain.Models.TitiModels.ProductsModel;
using GitMarket.ViewModels.PagesViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace GitMarket.Views.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для AddProductFromCollectionDialogWindow.xaml
    /// </summary>
    public partial class AddProductFromCollectionDialogWindow : Window
    {
        private ViewModels.WindowsViewModels.MainNavigationWindowViewModel model;
        public AddProductFromCollectionDialogWindow(List<SaleProduct> products, ViewModels.WindowsViewModels.MainNavigationWindowViewModel _model)
        {
            InitializeComponent();
            model = _model;
            mainGrid.ItemsSource = products;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ReturnProduct();
        }
        private void ReturnProduct()
        {
            if (mainGrid.SelectedItem != null && mainGrid.SelectedItem.GetType() == typeof(SaleProduct))
            {
                var prod = mainGrid.SelectedItem as SaleProduct;
                if (!model.SelectedProductsCollection!.Any(s => s.Prihod_Detail_Id == prod!.Prihod_Detail_Id))
                {
                    prod!.QuantityCount = 1;
                    model!.SelectedProductsCollection!.Add(prod);
                }
                else
                {
                    var product = model.SelectedProductsCollection!.First(s => s.Prihod_Detail_Id == prod!.Prihod_Detail_Id);
                    if (product.QuantityCount < product.Quantity)
                    {
                        product.QuantityCount++;
                        model.SelectedProductsCollection!.Remove(product);
                        model.SelectedProductsCollection!.Add(product);
                    }
                    else
                        MessageBox.Show("Не достаточно товаров!");
                }
                model.GetCalculate();
                Close();
            }

        }
        private void Close_Button(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case System.Windows.Input.Key.Up:
                    DownUpProduct("up");
                    break;

                case System.Windows.Input.Key.Down:
                    DownUpProduct("down");
                    break;

                case System.Windows.Input.Key.Return:
                    ReturnProduct();
                    break;

                case System.Windows.Input.Key.Escape:
                    Close();
                    break;
            }
        }
        private void DownUpProduct(string ev)
        {
            var index = mainGrid.SelectedIndex;
            List<SaleProduct>? products = mainGrid.ItemsSource as List<SaleProduct>;
            if (ev == "down")
            {
                if(index == products?.Count-1)
                    mainGrid.SelectedIndex = 0;    
                else
                    mainGrid.SelectedIndex = ++index;
            }
            else 
            {
                if (index == 0)
                    mainGrid.SelectedIndex = products.Count - 1;
                else
                    mainGrid.SelectedIndex = --index;
            }
        }
    }
}
