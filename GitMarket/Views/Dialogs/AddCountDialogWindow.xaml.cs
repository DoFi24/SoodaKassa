using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace GitMarket.Views.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для AddCountDialogWindow.xaml
    /// </summary>
    public partial class AddCountDialogWindow : Window
    {
        public delegate void SendCountDel(decimal ProductCount);
        public event SendCountDel? ReturnProductCountEvent;
        public AddCountDialogWindow(string ProductName, decimal _maxCount, string pack = "pack")
        {
            InitializeComponent();
            if (pack == "unpack")
            {
                ImageProducts.Source = new BitmapImage(new Uri(@"/Resources/DefaultImages/manyproducts.png", UriKind.RelativeOrAbsolute));
            }
            maxCount.Text = _maxCount.ToString();
            ProductNameLabel.Text = ProductName;
            ProductCountLabel.Focus();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
            else if (e.Key == Key.Return)
            {
                try
                {
                    string str = ProductCountLabel.Text.ToString();
                    if (Convert.ToDecimal(str.Replace('.', ',')) <= Convert.ToDecimal(maxCount.Text.Replace('.', ',')))
                        ReturnProductCountEvent(Convert.ToDecimal(str.Replace('.', ',')));
                    else
                        MessageBox.Show("Количество превышает максимальное количество!");
                }
                catch
                {
                    MessageBox.Show("Не правильный формат данных");
                }
                Close();
            }
        }
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
