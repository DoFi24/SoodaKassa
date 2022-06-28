using GitMarket.Commands;
using GitMarket.Domain.Models.APIResponseRequest;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace GitMarket.ViewModels.DialogsViewModel
{
    public class ChecksHistoryDialogWindowViewModel : ViewModels.Base.BasePageViewModel
    {
        public Action? CloseWindow { get; set; }
        public ChecksHistoryDialogWindowViewModel()
        {
            GetChecks();
        }

        private async void GetChecks()
        {
            var result = await Infrastructure.APIs.APIRequests.GetFromAPIAsync<CheckModel>(
                new RequestModelGet
                {
                    parameter = "window",
                    pageSize = 100,
                    page = 1,
                    staffId = Setts.Default.StaffId,
                    shopId = Setts.Default.ShopId,
                    data = null,

                }, "FC_PRODAJA_GET_DOC");
            ChecksCollection = new ObservableCollection<CheckModel>(result);
        }

        private ObservableCollection<CheckModel> _checksCollection = new ObservableCollection<CheckModel>();
        public ObservableCollection<CheckModel> ChecksCollection
        {
            get { return _checksCollection; }
            set { Set(ref _checksCollection, value); }
        }

        private CheckModel _selectedCheck = new CheckModel();
        public CheckModel SelectedCheck
        {
            get { return _selectedCheck;}
            set { Set(ref _selectedCheck, value);}
        }

        private RelayCommand? _printCheckCommand;
        public RelayCommand? PrintCheckCommand =>
            _printCheckCommand ??= new RelayCommand(ExecutePrintCheckCommand, (object obj) => { if ((SelectedCheck != new CheckModel()) && (SelectedCheck is not null)) return true; return false; });
        private void ExecutePrintCheckCommand(object obj)
        {
            try
            {
                new PrintDialog().PrintVisual((obj as ScrollViewer), "Печать чека");
            }
            catch 
            {
                MessageBox.Show("Произошла ошибка при отправке на принтер!");
            }
        }
    }
}
