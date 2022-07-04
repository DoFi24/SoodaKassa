using GitMarket.ViewModels.WindowsViewModels;
using System.Windows;

namespace GitMarket.Views.MainWindows
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly LoginWindowViewModel loginVM;
        public LoginWindow()
        {
            InitializeComponent();
            loginVM = new LoginWindowViewModel(this)
            {
                closeWindow = Close
            };
            DataContext = loginVM;
        }

        private void TextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Return)
                loginVM.SignInCommand.Execute(password.Password);
        }
    }
}
