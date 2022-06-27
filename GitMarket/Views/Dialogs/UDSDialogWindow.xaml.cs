using GitMarket.Domain.Models.UDSModels;
using System;
using System.Windows;
using System.Windows.Input;

namespace GitMarket.Views.Dialogs
{
    public partial class UDSDialogWindow : Window
    {
        private ResponseUserInfoModel? user;
        public UDSDialogWindow()
        {
            InitializeComponent();
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
        private async void Button_Click_Accept(object sender, RoutedEventArgs e)
        {
            var cashh = Infrastructure.APIs.APIRequests.GetCashRequest(
                                new RequestInfoModel
                                {
                                    code = user!.code!,
                                    participant = new Participant { uid = user!.user!.uid!, phone = user!.user!.phone! },
                                    receipt = new Receipt
                                    {
                                        total = Convert.ToDecimal(TotalTextBlock.Text),
                                        skipLoyaltyTotal = 0,
                                        points = Convert.ToDecimal(UserBonus.Text)
                                    }
                                });


            var operInfo = await Infrastructure.APIs.APIRequests.
                MakeUDSOperationRequest(
                    new RequestOperationModel
                    {
                        code = user!.code!,
                        participant = new Participant
                        {
                            uid = user.user.uid!,
                            phone = user.user.phone!
                        },
                        nonce = Setts.Default.NonceUuid++.ToString(),
                        cashier = null,
                        receipt = new Receipt
                        {
                            total = Convert.ToDecimal(TotalTextBlock.Text),
                            points = Convert.ToDecimal(UdsSpisanieBonus.Text),
                            skipLoyaltyTotal = null,
                            cash = cashh!.purchase!.cashTotal,
                            number = null
                        },
                        tags = null
                    });

            Setts.Default.Save();

            var result = await Infrastructure.APIs.APIRequests.GetBonusRequest(
                new RequestBonusModel
                {
                    points = (decimal)cashh!.purchase!.cashBack!,
                    comment = null,
                    silent = (bool)silentCheckBox.IsChecked!,
                    participants = { user!.user!.uid!.ToString() }
                });

            if (int.Parse(result!.accepted) < 1)
            {
                MessageBox.Show("Бонусы начислены не будут!");
                return;
            }
        }
    }
}
