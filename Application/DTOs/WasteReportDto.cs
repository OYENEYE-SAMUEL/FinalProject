using Domain.Entities;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class WasteReportRequestModel
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
    }

    public class WasteReportResponseModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public ReportStatus Status { get; set; }
        public Guid WasteCollectionId { get; set; }
        public WasteCollection WasteCollection { get; set; }
        public Guid CommunityId { get; set; }
        public Community Community { get; set; }
        public Guid GovernmentAgentId { get; set; }
        public GovernmentAgent GovernmentAgent { get; set; }
        public bool IsActive { get; set; }
    }
}
