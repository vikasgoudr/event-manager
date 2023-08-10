using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Models.DTO
{
    public class EventQuestionAnswerDTO
    {
        public TicketDTO TicketDetails { get; set; }
        public EventDTO Event { get; set; }

        public PaymentTierDTO PaymentTier { get; set; }
        public List<QuestionAnswerDTO> QuestionAnswers { get; set; }
    }
}
