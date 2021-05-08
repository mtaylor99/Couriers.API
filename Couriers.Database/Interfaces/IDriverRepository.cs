using Couriers.Database.Models;
using System.Linq;

namespace Couriers.Database.Interfaces
{
    public interface IDriverRepository
    {
        IQueryable<Driver> GetDrivers { get; }
    }
}
