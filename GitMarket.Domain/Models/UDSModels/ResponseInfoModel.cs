namespace GitMarket.Domain.Models.UDSModels
{
    public class ResponseInfoModel
    {
        public User? user { get; set; }
        public Purchase? purchase { get; set; }
    }
    public class User
    {
        public string? uid { get; set; }
        public string? avatar { get; set; }
        public string? displayName { get; set; }
        public string? gender { get; set; }
        public string? phone { get; set; }
        public string? birthDate { get; set; }
        public ParticipantInfo? participant { get; set; }
        public string? channelName { get; set; }
        public string? email { get; set; }
    }
    public class Purchase
    {
        public decimal? maxPoints { get; set; }
        public decimal? total { get; set; }
        public decimal? skipLoyaltyTotal { get; set; }
        public decimal? discountAmount { get; set; }
        public decimal? discountPercent { get; set; }
        public decimal? points { get; set; }
        public decimal? pointsPercent { get; set; }
        public object? netDiscount { get; set; }
        public object? netDiscountPercent { get; set; }
        public decimal cash { get; set; }
        public decimal cashTotal { get; set; }
        public decimal? cashBack { get; set; }
        public Extras? extras { get; set; }

    }
    public class Extras
    {
        public long? delivery { get; set; }
    }
    public class ParticipantInfo
    {
        public long? id { get; set; }
        public long? inviterId { get; set; }
        public decimal? points { get; set; }
        public decimal? discountRate { get; set; }
        public decimal? cashbackRate { get; set; }
        public DateTime? dateCreated { get; set; }
        public MembershipTier? membershipTier { get; set; }
    }
    public class MembershipTier
    {
        public string? uid { get; set; }
        public string? name { get; set; }
        public int? rate { get; set; }
        public Conditions? conditions { get; set; }
    }

    public class Conditions
    {
        public Target? totalCashSpent { get; set; }
        public Target? effectiveInvitedCount { get; set; }
    }
    public class Target
    {
        public decimal? target { get; set; }
    } 
}
