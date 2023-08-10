using EventManager.BLL.Contracts;
using EventManager.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EventManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentTierController : ControllerBase
    {
        private readonly IPaymentTierService _paymentTierService;

        public PaymentTierController(IPaymentTierService paymentTierService)
        {
            _paymentTierService = paymentTierService;
        }

        [HttpGet("get-payment-tier")]
        public async Task<IActionResult> GetPaymentTiersByEventId(int eventId)
        {
            var paymentTiers = await _paymentTierService.GetPaymentTiersByEventId(eventId);
            return Ok(paymentTiers);
        }

        [HttpPost("add-payment-tier")]
        public async Task<IActionResult> AddPaymentTier([FromBody]PaymentTierDTO paymentTierDto)
        {
            var added = await _paymentTierService.AddPaymentTier(paymentTierDto);
            if (added)
            {
                return Accepted(added);
            }
            return BadRequest();
        }

        [HttpPatch("event")]
        public async Task<IActionResult> UpdatePaymentTier(PaymentTierDTO paymentTierDto)
        {
            var updated = await _paymentTierService.UpdatePaymentTier(paymentTierDto);
            if (updated)
            {
                return Accepted(updated);
            }
            return NotFound();
        }

        [HttpDelete("event/{eventId}")]
        public async Task<IActionResult> DeletePaymentTiersByEventId(int eventId)
        {
            var deleted = await _paymentTierService.DeletePaymentTiersByEventId(eventId);
            if (deleted)
            {
                return Accepted(deleted);
            }
            return NotFound();
        }
    }
}
