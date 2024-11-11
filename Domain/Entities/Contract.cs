using Domain.Enum;

namespace Domain.Entities
{
    public class Contract
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ProjectDescription { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal FundingAmount { get; set; }
        public ContractStatus Status { get; set; }
        public ICollection<GovernmentAgent> GovernmentAgents { get; set; } 
            = new HashSet<GovernmentAgent>();
        public string AuthorizedSignature { get; set; } = default!;
        public bool IsActive { get; set; }
    }
}
