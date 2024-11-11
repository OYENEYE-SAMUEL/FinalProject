using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class GovernmentAgentRequestModel
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string SectorName { get; set; } = default!;
        public string StateName { get; set; } = default!;
    }

    public class GovernmentAgentResponseModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = default!;
        public string SectorName { get; set; } = default!;
        public string StateName { get; set; } = default!;
        public ICollection<WasteCollection> WasteCollections { get; set; }
            = new HashSet<WasteCollection>();
        public ICollection<Contract> Contracts { get; set; } = new HashSet<Contract>();
        public DateTime DateCreated { get; set; }
        public bool IsActive { get; set; }
    }
}
