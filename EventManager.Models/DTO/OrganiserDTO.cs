using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Models.DTO
{
    public class OrganiserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Gender { get; set; }
        public int? ApprovalStatus { get; set; }
        public string? UserName { get; set; }
        public string PhoneNumber { get; set; }
        public int? Age { get; set; }
        public AddressDTO Address { get; set; }
    }
}
