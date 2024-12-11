using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class WasteCollectionRequestModel
    {
        public string Location { get; set; } = default!;
    }

    public class WasteCollectionResponseModel
    {
        public Guid Id { get; set; } 
        public string Location { get; set; } = default!;
        public DateTime DateCreated { get; set; }
        public WasteReport WasteReport { get; set; }
        public Guid StaffId { get; set; }
        public Staff Staff { get; set; }
        public Guid CommunityId { get; set; }
        public Community Community { get; set; }
        public Guid GovernmentAgentId { get; set; }
        public GovernmentAgent GovernmentAgent { get; set; }
        public bool IsActive { get; set; }
    }

	}
