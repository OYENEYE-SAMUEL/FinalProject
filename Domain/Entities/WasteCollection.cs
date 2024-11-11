using Domain.Enum;

namespace Domain.Entities
{
    public class WasteCollection
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Location { get; set; } = default!;
        public DateTime DateCreated { get; set; }
        public WasteReport? WasteReport { get; set; }
        public Guid? StaffId { get; set; }
        public Staff? Staff { get; set; }
        public Guid? CommunityId { get; set; }
        public Community? Community { get; set; }
        public Guid? GovernmentAgentId { get; set; }
        public GovernmentAgent? GovernmentAgent { get; set; }
        public Guid? IndividualId { get; set; }
        public Individual? Individual { get; set; }
        public bool IsActive { get; set; }
    }
}
