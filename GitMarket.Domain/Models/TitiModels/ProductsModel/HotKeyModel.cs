namespace GitMarket.Domain.Models.TitiModels.ProductsModel
{
    public class HotKeyModel
    {
        public int Id { get; set; }
        public int Product_Id { get; set; }
        public string? Product_Name { get; set; }
        public string? Key { get; set; }
        public bool Type { get; set; }
    }
}
