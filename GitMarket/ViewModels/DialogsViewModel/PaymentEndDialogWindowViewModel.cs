using GitMarket.Domain.Models.TitiModels.ProductsModel;
using GitMarket.ViewModels.PagesViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitMarket.ViewModels.DialogsViewModel
{
    public class PaymentEndDialogWindowViewModel : Base.BaseViewModel
    {
        private ViewModels.WindowsViewModels.MainNavigationWindowViewModel model;
        public PaymentEndDialogWindowViewModel(ViewModels.WindowsViewModels.MainNavigationWindowViewModel _model)
        {
            model = _model;
            ProductList = new ObservableCollection<SaleProduct>(model.SelectedProductsCollection);
            SetList(model.ReceiptPrice, model.SelectedProductsCollection.Sum(s => s.Itog),  model.ReceiptPaid, model.ReceiptPaidCard);
        }
        public PaymentEndDialogWindowViewModel()
        {

        }

        #region Propertuies

        private ObservableCollection<SaleProduct> productList;
        public ObservableCollection<SaleProduct> ProductList
        {
            get => productList;
            set => Set(ref productList, value);
        }
        private string _ShopName = "";
        public string ShopName
        {
            get => _ShopName;
            set => Set(ref _ShopName, value);
        }
        private string _RecipeTotalSum = "";
        public string RecipeTotalSum
        {
            get => _RecipeTotalSum;
            set => Set(ref _RecipeTotalSum, value);
        }
        private string _RecipePayMoney = "";
        public string RecipePayMoney
        {
            get => _RecipePayMoney;
            set => Set(ref _RecipePayMoney, value);
        }
        private string _RecipePayMoneyWithCard = "";
        public string RecipePayMoneyWithCard
        {
            get => _RecipePayMoneyWithCard;
            set => Set(ref _RecipePayMoneyWithCard, value);
        }

        private string _RecipeSdacha = "";
        public string RecipeSdacha
        {
            get => _RecipeSdacha;
            set => Set(ref _RecipeSdacha, value);
        }
        private string _CheckText = "";
        public string CheckText
        {
            get => _CheckText;
            set => Set(ref _CheckText, value);
        }
        private string _CheckNomer = "";
        public string CheckNomer
        {
            get => _CheckNomer;
            set => Set(ref _CheckNomer, value);
        }
        private string _RecipeDiscount = "";
        public string RecipeDiscount
        {
            get => _RecipeDiscount;
            set => Set(ref _RecipeDiscount, value);
        }
        private string _RecipeNotDiscount = "";
        public string RecipeNotDiscount
        {
            get => _RecipeNotDiscount;
            set => Set(ref _RecipeNotDiscount, value);
        }
        #endregion
        private void SetList(decimal Itog, decimal ItogWithNotDiscount, decimal Nalichnie, decimal Kartoi) //≡
        {
            RecipeNotDiscount = "≡" + ItogWithNotDiscount.ToString("#.##");
            RecipeTotalSum = "≡" + Itog.ToString("#.##");
            RecipeSdacha = "≡" + ((Nalichnie+ Kartoi) - Itog).ToString("#.##");
            RecipePayMoney = "≡" + Nalichnie.ToString("#.##");
            RecipePayMoneyWithCard = "≡" + Kartoi.ToString("#.##");
            RecipeDiscount = "≡" + (ItogWithNotDiscount - Itog).ToString("#.##");
        }
    }
}
