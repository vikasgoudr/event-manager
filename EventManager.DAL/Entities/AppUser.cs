using EventManager.DAL.EntityContracts;
using Microsoft.AspNetCore.Identity;

namespace EventManager.DAL.Entities
{
    public class AppUser: IdentityUser<int>, ISoftDeletable, ITrackable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Gender { get; set; }
        public int? Rating { get; set; }
        public byte[]? DisplayPicture { get; set; }
        public int? ApprovalStatus { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDt { get; set; }
        public int? DeletedBy { get; set; }
        public int? Age { get; set; }
        public DateTime? CreatedDt { get; set; }
        public DateTime? LastUpdatedDt { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? LastLogin { get; set; }
        public int? AddressId { get; set; }
        public virtual Address Address { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }

    }
}
