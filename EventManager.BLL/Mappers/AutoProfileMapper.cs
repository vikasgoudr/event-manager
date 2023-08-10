using EventManager.DAL.Entities;
using EventManager.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.BLL.Mappers
{
    public class AutoProfileMapper : AutoMapper.Profile
    {
        public AutoProfileMapper()
        {
            CreateMap<Address, AddressDTO>().ReverseMap();
            CreateMap<Event, EventDTO>().ReverseMap();
            CreateMap<AppUser, OrganiserDTO>().ReverseMap();
            CreateMap<AppUser, UserDTO>().ReverseMap();
            CreateMap<Question, QuestionDTO>().ReverseMap();
            CreateMap<Answer, AnswerDTO>().ReverseMap();
            CreateMap<PaymentTier, PaymentTierDTO>().ReverseMap();
            CreateMap<AppUser, RegisteredUsersDto>().ReverseMap();
            CreateMap<Ticket, TicketDTO>().ReverseMap();
            CreateMap<AppUser, UserProfileDTO>().ReverseMap();
        }
    }
}
