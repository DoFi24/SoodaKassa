namespace GitMarket.Domain.Models.TitiModels.ProductsModel
{
    public class APICategories
    {
        public int ID { get; set; }
        public string? NAME { get; set; }
        public string? IMAGE { get; set; }
        public int? PARENT_ID { get; set; }
        public int? PARENT_POSITION { get; set; }
        public string? COMMENT { get; set; }
    }
}
