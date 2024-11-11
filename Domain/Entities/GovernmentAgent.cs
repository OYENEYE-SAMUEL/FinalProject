

namespace Domain.Entities
{
    public class GovernmentAgent
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Email { get; set; } = default!;
        public string SectorName { get; set; } = default!;
        public string StateName { get; set; } = default!;
        public ICollection<WasteCollection> WasteCollections { get; set; } 
            = new HashSet<WasteCollection>();
        public Guid? ContractId { get; set; }
        public Contract? Contract { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsActive { get; set; }
    }
}
