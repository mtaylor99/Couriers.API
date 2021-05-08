using Couriers.Database.Models;
using System.Linq;

namespace Couriers.Database.Interfaces
{
    public interface IJobStatusRepository
    {
        IQueryable<JobStatus> GetStatuses { get; }
    }
}
