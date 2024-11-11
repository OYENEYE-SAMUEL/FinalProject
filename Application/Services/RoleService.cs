using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using Mapster;

namespace Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepo;
        private readonly IUnitOfWork _unitOfWork;
        public RoleService(IRoleRepository roleRepo, IUnitOfWork unitOfWork)
        {
            _roleRepo = roleRepo;
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponse<RoleResponseModel>> CreateRole(RoleRequestModel request)
        {
            var exist = await _roleRepo.Check(a => a.Name == request.Name);
            if (exist)
            {
                return new BaseResponse<RoleResponseModel>
                {
                    Data = null,
                    Message = "Role already exist",
                    Status = false
                };
            }
            var role = new Role
            {
                Name = request.Name,
                DateCreated = DateTime.UtcNow,
                IsActive = true
            };
            await _roleRepo.Create(role);
            await _unitOfWork.Save();
            return new BaseResponse<RoleResponseModel>
            {
                Message = "Role created Successfully",
                Status = true,
                Data = role.Adapt<RoleResponseModel>()
            };
        }

        public async Task<BaseResponse<ICollection<RoleResponseModel>>> GetAll()
        {
            var roles = await _roleRepo.GetAll();
            if (roles == null)
            {
                return new BaseResponse<ICollection<RoleResponseModel>>
                {
                    Data = null,
                    Message = "Roles Not Found",
                    Status = false
                };
            }

            return new BaseResponse<ICollection<RoleResponseModel>>
            {
                Data = roles.Select(a => a.Adapt<RoleResponseModel>()).ToList(),
                Status = true
            };
        }

        public async Task<BaseResponse<RoleResponseModel>> GetRoleName(string name)
        {
            var role = await _roleRepo.GetRole(name);
            if(role == null)
            {
                return new BaseResponse<RoleResponseModel>
                {
                    Data = null,
                    Message = "Role Not Found",
                    Status = false
                };
            }

            return new BaseResponse<RoleResponseModel>
            {
                Data = role.Adapt<RoleResponseModel>(),
                Status = true,
            };
        }

        public Task<BaseResponse<RoleResponseModel>> GetRoleByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
