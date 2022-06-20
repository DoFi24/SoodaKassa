using GitMarket.ViewModels.WindowsViewModels;
using System.Windows;

namespace GitMarket.Views.MainWindows
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            DataContext = new LoginWindowViewModel(this);
        }
    }
}
