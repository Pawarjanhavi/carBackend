using Car2Go.DTOs;
using Car2Go.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Car2Go.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [Route("add-payment")]
        [HttpPost]
        public IActionResult AddPayment([FromBody] CreatePaymentDto createPaymentDto,int reservationId)
        {
            try
            {
                var result = _paymentService.CreatePayment(createPaymentDto, reservationId);

                if (result == false)
                {
                    return BadRequest();
                }
                return Ok(new{ result = true });
            }
            catch (Exception ex)
            {
                // Return error response in case of failure
                return BadRequest(new
                {
                    Message = "Error during payment.",
                    Details = ex.Message
                });
            }
        }
    }
}
