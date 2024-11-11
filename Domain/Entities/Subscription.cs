using Domain.Enum;


namespace Domain.Entities
{
    public class Subscription
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public decimal Price { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime CurrentPlanDateStart { get; set; }
        public Frequency Frequency { get; set; }
        public DateTime CurrentPlanDateEnd { get; set; }
        public PlanType CurrentPlanType { get; set; }
        public ICollection<Community> Communities { get; set; } = new HashSet<Community>();
        public ICollection<Individual> Individuals { get; set; } = new HashSet<Individual>();
        public bool IsActive {  get; set; }
    }
}
