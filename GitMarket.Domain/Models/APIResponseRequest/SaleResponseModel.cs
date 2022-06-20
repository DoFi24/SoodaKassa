using GitMarket.Domain.Models.TitiModels.ProductsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitMarket.Domain.Models.APIResponseRequest
{
    public class SaleResponseModel
    {
        public string? message { get; set; }
        public ProdajaModel? data { get; set; }
    }
}
