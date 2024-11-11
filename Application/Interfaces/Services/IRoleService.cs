using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IRoleService
    {
        Task<BaseResponse<RoleResponseModel>> CreateRole(RoleRequestModel request);
        Task<BaseResponse<RoleResponseModel>> GetRoleByName(string name);
        Task<BaseResponse<RoleResponseModel>> GetRoleName(string name);
        Task<BaseResponse<ICollection<RoleResponseModel>>> GetAll();
    }
}
