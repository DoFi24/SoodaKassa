namespace GitMarket.Domain.Models.UDSModels
{
    public class uToperationResult
    {
        public List<uToneOfs> oneOf { get; set; }
    }

    public class uToneOfs
    {
        public string? id { get; set; }
        public DateTime? dateCreated { get; set; }
        public string? action { get; set; } = "PURCHASE";
        public string? state { get; set; } = "NORMAL";
        public Customer? customer { get; set; }
        public Info? cashier { get; set; }
        public Info? branch { get; set; }
        public decimal points { get; set; }
        public decimal cash { get; set; }
        public decimal total { get; set; }
        public Origin? origin { get; set; }
        public string? receiptNumber { get; set; }
    }
    public class Info 
    {
        public long id { get; set; }
        public string? displayName { get; set; }
    }
    public class Customer : Info
    {
        public long uid { get; set; }
        public MembershipTier? membershipTier { get; set; }
    }
    public class Origin 
    {
        public long id { get; set; }
    }
}