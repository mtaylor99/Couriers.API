using Couriers.Database.Models;
using System.Linq;

namespace Couriers.Database.Interfaces
{
    public interface IScheduleStatusRepository
    {
        IQueryable<ScheduleStatus> GetStatuses { get; }
    }
}
