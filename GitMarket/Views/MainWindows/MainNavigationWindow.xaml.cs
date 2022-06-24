using GitMarket.Domain.Models.APIResponseRequest;
using GitMarket.Domain.Models.TitiModels.ProductsModel;
using GitMarket.Infrastructure;
using GitMarket.Infrastructure.APIs;
using GitMarket.ViewModels.WindowsViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GitMarket.Views.MainWindows
{
    /// <summary>
    /// Логика взаимодействия для MainNavigationWindow.xaml
    /// </summary>
    public partial class MainNavigationWindow : Window
    {
        public bool _isNavigating = false;
        private MainNavigationWindowViewModel main;
        private MainNavigationWindow SecondWindow;
        public MainNavigationWindow()
        {
            InitializeComponent();
            main = new MainNavigationWindowViewModel(this)
            {
                closeMainWindow = Close
            };
            DataContext = main;
            ProgramStartWriteToJournal();
        }

        private async void ProgramStartWriteToJournal()
        {
            await APIRequests.SaveChangesAsync(new JournalApiModel
            {
                parameter = "window",
                data = new JournalApiModelData
                {
                    shop_id = Setts.Default.ShopId,
                    staff_id = Setts.Default.StaffId,
                    rows = new List<JournalValue>
                    {
                        new JournalValue {value = $"Запуск программы"}
                    }
                }
            });
        }

        public void CloseWindow()
        {
            main.Dispose();
            this.Close();
        }

        private string inputDeviceText = "";
        private async void CheckIsDevice()
        {
            await Task.Run(() => timerForDevice());
        }
        private void timerForDevice()
        {
            Thread.Sleep(200);

            if (inputDeviceText.Length > 4)
            {
                this.Dispatcher.Invoke(() =>
                {
                    main.SearchByBarcode(inputDeviceText);//дополнительная логика, нужно обработать перегрузку0
                });
            }
            Thread.Sleep(5);  // это для того чтобы асинхронные изменения успели вступить в силу, а то тазалаганда улгурбойтат, астыдагы переменныйды

            inputDeviceText = "";

            return;
        }
        private void Window_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case System.Windows.Input.Key.Add:
                    main.ToPayCommand.Execute("");
                    itogTextBox.Focus();
                    return;
                case System.Windows.Input.Key.Insert:
                    textBoxSearch.Focus();
                    return;
                case System.Windows.Input.Key.Escape:
                    if (main.IsOpenPopup || main.SearchText.Length > 0)
                    {
                        main.IsOpenPopup = false;
                        main.SearchText = "";
                    }
                    itogTextBox.Focus();
                    return;
                case System.Windows.Input.Key.Subtract:
                    main.GetBonusCommand.Execute("");
                    itogTextBox.Focus();
                    return;
                case System.Windows.Input.Key.Up:
                    main.UpDownCommand("Up");
                    itogTextBox.Focus();
                    return;
                case System.Windows.Input.Key.Down:
                    main.UpDownCommand("Down");
                    itogTextBox.Focus();
                    return;
                case System.Windows.Input.Key.Divide:
                    if (main.SelectedProductItem != new SaleProduct())
                    {
                        main.ChangeProductQuantityCommand.Execute("unpack");
                    }
                    itogTextBox.Focus();
                    return;
                case System.Windows.Input.Key.Multiply:
                    if (main.SelectedProductItem != new SaleProduct())
                    {
                        main.ChangeProductQuantityCommand.Execute("");
                    }
                    itogTextBox.Focus();
                    return;
                case System.Windows.Input.Key.Delete:
                    if (main.SelectedProductItem != new SaleProduct())
                        main.DeleteSelectedProduct();
                    itogTextBox.Focus();
                    return;
                case System.Windows.Input.Key.Return:
                    main.AddSelectedProduct();
                    itogTextBox.Focus();
                    return;

            }

            if (e.Key.ToString().Length > 1 && e.Key.ToString()[0] == 'D' && e.Key.ToString() != "Decimal")
            {
                if (inputDeviceText == "")
                    CheckIsDevice();
                inputDeviceText += e.Key.ToString()[1];
                return;
            }
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == System.Windows.Input.Key.Up)
            {
                itogTextBox.Focus();
                main.SearchUpDown("Up");
                return;
            }
            else if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == System.Windows.Input.Key.Down)
            {
                itogTextBox.Focus();
                main.SearchUpDown("Down");
                return;
            }
            else if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == System.Windows.Input.Key.Delete)
            {
                main.ClearSelectedProductsCommand.Execute("");
                itogTextBox.Focus();
                return;
            }
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift && HotKeysStructure.HotKeysDictionary.Any(s => s.Key == e.Key.ToString()))
            {
                main.AddWithHotKey(HotKeysStructure.HotKeysDictionary.First(s => s.Key == e.Key.ToString()));
                itogTextBox.Focus();
                return;
            }
            else if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == System.Windows.Input.Key.F)
            {
                main.GetProductwithAPI();
                return;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
        /// <summary>
        /// Makes virtual keyboard disappear
        /// </summary>
        /// <param name="sender"></param>
        private void LoseFocus(object sender)
        {
            var control = sender as Control;
            var isTabStop = control.IsTabStop;
            control.IsTabStop = false;
            control.IsEnabled = false;
            control.IsEnabled = true;
            control.IsTabStop = isTabStop;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Button_Click_Minimize(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else
                this.WindowState = WindowState.Normal;
        }

        private void PaidTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_isNavigating)
            {
                if (WindowComboBox.SelectedIndex == 1)
                {
                    SecondWindow = new MainNavigationWindow
                    {
                        Owner = this,
                        _isNavigating = true
                    };
                    SecondWindow.Show();
                }
                else
                {
                    Owner.Show();
                }
                this.Hide();
            }
        }
    }
}
