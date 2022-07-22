using System.Windows;
using System.Windows.Input;

namespace GitMarket.Views.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для ErrorMessageDialogWindow.xaml
    /// </summary>
    public partial class ErrorMessageDialogWindow : Window
    {
        public ErrorMessageDialogWindow(string message)
        {
            InitializeComponent();
            ErrorLabel.Text = message;
        }
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                DialogResult = true;
                Close();
            }
            else if(e.Key == Key.Escape)
            {
                DialogResult = false;
                Close();
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
