using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Models.DTO
{
    public class RegisteredUsersDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int Gender { get; set; }
        public int? Age { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
      
}
