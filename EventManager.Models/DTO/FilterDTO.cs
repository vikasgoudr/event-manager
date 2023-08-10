using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Models.DTO
{
    public class FilterDTO
    {
        public string FilterText{get; set;}
        public DateTime StartDate { get; set;}
        public DateTime EndDate { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool? FreeToAttend { get; set; }
    }
}
