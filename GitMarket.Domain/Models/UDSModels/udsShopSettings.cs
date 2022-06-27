namespace GitMarket.Domain.Models.UDSModels
{
    public class udsShopSettings
    {
        public ulong id { get; set; }
        public string name { get; set; }
        public string? promoCode { get; set; }
        public uTbaseDiscountPolicy? baseDiscountPolicy { get; set; }
        public bool purchaseByPhone { get; set; }
        public bool writeInvoice { get; set; }
        public string? currency { get; set; }
        public shopProgramSettings? loyaltyProgramSettings { get; set; }
        public string? slug { get; set; }
    }

    public enum uTbaseDiscountPolicy
    {
        APPLY_DISCOUNT = 1,
        CHARGE_SCORES = 2
    }

    public class shopProgramSettings
    {
        public List<uTmembershipTiers>? membershipTiers { get; set; }
        public object deferPointsForDays { get; set; }
        public List<decimal> referralCashbackRates { get; set; }
        public uTbaseMembershipTier baseMembershipTier { get; set; }
        public object receiptLimit { get; set; }
        public object cashierAward { get; set; }
        public object referralReward {get;set;}
        public decimal maxScoresDiscount { get; set; }
    }

    public class uTmembershipTiers
    {
        public string uid { get; set; }
        public string name { get; set; }
        public uTconditions? conditions { get; set; }
        public decimal rate { get; set; }
    }

    public class uTconditions
    {
        public uTtotalCashSpent? totalCashSpent { get; set; }
        public object effectiveInvitedCount { get; set; }
    }

    public class uTtotalCashSpent
    {
        public decimal target { get; set; }
    }
    public class uTbaseMembershipTier
    {
        public string uid { get; set; }
        public string name { get; set; }
        public uTconditions conditions { get; set; }
        public decimal rate { get; set; }
    }
}