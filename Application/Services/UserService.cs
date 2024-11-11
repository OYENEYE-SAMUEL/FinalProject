using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Mapster;
using System.Reflection.Metadata.Ecma335;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        public UserService(IUserRepository userRepo, IUnitOfWork unitOfWork, IAuthService authService)
        {
            _userRepo = userRepo;
            _unitOfWork = unitOfWork;
            _authService = authService;
        }
        public async Task<BaseResponse<UserResponseModel>> Login(UserRequestModel request)
        {
            var exist = await _userRepo.Check(a => a.Email == null);
            if(exist)
            {
                return new BaseResponse<UserResponseModel>
                {
                    Data = null,
                    Message = "User Not Found",
                    Status = false
                };

            }
            var user = await _userRepo.Get(request.Email);
            var hashPassword = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
            if(!hashPassword)
            {
                return new BaseResponse<UserResponseModel>
                {
                    Message = "Invalid Credentials",
                    Status = false
                };
            }

            var token = _authService.GenerateToken(user);
            return new BaseResponse<UserResponseModel>
            {
                Message = "Login Successfully",
                Status = true,
                Data = new UserResponseModel
                {
                    Email = user.Email,
                    Password = user.Password,
                    FullName = user.FullName,
                    Role = user.Role,
                    IsActive = true,
                    Token = token
                }
            };
        }
    }
}
