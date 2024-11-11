using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using Domain.Enum;
using Mapster;

namespace Application.Services
{
    public class WasteReportService : IWasteReportService
    {
        private readonly IWasteReportRepository _reportRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUser _currentUser;
        private readonly IWasteCollectionRepository _collection;
        private readonly ICommunityRepository _communityRepo;
        private readonly IIndividualRepository _individualRepo;
        private readonly IGovernmentAgentRepository _govtRepo;
        public WasteReportService(IWasteReportRepository reportRepo, IUnitOfWork unitOfWork, ICurrentUser currentUser, IWasteCollectionRepository collection, ICommunityRepository communityRepo, IIndividualRepository individualRepo, IGovernmentAgentRepository govtRepo)
        {
            _reportRepo = reportRepo;
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
            _collection = collection;
            _communityRepo = communityRepo;
            _individualRepo = individualRepo;
            _govtRepo = govtRepo;
        }
        public async Task<BaseResponse<WasteReportResponseModel>> Create(WasteReportRequestModel request)
        {
           /* var user = _currentUser.GetCurrentUser();
            var community = await _communityRepo.Get(e => e.Email == user);
            var individual = await _individualRepo.Get(e => e.Email == user);
            var govt = await _govtRepo.Get(e => e.Email == user);
            if (community == null && individual == null && govt == null)
            {
                return new BaseResponse<WasteReportResponseModel>
                {
                    Data = null,
                    Message = "User Not Found",
                    Status = false
                };
            }*/

          /*  IEnumerable<WasteCollection> wasteCollections = null;

            if (community != null)
                wasteCollections = community.WasteCollections.Where(e => e.CommunityId == community.Id);
            else if (individual != null)
                wasteCollections = individual.WasteCollections.Where(e => e.IndividualId == individual.Id);
            else if (govt != null)
                wasteCollections = govt.WasteCollections.Where(e => e.GovernmentAgentId == govt.Id);*/

            
          /*  if (wasteCollections == null || !wasteCollections.Any())
            {
                return new BaseResponse<WasteReportResponseModel>
                {
                    Data = null,
                    Message = "No associated waste collections found for the user.",
                    Status = false
                };
            }
          
                var reply = await _reportRepo.Check(e => wasteCollections.Any(w => w.Id == e.WasteCollectionId));
            if (reply)
            {
                return new BaseResponse<WasteReportResponseModel>
                {
                    Data = null,
                    Message = "Waste report cannot be initiated on empty waste requests.",
                    Status = false
                };
            }
*/

            var report = new WasteReport()
            {
                Description = request.Description,
                Title = request.Title,
                Status = ReportStatus.Pending,
                IsActive = true,
            };

            try
            {
                await _reportRepo.Create(report);
                await _unitOfWork.Save();
                return new BaseResponse<WasteReportResponseModel>
                {
                    Data = report.Adapt<WasteReportResponseModel>(),
                    Message = "Report Created Successfully",
                    Status = true
                };
            }

            catch (Exception ex)
            {
                return new BaseResponse<WasteReportResponseModel>
                {
                    Data = null,
                    Status = false,
                    Message = $"An error occurred: {ex.Message}"
                };
            }

        }

        public async Task<BaseResponse<WasteReportResponseModel>> Get(Guid id)
        {
           var report = await _reportRepo.Get(id);
            if(report == null)
            {
                return new BaseResponse<WasteReportResponseModel>
                {
                    Data = null,
                    Message = "Not Found",
                    Status = false
                };
            }

            return new BaseResponse<WasteReportResponseModel>
            {
                Status = true,
                Data = report.Adapt<WasteReportResponseModel>(),
            };
        }

        public async Task<BaseResponse<ICollection<WasteReportResponseModel>>> GetAll()
        {
            var report = await _reportRepo.GetAll();
            if (report == null)
            {
                return new BaseResponse<ICollection<WasteReportResponseModel>>
                {
                    Data = null,
                    Status = false,
                    Message = "Not Found"
                };
            }

            return new BaseResponse<ICollection<WasteReportResponseModel>>
            {
                Data = report.Select(e => e.Adapt<WasteReportResponseModel>()).ToList(),
                Status = true,
            };
        }

        public async Task<BaseResponse<WasteCollectionResponseModel>> PickStatus(Guid wasteId)
        {
            var report = await _reportRepo.Get(wasteId);
            if (report == null)
            {
                return new BaseResponse<WasteCollectionResponseModel>
                {
                    Status = false,
                    Data = null,
                    Message = "Waste not found"
                };
            }

            if(report.Status != ReportStatus.Pending)
            {
                return new BaseResponse<WasteCollectionResponseModel>
                {
                    Status = false,
                    Data = null,
                    Message = "Waste report cannot be picked up because it is not pending."
                };
            }
           
            
            report.Status = ReportStatus.PickedUp;
            await _reportRepo.Update(report);
            await _unitOfWork.Save();
            return new BaseResponse<WasteCollectionResponseModel>
            {
                Data = report.Adapt<WasteCollectionResponseModel>(),
                Status = true,
                Message = "Waste is Successfully Picked"
            };
        }
    }
}
