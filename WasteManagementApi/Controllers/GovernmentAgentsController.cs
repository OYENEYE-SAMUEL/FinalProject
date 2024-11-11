using Application.DTOs;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WasteManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GovernmentAgentController : ControllerBase
    {
        private readonly IGovernmentAgentService _governmentAgentService;
        public GovernmentAgentController(IGovernmentAgentService governmentAgentService)
        {
            _governmentAgentService = governmentAgentService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(GovernmentAgentRequestModel request)
        {
            if (request == null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { Message = "Invalid Registration" });
            }

            var response = await _governmentAgentService.Register(request);
            if (!response.Status)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGovt(Guid id)
        {
            var response = await _governmentAgentService.Get(id);
            if (!response.Status)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }

        [HttpGet("allgovernments")]
        public async Task<IActionResult> GetAll()
        {
            var govt = await _governmentAgentService.GetAll();
            if (!govt.Status)
            {
                return BadRequest(govt.Message);
            }

            return Ok(govt.Data);
        }
    }
}
