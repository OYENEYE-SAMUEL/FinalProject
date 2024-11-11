using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface ISubscrptionService
    {
        Task<BaseResponse<SubscriptionResponseModel>> CreatePlan(SubscriptionRequestModel request);
        Task<BaseResponse<SubscriptionResponseModel>> UpdatePlan(SubscriptionRequestModel request);
        Task<BaseResponse<SubscriptionResponseModel>> GetActivePlan(Guid id);
        Task<BaseResponse<ICollection<SubscriptionResponseModel>>> AllExpiredPlan();
        Task<BaseResponse<ICollection<SubscriptionResponseModel>>> GetAllPlan();

    }
}
