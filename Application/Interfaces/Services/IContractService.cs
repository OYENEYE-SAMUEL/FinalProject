using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IContractService
    {
        Task<BaseResponse<ContractResponseModel>> CreateContract(ContractRequestModel request);
        Task<BaseResponse<ContractResponseModel>> GetContract(Guid id);
        //Task<BaseResponse<ContractResponseModel>> TerminateContract(Guid id);
        Task<BaseResponse<ICollection<ContractResponseModel>>> GetAllActiveContract();
        Task<BaseResponse<ICollection<ContractResponseModel>>> GetAllContracts();
    }
}
