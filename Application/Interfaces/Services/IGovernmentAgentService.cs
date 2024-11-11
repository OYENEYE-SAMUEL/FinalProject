using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IGovernmentAgentService
    {
        Task<BaseResponse<GovernmentAgentResponseModel>> Register(GovernmentAgentRequestModel request);
        Task<BaseResponse<GovernmentAgentResponseModel>> Get(Guid id);
        Task<BaseResponse<GovernmentAgentResponseModel>> UpdateStatus(Guid id, GovernmentAgentRequestModel request);
        Task<BaseResponse<ICollection<GovernmentAgentResponseModel>>> GetAll();
    }
}
