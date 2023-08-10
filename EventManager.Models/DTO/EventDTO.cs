using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Models.DTO
{
    public class EventDTO
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int OrganizerId { get; set; }
        public int? AgeLimitLower { get; set; }
        public int? AgeLimitUpper { get; set; }
        public bool HasAgeLimit { get; set; }
        public byte[]? PosterImage { get; set; }
        public bool IsFreeToAttend { get; set; }
        public int? EventCapacity { get; set; }
        public int? CurrentOccupancy { get; set; }
        public bool IsPublished { get; set; }
        public AddressDTO? Address { get; set; }
    }
}
