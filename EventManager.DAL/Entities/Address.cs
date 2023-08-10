using EventManager.DAL.EntityContracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManager.DAL.Entities
{
    [Table("Address", Schema = ("General"))]
    public class Address: Entity<int>, ISoftDeletable
    {
        public string Line1 { get; set; }
        public string? Line2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<AppUser> Users { get; set; }
    }
}
