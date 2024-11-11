using Application.DTOs;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WasteManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionsController : ControllerBase
    {
        private readonly ISubscrptionService _subscrptionService;
        public SubscriptionsController(ISubscrptionService subscrptionService)
        {
            _subscrptionService = subscrptionService;
        }

    /*    [Authorize("Admin")]*/
        [HttpPost("createplan")]
        public async Task<IActionResult> MakeSubcription([FromBody] SubscriptionRequestModel request)
        {
            if (request == null)
            {
                return BadRequest(new { Message = "Plan cannot be empty" });
            }

            var subscribe = await _subscrptionService.CreatePlan(request);
            if (!subscribe.Status)
            {
                return BadRequest(subscribe.Message);
            }

            return Ok(subscribe.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlan(Guid id)
        {
            var plan = await _subscrptionService.GetActivePlan(id);
            if (!plan.Status)
            {
                return BadRequest(plan.Message);
            }

            return Ok(plan.Data);
        }

        [HttpGet("pendingplan")]
        public async Task<IActionResult> GetPendingPlan()
        {
            var pending = await _subscrptionService.GetAllPlan();
            if (!pending.Status)
            {
                return BadRequest(pending.Message);
            }
            return Ok(pending.Data);
        }

        [HttpGet("allexpiredplan")]
        public async Task<IActionResult> AllExpiredPlan()
        {
            var expired = await _subscrptionService.AllExpiredPlan();
            if (!expired.Status)
            {
                return BadRequest(expired.Message);
            }

            return Ok(expired.Data);
        }

    }
}
