using Application.DTOs;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WasteManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndividualsController : ControllerBase
    {
        private readonly IIndividualService _individualService;
        public IndividualsController(IIndividualService individualService)
        {
            _individualService = individualService;
        }
        [HttpPost("registerperson")]
        public async Task<IActionResult> Register([FromBody] IndividualRequestModel request)
        {
            if (request == null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { Message = "Invalid Registration request" });
            }

            var response = await _individualService.Register(request);
            if (!response.Status)
            {
                return BadRequest(response);
            }

            return Ok(response.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerson(Guid id)
        {
            var response = await _individualService.GetById(id);
            if (!response.Status)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Data);
        }

        [HttpGet("allpersons")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _individualService.GetAll();
            if (!response.Status)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }
    }
}
