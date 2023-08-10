using EventManager.DAL.EntityContracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManager.DAL.Entities
{
    [Table("Events", Schema = ("Event"))]
    public class Event: Entity<int>, ISoftDeletable, ITrackable
    {
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int OrganizerId { get; set; }
        public int? AgeLimitLower { get; set; }
        public int? AgeLimitUpper { get; set; }
        public bool HasAgeLimit { get; set; }
        public bool IsPublished { get; set; }
        public byte[]? PosterImage { get; set; }
        public bool IsFreeToAttend { get; set; }
        public int EventCapacity { get; set; }
        public int CurrentOccupancy { get; set; }
        public int AddressId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDt { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? CreatedDt { get; set; }
        public DateTime? LastUpdatedDt { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public virtual AppUser Organizer { get; set; }
        public virtual Address Address { get; set; }
        public virtual ICollection<Question> Questions { get; set; } = new HashSet<Question>();
        public virtual ICollection<PaymentTier> PaymentTiers { get; set; } = new HashSet<PaymentTier>();
        public virtual ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();
    }
}
