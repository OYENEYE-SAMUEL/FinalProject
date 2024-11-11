using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using Mapster;
using System.Collections.ObjectModel;

namespace Application.Services
{
    public class WasteCollecitonService : IWasteCollectionService
    {
        private readonly IWasteCollectionRepository _wasteRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStaffRepository _staffRepo;
        private readonly ICurrentUser _currentUser;
        public WasteCollecitonService(IWasteCollectionRepository wasteRepo, IUnitOfWork unitOfWork, IStaffRepository staffRepo, ICurrentUser currentUser)
        {
            _wasteRepo = wasteRepo;
            _unitOfWork = unitOfWork;
            _staffRepo = staffRepo;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<ICollection<WasteCollectionResponseModel>>> AssignStaffs(Guid staffId)
        {
            var collections = await _wasteRepo.GetAll();
            if (collections == null)
            {
                return new BaseResponse<ICollection<WasteCollectionResponseModel>>
                {
                    Data = null,
                    Message = "Not Found",
                    Status = false
                };
            }
            var staffAssign = collections.Where(e => e.StaffId == staffId && e.IsActive == true);

            if (staffAssign == null)
            {
                return new BaseResponse<ICollection<WasteCollectionResponseModel>>
                {
                    Message = "There is no Collection assign to this Staff",
                    Data = null,
                    Status = false
                };
            }

            return new BaseResponse<ICollection<WasteCollectionResponseModel>>
            {
                Data = staffAssign.Select(r => r.Adapt<WasteCollectionResponseModel>()).ToList(),
                Status = true
            };

        }

        public async Task<BaseResponse<WasteCollectionResponseModel>> Create(WasteCollectionRequestModel request)
        {

            var user = _currentUser.GetCurrentUser();
            if (user == null)
            {
                return new BaseResponse<WasteCollectionResponseModel>
                {
                    Data = null,
                    Status = false,
                    Message = "User Not Found"
                };
            }

           
            var isAuthorized = await _wasteRepo.Check(e => e.Community == null ? e.Community.Email != user : e.GovernmentAgent.Email != user);
            if (isAuthorized)
            {
                return new BaseResponse<WasteCollectionResponseModel>
                {
                    Data = null,
                    Message = "Only authorized users can create reports",
                    Status = false
                };
            }

            var waste = new WasteCollection()
            {
                
                Location = request.Location,
                DateCreated = DateTime.UtcNow,
                IsActive = true,
                
            };
            await _wasteRepo.Create(waste);
            await _unitOfWork.Save();

            return new BaseResponse<WasteCollectionResponseModel>
            {
                Message = "Created Successfully",
                Data = waste.Adapt<WasteCollectionResponseModel>(),
                Status = true
            };

        }

        public async Task<BaseResponse<ICollection<WasteCollectionResponseModel>>> GetAll()
        {
            var waste = await _wasteRepo.GetAll();
            if (waste == null)
            {
                return new BaseResponse<ICollection<WasteCollectionResponseModel>>
                {
                    Status = false,
                    Data = null,
                    Message = "Not Found"
                };
            }

            return new BaseResponse<ICollection<WasteCollectionResponseModel>>
            {
                Data = waste.Select(e => e.Adapt<WasteCollectionResponseModel>()).ToList(),
                Status = true
            };
        }

        public async Task<BaseResponse<WasteCollectionResponseModel>> GetById(Guid id)
        {
            var waste = await _wasteRepo.Get(id);
            if (waste == null)
            {
                return new BaseResponse<WasteCollectionResponseModel>
                {
                    Status = false,
                    Data = null,
                    Message = "Not Found",
                };

            }

            return new BaseResponse<WasteCollectionResponseModel>
            {
                Data = waste.Adapt<WasteCollectionResponseModel>(),
                Status = true
            };
        }

        public async Task<BaseResponse<ICollection<WasteCollectionResponseModel>>> RequestAssignedToStaff(Guid staffId)
        {
            var staffs = await _staffRepo.Get(staffId);
            if (staffs == null || !staffs.IsActive)
            {
                return new BaseResponse<ICollection<WasteCollectionResponseModel>>
                {
                    Status = false,
                    Data = null,
                    Message = "Staff Not Found"
                };
            }

            var collections = await _wasteRepo.GetAll();
            if (collections == null || !collections.Any())
            {
                return new BaseResponse<ICollection<WasteCollectionResponseModel>>
                {
                    Status = false,
                    Message = "No waste collections available",
                    Data = null
                };
            }

            foreach (var item in collections)
            {
                item.StaffId = staffs.Id;
                item.IsActive = true;
                await _wasteRepo.Update(item);
            }

            await _staffRepo.Update(staffs);

            var assignedCollections = collections.Select(wasteCollection => new WasteCollectionResponseModel
            {
                Location = wasteCollection.Location,
                StaffId = staffs.Id,
                IsActive = wasteCollection.IsActive

            }).ToList();


            return new BaseResponse<ICollection<WasteCollectionResponseModel>>
            {
                Status = true,
                Message = "Waste collections assigned to staff successfully.",
                Data = assignedCollections
            };
        }
    }
}
