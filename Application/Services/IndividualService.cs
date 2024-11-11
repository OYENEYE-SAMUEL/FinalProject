using Application.Constant;
using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Services
{
    public class IndividualService : IIndividualService
    {
        private readonly IIndividualRepository _individualRepo;
        private readonly IUserRepository _userRepo;
        private readonly IRoleRepository _roleRepo;
        private readonly IUnitOfWork _unitOfWork;
        public IndividualService(IIndividualRepository individualRepo, IUserRepository userRepo, IRoleRepository roleRepo, IUnitOfWork unitOfWork)
        {
            _individualRepo = individualRepo;
            _userRepo = userRepo;
            _roleRepo = roleRepo;
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponse<IndividualResponseModel>> Register(IndividualRequestModel request)
        {
            var exist = await _userRepo.Check(e => e.Email == request.Email);
            if (exist)
            {
                return new BaseResponse<IndividualResponseModel>
                {
                    Data = null,
                    Message = "User Already Exist with this Email",
                    Status = false
                };
            }

            if (!Regex.IsMatch(request.Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                return new BaseResponse<IndividualResponseModel>
                {
                    Message = "Invalid Email input",
                    Data = null,
                    Status = false
                };
            }

            var role = new Role()
            {
                Name = "Individual"
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

            /*var getRole = await _roleRepo.GetRole(RoleConstant.Individual);
            if (getRole == null)
            {
                return new BaseResponse<IndividualResponseModel>
                {
                    Data = null,
                    Message = "Role Not Found",
                    Status = false
                };
            }*/
            

            var person = new Individual()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                Email = request.Email,
                IsActive = true
            };

            await _userRepo.Create(user);
            await _roleRepo.Create(role);
            await _individualRepo.Create(person);
            await _unitOfWork.Save();

            return new BaseResponse<IndividualResponseModel>
            {
                Data = person.Adapt<IndividualResponseModel>(),
                Message = "Registration Successful",
                Status = true
            };
        }
        public async Task<BaseResponse<ICollection<IndividualResponseModel>>> GetAll()
        {
            var person = await _individualRepo.GetAll();
            if (person == null)
            {
                return new BaseResponse<ICollection<IndividualResponseModel>>
                {
                    Message = "Not Found",
                    Data = null,
                    Status = false
                };
            }

            return new BaseResponse<ICollection<IndividualResponseModel>>
            {
                Status = true,
                Data = person.Select(e => e.Adapt<IndividualResponseModel>()).ToList()
            };
        }

        public async Task<BaseResponse<IndividualResponseModel>> GetById(Guid id)
        {
            var person = await _individualRepo.Get(id);
            if (person == null)
            {
                return new BaseResponse<IndividualResponseModel>
                {
                    Data = null,
                    Message = "Not Found",
                    Status = false
                };
            }

            return new BaseResponse<IndividualResponseModel>
            {
                Status = true,
                Data = person.Adapt<IndividualResponseModel>()
            };
        }


        public async Task<BaseResponse<IndividualResponseModel>> UpdateProfile(Guid id, IndividualRequestModel request)
        {
            var person = await _individualRepo.Get(id);
            if (person == null)
            {
                return new BaseResponse<IndividualResponseModel>
                {
                    Data = null,
                    Message = "Not Found",
                    Status = false
                };
            }

            person.FirstName = request.FirstName;
            person.LastName = request.LastName;
            person.Email = request.Email;
            person.Gender = request.Gender;
            person.Address = request.Address;
            person.PhoneNumber = request.PhoneNumber;
            await _individualRepo.Update(person);
            await _unitOfWork.Save();

            return new BaseResponse<IndividualResponseModel>
            {
                Status = true,
                Data = person.Adapt<IndividualResponseModel>(),
                Message = "Updated Successfully"
            };
        }
    }
}
