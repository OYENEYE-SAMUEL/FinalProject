using Domain.Enum;


namespace Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public decimal Amount { get; set; }
        public DateTime DateCreated { get; set; }
        public PaymentStatus Status { get; set; }
        public string ReferenceNumber { get; set; } = default!;
        public bool IsActive { get; set; }
        public Guid? SubscriptionId { get; set; }
        public Subscription? Subscription { get; set; }
    }
}
