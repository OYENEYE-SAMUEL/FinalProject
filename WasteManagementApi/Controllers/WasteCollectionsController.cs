using Application.DTOs;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WasteManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WasteCollectionsController : ControllerBase
    {
        private readonly IWasteCollectionService _collection;
        public WasteCollectionsController(IWasteCollectionService collection)
        {
            _collection = collection;
        }

        [HttpPost("create")]
       /* [Authorize("Individual")]*/
        public async Task<IActionResult> WasteRequest([FromBody] WasteCollectionRequestModel reqest)
        {
            if (reqest == null)
            {
                return BadRequest(new { Message = "Waste Request cannot be book" });
            }

            var response = await _collection.Create(reqest);
            if (!response.Status)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRequest(Guid id)
        {
            var waste = await _collection.GetById(id);
            if (waste.Status)
            {
                return BadRequest(waste.Message);
            }
            return Ok(waste.Data);
        }

        [HttpGet("allconnections")]
        public async Task<IActionResult> GetAllWasteRequest()
        {
            var wastes = await _collection.GetAll();
            if (!wastes.Status)
            {
                return BadRequest(wastes.Message);
            }

            return Ok(wastes.Data);
        }

        [HttpGet("staffassigned")]
        public async Task<IActionResult> GetWasteAssign(Guid staffId)
        {
            var collection = await _collection.AssignStaffs(staffId);
            if (!collection.Status)
            {
                return BadRequest(collection.Message);
            }
            return Ok(collection.Data);
        }

		[HttpPost("assignrequest")]
        public async Task<IActionResult> AssignRequestToStaff(Guid staffId)
        {
            var implement = await _collection.RequestAssignedToStaff(staffId);
            if (!implement.Status)
            {
                return BadRequest(implement.Message);
            }

            return Ok(implement.Data);
        }

      
    }
}
