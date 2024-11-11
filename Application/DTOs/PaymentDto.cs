using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class PaymentRequestModel
    {
        public decimal Amount { get; set; }
        public DateTime DateCreated { get; set; }
        public PaymentStatus Status { get; set; }
        public string ReferenceNumber { get; set; } = default!;
    }

    public class PaymentResponseModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public decimal Amount { get; set; }
        public DateTime DateCreated { get; set; }
        public PaymentStatus Status { get; set; }
        public string ReferenceNumber { get; set; } = default!;
        public bool IsActive { get; set; }
    }
}
