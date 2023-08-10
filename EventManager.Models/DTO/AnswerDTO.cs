namespace EventManager.Models.DTO
{
    public class AnswerDTO
    {
        public int Id { get; set; }
        public string Response { get; set; }
        public int QuestionId { get; set; }
        public int AnswererId { get; set; }
        public bool IsDeleted { get; set; }
        public int TicketId { get; set; }
    }
}
