using GitMarket.Domain.Models.TitiModels.ProductsModel;
using GitMarket.Infrastructure.APIs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitMarket.ViewModels.PagesViewModels
{
    public class ProductsSpisaniePageViewModel : ViewModels.Base.BasePageViewModel
    {
        public ProductsSpisaniePageViewModel()
        {
            GetSpisanieProducts();
        }

        #region Properties

        #region SelectedDates

        private DateTime? _selectedStartDate = DateTime.Now.AddDays(-7).Date;
        public DateTime? SelectedStartDate
        {
            get => _selectedStartDate;
            set => Set(ref _selectedStartDate, value);
        }
        private DateTime? _selectedEndDate = DateTime.Now.Date;
        public DateTime? SelectedEndDate
        {
            get { return _selectedEndDate; }
            set { Set(ref _selectedEndDate, value); }
        }

        #endregion

        private ObservableCollection<SpisanieDetail>? _SpisanieCollection;
        public ObservableCollection<SpisanieDetail>? SpisanieCollection
        {
            get { return _SpisanieCollection; }
            set { Set(ref _SpisanieCollection, value); }
        }

        #endregion

        #region Methods

        private async void GetSpisanieProducts()
        {
            var result = await APIRequests.GetFromAPIAsync<SpisanieModel>(
                       new Domain.Models.APIResponseRequest.RequestModelGet
                       {
                           shopId = Setts.Default.ShopId,
                           staffId = Setts.Default.StaffId,
                           page = 1,
                           pageSize = 10,
                           parameter = "get",
                           data = null
                       }, "FC_SPISANIE_DOC_GET");

            SpisanieCollection = new ObservableCollection<SpisanieDetail>(result.SelectMany(s=>s.SPISANIE_DETAILS));
                
        }

        #endregion
    }
}
