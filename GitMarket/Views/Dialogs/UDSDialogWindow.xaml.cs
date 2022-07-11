using GitMarket.Domain.Models.TitiModels.ProductsModel;
using GitMarket.Domain.Models.UDSModels;
using GitMarket.ViewModels.WindowsViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GitMarket.Views.Dialogs
{
    public partial class UDSDialogWindow : Window
    {
        public LastCheckResponce lastCheck = new LastCheckResponce();
        private ResponseUserInfoModel? user;
        MainNavigationWindowViewModel _vmodel;
        public UDSDialogWindow(MainNavigationWindowViewModel vmodel)
        {
            InitializeComponent();
            _vmodel = vmodel;
            TotalTextBlock.Text = vmodel.ReceiptPrice.ToString();
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
            try
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
            catch (Exception w)
            {
                MessageBox.Show("TextKeyUp \n" + w.Message);
            }
        }
        private async void Button_Click_Accept(object sender, RoutedEventArgs e)
        {
            try
            {
                decimal point = 0;
                if ((sender as Button)!.Uid == "0")
                    point = Convert.ToDecimal(UdsSpisanieBonus.Text.Replace(",", "."));

                var cashh = Infrastructure.APIs.APIRequests.GetCashRequest(
                                    new RequestInfoModel
                                    {
                                        code = user!.code!,
                                        participant = null,
                                        receipt = new Receipt
                                        {
                                            total = Convert.ToDecimal(TotalTextBlock.Text),
                                            skipLoyaltyTotal = String.IsNullOrEmpty(UdsSkipLoyaltyTotal.Text.Replace(",", ".")) ? 1 : Convert.ToDecimal(UdsSkipLoyaltyTotal.Text.Replace(",", ".")),
                                            points = point
                                        }
                                    });

                var operInfo = await Infrastructure.APIs.APIRequests.
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
                                skipLoyaltyTotal = String.IsNullOrWhiteSpace(UdsSkipLoyaltyTotal.Text) ? 1 : Convert.ToDecimal(UdsSkipLoyaltyTotal.Text.Replace(",", ".")),
                                cash = cashh!.purchase!.cashTotal,
                                number = lastCheck?.data?.check_no ?? null
                            },
                            tags = null
                        });

                if (operInfo is null || operInfo == new uToperationResult())
                {
                    MessageBox.Show("Ошибка! \nНеверно введены данные!");
                    return;
                }

                Setts.Default.Save();

                _vmodel.ChangePriceWithUDS(Convert.ToDecimal(UdsSpisanieBonus.Text.Replace(",", ".")));

                MessageBox.Show("Успешно!");

                this.Close();
            }
            catch (Exception w)
            {
                MessageBox.Show("Button_Click_Accept \n" + w.Message);
            }
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
