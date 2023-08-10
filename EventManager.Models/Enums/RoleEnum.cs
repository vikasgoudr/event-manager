using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EventManager.Models.Enums
{
    public enum RoleEnum
    {
        [Display(Name = "User")]
        User = 1,
        [Display(Name = "Organiser")]
        EventOrganizer,
        [Display(Name = "Admin")]
        SuperAdmin
    }
}
