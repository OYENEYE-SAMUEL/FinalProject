using Application.DTOs;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WasteManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("makepayment")]
        public async Task<IActionResult> MakePayment([FromBody] PaymentRequestModel request)
        {
            if (request == null)
            {
                return BadRequest(new { Message = "Payment Failed" });
            }

            var response = await _paymentService.MakePayment(request);
            if (!response.Status)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Message);
        }

        [HttpGet("failedpayments")]
        public async Task<IActionResult> FailedPayment()
        {
            var payments = await _paymentService.GetAllFailedPayment();
            if (!payments.Status)
            {
                return NotFound(payments.Message);
            }

            return Ok(payments.Data);
        }

        [HttpGet("pendingpayments")]
        public async Task<IActionResult> PendingPayment()
        {
            var payments = await _paymentService.GetAllPendingPayment();
            if (!payments.Status)
            {
                return NotFound(payments.Message);
            }

            return Ok(payments.Data);
        }

        [HttpGet("succcesspayments")]
        public async Task<IActionResult> SuccessPayment()
        {
            var payments = await _paymentService.GetAllSuccessPayment();
            if (!payments.Status)
            {
                return NotFound(payments.Message);
            }

            return Ok(payments.Data);
        }
    }
}
