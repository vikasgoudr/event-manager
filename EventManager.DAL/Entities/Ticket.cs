using EventManager.DAL.EntityContracts;

namespace EventManager.DAL.Entities
{
    public class Ticket : Entity<int>, ISoftDeletable, ITrackable
    {
        public int EventId { get; set; }
        public string? TransactionId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreatedDt { get; set; }
        public DateTime? LastUpdatedDt { get; set; }
        public DateTime? DeletedDt { get; set; }
        public int? DeletedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public int? PaymentTierId { get; set; }

        public virtual ICollection<Answer> Answers { get; set; } = new HashSet<Answer>();
        public virtual Event Event { get; set; }
        public virtual PaymentTier PaymentTier { get; set; }
    }
}
