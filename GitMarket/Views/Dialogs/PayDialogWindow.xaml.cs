using GitMarket.ViewModels.PagesViewModels;
using GitMarket.ViewModels.WindowsViewModels;
using System;
using System.Windows;

namespace GitMarket.Views.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для PayDialogWindow.xaml
    /// </summary>
    public partial class PayDialogWindow : Window
    {
        private MainNavigationWindowViewModel viewModel;
        public PayDialogWindow(MainNavigationWindowViewModel _viewModel)
        {
            InitializeComponent();
            viewModel = _viewModel;
            NalLabel.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EndOplata();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void EndOplata() 
        {
            viewModel.ReceiptPaid = Convert.ToDecimal(NalLabel.Text.Replace('.',','));
            viewModel.ReceiptPaidCard = Convert.ToDecimal(CardLabel.Text.Replace('.', ','));
            viewModel.GetCalculate();
            Close();
        }

        private void Window_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case System.Windows.Input.Key.Escape:
                    Close();
                    break;
                case System.Windows.Input.Key.Return:
                    EndOplata();
                    break;
            }
        }

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}