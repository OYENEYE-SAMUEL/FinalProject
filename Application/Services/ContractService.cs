using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using Domain.Enum;
using Mapster;

namespace Application.Services
{
    public class ContractService : IContractService
    {
        private readonly IContractRepository _contractRepo;
        private readonly ICurrentUser _currentUser;
        private readonly IUnitOfWork _unitOfWork;
        public ContractService(IContractRepository contractRepo, ICurrentUser currentUser, IUnitOfWork unitOfWork)
        {
            _contractRepo = contractRepo;
            _currentUser = currentUser;
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponse<ContractResponseModel>> CreateContract(ContractRequestModel request)
        {
            /*var user = _currentUser.GetCurrentUser();
            var isAuthorized = await _contractRepo.Check(e => e.GovernmentAgent.Email == user & e.IsActive.Equals(true));

            if (isAuthorized)
            {
                return new BaseResponse<ContractResponseModel>
                {
                    Data = null,
                    Message = "THere is an active contract already",
                    Status = false
                };
            }*/

            var durationDays = (request.EndDate - request.StartDate).Days;

            if (durationDays < 180)
            {
                return new BaseResponse<ContractResponseModel>
                {
                    Data = null,
                    Message = "The contract duration should be at least six months or more",
                    Status = false
                };
            }

            var endingDate = request.StartDate.AddDays(durationDays);
            var fund = durationDays * 1000;
            var contract = new Contract()
            {
                ProjectDescription = request.ProjectDescription,
                StartDate = request.StartDate,
                EndDate = endingDate,
                Status = request.EndDate <= DateTime.UtcNow ? ContractStatus.Completed
                : (request.EndDate > DateTime.UtcNow ? ContractStatus.Active : ContractStatus.Pending),
                IsActive = request.EndDate > DateTime.Now ? true : false,
                FundingAmount = fund,
                AuthorizedSignature = request.AuthorizedSignature
            };

            await _contractRepo.Create(contract);
            await _unitOfWork.Save();

            return new BaseResponse<ContractResponseModel>
            {
                Message = "Contract Successfully Created",
                Data = contract.Adapt<ContractResponseModel>(),
                Status = true
            };
        }

        //private decimal CalculateFundingAmount(ContractRequestModel request, decimal baseAmount)
        //{
        //    var duration = (request.EndDate -  request.StartDate).Days;

        //    if(duration < 180)
        //    {
               
        //    }
        //    return duration * baseAmount;

        //}

        //private string UploadSignature(ContractRequestModel request)
        //{

        //}


        public async Task<BaseResponse<ICollection<ContractResponseModel>>> GetAllActiveContract()
        {
            var contracts = await _contractRepo.GetAll();

            if (contracts == null)
            {
                return new BaseResponse<ICollection<ContractResponseModel>>
                {
                    Data = null,
                    Message = "Contracts Not Found",
                    Status = false
                };
            }
            var activeContracts = contracts
        .Where(e => e.Status == ContractStatus.Active && e.IsActive)
        .Select(e => e.Adapt<ContractResponseModel>())
        .ToList();
            return new BaseResponse<ICollection<ContractResponseModel>>
            {
                Status = true,
                Data = activeContracts
            };

        }

        public async Task<BaseResponse<ICollection<ContractResponseModel>>> GetAllContracts()
        {
            var contracts = await _contractRepo.GetAll();
            if (contracts == null)
            {
                return new BaseResponse<ICollection<ContractResponseModel>>
                {
                    Data = null,
                    Message = "Contracts Not Found",
                    Status = false
                }; 
            }

            var activeContracts = contracts.Where(e => e.IsActive);
            return new BaseResponse<ICollection<ContractResponseModel>>
            {
                Status = true,
                Data = activeContracts.Select(e => e.Adapt<ContractResponseModel>()).ToList()
            };
        }


        public async Task<BaseResponse<ContractResponseModel>> GetContract(Guid id)
        {
            var contract = await _contractRepo.Get(id);
            if (contract == null)
            {
                return new BaseResponse<ContractResponseModel>
                {
                    Status = false,
                    Data = null,
                    Message = "Contract Not Found"
                };
            }

            return new BaseResponse<ContractResponseModel>
            {
                Status = true,
                Data = contract.Adapt<ContractResponseModel>(),
            };
        }

        
    }
}
