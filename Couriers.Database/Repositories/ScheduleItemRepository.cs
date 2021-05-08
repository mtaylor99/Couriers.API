using Couriers.Database.Interfaces;
using Couriers.Database.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Couriers.Database.Repositories
{
    public class ScheduleItemRepository : IScheduleItemRepository
    {
        private CouriersDbContext context;

        public ScheduleItemRepository(CouriersDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<ScheduleItem> GetScheduleItems(int scheduleId)
        {
            return context.ScheduleItems
                .Include(jl => jl.JobLine)
                .ThenInclude(c => c.CollectionAddress)
                .Include(jl => jl.JobLine)
                .ThenInclude(d => d.DeliveryAddress)
                .OrderBy(s => s.DisplayOrder);
        }

        public bool UpdateScheduleItems(int scheduleId, List<ScheduleItem> scheduleItems)
        {
            var entities = context.ScheduleItems.Where(s => s.ScheduleId == scheduleId).ToList();

            for (var i = 0; i < scheduleItems.Count; i++)
            {
                if (entities.Any(x => x.ScheduleItemId == scheduleItems[i].ScheduleItemId)) // Update
                {
                    var entity = entities.First(x => x.ScheduleItemId == scheduleItems[i].ScheduleItemId);
                    entity.IsCollection = scheduleItems[i].IsCollection;
                    entity.DisplayOrder = i;
                    entity.Completed = scheduleItems[i].Completed;
                }
                else // Add
                {
                    scheduleItems[i].ScheduleId = scheduleId;
                    scheduleItems[i].DisplayOrder = i;
                    entities.Add(scheduleItems[i]);
                }
            }
            context.ScheduleItems.UpdateRange(entities);
            context.SaveChangesAsync();

            foreach (var entity in entities)
            {
                if (!scheduleItems.Any(x => x.ScheduleItemId == entity.ScheduleItemId)) // Delete
                {
                    context.ScheduleItems.Remove(entity);
                }
            }
            context.SaveChangesAsync();

            return true;
        }
    }
}
