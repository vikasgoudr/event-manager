using EventManager.DAL.EntityContracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.DAL.Entities
{
    public class Entity<T>: IEntity<T>
    {
        [Key]
        public T Id { get; set; }
    }
}
