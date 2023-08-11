using Microsoft.AspNetCore.Mvc;
using PayPal.Api;
using PayPal;

namespace SistemaProveeduriaCORE.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : Controller
    {
        [HttpPost]
        [Route("CreatePayment")]
        public async Task<IActionResult> CreatePayment(decimal amount, string currency, string description, string returnUrl, string cancelUrl)
        {
            try
            {
                var payment = PayPalHelper.CreatePayment(amount, currency, description, returnUrl, cancelUrl);
                return Ok(payment);
            }
            catch (PayPalException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("ExecutePayment")]
        public async Task<IActionResult> ExecutePayment(string paymentId, string token, string payerId)
        {
            try
            {
                var payment = PayPalHelper.ExecutePayment(paymentId, token, payerId);
                return Ok(payment);
            }
            catch (PayPalException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetPayment")]
        public async Task<IActionResult> GetPayment(string paymentId)
        {
            try
            {
                var payment = PayPalHelper.GetPayment(paymentId);
                return Ok(payment);
            }
            catch (PayPalException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

