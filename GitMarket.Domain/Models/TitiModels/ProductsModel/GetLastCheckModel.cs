using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitMarket.Domain.Models.TitiModels.ProductsModel
{
    public class GetLastCheckModel
    {
        public string parameter { get; set; }
        public LastCheckData data { get; set; }
        public decimal sum { get; set; }
        public decimal esum { get; set; }
    }
    public class LastCheckData
    {
        public long sklad_id { get; set; }
        public long shop_id { get; set; }
        public long staff_id { get; set; }
        public long kassa_id { get; set; }
        public long kontragent_id { get; set; }
    }
}
