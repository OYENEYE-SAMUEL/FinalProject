using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<BaseResponse<PaymentResponseModel>> MakePayment(PaymentRequestModel request);
        Task<BaseResponse<PaymentResponseModel>> GetPaymentId(Guid paymentId);
        Task<BaseResponse<ICollection<PaymentResponseModel>>> GetAllFailedPayment();
        Task<BaseResponse<ICollection<PaymentResponseModel>>> GetAllSuccessPayment();
        Task<BaseResponse<ICollection<PaymentResponseModel>>> GetAllPendingPayment();
    }
}
