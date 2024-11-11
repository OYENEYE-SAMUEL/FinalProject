using Application.DTOs;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WasteManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunityController : ControllerBase
    {
        private readonly ICommunityService _communityService;
        public CommunityController(ICommunityService communityService)
        {
            _communityService = communityService;
        }

        [HttpPost("registercommunity")]
        public async Task<IActionResult> Register([FromBody] CommunityRequestModel request)
        {
            if (request == null || string.IsNullOrEmpty(request.Email))
            {
                return BadRequest(new { Message = "Invalid registration request" });
            }

            var response = await _communityService.Register(request);
            if (!response.Status)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Message);
        }

        [HttpGet("allcommunities")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _communityService.GetAll();
            if (!response.Status)
            {
                return NotFound(response.Message);

            }
                return Ok(response.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommunity(Guid id)
        {
            var response = await _communityService.GetById(id);
            if (!response.Status)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }
    }
}
    
