using EventManager.BLL.Contracts;
using EventManager.BLL.Services;
using EventManager.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _answerService;
        public AnswerController(IAnswerService answerService) { 
            _answerService = answerService;
        }
        [HttpPost("create-answer")]
        public async Task<IActionResult> CreateAnswer(AnswerDTO answerDTO)
        {
            var data = await _answerService.CreateAnswer(answerDTO);
            if (data != null)
            {
                return Accepted(data);
            }
            return BadRequest();
        }

        [HttpGet("get-all-tickets-by-userId")]

        public async Task<IActionResult> GetEventQuestionAnswers(int userId)
        {
            var res = await _answerService.GetEventQuestionAnswers(userId);
            if (res != null)
            {
                return Accepted(res);
            }
            return BadRequest();
        }

        [HttpGet("get-all-ticket-details")]

        public async Task<IActionResult> GetAllAnswers(int eventId, int userId)
        {
            var res = await _answerService.GetAllAnswers(eventId, userId);
            if (res != null)
            {
                return Accepted(res);
            }
            return BadRequest();
        }
    }
}
