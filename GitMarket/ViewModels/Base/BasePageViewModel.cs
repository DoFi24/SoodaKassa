using GitMarket.Domain.Models.TitiModels.ProductsModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitMarket.ViewModels.Base
{
    public abstract class BasePageViewModel : BaseViewModel
    {

        #region Properties

        private string? _pageTitle;
        public string? PageTitle
        {
            get { return _pageTitle; }
            set { Set(ref _pageTitle, value); }
        }

        private double _pageWidth = 600d;
        public double PageWidth
        {
            get { return _pageWidth; }
            set { Set(ref _pageWidth, value); }
        }

        private double _pageHeight = 900d;
        public double PageHeight
        {
            get { return _pageHeight; }
            set { Set(ref _pageHeight, value); }
        }

        #endregion

    }
}
