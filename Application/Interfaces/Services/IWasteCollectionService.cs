using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IWasteCollectionService
    {
        Task<BaseResponse<WasteCollectionResponseModel>> Create(WasteCollectionRequestModel request);
        Task<BaseResponse<WasteCollectionResponseModel>> GetById(Guid id);
        Task<BaseResponse<ICollection<WasteCollectionResponseModel>>> GetAll();
        Task<BaseResponse<ICollection<WasteCollectionResponseModel>>> AssignStaffs(Guid staffId);
        Task<BaseResponse<ICollection<WasteCollectionResponseModel>>> RequestAssignedToStaff(Guid staffId);
    }
}
