using Domain.Enum;

namespace WasteSystemApp.Models
{
    public class SubscriptionViewModel
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public decimal AmountPaid { get; set; }
        public Frequency Frequency { get; set; }
        public DateTime CurrentPlanDateStart { get; set; }
        public PlanType CurrentPlanType { get; set; }
    }
}
