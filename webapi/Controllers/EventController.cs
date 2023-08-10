using EventManager.BLL.Contracts;
using EventManager.Models.DTO;
using EventManager.Util.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost("all")]
        public async Task<IActionResult> GetAll(PagerSettings pagerSettings)
        {
            var events = await _eventService.AllEvents(pagerSettings);
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _eventService.GetEventById(id);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost("filterByText")]
        public async Task<IActionResult> FilterEvents(FilterDTO filterDTO)
        {
            var events = await _eventService.FilterEvents(filterDTO);
            if (events != null)
            {
                return Ok(events);
            }
            return BadRequest();
        }

        [HttpPost("user-events/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId, PagerSettings pagerSettings)
        {
            var events = await _eventService.AllUserEvents(userId, pagerSettings);
            return Ok(events);
        }
        [HttpPost("past-events/{userId}")]
        public async Task<IActionResult> GetPastEventsByUserId(int userId, PagerSettings pagerSettings)
        {
            var events = await _eventService.AllUserPastEvents(userId, pagerSettings);
            return Ok(events);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var deleted = await _eventService.DeleteEvent(id);
            if(deleted)
            {
                return Accepted(deleted);
            }
            return NotFound();
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateEvent(EventDTO eventDTO)
        {
            var created = await _eventService.CreateEvent(eventDTO);
            if(created>0)
            {
                return Accepted(created);
            }
            return BadRequest();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateEvent(EventDTO eventDTO)
        {
            var updated = await _eventService.UpdateEvent(eventDTO);
            if(updated)
            {
                return Accepted(updated);
            }
            return NotFound();
        }

        [HttpGet("top/{count}")]
        public async Task<IActionResult> GetTopN(int count)
        {
            var events = await _eventService.GetTopNEvents(count);
            return Ok(events);
        }

        [HttpPatch("Publish")]
        public async Task<IActionResult> PublishEvent(PublishDTO publishDTO)
        {
            var result = await _eventService.PublishEvent(publishDTO);
            if (result)
            {
                return Accepted(result);
            }
            return NotFound();
        }

        [HttpPost("registered-users/{id}")]
        public async Task<IActionResult> GetRegisteredUsers(int id,PagerSettings pagerSettings)
        {
            var result = await _eventService.GetRegisteredUsers(id,pagerSettings);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
