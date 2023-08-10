using EventManager.BLL.Contracts;
using EventManager.BLL.Services;
using EventManager.DAL.Entities;
using EventManager.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EventManager.API.Controllers
{
    public class TicketController : Controller
    {
        ITicketService _ticketService;
        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost("create-ticket")]
        public async Task<IActionResult> CreateTicket([FromBody]TicketDTO ticket)
        {
              var res= await  _ticketService.CreateTicket(ticket);
            if(res!=null)
            {
                return Accepted(res);
            }
            return BadRequest(res);
        }

        [HttpDelete("cancel-ticket")]
        public async Task<IActionResult> CancelTicket(string transactionId)
        {
            var res = await _ticketService.CancelTicket(transactionId);
            if (res != null)
            {
                return Accepted(res);
            }
            return BadRequest(res);
        }


    }
}
