using System.Windows;
using System.Windows.Input;

namespace GitMarket.Views.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для ErrorMessegeDialogWindow.xaml
    /// </summary>
    public partial class ErrorMessegeDialogWindow : Window
    {
        public ErrorMessegeDialogWindow()
        {
            InitializeComponent();
        }
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Escape)
                Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
