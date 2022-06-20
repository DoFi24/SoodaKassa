﻿using GitMarket.Domain.Models.TitiModels.ProductsModel;
using GitMarket.Infrastructure;
using GitMarket.ViewModels.DialogsViewModel;
using GitMarket.ViewModels.PagesViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GitMarket.Views.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для PaymentEndDialogWindow.xaml
    /// </summary>
    public partial class PaymentEndDialogWindow : Window
    {
        private ViewModels.WindowsViewModels.MainNavigationWindowViewModel _model;
        public PaymentEndDialogWindow(ViewModels.WindowsViewModels.MainNavigationWindowViewModel model)
        {
            InitializeComponent();
            _model = model;
            DataContext = new PaymentEndDialogWindowViewModel(model);
            UserName.Text = StatisticalResources.UserName;
            DateTimeStr.Text = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();
            CheckNumber.Text = model.Prodaja.Check_Id.ToString();
        }

        private void PrintMethod()
        {
            try
            {
                new PrintDialog().PrintVisual(printGrid, "Печать чека");
                //PrintDialog printDialog = new PrintDialog();
                //printGrid.Measure(new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight));
                //printGrid.Arrange(new Rect(printGrid.DesiredSize));
                //printGrid.UpdateLayout();
                //printDialog.PrintTicket.PageMediaSize = new PageMediaSize(printDialog.PrintableAreaWidth, printGrid.ActualHeight);
                //printDialog.PrintQueue = LocalPrintServer.GetDefaultPrintQueue();
                //printDialog.PrintVisual(printGrid, "GitMarket");
            }
            catch 
            {
            
            }
        }
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PrintMethod();
            _model.ExecutedClearSelectedProductsCommand(null);
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _model.ExecutedClearSelectedProductsCommand(null);
            Close();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                _model.ExecutedClearSelectedProductsCommand(null);
                Close();
            }
            else if (e.Key == Key.Return)
            {
                PrintMethod();
                _model.ExecutedClearSelectedProductsCommand(null);
                Close();
            }
        }
    }
}
