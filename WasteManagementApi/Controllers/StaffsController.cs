using Application.DTOs;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
namespace WasteManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffsController : ControllerBase
    {
        private readonly IStaffService _staffService;
        public StaffsController(IStaffService staffService)
        {
            _staffService = staffService;
        }
    
        [HttpPost("registerstaff")]
        public async Task<IActionResult> RegisterStaff(StaffRequestModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.Email))
            {
                return BadRequest(new { Message = "Invalid Registration request" });
            }

            var response = await _staffService.Register(model);
            if (!response.Status)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStaff(Guid id)
        {
            var staff = await _staffService.GetById(id);
            if (!staff.Status)
            {
                return BadRequest(staff.Message);
            }
            return Ok(staff.Data);
        }

        [HttpGet("allstaffs")]
        public async Task<IActionResult> GetAllStaff()
        {
            var staffs = await _staffService.GetAll();
            if (!staffs.Status)
            {
                return BadRequest(staffs.Message);
            }
            return Ok(staffs.Data);
        }

        /*[HttpGet("assigncollection")]
        public async Task<IActionResult> AssignCollection()*/
    }
}
