using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Couriers.Database.Models
{
    [Table("ScheduleStatus")]
    public partial class ScheduleStatus
    {
        public ScheduleStatus()
        {
            Schedules = new HashSet<Schedule>();
        }

        [Key]
        public int ScheduleStatusId { get; set; }
        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        [InverseProperty(nameof(Schedule.ScheduleStatus))]
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
