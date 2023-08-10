using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Models.Models
{
    public class PaymentTier
    {
        public string Name { get; set; }
        public double Amount { get; set; }
        public int AvailableTickets { get; set; }
        public int EventId { get; set; }
    }
}
