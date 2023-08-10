namespace EventManager.Models.DTO
{
    public class TicketDTO
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string? TransactionId { get; set; }
        public int? PaymentTierId { get; set; }
    }
}
