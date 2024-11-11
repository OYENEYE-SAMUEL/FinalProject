using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IIndividualService
    {
        Task<BaseResponse<IndividualResponseModel>> Register(IndividualRequestModel request);
        Task<BaseResponse<IndividualResponseModel>> GetById(Guid id);
        Task<BaseResponse<ICollection<IndividualResponseModel>>> GetAll();
        Task<BaseResponse<IndividualResponseModel>> UpdateProfile(Guid id, IndividualRequestModel request);
    }
}
