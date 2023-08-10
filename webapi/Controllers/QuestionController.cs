using EventManager.BLL.Contracts;
using EventManager.Models.DTO;
using Microsoft.AspNetCore.Mvc;
namespace EventManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }
        [HttpGet("get-form-data/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var res = await _questionService.GetQuestions(id);
            if (res!=null)
            {
                return Accepted(res);
            }
            return BadRequest();
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] QuestionDTO value)
        {
            var res = await _questionService.CreateQuestion(value);
            if (res)
            {
                return Accepted(res);
            }
            return BadRequest();
        }
        [HttpPut("update")]
        public async Task<IActionResult> Put(QuestionDTO value)
        {
            var res = await _questionService.UpdateQuestion(value);
            if (res)
            {
                return Accepted(res);
            }
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _questionService.DeleteQuestion(id);
            if (res)
            {
                return Accepted(res);
            }
            return BadRequest();
        }
    }
}
