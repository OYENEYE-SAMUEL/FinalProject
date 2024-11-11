using Domain.Enum;

namespace Application.DTOs
{
    public class SubscriptionRequestModel
    {
        public decimal Amount { get; set; }
        public Frequency Frequency { get; set; }
        public DateTime CurrentPlanDateStart { get; set; }
        public PlanType CurrentPlanType { get; set; }
    }

    public class SubscriptionResponseModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public decimal Price { get; set; }
        public decimal AmountPaid { get; set; }
        public Frequency Frequency { get; set; }
        public DateTime CurrentPlanDateStart { get; set; }
        public DateTime CurrentPlanDateEnd { get; set; }
        public PlanType CurrentPlanType { get; set; }
        public bool IsActive {  get; set; }
    }


}
