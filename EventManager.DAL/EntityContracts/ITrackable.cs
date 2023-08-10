using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.DAL.EntityContracts
{
    public interface ITrackable
    {
        DateTime? CreatedDt { get; set; }
        DateTime? LastUpdatedDt { get; set; }
        DateTime? DeletedDt { get; set; }
        int? DeletedBy { get; set; }
        int? UpdatedBy { get; set; }
        int? CreatedBy { get; set; }
    }
}
