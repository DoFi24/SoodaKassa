using GitMarket.Domain.Models.TitiModels.ProductsModel;
using GitMarket.ViewModels.PagesViewModels;
using GitMarket.Views.Dialogs;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace GitMarket.ViewModels.DialogsViewModel
{
    public class HotKeysDialogWindowViewModel : Base.BaseViewModel
    {
        public ProductsSalePageViewModel SalePage;

        public HotKeysDialogWindow ModelPage;
        public HotKeysDialogWindowViewModel(HotKeysDialogWindow modelPage, ViewModels.WindowsViewModels.MainNavigationWindowViewModel salePage, List<SaleProduct> products)
        {
            //SalePage = salePage;
            ModelPage = modelPage;
            for (int i = 0; i < 21; i++)
            {
                ProductsCollection.Add(new SaleProduct
                {
                    Product_Name = i.ToString(),
                    Sale_Price = i * 10,
                    Comment = @"/Resources/Base/logo2.png"
                });
            }
            //GetInitializeProducts(products);
        }
        public HotKeysDialogWindowViewModel()
        {

        }

        private ObservableCollection<SaleProduct>? _productsCollection = new();
        public ObservableCollection<SaleProduct>? ProductsCollection
        {
            get { return _productsCollection; }
            set { Set(ref _productsCollection, value); }
        }

        private SaleProduct? _selectedProduct = new();
        public SaleProduct? SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                Set(ref _selectedProduct, value);
            }
        }

        private int? _selectedProductIndex = 0;
        public int? SelectedProductIndex
        {
            get { return _selectedProductIndex; }
            set
            {
                Set(ref _selectedProductIndex, value);
            }
        }
        public void ChangeSelectedProductUPLR(string v)
        {
            if (v == "Left")
            {
                if (SelectedProductIndex == 0)
                    SelectedProductIndex = ProductsCollection.Count - 1;
                else
                    SelectedProductIndex--;
            }
            else if (v == "Right")
            {
                if (SelectedProductIndex == ProductsCollection.Count - 1)
                    SelectedProductIndex = 0;
                else
                    SelectedProductIndex++;
            }
            else if (v == "Down" && ProductsCollection.Count >= (10 + SelectedProductIndex))
            {
                SelectedProductIndex += 10;
            }
            else if (v == "Up" && SelectedProductIndex >= 10)
            {
                SelectedProductIndex -= 10;
            }
        }
        private async void GetInitializeProducts(List<SaleProduct> products)
        {
            foreach (var item in products)
            {
                await Task.Run(() => ProductsCollection.Add(item));
            }
        }
        public void ReturnProduct()
        {
            if (SelectedProduct != new SaleProduct())
            {
                SelectedProduct.QuantityCount = 1;
                SalePage.SelectedProductsCollection.Add(SelectedProduct);
                SalePage.SelectedProductItem = SalePage.SelectedProductsCollection.Last();
            };
            ModelPage.Close();
        }
    }
}
