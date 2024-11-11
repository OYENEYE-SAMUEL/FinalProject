using Domain.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ContractRequestModel
    {
        public string ProjectDescription { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string AuthorizedSignature { get; set; } = default!;

    }

    public class ContractResponseModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ProjectDescription { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal FundingAmount { get; set; }
        public ContractStatus Status { get; set; }
        public string AuthorizedSignature { get; set; } = default!;
        public bool IsActive { get; set; }
    }
}
