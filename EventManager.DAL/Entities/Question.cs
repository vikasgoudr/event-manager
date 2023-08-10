using EventManager.DAL.EntityContracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.DAL.Entities
{
    [Table("Questions", Schema = ("Event"))]

    public class Question: Entity<int>, ISoftDeletable, ITrackable
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string? Options { get; set; }
        public int? defaultOption { get; set; }
        public string? defaultValue { get; set; }
        public bool IsRequired { get; set; }
        public int EventId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDt { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? CreatedDt { get; set; }
        public DateTime? LastUpdatedDt { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public virtual Event Event { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
