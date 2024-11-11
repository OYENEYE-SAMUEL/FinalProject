using Application.DTOs;

namespace Application.Interfaces.Services
{
    public interface IWasteReportService
    {
        Task<BaseResponse<WasteReportResponseModel>> Create(WasteReportRequestModel request);
        Task<BaseResponse<WasteReportResponseModel>> Get(Guid id);
        Task<BaseResponse<ICollection<WasteReportResponseModel>>> GetAll();
        Task<BaseResponse<WasteCollectionResponseModel>> PickStatus(Guid wasteId);
    }
}
