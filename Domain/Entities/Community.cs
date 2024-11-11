
namespace Domain.Entities
{
    public class Community
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Email {  get; set; } = default!;
        public string TagNumber { get; set; } = default!;
        public ICollection<WasteCollection> WasteCollections { get; set; }
            = new HashSet<WasteCollection>();
        public ICollection<WasteReport> WasteReports { get; set; } = new HashSet<WasteReport>();
        public Subscription Subscription { get; set; } = default!;
        public Guid SubscriptionId { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
