using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Models.DTO
{
    public class UserProfileDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Gender { get; set; }
        public int? Rating { get; set; }
        public string? UserName { get; set; }
        public byte[]? DisplayPicture { get; set; }
        public string PhoneNumber { get; set; }
        public int? Age { get; set; }
        public int Role { get; set; }
        public int? AddressId { get; set; }
        public virtual AddressDTO? Address { get; set; }
        public string Email { get; set; }
    }
}
