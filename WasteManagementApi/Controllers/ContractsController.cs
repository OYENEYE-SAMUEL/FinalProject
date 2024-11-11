using Application.DTOs;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WasteManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly IContractService _contract;
        public ContractController(IContractService contract)
        {
            _contract = contract;
        }

    
        [HttpPost("createcontract")]
        public async Task<IActionResult> CreateContract([FromBody] ContractRequestModel request)
        {
            if (request == null)
            {
                return BadRequest(new { Message = "Contract cannot be created" });
            }

            var response = await _contract.CreateContract(request);
            if (!response.Status)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContract(Guid id)
        {
            var response = await _contract.GetContract(id);
            if (!response.Status)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Data);
        }

        [HttpGet("activecontracts")]
        public async Task<IActionResult> ActiveContract()
        {
            var contracts = await _contract.GetAllContracts();
            if (!contracts.Status)
            {
                return BadRequest(contracts.Message);
            }
            return Ok(contracts.Data);
        }

        [HttpGet("pendingcontracts")]
        public async Task<IActionResult> PendingContracts()
        {
            var contracts = await _contract.GetAllContracts();
            if (!contracts.Status)
            {
                return BadRequest(contracts.Message);
            }
            return Ok(contracts.Data);
        }
    }
}
