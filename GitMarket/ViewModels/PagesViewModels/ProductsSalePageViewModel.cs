using GitMarket.Commands;
using GitMarket.Domain.Models.APIModels;
using GitMarket.Domain.Models.TitiModels.ProductsModel;
using GitMarket.Infrastructure.APIs;
using GitMarket.Views.Dialogs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace GitMarket.ViewModels.PagesViewModels
{
    public class ProductsSalePageViewModel : Base.BasePageViewModel
    {
        public ProductsSalePageViewModel()
        {

        }

        #region Prperties

        public List<DiscountProduct> discount = new();
        public List<BonusProducts> bonuses = new();
        public List<ProductTaxes> taxes = new();

        public decimal taxSum = 0;

        private decimal _ReceiptPrice = 0m;
        public decimal ReceiptPrice
        {
            get => _ReceiptPrice;
            set => Set(ref _ReceiptPrice, value);
        }
        private decimal _ReceiptDiscount = 0m;
        public decimal ReceiptDiscount
        {
            get => _ReceiptDiscount;
            set => Set(ref _ReceiptDiscount, value);
        }
        private decimal _ReceiptBonus = 0m;
        public decimal ReceiptBonus
        {
            get => _ReceiptBonus;
            set => Set(ref _ReceiptBonus, value);
        }
        private decimal _ReceiptPaid = 0m;
        public decimal ReceiptPaid
        {
            get => _ReceiptPaid;
            set
            {
                Set(ref _ReceiptPaid, value); GetCalculateOverPrice();
            }
        }

        private decimal _ReceiptPaidCard = 0m;
        public decimal ReceiptPaidCard
        {
            get => _ReceiptPaidCard;
            set
            {
                Set(ref _ReceiptPaidCard, value); GetCalculateOverPrice();
            }
        }
        private ObservableCollection<SaleProduct>? _selectedProductsCollection = new();
        public ObservableCollection<SaleProduct>? SelectedProductsCollection
        {
            get { return _selectedProductsCollection; }
            set { Set(ref _selectedProductsCollection, value); }
        }

        private decimal _ReceiptOverpay = 0m;
        public decimal ReceiptOverpay
        {
            get => _ReceiptOverpay;
            set => Set(ref _ReceiptOverpay, value);
        }

        private ProdajaModel _prodaja = new ProdajaModel();
        public ProdajaModel Prodaja
        {
            get => _prodaja;
            set => Set(ref _prodaja, value);
        }

        private SaleProduct _selectedProductItem = new();
        public void DeleteSelectedProduct()
        {
            SelectedProductsCollection.Remove(SelectedProductItem);
        }

        public SaleProduct SelectedProductItem
        {
            get { return _selectedProductItem; }
            set { Set(ref _selectedProductItem, value); }
        }

        #endregion


        private RelayCommand _changeProductQuantityCommand;
        public RelayCommand ChangeProductQuantityCommand =>
            _changeProductQuantityCommand ?? (_changeProductQuantityCommand = new RelayCommand(ExecutedChangeProductQuantityCommand, (object obj) => true));
        private void ExecutedChangeProductQuantityCommand(object obj)
        {
            if (obj == "unpack")
            {
                if (SelectedProductItem.Unpack > 1)
                {
                    AddCountDialogWindow addCount = new(SelectedProductItem.Product_Name, SelectedProductItem.Quantity, "unpack");
                    addCount.ReturnProductCountEvent += (x) =>
                    {
                        SelectedProductItem.QuantityCount = ((decimal)(Convert.ToDecimal(x.ToString()) / SelectedProductItem.Unpack));
                        SelectedProductItem.IsUnpack = true;
                        SelectedProductsCollection = new ObservableCollection<SaleProduct>(SelectedProductsCollection);

                    };
                    addCount.ShowDialog();
                    GetCalculate();
                }
            }
            else
            {
                AddCountDialogWindow addCount = new(SelectedProductItem.Product_Name ,SelectedProductItem.Quantity);
                addCount.ReturnProductCountEvent += (x) =>
                {
                    SelectedProductItem.QuantityCount = Convert.ToDecimal(x.ToString());
                    SelectedProductsCollection = new ObservableCollection<SaleProduct>(SelectedProductsCollection);
                };
                addCount.ShowDialog();
                GetCalculate();
            }
        }
        private RelayCommand _toPayCommand;
        public RelayCommand ToPayCommand =>
            _toPayCommand ??= new RelayCommand(ExecutedToPayCommand, (object obj) => { if (SelectedProductsCollection.Any()) return true; return false; });
        private async void ExecutedToPayCommand(object obj)
        {
            var alltaxes = new TaxesModel
            {
                parameter = "",
                pageSize = 10,
                page = 1,
                data = new List<taxeData>()
            };

            foreach (var item in SelectedProductsCollection)
            {
                alltaxes.data.Add(new taxeData { id = (int)item.Prihod_Detail_Id, quantity = item.QuantityCount });
            }

            var x = APIRequests.GetTaxesSum(alltaxes);
            taxes = x.Item1.ToList();
            taxSum = x.Item2;

            Prodaja = await PayMethodAsync();

            Thread.Sleep(100);

            //new PaymentEndDialogWindow(this).Show();
        }

        private RelayCommand _getBonusCommand;
        public RelayCommand GetBonusCommand =>
            _getBonusCommand ??= new RelayCommand(ExecutedGetBonusCommand, (object obj) => { if (SelectedProductsCollection.Any()) return true; return false; });
        private void ExecutedGetBonusCommand(object obj)
        {
            //_ = new EndSaleDialogWindow(SelectedProductsCollection.ToList(), this).ShowDialog();
        }

        private RelayCommand _clearSelectedProductsCommand;
        public RelayCommand ClearSelectedProductsCommand =>
            _clearSelectedProductsCommand ??= new RelayCommand(ExecutedClearSelectedProductsCommand, (object obj) => { if (SelectedProductsCollection.Any()) return true; return false; });
        public void ExecutedClearSelectedProductsCommand(object obj)
        {
            SelectedProductsCollection?.Clear();
            discount.Clear();
            bonuses.Clear();
            taxes.Clear();
            GetCalculate();
        }

        private RelayCommand _checkPaymentEndCommand;
        public RelayCommand CheckPaymentEndCommand =>
            _checkPaymentEndCommand ??= new RelayCommand(ExecutedCheckPaymentEndCommand, (object obj) => { if (SelectedProductsCollection.Any()) return true; return false; });
        public void ExecutedCheckPaymentEndCommand(object obj)
        {
            //new PaymentEndDialogWindow(this).ShowDialog();
        }
        public void GetCalculateOverPrice()
        {
            if (ReceiptPaid + ReceiptPaidCard >= ReceiptPrice)
                ReceiptOverpay = (ReceiptPaid + ReceiptPaidCard) - ReceiptPrice;
            else
                ReceiptOverpay = 0;
        }
        public void GetCalculate()
        {
            if (SelectedProductsCollection.Count > 0)
            {
                ReceiptPrice = SelectedProductsCollection.Select(t => t.Itog).Sum() - ReceiptDiscount - ReceiptBonus;
                GetCalculateOverPrice();
            }
            else
            {
                ReceiptPrice = 0;
                ReceiptDiscount = 0;
                ReceiptBonus = 0;
                ReceiptOverpay = 0;
                ReceiptPaid = 0;
                ReceiptPaidCard = 0;
            }

        }
        private async Task<ProdajaModel> PayMethodAsync() //при uds оплате изменить структуру
        {
            try
            {
                if (ReceiptPaid + ReceiptPaidCard >= ReceiptPrice)
                {
                    ProdajaModels lasteses;
                    if (bonuses.Any() && discount.Any())
                        lasteses = new ProdajaModels
                        {
                            parameter = "windows",
                            data = new data
                            {
                                shop_id = Setts.Default.ShopId,
                                sklad_id = Setts.Default.StaffId,
                                staff_id = Setts.Default.SkladId,
                                kontragent_id = null,
                                pay_sum = ReceiptPrice,
                                comment = "",
                                rows = (from s in SelectedProductsCollection
                                        join d in discount on s.Prihod_Detail_Id equals d.Prihod_Detail_Id
                                        join b in bonuses on s.Prihod_Detail_Id equals b.Prihod_Detail_Id
                                        join t in taxes on s.Prihod_Detail_Id equals t.prihod_detail_id
                                        select new ProdajaProduct
                                        {
                                            prihod_detail_id = d.Prihod_Detail_Id,
                                            sale_price = (decimal)((bool)!s.IsUnpack ? s.Sale_Price : s.Unpack_Sale_Price),
                                            bonus_sum = b.Bonus_Sum,
                                            discount_id = d.Discount_Id,
                                            discount_sum = d.Discount_Sum,
                                            bonus_id = b.Bonus_Id,
                                            pay_bonus_sum = b.Pay_Bonus_Sum,
                                            taxe_sum = t.taxe_sum,
                                            comment = s.Comment,
                                            quantity = s.QuantityCount
                                        }).ToList()
                            },
                            sum = ReceiptPaid,
                            esum = ReceiptPaidCard,
                            pageSize = 2,
                            page = 1
                        };
                    else if (!bonuses.Any() && !discount.Any())
                        lasteses = new ProdajaModels
                        {
                            parameter = "windows",
                            data = new data
                            {
                                shop_id = Setts.Default.ShopId,
                                sklad_id = Setts.Default.StaffId,
                                staff_id = Setts.Default.SkladId,
                                comment = "",
                                pay_sum = ReceiptPrice,
                                kontragent_id = 0,
                                rows = (from s in SelectedProductsCollection
                                        join t in taxes on s.Prihod_Detail_Id equals t.prihod_detail_id
                                        select new ProdajaProduct
                                        {
                                            prihod_detail_id = (int)s.Prihod_Detail_Id,
                                            bonus_id = 0,
                                            discount_id = 0,
                                            sale_price = (decimal)((bool)!s.IsUnpack ? s.Sale_Price : s.Unpack_Sale_Price),
                                            quantity = s.QuantityCount,
                                            discount_sum = 0,
                                            bonus_sum = 0,
                                            pay_bonus_sum = 0,
                                            taxe_sum = t.taxe_sum,
                                            comment = s.Comment
                                        }).ToList()
                            },
                            sum = ReceiptPaid,
                            esum = ReceiptPaidCard,
                            pageSize = 10,
                            page = 1
                        };
                    else if (!bonuses.Any() && discount.Any())
                        lasteses = new ProdajaModels
                        {
                            parameter = "windows",
                            data = new data
                            {
                                shop_id = Setts.Default.ShopId,
                                sklad_id = Setts.Default.StaffId,
                                staff_id = Setts.Default.SkladId,
                                comment = "",
                                pay_sum = ReceiptPrice,
                                kontragent_id = 0,
                                rows = (from s in SelectedProductsCollection
                                        join d in discount on s.Prihod_Detail_Id equals d.Prihod_Detail_Id
                                        join t in taxes on s.Prihod_Detail_Id equals t.prihod_detail_id
                                        select new ProdajaProduct
                                        {
                                            prihod_detail_id = (int)s.Prihod_Detail_Id,
                                            bonus_id = 0,
                                            discount_id = d.Discount_Id,
                                            sale_price = (decimal)((bool)!s.IsUnpack ? s.Sale_Price : s.Unpack_Sale_Price),
                                            quantity = s.QuantityCount,
                                            discount_sum = d.Discount_Sum,
                                            bonus_sum = 0,
                                            pay_bonus_sum = 0,
                                            taxe_sum = t.taxe_sum,
                                            comment = s.Comment
                                        }).ToList()
                            },
                            sum = ReceiptPaid,
                            esum = ReceiptPaidCard,
                            pageSize = 10,
                            page = 1
                        };
                    else
                        lasteses = new ProdajaModels
                        {
                            parameter = "windows",
                            data = new data
                            {
                                shop_id = Setts.Default.ShopId,
                                sklad_id = Setts.Default.StaffId,
                                staff_id = Setts.Default.SkladId,
                                comment = "",
                                pay_sum = ReceiptPrice,
                                kontragent_id = 0,
                                rows = (from s in SelectedProductsCollection
                                        join d in bonuses on s.Prihod_Detail_Id equals d.Prihod_Detail_Id
                                        join t in taxes on s.Prihod_Detail_Id equals t.prihod_detail_id
                                        select new ProdajaProduct
                                        {
                                            prihod_detail_id = (int)s.Prihod_Detail_Id,
                                            bonus_id = d.Bonus_Id,
                                            discount_id = 0,
                                            sale_price = (decimal)((bool)!s.IsUnpack ? s.Sale_Price : s.Unpack_Sale_Price),
                                            quantity = s.QuantityCount,
                                            discount_sum = 0,
                                            bonus_sum = d.Bonus_Sum,
                                            pay_bonus_sum = 0,
                                            taxe_sum = t.taxe_sum,
                                            comment = s.Comment
                                        }).ToList()
                            },
                            sum = ReceiptPaid,
                            esum = ReceiptPaidCard,
                            pageSize = 10,
                            page = 1
                        };
                    return await APIRequests.GetSale(lasteses);
                }
                return new ProdajaModel();
            }
            catch (Exception e)
            {
                MessageBox.Show("Не правильный формат данных " + e.Message);
                return new ProdajaModel();
            }
        }
        public void UpDownCommand(string v)
        {
            if (SelectedProductsCollection.Count > 0)
            {
                if (SelectedProductItem == new SaleProduct())
                {
                    SelectedProductItem = SelectedProductsCollection.First();
                }
                else
                {
                    int index = SelectedProductsCollection.IndexOf(SelectedProductItem);
                    if (v == "Down")
                    {
                        if (index == SelectedProductsCollection.Count - 1)
                            SelectedProductItem = SelectedProductsCollection.First();
                        else
                            SelectedProductItem = SelectedProductsCollection[index + 1];
                    }
                    else
                    {
                        if (index <= 0)
                            SelectedProductItem = SelectedProductsCollection.Last();
                        else
                            SelectedProductItem = SelectedProductsCollection[index - 1];
                    }
                }
                GetCalculate();
            }
        }
        public async void AddWithHotKey(HotKeyModel hotKeyModel)
        {
            var prod = await APIRequests.GetFromAPIAsync<SaleProduct>(new Domain.Models.APIResponseRequest.RequestModelGet
            {
                page = 1,
                pageSize = 10,
                shopId = 1,
                staffId = 1,
                parameter = "get",
                data = new
                {
                    productId = new ArrayList { "=", hotKeyModel.Product_Id }
                }
            });
            if (prod.Any())
                if (hotKeyModel.Type)
                {
                    var firstproduct = prod.First();

                    if (!SelectedProductsCollection.Any(s => s.Prihod_Detail_Id == firstproduct.Prihod_Detail_Id))
                    {
                        firstproduct.QuantityCount = 1;
                        SelectedProductsCollection.Add(firstproduct);
                    }
                    else
                    {
                        var product = SelectedProductsCollection.First(s => s.Prihod_Detail_Id == firstproduct.Prihod_Detail_Id);
                        product.QuantityCount++;
                        SelectedProductsCollection.Remove(product);
                        SelectedProductsCollection.Add(product);
                    }
                }
                else
                {
                    //new HotKeysDialogWindow(this, prod).ShowDialog();
                }
            GetCalculate();
        }
    }
}
