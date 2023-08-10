using AutoMapper;
using EventManager.BLL.Contracts;
using EventManager.DAL.Contracts;
using EventManager.DAL.Entities;
using EventManager.Models.DTO;
using EventManager.Util.Models;
using Microsoft.Extensions.Logging;

namespace EventManager.BLL.Services
{ 
    public class EventService : IEventService
    {
        private readonly IEventRepository _repo;
        private readonly IMapper _mapper;

        public EventService(IEventRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        
        public async Task<PagedList<EventDTO>> AllEvents(PagerSettings pagerSettings)
        {
            var events = await _repo.GetAllEvents(pagerSettings);
            if (events != null)
            {
                return new PagedList<EventDTO>()
                {
                    CurrentPage = events.CurrentPage,
                    PageCount = events.PageCount,
                    PageSize = events.PageSize,
                    RowCount = events.RowCount,
                    Data = _mapper.Map<List<EventDTO>>(events.Data)
                };
            }
            return new PagedList<EventDTO>()
            {
                CurrentPage = 1,
                PageCount = 1,
                PageSize = 1,
                RowCount = 0,
                Data = new List<EventDTO>()
            };
        }

       
        public async Task<PagedList<EventDTO>> AllUserEvents(int userId, PagerSettings pagerSettings)
        {
            var events = await _repo.GetEventsByUserId(userId, pagerSettings);
            if (events != null)
            {
                return new PagedList<EventDTO>()
                {
                    CurrentPage = events.CurrentPage,
                    PageCount = events.PageCount,
                    PageSize = events.PageSize,
                    RowCount = events.RowCount,
                    Data = _mapper.Map<List<EventDTO>>(events.Data)
                };
            }
            return new PagedList<EventDTO>()
            {
                CurrentPage = 1,
                PageCount = 1,
                PageSize = 1,
                RowCount = 0,
                Data = new List<EventDTO>()
            };
        }

        
        public async Task<PagedList<EventDTO>> AllUserPastEvents(int userId, PagerSettings pagerSettings)
        {
            var events = await _repo.GetPastEventsByUserId(userId, pagerSettings);
            if (events != null)
            {
                return new PagedList<EventDTO>()
                {
                    CurrentPage = events.CurrentPage,
                    PageCount = events.PageCount,
                    PageSize = events.PageSize,
                    RowCount = events.RowCount,
                    Data = _mapper.Map<List<EventDTO>>(events.Data)
                };
            }
            return new PagedList<EventDTO>()
            {
                CurrentPage = 1,
                PageCount = 1,
                PageSize = 1,
                RowCount = 0,
                Data = new List<EventDTO>()
            };
        }
 
        public async Task<int> CreateEvent(EventDTO eventDto)
        {
            var created = await _repo.CreateEvent(_mapper.Map<Event>(eventDto));
            return created;
        }


 

        public async Task<bool> DeleteEvent(int eventId)
        {
            var deleted = await _repo.DeleteEvent(eventId);
            return deleted;
        }

       
        public async Task<PagedList<EventDTO>> FilterEvents(FilterDTO filterDTO)
        {
            var events = await _repo.FilterEvents(filterDTO);
            if (events != null)
            {
                return new PagedList<EventDTO>()
                {
                    CurrentPage = events.CurrentPage,
                    PageCount = events.PageCount,
                    PageSize = events.PageSize,
                    RowCount = events.RowCount,
                    Data = _mapper.Map<List<EventDTO>>(events.Data)
                };
            }
            return new PagedList<EventDTO>()
            {
                CurrentPage = 1,
                PageCount = 1,
                PageSize = 1,
                RowCount = 0,
                Data = new List<EventDTO>()
            };
        }

       
        public async Task<EventDTO?> GetEventById(int eventId)
        {
            var result = await _repo.GetEventById(eventId);
            if (result == null)
            {
                return null;
            }
            return _mapper.Map<EventDTO>(result);
        }

         
        public async Task<PagedList<RegisteredUsersDto>> GetRegisteredUsers(int eventId, PagerSettings pagerSettings)
        {
            var users = await _repo.GetRegisteredUsers(eventId, pagerSettings);
            if (users != null)
            {
                return new PagedList<RegisteredUsersDto>()
                {
                    CurrentPage = users.CurrentPage,
                    PageCount = users.PageCount,
                    PageSize = users.PageSize,
                    RowCount = users.RowCount,
                    Data = _mapper.Map<List<RegisteredUsersDto>>(users.Data)
                };
            }
            return new PagedList<RegisteredUsersDto>()
            {
                CurrentPage = 1,
                PageCount = 1,
                PageSize = 1,
                RowCount = 0,
                Data = new List<RegisteredUsersDto>()
            };
        }



        
        public async Task<List<EventDTO>> GetTopNEvents(int count)
        {
            var events = await _repo.GetTopNEvents(count);
            return _mapper.Map<List<EventDTO>>(events);
        }



     
        public Task<bool> PublishEvent(PublishDTO data)
        {
            return _repo.PublishEvent(data);
        }


        
        public async Task<bool> UpdateEvent(EventDTO eventDto)
        {
            var updated = await _repo.UpdateEvent(_mapper.Map<Event>(eventDto));
            return updated;
        }
    }
}
