namespace GitMarket.Domain.Models.UDSModels
{
    public class uTuserFindErr
    {
        public string errorCode { get; set; }
        public string message { get; set; }
    }

    public class uTuserFind
    {
        public uTuser user { get; set; }
        public string? code { get; set; } //kod oplaty dl9 oplaty
        public uTpurchase? purchase { get; set; }
    }


    public class uTpurchase
    {
        public decimal cash { get; set; } //итого со скидкой
        public decimal discountAmount { get; set; } //неизв возможно старые покупки
        public decimal certificatePoints { get; set; } //использованные скидки сертификата
        public decimal maxPoints { get; set; } //макс отдаваемые бонусы
        public object extras { get; set; } //хз
        public decimal netDiscount { get; set; } //skidka navernoe
        public decimal points { get; set; } //bonus
        public decimal netDiscountPercent { get; set; } //dis prosent
        public decimal cashBack { get; set; }
        public decimal total { get; set; } //itogo bez skidki
        public decimal cashTotal { get; set; } //doljen oplatit
        public decimal skipLoyaltyTotal { get; set; }
        public decimal pointsPercent { get; set; } //bonus prosent
        public decimal discountPercent { get; set; }
    }


    public class uTuser
    {
        public object phone { get; set; }
        public uTgender gender { get; set; }
        public string uid { get; set; }
        public DateTime? birthDate { get; set; }
        public string? channelName { get; set; }
        public List<object> tags { get; set; }
        public string? email { get; set; }
        public object avatar { get; set; }
        public uTparticipant participant { get; set; }
        public string? displayName { get; set; }
    }


    public class uTparticipant
    {
        public ulong? inviterId { get; set; }
        public decimal discountRate { get; set; }   //Применяется скидка к заказу в размере discountRate. Если discountRate = 0, то это значит, что клиенту после оплаты будут начислены бонусы и скидка не нужна.
        public decimal cashbackRate { get; set; }
        public DateTime? dateCreated { get; set; }
        public decimal points { get; set; }
        public ulong id { get; set; }
        public DateTime? lastTransactionTime { get; set; }
        public uTmembershipTiers? membershipTier { get; set; } //uid: base

    }

    public enum uTgender
    {
        NOT_SPECIFIED = 0,
        MALE = 1,
        FEMALE = 2
    }
}
	