﻿using GitMarket.Domain.Models.APIResponseRequest;
using GitMarket.Domain.Models.TitiModels.ProductsModel;
using GitMarket.Infrastructure;
using GitMarket.Infrastructure.APIs;
using GitMarket.ViewModels.WindowsViewModels;
using GitMarket.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace GitMarket.Views.MainWindows
{
    /// <summary>
    /// Логика взаимодействия для MainNavigationWindow.xaml
    /// </summary>
    public partial class MainNavigationWindow : Window
    {
        public bool _isMain;
        private MainNavigationWindowViewModel? main;
        private MainNavigationWindow? SecondWindow;
        public MainNavigationWindow(bool isMain)
        {
            InitializeComponent();

            _isMain = isMain;
            main = new MainNavigationWindowViewModel(this)
            {
                closeMainWindow = Close
            };
            DataContext = main;
            WindowComboBox.SelectedIndex = 0;
            if (_isMain)
                ProgramStartWriteToJournal();
            else
                WindowComboBox.Foreground = Brushes.Red;
            DateTextBlock.Text = DateTime.UtcNow.ToString("dd.MM.yyyy");
        }

        private async void ProgramStartWriteToJournal()
        {
            await APIRequests.SaveChangesAsync(new JournalApiModel
            {
                parameter = "windows",
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
            main!.Dispose();
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
                    main!.SearchByBarcode(inputDeviceText);//дополнительная логика, нужно обработать перегрузку0
                });
            }
            Thread.Sleep(5);  // это для того чтобы асинхронные изменения успели вступить в силу, а то тазалаганда улгурбойтат, астыдагы переменныйды

            inputDeviceText = "";

            return;
        }
        private void Window_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //MessageBox.Show(e.Key.ToString());
            //if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == System.Windows.Input.Key.Up)
            //{
            //    itogTextBox.Focus();
            //    main!.SearchUpDown(true);
            //    return;
            //}
            //else if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == System.Windows.Input.Key.Down)
            //{
            //    itogTextBox.Focus();
            //    main!.SearchUpDown(false);
            //    return;
            //}
            //else 
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == System.Windows.Input.Key.Delete)
            {
                if (main!.ClearSelectedProductsCommand.CanExecute(null)) main.ClearSelectedProductsCommand.Execute(null);
                itogTextBox.Focus();
                return;
            }
            else if (HotKeysStructure.HotKeysDictionary!.Any(s => s.Key == e.Key.ToString()))
            {
                main!.AddWithHotKey(HotKeysStructure.HotKeysDictionary!.Single(s => s.Key == e.Key.ToString())!);
                itogTextBox.Focus();
                return;
            }
            //else if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == System.Windows.Input.Key.F)
            //{
            //    main!.GetProductwithAPI();
            //    return;
            ////}
            //else if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == System.Windows.Input.Key.C)
            //{
            //    main!.OpenSpesialDiscount();
            //    return;
            //}
            switch (e.Key)
            {
                case System.Windows.Input.Key.Up:
                    main!.UpDownCommand(false);
                    itogTextBox.Focus();
                    return;
                case System.Windows.Input.Key.Down:
                    main!.UpDownCommand(true);
                    itogTextBox.Focus();
                    return;
                case System.Windows.Input.Key.Enter:
                    if (textBoxSearch.IsFocused)
                    {
                        main!.GetProductwithAPI();
                        textBoxSearch.Text = "";
                    }
                    itogTextBox.Focus();
                    return;
                case System.Windows.Input.Key.Add:
                    if (main!.ToPayCommand.CanExecute(null)) main.ToPayCommand.Execute(null);
                    itogTextBox.Focus();
                    return;
                case System.Windows.Input.Key.Insert:
                    textBoxSearch.Focus();
                    return;
                case System.Windows.Input.Key.Escape:
                    if (main!.IsOpenPopup || main.SearchText.Length > 0)
                    {
                        main.IsOpenPopup = false;
                        main.SearchText = "";
                    }
                    itogTextBox.Focus();
                    return;
                case System.Windows.Input.Key.Subtract:
                    main!.ExecutedGetBonusCommand(null);
                    itogTextBox.Focus();
                    return;
                case System.Windows.Input.Key.Decimal:
                    textBoxSearch.Focus();
                    return;
                case System.Windows.Input.Key.Divide:
                    if (main!.SelectedProductItem is not null || main!.SelectedProductItem != new SaleProduct())
                    {
                        main!.OpenSpesialDiscount();
                    }
                    itogTextBox.Focus();
                    return;
                case System.Windows.Input.Key.Multiply:
                    if (main!.SelectedProductItem is not null || main!.SelectedProductItem != new SaleProduct())
                    {
                        main.ChangeProductQuantityCommand.Execute(false);
                    }
                    itogTextBox.Focus();
                    return;
                case System.Windows.Input.Key.Delete:
                    if (main!.SelectedProductItem != new SaleProduct())
                        main.DeleteSelectedProduct();
                    itogTextBox.Focus();
                    return;
                case System.Windows.Input.Key.Oem3:
                    new ProductsAndCategoriesPage(main!).ShowDialog();
                    return;
                case System.Windows.Input.Key.Left:
                    ChangeWindow(0);
                    return;
                case System.Windows.Input.Key.Right:
                    ChangeWindow(1);
                    return;
            }

            if (e.Key.ToString().Length > 1 && e.Key.ToString()[0] == 'D' && e.Key.ToString() != "Decimal")
            {
                if (inputDeviceText == "")
                    CheckIsDevice();
                inputDeviceText += e.Key.ToString()[1];
                return;
            }
        }

        private void ChangeWindow(int v)
        {
            WindowComboBox.SelectedIndex = v;
            if (_isMain)
            {
                if (WindowComboBox.SelectedIndex == 0)
                    return;
                SecondWindow!.WindowComboBox.SelectedIndex = 1;
                SecondWindow.Show();
            }
            else
            {
                if (WindowComboBox.SelectedIndex == 1)
                    return;
                (Owner as MainNavigationWindow)!.WindowComboBox.SelectedIndex = 0;
                Owner.Show();
            }
            this.Hide();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
        private void LoseFocus(object sender)
        {
            var control = sender as Control;
            var isTabStop = control!.IsTabStop;
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
        private void WindowComboBox_DropDownClosed(object sender, System.EventArgs e)
        {
            if (_isMain)
            {
                if (WindowComboBox.SelectedIndex == 0)
                    return;
                SecondWindow!.WindowComboBox.SelectedIndex = 1;
                SecondWindow.Show();
            }
            else
            {
                if (WindowComboBox.SelectedIndex == 1)
                    return;
                (Owner as MainNavigationWindow)!.WindowComboBox.SelectedIndex = 0;
                Owner.Show();
            }
            this.Hide();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_isMain)
                SecondWindow = new MainNavigationWindow(false)
                {
                    Owner = this
                };
        }

        private void OpenMenuClick(object sender, RoutedEventArgs e)
        {
            DoubleAnimation openAnimation = new DoubleAnimation();
            if (MenuGrid.Width < 250)
                openAnimation.To = 250;
             if (MenuGrid.Width >= 250)
                openAnimation.To = 0;
            openAnimation.Duration = new Duration(new TimeSpan(0,0,1));
            MenuGrid.BeginAnimation(Grid.WidthProperty, openAnimation);
        }
    }
}
