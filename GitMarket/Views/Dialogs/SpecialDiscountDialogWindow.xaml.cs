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
            if (e.Key == Key.Return)
            {
                GetDiscountEvent(Convert.ToDecimal(DiscountLabel.Text.ToString().Replace('.', ',')));
                Close();
            }
            else if (e.Key == Key.Escape)
            {
                Close();
            }
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
