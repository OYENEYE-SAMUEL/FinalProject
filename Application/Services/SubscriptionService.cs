using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using Domain.Enum;
using Mapster;

namespace Application.Services
{
    public class SubscriptionService : ISubscrptionService
    {
        private readonly ISubscriptionRepository _subscribeRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommunityRepository _communityRepo;
        private readonly IIndividualRepository _individualRepo;
        private readonly ICurrentUser _currentUser;
        public SubscriptionService(ISubscriptionRepository subscribeRepo, IUnitOfWork unitOfWork, ICommunityRepository communityRepo, IIndividualRepository individualRepo, ICurrentUser currentUser)
        {
            _subscribeRepo = subscribeRepo;
            _unitOfWork = unitOfWork;
            _communityRepo = communityRepo;
            _individualRepo = individualRepo;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<ICollection<SubscriptionResponseModel>>> AllExpiredPlan()
        {
            var subscribe = await _subscribeRepo.GetAll();
            if (subscribe == null)
            {
                return new BaseResponse<ICollection<SubscriptionResponseModel>>
                {
                    Status = false,
                    Data = null,
                    Message = "No Plan to Show"
                };
            }
            var expiredPlan = subscribe.Select(e => e.IsActive == false && e.CurrentPlanDateEnd == DateTime.UtcNow);
            return new BaseResponse<ICollection<SubscriptionResponseModel>>
            {
                Status = true,
                Data = expiredPlan.Select(e => e.Adapt<SubscriptionResponseModel>()).ToList()

            };

        }

        public async Task<BaseResponse<SubscriptionResponseModel>> CreatePlan(SubscriptionRequestModel request)
        {
           /* var user = _currentUser.GetCurrentUser();
            if (user == null)
            {
                return new BaseResponse<SubscriptionResponseModel>
                {
                    Data = null,
                    Status = false,
                    Message = "User Not Found"
                };
            }*/
            var exist = await _subscribeRepo.Check(e => e.IsActive == true);

            if (exist)
            {
                    return new BaseResponse<SubscriptionResponseModel>
                {
                    Message = "There is an active plan presently",
                    Data = null,
                    Status = false
                };
            }

            DateTime expirationDate = request.CurrentPlanType switch
            {
                PlanType.Basic => DateTime.UtcNow.AddDays(7),      
                PlanType.Standard => DateTime.UtcNow.AddMonths(3), 
                PlanType.Premium => DateTime.UtcNow.AddYears(1),   
                _ => DateTime.UtcNow
            };

            int totalDays = (expirationDate - request.CurrentPlanDateStart).Days;
           /* decimal dailyPrice = request.Amount / totalDays;*/


            var subscribe = new Subscription()
            {
                AmountPaid = request.Amount,
                CurrentPlanDateStart = request.CurrentPlanDateStart,
                CurrentPlanDateEnd = expirationDate,
                CurrentPlanType = request.CurrentPlanType,
                Frequency = request.Frequency,
                Price = request.Amount * totalDays,
            };

            await _subscribeRepo.Create(subscribe);
            await _unitOfWork.Save();
            return new BaseResponse<SubscriptionResponseModel>
            {
                Message = "Subscription created successfully",
                Data = subscribe.Adapt<SubscriptionResponseModel>(),
                Status = true
            };


        }

        public async Task<BaseResponse<SubscriptionResponseModel>> GetActivePlan(Guid id)
        {
            var subscribe = await _subscribeRepo.Get(id);
            if(subscribe == null && !subscribe.IsActive)
            {
                return new BaseResponse<SubscriptionResponseModel>
                {
                    Status = false,
                    Data = null,
                    Message = "There is no Active Plan Presently"
                };
               
            }
            return new BaseResponse<SubscriptionResponseModel>
            {
                Status = true,
                Data = subscribe.Adapt<SubscriptionResponseModel>(),
            };
        }

        public async Task<BaseResponse<ICollection<SubscriptionResponseModel>>> GetAllPlan()
        {
            var subscribe = await _subscribeRepo.GetAll();
            if (subscribe == null)
            {
                return new BaseResponse<ICollection<SubscriptionResponseModel>>
                {
                    Data = null,
                    Message = "Not Found",
                    Status = false
                };
            }

            return new BaseResponse<ICollection<SubscriptionResponseModel>>
            {
                Data = subscribe.Select(e => e.Adapt<SubscriptionResponseModel>()).ToList(),
                Status = true,
            };
        }

        public Task<BaseResponse<SubscriptionResponseModel>> UpdatePlan(SubscriptionRequestModel request)
        {
            throw new NotImplementedException();
        }
    }
}
