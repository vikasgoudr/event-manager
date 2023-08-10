using EventManager.DAL.EntityContracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManager.DAL.Entities
{
    [Table("Answers", Schema = ("Event"))]

    public class Answer: Entity<int>, ISoftDeletable, ITrackable
    {
        public int? TicketId { get; set; }
        public string Response { get; set; }
        public int QuestionId { get; set; }
        public int AnswererId { get; set; }
        public int NumberOfRegistrations { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDt { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? CreatedDt { get; set; }
        public DateTime? LastUpdatedDt { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public virtual Question Question { get; set; }
        public virtual AppUser Answerer { get; set; }
        public virtual Ticket? Ticket { get; set; }
    }
}
