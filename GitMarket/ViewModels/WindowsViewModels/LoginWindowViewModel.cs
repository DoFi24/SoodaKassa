using GitMarket.Commands;
using GitMarket.Infrastructure.APIs;
using GitMarket.Views.MainWindows;
using System.Windows;
using System.Windows.Controls;
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
            if ((obj) is null || (obj as PasswordBox) is null) return;
            if (await APIRequests.RegisterAsync(LoginText, (obj as PasswordBox)!.Password))
            {
                if (IsSaveLP)
                {
                    Setts.Default.IsSaveLoginPassword = true;
                    Setts.Default.Save();
                }
                new MainNavigationWindow().Show();
                closeWindow!.Invoke();
            }
            else
                MessageBox.Show("Логин или пароль введены неправильно!", "Неверные данные!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        #endregion

        #endregion

        #region Props

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
