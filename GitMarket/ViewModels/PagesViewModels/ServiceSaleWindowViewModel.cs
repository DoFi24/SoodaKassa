using GitMarket.Domain.Models.APIResponseRequest;
using GitMarket.Domain.Models.TitiModels.ProductsModel;
using GitMarket.Infrastructure.APIs;
using GitMarket.ViewModels.WindowsViewModels;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;

namespace GitMarket.ViewModels.PagesViewModels
{
    public class ServiceSaleWindowViewModel : Base.BasePageViewModel
    {
        public Action? CloseAction { get; set; }

        private MainNavigationWindowViewModel _model;
        public ServiceSaleWindowViewModel(MainNavigationWindowViewModel model)
        {
            GetCategoriesAPI();
            _model = model;
            GetUnCategoryProduct();
        }
        private async void GetUnCategoryProduct()
        {
            ProductsCollection = new ObservableCollection<SaleProduct>(await APIRequests.GetFromAPIAsync<SaleProduct>(
                    new RequestModelGet
                    {
                        page = 1,
                        shopId = Setts.Default.ShopId,
                        staffId = Setts.Default.StaffId,
                        parameter = "get",
                        pageSize = 1,
                        data = new
                        {
                            categoryId = new ArrayList { "=", "0" }
                        }
                    }, "FC_PRODAJA_GET_SERVICE"));
        }

        private string? _searchTextBox;
        public string? SearchTextBox
        {
            get { return _searchTextBox; }
            set
            {
                Set(ref _searchTextBox, value);
                if (value != null && value != string.Empty && value.Length > 1)
                {
                    GetSearchedText(value);
                }
                else SearchItems.Clear();
            }
        }
        private async void GetSearchedText(string value)
        {
            var result = await APIRequests.GetFromAPIAsync<SaleProduct>(
                new RequestModelGet
                {
                    page = 1,
                    shopId = Setts.Default.ShopId,
                    staffId = Setts.Default.StaffId,
                    parameter = "get",
                    pageSize = 1,
                    data = new
                    {
                        barcode = new ArrayList { "=", value }
                    }
                }, "FC_PRODAJA_GET_SERVICE");
            SearchItems = new ObservableCollection<SaleProduct>(result.Where(s => s.Product_Name.Contains(value)).ToArray());
        }

        private ObservableCollection<APICategories>? _categoryItems;
        public ObservableCollection<APICategories> CategoryItems
        {
            get { return _categoryItems; }
            set { Set(ref _categoryItems, value); }
        }

        private ObservableCollection<SaleProduct>? _searchItems = new();
        public ObservableCollection<SaleProduct> SearchItems
        {
            get { return _searchItems; }
            set { Set(ref _searchItems, value); }
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
                    if (!_model.SelectedProductsCollection.Any(s => s.Product_Id == value.Product_Id))
                    {
                        value.QuantityCount = 1;
                        value.Service_id = value.Product_Id;
                        value.Is_service = true;
                        _model.SelectedProductsCollection.Add(value);
                    }
                    else
                    {
                        var product = _model.SelectedProductsCollection.First(s => s.Prihod_Detail_Id == value.Prihod_Detail_Id);
                        product.QuantityCount++;
                        _model.SelectedProductsCollection.Remove(product);
                        _model.SelectedProductsCollection.Add(product);
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
            ProductsCollection = new ObservableCollection<SaleProduct>(
                await APIRequests.GetFromAPIAsync<SaleProduct>(
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
                }, "FC_PRODAJA_GET_SERVICE"));
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
                data = null
            }, "FC_SERVICES_CATEGORIES"))
            {
                new APICategories { NAME = "Услуги без категории", ID = 0 }
            };
        }

    }
}
