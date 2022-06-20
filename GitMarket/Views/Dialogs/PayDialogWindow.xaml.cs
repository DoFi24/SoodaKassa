using GitMarket.ViewModels.PagesViewModels;
using System.Windows;

namespace GitMarket.Views.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для PayDialogWindow.xaml
    /// </summary>
    public partial class PayDialogWindow : Window
    {
        //private ProductsSalePageViewModel model;

        //public ProdajaModel prodaja = new();

        //private decimal ToPay;
        //private decimal ToPayWithUds;
        //private decimal ToPayDiscount;
        //private decimal ToPayBonus;

        public PayDialogWindow(ProductsSalePageViewModel md)
        {
            InitializeComponent();
            //model = md;
            //SetGifAsync();
            //GetPays();
        }

        //private void GetPays()
        //{
        //    //ToPayDiscount = model.ReceiptDiscount;
        //    //ToPayBonus = model.ReceiptDiscount;
        //    //ToPay = model.SelectedProductsCollection.Sum(s=>s.Quantity * s.Sale_Price) - model.ReceiptDiscount - model.ReceiptDiscount;
        //}

        //#region Для дизайна

        //#endregion
        //private void SetGifAsync()
        //{
        //    Parallel.Invoke(() =>
        //    {
        //        Thread.Sleep(5);
        //        this.Dispatcher.Invoke(() =>
        //        {
        //            SetGif();
        //        });
        //    });
        //}
        //private void SetGif()
        //{   
        //    BitmapImage image = new();
        //    image.BeginInit();
        //    image.UriSource = new Uri("pack://application:,,,/Resources/GIFs/barcode-scan.gif");
        //    image.EndInit();
        //    ImageBehavior.SetAnimatedSource(Gif, image);
        //}

        //#region Логика
        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    Close();
        //}
        //private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    DragMove();
        //}
        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        //    PayMethod();
        //}

        //#endregion
        //private void PayMethod() //при uds оплате изменить структуру
        //{
        //    try
        //    {
        //        decimal Money = Convert.ToDecimal(moneyLabel.Text);
        //        decimal CardMoney = Convert.ToDecimal(cardMoneyLabel.Text);
        //        if (Money + CardMoney >= ToPay)
        //        {
        //            var lasteses = (from s in model.SelectedProductsCollection
        //                            join d in model.discount on s.Prihod_Detail_Id equals d.Prihod_Detail_Id
        //                            join b in model.bonuses on s.Prihod_Detail_Id equals b.Prihod_Detail_Id
        //                            join t in model.taxes on s.Prihod_Detail_Id equals t.Prihod_Detail_Id
        //                            select new ProdajaProduct
        //                            {
        //                                Prihod_Detail_Id = d.Prihod_Detail_Id,
        //                                Sale_Price = s.Sale_Price,
        //                                Bonus_Sum = b.Bonus_Sum,
        //                                Discount_Id = d.Discount_Id,
        //                                Discount_Sum = d.Discount_Sum,
        //                                Bonus_Id = b.Bonus_Id,
        //                                Pay_Bonus_Sum = b.Pay_Bonus_Sum,
        //                                Taxe_Sum = t.Taxe_Sum,
        //                                Comment = s.Comment,
        //                                Quantity = s.Quantity
        //                            }).ToList();
        //            prodaja = APIRequests.GetSale(lasteses, ToPay);
        //            Close();
        //        }
        //    }
        //    catch(Exception e)
        //    {
        //        MessageBox.Show("Не правильный формат данных " + e.Message);
        //        Close();
        //    }
        //}
        //private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        //{

        //}
        //private void Window_KeyUp(object sender, KeyEventArgs e)
        //{
        //    switch (e.Key)
        //    {
        //        case Key.Escape:
        //            Close();
        //            break;
        //        case Key.Return:
        //            PayMethod();
        //            break;
        //    }
        //}
    }
}
