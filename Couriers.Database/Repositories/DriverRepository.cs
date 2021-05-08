using Couriers.Database.Interfaces;
using Couriers.Database.Models;
using System.Linq;

namespace Couriers.Database.Repositories
{
    public class DriverRepository : IDriverRepository
    {
        private CouriersDbContext context;

        public DriverRepository(CouriersDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Driver> GetDrivers => context.Drivers
            .OrderByDescending(d => d.FirstName).ThenBy(d => d.LastName);
    }
}
