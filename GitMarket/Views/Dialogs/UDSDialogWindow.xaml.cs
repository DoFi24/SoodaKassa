using GitMarket.Domain.Models.TitiModels.ProductsModel;
using GitMarket.Domain.Models.UDSModels;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GitMarket.Views.Dialogs
{
    public partial class UDSDialogWindow : Window
    {
        public delegate void UDSPrice(decimal totalPrice);
        public UDSPrice sendPrice;
        public LastCheckResponce lastCheck = new LastCheckResponce();
        private ResponseUserInfoModel? user;
        public UDSDialogWindow(decimal totalPrice)
        {
            InitializeComponent();
            TotalTextBlock.Text = totalPrice.ToString();
        }
        private void CloseWindowClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void DropDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private async void TextKeyUp(object sender, KeyEventArgs e)
        {
            if (UdsTextCode.Text.Length < 6)
                return;
            if (e.Key == Key.Return)
            {
                user = await Infrastructure.APIs.APIRequests.GetUDSUserInfoRequest(TotalTextBlock.Text, UdsTextCode.Text);
                if (user is null)
                {
                    MessageBox.Show("Неправильный код!", "Непрвильный ввод", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                UserName.Text = user.user.displayName;
                UserBonus.Text = user!.user!.participant!.points!.ToString() ?? "Нету баллов";
            }
        }
        private void Button_Click_Accept(object sender, RoutedEventArgs e)
        {
            decimal point = 0;
            if ((sender as Button)!.Uid == "0")
                point = Convert.ToDecimal(UdsSpisanieBonus.Text);

            var cashh = Infrastructure.APIs.APIRequests.GetCashRequest(
                                new RequestInfoModel
                                {
                                    code = user!.code!,
                                    participant = null,
                                    receipt = new Receipt
                                    {
                                        total = Convert.ToDecimal(TotalTextBlock.Text),
                                        skipLoyaltyTotal = String.IsNullOrEmpty(UdsSkipLoyaltyTotal.Text) ? 1 : Convert.ToDecimal(UdsSkipLoyaltyTotal.Text),
                                        points = point
                                    }
                                });

            var operInfo = Infrastructure.APIs.APIRequests.
                MakeUDSOperationRequest(
                    new RequestOperationModel
                    {
                        code = user!.code!,
                        participant = null,
                        nonce = Guid.NewGuid().ToString(),
                        cashier = null,
                        receipt = new Receipt
                        {
                            total = Convert.ToDecimal(TotalTextBlock.Text),
                            points = point,
                            skipLoyaltyTotal = String.IsNullOrEmpty(UdsSkipLoyaltyTotal.Text) ? 1 : Convert.ToDecimal(UdsSkipLoyaltyTotal.Text),
                            cash = cashh!.purchase!.cashTotal,
                            number = lastCheck.data.check_no
                        },
                        tags = null
                    });

            Setts.Default.Save();
            if (operInfo is null)
            {
                MessageBox.Show("Ошибка! \nНеверно введены данные");
                return;
            }
            sendPrice(operInfo!.Result!.oneOf.First().cash);
            MessageBox.Show("Успешно!");
            Close();
        }

        private async void Button_Click_SearchClient(object sender, RoutedEventArgs e)
        {
            user = await Infrastructure.APIs.APIRequests.GetUDSUserInfoRequest(TotalTextBlock.Text, UdsTextCode.Text);
            if (user is null)
            {
                MessageBox.Show("Неправильный код!", "Непрвильный ввод", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            UserName.Text = user.user.displayName;
            UserBonus.Text = user!.user!.participant!.points!.ToString() ?? "Нету баллов";
        }
    }
}
