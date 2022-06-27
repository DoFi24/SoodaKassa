using GitMarket.Domain.Models.APIModels;
using GitMarket.Domain.Models.TitiModels.ProductsModel;
using GitMarket.Infrastructure.APIs;
using GitMarket.ViewModels.PagesViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WpfAnimatedGif;

namespace GitMarket.Views.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для EndSaleDialogWindow.xaml
    /// </summary>
    public partial class EndSaleDialogWindow : Window
    {
        public readonly List<SaleProduct> products;

        private string inputDeviceText = "";
        public ViewModels.WindowsViewModels.MainNavigationWindowViewModel model;
        public EndSaleDialogWindow(List<SaleProduct> _products, ViewModels.WindowsViewModels.MainNavigationWindowViewModel md)
        {
            InitializeComponent();
            products = _products;
            model = md;
            SetGifAsync();
            GetDiscount();
        }

        private void GetDiscount()
        {
            var disc = new DiscountModel
            {
                page = 1,
                pageSize = 1,
                parameter = "",
                data = new List<taxeData>()
            };

            foreach (var item in products)
            {
                disc.data.Add(new taxeData { id = (int)item.Prihod_Detail_Id, quantity = item.Quantity });
            }

            model.discount = APIRequests.GetDiscountSum(disc).ToList();
            model.ReceiptDiscount = model.ReceiptDiscount = model.discount.Sum(s => s.Discount_Sum);
            DiscountLabel.Text = model.ReceiptDiscount.ToString();
        }

        private void SetGifAsync()
        {
            Parallel.Invoke(() =>
            {
                Thread.Sleep(5);
                this.Dispatcher.Invoke(() =>
                {
                    BitmapImage image = new();
                    image.BeginInit();
                    image.UriSource = new Uri("pack://application:,,,/Resources/GIFs/barcode-scan.gif");
                    image.EndInit();
                    ImageBehavior.SetAnimatedSource(Gif, image);
                });
            });
        }

        private async void CheckIsDevice()
        {
            await Task.Run(() => timerForDevice());
        }
        private void timerForDevice()
        {
            Thread.Sleep(200);

            if (inputDeviceText.Length > 4)
            {
                this.Dispatcher.Invoke(() =>
                {
                    GetBonuses();//дополнительная логика, нужно обработать перегрузку0
                });
            }
            Thread.Sleep(5);  // это для того чтобы асинхронные изменения успели вступить в силу, а то тазалаганда улгурбойтат, астыдагы переменныйды

            inputDeviceText = "";

            return;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EndCardPayment();
            Close();
        }

        private void EndCardPayment()
        {
            model.GetCalculate();
            Close();
        }
        private async void GetBonuses()
        {
            if (inputDeviceText != string.Empty)
            {
                if (await APIRequests.ReturnCardTypeAsync(new Domain.Models.APIResponseRequest.RequestModelGet 
                {
                    shopId = 1,
                    data = null,
                    staffId = 1,
                    pageSize  = 10,
                    page = 1,
                    parameter = inputDeviceText
                }) == "success")
                {
                    var bonus = new TaxesModel
                    {
                        parameter = inputDeviceText,
                        pageSize = 10,
                        page = 1,
                        data = new List<taxeData>()
                    };
                    foreach (var item in products)
                    {
                        bonus.data.Add(new taxeData { id = (long)item.Prihod_Detail_Id!, quantity = item.Quantity });
                    }

                    model.bonuses = APIRequests.GetBonusSum(bonus).ToList();

                    model.ReceiptBonus = model.bonuses.Sum(s => s.Bonus_Sum);
                    BonusLabel.Text = model.ReceiptBonus.ToString();
                }
            }
        }
        
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            model.GetCalculate();
            Close();
        }

        private void Win_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key.ToString().Length > 1 && e.Key.ToString()[0] == 'D' && e.Key.ToString() != "Decimal")
            {
                if (inputDeviceText == "")
                    CheckIsDevice();
                inputDeviceText += e.Key.ToString()[1];
            }
            else
                switch (e.Key)
                {
                    case Key.Return:
                        GetBonuses();
                        Close();
                        break;
                    case Key.Escape:
                        Close();
                        break;
                }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
