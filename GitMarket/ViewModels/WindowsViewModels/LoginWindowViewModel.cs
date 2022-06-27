using GitMarket.Commands;
using GitMarket.Infrastructure.APIs;
using GitMarket.Views.MainWindows;
using System.Windows;
using System.Windows.Input;

namespace GitMarket.ViewModels.WindowsViewModels
{
    public class LoginWindowViewModel : Base.BaseViewModel
    {
        public delegate void CloseWindow();
        public CloseWindow? closeWindow;
        public LoginWindowViewModel(LoginWindow _loginView)
        {
            SignInCommand = new RelayCommand(ExecuteSignInCommand, CanSignInCommandExecuted);
        }
        public LoginWindowViewModel()
        {
            SignInCommand = new RelayCommand(ExecuteSignInCommand, CanSignInCommandExecuted);
        }

        #region Commands

        #region SignInCommand
        public ICommand SignInCommand { get; }
        private bool CanSignInCommandExecuted(object arg)
        {
            if (LoginText != string.Empty)
                return true;
            return false;
        }
        public async void ExecuteSignInCommand(object obj)
        {
            if (await APIRequests.RegisterAsync(LoginText, PasswordText))
            {
                if (IsSaveLP)
                {
                    Setts.Default.IsSaveLoginPassword = true;
                    Setts.Default.Save();
                }
                new MainNavigationWindow().Show();
                closeWindow();
            }
            else
                MessageBox.Show("Логин или пароль введены неправильно!", "Неверные данные!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        #endregion

        #endregion

        #region Props

        private string? _passwordText = "";
        public string? PasswordText
        {
            get { return _passwordText; }
            set
            {
                Set(ref _passwordText, value);
            }
        }

        private string? _loginText;
        public string? LoginText
        {
            get { return _loginText; }
            set
            {
                Set(ref _loginText, value);
            }
        }

        private bool _isSaveLP;
        public bool IsSaveLP
        {
            get { return _isSaveLP; }
            set
            {
                Set(ref _isSaveLP, value);
            }
        }


        #endregion
    }
}
