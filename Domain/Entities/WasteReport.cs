using Domain.Enum;

namespace Domain.Entities
{
    public class WasteReport
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public ReportStatus Status { get; set; } 
        public Guid? WasteCollectionId { get; set; }
        public WasteCollection? WasteCollection { get; set; }
        public Guid? CommunityId { get; set; }
        public Community? Community { get; set; }
        public Guid? GovernmentAgentId { get; set; }
        public Guid? IndividualId { get; set; }
        public Individual? Individual { get; set; }
        public GovernmentAgent? GovernmentAgent { get; set; }
        public bool IsActive { get; set; }
    }
}
