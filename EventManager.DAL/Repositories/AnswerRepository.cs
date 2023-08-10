using EventManager.DAL.Context;
using EventManager.DAL.Contracts;
using EventManager.DAL.Entities;
using EventManager.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace EventManager.DAL.Repositories;

public class AnswerRepository : IAnswerRepository
{
    private readonly EventManagerDbContext _context;

    public AnswerRepository(EventManagerDbContext context)
    {
        _context = context;
    }
    public async Task<bool> CreateAnswer(Answer answer)
    {
        var res = await _context.Answers.AddAsync(answer);

        if (res != null)
        {
            var question = await _context.Questions.FirstOrDefaultAsync(x => x.Id == answer.QuestionId);
            var eventById = await _context.Events.FirstOrDefaultAsync(x => x.Id == question.EventId);
            eventById.CurrentOccupancy = eventById.CurrentOccupancy + 1;
            await _context.SaveChangesAsync();

            return true;
        }
        return false;
    }
    public async Task<List<EventQuestionAnswerDTO>> GetEventQuestionAnswers(int id)
    {
        var eventQuestionAnswers = await _context.Answers
            .Include(a => a.Question)
            .Include(a => a.Ticket)
            .Where(x => x.AnswererId == id && !x.IsDeleted && !x.Ticket.IsDeleted)
            .GroupBy(x => x.TicketId)
            .Select(group => new EventQuestionAnswerDTO
            {
                TicketDetails = new TicketDTO
                {
                    //EventId = (group.FirstOrDefault().Ticket.EventId),
                    TransactionId = group.FirstOrDefault().Ticket.TransactionId

                },
                PaymentTier = new PaymentTierDTO
                {
                    Name = group.FirstOrDefault().Ticket.PaymentTier.Name,
                    Amount = group.FirstOrDefault().Ticket.PaymentTier.Amount,
                },
                Event = new EventDTO
                {

                    //Id = group.FirstOrDefault().Question.Event.Id,
                    Name = group.FirstOrDefault().Question.Event.Name,
                    StartDate = group.FirstOrDefault().Question.Event.StartDate,
                    EndDate = group.FirstOrDefault().Question.Event.EndDate,
                    OrganizerId = group.FirstOrDefault().Question.Event.OrganizerId,
                    AgeLimitLower = group.FirstOrDefault().Question.Event.AgeLimitLower,
                    AgeLimitUpper = group.FirstOrDefault().Question.Event.AgeLimitUpper,
                    HasAgeLimit = group.FirstOrDefault().Question.Event.HasAgeLimit,
                    //PosterImage = group.FirstOrDefault().Question.Event.PosterImage,
                    IsFreeToAttend = group.FirstOrDefault().Question.Event.IsFreeToAttend,
                    //EventCapacity = group.FirstOrDefault().Question.Event.EventCapacity,
                    // CurrentOccupancy = group.FirstOrDefault().Question.Event.CurrentOccupancy,
                    // IsPublished = group.FirstOrDefault().Question.Event.IsPublished,
                    Address = new AddressDTO
                    {
                        // Id = group.FirstOrDefault().Question.Event.Address.Id,
                        // Line1 = group.FirstOrDefault().Question.Event.Address.Line1,
                        //Line2 = group.FirstOrDefault().Question.Event.Address.Line2,
                        City = group.FirstOrDefault().Question.Event.Address.City,
                        State = group.FirstOrDefault().Question.Event.Address.State,
                        Country = group.FirstOrDefault().Question.Event.Address.Country
                    }
                },
                QuestionAnswers = group.Select(x => new QuestionAnswerDTO
                {
                    QuestionName = x.Question.Name,
                    Answer = x.Response
                }).ToList()
            })
            .ToListAsync();

        return eventQuestionAnswers;
    }
    public async Task<List<EventQuestionAnswerDTO>> GetAllAnswers(int eventID, int userId)
    {
        var eventQuestionAnswers = await _context.Answers
            .Include(a => a.Question)
            .Include(a => a.Ticket)
            .Where(x => x.AnswererId == userId && x.Question.EventId == eventID && !x.IsDeleted && !x.Ticket.IsDeleted)
            .GroupBy(x => x.TicketId)
            .Select(group => new EventQuestionAnswerDTO
            {
                TicketDetails = new TicketDTO
                {
                    EventId = (group.FirstOrDefault().Ticket.EventId),
                    TransactionId = group.FirstOrDefault().Ticket.TransactionId

                },
                PaymentTier = new PaymentTierDTO
                {
                    Name = group.FirstOrDefault().Ticket.PaymentTier.Name,
                    Amount = group.FirstOrDefault().Ticket.PaymentTier.Amount,
                },
                Event = new EventDTO
                {

                    //Id = group.FirstOrDefault().Question.Event.Id,
                    Name = group.FirstOrDefault().Question.Event.Name,
                    StartDate = group.FirstOrDefault().Question.Event.StartDate,
                    EndDate = group.FirstOrDefault().Question.Event.EndDate,
                    OrganizerId = group.FirstOrDefault().Question.Event.OrganizerId,
                    AgeLimitLower = group.FirstOrDefault().Question.Event.AgeLimitLower,
                    AgeLimitUpper = group.FirstOrDefault().Question.Event.AgeLimitUpper,
                    HasAgeLimit = group.FirstOrDefault().Question.Event.HasAgeLimit,
                    //PosterImage = group.FirstOrDefault().Question.Event.PosterImage,
                    IsFreeToAttend = group.FirstOrDefault().Question.Event.IsFreeToAttend,
                    //EventCapacity = group.FirstOrDefault().Question.Event.EventCapacity,
                    // CurrentOccupancy = group.FirstOrDefault().Question.Event.CurrentOccupancy,
                    // IsPublished = group.FirstOrDefault().Question.Event.IsPublished,
                    Address = new AddressDTO
                    {
                        // Id = group.FirstOrDefault().Question.Event.Address.Id,
                        // Line1 = group.FirstOrDefault().Question.Event.Address.Line1,
                        //Line2 = group.FirstOrDefault().Question.Event.Address.Line2,
                        City = group.FirstOrDefault().Question.Event.Address.City,
                        State = group.FirstOrDefault().Question.Event.Address.State,
                        Country = group.FirstOrDefault().Question.Event.Address.Country
                    }
                },
                QuestionAnswers = group.Select(x => new QuestionAnswerDTO
                {
                    QuestionName = x.Question.Name,
                    Answer = x.Response
                }).ToList()
            })
            .ToListAsync();

        return eventQuestionAnswers;
    }


}
