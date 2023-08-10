using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EventManager.Models.Enums
{
    public enum GenderEnum
    {
        [Display(Name = "Female")]
        Female = 1,
        [Display(Name = "Male")]
        Male,
        [Display(Name = "Other")]
        Other
    }
}
