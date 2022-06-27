using GitMarket.ViewModels.DialogsViewModel;
using System.Windows;
using System.Windows.Input;

namespace GitMarket.Views.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для ChecksHistoryDialogWindow.xaml
    /// </summary>
    public partial class ChecksHistoryDialogWindow : Window
    {
        public ChecksHistoryDialogWindow()
        {
            InitializeComponent();
            DataContext = new ChecksHistoryDialogWindowViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_KeyDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
