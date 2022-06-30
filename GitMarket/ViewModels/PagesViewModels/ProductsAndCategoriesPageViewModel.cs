using GitMarket.Domain.Models.APIResponseRequest;
using GitMarket.Domain.Models.TitiModels.ProductsModel;
using GitMarket.Infrastructure.APIs;
using GitMarket.ViewModels.WindowsViewModels;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace GitMarket.ViewModels.PagesViewModels
{
    public class ProductsAndCategoriesPageViewModel : Base.BasePageViewModel
    {
        public Action? CloseAction { get; set; }

        private MainNavigationWindowViewModel _model;
        public ProductsAndCategoriesPageViewModel(MainNavigationWindowViewModel model)
        {
            GetCategoriesAPI();
            _model = model;
        }
        private ObservableCollection<APICategories>? _categoryItems;
        public ObservableCollection<APICategories> CategoryItems
        {
            get { return _categoryItems; }
            set { Set(ref _categoryItems, value); }
        }

        private ObservableCollection<SaleProduct>? _productsCollection = new();
        public ObservableCollection<SaleProduct> ProductsCollection
        {
            get { return _productsCollection; }
            set { Set(ref _productsCollection, value); }
        }

        private APICategories? _selectedCategoryItem = new();
        public APICategories SelectedCategoryItem
        {
            get { return _selectedCategoryItem; }
            set
            {
                Set(ref _selectedCategoryItem, value);
                if (value != null && value != new APICategories())
                    GetProductsFromCategory(value);
            }
        }

        private SaleProduct? _selectedProduct = new();
        public SaleProduct SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                Set(ref _selectedProduct, value);
                if (value != null && value != new SaleProduct())
                {
                    value.QuantityCount = 1;
                    if (!_model.SelectedProductsCollection.Any(s => s.Prihod_Detail_Id == value.Prihod_Detail_Id))
                    {
                        value.QuantityCount = 1;
                        _model.SelectedProductsCollection.Add(value);
                    }
                    else
                    {
                        var product = _model.SelectedProductsCollection.First(s => s.Prihod_Detail_Id == value.Prihod_Detail_Id);
                        if (product.QuantityCount < product.Quantity)
                        {
                            product.QuantityCount++;
                            _model.SelectedProductsCollection.Remove(product);
                            _model.SelectedProductsCollection.Add(product);
                        }
                        MessageBox.Show("Не достаточно товаров!");
                    }
                    _model.SelectedProductItem = _model.SelectedProductsCollection.Last();
                    _model.GetCalculate();
                    CloseAction();
                    //_model.ChangePage(_model._productsSalePage);
                }
            }
        }
        private async void GetProductsFromCategory(APICategories category)
        {
            ProductsCollection = new ObservableCollection<SaleProduct>(await APIRequests.GetFromAPIAsync<SaleProduct>(
                new RequestModelGet
                {
                    page = 1,
                    shopId = Setts.Default.ShopId,
                    staffId = Setts.Default.StaffId,
                    parameter = "get",
                    pageSize = 10,
                    data = new
                    {
                        categoryId = new ArrayList { "=", category.ID }
                    }
                }));
        }
        private async void GetCategoriesAPI()
        {
            CategoryItems = new(await APIRequests.GetFromAPIAsync<APICategories>(new RequestModelGet
            {
                parameter = "get",
                page = 1,
                pageSize = 10,
                shopId = Setts.Default.ShopId,
                staffId = Setts.Default.StaffId,
                data = new
                {
                    parentId = "any"
                }
            }, "FC_PRODUCTS_CATEGORIES"))
            {
                new APICategories { NAME = "Товары без категории", ID = 0 }
            };

            SelectedCategoryItem = CategoryItems.Last();
        }
    }
}
