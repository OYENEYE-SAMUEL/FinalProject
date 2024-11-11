using Domain.Enum;

namespace Domain.Entities
{
    public class Staff
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Email { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string? Address { get; set; }
        public Gender Gender { get; set; }
        public string StaffNumber { get; set; } = default!;
        public ICollection<WasteCollection> WasteCollections { get; set; } 
            = new HashSet<WasteCollection>();
        public bool IsActive { get; set; }
    }
}
