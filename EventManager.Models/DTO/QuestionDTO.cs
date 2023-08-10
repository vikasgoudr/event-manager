using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Models.DTO
{
    public class QuestionDTO
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string? Options { get; set; }
        public int? defaultOption { get; set; }
        public string? defaultValue { get; set; }
        public bool IsRequired { get; set; }
        public int EventId { get; set; }
    }
}
