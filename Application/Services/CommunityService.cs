using Application.Constant;
using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using Mapster;
using System.Text.RegularExpressions;

namespace Application.Services
{
    public class CommunityService : ICommunityService
    {
        private readonly ICommunityRepository _communityRepo;
        private readonly IUserRepository _userRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoleRepository _roleRepo;
        public CommunityService(ICommunityRepository communityRepo, IUserRepository userRepo, IUnitOfWork unitOfWork, IRoleRepository roleRepo)
        {
            _communityRepo = communityRepo;
            _userRepo = userRepo;
            _unitOfWork = unitOfWork;
            _roleRepo = roleRepo;
        }
        public async Task<BaseResponse<CommunityResponseModel>> Register(CommunityRequestModel request)
        {
            var exist = await _userRepo.Check(c => c.Email  == request.Email);
            if (exist)
            {
                return new BaseResponse<CommunityResponseModel>
                {
                    Data = null,
                    Message = "Community Already exist",
                    Status = false
                };
            }

            if (!Regex.IsMatch(request.Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                return new BaseResponse<CommunityResponseModel>
                {
                    Message = "Invalid Email input",
                    Data = null,
                    Status = false
                };
            }

            var role = new Role()
            {
                Name = "Community",
            };

           /* var getRole = await _roleRepo.GetRole(RoleConstant.Community);
            if (getRole == null)
            {
                return new BaseResponse<CommunityResponseModel>
                {
                    Data = null,
                    Message = "Role Not Found",
                    Status = false
                };
            }*/

            var salt = BCrypt.Net.BCrypt.GenerateSalt(20);
            var hashPassword = BCrypt.Net.BCrypt.HashPassword(request.Password, salt);
            var user = new User()
            {
                Email = request.Email,
                Password = hashPassword,
                FullName = request.Name,
                IsActive = true,
                Role = role
            };

         
            

            var community = new Community()
            {
                Name = request.Name,
                Email = request.Email,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber,
                TagNumber = Guid.NewGuid().ToString().Substring(2, 10),
                IsActive = true,
                DateCreated = DateTime.UtcNow,
            };

            await _userRepo.Create(user);
            await _roleRepo.Create(role);
            await _communityRepo.Create(community);
            await _unitOfWork.Save();
            return new BaseResponse<CommunityResponseModel>
            {
                Message = "Registration Successful",
                Status = true,
                Data = community.Adapt<CommunityResponseModel>()
            };
        }
        public async Task<BaseResponse<ICollection<CommunityResponseModel>>> GetAll()
        {
            var community = await _communityRepo.GetAll();
            if (community == null)
            {
                return new BaseResponse<ICollection<CommunityResponseModel>>
                {
                    Data = null,
                    Message = "Not Found",
                    Status = false,
                };
            }

            return new BaseResponse<ICollection<CommunityResponseModel>>
            {
                Status = true,
                Data = community.Select(c => c.Adapt<CommunityResponseModel>()).ToList()
            };
        }

        public async Task<BaseResponse<CommunityResponseModel>> GetById(Guid id)
        {
            var community = await _communityRepo.Get(id);
            if (community == null)
            {
                return new BaseResponse<CommunityResponseModel>
                {
                    Data = null,
                    Message = "Not Found",
                    Status = false,
                };
            }

            return new BaseResponse<CommunityResponseModel>
            {
                Status = true,
                Data = community.Adapt<CommunityResponseModel>()
            };
        }

    }
}
