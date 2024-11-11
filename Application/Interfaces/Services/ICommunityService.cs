using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface ICommunityService
    {
        Task<BaseResponse<CommunityResponseModel>> Register(CommunityRequestModel request);
        Task<BaseResponse<CommunityResponseModel>> GetById(Guid id);
        Task<BaseResponse<ICollection<CommunityResponseModel>>> GetAll();
    }
}
