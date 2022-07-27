using GitMarket.Commands;
using GitMarket.Domain.Models.APIResponseRequest;
using GitMarket.Domain.Models.TitiModels.ProductsModel;
using GitMarket.Infrastructure.APIs;
using GitMarket.ViewModels.WindowsViewModels;
using GitMarket.Views.Dialogs;
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
            }
        }

        private string _searchText = String.Empty;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                Set(ref _searchText, value);
            }
        }

        private int _selectedProductIndex = 0;
        public int SelectedProductIndex
        {
            get { return _selectedProductIndex; }
            set
            {
                Set(ref _selectedProductIndex, value);
            }
        }

        private RelayCommand _searchProductCommand;
        public RelayCommand SearchProductCommand =>
            _searchProductCommand ?? (_searchProductCommand = new RelayCommand(SearchProductCommandExecute, (object obj) => true));
        public async void SearchProductCommandExecute(object obj)
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                new ErrorMessageDialogWindow("Текст для поиска, пустой!").ShowDialog();
                return;
            }
            ProductsCollection = new ObservableCollection<SaleProduct>(await APIRequests.GetFromAPIAsync<SaleProduct>(
                new RequestModelGet
                {
                    page = 1,
                    shopId = Setts.Default.ShopId,
                    staffId = Setts.Default.StaffId,
                    parameter = "get",
                    pageSize = 1000,
                    data = new
                    {
                        search = new ArrayList { "=", SearchText }
                    }
                },"FC_PRODAJA_GET_SERVICE"));

            if (ProductsCollection.Any())
                SelectedProductIndex = 0;
            SearchText = String.Empty;
        }

        private RelayCommand _selectProductCommand;
        public RelayCommand SelectProductCommand =>
            _selectProductCommand ??= new RelayCommand(SelectProductCommandExecute, (object obj) => true);
        public void SelectProductCommandExecute(object obj)
        {
            if (SelectedProduct is null || SelectedProduct == new SaleProduct())
                return;
            if (!_model.SelectedProductsCollection.Any(s => s.Prihod_Detail_Id == SelectedProduct.Prihod_Detail_Id))
            {
                SelectedProduct.QuantityCount = 1;
                _model.SelectedProductsCollection.Add(SelectedProduct);
            }
            else
            {
                var product = _model.SelectedProductsCollection.First(s => s.Prihod_Detail_Id == SelectedProduct.Prihod_Detail_Id);
                if (product.QuantityCount < product.Quantity)
                {
                    product.QuantityCount++;
                    _model.SelectedProductsCollection.Remove(product);
                    _model.SelectedProductsCollection.Add(product);
                }
                else
                    new ErrorMessageDialogWindow("Недостаточно товаров!").ShowDialog();
            }
            _model.SelectedProductItem = _model.SelectedProductsCollection.Last();
            _model.GetCalculate();
            CloseAction!.Invoke();
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
                    pageSize = 1000,
                    data = new
                    {
                        categoryId = new ArrayList { "=", category.ID }
                    }
                }, "FC_PRODAJA_GET_SERVICE")); 
            if (ProductsCollection.Any())
                SelectedProductIndex = 0;
        }
        private async void GetCategoriesAPI()
        {
            CategoryItems = new(await APIRequests.GetFromAPIAsync<APICategories>(new RequestModelGet
            {
                parameter = "get",
                page = 1,
                pageSize = 20,
                shopId = Setts.Default.ShopId,
                staffId = Setts.Default.StaffId,
                data = null
            }, "FC_SERVICES_CATEGORIES"))
            {
                new APICategories { NAME = "Услуги без категории", ID = 0 }
            }; 
            SelectedCategoryItem = CategoryItems.Last();
        }
        public void UpDown(bool isUp)
        {
            if (ProductsCollection.Count > 1)
            {
                if (isUp)
                {
                    if (SelectedProductIndex == ProductsCollection.Count - 1)
                        SelectedProductIndex = 0;
                    else
                        SelectedProductIndex++;
                }
                else
                {
                    if (SelectedProductIndex <= 0)
                        SelectedProductIndex = ProductsCollection.Count - 1;
                    else
                        SelectedProductIndex--;

                }
            }
        }
    }
}
