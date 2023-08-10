using EventManager.DAL.Context;
using EventManager.DAL.Contracts;
using EventManager.DAL.Entities;
using EventManager.Models.DTO;
using EventManager.Util.ExtensionMethods;
using EventManager.Util.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace EventManager.DAL.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly EventManagerDbContext _context;

        public EventRepository(EventManagerDbContext context)
        {
            _context = context;
        }
        public async Task<bool> DeleteEvent(int id)
        {
            var result = await _context.Events.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefaultAsync();
            if (result == null)
            {
                return false;
            }
            result.IsDeleted = true;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<PagedList<Event>> GetAllEvents(PagerSettings pagerSettings)
        {
            var events = await _context.Events.Include(x => x.Address)
                                                .Where(x => !x.IsDeleted && x.IsPublished == true && x.EndDate >= DateTime.UtcNow)
                                                .OrderBy(x => x.Id)
                                                .ToPagedListAsync(pagerSettings);
            return events;
        }
        public async Task<PagedList<Event>> FilterEvents(FilterDTO filterDTO)
        {
            var pagerSettings = new PagerSettings()
            {
                PageNumber = filterDTO.PageNumber,
                PageSize = filterDTO.PageSize,
            };
            Console.WriteLine(filterDTO.FreeToAttend.HasValue+" 55");
            var events = await _context.Events.Include(x => x.Address)
                .Where(x => !x.IsDeleted && x.IsPublished == true && (filterDTO.FreeToAttend == true ? x.IsFreeToAttend == true : filterDTO.FreeToAttend == false ? x.IsFreeToAttend == false : true) && (string.IsNullOrEmpty(filterDTO.StartDate.ToLongDateString()) || x.StartDate >= filterDTO.StartDate) && x.EndDate <= filterDTO.EndDate && (x.Name.Contains(filterDTO.FilterText.ToLower()) ||x.Name.Contains(filterDTO.FilterText.ToUpper())))
                                                .OrderBy(x => x.Id)
                                                .ToPagedListAsync(pagerSettings);
            //Console.WriteLine(filterDTO.StartDate+" "+filterDTO.EndDate);
            if (events != null)
            {
                return events;
            }
            return null;
        }
        public async Task<PagedList<Event>?> GetEventsByUserId(int userId, PagerSettings pagerSettings)
        {
            var userExits = await _context.Users.AnyAsync(x => x.Id == userId);
            if (!userExits)
            {
                return null;
            }
            var events = await _context.Events.Include(x => x.Address)
                                                .Where(x => x.OrganizerId == userId && !x.IsDeleted && x.EndDate >= DateTime.UtcNow)
                                                .ToPagedListAsync(pagerSettings);
            return events;
        }
        public async Task<PagedList<Event>?> GetPastEventsByUserId(int userId, PagerSettings pagerSettings)
        {
            var userExits = await _context.Users.AnyAsync(x => x.Id == userId);
            if (!userExits)
            {
                return null;
            }
            var events = await _context.Events.Include(x => x.Address)
                                                .Where(x => x.OrganizerId == userId && !x.IsDeleted && x.EndDate < DateTime.UtcNow)
                                                .ToPagedListAsync(pagerSettings);
            return events;
        }

        public async Task<Event?> GetEventById(int id)
        {
            var result = await _context.Events.Include(x => x.Address)
                                                .Where(x => x.Id == id && !x.IsDeleted)
                                                .FirstOrDefaultAsync();
            return result;
        }


        public async Task<bool> UpdateEvent(Event newEvent)
        {
            var oldEvent = await _context.Events.Where(x => x.Id == newEvent.Id && !x.IsDeleted).FirstOrDefaultAsync();
            if (oldEvent == null)
            {
                return false;
            }
            oldEvent.Name = newEvent.Name;
            oldEvent.StartDate = newEvent.StartDate;
            oldEvent.EndDate = newEvent.EndDate;
            oldEvent.HasAgeLimit = newEvent.HasAgeLimit;
            oldEvent.AgeLimitLower = newEvent.AgeLimitLower;
            oldEvent.AgeLimitUpper = newEvent.AgeLimitUpper;
            oldEvent.PosterImage = newEvent.PosterImage;
            oldEvent.IsFreeToAttend = newEvent.IsFreeToAttend;
            oldEvent.EventCapacity = newEvent.EventCapacity;

            oldEvent.UpdatedBy = newEvent.UpdatedBy;
            oldEvent.LastUpdatedDt = DateTime.Now;

            if (newEvent.Address != null)
            {
                oldEvent.Address = newEvent.Address;
            }

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<int> CreateEvent(Event newEvent)
        {
            try
            {
            await _context.Events.AddAsync(newEvent);
                await _context.SaveChangesAsync();
                await _context.Events.Entry(newEvent).ReloadAsync();
                return newEvent.Id;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<List<Event>> GetTopNEvents(int count)
        {
            Console.WriteLine(DateTime.UtcNow + "UTC");
            var events = await _context.Events.Where(x => !x.IsDeleted && x.IsPublished==true && x.EndDate>=DateTime.UtcNow).OrderByDescending(x => x.CurrentOccupancy).Take(count).ToListAsync();
            foreach( var e in events)
            {
                Console.WriteLine(e.EndDate + "END");
            }
            return events;
        }

        public async Task<bool> PublishEvent(PublishDTO data)
        {
            var eventById = _context.Events.FirstOrDefault(x => x.Id == data.EventId);
            if (data.PublishStatus == true)
            {
                eventById.IsPublished = true;
            }
            else
            {
                eventById.IsPublished = false;
            }
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<PagedList<AppUser>> GetRegisteredUsers(int eventId,PagerSettings pagerSettings)
        {
            var eventQuestionAnswers = await _context.Questions.Include(x => x.Answers).Where(x => x.EventId == eventId).SelectMany(x => x.Answers).ToListAsync();
            var answerers = eventQuestionAnswers.Select(x => x.AnswererId).ToList();

            var distinctAnswerers = new HashSet<int>();
            foreach (var answererId in answerers) distinctAnswerers.Add(answererId);

            var users = await _context.Users.Where(x => distinctAnswerers.Contains(x.Id)).ToPagedListAsync(pagerSettings);

            return users;
        }

    }
}
