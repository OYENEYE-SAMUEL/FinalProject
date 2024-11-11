using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CommunityRequestModel
    {
        public string Name { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;

    }

    public class CommunityResponseModel
    {
        public Guid Id { get; set; } 
        public string Name { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string TagNumber { get; set; } = default!;
        public ICollection<WasteCollection> WasteCollections { get; set; }
            = new HashSet<WasteCollection>();
        public ICollection<WasteReport> WasteReports { get; set; } = new HashSet<WasteReport>();
        public Guid SubscriptionId { get; set; }
        public Subscription Subscription { get; set; } = default!;
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
