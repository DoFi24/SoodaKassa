using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitMarket.Domain.Models.UDSModels
{
    public class ResponseInfoModel
    {
        public User user { get; set; }
        public Purchase purchase { get; set; }
    }
    public class User
    {
        public string uid { get; set; }
        public string avatar { get; set; }
        public string displayName { get; set; }
        public string gender { get; set; }
        public string phone { get; set; }
        public string birthDate { get; set; }
        public ParticipantInfo participant { get; set; }
        public string channelName { get; set; }
        public string email { get; set; }
    }
    public class Purchase
    {
        public int maxPoints { get; set; }
        public int total { get; set; }
        public int skipLoyaltyTotal { get; set; }
        public int discountAmount { get; set; }
        public int discountPercent { get; set; }
        public int points { get; set; }
        public int pointsPercent { get; set; }
        public object? netDiscount { get; set; }
        public object? netDiscountPercent { get; set; }
        public int cash { get; set; }
        public int cashTotal { get; set; }
        public int cashBack { get; set; }
        public Extras extras { get; set; }

    }
    public class Extras
    {
        public int delivery { get; set; }
    }
    public class ParticipantInfo
    {
        public int id { get; set; }
        public int inviterId { get; set; }
        public int points { get; set; }
        public int discountRate { get; set; }
        public int cashbackRate { get; set; }
        public MembershipTier membershipTier { get; set; }
    }
    public class MembershipTier
    {
        public string uid { get; set; }
        public string name { get; set; }
        public int rate { get; set; }
        public Conditions conditions { get; set; }
    }

    public class Conditions
    {
        public Target totalCashSpent { get; set; }
        public Target effectiveInvitedCount { get; set; }
    }
    public class Target
    {
        public int target { get; set; }
    } 
}
