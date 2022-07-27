using System;
using System.Windows;
using System.Windows.Input;

namespace GitMarket.Views.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для SpecialDiscountDialogWindow.xaml
    /// </summary>
    public partial class SpecialDiscountDialogWindow : Window
    {
        public delegate void GetDiscountDel(decimal discountValue);
        public event GetDiscountDel? GetDiscountEvent;
        private decimal price;
        public SpecialDiscountDialogWindow(decimal _price)
        {
            InitializeComponent();
            price = _price;
            DiscountLabel.Focus();
        }
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Return:
                    var discount = Convert.ToDecimal(DiscountLabel.Text.ToString().Replace('.', ','));
                    if (discount > 100)
                    {
                        new ErrorMessageDialogWindow("Скидка не может превышать 100%!").ShowDialog();
                        return;
                    }
                    GetDiscountEvent!(discount);
                    Close();
                    break;
                case Key.Escape:
                    Close();
                    break;
            }
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var discount = Convert.ToDecimal(DiscountLabel.Text.ToString().Replace('.', ','));
            if (discount > 100)
            {
                new ErrorMessageDialogWindow("Скидка не может превышать 100%!").Show();
                return;
            }
            GetDiscountEvent!(discount);
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
