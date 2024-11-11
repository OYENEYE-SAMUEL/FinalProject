using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using Domain.Enum;
using Mapster;
using Microsoft.Extensions.Configuration;
using PayStack.Net;

namespace Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _payment;
        private readonly ICurrentUser _currentUser;
        private readonly ICommunityRepository _communityRepo;
        private readonly ISubscriptionRepository _subscribeRepo;
        private readonly IIndividualRepository _individualRepo;
        private readonly IPayStackApi _payStack;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly string Token;


        private PayStackApi PayStack { get; set; }
        public PaymentService(IPaymentRepository payment, ICurrentUser currentUser, ICommunityRepository communityRepo, ISubscriptionRepository subscribeRepo, IIndividualRepository individualRepo, IPayStackApi payStack, IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _payment = payment;
            _currentUser = currentUser;
            _subscribeRepo = subscribeRepo;
            _communityRepo = communityRepo;
            _individualRepo = individualRepo;
            _payStack = payStack;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            Token = _configuration["PayStack:SecretKey"];
            PayStack = new PayStackApi(Token);
        }
        public async Task<BaseResponse<PaymentResponseModel>> MakePayment(PaymentRequestModel request)
        {
            var user = _currentUser.GetCurrentUser();
            var community = await _communityRepo.Get(e => e.Email == user);
            var individual = await _individualRepo.Get(e => e.Email == user);
            if (community == null || individual == null)
            {
                return new BaseResponse<PaymentResponseModel>
                {
                    Data = null,
                    Message = "User Not Found",
                    Status = false
                };
            }


            var subscribe = community?.Subscription ?? individual?.Subscription;
            var exist = await _payment.Check(e => e.SubscriptionId != subscribe.Id);
            if(exist)
            {
                return new BaseResponse<PaymentResponseModel>
                {
                    Data = null,
                    Message = "Payment cannot be initialize",
                    Status = false
                };
            }

           /* var subscribe = await _subscribeRepo.Get(e => e.Id == community.SubscriptionId 
            || e.Id == individual.SubscriptionId);
            if (subscribe == null)
            {
                return new BaseResponse<PaymentResponseModel>
                {
                    Data = null,
                    Message = "There is No Plan to make payment for",
                    Status = false
                };
            }*/

            var payment = new Payment
            {
                Amount = subscribe.Price,
                DateCreated = DateTime.UtcNow,
                ReferenceNumber = Guid.NewGuid().ToString().Substring(10),
                Status = PaymentStatus.Pending
            };
            await _payment.Create(payment);

            try
            {
                TransactionInitializeRequest transaction = new()
                {
                    AmountInKobo = (int)(subscribe.Price * 100),
                    Email = community?.Email ?? individual?.Email,
                    Reference = payment.ReferenceNumber,
                    Currency = "NGN",
                };

                TransactionInitializeResponse response = PayStack.Transactions.Initialize(transaction);
                if (!response.Status)
                {
                    return new BaseResponse<PaymentResponseModel>
                    {
                        Data = null,
                        Message = "Transaction Failed",
                        Status = false
                    };
                }
                payment.Status = PaymentStatus.Successful;
                await _payment.Update(payment);
                subscribe.IsActive = true;
                await _subscribeRepo.Update(subscribe);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                return new BaseResponse<PaymentResponseModel>
                {
                    Data = null,
                    Status = false,
                    Message = $"Transaction failed {ex.Message}"
                };
            }
            return new BaseResponse<PaymentResponseModel>
            {
                Message = "Payment Successful",
                Status = true,
                Data = payment.Adapt<PaymentResponseModel>()
            };


        }

        public async Task<BaseResponse<ICollection<PaymentResponseModel>>> GetAllFailedPayment()
        {
            var getPayment = await _payment.GetAllPayment();
           var failPay =  getPayment.Where(e => e.Status == PaymentStatus.Failed);
            if (failPay == null)
            {
                return new BaseResponse<ICollection<PaymentResponseModel>>
                {
                    Data = null,
                    Status = false,
                    Message = "No Failed Payment"
                };

            }

            return new BaseResponse<ICollection<PaymentResponseModel>>
            {
                Status = true,
                Data = failPay.Select(x => x.Adapt<PaymentResponseModel>()).ToList(),
            };
        }

        public async Task<BaseResponse<ICollection<PaymentResponseModel>>> GetAllPendingPayment()
        {
            var getPayment = await _payment.GetAllPayment();
            var failPay = getPayment.Where(e => e.Status == PaymentStatus.Pending);
            if (failPay == null)
            {
                return new BaseResponse<ICollection<PaymentResponseModel>>
                {
                    Data = null,
                    Status = false,
                    Message = "No Failed Payment"
                };

            }


            return new BaseResponse<ICollection<PaymentResponseModel>>
            {
                Status = true,
                Data = failPay.Select(x => x.Adapt<PaymentResponseModel>()).ToList(),
            };
        }

        public async Task<BaseResponse<ICollection<PaymentResponseModel>>> GetAllSuccessPayment()
        {
            var getPayment = await _payment.GetAllPayment();
            var failPay = getPayment.Where(e => e.Status == PaymentStatus.Successful);
            if (failPay == null)
            {
                return new BaseResponse<ICollection<PaymentResponseModel>>
                {
                    Data = null,
                    Status = false,
                    Message = "No Failed Payment"
                };

            }


            return new BaseResponse<ICollection<PaymentResponseModel>>
            {
                Status = true,
                Data = failPay.Select(x => x.Adapt<PaymentResponseModel>()).ToList(),
            };
        }

        public async Task<BaseResponse<PaymentResponseModel>> GetPaymentId(Guid paymentId)
        {
            var payment = await _payment.Get(paymentId);
            if (payment == null)
            {
                return new BaseResponse<PaymentResponseModel>
                {
                    Data = null,
                    Message = "No Payment is Found",
                    Status = false
                };
            }

            return new BaseResponse<PaymentResponseModel>
            {
                Status = true,
                Data = payment.Adapt<PaymentResponseModel>()
            };
        }

       
    }
}
