using Application.DTOs;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WasteManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WasteReportsController : ControllerBase
    {
        private readonly IWasteReportService _wasteReportService;
        public WasteReportsController(IWasteReportService wasteReportService)
        {
            _wasteReportService = wasteReportService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateReport([FromBody] WasteReportRequestModel request)
        {
            if (request == null)
            {
                return BadRequest(new { Message = "Unable to make report" });
            }
            var response = await _wasteReportService.Create(request);
            if (!response.Status)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReport(Guid id)
        {
            var report = await _wasteReportService.Get(id);
            if (!report.Status)
            {
                return BadRequest(report.Message);
            }
            return Ok(report.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReport()
        {
            var reports = await _wasteReportService.GetAll();
            if (!reports.Status)
            {
                return BadRequest(reports.Message);
            }
            return Ok(reports.Data);
        }


    }
}
