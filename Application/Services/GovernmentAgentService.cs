using Application.Constant;
using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using Mapster;
using System.Text.RegularExpressions;

namespace Application.Services
{
    public class GovernmentAgentService : IGovernmentAgentService
    {
        private readonly IGovernmentAgentRepository _govtAgentRepo;
        private readonly IUserRepository _userRepo;
        private readonly IRoleRepository _roleRepo;
        private readonly IUnitOfWork _unitOfWork;
        public GovernmentAgentService(IGovernmentAgentRepository govtAgentRepo, IUserRepository userRepo, IRoleRepository roleRepo, IUnitOfWork unitOfWork)
        {
            _govtAgentRepo = govtAgentRepo;
            _userRepo = userRepo;
            _roleRepo = roleRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<GovernmentAgentResponseModel>> Register(GovernmentAgentRequestModel request)
        {
            var exist = await _userRepo.Check(r  => r.Email == request.Email);
            if (exist)
            {
                return new BaseResponse<GovernmentAgentResponseModel>
                {
                    Data = null,
                    Message = "User Already Exist",
                    Status = false
                };
            }

            if (!Regex.IsMatch(request.Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                return new BaseResponse<GovernmentAgentResponseModel>
                {
                    Message = "Invalid Email input",
                    Data = null,
                    Status = false
                };
            }

            var role = new Role()
            {
                Name = "GovernmentAgent"
            };

            var salt = BCrypt.Net.BCrypt.GenerateSalt(20);
            var hashPassword = BCrypt.Net.BCrypt.HashPassword(request.Password, salt);

            var user = new User()
            {
                Email = request.Email,
                Password = hashPassword,
                FullName = request.StateName,
                IsActive = true,
                Role = role
            };

            /*  var getRole = await _roleRepo.GetRole(RoleConstant.GovernmentAgent);
              if (getRole == null)
              {
                  return new BaseResponse<GovernmentAgentResponseModel>
                  {
                      Data = null,
                      Message = "Role Not Found",
                      Status = false
                  };
              }*/



            var govt = new GovernmentAgent()
            {
                StateName = request.StateName,
                SectorName = request.SectorName,
                Email = request.Email,
                IsActive = true,
                DateCreated = DateTime.UtcNow
            };

            await _userRepo.Create(user);
            await _roleRepo.Create(role);
            await _govtAgentRepo.Create(govt);
            await _unitOfWork.Save();

            return new BaseResponse<GovernmentAgentResponseModel>
            {
                Data = govt.Adapt<GovernmentAgentResponseModel>(),
                Message = "Registration Successful",
                Status = true
            };
        }


        public async Task<BaseResponse<GovernmentAgentResponseModel>> Get(Guid id)
        {
            var govt = await _govtAgentRepo.Get(id);
            if (govt == null)
            {
                return new BaseResponse<GovernmentAgentResponseModel>
                {
                    Status = false,
                    Data = null,
                    Message = "Not Found"
                };
            }

            return new BaseResponse<GovernmentAgentResponseModel>
            {
                Data = govt.Adapt<GovernmentAgentResponseModel>(),
                Status = true,
            };
        }

        public async Task<BaseResponse<ICollection<GovernmentAgentResponseModel>>> GetAll()
        {
            var govts = await _govtAgentRepo.GetAll();
            if (govts == null)
            {
                return new BaseResponse<ICollection<GovernmentAgentResponseModel>>
                {
                    Status = false,
                    Data = null,
                    Message = "Not Found"
                };
            }

            return new BaseResponse<ICollection<GovernmentAgentResponseModel>>
            {
                Data = govts.Select(g => g.Adapt<GovernmentAgentResponseModel>()).ToList(),
                Status = true,
            };
        }


        public Task<BaseResponse<GovernmentAgentResponseModel>> UpdateStatus(Guid id, GovernmentAgentRequestModel request)
        {
            throw new NotImplementedException();
        }
    }
}
