using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Couriers.Database.Models
{
    [Table("Schedule")]
    public partial class Schedule
    {
        public Schedule()
        {
            ScheduleItems = new HashSet<ScheduleItem>();
        }

        [Key]
        public int ScheduleId { get; set; }
        public int DriverId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
        public int ScheduleStatusId { get; set; }

        [ForeignKey(nameof(ScheduleStatusId))]
        [InverseProperty("Schedules")]
        public virtual ScheduleStatus ScheduleStatus { get; set; }
        [InverseProperty(nameof(ScheduleItem.Schedule))]
        public virtual ICollection<ScheduleItem> ScheduleItems { get; set; }
    }
}
