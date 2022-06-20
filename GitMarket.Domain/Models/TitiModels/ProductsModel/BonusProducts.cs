namespace GitMarket.Domain.Models.TitiModels.ProductsModel
{
    public class BonusProducts
    {
        public int Prihod_Detail_Id { get; set; } =0;
        public int Card_Id { get; set; }=0;
        public int Bonus_Id { get; set; } = 0;
        public int Pay_Bonus_Id { get; set; } = 0;
        public int Bonuses { get; set; } = 0;
        public object[]? ToProducts { get; set; }
        public int Pay_Bonus_Sum { get; set; } = 0;
        public int Bonus_Sum { get; set; } = 0;
    }
}
