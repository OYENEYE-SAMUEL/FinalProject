using Application.Constant;
using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using Mapster;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace Application.Services
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepo;
        private readonly IRoleRepository _roleRepo;
        public StaffService(IStaffRepository staffRepo, IUnitOfWork unitOfWork, IUserRepository userRepo, IRoleRepository roleRepo)
        {
            _staffRepo = staffRepo;
            _unitOfWork = unitOfWork;
            _userRepo = userRepo;
            _roleRepo = roleRepo;
        }
        public async Task<BaseResponse<StaffResponseModel>> Register(StaffRequestModel request)
        {
            var exist = await _userRepo.Check(e => e.Email == request.Email);
            if (exist)
            {
                return new BaseResponse<StaffResponseModel>
                {
                    Data = null,
                    Message = $"Staff with this Email already exist",
                    Status = false
                };
            }

            if (!Regex.IsMatch(request.Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                return new BaseResponse<StaffResponseModel>
                {
                    Message = "Invalid Email input",
                    Data = null,
                    Status = false
                };
            }

            var role = new Role()
            {
                Name = "Staff",
                IsActive = true,
                DateCreated = DateTime.UtcNow
            };

            var salt = BCrypt.Net.BCrypt.GenerateSalt(20);
            var hashPassword = BCrypt.Net.BCrypt.HashPassword(request.Password, salt);

            var user = new User()
            {
                Email = request.Email,
                Password = hashPassword,
                FullName = $"{request.FirstName} {request.LastName}",
                IsActive = true,
                Role = role

            };

            /*var getRole = await _roleRepo.GetRole(RoleConstant.WasteTaker);
            if (getRole == null)
            {
                return new BaseResponse<StaffResponseModel>
                {
                    Data = null,
                    Message = "Role Not Found",
                    Status = false
                };
            }*/

           

            var staff = new Staff()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Address = request.Address,
                Gender = request.Gender,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                StaffNumber = Guid.NewGuid().ToString().Substring(3, 6),
                IsActive = true,
            };

            if(_roleRepo == null || _staffRepo == null || _unitOfWork == null)
            {
                throw new InvalidOperationException("Repositories or UnitOfWork are not properly initialized");
            }

            await _userRepo.Create(user);
            await _roleRepo.Create(role);
            await _staffRepo.Create(staff);
            await _unitOfWork.Save();

            return new BaseResponse<StaffResponseModel>
            {
                Message = "Staff Registered Successfully",
                Status = true
            };
        }

        public async Task<BaseResponse<ICollection<StaffResponseModel>>> GetAll()
        {
            var staffs = await _staffRepo.GetAll();
            if (staffs == null)
            {
                return new BaseResponse<ICollection<StaffResponseModel>>
                {
                    Status = false,
                    Data = null,
                    Message = "Not Found"
                };
                
            }
            var listOfStaff = staffs.Select(e => new StaffResponseModel
            {
                FirstName = e.FirstName,
                LastName = e.LastName,
                Address = e.Address,
                Email = e.Email,
                Gender = e.Gender,
                PhoneNumber = e.PhoneNumber,
                StaffNumber = e.StaffNumber

            }).ToList();

            return new BaseResponse<ICollection<StaffResponseModel>>
            {
                Status = true,
                Data = listOfStaff
            };
        }

        public async Task<BaseResponse<StaffResponseModel>> GetById(Guid id)
        {
            var staff = await _staffRepo.Get(id);
            if (staff == null)
            {
                return new BaseResponse<StaffResponseModel>
                {
                    Status = false,
                    Data = null,
                    Message = "Staff Not Found"
                };
            }

            return new BaseResponse<StaffResponseModel>
            {
                Data = staff.Adapt<StaffResponseModel>(),
                Status = true,
            };
        }

        public async Task<BaseResponse<StaffResponseModel>> UpdateProfile(Guid id, StaffRequestModel request)
        {
            var staff = await _staffRepo.Get(id);
            if (staff == null)
            {
                return new BaseResponse<StaffResponseModel>
                {
                    Data = null,
                    Message = "Not Found",
                    Status = false
                };
            }

            staff.FirstName = request.FirstName;
            staff.LastName = request.LastName;
            staff.Email = request.Email;
            staff.Gender = request.Gender;
            staff.PhoneNumber = request.PhoneNumber;
            staff.Address = request.Address;
            await _staffRepo.Update(staff);
            await _unitOfWork.Save();

            return new BaseResponse<StaffResponseModel>
            {
                Message = "Updated Successfully",
                Data = staff.Adapt<StaffResponseModel>(),
                Status = true
            };
        }
    }
}
