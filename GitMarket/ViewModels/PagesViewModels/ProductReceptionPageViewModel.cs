using GitMarket.Domain.Models.TitiModels.ProductsModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitMarket.ViewModels.PagesViewModels
{
    public class ProductReceptionPageViewModel : Base.BasePageViewModel
    {
        public ProductReceptionPageViewModel()
        {
            PageTitle = "Прием товаров"; 
            GetRequsts();
        }
        public async void GetRequsts()
        {
            RequestsList = new ObservableCollection<PrihodModel>(
                await Infrastructure.APIs.APIRequests.GetFromAPIAsync<PrihodModel>(
                    new Domain.Models.APIResponseRequest.RequestModelGet
                    {
                        shopId = Setts.Default.ShopId,
                        staffId = Setts.Default.StaffId,
                        page = 1,
                        pageSize = 10,
                        parameter = "get",
                        data = new
                        {
                            to_shop_id = new ArrayList { "=", 1 }
                        }
                    }, "FC_PRIHOD_DOC_GET"));
            if (RequestsList.Any())
                SelectedRequest = RequestsList[0];
            else
                SelectedRequest = new PrihodModel();
        }

        private ObservableCollection<PrihodModel>? _requestsList;
        public ObservableCollection<PrihodModel>? RequestsList
        {
            get { return _requestsList; }
            set { Set(ref _requestsList, value); }
        }
        private PrihodModel? _selectedRequest;
        public PrihodModel? SelectedRequest
        {
            get { return _selectedRequest; }
            set { Set(ref _selectedRequest, value); }
        }
    }
}
