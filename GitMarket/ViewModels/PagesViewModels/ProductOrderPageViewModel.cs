using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitMarket.ViewModels.PagesViewModels
{
    public class ProductOrderPageViewModel : Base.BasePageViewModel
    {
        public ProductOrderPageViewModel()
        {

        }

        private string? _searchTextBox = "Поиск";
        public string? SearchTextBox
        {
            get { return _searchTextBox; }
            set { Set(ref _searchTextBox, value); }
        }

    }
}
