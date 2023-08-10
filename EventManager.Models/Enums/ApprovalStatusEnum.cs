using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EventManager.Models.Enums
{
    public enum ApprovalStatusEnum
    {
        [Display(Name = "Approved")]
        Approved = 1,
        [Display(Name = "Rejected")]
        Rejected,
        [Display(Name = "InProgress")]
        InProgress
    }
}
