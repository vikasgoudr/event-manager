using EventManager.DAL.EntityContracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.DAL.Entities
{
    [Table("PaymentTiers", Schema = ("Event"))]
    public class PaymentTier: Entity<int>, ISoftDeletable
    {
        public string Name { get; set; }
        public double Amount { get; set; }
        public int AvailableTickets { get; set; }
        public int EventId { get; set; }
        public bool IsDeleted { get; set; }
        public virtual Event Event { get; set; }
    }
}
