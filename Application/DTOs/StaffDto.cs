using Domain.Entities;
using Domain.Enum;

namespace Application.DTOs
{
    public class StaffRequestModel
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string? Address { get; set; }
        public Gender Gender { get; set; }
    }

    public class StaffResponseModel
    {
        public Guid Id { get; set; } 
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
