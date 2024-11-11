using Application.DTOs;

namespace Application.Interfaces.Services
{
    public interface IStaffService
    {
        Task<BaseResponse<StaffResponseModel>> Register(StaffRequestModel request);
        Task<BaseResponse<StaffResponseModel>> GetById(Guid id);
        Task<BaseResponse<StaffResponseModel>> UpdateProfile(Guid id, StaffRequestModel request);
        Task<BaseResponse<ICollection<StaffResponseModel>>> GetAll();
    }
}
